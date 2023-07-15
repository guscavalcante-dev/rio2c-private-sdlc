// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-13-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2023
// ***********************************************************************
// <copyright file="CreateInnovationOrganizationTrackOption.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateInnovationOrganizationTrackOption</summary>
    public class CreateInnovationOrganizationTrackOption : BaseCommand
    {
        [Display(Name = "Vertical", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? InnovationOrganizationOptionGroupUid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Name { get; set; }

        public List<InnovationOrganizationTrackOptionGroupDto> InnovationOrganizationTrackOptionGroupDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganizationTrackOption"/> class.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionGroupDtos">The innovation organization track option group dtos.</param>
        public CreateInnovationOrganizationTrackOption(List<InnovationOrganizationTrackOptionGroupDto> innovationOrganizationTrackOptionGroupDtos)
        {
            this.UpdateDropdowns(innovationOrganizationTrackOptionGroupDtos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganizationTrackOption"/> class.
        /// </summary>
        public CreateInnovationOrganizationTrackOption()
        {
        }

        /// <summary>
        /// Updates the dropdowns.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionGroupDtos">The innovation organization track option group dtos.</param>
        public void UpdateDropdowns(List<InnovationOrganizationTrackOptionGroupDto> innovationOrganizationTrackOptionGroupDtos)
        {
            this.InnovationOrganizationTrackOptionGroupDtos = innovationOrganizationTrackOptionGroupDtos;
        }
    }
}