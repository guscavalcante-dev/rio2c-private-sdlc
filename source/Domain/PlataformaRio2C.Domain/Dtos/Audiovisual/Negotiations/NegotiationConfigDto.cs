// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="NegotiationConfigDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationConfigDto</summary>
    public class NegotiationConfigDto
    {
        public NegotiationConfig NegotiationConfig { get; set; }
        public IEnumerable<NegotiationRoomConfigDto> NegotiationRoomConfigDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationConfigDto"/> class.</summary>
        public NegotiationConfigDto()
        {
        }
    }
}