// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-18-2019
// ***********************************************************************
// <copyright file="HoldingDescriptionBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>HoldingDescriptionBaseCommand</summary>
    public class HoldingDescriptionBaseCommand
    {
        [AllowHtml]
        [StringLength(8000, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingDescriptionBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public HoldingDescriptionBaseCommand(HoldingDescriptionBaseDto entity)
        {
            this.Value = entity.Value;
            this.LanguageCode = entity.LanguageDto.Code;
            this.LanguageName = entity.LanguageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="HoldingDescriptionBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public HoldingDescriptionBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="HoldingDescriptionBaseCommand"/> class.</summary>
        public HoldingDescriptionBaseCommand()
        {
        }
    }
}