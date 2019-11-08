// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-08-2019
// ***********************************************************************
// <copyright file="ProjectInterestDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectTargetAudienceDto</summary>
    public class ProjectTargetAudienceDto
    {
        public TargetAudience TargetAudience { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectTargetAudienceDto"/> class.</summary>
        public ProjectTargetAudienceDto()
        {
        }
    }
}