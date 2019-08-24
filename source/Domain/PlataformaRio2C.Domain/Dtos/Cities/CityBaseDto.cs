// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="CityBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CityBaseDto</summary>
    public class CityBaseDto : BaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CityBaseDto"/> class.</summary>
        public CityBaseDto()
        {
        }
    }
}