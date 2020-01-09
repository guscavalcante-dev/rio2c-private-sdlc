// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="UpdateConferenceMainInformation.cs" company="Softo">
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
    /// <summary>UpdateConferenceMainInformation</summary>
    public class UpdateConferenceMainInformation : BaseCommand
    {
        public Guid ConferenceUid { get; set; }

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

        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public List<EditionEvent> EditionEvents { get; private set; }
        public List<RoomJsonDto> Rooms { get; private set; }

        public UpdateConferenceMainInformation(
            ConferenceDto conferenceDto,
            List<EditionEvent> editionEvents,
            List<LanguageDto> languagesDtos,
            List<RoomDto> roomDtos,
            string userInterfaceLanguage)
        {
            this.EditionEventUid = conferenceDto?.EditionEvent?.Uid;
            this.Date = conferenceDto?.Conference?.StartDate.Date;
            this.StartTime = conferenceDto?.Conference?.StartDate.ToShortTimeString();
            this.EndTime = conferenceDto?.Conference?.EndDate.ToShortTimeString();
            this.RoomUid = conferenceDto?.RoomDto?.Room?.Uid;
            this.UpdateTitles(conferenceDto, languagesDtos);
            this.UpdateSynopsis(conferenceDto, languagesDtos);

            this.UpdateDropdowns(editionEvents, roomDtos, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceMainInformation"/> class.</summary>
        public UpdateConferenceMainInformation()
        {
        }

        public void UpdateDropdowns(
            List<EditionEvent> editionEvents,
            List<RoomDto> roomDtos,
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
        }

        #region Private Methods

        /// <summary>Updates the titles.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateTitles(ConferenceDto conferenceDto, List<LanguageDto> languagesDtos)
        {
            this.Titles = new List<ConferenceTitleBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var conferenceTitle = conferenceDto?.ConferenceTitleDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Titles.Add(conferenceTitle != null ? new ConferenceTitleBaseCommand(conferenceTitle) :
                                                          new ConferenceTitleBaseCommand(languageDto));
            }
        }

        /// <summary>Updates the synopsis.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateSynopsis(ConferenceDto conferenceDto, List<LanguageDto> languagesDtos)
        {
            this.Synopsis = new List<ConferenceSynopsisBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var conferenceSynopsis = conferenceDto?.ConferenceSynopsisDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Synopsis.Add(conferenceSynopsis != null ? new ConferenceSynopsisBaseCommand(conferenceSynopsis) :
                                                               new ConferenceSynopsisBaseCommand(languageDto));
            }
        }

        #endregion
    }
}