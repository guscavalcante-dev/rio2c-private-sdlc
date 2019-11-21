// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-21-2019
// ***********************************************************************
// <copyright file="ProjectInterestMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectInterestMap</summary>
    public class ProjectInterestMap : EntityTypeConfiguration<ProjectInterest>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectInterestMap"/> class.</summary>
        public ProjectInterestMap()
        {
            this.ToTable("ProjectInterests");

            this.Property(t => t.AdditionalInfo)
                .HasMaxLength(OrganizationActivity.AdditionalInfoMaxLength);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(p => p.Interests)
                .HasForeignKey(t => t.ProjectId);

            this.HasRequired(t => t.Interest)
                .WithMany()
                .HasForeignKey(d => d.InterestId);
        }
    }
}