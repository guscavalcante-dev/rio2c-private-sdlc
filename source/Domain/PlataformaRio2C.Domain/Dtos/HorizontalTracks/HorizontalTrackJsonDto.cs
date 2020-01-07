// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="HorizontalTrackJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>HorizontalTrackJsonDto</summary>
    public class HorizontalTrackJsonDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HorizontalTrackJsonDto"/> class.</summary>
        public HorizontalTrackJsonDto()
        {
        }
    }
}