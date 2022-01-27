// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-16-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-16-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationTrackMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationTrackMap</summary>
    public class AttendeeInnovationOrganizationTrackMap : EntityTypeConfiguration<AttendeeInnovationOrganizationTrack>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrackMap"/> class.</summary>
        public AttendeeInnovationOrganizationTrackMap()
        {
            this.ToTable("AttendeeInnovationOrganizationTracks");

            // Relationships
            this.HasRequired(t => t.AttendeeInnovationOrganization)
                .WithMany(aio => aio.AttendeeInnovationOrganizationTracks)
                .HasForeignKey(d => d.AttendeeInnovationOrganizationId);

            this.HasRequired(t => t.InnovationOrganizationTrackOption)
                .WithMany()
                .HasForeignKey(d => d.InnovationOrganizationTrackOptionId);
            this.Property(x => x.InnovationOrganizationTrackOptionId).HasColumnName("InnovationOrganizationTrackOptionId");
        }
    }
}