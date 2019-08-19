// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="OrganizationDescriptionBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationDescriptionBaseDto</summary>
    public class OrganizationDescriptionBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Value { get; set; }

        public LanguageBaseDto LanguageDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationDescriptionBaseDto"/> class.</summary>
        public OrganizationDescriptionBaseDto()
        {
        }
    }
}