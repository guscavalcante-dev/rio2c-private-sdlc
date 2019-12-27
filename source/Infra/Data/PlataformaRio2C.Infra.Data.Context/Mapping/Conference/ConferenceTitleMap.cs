// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="ConferenceTitleMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceTitleMap</summary>
    public class ConferenceTitleMap : EntityTypeConfiguration<ConferenceTitle>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceTitleMap"/> class.</summary>
        public ConferenceTitleMap()
        {
            this.ToTable("ConferenceTitles");

            this.Property(p => p.Value)
                .HasMaxLength(ConferenceTitle.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Conference)
                .WithMany(t => t.ConferenceTitles)
                .HasForeignKey(d => d.ConferenceId);
        }
    }
}