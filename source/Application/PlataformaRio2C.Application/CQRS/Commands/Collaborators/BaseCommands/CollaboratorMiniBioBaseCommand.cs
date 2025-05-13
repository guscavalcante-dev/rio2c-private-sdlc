// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="CollaboratorMiniBioBaseCommand.cs" company="Softo">
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
    /// <summary>CollaboratorMiniBioBaseCommand</summary>
    public class CollaboratorMiniBioBaseCommand
    {
        //[AllowHtml]
        [Display(Name = "MiniBio", ResourceType = typeof(Labels))]
        [RequiredIf("IsRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(710, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        //[CkEditorMaxChars(710)]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public bool IsRequired { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorMiniBioBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public CollaboratorMiniBioBaseCommand(CollaboratorMiniBioBaseDto entity, bool isRequired)
        {
            this.Value = entity.Value;
            this.LanguageCode = entity.LanguageDto.Code;
            this.LanguageName = entity.LanguageDto.Name;
            this.IsRequired = isRequired;
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorMiniBioBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public CollaboratorMiniBioBaseCommand(LanguageDto languageDto, bool isRequired)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
            this.IsRequired = isRequired;
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorMiniBioBaseCommand"/> class.</summary>
        public CollaboratorMiniBioBaseCommand()
        {
        }
    }
}