// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-19-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2023
// ***********************************************************************
// <copyright file="UpdateInnovationOrganizationTrackOptionGroupMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class UpdateInnovationOrganizationTrackOptionGroupMainInformation : CreateInnovationOrganizationTrackOptionGroup
    {
        public Guid InnovationOrganizationTrackOptionGroupUid { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationOrganizationTrackOptionGroupMainInformation"/> class.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionGroupDto">The innovation organization track option group dto.</param>
        public UpdateInnovationOrganizationTrackOptionGroupMainInformation(InnovationOrganizationTrackOptionGroupDto innovationOrganizationTrackOptionGroupDto)
        {
            this.InnovationOrganizationTrackOptionGroupUid = innovationOrganizationTrackOptionGroupDto.Uid;
            this.Name = innovationOrganizationTrackOptionGroupDto.GroupName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationOrganizationTrackOptionGroupMainInformation" /> class.
        /// </summary>
        public UpdateInnovationOrganizationTrackOptionGroupMainInformation()
        {
        }
    }
}