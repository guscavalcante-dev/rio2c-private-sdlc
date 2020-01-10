// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleTitle.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ConferenceParticipantRoleTitle</summary>
    public class ConferenceParticipantRoleTitle : Entity
    {
        public static readonly int ValueMinLength = 1;
        public static readonly int ValueMaxLength = 256;

        public int ConferenceParticipantRoleId { get; private set; }
        public int LanguageId { get; private set; }
        public string Value { get; private set; }

        public virtual ConferenceParticipantRole ConferenceParticipantRole { get; private set; }
        public virtual Language Language { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleTitle"/> class.</summary>
        /// <param name="value">The value.</param>
        /// <param name="language">The language.</param>
        /// <param name="userId">The user identifier.</param>
        public ConferenceParticipantRoleTitle(string value, Language language, int userId)
        {
            this.Value = value?.Trim();
            this.Language = language;
            this.LanguageId = language?.Id ?? 0;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleTitle"/> class.</summary>
        protected ConferenceParticipantRoleTitle()
        {
        }

        /// <summary>Updates the specified conference participant role title.</summary>
        /// <param name="conferenceParticipantRoleTitle">The conference participant role title.</param>
        public void Update(ConferenceParticipantRoleTitle conferenceParticipantRoleTitle)
        {
            this.Value = conferenceParticipantRoleTitle.Value?.Trim();
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = conferenceParticipantRoleTitle.UpdateUserId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
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
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Title, ValueMaxLength, ValueMinLength), new string[] { "Title" }));
            }
        }

        /// <summary>Validates the language.</summary>
        public void ValidateLanguage()
        {
            if (this.Language == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Language), new string[] { "Title" }));
            }
        }

        #endregion
    }
}