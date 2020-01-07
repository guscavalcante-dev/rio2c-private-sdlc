// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="PresentationFormatMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>PresentationFormatMap</summary>
    public class PresentationFormatMap : EntityTypeConfiguration<PresentationFormat>
    {
        /// <summary>Initializes a new instance of the <see cref="PresentationFormatMap"/> class.</summary>
        public PresentationFormatMap()
        {
            this.ToTable("PresentationFormats");

            this.Property(p => p.Name)
                .HasMaxLength(PresentationFormat.NameMaxLength);

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(t => t.EditionId);
        }
    }
}