// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-13-2019
// ***********************************************************************
// <copyright file="SpeakerSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>SpeakerSearchViewModel</summary>
    public class SpeakerSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        public bool ShowAllEditions { get; set; }

        [Display(Name = "ShowAllParticipants", ResourceType = typeof(Labels))]
        public bool ShowAllParticipants { get; set; }

        [Display(Name = "ShowHighlights", ResourceType = typeof(Labels))]
        public bool ShowHighlights { get; set; }

        [Display(Name = "ShowNotPublishableToApi", ResourceType = typeof(Labels))]
        public bool ShowNotPublishableToApi { get; set; }
        
        [Display(Name = "Room", ResourceType = typeof(Labels))]
        public Guid? RoomsUids { get; set; }

        public List<RoomJsonDto> Rooms { get; private set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SpeakerSearchViewModel"/> class.</summary>
        public SpeakerSearchViewModel()
        {
            //
        }

        /// <summary>Initializes a new instance of the <see cref="SpeakerSearchViewModel"/> class.</summary>
        public SpeakerSearchViewModel(
            List<RoomDto> roomDtos,
            string userInterfaceLanguag
        )
        {
            this.UpdateDropdowns(roomDtos, userInterfaceLanguag);
        }

        /// <summary>Updates the dropdowns.</summary>
        /// <param name="roomDtos">The room dtos.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdowns(
            List<RoomDto> roomDtos,
            string userInterfaceLanguage
        )
        {
            this.Rooms = roomDtos?.Select(r => new RoomJsonDto
            {
                Id = r.Room.Id,
                Uid = r.Room.Uid,
                Name = r.GetRoomNameByLanguageCode(userInterfaceLanguage)?.RoomName?.Value
            })?.ToList();
        }
    }
}