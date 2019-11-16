// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-16-2019
// ***********************************************************************
// <copyright file="ProjectImageLinkDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectImageLinkDto</summary>
    public class ProjectImageLinkDto
    {
        public ProjectImageLink ProjectImageLink { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectImageLinkDto"/> class.</summary>
        public ProjectImageLinkDto()
        {
        }
    }
}