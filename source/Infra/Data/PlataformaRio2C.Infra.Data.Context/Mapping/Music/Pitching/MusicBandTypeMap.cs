// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="MusicBandTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicBandTypeMap</summary>
    public class MusicBandTypeMap : EntityTypeConfiguration<MusicBandType>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandTypeMap"/> class.</summary>
        public MusicBandTypeMap()
        {
            this.ToTable("MusicBandTypes");

            this.Property(t => t.Name)
                .HasMaxLength(MusicBandType.NameMaxLength);

            // Relationships
            //this.HasRequired(t => t.RelatedProjectType)
            //    .WithMany(e => e.OrganizationTypes)
            //    .HasForeignKey(d => d.RelatedProjectTypeId);

            //this.HasOptional(t => t.Image)
            //    .WithMany()
            //    .HasForeignKey(d => d.ImageId);
        }
    }
}