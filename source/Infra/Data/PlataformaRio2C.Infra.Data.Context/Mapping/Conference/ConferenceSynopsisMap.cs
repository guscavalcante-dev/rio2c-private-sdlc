// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="ProjectMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceSynopsisMap</summary>
    public class ConferenceSynopsisMap : EntityTypeConfiguration<ConferenceSynopsis>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceSynopsisMap"/> class.</summary>
        public ConferenceSynopsisMap()
        {
            this.ToTable("ConferenceSynopsis");

            this.Property(p => p.Value)
                .HasMaxLength(ConferenceSynopsis.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Conference)
                .WithMany(t => t.ConferenceSynopses)
                .HasForeignKey(d => d.ConferenceId);
        }
    }
}