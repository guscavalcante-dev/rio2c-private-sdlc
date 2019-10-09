// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorDto</summary>
    public class AttendeeCollaboratorDto
    {
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorDto"/> class.</summary>
        public AttendeeCollaboratorDto()
        {
        }

        /// <summary>Determines whether this instance has image.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.</returns>
        public bool HasImage()
        {
            return this.Collaborator?.ImageUploadDate != null;
        }

        /// <summary>Gets the job title dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public CollaboratorJobTitleBaseDto GetJobTitleDtoByLanguageCode(string culture)
        {
            return this.JobTitlesDtos?.FirstOrDefault(dd => dd.LanguageDto.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }
    }
}