// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="HoldingDescription.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>HoldingDescription</summary>
    public class HoldingDescription : Entity
    {
        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 8000;

        public int HoldingId { get; private set; }
        public int LanguageId { get; private set; }
        public string Value { get; private set; }

        public virtual string LanguageCode { get; private set; }

        public virtual Language Language { get; private set; }
        public virtual Holding Holding { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingDescription"/> class.</summary>
        protected HoldingDescription()
        {
        }

        public HoldingDescription(string value, Language language, int userId)
        {
            this.Value = value;
            this.SetLanguage(language);
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        public HoldingDescription(string value, string languageCode)
        {
            this.Value = value;
            this.LanguageCode = languageCode;
        }

        /// <summary>Sets the language.</summary>
        /// <param name="language">The language.</param>
        public void SetLanguage(Language language)
        {
            this.Language = language;
            this.LanguageId = language.Id;
            this.LanguageCode = language.Code;
        }

        /// <summary>Sets the holding.</summary>
        /// <param name="holding">The holding.</param>
        public void SetHolding(Holding holding)
        {
            this.Holding = holding;
            this.HoldingId = holding.Id;
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}
