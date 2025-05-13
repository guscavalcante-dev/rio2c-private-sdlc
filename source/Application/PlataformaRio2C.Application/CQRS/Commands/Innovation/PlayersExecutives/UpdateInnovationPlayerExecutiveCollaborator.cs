// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-29-2023
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-06-2024
// ***********************************************************************
// <copyright file="UpdateInnovationPlayerExecutiveCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class UpdateInnovationPlayerExecutiveCollaborator : InnovationPlayerExecutiveCollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }

        public DateTimeOffset? WelcomeEmailSendDate { get; private set; }
        public DateTimeOffset? OnboardingStartDate { get; private set; }
        public DateTimeOffset? OnboardingFinishDate { get; private set; }
        public DateTimeOffset? OnboardingUserDate { get; private set; }
        public DateTimeOffset? OnboardingCollaboratorDate { get; private set; }
        public DateTimeOffset? AudiovisualPlayerTermsAcceptanceDate { get; private set; }
        public DateTimeOffset? InnovationPlayerTermsAcceptanceDate { get; private set; }
        public DateTimeOffset? MusicPlayerTermsAcceptanceDate { get; private set; }
        public DateTimeOffset? AudiovisualProducerBusinessRoundTermsAcceptanceDate { get; private set; }
        public DateTimeOffset? AudiovisualProducerPitchingTermsAcceptanceDate { get; private set; }
        public UserBaseDto UpdaterBaseDto { get; private set; }
        public DateTimeOffset UpdateDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationPlayerExecutiveCollaborator" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="collaboratorRoles">The roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="isPlayerRequired">if set to <c>true</c> [is attendee organization required].</param>
        /// <param name="isVirtualMeetingRequired">if set to <c>true</c> [is virtual meeting required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        public UpdateInnovationPlayerExecutiveCollaborator(
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
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos,
            int currentEditionId,
            bool? isAddingToCurrentEdition,
            bool isJobTitleRequired,
            bool isMiniBioRequired,
            bool isImageRequired,
            bool isPlayerRequired,
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
                innovationOrganizationTrackOptionDtos,
                currentEditionId,
                isJobTitleRequired,
                isMiniBioRequired,
                isImageRequired,
                isPlayerRequired,
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

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInnovationPlayerExecutiveCollaborator"/> class.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="cmd">The command.</param>
        public UpdateInnovationPlayerExecutiveCollaborator(Guid collaboratorUid, CreateInnovationPlayerExecutiveCollaborator cmd)
        {
            this.CollaboratorUid = collaboratorUid;
            this.IsAddingToCurrentEdition = true;

            this.FirstName = cmd.FirstName;
            this.LastNames = cmd.LastNames;
            this.Badge = cmd.Badge;
            this.Email = cmd.Email;
            this.PhoneNumber = cmd.PhoneNumber;
            this.CellPhone = cmd.CellPhone;
            this.SharePublicEmail = cmd.SharePublicEmail;
            this.PublicEmail = cmd.PublicEmail;
            this.Website = cmd.Website;
            this.Linkedin = cmd.Linkedin;
            this.Twitter = cmd.Twitter;
            this.Instagram = cmd.Instagram;
            this.Youtube = cmd.Youtube;
            this.CropperImage = cmd.CropperImage;
            this.JobTitles = cmd.JobTitles;
            this.MiniBios = cmd.MiniBios;
            this.AttendeeCollaboratorActivities = cmd.AttendeeCollaboratorActivities;
            this.InnovationOrganizationTrackGroups = cmd.InnovationOrganizationTrackGroups;
            this.EditionsUids = cmd.EditionsUids;
            this.CollaboratorGenderUid = cmd.CollaboratorGenderUid;
            this.CollaboratorRoleUid = cmd.CollaboratorRoleUid;
            this.CollaboratorIndustryUid = cmd.CollaboratorIndustryUid;
            this.Interests = cmd.Interests;
            this.AttendeeOrganizationBaseCommands = cmd.AttendeeOrganizationBaseCommands;
            this.BirthDate = cmd.BirthDate;
            this.CollaboratorGenderAdditionalInfo = cmd.CollaboratorGenderAdditionalInfo;
            this.CollaboratorRoleAdditionalInfo = cmd.CollaboratorRoleAdditionalInfo;
            this.CollaboratorIndustryAdditionalInfo = cmd.CollaboratorIndustryAdditionalInfo;
            this.HasAnySpecialNeeds = cmd.HasAnySpecialNeeds;
            this.SpecialNeedsDescription = cmd.SpecialNeedsDescription;
            this.HaveYouBeenToRio2CBefore = cmd.HaveYouBeenToRio2CBefore;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateInnovationPlayerExecutiveCollaborator"/> class.</summary>
        public UpdateInnovationPlayerExecutiveCollaborator()
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
            this.AudiovisualPlayerTermsAcceptanceDate = entity.EditionAttendeeCollaboratorBaseDto?.AudiovisualPlayerTermsAcceptanceDate;
            this.InnovationPlayerTermsAcceptanceDate = entity.EditionAttendeeCollaboratorBaseDto?.InnovationPlayerTermsAcceptanceDate;
            this.MusicPlayerTermsAcceptanceDate = entity.EditionAttendeeCollaboratorBaseDto?.MusicPlayerTermsAcceptanceDate;
            this.AudiovisualProducerBusinessRoundTermsAcceptanceDate = entity.EditionAttendeeCollaboratorBaseDto?.AudiovisualProducerBusinessRoundTermsAcceptanceDate;
            this.AudiovisualProducerPitchingTermsAcceptanceDate = entity.EditionAttendeeCollaboratorBaseDto?.AudiovisualProducerPitchingTermsAcceptanceDate;
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