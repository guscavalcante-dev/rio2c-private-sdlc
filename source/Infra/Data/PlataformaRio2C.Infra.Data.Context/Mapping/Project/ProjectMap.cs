// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-01-2019
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
    /// <summary>ProjectMap</summary>
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectMap"/> class.</summary>
        public ProjectMap()
        {
            this.ToTable("Projects");

            //Property(u => u.Pitching)
            //    .IsRequired();

            //Relationships
            this.HasRequired(t => t.SellerAttendeeOrganization)
                .WithMany(e => e.Projects)
                .HasForeignKey(d => d.SellerAttendeeOrganizationId);

            this.HasMany(c => c.Titles)
                  .WithRequired(t => t.Project)
                  .HasForeignKey(t => t.ProjectId);
        }
    }
}