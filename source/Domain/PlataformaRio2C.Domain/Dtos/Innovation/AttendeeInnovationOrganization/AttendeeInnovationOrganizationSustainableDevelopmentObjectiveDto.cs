// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-27-2023
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>

using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    public class AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto
    {
        public string Name { get; set; }
        public string AdditionalInfo { get; set; }

        public AttendeeInnovationOrganizationSustainableDevelopmentObjective AttendeeInnovationOrganizationSustainableDevelopmentObjective { get; set; }
        public InnovationOrganizationSustainableDevelopmentObjectivesOption InnovationOrganizationSustainableDevelopmentObjectivesOption { get; set; }

        /// <summary>
        /// Gets the name translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto" /> class.</summary>
        public AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto()
        {

        }
    }
}
