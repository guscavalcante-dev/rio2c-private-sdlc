// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
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
        public static readonly int NameMinLength = 5;
        public static readonly int NameMaxLength = 100;

        public string Name { get; private set; }
        public bool IsAirfareTicketRequired { get; private set; }
        public bool IsOtherRequired { get; protected set; }
        
        protected LogisticSponsor(){}

        public LogisticSponsor(
            List<TranslatedName> names,
            int userId)
        {
            UpdateName(names);

            this.IsDeleted = false;
            this.IsAirfareTicketRequired = false;
            this.IsOtherRequired = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }
        public LogisticSponsor(string name, int userId)
        {
            this.Name = name;
            this.IsDeleted = false;
            this.IsAirfareTicketRequired = false;
            this.IsOtherRequired = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        public virtual ICollection<AttendeeLogisticSponsor> AttendeeLogisticSponsors { get; private set; }

        public void Update(List<TranslatedName> names, 
            Edition edition, 
            bool isAirfareTicketRequired,
            bool isAddingToCurrentEdition, 
            int userId)
        {
            this.IsDeleted = false;
            this.IsAirfareTicketRequired = isAirfareTicketRequired;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.UpdateName(names);
            this.SynchronizeAttendeeSponsors(edition, isAddingToCurrentEdition, userId);
        }

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

        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            
            foreach(var attendee in this.AttendeeLogisticSponsors)
            {
                attendee.Delete(userId);
            }
        }

        /// <summary>Synchronizes the attendee collaborators.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeSponsors(
            Edition edition, 
            bool isAddingToCurrentEdition, 
            int userId)
        {
            // Synchronize only when is adding to current edition
            if (!isAddingToCurrentEdition)
            {
                return;
            }

            if (this.AttendeeLogisticSponsors == null)
            {
                this.AttendeeLogisticSponsors = new List<AttendeeLogisticSponsor>();
            }

            if (edition == null)
            {
                return;
            }

            var attendeeSponsor = this.GetAttendeeByEditionId(edition.Id);
            if (attendeeSponsor != null)
            {
                attendeeSponsor.Update(edition, this, userId);
            }
            else
            {
                this.AttendeeLogisticSponsors.Add(new AttendeeLogisticSponsor(edition, this, userId));
            }
        }

        /// <summary>Gets the attendee collaborator by edition identifier.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public AttendeeLogisticSponsor GetAttendeeByEditionId(int editionId)
        {
            return this.AttendeeLogisticSponsors?.FirstOrDefault(ac => ac.EditionId == editionId);
        }

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