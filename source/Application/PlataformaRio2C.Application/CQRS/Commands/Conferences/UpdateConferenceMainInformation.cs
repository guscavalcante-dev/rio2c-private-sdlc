// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
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
    public class UpdateConferenceMainInformation : CreateConference
    {
        public Guid ConferenceUid { get; set; }

        [Display(Name = "Room", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? RoomUid { get; set; }

        public List<ConferenceSynopsisBaseCommand> Synopsis { get; set; }

        public List<RoomJsonDto> Rooms { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceMainInformation"/> class.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="editionEvents">The edition events.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="roomDtos">The room dtos.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public UpdateConferenceMainInformation(
            ConferenceDto conferenceDto,
            List<EditionEvent> editionEvents,
            List<LanguageDto> languagesDtos,
            List<RoomDto> roomDtos,
            string userInterfaceLanguage)
            : base(conferenceDto, editionEvents, languagesDtos)
        {
            this.ConferenceUid = conferenceDto?.Conference?.Uid ?? Guid.Empty;
            this.RoomUid = conferenceDto?.RoomDto?.Room?.Uid;
            this.UpdateSynopsis(conferenceDto, languagesDtos);
            this.UpdateDropdowns(editionEvents, roomDtos, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceMainInformation"/> class.</summary>
        public UpdateConferenceMainInformation()
        {
        }

        /// <summary>Updates the dropdowns.</summary>
        /// <param name="editionEvents">The edition events.</param>
        /// <param name="roomDtos">The room dtos.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdowns(
            List<EditionEvent> editionEvents,
            List<RoomDto> roomDtos,
            string userInterfaceLanguage)
        {
            this.UpdateDropdowns(editionEvents);
            this.Rooms = roomDtos?.Select(r => new RoomJsonDto
            {
                Id = r.Room.Id,
                Uid = r.Room.Uid,
                Name = r.GetRoomNameByLanguageCode(userInterfaceLanguage)?.RoomName?.Value
            })?.ToList();
        }

        #region Private Methods

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