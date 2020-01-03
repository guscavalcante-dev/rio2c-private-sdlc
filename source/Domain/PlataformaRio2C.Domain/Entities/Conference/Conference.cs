// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="Conference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Conference</summary>
    public class Conference : Entity
    {
        //public static readonly int LocalMinLength = 2;
        //public static readonly int LocalMaxLength = 1000;
        //public static readonly int InfoMaxLength = 3000;

        public int EditionId { get; private set; }
        public int? RoomId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        //public string Info { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Room Room { get; private set; }

        public virtual ICollection<ConferenceTitle> ConferenceTitles { get; private set; }
        public virtual ICollection<ConferenceSynopsis> ConferenceSynopses { get; private set; }
        public virtual ICollection<ConferenceParticipant> ConferenceParticipants { get; private set; }
        //public virtual ICollection<ConferenceLecturer> Lecturers { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Conference"/> class.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="conferenceTitles">The conference titles.</param>
        /// <param name="userId">The user identifier.</param>
        public Conference(
            Guid conferenceUid,
            Edition edition,
            DateTime date,
            string startTime,
            string endTime,
            List<ConferenceTitle> conferenceTitles,
            int userId)
        {
            //this.Uid = conferenceUid;
            this.EditionId = edition?.Id ?? 0;
            this.Edition = edition;
            this.StartDate = date.JoinDateAndTime(startTime, true);
            this.EndDate = date.JoinDateAndTime(endTime, true);
            this.SynchronizeConferenceTitles(conferenceTitles, userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Conference"/> class.</summary>
        protected Conference()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="conferenceTitles">The conference titles.</param>
        /// <param name="conferenceSynopses">The conference synopses.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            DateTime date,
            string startTime,
            string endTime,
            List<ConferenceTitle> conferenceTitles,
            List<ConferenceSynopsis> conferenceSynopses,
            int userId)
        {
            this.StartDate = date.JoinDateAndTime(startTime, true);
            this.EndDate = date.JoinDateAndTime(endTime, true);
            this.SynchronizeConferenceTitles(conferenceTitles, userId);
            this.SynchronizeConferenceSynopses(conferenceSynopses, userId);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.SynchronizeConferenceTitles(new List<ConferenceTitle>(), userId);
            this.SynchronizeConferenceSynopses(new List<ConferenceSynopsis>(), userId);

            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Conference Titles

        /// <summary>Synchronizes the conference titles.</summary>
        /// <param name="conferenceTitles">The conference titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeConferenceTitles(List<ConferenceTitle> conferenceTitles, int userId)
        {
            if (this.ConferenceTitles == null)
            {
                this.ConferenceTitles = new List<ConferenceTitle>();
            }

            this.DeleteConferenceTitles(conferenceTitles, userId);

            if (conferenceTitles?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var conferenceTitle in conferenceTitles)
            {
                var conferenceTitleDb = this.ConferenceTitles.FirstOrDefault(d => d.Language.Code == conferenceTitle.Language.Code);
                if (conferenceTitleDb != null)
                {
                    conferenceTitleDb.Update(conferenceTitle);
                }
                else
                {
                    this.CreateConferenceTitle(conferenceTitle);
                }
            }
        }

        /// <summary>Deletes the conference titles.</summary>
        /// <param name="newConferenceTitles">The new conference titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferenceTitles(List<ConferenceTitle> newConferenceTitles, int userId)
        {
            var conferenceTitlesToDelete = this.ConferenceTitles.Where(db => newConferenceTitles?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var conferenceTitleToDelete in conferenceTitlesToDelete)
            {
                conferenceTitleToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference title.</summary>
        /// <param name="conferenceTitle">The conference title.</param>
        private void CreateConferenceTitle(ConferenceTitle conferenceTitle)
        {
            this.ConferenceTitles.Add(conferenceTitle);
        }

        #endregion

        #region Conference Synopses

        /// <summary>Synchronizes the conference synopses.</summary>
        /// <param name="conferenceSynopses">The conference synopses.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeConferenceSynopses(List<ConferenceSynopsis> conferenceSynopses, int userId)
        {
            if (this.ConferenceSynopses == null)
            {
                this.ConferenceSynopses = new List<ConferenceSynopsis>();
            }

            this.DeleteConferenceSynopses(conferenceSynopses, userId);

            if (conferenceSynopses?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var conferenceSynopsis in conferenceSynopses)
            {
                var conferenceSynopsisDb = this.ConferenceSynopses.FirstOrDefault(d => d.Language.Code == conferenceSynopsis.Language.Code);
                if (conferenceSynopsisDb != null)
                {
                    conferenceSynopsisDb.Update(conferenceSynopsis);
                }
                else
                {
                    this.CreateConferenceSynopsis(conferenceSynopsis);
                }
            }
        }

        /// <summary>Deletes the conference synopses.</summary>
        /// <param name="newConferenceSynopses">The new conference synopses.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferenceSynopses(List<ConferenceSynopsis> newConferenceSynopses, int userId)
        {
            var conferenceSynopsesToDelete = this.ConferenceTitles.Where(db => newConferenceSynopses?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var conferenceSynopsisToDelete in conferenceSynopsesToDelete)
            {
                conferenceSynopsisToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference synopsis.</summary>
        /// <param name="conferenceSynopsis">The conference synopsis.</param>
        private void CreateConferenceSynopsis(ConferenceSynopsis conferenceSynopsis)
        {
            this.ConferenceSynopses.Add(conferenceSynopsis);
        }

        #endregion

        #region Conference Participants

        /// <summary>Creates the conference participant.</summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="conferenceParticipantRole">The conference participant role.</param>
        /// <param name="userId">The user identifier.</param>
        public void CreateConferenceParticipant(
            AttendeeCollaborator attendeeCollaborator,
            ConferenceParticipantRole conferenceParticipantRole,
            int userId)
        {
            if (this.ConferenceParticipants == null)
            {
                this.ConferenceParticipants = new List<ConferenceParticipant>();
            }

            var conferenceParticipant = this.GetConferenceParticipantByAttendeeCollaboratorId(attendeeCollaborator?.Id ?? 0);
            if (conferenceParticipant == null)
            {
                this.ConferenceParticipants.Add(new ConferenceParticipant(Guid.NewGuid(), attendeeCollaborator, conferenceParticipantRole, userId));
            }
            else
            {
                conferenceParticipant.Update(conferenceParticipantRole, userId);
            }
        }

        /// <summary>Gets the conference participant by attendee collaborator identifier.</summary>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        public ConferenceParticipant GetConferenceParticipantByAttendeeCollaboratorId(int attendeeCollaboratorId)
        {
            return this.ConferenceParticipants?.FirstOrDefault(cp => cp.AttendeeCollaboratorId == attendeeCollaboratorId);
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateEdition();
            this.ValidateDates();
            this.ValidateConferenceTitles();
            this.ValidateConferenceSynopses();
            this.ValidateConferenceParticipants();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the edition.</summary>
        public void ValidateEdition()
        {
            if (this.Edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Edition), new string[] { "Edition" }));
            }
        }

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            if (this.StartDate < this.Edition.StartDate || this.StartDate > this.Edition.EndDate
                || this.EndDate < this.Edition.StartDate || this.EndDate > this.Edition.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.Edition.EndDate.ToShortDateString(), this.Edition.StartDate.ToShortDateString()), new string[] { "Date" }));
            }

            if (this.StartDate > this.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndTime, Labels.StartTime), new string[] { "EndTime" }));
            }
        }

        /// <summary>Validates the conference titles.</summary>
        public void ValidateConferenceTitles()
        {
            if (this.ConferenceTitles?.Any() != true)
            {
                return;
            }

            foreach (var conferenceTitle in this.ConferenceTitles?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(conferenceTitle.ValidationResult);
            }
        }

        /// <summary>Validates the conference synopses.</summary>
        public void ValidateConferenceSynopses()
        {
            if (this.ConferenceSynopses?.Any() != true)
            {
                return;
            }

            foreach (var conferenceSynopsis in this.ConferenceSynopses?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(conferenceSynopsis.ValidationResult);
            }
        }

        /// <summary>Validates the conference participants.</summary>
        public void ValidateConferenceParticipants()
        {
            if (this.ConferenceParticipants?.Any() != true)
            {
                return;
            }

            foreach (var conferenceParticipant in this.ConferenceParticipants?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(conferenceParticipant.ValidationResult);
            }
        }

        #endregion
    }
}