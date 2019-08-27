// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="CollaboratorMiniBioMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CollaboratorMiniBioMap</summary>
    public class CollaboratorMiniBioMap : EntityTypeConfiguration<CollaboratorMiniBio>
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorMiniBioMap"/> class.</summary>
        public CollaboratorMiniBioMap()
        {
            this.ToTable("CollaboratorMiniBios");

            this.Property(p => p.Value)
                .HasMaxLength(CollaboratorMiniBio.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Collaborator)
                .WithMany(e => e.MiniBios)
                .HasForeignKey(d => d.CollaboratorId);

            this.HasRequired(t => t.Language)
                .WithMany()
                .HasForeignKey(d => d.LanguageId);
        }
    }
}