// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-27-2019
// ***********************************************************************
// <copyright file="CollaboratorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CollaboratorMap</summary>
    public class CollaboratorMap : EntityTypeConfiguration<Collaborator>
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorMap"/> class.</summary>
        public CollaboratorMap()
        {
            this.ToTable("Collaborators");

            this.Property(t => t.FirstName)
                .HasMaxLength(Collaborator.FirstNameMaxLength)
                .IsRequired();

            this.Property(t => t.LastNames)
                .HasMaxLength(Collaborator.LastNamesMaxLength);

            // Relationships
            this.HasRequired(t => t.User)
                .WithOptional(e => e.Collaborator);

            //this.HasOptional(t => t.Holding)
            //    .WithMany(e => e.Organizations)
            //    .HasForeignKey(d => d.HoldingId);

            this.HasRequired(t => t.Updater)
                .WithMany(e => e.UpdatedCollaborators)
                .HasForeignKey(d => d.UpdateUserId);

            this.HasOptional(t => t.Address)
                .WithMany(e => e.Collaborators)
                .HasForeignKey(d => d.AddressId);

            this.HasMany(t => t.AttendeeCollaborators)
                .WithRequired(e => e.Collaborator)
                .HasForeignKey(e => e.CollaboratorId);

            this.HasMany(t => t.JobTitles)
                .WithRequired(e => e.Collaborator)
                .HasForeignKey(e => e.CollaboratorId);

            this.HasMany(t => t.MiniBios)
                .WithRequired(e => e.Collaborator)
                .HasForeignKey(e => e.CollaboratorId);
        }
    }
}