// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="SentEmail.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>SentEmail</summary>
    public class SentEmail : AggregateRoot
    {
        public static readonly int EmailTypeMinLength = 2;
        public static readonly int EmailTypeMaxLength = 81;

        public int RecipientUserId { get; private set; }
        public int? EditionId { get; private set; }
        public string EmailType { get; private set; }
        public DateTimeOffset EmailSendDate { get; private set; }
        public DateTimeOffset? EmailReadDate { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SentEmail"/> class.</summary>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="emailType">Type of the email.</param>
        public SentEmail(Guid sentEmailUid, int recipientUserId, int? editionId, string emailType)
        {
            //this.Uid = sentEmailUid;
            this.RecipientUserId = recipientUserId;
            this.EditionId = editionId;
            this.EmailType = emailType?.Trim();
            this.EmailSendDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="SentEmail"/> class.</summary>
        protected SentEmail()
        {
        }

        #region Validation

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateEmailType();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the type of the email.</summary>
        public void ValidateEmailType()
        {
            if (string.IsNullOrEmpty(this.EmailType?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Email Type"), new string[] { "EmailType" }));
            }

            if (this.EmailType?.Trim().Length < EmailTypeMinLength || this.EmailType?.Trim().Length > EmailTypeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Email Type", EmailTypeMaxLength, EmailTypeMinLength), new string[] { "EmailType" }));
            }
        }

        #endregion
    }
}