// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-16-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-16-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationCompetitorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationCompetitorMap</summary>
    public class AttendeeInnovationOrganizationCompetitorMap : EntityTypeConfiguration<AttendeeInnovationOrganizationCompetitor>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationCompetitorMap"/> class.</summary>
        public AttendeeInnovationOrganizationCompetitorMap()
        {
            this.ToTable("AttendeeInnovationOrganizationCompetitors");

            // Relationships
            this.HasRequired(t => t.AttendeeInnovationOrganization)
                .WithMany(aio => aio.AttendeeInnovationOrganizationCompetitors)
                .HasForeignKey(d => d.AttendeeInnovationOrganizationId);
        }
    }
}