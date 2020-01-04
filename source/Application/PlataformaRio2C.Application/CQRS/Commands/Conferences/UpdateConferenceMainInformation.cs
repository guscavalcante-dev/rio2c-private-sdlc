// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="UpdateConferenceMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateConferenceMainInformation</summary>
    public class UpdateConferenceMainInformation : CreateConference
    {
        public Guid ConferenceUid { get; set; }
        public List<ConferenceSynopsisBaseCommand> Synopsis { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceMainInformation"/> class.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="editionEvents">The edition events.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateConferenceMainInformation(
            ConferenceDto conferenceDto,
            List<EditionEvent> editionEvents,
            List<LanguageDto> languagesDtos)
            : base(conferenceDto, editionEvents, languagesDtos)
        {
            this.ConferenceUid = conferenceDto?.Conference?.Uid ?? Guid.Empty;
            this.UpdateSynopsis(conferenceDto, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceMainInformation"/> class.</summary>
        public UpdateConferenceMainInformation()
        {
        }

        #region Private Methods

        /// <summary>Updates the synopsis.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateSynopsis(ConferenceDto conferenceDto, List<LanguageDto> languagesDtos)
        {
            this.Synopsis = new List<ConferenceSynopsisBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var conferenceSynopsis = conferenceDto?.ConferenceSynopsisDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Synopsis.Add(conferenceSynopsis != null ? new ConferenceSynopsisBaseCommand(conferenceSynopsis) :
                                                               new ConferenceSynopsisBaseCommand(languageDto));
            }
        }

        #endregion
    }
}