// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="LogisticsSponsor.cs" company="Softo">
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
    /// <summary>LogisticSponsor</summary>
    public class LogisticSponsor : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 100;

        public string Name { get; private set; }
        public bool IsAirfareTicketRequired { get; private set; }
        public bool IsOtherRequired { get; protected set; }

        public virtual ICollection<AttendeeLogisticSponsor> AttendeeLogisticSponsors { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsor"/> class.</summary>
        /// <param name="logisticSponsorUid">The logistic sponsor uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="names">The names.</param>
        /// <param name="isOther">if set to <c>true</c> [is other].</param>
        /// <param name="userId">The user identifier.</param>
        public LogisticSponsor(Guid logisticSponsorUid, Edition edition, List<TranslatedName> names, bool isOther, int userId)
        {
            //this.Uid = logisticSponsorUid;
            this.UpdateName(names);
            this.IsAirfareTicketRequired = false;
            this.IsOtherRequired = false;
            this.SynchronizeAttendeeLogisticSponsors(edition, isOther, userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsor"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public LogisticSponsor(string name, int userId)
        {
            this.Name = name?.Trim();
            this.IsAirfareTicketRequired = false;
            this.IsOtherRequired = false;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsor"/> class.</summary>
        protected LogisticSponsor()
        {
        }

        /// <summary>Updates the specified names.</summary>
        /// <param name="names">The names.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="isAirfareTicketRequired">if set to <c>true</c> [is airfare ticket required].</param>
        /// <param name="isOther">if set to <c>true</c> [is other].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            List<TranslatedName> names, 
            Edition edition, 
            bool isAirfareTicketRequired,
            bool isOther,
            int userId)
        {
            this.UpdateName(names);
            this.IsAirfareTicketRequired = isAirfareTicketRequired;
            this.IsOtherRequired = false;
            this.SynchronizeAttendeeLogisticSponsors(edition, isOther, userId);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(Edition edition, int userId)
        {
            this.DeleteAttendeeLogisticSponsors(edition, userId);

            if (this.FindAllAttendeeLogisticSponsorsNotDeleted(null)?.Any() == false)
            {
                this.IsDeleted = true;
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Attendee Logistic Sponsors

        /// <summary>Synchronizes the attendee logistic sponsors.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="isOther">if set to <c>true</c> [is other].</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeLogisticSponsors(Edition edition, bool isOther, int userId)
        {
            if (this.AttendeeLogisticSponsors == null)
            {
                this.AttendeeLogisticSponsors = new List<AttendeeLogisticSponsor>();
            }

            if (edition == null)
            {
                return;
            }

            var attendeePlace = this.GetAttendeeLogisticSponsorByEditionId(edition.Id);
            if (attendeePlace != null)
            {
                attendeePlace.Update(isOther, userId);
            }
            else
            {
                this.AttendeeLogisticSponsors.Add(new AttendeeLogisticSponsor(edition, this, isOther, userId));
            }
        }

        /// <summary>Deletes the attendee logistic sponsors.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeLogisticSponsors(Edition edition, int userId)
        {
            foreach (var attendeeLogisticSponsor in this.FindAllAttendeeLogisticSponsorsNotDeleted(edition))
            {
                attendeeLogisticSponsor?.Delete(userId);
            }
        }

        /// <summary>Gets the attendee logistic sponsor by edition identifier.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        private AttendeeLogisticSponsor GetAttendeeLogisticSponsorByEditionId(int editionId)
        {
            return this.AttendeeLogisticSponsors?.FirstOrDefault(als => als.EditionId == editionId);
        }

        /// <summary>Finds all attendee logistic sponsors not deleted.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private List<AttendeeLogisticSponsor> FindAllAttendeeLogisticSponsorsNotDeleted(Edition edition)
        {
            return this.AttendeeLogisticSponsors?.Where(als => (edition == null || als.EditionId == edition.Id) && !als.IsDeleted)?.ToList();
        }

        #endregion

        #region Names

        /// <summary>Updates the name.</summary>
        /// <param name="names">The names.</param>
        private void UpdateName(List<TranslatedName> names)
        {
            var name = string.Empty;
            foreach (var languageCode in Language.CodesOrder)
            {
                name += (!string.IsNullOrEmpty(name) ? " " + Language.Separator + " " : String.Empty) +
                        names?.FirstOrDefault(vtc => vtc.Language.Code == languageCode)?.Value;
            }

            this.Name = name;
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
            
            ValidateName();

            return this.ValidationResult.IsValid;
        }
        
        /// <summary>Validates the first name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        #endregion
    }
}