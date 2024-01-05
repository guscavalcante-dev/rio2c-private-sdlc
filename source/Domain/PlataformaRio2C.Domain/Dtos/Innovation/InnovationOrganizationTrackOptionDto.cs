// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 12-27-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-28-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationOrganizationTrackOptionDto</summary>
    public class InnovationOrganizationTrackOptionDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public bool HasAdditionalInfo { get; set; }

        public Guid GroupUid { get; set; }
        public string GroupName { get; set; }

        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        [Obsolete("Use the 'InnovationOrganizationTrackOptionDto' properties instead of this. This property will be deleted!")]
        public InnovationOrganizationTrackOptionGroup InnovationOrganizationTrackOptionGroup { get; set; }

        [Obsolete("Use the 'InnovationOrganizationTrackOptionDto' properties instead of this. This property will be deleted!")]
        public InnovationOrganizationTrackOption InnovationOrganizationTrackOption { get; set; }

        /// <summary>
        /// Gets the name translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>
        /// Gets the group name translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetGroupNameTranslation(string languageCode)
        {
            return this.GroupName.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionDto"/> class.</summary>
        public InnovationOrganizationTrackOptionDto()
        {
        }
    }
}