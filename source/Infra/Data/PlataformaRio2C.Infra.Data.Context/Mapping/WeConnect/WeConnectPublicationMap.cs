// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 08-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
// ***********************************************************************
// <copyright file="WeConnectPublicationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>WeConnectPublicationMap</summary>
    public class WeConnectPublicationMap : EntityTypeConfiguration<WeConnectPublication>
    {
        /// <summary>Initializes a new instance of the <see cref="WeConnectPublicationMap"/> class.</summary>
        public WeConnectPublicationMap()
        {
            this.ToTable("WeConnectPublications");

            this.Property(u => u.SocialMediaPlatformPublicationId)
                .HasMaxLength(WeConnectPublication.SocialMediaPlatformPublicationIdMaxLenght);

            this.Property(u => u.PublicationText)
                .HasMaxLength(WeConnectPublication.PublicationTextMaxLenght);

            // Relationships
            this.HasOptional(wcp => wcp.SocialMediaPlatform)
                .WithMany(smp => smp.WeConnectPublications)
                .HasForeignKey(wcp => wcp.SocialMediaPlatformId);
        }
    }
}