// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="CreateCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateCollaborator</summary>
    public class CreateCollaborator : CollaboratorBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateCollaborator"/> class.</summary>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        public CreateCollaborator(
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos, 
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            bool isJobTitleRequired,
            bool isMiniBioRequired)
        {
            this.UpdateBaseProperties(null, attendeeOrganizationsBaseDtos, languagesDtos, countriesBaseDtos, isJobTitleRequired, isMiniBioRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateCollaborator"/> class.</summary>
        public CreateCollaborator()
        {
        }
    }
}