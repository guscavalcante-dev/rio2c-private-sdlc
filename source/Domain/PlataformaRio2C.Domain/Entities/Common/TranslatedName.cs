// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-28-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-28-2020
// ***********************************************************************
// <copyright file="TranslatedName.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>TranslatedName</summary>
    public class TranslatedName
    {
        public static readonly int ValueMinLength = 1;
        public static readonly int ValueMaxLength = 256;

        public int LanguageId { get; private set; }
        public string Value { get; private set; }
        public Language Language { get; private set; }        

        /// <summary>Initializes a new instance of the <see cref="TrackName"/> class.</summary>
        /// <param name="value">The value.</param>
        /// <param name="language">The language.</param>
        /// <param name="userId">The user identifier.</param>
        public TranslatedName(string value, Language language)
        {
            this.Value = value?.Trim();
            this.Language = language;
            this.LanguageId = language?.Id ?? 0;
        }
    }
}