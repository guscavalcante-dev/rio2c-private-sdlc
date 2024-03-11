// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-26-2024
// ***********************************************************************
// <copyright file="CreatorProjectMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CreatorProjectMap</summary>
    public class CreatorProjectMap : EntityTypeConfiguration<CreatorProject>
    {
        public CreatorProjectMap()
        {
            this.ToTable("CreatorProjects");

            this.Property(t => t.Name)
                .HasMaxLength(CreatorProject.NameMaxLength);

            this.Property(t => t.Document)
                .HasMaxLength(CreatorProject.DocumentMaxLength);

            this.Property(t => t.AgentName)
                .HasMaxLength(CreatorProject.AgentNameMaxLength);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(CreatorProject.PhoneNumberMaxLength);

            this.Property(t => t.Curriculum)
                .HasMaxLength(CreatorProject.CurriculumMaxLength);

            this.Property(t => t.Title)
                .HasMaxLength(CreatorProject.TitleMaxLength);

            this.Property(t => t.Logline)
                .HasMaxLength(CreatorProject.LoglineMaxLength);

            this.Property(t => t.Description)
                .HasMaxLength(CreatorProject.DescriptionMaxLength);

            this.Property(t => t.MotivationToDevelop)
                .HasMaxLength(CreatorProject.MotivationMaxLength);

            this.Property(t => t.MotivationToTransform)
                .HasMaxLength(CreatorProject.MotivationMaxLength);

            this.Property(t => t.DiversityAndInclusionElements)
                .HasMaxLength(CreatorProject.DiversityAndInclusionElementsMaxLength);

            this.Property(t => t.ThemeRelevation)
                .HasMaxLength(CreatorProject.ThemeRelevationMaxLength);

            this.Property(t => t.MarketingStrategy)
                .HasMaxLength(CreatorProject.MarketingStrategyMaxLength);

            this.Property(t => t.SimilarAudiovisualProjects)
                .HasMaxLength(CreatorProject.SimilarProjectsMaxLength);

            this.Property(t => t.OnlinePlatformsWhereProjectIsAvailable)
                .HasMaxLength(CreatorProject.OnlinePlatformsMaxLength);

            this.Property(t => t.OnlinePlatformsAudienceReach)
                .HasMaxLength(CreatorProject.OnlinePlatformsMaxLength);

            this.Property(t => t.ProjectAwards)
                .HasMaxLength(CreatorProject.AwardsMaxLength);

            this.Property(t => t.ProjectPublicNotice)
                .HasMaxLength(CreatorProject.PublicNoticeMaxLength);

            this.Property(t => t.PreviouslyDevelopedProjects)
                .HasMaxLength(CreatorProject.PreviousProjectsMaxLength);

            this.Property(t => t.AssociatedInstitutions)
                .HasMaxLength(CreatorProject.AssociatedInstitutionsMaxLength);

            this.Property(t => t.ArticleFileExtension)
                .HasMaxLength(CreatorProject.ArticleFileExtensionMaxLength);

            this.Property(t => t.ClippingFileExtension)
                .HasMaxLength(CreatorProject.ClippingFileExtensionMaxLength);

            this.Property(t => t.OtherFileExtension)
                .HasMaxLength(CreatorProject.OtherFileExtensionMaxLength);

            this.Property(t => t.OtherFileDescription)
               .HasMaxLength(CreatorProject.OtherFileDescriptionMaxLength);

            this.Property(t => t.Links)
                .HasMaxLength(CreatorProject.LinksMaxLength);
        }
    }
}