// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="AttendeeSalesPlatformDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeSalesPlatformDto</summary>
    public class AttendeeSalesPlatformDto
    {
        public AttendeeSalesPlatform AttendeeSalesPlatform { get; set; }
        public SalesPlatform SalesPlatform { get; set; }
        public Edition Edition { get; set; }
        public IEnumerable<AttendeeSalesPlatformTicketTypeDto> AttendeeSalesPlatformTicketTypesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformDto"/> class.</summary>
        public AttendeeSalesPlatformDto()
        {
        }
    }
}