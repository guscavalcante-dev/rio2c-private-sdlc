// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-25-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-25-2023
// ***********************************************************************
// <copyright file="UpdateInnovationOrganizationTrackOptionMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class UpdateInnovationOrganizationTrackOptionMainInformation : CreateInnovationOrganizationTrackOption
    {
        public Guid InnovationOrganizationTrackOptionUid { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationOrganizationTrackOptionMainInformation" /> class.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionDto">The innovation organization track option  dto.</param>
        /// <param name="innovationOrganizationTrackOptionGroupDtos">The innovation organization track option group dtos.</param>
        public UpdateInnovationOrganizationTrackOptionMainInformation(
            InnovationOrganizationTrackOptionDto innovationOrganizationTrackOptionDto, 
            List<InnovationOrganizationTrackOptionGroupDto> innovationOrganizationTrackOptionGroupDtos)
        {
            this.InnovationOrganizationTrackOptionUid = innovationOrganizationTrackOptionDto.Uid;
            this.Name = innovationOrganizationTrackOptionDto.Name;
            base.InnovationOrganizationOptionGroupUid = innovationOrganizationTrackOptionDto.GroupUid;

            base.UpdateDropdowns(innovationOrganizationTrackOptionGroupDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationOrganizationTrackOptionMainInformation" /> class.
        /// </summary>
        public UpdateInnovationOrganizationTrackOptionMainInformation()
        {
        }
    }
}