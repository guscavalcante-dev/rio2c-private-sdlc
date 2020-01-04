// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
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
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateConference</summary>
    public class CreateConference : BaseCommand
    {
        [Display(Name = "Event", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? EditionEventUid { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public DateTime? Date { get; set; }

        [Display(Name = "StartTime", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string StartTime { get; set; }

        [Display(Name = "EndTime", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string EndTime { get; set; }

        public List<ConferenceTitleBaseCommand> Titles { get; set; }

        public List<EditionEvent> EditionEvents { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CreateConference"/> class.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="editionEvents">The edition events.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreateConference(
            ConferenceDto conferenceDto,
            List<EditionEvent> editionEvents,
            List<LanguageDto> languagesDtos)
        {
            this.EditionEventUid = conferenceDto?.EditionEvent?.Uid;
            this.Date = conferenceDto?.Conference?.StartDate.Date;
            this.StartTime = conferenceDto?.Conference?.StartDate.ToShortTimeString();
            this.EndTime = conferenceDto?.Conference?.EndDate.ToShortTimeString();
            this.UpdateTitles(conferenceDto, languagesDtos);
            this.UpdateDropdowns(editionEvents);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateConference"/> class.</summary>
        public CreateConference()
        {
        }

        /// <summary>Updates the dropdowns.</summary>
        /// <param name="editionEvents">The edition events.</param>
        public void UpdateDropdowns(List<EditionEvent> editionEvents)
        {
            this.EditionEvents = editionEvents;
        }

        #region Private Methods

        /// <summary>Updates the titles.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateTitles(ConferenceDto conferenceDto, List<LanguageDto> languagesDtos)
        {
            this.Titles = new List<ConferenceTitleBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var conferenceTitle = conferenceDto?.ConferenceTitleDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Titles.Add(conferenceTitle != null ? new ConferenceTitleBaseCommand(conferenceTitle) :
                                                          new ConferenceTitleBaseCommand(languageDto));
            }
        }

        #endregion
    }
}