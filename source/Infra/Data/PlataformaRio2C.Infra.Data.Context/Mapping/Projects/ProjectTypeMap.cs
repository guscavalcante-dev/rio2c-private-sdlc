// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-08-2019
// ***********************************************************************
// <copyright file="ProjectTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectTypeMap</summary>
    public class ProjectTypeMap : EntityTypeConfiguration<ProjectType>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectTypeMap"/> class.</summary>
        public ProjectTypeMap()
        {
            this.ToTable("ProjectTypes");

            this.Property(t => t.Name)
                .HasMaxLength(ProjectType.NameMaxLength)
                .IsRequired();

            // Relationships
            //this.HasRequired(t => t.Holding)
            //    .WithMany(e => e.Organizations)
            //    .HasForeignKey(d => d.HoldingId);

            //this.HasOptional(t => t.Image)
            //    .WithMany()
            //    .HasForeignKey(d => d.ImageId);
        }
    }
}