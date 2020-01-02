// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRole.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ConferenceParticipantRole</summary>
    public class ConferenceParticipantRole : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 80;

        public string Name { get; private set; } //TODO: Remove this
        public bool IsLecturer { get; private set; } //TODO: Remove this

        public virtual ICollection<ConferenceParticipantRoleTitle> ConferenceParticipantRoleTitles { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRole"/> class.</summary>
        /// <param name="conferenceParticipantRoleUid">The conference participant role uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="conferenceParticipantRoleTitles">The conference participant role titles.</param>
        /// <param name="userId">The user identifier.</param>
        public ConferenceParticipantRole(
            Guid conferenceParticipantRoleUid,
            string name,
            List<ConferenceParticipantRoleTitle> conferenceParticipantRoleTitles,
            int userId)
        {
            //this.Uid = conferenceParticipantRoleUid;
            this.Name = name?.Trim();
            this.SynchronizeConferenceParticipantRoleTitles(conferenceParticipantRoleTitles, userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRole"/> class.</summary>
        protected ConferenceParticipantRole()
        {
        }

        #region Conference Participant Role Titles

        /// <summary>Synchronizes the conference participant role titles.</summary>
        /// <param name="conferenceParticipantRoleTitles">The conference participant role titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeConferenceParticipantRoleTitles(List<ConferenceParticipantRoleTitle> conferenceParticipantRoleTitles, int userId)
        {
            if (this.ConferenceParticipantRoleTitles == null)
            {
                this.ConferenceParticipantRoleTitles = new List<ConferenceParticipantRoleTitle>();
            }

            this.DeleteConferenceParticipantRoleTitles(conferenceParticipantRoleTitles, userId);

            if (conferenceParticipantRoleTitles?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var conferenceParticipantRoleTitle in conferenceParticipantRoleTitles)
            {
                var conferenceTitleDb = this.ConferenceParticipantRoleTitles.FirstOrDefault(d => d.Language.Code == conferenceParticipantRoleTitle.Language.Code);
                if (conferenceTitleDb != null)
                {
                    conferenceTitleDb.Update(conferenceParticipantRoleTitle);
                }
                else
                {
                    this.CreateConferenceParticipantRoleTitle(conferenceParticipantRoleTitle);
                }
            }
        }

        /// <summary>Deletes the conference participant role titles.</summary>
        /// <param name="newConferenceParticipantRoleTitles">The new conference participant role titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferenceParticipantRoleTitles(List<ConferenceParticipantRoleTitle> newConferenceParticipantRoleTitles, int userId)
        {
            var conferenceParticipantRoleTitlesToDelete = this.ConferenceParticipantRoleTitles.Where(db => newConferenceParticipantRoleTitles?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var conferenceParticipantRoleTitleToDelete in conferenceParticipantRoleTitlesToDelete)
            {
                conferenceParticipantRoleTitleToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference participant role title.</summary>
        /// <param name="conferenceParticipantRoleTitle">The conference participant role title.</param>
        private void CreateConferenceParticipantRoleTitle(ConferenceParticipantRoleTitle conferenceParticipantRoleTitle)
        {
            this.ConferenceParticipantRoleTitles.Add(conferenceParticipantRoleTitle);
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            //this.ValidateEdition();
            //this.ValidateDates();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
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


        ///// <summary>Validates the edition.</summary>
        //public void ValidateEdition()
        //{
        //    if (this.Edition == null)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Edition), new string[] { "Edition" }));
        //    }
        //}

        ///// <summary>Validates the dates.</summary>
        //public void ValidateDates()
        //{
        //    if (this.StartDate < this.Edition.StartDate || this.StartDate > this.Edition.EndDate
        //        || this.EndDate < this.Edition.StartDate || this.EndDate > this.Edition.EndDate)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.Edition.EndDate.ToShortDateString(), this.Edition.StartDate.ToShortDateString()), new string[] { "Date" }));
        //    }

        //    if (this.StartDate > this.EndDate)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndTime, Labels.StartTime), new string[] { "EndTime" }));
        //    }
        //}

        #endregion
    }
}