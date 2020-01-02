// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="ConferenceParticipant.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ConferenceParticipant</summary>
    public class ConferenceParticipant : Entity
    {
        public int ConferenceId { get; private set; }
        public int AttendeeCollaboratorId { get; private set; }
        public int ConferenceParticipantRoleId { get; private set; }
        public bool IsPreRegistered { get; private set; }  //TODO: Remove this

        public virtual Conference Conference { get; private set; }
        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual ConferenceParticipantRole ConferenceParticipantRole { get; private set; }

        public ConferenceParticipant(
            Guid conferenceParticipantUid,
            int userId)
        {
            //this.Uid = conferenceUid;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipant"/> class.</summary>
        protected ConferenceParticipant()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            //this.ValidateEdition();
            //this.ValidateDates();

            return this.ValidationResult.IsValid;
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