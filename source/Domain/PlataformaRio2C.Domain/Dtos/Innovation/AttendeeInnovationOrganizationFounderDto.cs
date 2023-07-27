// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-27-2023
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationFounderDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationFounderDto</summary>
    public class AttendeeInnovationOrganizationFounderDto //: CollaboratorDto
    {
        public string Name { get; set; }
        public string Curriculum { get; set; }
        public string WorkDedicationName { get; set; }

        public AttendeeInnovationOrganization AttendeeInnovationOrganization { get; set; }
        public AttendeeInnovationOrganizationFounder AttendeeInnovationOrganizationFounder { get; set; }

        /// <summary>
        /// Gets the work dedication name translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetWorkDedicationNameTranslation(string languageCode)
        {
            return this.WorkDedicationName.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationFounderDto"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationFounderDto()
        {
        }
    }
}