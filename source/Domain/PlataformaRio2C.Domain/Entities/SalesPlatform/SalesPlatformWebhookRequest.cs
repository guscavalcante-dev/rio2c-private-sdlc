// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="SalesPlatformWebhookRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>SalesPlatformWebhookRequest</summary>
    public class SalesPlatformWebhookRequest : Entity
    {
        public static readonly int EndpointMaxLength = 250;
        public static readonly int HeaderMaxLength = 1000;
        public static readonly int PayloadtMaxLength = 5000;
        public static readonly int IpAddressMaxLength = 38;
        public static readonly int ProcessingErrorCodeLength = 10;
        public static readonly int ProcessingErrorMessageLength = 250;

        public int SalesPlatformId { get; private set; }
        public string Endpoint { get; private set; }
        public string Header { get; private set; }
        public string Payload { get; private set; }
        public string IpAddress { get; private set; }
        public bool IsProcessed { get; private set; }
        public bool IsProcessing { get; private set; }
        public int ProcessingCount { get; private set; }
        public DateTime? LastProcessingDate { get; private set; }
        public DateTime? NextProcessingDate { get; private set; }
        public string ProcessingErrorCode { get; private set; }
        public string ProcessingErrorMessage { get; private set; }
        public int? ManualProcessingUserId { get; private set; }
        public string SecurityStamp { get; private set; }

        public virtual SalesPlatform SalesPlatform { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformWebhookRequest"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestUid">The sales platform webhook request uid.</param>
        /// <param name="salesPlatform">The sales platform.</param>
        /// <param name="webhookSecurityKey">The webhook security key.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="header">The header.</param>
        /// <param name="payload">The payload.</param>
        /// <param name="ipAddress">The ip address.</param>
        public SalesPlatformWebhookRequest(
            Guid salesPlatformWebhookRequestUid,
            SalesPlatform salesPlatform,
            string webhookSecurityKey,
            string endpoint,
            string header,
            string payload,
            string ipAddress)
        {
            if (salesPlatform == null || salesPlatform.IsDeleted)
            {
                throw new DomainException("The sales platform was not found");
            }

            salesPlatform.ValidateWebhookSecurityKey(webhookSecurityKey);

            this.Uid = salesPlatformWebhookRequestUid;
            this.SalesPlatformId = salesPlatform.Id;
            this.SalesPlatform = salesPlatform;
            this.Endpoint = endpoint;
            this.Header = header;
            this.Payload = payload;
            this.IpAddress = ipAddress;
            this.IsProcessed = false;
            this.IsProcessing = false;
            this.ProcessingCount = 0;
            this.NextProcessingDate = this.GetNextProcessingDate();
            this.CreateDate = DateTime.UtcNow;
            this.SecurityStamp = Guid.NewGuid().ToString();
        }

        /// <summary>Prevents a default instance of the <see cref="SalesPlatformWebhookRequest"/> class from being created.</summary>
        private SalesPlatformWebhookRequest()
        {
        }

        /// <summary>Processes this instance.</summary>
        public void Process()
        {
            if (this.IsProcessing)
            {
                throw new DomainException("Sales platform webhook request is already being processed.");
            }

            this.IsProcessing = true;
        }

        /// <summary>Concludes the specified security stamp.</summary>
        /// <param name="securityStamp">The security stamp.</param>
        public void Conclude(/*string securityStamp*/)
        {
            //this.ValidateSecurityStamp(securityStamp);
            this.ValidateProcessing();

            if (this.IsProcessed)
            {
                throw  new DomainException("The sales platform webhook request is already processed.");
            }

            this.IsProcessing = false;
            this.IsProcessed = true;
            this.ProcessingCount += 1;
            this.LastProcessingDate = DateTime.UtcNow;
            this.NextProcessingDate = null;
            this.ProcessingErrorCode = null;
            this.ProcessingErrorMessage = null;
            this.SecurityStamp = Guid.NewGuid().ToString();
        }

        /// <summary>Postpones the specified error code.</summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="securityStamp">The security stamp.</param>
        public void Postpone(string errorCode, string errorMessage/*, string securityStamp*/)
        {
            //this.ValidateSecurityStamp(securityStamp);
            this.ValidateProcessing();

            this.IsProcessing = false;
            this.IsProcessed = false;
            this.ProcessingCount += 1;
            this.LastProcessingDate = DateTime.UtcNow;
            this.NextProcessingDate = this.GetNextProcessingDate();
            this.ProcessingErrorCode = errorCode;
            this.ProcessingErrorMessage = errorMessage;
            this.SecurityStamp = Guid.NewGuid().ToString();
        }

        /// <summary>Aborts the specified error code.</summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public void Abort(string errorCode, string errorMessage/*, string securityStamp*/)
        {
            //this.ValidateSecurityStamp(securityStamp);
            this.ValidateProcessing();

            this.IsProcessing = false;
            this.IsProcessed = true;
            this.ProcessingCount = 99;
            this.LastProcessingDate = DateTime.UtcNow;
            this.NextProcessingDate = null;
            this.ProcessingErrorCode = errorCode;
            this.ProcessingErrorMessage = errorMessage;
            this.SecurityStamp = Guid.NewGuid().ToString();
        }

        /// <summary>Gets the next processing date.</summary>
        /// <returns></returns>
        private DateTime? GetNextProcessingDate()
        {
            var visibilityTimeouts = new[] { 0, 60, 120, 180, 300, 480, 780, 1260, 2040, 3300, 5340, 8640, 13980, 22620, 36600, 42000, 84000 };

            if (this.ProcessingCount > 15)
            {
                this.ProcessingCount = 15;
                return null;
            }

            return (this.LastProcessingDate ?? DateTime.UtcNow).AddSeconds(visibilityTimeouts[this.ProcessingCount]);
        }

        #region Validations

        /// <summary>Validates the security stamp.</summary>
        /// <param name="securityStamp">The security stamp.</param>
        private void ValidateSecurityStamp(string securityStamp)
        {
            if (!string.Equals(this.SecurityStamp, securityStamp, StringComparison.OrdinalIgnoreCase))
            {
                throw new DomainException("Invalid security stamp.");

            }
        }

        /// <summary>Validates the processing.</summary>
        private void ValidateProcessing()
        {
            if (!this.IsProcessing)
            {
                throw new DomainException("The sales platform webhook request is not being processed.");
            }
        }

        #endregion

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            return ValidationResult.IsValid;
        }
    }
}
