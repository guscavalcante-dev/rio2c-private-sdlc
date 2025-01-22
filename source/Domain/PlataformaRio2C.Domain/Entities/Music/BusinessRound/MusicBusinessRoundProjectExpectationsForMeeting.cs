// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-18-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectExpectationsForMeeting.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicBusinessRoundProjectExpectationsForMeeting : Entity
    {
        public static readonly int ValueMaxLength = 256;

        public int MusicBusinessRoundProjectId { get; private set; }
        public int LanguageId { get; private set; }
        public string Value { get; private set; }

        public virtual MusicBusinessRoundProject MusicBusinessRoundProject { get; private set; }
        public virtual Language Language { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectExpectationsForMeeting"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="language">The language.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBusinessRoundProjectExpectationsForMeeting(string value, Language language, int userId)
        {
            this.Value = value?.Trim();
            this.Language = language;
            this.LanguageId = language?.Id ?? 0;

            base.SetCreateDate(userId);
        }

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            //TODO: Implement validations here

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}