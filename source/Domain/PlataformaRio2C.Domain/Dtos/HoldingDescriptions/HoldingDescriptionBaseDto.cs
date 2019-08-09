// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="HoldingDescriptionBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>HoldingDescriptionBaseDto</summary>
    public class HoldingDescriptionBaseDto
    {
        public Guid Uid { get; set; }
        public string Value { get; set; }

        public LanguageBaseDto LanguageDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingDescriptionBaseDto"/> class.</summary>
        public HoldingDescriptionBaseDto()
        {
        }
    }
}