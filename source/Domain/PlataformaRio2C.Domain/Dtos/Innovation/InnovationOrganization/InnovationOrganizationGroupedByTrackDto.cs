// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-06-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationGroupedByTrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationOrganizationGroupedByTrackDto</summary>
    public class InnovationOrganizationGroupedByTrackDto
    {
        public string TrackName { get; set; }

        /// <summary>
        /// Total of Innovation Projects with this Track in current Edition
        /// </summary>
        public int InnovationProjectsTotalCount { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationGroupedByTrackDto"/> class.</summary>
        public InnovationOrganizationGroupedByTrackDto()
        {
        }
    }
}