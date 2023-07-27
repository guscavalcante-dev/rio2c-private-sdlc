// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-27-2023
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationTechnologyDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationTechnologyDto</summary>
    public class AttendeeInnovationOrganizationTechnologyDto
    {
        public string Name { get; set; }
        public string AdditionalInfo { get; set; }

        public InnovationOrganizationTechnologyOption InnovationOrganizationTechnologyOption { get; set; }
        public AttendeeInnovationOrganizationTechnology AttendeeInnovationOrganizationTechnology { get; set; }

        /// <summary>
        /// Gets the name translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTechnologyDto"/> class.</summary>
        public AttendeeInnovationOrganizationTechnologyDto()
        {
        }
    }
}