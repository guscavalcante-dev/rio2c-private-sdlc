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

using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class UpdateMusicPlayerExecutiveCollaborator : MusicPlayerExecutiveCollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }

        public DateTimeOffset? WelcomeEmailSendDate { get; private set; }
        public DateTimeOffset? OnboardingStartDate { get; private set; }
        public DateTimeOffset? OnboardingFinishDate { get; private set; }
        public DateTimeOffset? OnboardingUserDate { get; private set; }
        public DateTimeOffset? OnboardingCollaboratorDate { get; private set; }
        public DateTimeOffset? PlayerTermsAcceptanceDate { get; private set; }
        public DateTimeOffset? ProducerTermsAcceptanceDate { get; private set; }
        public UserBaseDto UpdaterBaseDto { get; private set; }
        public DateTimeOffset UpdateDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicPlayerExecutiveCollaborator" /> class.
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
        /// <param name="isAttendeeOrganizationRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="isVirtualMeetingRequired">if set to <c>true</c> [is isVirtualMeetingRequired required].</param>    
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public UpdateMusicPlayerExecutiveCollaborator(
            CollaboratorDto entity,
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos,
            List<LanguageDto> languagesDtos,
            List<CountryBaseDto> countriesBaseDtos,
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> collaboratorRoles,
            List<EditionDto> editionsDtos,
            List<Activity> activities,
            List<InterestDto> interestsDtos,
            List<TargetAudience> targetAudiences,
            int currentEditionId,
            bool? isAddingToCurrentEdition,
            bool isJobTitleRequired,
            bool isMiniBioRequired,
            bool isImageRequired,
            bool isAttendeeOrganizationRequired,
            bool isVirtualMeetingRequired,
            string userInterfaceLanguage)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Player, Labels.FoundM));
            }

            this.BirthDate = entity?.BirthDate;
            this.HasAnySpecialNeeds = entity?.HasAnySpecialNeeds;
            this.SpecialNeedsDescription = entity?.SpecialNeedsDescription;
            this.CollaboratorGenderUid = entity?.Gender?.Uid;
            this.CollaboratorGenderAdditionalInfo = entity?.CollaboratorGenderAdditionalInfo;
            this.CollaboratorIndustryUid = entity?.Industry?.Uid;
            this.CollaboratorIndustryAdditionalInfo = entity?.CollaboratorIndustryAdditionalInfo;
            this.CollaboratorRoleUid = entity?.CollaboratorRole?.Uid;
            this.CollaboratorRoleAdditionalInfo = entity?.CollaboratorRoleAdditionalInfo;
            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;
            base.UpdateBaseProperties(
                entity,
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
                isAttendeeOrganizationRequired,
                isVirtualMeetingRequired,
                userInterfaceLanguage);

            this.UpdateDropdownProperties(
                entity,
                attendeeOrganizationsBaseDtos,
                genders,
                industries,
                collaboratorRoles,
                editionsDtos,
                currentEditionId,
                userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateMusicPlayerExecutiveCollaborator"/> class.</summary>
        public UpdateMusicPlayerExecutiveCollaborator()
        {
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="collaboratorRoles">The roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdownProperties(
            CollaboratorDto entity,
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos,
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> collaboratorRoles,
            List<EditionDto> editionsDtos,         
            int currentEditionId,
            string userInterfaceLanguage)
        {
            this.WelcomeEmailSendDate = entity.EditionAttendeeCollaboratorBaseDto?.WelcomeEmailSendDate;
            this.OnboardingStartDate = entity.EditionAttendeeCollaboratorBaseDto?.OnboardingStartDate;
            this.OnboardingFinishDate = entity.EditionAttendeeCollaboratorBaseDto?.OnboardingFinishDate;
            this.OnboardingUserDate = entity.EditionAttendeeCollaboratorBaseDto?.OnboardingUserDate;
            this.OnboardingCollaboratorDate = entity.EditionAttendeeCollaboratorBaseDto?.OnboardingCollaboratorDate;
            this.PlayerTermsAcceptanceDate = entity.EditionAttendeeCollaboratorBaseDto?.PlayerTermsAcceptanceDate;
            this.ProducerTermsAcceptanceDate = entity.EditionAttendeeCollaboratorBaseDto?.ProducerTermsAcceptanceDate;
            this.UpdaterBaseDto = entity.UpdaterBaseDto;
            this.UpdateDate = entity.UpdateDate;

            base.UpdateDropdownProperties(
                attendeeOrganizationsBaseDtos,
                genders,
                industries,
                collaboratorRoles,
                editionsDtos,              
                currentEditionId,
                userInterfaceLanguage);
        }
    }
}