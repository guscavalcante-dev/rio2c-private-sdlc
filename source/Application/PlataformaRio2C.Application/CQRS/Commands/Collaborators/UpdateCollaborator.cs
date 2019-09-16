// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="UpdateCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateCollaborator</summary>
    public class UpdateCollaborator : CollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }
        public UserBaseDto UpdaterBaseDto { get; set; }
        public DateTime UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaborator"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public UpdateCollaborator(
            CollaboratorDto entity, 
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos, 
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos, 
            bool? isAddingToCurrentEdition, 
            bool isJobTitleRequired,
            bool isMiniBioRequired, 
            bool isImageRequired)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Player, Labels.FoundM));
            }

            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;
            this.UpdaterBaseDto = entity.UpdaterDto;
            this.UpdateDate = entity.UpdateDate;
            this.UpdateBaseProperties(entity, attendeeOrganizationsBaseDtos, languagesDtos, countriesBaseDtos, isJobTitleRequired, isMiniBioRequired, isImageRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaborator"/> class.</summary>
        public UpdateCollaborator()
        {
        }
    }
}