// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="LogisticSponsorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>LogisticSponsorDto</summary>
    public class LogisticSponsorDto
    {
        public LogisticSponsor LogisticSponsor { get; set; }
        public AttendeeLogisticSponsor AttendeeLogisticSponsor { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorDto"/> class.</summary>
        public LogisticSponsorDto()
        {
        }

        /// <summary>Gets the name by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameByLanguageCode(string languageCode)
        {
            return this.LogisticSponsor?.Name?.GetSeparatorTranslation(languageCode, Language.Separator);
        }
    }
}