// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="HoldingDescriptionMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>HoldingDescriptionMap</summary>
    public class HoldingDescriptionMap : EntityTypeConfiguration<HoldingDescription>
    {
        /// <summary>Initializes a new instance of the <see cref="HoldingDescriptionMap"/> class.</summary>
        public HoldingDescriptionMap()
        {
            this.ToTable("HoldingDescriptions");

            this.Ignore(p => p.LanguageCode);

            this.Property(p => p.Value)
                .HasMaxLength(HoldingDescription.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Holding)
                .WithMany(e => e.Descriptions)
                .HasForeignKey(d => d.HoldingId);

            this.HasRequired(t => t.Language)
               .WithMany()
               .HasForeignKey(d => d.LanguageId);
        }
    }
}