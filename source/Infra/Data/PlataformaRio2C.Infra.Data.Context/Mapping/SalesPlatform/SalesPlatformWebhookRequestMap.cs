// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="SalesPlatformWebhookRequestMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>SalesPlatformWebhookRequestMap</summary>
    public class SalesPlatformWebhookRequestMap : EntityTypeConfiguration<SalesPlatformWebhookRequest>
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformWebhookRequestMap"/> class.</summary>
        public SalesPlatformWebhookRequestMap()
        {
            this.ToTable("SalesPlatformWebhookRequest");

            //this.Property(t => t.Date)
            //    .IsRequired();

            this.Property(p => p.Endpoint)
                .HasMaxLength(SalesPlatformWebhookRequest.EndpointMaxLength);

            this.Property(p => p.Header)
                .HasMaxLength(SalesPlatformWebhookRequest.HeaderMaxLength);

            this.Property(p => p.Payload)
                .HasMaxLength(SalesPlatformWebhookRequest.PayloadtMaxLength);

            this.Property(p => p.IpAddress)
                .HasMaxLength(SalesPlatformWebhookRequest.IpAddressMaxLength);

            //this.Property(m => m.Payload)
            //    .HasColumnType("nvarchar(MAX)");

            this.HasRequired(m => m.SalesPlatform)
                .WithMany()
                .HasForeignKey(d => d.SalesPlatformId);
        }
    }
}