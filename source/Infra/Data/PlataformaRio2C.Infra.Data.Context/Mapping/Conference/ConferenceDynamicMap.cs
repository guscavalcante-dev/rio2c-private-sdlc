// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 05-09-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-09-2024
// ***********************************************************************
// <copyright file="ConferenceDynamicMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceDynamicMap</summary>
    public class ConferenceDynamicMap : EntityTypeConfiguration<ConferenceDynamic>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceDynamicMap"/> class.</summary>
        public ConferenceDynamicMap()
        {
            this.ToTable("ConferenceDynamics");

            this.Property(p => p.Value)
                .HasMaxLength(ConferenceDynamic.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Conference)
                .WithMany(t => t.ConferenceDynamics)
                .HasForeignKey(d => d.ConferenceId);

            //this.HasRequired(t => t.Language)
            //    .WithMany()
            //    .HasForeignKey(d => d.LanguageId);
        }
    }
}