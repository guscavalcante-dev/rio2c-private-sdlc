// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="CreateNegotiationRoomConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateNegotiationRoomConfig</summary>
    public class CreateNegotiationRoomConfig : BaseCommand
    {
        public Guid? NegotiationConfigUid { get; set; }

        [Display(Name = "Room", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? RoomUid { get; set; }

        [Display(Name = "CountAutomaticTables", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? CountAutomaticTables { get; set; }

        [Display(Name = "CountManualTables", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public int? CountManualTables { get; set; }

        public List<RoomJsonDto> Rooms { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CreateNegotiationRoomConfig"/> class.</summary>
        /// <param name="negotiationConfigDto">The negotiation configuration dto.</param>
        /// <param name="roomDtos">The room dtos.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateNegotiationRoomConfig(
            NegotiationConfigDto negotiationConfigDto,
            List<RoomDto> roomDtos,
            string userInterfaceLanguage)
        {
            this.NegotiationConfigUid = negotiationConfigDto?.NegotiationConfig?.Uid;
            this.UpdateModelsAndLists(roomDtos, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateNegotiationRoomConfig"/> class.</summary>
        public CreateNegotiationRoomConfig()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="roomDtos">The room dtos.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateModelsAndLists(List<RoomDto> roomDtos, string userInterfaceLanguage)
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