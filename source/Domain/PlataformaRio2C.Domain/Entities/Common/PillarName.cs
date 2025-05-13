// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="TrackName.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>TrackName</summary>
    public class PillarName : Entity
    {
        public static readonly int ValueMinLength = 1;
        public static readonly int ValueMaxLength = 256;

        public int TrackId { get; private set; }
        public int LanguageId { get; private set; }
        public string Value { get; private set; }

        public virtual Track Track { get; private set; }
        public virtual Language Language { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="TrackName"/> class.</summary>
        /// <param name="value">The value.</param>
        /// <param name="language">The language.</param>
        /// <param name="userId">The user identifier.</param>
        public PillarName(string value, Language language, int userId)
        {
            this.Value = value?.Trim();
            this.Language = language;
            this.LanguageId = language?.Id ?? 0;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="TrackName"/> class.</summary>
        protected PillarName()
        {
        }

        /// <summary>Updates the specified track name.</summary>
        /// <param name="trackName">Name of the track.</param>
        public void Update(PillarName trackName)
        {
            this.Value = trackName.Value?.Trim();
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = trackName.UpdateUserId;
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
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, ValueMaxLength, ValueMinLength), new string[] { "Names" }));
            }
        }

        /// <summary>Validates the language.</summary>
        public void ValidateLanguage()
        {
            if (this.Language == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Language), new string[] { "Names" }));
            }
        }

        #endregion
    }
}