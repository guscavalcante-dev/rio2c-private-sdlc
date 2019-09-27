// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCollaboratorTypeMap</summary>
    public class AttendeeCollaboratorTypeMap : EntityTypeConfiguration<AttendeeCollaboratorType>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTypeMap"/> class.</summary>
        public AttendeeCollaboratorTypeMap()
        {
            this.ToTable("AttendeeCollaboratorTypes");

            // Relationships
            this.HasRequired(t => t.AttendeeCollaborator)
                .WithMany(e => e.AttendeeCollaboratorTypes)
                .HasForeignKey(d => d.AttendeeCollaboratorId);

            this.HasRequired(t => t.CollaboratorType)
                .WithMany(e => e.AttendeeCollaboratorTypes)
                .HasForeignKey(d => d.CollaboratorTypeId);
        }
    }
}