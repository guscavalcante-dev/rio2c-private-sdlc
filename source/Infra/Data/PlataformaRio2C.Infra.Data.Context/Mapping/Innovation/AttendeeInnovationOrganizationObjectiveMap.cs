// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-16-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-16-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationObjectiveMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationObjectiveMap</summary>
    public class AttendeeInnovationOrganizationObjectiveMap : EntityTypeConfiguration<AttendeeInnovationOrganizationObjective>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationObjectiveMap"/> class.</summary>
        public AttendeeInnovationOrganizationObjectiveMap()
        {
            this.ToTable("AttendeeInnovationOrganizationObjectives");

            // Relationships
            this.HasRequired(t => t.AttendeeInnovationOrganization)
                .WithMany(aio => aio.AttendeeInnovationOrganizationObjectives)
                .HasForeignKey(d => d.AttendeeInnovationOrganiaztionId);

            this.HasRequired(t => t.InnovationOrganizationObjectivesOption)
                .WithMany()
                .HasForeignKey(d => d.InnovationOrganizationObjectiveOptionId);
        }
    }
}