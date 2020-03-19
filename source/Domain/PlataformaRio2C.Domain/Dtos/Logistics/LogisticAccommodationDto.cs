// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-19-2020
// ***********************************************************************
// <copyright file="LogisticAccommodationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>LogisticAccommodationDto</summary>
    public class LogisticAccommodationDto
    {
        public LogisticAccommodation LogisticAccommodation { get; set; }
        public PlaceDto PlaceDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticAccommodationDto"/> class.</summary>
        public LogisticAccommodationDto()
        {
        }
    }
}