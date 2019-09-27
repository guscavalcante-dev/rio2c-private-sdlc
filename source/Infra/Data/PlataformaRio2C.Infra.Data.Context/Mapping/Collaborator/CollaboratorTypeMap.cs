// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="CollaboratorTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CollaboratorTypeMap</summary>
    public class CollaboratorTypeMap : EntityTypeConfiguration<CollaboratorType>
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorTypeMap"/> class.</summary>
        public CollaboratorTypeMap()
        {
            this.ToTable("CollaboratorTypes");

            this.Property(t => t.Name)
                .HasMaxLength(CollaboratorType.NameMaxLength);

            // Relationships
            this.HasRequired(t => t.Role)
                .WithMany(e => e.CollaboratorTypes)
                .HasForeignKey(e => e.RoleId);

            this.HasMany(t => t.AttendeeCollaboratorTypes)
                .WithRequired(e => e.CollaboratorType)
                .HasForeignKey(e => e.CollaboratorTypeId);

            this.HasMany(t => t.AttendeeSalesPlatformTicketTypes)
                .WithRequired(e => e.CollaboratorType)
                .HasForeignKey(e => e.CollaboratorTypeId);
        }
    }
}