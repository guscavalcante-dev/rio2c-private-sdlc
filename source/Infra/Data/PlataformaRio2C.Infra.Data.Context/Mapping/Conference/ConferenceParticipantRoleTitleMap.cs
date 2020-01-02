// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-02-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleTitleMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceParticipantRoleTitleMap</summary>
    public class ConferenceParticipantRoleTitleMap : EntityTypeConfiguration<ConferenceParticipantRoleTitle>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleTitleMap"/> class.</summary>
        public ConferenceParticipantRoleTitleMap()
        {
            this.ToTable("ConferenceParticipantRoleTitles");

            this.Property(t => t.Value)
                .HasMaxLength(ConferenceParticipantRoleTitle.ValueMaxLength);

            //Relationships 
            this.HasRequired(t => t.ConferenceParticipantRole)
              .WithMany(d => d.ConferenceParticipantRoleTitles)
              .HasForeignKey(t => t.ConferenceParticipantRoleId);
        }
    }
}