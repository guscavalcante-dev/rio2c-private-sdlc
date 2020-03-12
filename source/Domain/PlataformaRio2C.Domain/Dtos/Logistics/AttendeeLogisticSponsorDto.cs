// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Danta Ruiz
// Created          : 03-12-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-12-2020
// ***********************************************************************
// <copyright file="AttendeeLogisticSponsorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeLogisticSponsorDto</summary>
    public class AttendeeLogisticSponsorDto
    {
        public AttendeeLogisticSponsor AttendeeLogisticSponsor { get; set; }
        public LogisticSponsor LogisticSponsor { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeLogisticSponsorDto"/> class.</summary>
        public AttendeeLogisticSponsorDto()
        {
        }

        /// <summary>Gets the name translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.LogisticSponsor?.Name?.GetSeparatorTranslation(languageCode, Language.Separator);
        }
    }
}