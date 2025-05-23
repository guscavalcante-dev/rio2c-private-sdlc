﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-25-2024
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

            this.Property(t => t.Document)
                .HasMaxLength(Collaborator.DocumentMaxLength);

            this.Property(t => t.PublicEmail)
                .HasMaxLength(Collaborator.PublicEmailMaxLength);

            this.Property(t => t.Website)
                .HasMaxLength(Collaborator.WebsiteMaxLength);

            this.Property(t => t.Linkedin)
                .HasMaxLength(Collaborator.LinkedinMaxLength);

            this.Property(t => t.Twitter)
                .HasMaxLength(Collaborator.TwitterMaxLength);

            this.Property(t => t.Instagram)
                .HasMaxLength(Collaborator.InstagramMaxLength);

            this.Property(t => t.Youtube)
                .HasMaxLength(Collaborator.YoutubeMaxLength);

            this.Property(t => t.SpecialNeedsDescription)
                .HasMaxLength(Collaborator.SpecialNeedsDescriptionMaxLength);

            this.Property(t => t.CollaboratorGenderAdditionalInfo)
                .HasMaxLength(Collaborator.CollaboratorGenderAdditionalInfoMaxLength);

            this.Property(t => t.CollaboratorRoleAdditionalInfo)
                .HasMaxLength(Collaborator.CollaboratorRoleAdditionalInfoMaxLength);

            this.Property(t => t.CollaboratorIndustryAdditionalInfo)
                .HasMaxLength(Collaborator.CollaboratorIndustryAdditionalInfoMaxLength);

            // Relationships
            this.HasRequired(t => t.User)
                .WithOptional(e => e.Collaborator);

            this.HasRequired(t => t.Updater)
                .WithMany(e => e.UpdatedCollaborators)
                .HasForeignKey(d => d.UpdateUserId);

            this.HasRequired(t => t.Creator)
               .WithMany(e => e.CreatedCollaborators)
               .HasForeignKey(d => d.CreateUserId);

            this.HasOptional(t => t.Address)
                .WithMany(e => e.Collaborators)
                .HasForeignKey(d => d.AddressId);
        }
    }
}