// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 12-27-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-04-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationOrganizationTrackOptionDto</summary>
    public class InnovationOrganizationTrackOptionDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        public InnovationOrganizationTrackOptionGroup InnovationOrganizationTrackOptionGroup { get; set; }
        public InnovationOrganizationTrackOption InnovationOrganizationTrackOption { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionDto"/> class.</summary>
        public InnovationOrganizationTrackOptionDto()
        {
        }
    }
}