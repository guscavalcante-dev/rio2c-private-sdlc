// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 08-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-17-2021
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInterestMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCollaboratorInterestMap</summary>
    public class AttendeeCollaboratorInterestMap : EntityTypeConfiguration<AttendeeCollaboratorInterest>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInterestMap"/> class.</summary>
        public AttendeeCollaboratorInterestMap()
        {
            this.ToTable("AttendeeCollaboratorInterests");

            // Relationships
            this.HasRequired(caci => caci.Interest)
                .WithMany(i => i.AttendeeCollaboratorInterests)
                .HasForeignKey(caci => caci.InterestId);

            this.HasRequired(caci => caci.AttendeeCollaborator)
                .WithMany(ac => ac.AttendeeCollaboratorInterests)
                .HasForeignKey(caci => caci.AttendeeCollaboratorId);
        }
    }
}