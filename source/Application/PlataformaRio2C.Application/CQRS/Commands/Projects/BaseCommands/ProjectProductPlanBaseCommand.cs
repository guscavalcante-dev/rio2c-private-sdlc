// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="ProjectProductPlanBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ProjectProductPlanBaseCommand</summary>
    public class ProjectProductPlanBaseCommand
    {
        [Display(Name = "ProductionPlans", ResourceType = typeof(Labels))]
        [RequiredIf("IsRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(3000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public bool IsRequired { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectProductPlanBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public ProjectProductPlanBaseCommand(ProjectProductionPlanDto entity, bool isRequired)
        {
            this.Value = entity.ProjectProductionPlan.Value;
            this.LanguageCode = entity.Language.Code;
            this.LanguageName = entity.Language.Name;
            this.IsRequired = isRequired;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectProductPlanBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public ProjectProductPlanBaseCommand(LanguageDto languageDto, bool isRequired)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
            this.IsRequired = isRequired;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectProductPlanBaseCommand"/> class.</summary>
        public ProjectProductPlanBaseCommand()
        {
        }
    }
}