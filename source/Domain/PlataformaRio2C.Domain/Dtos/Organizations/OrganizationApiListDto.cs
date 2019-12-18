// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-30-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-18-2019
// ***********************************************************************
// <copyright file="OrganizationApiListDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationApiListDto</summary>
    public class OrganizationApiListDto
    {
        public Guid Uid { get; set; }
        public string CompanyName { get; set; }
        public string TradeName { get; set; }
        public string Document { get; set; }
        public DateTime? ImageUploadDate { get; set; }
        public int? ApiHighlightPosition { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationApiListDto"/> class.</summary>
        public OrganizationApiListDto()
        {
        }
    }
}