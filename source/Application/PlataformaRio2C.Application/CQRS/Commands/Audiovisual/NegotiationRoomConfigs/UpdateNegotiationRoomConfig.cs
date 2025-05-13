// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="UpdateNegotiationRoomConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateNegotiationRoomConfig</summary>
    public class UpdateNegotiationRoomConfig : BaseCommand
    {
        public Guid? NegotiationConfigUid { get; set; }
        public Guid? NegotiationRoomConfigUid { get; set; }

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

        /// <summary>Initializes a new instance of the <see cref="UpdateNegotiationRoomConfig"/> class.</summary>
        /// <param name="negotiationRoomConfigDto">The negotiation room configuration dto.</param>
        /// <param name="roomDtos">The room dtos.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public UpdateNegotiationRoomConfig(
            NegotiationRoomConfigDto negotiationRoomConfigDto,
            List<RoomDto> roomDtos,
            string userInterfaceLanguage)
        {
            this.RoomUid = negotiationRoomConfigDto?.RoomDto?.Room?.Uid;
            this.NegotiationConfigUid = negotiationRoomConfigDto?.NegotiationConfig?.Uid;
            this.NegotiationRoomConfigUid = negotiationRoomConfigDto?.NegotiationRoomConfig?.Uid;
            this.CountAutomaticTables = negotiationRoomConfigDto?.NegotiationRoomConfig?.CountAutomaticTables;
            this.CountManualTables = negotiationRoomConfigDto?.NegotiationRoomConfig?.CountManualTables;
            this.UpdateModelsAndLists(roomDtos, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateNegotiationRoomConfig"/> class.</summary>
        public UpdateNegotiationRoomConfig()
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