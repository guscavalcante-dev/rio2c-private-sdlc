// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="ProjectTitleDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectTitleDto</summary>
    public class ProjectTitleDto
    {
        public ProjectTitle ProjectTitle { get; set; }
        public Language Language { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectTitleDto"/> class.</summary>
        public ProjectTitleDto()
        {
        }
    }
}