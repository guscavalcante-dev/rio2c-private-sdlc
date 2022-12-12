// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="SalesPlatform.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>SalesPlatform</summary>
    public class SalesPlatform : Entity
    {
        public static readonly int NameMaxLenght = 100;
        public static readonly int ApiKeyMaxLength = 200;
        public static readonly int ApiSecretMaxLength = 200;

        public string Name { get; private set; }
        public Guid WebhookSecurityKey { get; private set; }
        public string ApiKey { get; private set; }
        public string ApiSecret { get; private set; }
        public int MaxProcessingCount { get; private set; }
        public string SecurityStamp { get; private set; }

        public virtual ICollection<AttendeeSalesPlatform> AttendeeSalesPlatforms { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatform"/> class.</summary>
        private SalesPlatform()
        {
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            return ValidationResult.IsValid;
        }

        /// <summary>Validates the webhook security key.</summary>
        /// <param name="webhookSecurityKeyString">The webhook security key string.</param>
        /// <exception cref="DomainException">Invalid sales platform security key.</exception>
        public void ValidateWebhookSecurityKey(string webhookSecurityKeyString)
        {
            if (!string.Equals(this.WebhookSecurityKey.ToString(), webhookSecurityKeyString, StringComparison.OrdinalIgnoreCase))
            {
                throw new DomainException("Invalid sales platform security key.");
            }
        }

        /// <summary>
        /// Updates the last sales platform order date.
        /// </summary>
        /// <param name="attendeeSalesPlatformEventId">The attendee sales platform event identifier.</param>
        /// <param name="lastSalesPlatformOrderDate">The last sales platform order date.</param>
        public void UpdateLastSalesPlatformOrderDate(string attendeeSalesPlatformEventId, DateTime? lastSalesPlatformOrderDate)
        {
            var attendeeSalesPlatform = this.AttendeeSalesPlatforms.FirstOrDefault(asp => asp.SalesPlatformEventid == attendeeSalesPlatformEventId);
            attendeeSalesPlatform?.UpdateLastSalesPlatformOrderDate(lastSalesPlatformOrderDate);
        }
    }
}