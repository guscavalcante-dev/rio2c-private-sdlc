// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="InterestMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>InterestMap</summary>
    public class InterestMap : EntityTypeConfiguration<Interest>
    {
        /// <summary>Initializes a new instance of the <see cref="InterestMap"/> class.</summary>
        public InterestMap()
        {
            this.ToTable("Interests");

            this.Ignore(p => p.InterestGroupUid);

            this.Property(t => t.Name)
              .HasMaxLength(Interest.NameMaxLength)
              .IsRequired();

            //Relationships
            this.HasRequired(t => t.InterestGroup)
                .WithMany()
                .HasForeignKey(d => d.InterestGroupId);
        }
    }
}