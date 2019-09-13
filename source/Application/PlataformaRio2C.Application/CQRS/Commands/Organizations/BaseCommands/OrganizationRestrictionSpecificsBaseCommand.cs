// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="OrganizationRestrictionSpecificsBaseCommand.cs" company="Softo">
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
    /// <summary>OrganizationRestrictionSpecificsBaseCommand</summary>
    public class OrganizationRestrictionSpecificsBaseCommand
    {
        [AllowHtml]
        [StringLength(8000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationRestrictionSpecificsBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public OrganizationRestrictionSpecificsBaseCommand(OrganizationRestrictionSpecificBaseDto entity)
        {
            this.Value = entity.Value;
            this.LanguageCode = entity.LanguageDto.Code;
            this.LanguageName = entity.LanguageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationRestrictionSpecificsBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public OrganizationRestrictionSpecificsBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationRestrictionSpecificsBaseCommand"/> class.</summary>
        public OrganizationRestrictionSpecificsBaseCommand()
        {
        }
    }
}