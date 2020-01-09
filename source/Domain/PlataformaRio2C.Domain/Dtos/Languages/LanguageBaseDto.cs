// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="LanguageBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>LanguageBaseDto</summary>
    public class LanguageBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LanguageBaseDto"/> class.</summary>
        public LanguageBaseDto()
        {
        }
    }
}