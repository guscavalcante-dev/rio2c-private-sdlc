// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="RoomDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>RoomDto</summary>
    public class RoomDto
    {
        public Room Room { get; set; }
        public IEnumerable<RoomName> RoomNames { get; set; }

        /// <summary>Initializes a new instance of the <see cref="RoomDto"/> class.</summary>
        public RoomDto()
        {
        }
    }
}