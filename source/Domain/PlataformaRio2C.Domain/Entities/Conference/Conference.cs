// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-27-2024
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
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Conference</summary>
    public class Conference : Entity
    {
        public int EditionEventId { get; private set; }
        public int RoomId { get; private set; }
        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }

        public virtual EditionEvent EditionEvent { get; private set; }
        public virtual Room Room { get; private set; }
        public bool IsApiDisplayEnabled { get; set; }
        public string ApiHighlightPosition { get; set; }

        public virtual ICollection<ConferenceTitle> ConferenceTitles { get; private set; }
        public virtual ICollection<ConferenceSynopsis> ConferenceSynopses { get; private set; }
        public virtual ICollection<ConferenceParticipant> ConferenceParticipants { get; private set; }
        public virtual ICollection<ConferenceTrack> ConferenceTracks { get; private set; }
        public virtual ICollection<ConferencePresentationFormat> ConferencePresentationFormats { get; private set; }
        public virtual ICollection<ConferencePillar> ConferencePillars { get; private set; }
        public virtual ICollection<ConferenceDynamic> ConferenceDynamics { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Conference"/> class.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="editionEvent">The edition event.</param>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="room">The room.</param>
        /// <param name="conferenceTitles">The conference titles.</param>
        /// <param name="conferenceSynopses">The conference synopses.</param>
        /// <param name="tracks">The tracks.</param>
        /// <param name="pillars">The pillars.</param>
        /// <param name="presentationFormats">The presentation formats.</param>
        /// <param name="userId">The user identifier.</param>
        public Conference(
            Guid conferenceUid,
            EditionEvent editionEvent,
            DateTime? date,
            string startTime,
            string endTime,
            Room room,
            List<ConferenceTitle> conferenceTitles,
            List<ConferenceSynopsis> conferenceSynopses,
            List<Track> tracks,
            List<Pillar> pillars,
            List<PresentationFormat> presentationFormats,
            List<ConferenceDynamic> conferenceDynamics,
            int userId)
        {
            //this.Uid = conferenceUid;
            this.EditionEventId = editionEvent?.Id ?? 0;
            this.EditionEvent = editionEvent;
            this.StartDate = date?.JoinDateAndTime(startTime, true).ToUtcTimeZone();
            this.EndDate = date?.JoinDateAndTime(endTime, true).ToUtcTimeZone();
            this.RoomId = room?.Id ?? 0;
            this.Room = room;
            this.SynchronizeConferenceTitles(conferenceTitles, userId);
            this.SynchronizeConferenceSynopses(conferenceSynopses, userId);
            this.SynchronizeConferenceTracks(tracks, userId);
            this.SynchronizeConferencePillars(pillars, userId);
            this.SynchronizeConferencePresentationFormats(presentationFormats, userId);
            this.SynchronizeConferenceDynamics(conferenceDynamics, userId);

            this.SetCreateDate(userId);
        }

        /// <summary>Initializes a new instance of the <see cref="Conference"/> class.</summary>
        protected Conference()
        {
        }

        /// <summary>
        /// Updates the main information.
        /// </summary>
        /// <param name="editionEvent">The edition event.</param>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="room">The room.</param>
        /// <param name="conferenceTitles">The conference titles.</param>
        /// <param name="conferenceSynopses">The conference synopses.</param>
        /// <param name="conferenceDynamics">The conference dynamics.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            EditionEvent editionEvent,
            DateTime date,
            string startTime,
            string endTime,
            Room room,
            List<ConferenceTitle> conferenceTitles,
            List<ConferenceSynopsis> conferenceSynopses,
            List<ConferenceDynamic> conferenceDynamics,
            int userId)
        {
            this.EditionEventId = editionEvent?.Id ?? 0;
            this.EditionEvent = editionEvent;
            this.StartDate = date.JoinDateAndTime(startTime, true).ToUtcTimeZone();
            this.EndDate = date.JoinDateAndTime(endTime, true).ToUtcTimeZone();
            this.RoomId = room?.Id ?? 0;
            this.Room = room;
            this.SynchronizeConferenceTitles(conferenceTitles, userId);
            this.SynchronizeConferenceSynopses(conferenceSynopses, userId);
            this.SynchronizeConferenceDynamics(conferenceDynamics, userId);

            this.SetUpdateDate(userId);
        }

        /// <summary>Updates the tracks and presentation formats.</summary>
        /// <param name="tracks">The tracks.</param>
        /// <param name="pillars">The pillars.</param>
        /// <param name="presentationFormats">The presentation formats.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateTracksAndPresentationFormats(List<Track> tracks, List<Pillar> pillars, List<PresentationFormat> presentationFormats, int userId)
        {
            this.SynchronizeConferenceTracks(tracks, userId);
            this.SynchronizeConferencePillars(pillars, userId);
            this.SynchronizeConferencePresentationFormats(presentationFormats, userId);

            this.SetUpdateDate(userId);
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.SynchronizeConferenceTitles(new List<ConferenceTitle>(), userId);
            this.SynchronizeConferenceSynopses(new List<ConferenceSynopsis>(), userId);

            base.Delete(userId);
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

        #region Conference Dynamics

        /// <summary>Synchronizes the conference Dynamics.</summary>
        /// <param name="conferenceDynamics">The conference Dynamics.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeConferenceDynamics(List<ConferenceDynamic> conferenceDynamics, int userId)
        {
            if (this.ConferenceDynamics == null)
            {
                this.ConferenceDynamics = new List<ConferenceDynamic>();
            }

            this.DeleteConferenceDynamics(conferenceDynamics, userId);

            if (conferenceDynamics?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var conferenceDynamic in conferenceDynamics)
            {
                var conferenceDynamicDb = this.ConferenceDynamics.FirstOrDefault(d => d.Language.Code == conferenceDynamic.Language.Code);
                if (conferenceDynamicDb != null)
                {
                    conferenceDynamicDb.Update(conferenceDynamic);
                }
                else
                {
                    this.CreateConferenceDynamic(conferenceDynamic);
                }
            }
        }

        /// <summary>Deletes the conference Dynamics.</summary>
        /// <param name="newConferenceDynamics">The new conference Dynamics.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferenceDynamics(List<ConferenceDynamic> newConferenceDynamics, int userId)
        {
            var conferenceDynamicsToDelete = this.ConferenceDynamics.Where(db => newConferenceDynamics?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var conferenceDynamicToDelete in conferenceDynamicsToDelete)
            {
                conferenceDynamicToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference Dynamic.</summary>
        /// <param name="conferenceDynamic">The conference Dynamic.</param>
        private void CreateConferenceDynamic(ConferenceDynamic conferenceDynamic)
        {
            this.ConferenceDynamics.Add(conferenceDynamic);
        }

        #endregion

        #region Tracks

        private void SynchronizeConferenceTracks(List<Track> tracks, int userId)
        {
            if (this.ConferenceTracks == null)
            {
                this.ConferenceTracks = new List<ConferenceTrack>();
            }

            this.DeleteConferenceTracks(tracks, userId);

            if (tracks?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var track in tracks)
            {
                var trackDb = this.ConferenceTracks.FirstOrDefault(a => a.Track.Uid == track.Uid);
                if (trackDb != null)
                {
                    trackDb.Update(userId);
                }
                else
                {
                    this.CreateConferenceTrack(track, userId);
                }
            }
        }

        /// <summary>Deletes the conference tracks.</summary>
        /// <param name="newTracks">The new tracks.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferenceTracks(List<Track> newTracks, int userId)
        {
            var conferenceTracksToDelete = this.ConferenceTracks.Where(db => newTracks?.Select(a => a.Uid)?.Contains(db.Track.Uid) == false && !db.IsDeleted).ToList();
            foreach (var conferenceTrackToDelete in conferenceTracksToDelete)
            {
                conferenceTrackToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference track.</summary>
        /// <param name="track">The track.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateConferenceTrack(Track track, int userId)
        {
            this.ConferenceTracks.Add(new ConferenceTrack(Guid.NewGuid(), this, track, userId));
        }

        #endregion

        #region Pillars

        /// <summary>
        /// Synchronizes the conference pillars.
        /// </summary>
        /// <param name="pillars">The pillars.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeConferencePillars(List<Pillar> pillars, int userId)
        {
            if (this.ConferencePillars == null)
            {
                this.ConferencePillars = new List<ConferencePillar>();
            }

            this.DeleteConferencePillars(pillars, userId);

            if (pillars?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var pillar in pillars)
            {
                var pillarDb = this.ConferencePillars.FirstOrDefault(a => a.Pillar.Uid == pillar.Uid);
                if (pillarDb != null)
                {
                    pillarDb.Update(userId);
                }
                else
                {
                    this.CreateConferencePillar(pillar, userId);
                }
            }
        }

        /// <summary>Deletes the conference pillars.</summary>
        /// <param name="newPillars">The new pillars.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferencePillars(List<Pillar> newPillars, int userId)
        {
            var conferencePillarsToDelete = this.ConferencePillars.Where(db => newPillars?.Select(a => a.Uid)?.Contains(db.Pillar.Uid) == false && !db.IsDeleted).ToList();
            foreach (var conferencePillarToDelete in conferencePillarsToDelete)
            {
                conferencePillarToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference pillar.</summary>
        /// <param name="pillar">The pillar.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateConferencePillar(Pillar pillar, int userId)
        {
            this.ConferencePillars.Add(new ConferencePillar(this, pillar, userId));
        }

        #endregion

        #region Presentation Formats

        /// <summary>Synchronizes the conference presentation formats.</summary>
        /// <param name="presentationFormats">The presentation formats.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeConferencePresentationFormats(List<PresentationFormat> presentationFormats, int userId)
        {
            if (this.ConferencePresentationFormats == null)
            {
                this.ConferencePresentationFormats = new List<ConferencePresentationFormat>();
            }

            this.DeleteConferencePresentationFormats(presentationFormats, userId);

            if (presentationFormats?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var presentationFormat in presentationFormats)
            {
                var presentationFormatDb = this.ConferencePresentationFormats.FirstOrDefault(a => a.PresentationFormat.Uid == presentationFormat.Uid);
                if (presentationFormatDb != null)
                {
                    presentationFormatDb.Update(userId);
                }
                else
                {
                    this.CreateConferencePresentationFormat(presentationFormat, userId);
                }
            }
        }

        /// <summary>Deletes the conference presentation formats.</summary>
        /// <param name="newPresentationFormats">The new presentation formats.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferencePresentationFormats(List<PresentationFormat> newPresentationFormats, int userId)
        {
            var conferencePresentationFormatsToDelete = this.ConferencePresentationFormats.Where(db => newPresentationFormats?.Select(a => a.Uid)?.Contains(db.PresentationFormat.Uid) == false && !db.IsDeleted).ToList();
            foreach (var conferencePresentationFormatToDelete in conferencePresentationFormatsToDelete)
            {
                conferencePresentationFormatToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the conference presentation format.</summary>
        /// <param name="presentationFormat">The presentation format.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateConferencePresentationFormat(PresentationFormat presentationFormat, int userId)
        {
            this.ConferencePresentationFormats.Add(new ConferencePresentationFormat(Guid.NewGuid(), this, presentationFormat, userId));
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

            this.SetUpdateDate(userId);
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

            this.SetUpdateDate(userId);
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

            this.SetUpdateDate(userId);
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
            this.ValidateRoom();
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
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Event), new string[] { "EditionEventUid" }));
            }
        }

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            if (this.StartDate < this.EditionEvent.StartDate || this.StartDate > this.EditionEvent.EndDate
                || this.EndDate < this.EditionEvent.StartDate || this.EndDate > this.EditionEvent.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.EditionEvent.EndDate.ToBrazilTimeZone().ToShortDateString(), this.EditionEvent.StartDate.ToBrazilTimeZone().ToShortDateString()), new string[] { "Date" }));
            }

            if (this.StartDate > this.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndTime, Labels.StartTime), new string[] { "EndTime" }));
            }
        }

        /// <summary>Validates the room.</summary>
        public void ValidateRoom()
        {
            if (this.Room == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Room), new string[] { "RoomUid" }));
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

        /// <summary>Updates the API configuration.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">if set to <c>true</c> [is API display enabled].</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateApiConfiguration(
            bool isApiDisplayEnabled,
            string apiHighlightPosition,
            int userId)
        {
            this.IsApiDisplayEnabled = isApiDisplayEnabled;
            this.ApiHighlightPosition = apiHighlightPosition;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the API highlight position.</summary>
        /// <param name="userId">The user identifier.</param>
        public void DeleteApiHighlightPosition(int userId)
        {
            this.ApiHighlightPosition = null;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion
    }
}