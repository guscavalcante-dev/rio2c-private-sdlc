// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-12-2019
// ***********************************************************************
// <copyright file="StateMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>StateMap</summary>
    class StateMap : EntityTypeConfiguration<State>
    {
        /// <summary>Initializes a new instance of the <see cref="StateMap"/> class.</summary>
        public StateMap()
        {
            this.ToTable("States");

            this.Property(p => p.Name)
                .HasMaxLength(State.NameMaxLength);

            this.Property(p => p.Code)
                .HasMaxLength(State.CodeMaxLength);

            // Relationships
            this.HasRequired(t => t.Country)
                .WithMany()
                .HasForeignKey(d => d.CountryId);

            this.HasMany(t => t.Cities)
                .WithRequired(e => e.State)
                .HasForeignKey(e => e.StateId);

            this.HasMany(t => t.Addresses)
                .WithOptional(e => e.State)
                .HasForeignKey(e => e.StateId);
        }
    }
}
