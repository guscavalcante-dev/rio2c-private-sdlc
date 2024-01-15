// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Elton Assunção
// Created          : 12-29-2023
//
// Last Modified By : Elton Assunção
// Last Modified On : 12-29-2023
// ***********************************************************************
// <copyright file="CreateMusicPlayerExecutiveCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class CreateMusicPlayerExecutiveCollaborator : MusicPlayerExecutiveCollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicPlayerExecutiveCollaborator" /> class.
        /// </summary>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="collaboratorRoles">The collaborator roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="targetAudiences">The targetAudiences track option dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="isPlayerRequired">if set to <c>true</c> [is player required].</param>
        /// <param name="isVirtualMeetingRequired">if set to <c>true</c> [is virtual meeting required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateMusicPlayerExecutiveCollaborator(
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos,
            List<LanguageDto> languagesDtos,
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> collaboratorRoles,
            List<EditionDto> editionsDtos,
            List<Activity> activities,
            List<InterestDto> interestsDtos,
            List<TargetAudience> targetAudiences,
            int currentEditionId,
            bool isJobTitleRequired,
            bool isMiniBioRequired,
            bool isImageRequired,
            bool isPlayerRequired,
            bool isVirtualMeetingRequired,
            string userInterfaceLanguage)
        {
            base.UpdateBaseProperties(
                null,
                attendeeOrganizationsBaseDtos,
                languagesDtos,
                genders,
                industries,
                collaboratorRoles,
                editionsDtos,
                activities,
                interestsDtos,
                targetAudiences,
                currentEditionId,
                isJobTitleRequired,
                isMiniBioRequired,
                isImageRequired,
                isPlayerRequired,
                isVirtualMeetingRequired,
                userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMusicPlayerExecutiveCollaborator" /> class.
        /// </summary>
        public CreateMusicPlayerExecutiveCollaborator()
        {
        }
    }
}