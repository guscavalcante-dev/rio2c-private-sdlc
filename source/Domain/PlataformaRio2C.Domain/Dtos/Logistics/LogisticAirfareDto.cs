// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="LogisticAirfareDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>
    /// LogisticAirfareDto.
    /// </summary>
    public class LogisticAirfareDto
    {
        public LogisticAirfare LogisticAirfare { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticAirfareDto"/> class.</summary>
        public LogisticAirfareDto()
        {
        }
    }
}