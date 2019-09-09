// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="CollaboratorJobTitleBaseCommand.cs" company="Softo">
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
    /// <summary>CollaboratorJobTitleBaseCommand</summary>
    public class CollaboratorJobTitleBaseCommand
    {
        [StringLength(81, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorJobTitleBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public CollaboratorJobTitleBaseCommand(CollaboratorJobTitleBaseDto entity)
        {
            this.Value = entity.Value;
            this.LanguageCode = entity.LanguageDto.Code;
            this.LanguageName = entity.LanguageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorJobTitleBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public CollaboratorJobTitleBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorJobTitleBaseCommand"/> class.</summary>
        public CollaboratorJobTitleBaseCommand()
        {
        }
    }
}