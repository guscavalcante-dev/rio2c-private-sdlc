// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-24-2021
// ***********************************************************************
// <copyright file="UpdateManager.cs" company="Softo">
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
    /// <summary>UpdateManager</summary>
    public class UpdateManager : ManagerBaseCommand
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
        /// Initializes a new instance of the <see cref="UpdateManager"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="genders">The genders.</param>
        /// <param name="industries">The industries.</param>
        /// <param name="collaboratorRoles">The collaborator roles.</param>
        /// <param name="editionsDtos">The editions dtos.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="currentEditionId">The current edition identifier.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="DomainException"></exception>
        public UpdateManager(
            CollaboratorDto entity,
            List<Role> roles,
            List<CollaboratorType> collaboratorTypes, 
            bool isAddingToCurrentEdition,
            string userInterfaceLanguage)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Manager, Labels.FoundM));
            }

            base.IsUpdatingManager = true;
            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition;

            base.UpdateBaseProperties(entity, roles, collaboratorTypes, userInterfaceLanguage);
            this.UpdateDropdownProperties(entity, roles, collaboratorTypes, userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateManager"/> class.
        /// </summary>
        public UpdateManager()
        {
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdownProperties(
            CollaboratorDto entity,
            List<Role> roles,
            List<CollaboratorType> collaboratorTypes,
            string userInterfaceLanguage)
        {
            this.WelcomeEmailSendDate = entity.EditionAttendeeCollaboratorBaseDto?.WelcomeEmailSendDate;
            this.OnboardingStartDate = entity.EditionAttendeeCollaboratorBaseDto?.OnboardingStartDate;
            this.OnboardingFinishDate = entity.EditionAttendeeCollaboratorBaseDto?.OnboardingFinishDate;
            this.OnboardingUserDate = entity.EditionAttendeeCollaboratorBaseDto?.OnboardingUserDate;
            this.OnboardingCollaboratorDate = entity.EditionAttendeeCollaboratorBaseDto?.OnboardingCollaboratorDate;
            this.PlayerTermsAcceptanceDate = entity.EditionAttendeeCollaboratorBaseDto?.PlayerTermsAcceptanceDate;
            this.ProducerTermsAcceptanceDate = entity.EditionAttendeeCollaboratorBaseDto?.ProducerTermsAcceptanceDate;
            this.UpdaterBaseDto = entity.UpdaterDto;
            this.UpdateDate = entity.UpdateDate;
            base.UpdateDropdownProperties(roles, collaboratorTypes, userInterfaceLanguage);
        }
    }
}