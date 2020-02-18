// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferencePillarMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferencePillarMap</summary>
    public class ConferencePillarMap : EntityTypeConfiguration<ConferencePillar>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferencePillarMap"/> class.</summary>
        public ConferencePillarMap()
        {
            this.ToTable("ConferencePillars");

            //Property(u => u.Info)
            //   .HasMaxLength(Conference.InfoMaxLength);

            //Relationships 
            this.HasRequired(t => t.Conference)
              .WithMany(d => d.ConferencePillars)
              .HasForeignKey(t => t.ConferenceId);

            this.HasRequired(t => t.Pillar)
                .WithMany(d => d.ConferencePillars)
                .HasForeignKey(t => t.PillarId);
        }
    }
}