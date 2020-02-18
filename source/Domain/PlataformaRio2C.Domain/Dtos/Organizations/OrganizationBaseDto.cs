// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="OrganizationBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationBaseDto</summary>
    public class OrganizationBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public HoldingBaseDto HoldingBaseDto { get; set; }
        public bool IsCompanyNumberRequired { get; set; }
        public string Document { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsInCurrentEdition { get; set; }
        public bool IsInOtherEdition { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationBaseDto"/> class.</summary>
        public OrganizationBaseDto()
        {
        }
    }
}