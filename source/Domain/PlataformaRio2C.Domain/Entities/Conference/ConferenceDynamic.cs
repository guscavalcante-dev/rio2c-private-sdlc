// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 05-09-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-09-2024
// ***********************************************************************
// <copyright file="ConferenceDynamic.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ConferenceDynamic</summary>
    public class ConferenceDynamic : Entity
    {
        public static readonly int ValueMinLength = 1;
        public static readonly int ValueMaxLength = 1000;

        public int ConferenceId { get; private set; }
        public int LanguageId { get; private set; }
        public string Value { get; private set; }

        public virtual Conference Conference { get; private set; }
        public virtual Language Language { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceDynamic"/> class.</summary>
        /// <param name="value">The value.</param>
        /// <param name="language">The language.</param>
        /// <param name="userId">The user identifier.</param>
        public ConferenceDynamic(string value, Language language, int userId)
        {
            this.Value = value?.Trim();
            this.Language = language;
            this.LanguageId = language?.Id ?? 0;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceDynamic"/> class.</summary>
        protected ConferenceDynamic()
        {
        }

        /// <summary>Updates the specified conference title.</summary>
        /// <param name="ConferenceDynamic">The conference title.</param>
        public void Update(ConferenceDynamic ConferenceDynamic)
        {
            this.Value = ConferenceDynamic.Value?.Trim();
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = ConferenceDynamic.UpdateUserId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

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
            //if (string.IsNullOrEmpty(this.Value?.Trim()))
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Descriptions), new string[] { "Descriptions" }));
            //}

            if (this.Value?.Trim().Length < ValueMinLength || this.Value?.Trim().Length > ValueMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Title, ValueMaxLength, ValueMinLength), new string[] { "Titles" }));
            }
        }

        /// <summary>Validates the language.</summary>
        public void ValidateLanguage()
        {
            if (this.Language == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Language), new string[] { "Titles" }));
            }
        }

        #endregion
    }
}