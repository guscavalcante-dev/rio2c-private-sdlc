// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorSiteMainInformationWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorSiteMainInformationWidgetDto</summary>
    public class AttendeeCollaboratorSiteMainInformationWidgetDto
    {
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }
        public User User { get; set; }

        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBioDtos { get; set; }
        public IEnumerable<CollaboratorEditionParticipationBaseDto> EditionParticipationDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorSiteMainInformationWidgetDto"/> class.</summary>
        public AttendeeCollaboratorSiteMainInformationWidgetDto()
        {
        }

        /// <summary>Gets the job title dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public CollaboratorJobTitleBaseDto GetJobTitleDtoByLanguageCode(string culture)
        {
            return this.JobTitlesDtos?.FirstOrDefault(dd => dd.LanguageDto.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        /// <summary>Gets the mini bio dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public CollaboratorMiniBioBaseDto GetMiniBioDtoByLanguageCode(string culture)
        {
            return this.MiniBioDtos?.FirstOrDefault(dd => dd.LanguageDto.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }
    }
}