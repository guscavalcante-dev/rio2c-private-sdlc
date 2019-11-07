// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="ProjectAdditionalInformationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectAdditionalInformationDto</summary>
    public class ProjectAdditionalInformationDto
    {
        public ProjectAdditionalInformation ProjectAdditionalInformation { get; set; }
        public Language Language { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectAdditionalInformationDto"/> class.</summary>
        public ProjectAdditionalInformationDto()
        {
        }
    }
}