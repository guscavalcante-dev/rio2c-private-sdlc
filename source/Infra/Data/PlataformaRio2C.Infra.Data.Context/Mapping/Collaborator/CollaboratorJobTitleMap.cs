// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="CollaboratorJobTitleMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CollaboratorJobTitleMap</summary>
    public class CollaboratorJobTitleMap : EntityTypeConfiguration<CollaboratorJobTitle>
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorJobTitleMap"/> class.</summary>
        public CollaboratorJobTitleMap()
        {
            this.ToTable("CollaboratorJobTitles");

            this.Property(p => p.Value)
                .HasMaxLength(CollaboratorJobTitle.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Collaborator)
                .WithMany(e => e.JobTitles)
                .HasForeignKey(d => d.CollaboratorId);

            this.HasRequired(t => t.Language)
                .WithMany()
                .HasForeignKey(d => d.LanguageId);
        }
    }
}