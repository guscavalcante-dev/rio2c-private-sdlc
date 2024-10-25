// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Gilson Oliveira
// Created          : 10-23-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-23-2024
// ***********************************************************************
// <copyright file="ProjectModalityMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectModalityMap</summary>
    public class ProjectModalityMap : EntityTypeConfiguration<ProjectModality>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectModalityMap"/> class.</summary>
        public ProjectModalityMap()
        {
            this.ToTable("ProjectModalities");

            this.Property(t => t.Name)
                .HasMaxLength(ProjectModality.NameMaxLength)
                .IsRequired();
        }
    }
}