// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-13-2021
// ***********************************************************************
// <copyright file="CreateAudiovisualPlayerExecutiveCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class CreateAudiovisualPlayerExecutiveCollaborator : AudiovisualPlayerExecutiveCollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAudiovisualPlayerExecutiveCollaborator" /> class.
        /// </summary>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="collaboratorRoles">The roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateAudiovisualPlayerExecutiveCollaborator(
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos, 
            List<LanguageDto> languagesDtos, 
            List<CollaboratorGender> genders, 
            List<CollaboratorIndustry> industries, 
            List<CollaboratorRole> collaboratorRoles,
            List<EditionDto> editionsDtos,
            int currentEditionId,
            bool isJobTitleRequired,
            bool isMiniBioRequired, 
            bool isImageRequired,
            bool isPlayerRequired,
            string userInterfaceLanguage)
        {
            base.UpdateBaseProperties(null, 
                attendeeOrganizationsBaseDtos, 
                languagesDtos, 
                genders, 
                industries, 
                collaboratorRoles, 
                editionsDtos,
                currentEditionId, 
                isJobTitleRequired, 
                isMiniBioRequired, 
                isImageRequired,
                isPlayerRequired,
                userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateAudiovisualPlayerExecutiveCollaborator"/> class.</summary>
        public CreateAudiovisualPlayerExecutiveCollaborator()
        {
        }
    }
}