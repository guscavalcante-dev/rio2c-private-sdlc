// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Franco
// Created          : 02-04-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-04-2022
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectCollaboratorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCartoonProjectCollaboratorMap</summary>
    public class AttendeeCartoonProjectCollaboratorMap : EntityTypeConfiguration<AttendeeCartoonProjectCollaborator>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProjectCollaboratorMap"/> class.</summary>
        public AttendeeCartoonProjectCollaboratorMap()
        {
            this.ToTable("AttendeeCartoonProjectCollaborators");
            //this.Property(x => x.AttendeeCartoonProjectId)
            //    .HasColumnName("AttendeeCartoonProjectId");

            // Relationships
            this.HasRequired(t => t.AttendeeCartoonProject)
                .WithMany(e => e.AttendeeCartoonProjectCollaborators)
                .HasForeignKey(d => d.AttendeeCartoonProjectId);

            this.HasRequired(t => t.AttendeeCollaborator)
                .WithMany(e => e.AttendeeCartoonProjectCollaborators)
                .HasForeignKey(d => d.AttendeeCollaboratorId);
        }
    }
}