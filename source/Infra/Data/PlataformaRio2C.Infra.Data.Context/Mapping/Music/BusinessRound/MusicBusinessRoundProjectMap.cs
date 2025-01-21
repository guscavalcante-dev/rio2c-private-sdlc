// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Daniel Giese Rodrigues
// Created          : 01-20-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
// ***********************************************************************
// <copyright file="ProjectInterestMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectMap</summary>
    public class MusicBusinessRoundProjectMap : EntityTypeConfiguration<MusicBusinessRoundProject>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundProjectMap"/> class.</summary>
        public MusicBusinessRoundProjectMap()
        {
            this.ToTable("MusicBusinessRoundProjects");
            this.HasKey(t => t.Id);

            //Mapping props
            this.Property(t => t.PlayerCategoriesThatHaveOrHadContract)
                .HasMaxLength(MusicBusinessRoundProject.PlayerCategoriesThatHaveOrHadContractMaxLength);
            this.Property(t => t.AttachmentUrl)
                .HasMaxLength(MusicBusinessRoundProject.AttachmentUrlMaxLength);

            // Player caterogories
            this.HasMany(t => t.PlayerCategories)
                .WithRequired(pc => pc.MusicBusinessRoundProject)
                .HasForeignKey(pc => pc.MusicBusinessRoundProjectId);

            //Relationships
            this.HasRequired(t => t.SellerAttendeeCollaborator)
                .WithMany(e => e.MusicBusinessRoundProjects)
                .HasForeignKey(d => d.SellerAttendeeCollaboratorId);
        }
    }
}