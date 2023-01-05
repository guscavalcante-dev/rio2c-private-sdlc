// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 12-27-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2022
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionGroupDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationOrganizationTrackOptionGroupedDto</summary>
    public class InnovationOrganizationTrackOptionGroupDto
    {
        public InnovationOrganizationTrackOptionGroup InnovationOrganizationTrackOptionGroup { get; set; }

        public IEnumerable<InnovationOrganizationTrackOption> InnovationOrganizationTrackOptions { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionGroupDto"/> class.</summary>
        public InnovationOrganizationTrackOptionGroupDto()
        {
        }
    }
}