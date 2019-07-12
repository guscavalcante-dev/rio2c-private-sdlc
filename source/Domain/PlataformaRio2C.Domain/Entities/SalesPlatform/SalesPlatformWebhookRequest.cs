// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-12-2019
// ***********************************************************************
// <copyright file="SalesPlatformWebhookRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class SalesPlatformWebhookRequest : Entity
    {
        public static readonly int EndpointMaxLength = 250;
        public static readonly int HeaderMaxLength = 1000;
        public static readonly int PayloadtMaxLength = 5000;
        public static readonly int IpAddressMaxLength = 38;

        public int SalesPlatformId { get; private set; }
        public string Endpoint { get; private set; }
        public string Header { get; private set; }
        public string Payload { get; private set; }
        public string IpAddress { get; private set; }
        public bool IsProcessed { get; private set; }
        public bool IsProcessing { get; private set; }
        public int ProcessingCount { get; private set; }
        public DateTime? LastProcessingDate { get; private set; }
        public string ProcessingErrorCode { get; private set; }
        public int? ManualProcessingUserId { get; private set; }
        //public Guid SecurityStamp { get; private set; }

        public virtual SalesPlatform SalesPlatform { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformWebhookRequest"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestUid">The sales platform webhook request uid.</param>
        /// <param name="salesPlatform">The sales platform.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="header">The header.</param>
        /// <param name="payload">The payload.</param>
        /// <param name="ipAddress">The ip address.</param>
        public SalesPlatformWebhookRequest(
            Guid salesPlatformWebhookRequestUid,
            SalesPlatform salesPlatform,
            string endpoint,
            string header,
            string payload,
            string ipAddress)
        {
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
            //this.SecurityStamp = Guid.NewGuid();
        }

        /// <summary>Prevents a default instance of the <see cref="SalesPlatformWebhookRequest"/> class from being created.</summary>
        private SalesPlatformWebhookRequest()
        {
        }

        //public void SetPlayer(Player entity)
        //{
        //    Player = entity;
        //    if (entity != null)
        //    {
        //        PlayerId = entity.Id;
        //    }
        //}    

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new SalesPlatformWebhookRequestIsConsistent().Valid(this));

            return ValidationResult.IsValid;
        }
    }
}
