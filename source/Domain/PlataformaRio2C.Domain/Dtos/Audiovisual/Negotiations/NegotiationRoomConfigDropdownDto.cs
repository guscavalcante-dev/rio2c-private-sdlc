// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="NegotiationRoomConfigDropdownDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationRoomConfigDropdownDto</summary>
    public class NegotiationRoomConfigDropdownDto
    {
        public Guid NegotiationRoomConfigUid { get; set; }
        public Guid RoomUid { get; set; }
        public string RoomName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationRoomConfigDropdownDto"/> class.</summary>
        public NegotiationRoomConfigDropdownDto()
        {
        }
    }
}