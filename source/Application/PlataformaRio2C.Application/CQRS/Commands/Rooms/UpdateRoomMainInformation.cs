// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="UpdateRoomMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateRoomMainInformation</summary>
    public class UpdateRoomMainInformation : CreateRoom
    {
        public Guid RoomUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateRoomMainInformation"/> class.</summary>
        /// <param name="roomDto">The room dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateRoomMainInformation(
            RoomDto roomDto,
            List<LanguageDto> languagesDtos)
            : base(roomDto, languagesDtos)
        {
            this.RoomUid = roomDto?.Room?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateRoomMainInformation"/> class.</summary>
        public UpdateRoomMainInformation()
        {
        }
    }
}