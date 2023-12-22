// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="InterestGroupMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>InterestGroupMap</summary>
    public class InterestGroupMap : EntityTypeConfiguration<InterestGroup>
    {
        /// <summary>Initializes a new instance of the <see cref="InterestGroupMap"/> class.</summary>
        public InterestGroupMap()
        {
            this.ToTable("InterestGroups");

            this.Property(p => p.Type)
                .HasMaxLength(InterestGroup.TypeMaxLength)
                .IsRequired(); 

            this.Property(t => t.Name)
               .HasMaxLength(InterestGroup.NameMaxLength)
               .IsRequired();

            //Relationships
            this.HasRequired(t => t.ProjectType)
                .WithMany(e => e.InterestGroups)
                .HasForeignKey(d => d.ProjectTypeId);
        }
    }
}