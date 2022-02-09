// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCartoonProjectMap</summary>
    public class AttendeeCartoonProjectMap : EntityTypeConfiguration<AttendeeCartoonProject>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProjectMap"/> class.</summary>
        public AttendeeCartoonProjectMap()
        {
            this.ToTable("AttendeeCartoonProjects");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.CartoonProject)
                  .WithMany(t => t.AttendeeCartoonProjects)
                  .HasForeignKey(d => d.CartoonProjectId);
        }
    }
}