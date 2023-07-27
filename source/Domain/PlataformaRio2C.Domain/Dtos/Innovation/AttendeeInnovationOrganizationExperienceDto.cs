// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-27-2023
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationExperienceDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationExperienceDto</summary>
    public class AttendeeInnovationOrganizationExperienceDto
    {
        public string Name { get; set; }
        public string AdditionalInfo { get; set; }

        public InnovationOrganizationExperienceOption InnovationOrganizationExperienceOption { get; set; }
        public AttendeeInnovationOrganizationExperience AttendeeInnovationOrganizationExperience { get; set; }

        /// <summary>
        /// Gets the name translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationExperienceDto"/> class.</summary>
        public AttendeeInnovationOrganizationExperienceDto()
        {
        }
    }
}