// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ConferenceParticipant.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
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

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipant"/> class.</summary>
        /// <param name="conferenceParticipantUid">The conference participant uid.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="conferenceParticipantRole">The conference participant role.</param>
        /// <param name="userId">The user identifier.</param>
        public ConferenceParticipant(
            Guid conferenceParticipantUid,
            AttendeeCollaborator attendeeCollaborator,
            ConferenceParticipantRole conferenceParticipantRole,
            int userId)
        {
            //this.Uid = conferenceParticipantUid;
            this.AttendeeCollaboratorId = attendeeCollaborator?.Id ?? 0;
            this.AttendeeCollaborator = attendeeCollaborator;
            this.ConferenceParticipantRoleId = conferenceParticipantRole?.Id ?? 0;
            this.ConferenceParticipantRole = conferenceParticipantRole;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipant"/> class.</summary>
        protected ConferenceParticipant()
        {
        }

        /// <summary>Updates the specified conference participant role.</summary>
        /// <param name="conferenceParticipantRole">The conference participant role.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            ConferenceParticipantRole conferenceParticipantRole,
            int userId)
        {
            this.ConferenceParticipantRoleId = conferenceParticipantRole?.Id ?? 0;
            this.ConferenceParticipantRole = conferenceParticipantRole;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateAttendeeCollaborator();
            this.ValidateConferenceParticipantRole();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the attendee collaborator.</summary>
        public void ValidateAttendeeCollaborator()
        {
            if (this.AttendeeCollaborator == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Speaker), new string[] { "Speaker" }));
            }
        }

        /// <summary>Validates the conference participant role.</summary>
        public void ValidateConferenceParticipantRole()
        {
            if (this.ConferenceParticipantRole == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Function), new string[] { "Function" }));
            }
        }

        #endregion
    }
}