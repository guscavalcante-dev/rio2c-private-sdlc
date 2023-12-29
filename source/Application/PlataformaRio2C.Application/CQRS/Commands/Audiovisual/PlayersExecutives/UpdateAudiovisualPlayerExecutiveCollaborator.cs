// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-31-2023
// ***********************************************************************
// <copyright file="UpdateAudiovisualPlayerExecutiveCollaborator.cs" company="Softo">
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
    public class UpdateAudiovisualPlayerExecutiveCollaborator : AudiovisualPlayerExecutiveCollaboratorBaseCommand
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
        /// Initializes a new instance of the <see cref="UpdateAudiovisualPlayerExecutiveCollaborator" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="collaboratorRoles">The roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        public UpdateAudiovisualPlayerExecutiveCollaborator(
            CollaboratorDto entity, 
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos, 
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos, 
            List<CollaboratorGender> genders, 
            List<CollaboratorIndustry> industries, 
            List<CollaboratorRole> collaboratorRoles,
            List<EditionDto> editionsDtos,
            int currentEditionId,
            bool? isAddingToCurrentEdition, 
            bool isJobTitleRequired,
            bool isMiniBioRequired, 
            bool isImageRequired,
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
                currentEditionId, 
                isJobTitleRequired, 
                isMiniBioRequired, 
                isImageRequired, 
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

        /// <summary>Initializes a new instance of the <see cref="UpdateAudiovisualPlayerExecutiveCollaborator"/> class.</summary>
        public UpdateAudiovisualPlayerExecutiveCollaborator()
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