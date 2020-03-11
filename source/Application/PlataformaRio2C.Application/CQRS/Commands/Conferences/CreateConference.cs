// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="CreateConference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateConference</summary>
    public class CreateConference : BaseCommand
    {
        [Display(Name = "Event", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? EditionEventUid { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? Date { get; set; }

        [Display(Name = "StartTime", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string StartTime { get; set; }

        [Display(Name = "EndTime", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string EndTime { get; set; }

        [Display(Name = "Room", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? RoomUid { get; set; }

        public List<ConferenceTitleBaseCommand> Titles { get; set; }
        public List<ConferenceSynopsisBaseCommand> Synopsis { get; set; }

        public List<Guid> TrackUids { get; set; }
        public List<Guid> PillarUids { get; set; }
        public List<Guid> PresentationFormatUids { get; set; }

        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }
        public List<EditionEvent> EditionEvents { get; private set; }
        public List<RoomJsonDto> Rooms { get; private set; }
        public List<Track> Tracks { get; private set; }
        public List<Pillar> Pillars { get; private set; }
        public List<PresentationFormat> PresentationFormats { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CreateConference"/> class.</summary>
        /// <param name="editionEvents">The edition events.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="roomDtos">The room dtos.</param>
        /// <param name="tracks">The tracks.</param>
        /// <param name="pillars">The pillars.</param>
        /// <param name="presentationFormats">The presentation formats.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateConference(
            List<EditionEvent> editionEvents,
            List<LanguageDto> languagesDtos,
            List<RoomDto> roomDtos,
            List<Track> tracks,
            List<Pillar> pillars,
            List<PresentationFormat> presentationFormats,
            string userInterfaceLanguage)
        {
            this.UpdateTitles(languagesDtos);
            this.UpdateSynopsis(languagesDtos);

            this.UpdateDropdowns(editionEvents, roomDtos, tracks, pillars, presentationFormats, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateConference"/> class.</summary>
        public CreateConference()
        {
        }

        /// <summary>Updates the dropdowns.</summary>
        /// <param name="editionEvents">The edition events.</param>
        /// <param name="roomDtos">The room dtos.</param>
        /// <param name="tracks">The tracks.</param>
        /// <param name="pillars">The pillars.</param>
        /// <param name="presentationFormats">The presentation formats.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdowns(
            List<EditionEvent> editionEvents,
            List<RoomDto> roomDtos,
            List<Track> tracks,
            List<Pillar> pillars,
            List<PresentationFormat> presentationFormats,
            string userInterfaceLanguage)
        {
            this.EditionEvents = editionEvents;

            if (this.EditionEventUid.HasValue)
            {
                var editionEvent = editionEvents.FirstOrDefault(ev => ev.Uid == this.EditionEventUid.Value);
                this.StartDate = editionEvent?.StartDate;
                this.EndDate = editionEvent?.EndDate;
            }

            this.Rooms = roomDtos?.Select(r => new RoomJsonDto
            {
                Id = r.Room.Id,
                Uid = r.Room.Uid,
                Name = r.GetRoomNameByLanguageCode(userInterfaceLanguage)?.RoomName?.Value
            })?.ToList();

            this.Tracks = tracks;
            this.Pillars = pillars;
            this.PresentationFormats = presentationFormats;
        }

        #region Private Methods

        /// <summary>Updates the titles.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateTitles(List<LanguageDto> languagesDtos)
        {
            this.Titles = new List<ConferenceTitleBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                this.Titles.Add(new ConferenceTitleBaseCommand(languageDto));
            }
        }

        /// <summary>Updates the synopsis.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateSynopsis(List<LanguageDto> languagesDtos)
        {
            this.Synopsis = new List<ConferenceSynopsisBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                this.Synopsis.Add(new ConferenceSynopsisBaseCommand(languageDto));
            }
        }

        #endregion
    }
}