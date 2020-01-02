// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2019
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

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateConferenceMainInformation</summary>
    public class UpdateConferenceMainInformation : CreateConference
    {
        public Guid ConferenceUid { get; set; }
        public List<ConferenceSynopsisBaseCommand> Synopsis { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceMainInformation"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateConferenceMainInformation(
            ConferenceDto entity, 
            List<LanguageDto> languagesDtos)
            : base(entity, languagesDtos)
        {
            this.ConferenceUid = entity?.Conference?.Uid ?? Guid.Empty;
            this.UpdateSynopsis(entity, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceMainInformation"/> class.</summary>
        public UpdateConferenceMainInformation()
        {
        }

        #region Private Methods

        /// <summary>Updates the synopsis.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateSynopsis(ConferenceDto entity, List<LanguageDto> languagesDtos)
        {
            this.Synopsis = new List<ConferenceSynopsisBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var conferenceSynopsis = entity?.ConferenceSynopsisDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Synopsis.Add(conferenceSynopsis != null ? new ConferenceSynopsisBaseCommand(conferenceSynopsis) :
                                                               new ConferenceSynopsisBaseCommand(languageDto));
            }
        }

        #endregion
    }
}