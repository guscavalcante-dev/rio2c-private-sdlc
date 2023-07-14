// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-13-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2023
// ***********************************************************************
// <copyright file="CreateInnovationOrganizationTrackOptionGroup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateInnovationOrganizationTrackOptionGroup</summary>
    public class CreateInnovationOrganizationTrackOptionGroup : BaseCommand
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Name { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateInnovationOrganizationTrackOptionGroup"/> class.</summary>
        public CreateInnovationOrganizationTrackOptionGroup()
        {
        }
    }
}