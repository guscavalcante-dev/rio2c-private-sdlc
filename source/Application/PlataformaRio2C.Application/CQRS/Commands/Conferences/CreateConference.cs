// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="CreateConference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateConference</summary>
    public class CreateConference : BaseCommand
    {
        [Display(Name = "StartDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "EndDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? EndDate { get; set; }

        public List<ConferenceTitleBaseCommand> Titles { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateConference"/> class.</summary>
        public CreateConference(List<LanguageDto> languagesDtos)
        {
            this.UpdateTitles(null, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateConference"/> class.</summary>
        public CreateConference()
        {
        }

        #region Private Methods

        /// <summary>Updates the titles.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateTitles(ConferenceDto entity, List<LanguageDto> languagesDtos)
        {
            this.Titles = new List<ConferenceTitleBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var conferenceTitle = entity?.ConferenceTitleDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Titles.Add(conferenceTitle != null ? new ConferenceTitleBaseCommand(conferenceTitle) :
                                                          new ConferenceTitleBaseCommand(languageDto));
            }
        }

        #endregion
    }
}