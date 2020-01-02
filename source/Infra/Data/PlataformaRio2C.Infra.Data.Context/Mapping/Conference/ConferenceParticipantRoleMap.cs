// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceParticipantRoleMap</summary>
    public class ConferenceParticipantRoleMap : EntityTypeConfiguration<ConferenceParticipantRole>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleMap"/> class.</summary>
        public ConferenceParticipantRoleMap()
        {
            this.ToTable("ConferenceParticipantRoles");

            this.Property(t => t.Name)
                .HasMaxLength(ConferenceParticipantRole.NameMaxLength);

            //Relationships 
            //this.HasRequired(t => t.Conference)
            //  .WithMany(d => d.ConferenceParticipants)
            //  .HasForeignKey(t => t.ConferenceId);
        }
    }
}