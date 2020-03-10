// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="LogisticAirfareMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>LogisticAirfareMap</summary>
    public class LogisticAirfareMap : EntityTypeConfiguration<LogisticAirfare>
    {
        /// <summary>Initializes a new instance of the <see cref="LogisticAirfareMap"/> class.</summary>
        public LogisticAirfareMap()
        {
            this.ToTable("LogisticAirfares");

            this.Property(p => p.From)
                .HasMaxLength(LogisticAirfare.FromMaxLength);

            this.Property(p => p.To)
                .HasMaxLength(LogisticAirfare.ToMaxLength);

            this.Property(p => p.TicketNumber)
                .HasMaxLength(LogisticAirfare.TicketNumberMaxLength);

            this.Property(p => p.AdditionalInfo)
                .HasMaxLength(LogisticAirfare.AdditionalInfoMaxLength);

            // Relationships
            this.HasRequired(e => e.Logistic)
                .WithMany(e => e.LogisticAirfares)
                .HasForeignKey(e => e.LogisticId);
        }
    }
}