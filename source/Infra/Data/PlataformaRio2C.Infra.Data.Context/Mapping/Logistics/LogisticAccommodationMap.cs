// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="LogisticAccommodationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>LogisticAccommodationMap</summary>
    public class LogisticAccommodationMap : EntityTypeConfiguration<LogisticAccommodation>
    {
        /// <summary>Initializes a new instance of the <see cref="LogisticAccommodationMap"/> class.</summary>
        public LogisticAccommodationMap()
        {
            this.ToTable("LogisticAccommodations");

            this.Property(p => p.AdditionalInfo)
                .HasMaxLength(LogisticAccommodation.AdditionalInfoMaxLength);

            // Relationships

            this.HasRequired(t => t.Logistic)
                .WithMany(e => e.LogisticAccommodations)
                .HasForeignKey(d => d.LogisticId);
        }
    }
}