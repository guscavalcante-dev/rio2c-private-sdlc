// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 12-15-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-15-2023
// ***********************************************************************
// <copyright file="PlayerOrganizationApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>PlayerOrganizationApiDto</summary>
    public class PlayerOrganizationApiDto
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string TradeName { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public int? ApiHighlightPosition { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        public IEnumerable<OrganizationDescriptionBaseDto> OrganizationDescriptionBaseDtos { get; set; }
        public IEnumerable<OrganizationInterestDto> OrganizationInterestDtos { get; set; }
        public IEnumerable<CollaboratorDto> CollaboratorsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="PlayerOrganizationApiDto"/> class.</summary>
        public PlayerOrganizationApiDto()
        {
        }
    }
}