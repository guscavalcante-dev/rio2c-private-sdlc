// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-27-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationObjectiveDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationObjectiveDto</summary>
    public class AttendeeInnovationOrganizationObjectiveDto
    {
        public string Name { get; set; }
        public string AdditionalInfo { get; set; }

        public InnovationOrganizationObjectivesOption InnovationOrganizationObjectivesOption { get; set; }
        public AttendeeInnovationOrganizationObjective AttendeeInnovationOrganizationObjective { get; set; }

        /// <summary>
        /// Gets the name translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationObjectiveDto"/> class.</summary>
        public AttendeeInnovationOrganizationObjectiveDto()
        {
        }
    }
}