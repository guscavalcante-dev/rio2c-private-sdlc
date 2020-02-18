// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-12-2019
// ***********************************************************************
// <copyright file="CreateCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateCollaborator</summary>
    public class CreateCollaborator : CollaboratorDataBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateCollaborator"/> class.</summary>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public CreateCollaborator(
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos, 
            List<LanguageDto> languagesDtos, 
            List<CollaboratorGender> genders, 
            List<CollaboratorIndustry> industries, 
            List<CollaboratorRole> roles,
            List<EditionDto> editionsDtos,
            int currentEditionId,
            bool isJobTitleRequired,
            bool isMiniBioRequired, 
            bool isImageRequired,
            string userInterfaceLanguage)
        {
            this.UpdateBaseProperties(null, 
                attendeeOrganizationsBaseDtos, 
                languagesDtos, 
                genders, 
                industries, 
                roles, 
                editionsDtos, 
                currentEditionId, 
                isJobTitleRequired, 
                isMiniBioRequired, 
                isImageRequired, 
                userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateCollaborator"/> class.</summary>
        public CreateCollaborator()
        {
        }
    }
}