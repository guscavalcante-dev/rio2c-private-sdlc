// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 08-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-17-2021
// ***********************************************************************
// <copyright file="CommissionAttendeeCollaboratorInterestMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationTrackMap</summary>
    public class CommissionAttendeeCollaboratorInterestMap : EntityTypeConfiguration<CommissionAttendeeCollaboratorInterest>
    {
        /// <summary>Initializes a new instance of the <see cref="CommissionAttendeeCollaboratorInterestMap"/> class.</summary>
        public CommissionAttendeeCollaboratorInterestMap()
        {
            this.ToTable("CommissionAttendeeCollaboratorInterests");

            // Relationships
            this.HasRequired(caci => caci.Interest)
                .WithMany(i => i.CommissionAttendeeCollaboratorInterests)
                .HasForeignKey(caci => caci.InterestId);

            this.HasRequired(caci => caci.AttendeeCollaborator)
                .WithMany(ac => ac.CommissionAttendeeCollaboratorInterests)
                .HasForeignKey(caci => caci.AttendeeCollaboratorId);
        }
    }
}