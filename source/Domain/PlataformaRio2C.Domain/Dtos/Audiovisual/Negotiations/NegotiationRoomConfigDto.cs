// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="NegotiationRoomConfigDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationRoomConfigDto</summary>
    public class NegotiationRoomConfigDto
    {
        public NegotiationRoomConfig NegotiationRoomConfig { get; set; }
        public RoomDto RoomDto { get; set; }
        public NegotiationConfig NegotiationConfig { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationRoomConfigDto"/> class.</summary>
        public NegotiationRoomConfigDto()
        {
        }
    }
}