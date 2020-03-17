// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-24-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="Place.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Place</summary>
    public class Place : Entity
    {
        public static readonly int NameMaxLength = 100;
        public static readonly int WebsiteMaxLength = 300;
        public static readonly int AdditionalInfoMaxLength = 1000;

        public string Name { get; private set; }
        public bool IsHotel { get; private set; }     
        public bool IsAirport { get; private set; }
        public int? AddressId { get; private set; }
        public string Website { get; private set; }
        public string AdditionalInfo { get; private set; }
        
        public virtual Address Address { get; private set; }

        public virtual ICollection<AttendeePlace> AttendeePlaces { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Place"/> class.</summary>
        /// <param name="placeUid">The place uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="website">The website.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public Place(Guid placeUid, Edition edition, string name, string type, string website, string additionalInfo, int userId)
        {
            //this.Uid = placeUid;
            this.Name = name?.Trim();
            this.Website = website?.Trim();
            this.AdditionalInfo = additionalInfo;
            this.UpdateType(type);
            this.SynchronizeAttendeePlaces(edition, userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Place"/> class.</summary>
        protected Place()
        {
        }

        /// <summary>Updates the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="website">The website.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(Edition edition, string name, string type, string website, string additionalInfo, int userId)
        {
            this.Name = name?.Trim();
            this.Website = website?.Trim();
            this.AdditionalInfo = additionalInfo;
            this.UpdateType(type);
            this.SynchronizeAttendeePlaces(edition, userId);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="website">The website.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(string name, string type, string website, string additionalInfo, int userId)
        {
            this.Name = name?.Trim();
            this.Website = website?.Trim();
            this.AdditionalInfo = additionalInfo;
            this.UpdateType(type);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(Edition edition, int userId)
        {
            this.DeleteAttendeePlaces(edition, userId);

            if (this.FindAllAttendeePlacesNotDeleted(null)?.Any() == false)
            {
                this.IsDeleted = true;
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Attendee Places

        /// <summary>Synchronizes the attendee places.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeePlaces(Edition edition, int userId)
        {
            if (this.AttendeePlaces == null)
            {
                this.AttendeePlaces = new List<AttendeePlace>();
            }

            if (edition == null)
            {
                return;
            }

            var attendeePlace = this.GetAttendeePlaceByEditionId(edition.Id);
            if (attendeePlace != null)
            {
                attendeePlace.Update(userId);
            }
            else
            {
                this.AttendeePlaces.Add(new AttendeePlace(edition,this, userId));
            }
        }

        /// <summary>Deletes the attendee places.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeePlaces(Edition edition, int userId)
        {
            foreach (var attendeePlace in this.FindAllAttendeePlacesNotDeleted(edition))
            {
                attendeePlace?.Delete(userId);
            }
        }

        /// <summary>Gets the attendee place by edition identifier.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        private AttendeePlace GetAttendeePlaceByEditionId(int editionId)
        {
            return this.AttendeePlaces?.FirstOrDefault(ac => ac.EditionId == editionId);
        }


        /// <summary>Finds all attendee places not deleted.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private List<AttendeePlace> FindAllAttendeePlacesNotDeleted(Edition edition)
        {
            return this.AttendeePlaces?.Where(ap => (edition == null || ap.EditionId == edition.Id) && !ap.IsDeleted)?.ToList();
        }

        #endregion

        #region Type

        /// <summary>Updates the type.</summary>
        /// <param name="type">The type.</param>
        private void UpdateType(string type)
        {
            this.IsHotel = false;
            this.IsAirport = false;

            if (type == "Hotel")
            {
                this.IsHotel = true;
            }
            else if (type == "Airport")
            {
                this.IsAirport = true;
            }
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.ValidateName();
            this.ValidateWebsite();
            this.ValidateAdditionalInfo();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, 1), new string[] { "Name" }));
            }
        }

        /// <summary>Validates the website.</summary>
        public void ValidateWebsite()
        {
            if (!string.IsNullOrEmpty(this.Website?.Trim()) && this.Website?.Trim().Length > WebsiteMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Website, WebsiteMaxLength, 1), new string[] { "Website" }));
            }
        }

        /// <summary>Validates the additional information.</summary>
        public void ValidateAdditionalInfo()
        {
            if (!string.IsNullOrEmpty(this.AdditionalInfo?.Trim()) && this.AdditionalInfo?.Trim().Length > AdditionalInfoMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AdditionalInfo, AdditionalInfoMaxLength, 1), new string[] { "AdditionalInfo" }));
            }
        }

        #endregion
    }
}