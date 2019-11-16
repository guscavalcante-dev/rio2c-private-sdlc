// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-16-2019
// ***********************************************************************
// <copyright file="ProjectTeaserLinkDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectTeaserLinkDto</summary>
    public class ProjectTeaserLinkDto
    {
        public ProjectTeaserLink ProjectTeaserLink { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectTeaserLinkDto"/> class.</summary>
        public ProjectTeaserLinkDto()
        {
        }
    }
}