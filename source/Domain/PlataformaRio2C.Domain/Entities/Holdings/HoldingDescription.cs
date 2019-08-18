// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-18-2019
// ***********************************************************************
// <copyright file="HoldingDescription.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;

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

        public virtual Language Language { get; private set; }
        public virtual Holding Holding { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingDescription"/> class.</summary>
        /// <param name="value">The value.</param>
        /// <param name="language">The language.</param>
        /// <param name="userId">The user identifier.</param>
        public HoldingDescription(string value, Language language, int userId)
        {
            this.Value = value?.Trim();
            this.Language = language;
            this.LanguageId = language?.Id ?? 0;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="HoldingDescription"/> class.</summary>
        protected HoldingDescription()
        {
        }

        /// <summary>Updates the specified description.</summary>
        /// <param name="description">The description.</param>
        public void Update(HoldingDescription description)
        {
            this.Value = description.Value.Trim();
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = description.UpdateUserId;
        }

        ///// <summary>Sets the language.</summary>
        ///// <param name="language">The language.</param>
        //public void SetLanguage(Language language)
        //{
        //    this.Language = language;
        //    this.LanguageId = language?.Id ?? 0;
        //    this.LanguageCode = language?.Code;
        //}

        // Remove below

        //public HoldingDescription(string value, string languageCode)
        //{
        //    this.Value = value?.Trim();
        //    this.LanguageCode = languageCode;
        //}

        ///// <summary>Sets the holding.</summary>
        ///// <param name="holding">The holding.</param>
        //public void SetHolding(Holding holding)
        //{
        //    this.Holding = holding;
        //    this.HoldingId = holding.Id;
        //}

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateValue();
            this.ValidateLanguage();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the value.</summary>
        public void ValidateValue()
        {
            // TODO: use resources on validation errrors
            if (string.IsNullOrEmpty(this.Value?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError("A descrição é obrigatório.", new string[] { "Value" }));
            }

            if (this.Value?.Trim().Length < ValueMinLength || this.Value?.Trim().Length > ValueMaxLength)
            {
                this.ValidationResult.Add(new ValidationError($"A descrição deve ter entre '{ValueMinLength}' e '{ValueMaxLength}' caracteres.", new string[] { "Name" }));
            }
        }

        /// <summary>Validates the language.</summary>
        public void ValidateLanguage()
        {
            // TODO: use resources on validation errrors
            if (this.Language == null)
            {
                this.ValidationResult.Add(new ValidationError("O idioma da descrição é obrigatório.", new string[] { "Value" }));
            }
        }

        #endregion
    }
}
