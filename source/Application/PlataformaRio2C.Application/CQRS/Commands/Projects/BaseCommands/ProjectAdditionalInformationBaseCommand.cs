// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="ProjectAdditionalInformationBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ProjectAdditionalInformationBaseCommand</summary>
    public class ProjectAdditionalInformationBaseCommand
    {
        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        [RequiredIf("IsRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(1500, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public bool IsRequired { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectAdditionalInformationBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public ProjectAdditionalInformationBaseCommand(ProjectAdditionalInformationDto entity, bool isRequired)
        {
            this.Value = entity.ProjectAdditionalInformation.Value;
            this.LanguageCode = entity.Language.Code;
            this.LanguageName = entity.Language.Name;
            this.IsRequired = isRequired;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectAdditionalInformationBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public ProjectAdditionalInformationBaseCommand(LanguageDto languageDto, bool isRequired)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
            this.IsRequired = isRequired;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectAdditionalInformationBaseCommand"/> class.</summary>
        public ProjectAdditionalInformationBaseCommand()
        {
        }
    }
}