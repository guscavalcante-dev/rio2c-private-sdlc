// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="PillarMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>PillarMap</summary>
    public class PillarMap : EntityTypeConfiguration<Pillar>
    {
        /// <summary>Initializes a new instance of the <see cref="PillarMap"/> class.</summary>
        public PillarMap()
        {
            this.ToTable("Pillars");

            this.Property(p => p.Name)
                .HasMaxLength(Pillar.NameMaxLength);

            this.Property(p => p.Color)
                .HasMaxLength(Pillar.ColorMaxLength);

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(t => t.EditionId);
        }
    }
}