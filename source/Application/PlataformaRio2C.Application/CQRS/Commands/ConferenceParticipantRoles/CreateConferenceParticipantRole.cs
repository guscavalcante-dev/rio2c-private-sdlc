// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="CreateConferenceParticipantRole.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateConferenceParticipantRole</summary>
    public class CreateConferenceParticipantRole : BaseCommand
    {
        public List<ConferenceParticipantRoleTitleBaseCommand> Titles { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateConferenceParticipantRole"/> class.</summary>
        /// <param name="conferenceParticipantRoleDto">The conferenceParticipantRole dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreateConferenceParticipantRole(
            ConferenceParticipantRoleDto conferenceParticipantRoleDto,
            List<LanguageDto> languagesDtos)
        {
            this.UpdateTitles(conferenceParticipantRoleDto, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateConferenceParticipantRole"/> class.</summary>
        public CreateConferenceParticipantRole()
        {
        }

        #region Private Methods

        /// <summary>Updates the names.</summary>
        /// <param name="conferenceParticipantRoleDto">The conferenceParticipantRole dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateTitles(ConferenceParticipantRoleDto conferenceParticipantRoleDto, List<LanguageDto> languagesDtos)
        {
            this.Titles = new List<ConferenceParticipantRoleTitleBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {       
                var conferenceParticipantRoleTitle = conferenceParticipantRoleDto?.GetConferenceParticipantRoleTitleDtoByLanguageCode(languageDto.Code);
                this.Titles.Add(conferenceParticipantRoleTitle != null ? new ConferenceParticipantRoleTitleBaseCommand(conferenceParticipantRoleTitle) :
                                                                         new ConferenceParticipantRoleTitleBaseCommand(languageDto));
            }
        }

        #endregion
    }
}