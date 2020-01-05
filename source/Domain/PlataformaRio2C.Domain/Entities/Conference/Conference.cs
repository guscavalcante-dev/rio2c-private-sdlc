// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
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
        public int EditionEventId { get; private set; }
        public int? RoomId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public virtual EditionEvent EditionEvent { get; private set; }
        public virtual Room Room { get; private set; }

        public virtual ICollection<ConferenceTitle> ConferenceTitles { get; private set; }
        public virtual ICollection<ConferenceSynopsis> ConferenceSynopses { get; private set; }
        public virtual ICollection<ConferenceParticipant> ConferenceParticipants { get; private set; }
        public virtual ICollection<ConferenceVerticalTrack> ConferenceVerticalTracks { get; private set; }
        public virtual ICollection<ConferenceHorizontalTrack> ConferenceHorizontalTracks { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Conference"/> class.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="editionEvent">The edition event.</param>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="conferenceTitles">The conference titles.</param>
        /// <param name="userId">The user identifier.</param>
        public Conference(
            Guid conferenceUid,
            EditionEvent editionEvent,
            DateTime date,
            string startTime,
            string endTime,
            List<ConferenceTitle> conferenceTitles,
            int userId)
        {
            //this.Uid = conferenceUid;
            this.EditionEventId = editionEvent?.Id ?? 0;
            this.EditionEvent = editionEvent;
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
        /// <param name="editionEvent">The edition event.</param>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="room">The room.</param>
        /// <param name="conferenceTitles">The conference titles.</param>
        /// <param name="conferenceSynopses">The conference synopses.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            EditionEvent editionEvent,
            DateTime date,
            string startTime,
            string endTime,
            Room room,
            List<ConferenceTitle> conferenceTitles,
            List<ConferenceSynopsis> conferenceSynopses,
            int userId)
        {
            this.EditionEventId = editionEvent?.Id ?? 0;
            this.EditionEvent = editionEvent;
            this.StartDate = date.JoinDateAndTime(startTime, true);
            this.EndDate = date.JoinDateAndTime(endTime, true);
            this.RoomId = room?.Id;
            this.Room = room;
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

        #region Tracks

        /// <summary>Updates the tracks.</summary>
        /// <param name="verticalTracks">The vertical tracks.</param>
        /// <param name="horizontalTracks">The horizontal tracks.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateTracks(List<VerticalTrack> verticalTracks, List<HorizontalTrack> horizontalTracks, int userId)
        {
            this.SynchronizeConferenceVerticalTracks(verticalTracks, userId);
            this.SynchronizeConferenceHorizontalTracks(horizontalTracks, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        #region Vertical Tracks

        /// <summary>Synchronizes the conference vertical tracks.</summary>
        /// <param name="verticalTracks">The vertical tracks.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeConferenceVerticalTracks(List<VerticalTrack> verticalTracks, int userId)
        {
            if (this.ConferenceVerticalTracks == null)
            {
                this.ConferenceVerticalTracks = new List<ConferenceVerticalTrack>();
            }

            this.DeleteConferenceVerticalTracks(verticalTracks, userId);

            if (verticalTracks?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var verticalTrack in verticalTracks)
            {
                var verticalTrackDb = this.ConferenceVerticalTracks.FirstOrDefault(a => a.VerticalTrack.Uid == verticalTrack.Uid);
                if (verticalTrackDb != null)
                {
                    verticalTrackDb.Update(userId);
                }
                else
                {
                    this.CreateConferenceVerticalTrack(verticalTrack, userId);
                }
            }
        }

        /// <summary>Deletes the conference vertical tracks.</summary>
        /// <param name="newVerticalTracks">The new vertical tracks.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferenceVerticalTracks(List<VerticalTrack> newVerticalTracks, int userId)
        {
            var conferenceVerticalTracksToDelete = this.ConferenceVerticalTracks.Where(db => newVerticalTracks?.Select(a => a.Uid)?.Contains(db.VerticalTrack.Uid) == false && !db.IsDeleted).ToList();
            foreach (var conferenceVerticalTrackToDelete in conferenceVerticalTracksToDelete)
            {
                conferenceVerticalTrackToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference vertical track.</summary>
        /// <param name="verticalTrack">The vertical track.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateConferenceVerticalTrack(VerticalTrack verticalTrack, int userId)
        {
            this.ConferenceVerticalTracks.Add(new ConferenceVerticalTrack(Guid.NewGuid(), this, verticalTrack, userId));
        }

        #endregion

        #region Horizontal Tracks

        /// <summary>Synchronizes the conference horizontal tracks.</summary>
        /// <param name="horizontalTracks">The horizontal tracks.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeConferenceHorizontalTracks(List<HorizontalTrack> horizontalTracks, int userId)
        {
            if (this.ConferenceHorizontalTracks == null)
            {
                this.ConferenceHorizontalTracks = new List<ConferenceHorizontalTrack>();
            }

            this.DeleteConferenceHorizontalTracks(horizontalTracks, userId);

            if (horizontalTracks?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var horizontalTrack in horizontalTracks)
            {
                var horizontalTrackDb = this.ConferenceHorizontalTracks.FirstOrDefault(a => a.HorizontalTrack.Uid == horizontalTrack.Uid);
                if (horizontalTrackDb != null)
                {
                    horizontalTrackDb.Update(userId);
                }
                else
                {
                    this.CreateConferenceHorizontalTrack(horizontalTrack, userId);
                }
            }
        }

        /// <summary>Deletes the conference horizontal tracks.</summary>
        /// <param name="newHorizontalTracks">The new horizontal tracks.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferenceHorizontalTracks(List<HorizontalTrack> newHorizontalTracks, int userId)
        {
            var conferenceHorizontalTracksToDelete = this.ConferenceHorizontalTracks.Where(db => newHorizontalTracks?.Select(a => a.Uid)?.Contains(db.HorizontalTrack.Uid) == false && !db.IsDeleted).ToList();
            foreach (var conferenceHorizontalTrackToDelete in conferenceHorizontalTracksToDelete)
            {
                conferenceHorizontalTrackToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference horizontal track.</summary>
        /// <param name="horizontalTrack">The horizontal track.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateConferenceHorizontalTrack(HorizontalTrack horizontalTrack, int userId)
        {
            this.ConferenceHorizontalTracks.Add(new ConferenceHorizontalTrack(Guid.NewGuid(), this, horizontalTrack, userId));
        }

        #endregion

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

        /// <summary>Updates the conference participant.</summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="conferenceParticipantRole">The conference participant role.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateConferenceParticipant(
            AttendeeCollaborator attendeeCollaborator,
            ConferenceParticipantRole conferenceParticipantRole,
            int userId)
        {
            var conferenceParticipant = this.GetConferenceParticipantByAttendeeCollaboratorId(attendeeCollaborator?.Id ?? 0);
            conferenceParticipant?.Update(conferenceParticipantRole, userId);
        }

        /// <summary>Deletes the conference participant.</summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteConferenceParticipant(
            AttendeeCollaborator attendeeCollaborator,
            int userId)
        {
            var conferenceParticipant = this.GetConferenceParticipantByAttendeeCollaboratorId(attendeeCollaborator?.Id ?? 0);
            conferenceParticipant?.Delete(userId);
        }

        /// <summary>Gets the conference participant by attendee collaborator identifier.</summary>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        private ConferenceParticipant GetConferenceParticipantByAttendeeCollaboratorId(int attendeeCollaboratorId)
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

            this.ValidateEditionEvent();
            this.ValidateDates();
            this.ValidateConferenceTitles();
            this.ValidateConferenceSynopses();
            this.ValidateConferenceParticipants();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the edition.</summary>
        public void ValidateEditionEvent()
        {
            if (this.EditionEvent == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Event), new string[] { "Event" }));
            }
        }

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            if (this.StartDate < this.EditionEvent.StartDate || this.StartDate > this.EditionEvent.EndDate
                || this.EndDate < this.EditionEvent.StartDate || this.EndDate > this.EditionEvent.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.EditionEvent.EndDate.ToShortDateString(), this.EditionEvent.StartDate.ToShortDateString()), new string[] { "Date" }));
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