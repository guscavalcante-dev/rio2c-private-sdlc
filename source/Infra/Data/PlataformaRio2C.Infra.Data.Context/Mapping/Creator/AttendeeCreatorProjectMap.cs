// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-26-2024
// ***********************************************************************
// <copyright file="AttendeeCreatorProjectMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCreatorProjectMap</summary>
    public class AttendeeCreatorProjectMap : EntityTypeConfiguration<AttendeeCreatorProject>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCreatorProjectMap"/> class.</summary>
        public AttendeeCreatorProjectMap()
        {
            this.ToTable("AttendeeCreatorProjects");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.CreatorProject)
                   .WithMany(cp => cp.AttendeeCreatorProjects)
                   .HasForeignKey(d => d.CreatorProjectId);
        }
    }
}