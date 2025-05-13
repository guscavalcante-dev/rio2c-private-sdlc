// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-24-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="AttendeeLogisticSponsorJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeLogisticSponsorJsonDto</summary>
    public class AttendeeLogisticSponsorJsonDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsInCurrentEdition { get; set; }
        public bool IsInOtherEdition { get; set; }
        public bool IsAirfareTicketRequired { get; set; }
        public bool IsOtherRequired { get; set; }
        public bool IsOthers { get; set; }

        /// <summary>Gets the name by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameByLanguageCode(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }
    }
}