// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-30-2019
// ***********************************************************************
// <copyright file="SalesPlatform.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>SalesPlatform</summary>
    public class SalesPlatform : Entity
    {
        public string Name { get; private set; }
        public Guid WebhookSecurityKey { get; private set; }
        public string ApiKey { get; private set; }
        public string ApiSecret { get; private set; }
        public int MaxProcessingCount { get; private set; }
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

        public void ValidateWebhookSecurityKey(string webhookSecurityKeyString)
        {
            if (!string.Equals(this.WebhookSecurityKey.ToString(), webhookSecurityKeyString, StringComparison.OrdinalIgnoreCase))
            {
                throw new DomainException("Invalid sales platform security key.");
            }
        }
    }
}