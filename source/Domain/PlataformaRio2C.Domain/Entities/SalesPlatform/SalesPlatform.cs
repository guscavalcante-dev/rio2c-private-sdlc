// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="SalesPlatform.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>SalesPlatform</summary>
    public class SalesPlatform : Entity
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public Guid WebhookSecurityKey { get; private set; }
        public string ApiKey { get; private set; }
        public string ApiSecret { get; private set; }
        public int MaxProcessingCount { get; private set; }
        public int CreationUserId { get; private set; }
        public int UpdateUserId { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public string SecurityStamp { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatform"/> class.</summary>
        private SalesPlatform()
        {
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new SalesPlatformIsConsistent().Valid(this));

            return ValidationResult.IsValid;
        }

        /// <summary>Determines whether [is valid webhook security key] [the specified webhook security key string].</summary>
        /// <param name="webhookSecurityKeyString">The webhook security key string.</param>
        /// <returns>
        ///   <c>true</c> if [is valid webhook security key] [the specified webhook security key string]; otherwise, <c>false</c>.</returns>
        public bool IsValidWebhookSecurityKey(string webhookSecurityKeyString)
        {
            return string.Equals(this.WebhookSecurityKey.ToString(), webhookSecurityKeyString, StringComparison.OrdinalIgnoreCase);
        }
    }
}
