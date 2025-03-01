// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-21-2025
// ***********************************************************************
// <copyright file="CreatorProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    public class CreatorProject : Entity
    {
        #region Max Length configuration

        public static readonly int NameMaxLength = 500;
        public static readonly int DocumentMaxLength = 50;
        public static readonly int EmailMaxLength = 300;
        public static readonly int AgentNameMaxLength = 300;
        public static readonly int PhoneNumberMaxLength = 50;
        public static readonly int CurriculumMaxLength = 750;
        public static readonly int TitleMaxLength = 300;
        public static readonly int LoglineMaxLength = 300;
        public static readonly int DescriptionMaxLength = 5000;
        public static readonly int MotivationMaxLength = 3000;
        public static readonly int DiversityAndInclusionElementsMaxLength = 3000;
        public static readonly int ThemeRelevationMaxLength = 3000;
        public static readonly int MarketingStrategyMaxLength = 3000;
        public static readonly int SimilarProjectsMaxLength = 3000;
        public static readonly int OnlinePlatformsMaxLength = 300;
        public static readonly int AwardsMaxLength = 1024;
        public static readonly int PublicNoticeMaxLength = 1024;
        public static readonly int PreviousProjectsMaxLength = 1024;
        public static readonly int AssociatedInstitutionsMaxLength = 300;
        public static readonly int ArticleFileExtensionMaxLength = 10;
        public static readonly int ClippingFileExtensionMaxLength = 10;
        public static readonly int OtherFileExtensionMaxLength = 10;
        public static readonly int OtherFileDescriptionMaxLength = 400;
        public static readonly int LinksMaxLength = 700;

        #endregion

        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string AgentName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Curriculum { get; private set; }
        public string Title { get; private set; }
        public string Logline { get; private set; }
        public string Description { get; private set; }
        public string MotivationToDevelop { get; private set; }
        public string MotivationToTransform { get; private set; }
        public string DiversityAndInclusionElements { get; private set; }
        public string ThemeRelevation { get; private set; }
        public string MarketingStrategy { get; private set; }
        public string SimilarAudiovisualProjects { get; private set; }
        public string OnlinePlatformsWhereProjectIsAvailable { get; private set; }
        public string OnlinePlatformsAudienceReach { get; private set; }
        public string ProjectAwards { get; private set; }
        public string ProjectPublicNotice { get; private set; }
        public string PreviouslyDevelopedProjects { get; private set; }
        public string AssociatedInstitutions { get; private set; }
        public DateTimeOffset? ArticleFileUploadDate { get; private set; }
        public string ArticleFileExtension { get; private set; }
        public DateTimeOffset? ClippingFileUploadDate { get; private set; }
        public string ClippingFileExtension { get; private set; }
        public DateTimeOffset? OtherFileUploadDate { get; private set; }
        public string OtherFileExtension { get; private set; }
        public string OtherFileDescription { get; private set; }
        public string Links { get; private set; }
        public DateTimeOffset TermsAcceptanceDate { get; private set; }

        public virtual ICollection<AttendeeCreatorProject> AttendeeCreatorProjects { get; private set; }
        public virtual ICollection<CreatorProjectInterest> CreatorProjectInterests { get; private set; }

        #region Evaluation

        /// <summary>
        /// Evaluates the specified edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="grade">The grade.</param>
        public void Evaluate(Edition edition, User evaluatorUser, decimal grade)
        {
            var attendeeCreatorProject = this.GetAttendeeCreatorProjectByEditionId(edition.Id);
            attendeeCreatorProject?.Evaluate(evaluatorUser, grade);
        }

        #endregion

        #region Attendee Creator Projects

        /// <summary>
        /// Gets the attendee creator project by edition identifier.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public AttendeeCreatorProject GetAttendeeCreatorProjectByEditionId(int editionId)
        {
            return this.AttendeeCreatorProjects?.FirstOrDefault(acp => acp.Edition.Id == editionId);
        }

        #endregion

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateDocument();
            this.ValidateEmail();
            this.ValidateAgentName();
            this.ValidatePhoneNumber();
            this.ValidateCurriculum();
            this.ValidateTitle();
            this.ValidateLogline();
            this.ValidateDescription();
            this.ValidateMotivationToDevelop();
            this.ValidateMotivationToTransform();
            this.ValidateDiversityAndInclusionElements();
            this.ValidateThemeRelevation();
            this.ValidateMarketingStrategy();
            this.ValidateSimilarAudiovisualProjects();
            this.ValidateOnlinePlatformsWhereProjectIsAvailable();
            this.ValidateOnlinePlatformsAudienceReach();
            this.ValidateProjectAwards();
            this.ValidateProjectPublicNotice();
            this.ValidatePreviouslyDevelopedProjects();
            this.ValidateAssociatedInstitutions();
            this.ValidateArticleFileExtension();
            this.ValidateClippingFileExtension();
            this.ValidateOtherFileExtension();
            this.ValidateOtherFileDescription();
            this.ValidateLinks();
            this.ValidateCreatorProjectInterests();

            return this.ValidationResult.IsValid;
        }

        private void ValidateName()
        {
            if (!string.IsNullOrEmpty(this.Name?.Trim()) && this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Name), NameMaxLength, 1), new string[] { nameof(Name) }));
            }
        }

        private void ValidateDocument()
        {
            if (!string.IsNullOrEmpty(this.Document?.Trim()) && this.Document?.Trim().Length > DocumentMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Document), DocumentMaxLength, 1), new string[] { nameof(Document) }));
            }
        }

        private void ValidateEmail()
        {
            if (!string.IsNullOrEmpty(this.Email?.Trim()) && this.Email?.Trim().Length > EmailMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Email), EmailMaxLength, 1), new string[] { nameof(Email) }));
            }
        }

        private void ValidateAgentName()
        {
            if (!string.IsNullOrEmpty(this.AgentName?.Trim()) && this.AgentName?.Trim().Length > AgentNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(AgentName), AgentNameMaxLength, 1), new string[] { nameof(AgentName) }));
            }
        }

        private void ValidatePhoneNumber()
        {
            if (!string.IsNullOrEmpty(this.PhoneNumber?.Trim()) && this.PhoneNumber?.Trim().Length > PhoneNumberMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(PhoneNumber), PhoneNumberMaxLength, 1), new string[] { nameof(PhoneNumber) }));
            }
        }

        private void ValidateCurriculum()
        {
            if (!string.IsNullOrEmpty(this.Curriculum?.Trim()) && this.Curriculum?.Trim().Length > CurriculumMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Curriculum), CurriculumMaxLength, 1), new string[] { nameof(Curriculum) }));
            }
        }

        private void ValidateTitle()
        {
            if (!string.IsNullOrEmpty(this.Title?.Trim()) && this.Title?.Trim().Length > TitleMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Title), TitleMaxLength, 1), new string[] { nameof(Title) }));
            }
        }

        private void ValidateLogline()
        {
            if (!string.IsNullOrEmpty(this.Logline?.Trim()) && this.Logline?.Trim().Length > LoglineMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Logline), LoglineMaxLength, 1), new string[] { nameof(Logline) }));
            }
        }

        private void ValidateDescription()
        {
            if (!string.IsNullOrEmpty(this.Description?.Trim()) && this.Description?.Trim().Length > DescriptionMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Description), DescriptionMaxLength, 1), new string[] { nameof(Description) }));
            }
        }

        private void ValidateMotivationToDevelop()
        {
            if (!string.IsNullOrEmpty(this.MotivationToDevelop?.Trim()) && this.MotivationToDevelop?.Trim().Length > MotivationMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(MotivationToDevelop), MotivationMaxLength, 1), new string[] { nameof(MotivationToDevelop) }));
            }
        }

        private void ValidateMotivationToTransform()
        {
            if (!string.IsNullOrEmpty(this.MotivationToTransform?.Trim()) && this.MotivationToTransform?.Trim().Length > MotivationMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(MotivationToTransform), MotivationMaxLength, 1), new string[] { nameof(MotivationToTransform) }));
            }
        }

        private void ValidateDiversityAndInclusionElements()
        {
            if (!string.IsNullOrEmpty(this.DiversityAndInclusionElements?.Trim()) && this.DiversityAndInclusionElements?.Trim().Length > DiversityAndInclusionElementsMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(DiversityAndInclusionElements), DiversityAndInclusionElementsMaxLength, 1), new string[] { nameof(DiversityAndInclusionElements) }));
            }
        }

        private void ValidateThemeRelevation()
        {
            if (!string.IsNullOrEmpty(this.ThemeRelevation?.Trim()) && this.ThemeRelevation?.Trim().Length > ThemeRelevationMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(ThemeRelevation), ThemeRelevationMaxLength, 1), new string[] { nameof(ThemeRelevation) }));
            }
        }

        private void ValidateMarketingStrategy()
        {
            if (!string.IsNullOrEmpty(this.MarketingStrategy?.Trim()) && this.MarketingStrategy?.Trim().Length > MarketingStrategyMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(MarketingStrategy), MarketingStrategyMaxLength, 1), new string[] { nameof(MarketingStrategy) }));
            }
        }

        private void ValidateSimilarAudiovisualProjects()
        {
            if (!string.IsNullOrEmpty(this.SimilarAudiovisualProjects?.Trim()) && this.SimilarAudiovisualProjects?.Trim().Length > SimilarProjectsMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(SimilarAudiovisualProjects), SimilarProjectsMaxLength, 1), new string[] { nameof(SimilarAudiovisualProjects) }));
            }
        }

        private void ValidateOnlinePlatformsWhereProjectIsAvailable()
        {
            if (!string.IsNullOrEmpty(this.OnlinePlatformsWhereProjectIsAvailable?.Trim()) && this.OnlinePlatformsWhereProjectIsAvailable?.Trim().Length > OnlinePlatformsMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(OnlinePlatformsWhereProjectIsAvailable), OnlinePlatformsMaxLength, 1), new string[] { nameof(OnlinePlatformsWhereProjectIsAvailable) }));
            }
        }

        private void ValidateOnlinePlatformsAudienceReach()
        {
            if (!string.IsNullOrEmpty(this.OnlinePlatformsAudienceReach?.Trim()) && this.OnlinePlatformsAudienceReach?.Trim().Length > OnlinePlatformsMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(OnlinePlatformsAudienceReach), OnlinePlatformsMaxLength, 1), new string[] { nameof(OnlinePlatformsAudienceReach) }));
            }
        }

        private void ValidateProjectAwards()
        {
            if (!string.IsNullOrEmpty(this.ProjectAwards?.Trim()) && this.ProjectAwards?.Trim().Length > AwardsMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(ProjectAwards), AwardsMaxLength, 1), new string[] { nameof(ProjectAwards) }));
            }
        }

        private void ValidateProjectPublicNotice()
        {
            if (!string.IsNullOrEmpty(this.ProjectPublicNotice?.Trim()) && this.ProjectPublicNotice?.Trim().Length > PublicNoticeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(ProjectPublicNotice), PublicNoticeMaxLength, 1), new string[] { nameof(ProjectPublicNotice) }));
            }
        }

        private void ValidatePreviouslyDevelopedProjects()
        {
            if (!string.IsNullOrEmpty(this.PreviouslyDevelopedProjects?.Trim()) && this.PreviouslyDevelopedProjects?.Trim().Length > PreviousProjectsMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(PreviouslyDevelopedProjects), PreviousProjectsMaxLength, 1), new string[] { nameof(PreviouslyDevelopedProjects) }));
            }
        }

        private void ValidateAssociatedInstitutions()
        {
            if (!string.IsNullOrEmpty(this.AssociatedInstitutions?.Trim()) && this.AssociatedInstitutions?.Trim().Length > AssociatedInstitutionsMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(AssociatedInstitutions), AssociatedInstitutionsMaxLength, 1), new string[] { nameof(AssociatedInstitutions) }));
            }
        }

        private void ValidateArticleFileExtension()
        {
            if (!string.IsNullOrEmpty(this.ArticleFileExtension?.Trim()) && this.ArticleFileExtension?.Trim().Length > ArticleFileExtensionMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(ArticleFileExtension), ArticleFileExtensionMaxLength, 1), new string[] { nameof(ArticleFileExtension) }));
            }
        }

        private void ValidateClippingFileExtension()
        {
            if (!string.IsNullOrEmpty(this.ClippingFileExtension?.Trim()) && this.ClippingFileExtension?.Trim().Length > ClippingFileExtensionMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(ClippingFileExtension), ClippingFileExtensionMaxLength, 1), new string[] { nameof(ClippingFileExtension) }));
            }
        }

        private void ValidateOtherFileExtension()
        {
            if (!string.IsNullOrEmpty(this.OtherFileExtension?.Trim()) && this.OtherFileExtension?.Trim().Length > OtherFileExtensionMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(OtherFileExtension), OtherFileExtensionMaxLength, 1), new string[] { nameof(OtherFileExtension) }));
            }
        }

        private void ValidateOtherFileDescription()
        {
            if (!string.IsNullOrEmpty(this.OtherFileDescription?.Trim()) && this.OtherFileDescription?.Trim().Length > OtherFileDescriptionMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(OtherFileDescription), OtherFileDescriptionMaxLength, 1), new string[] { nameof(OtherFileDescription) }));
            }
        }

        private void ValidateLinks()
        {
            if (!string.IsNullOrEmpty(this.Links?.Trim()) && this.Links?.Trim().Length > LinksMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Links), LinksMaxLength, 1), new string[] { nameof(Links) }));
            }
        }
        
        private void ValidateCreatorProjectInterests()
        {
            if (this.CreatorProjectInterests?.Any() != true)
            {
                return;
            }

            foreach (var creatorProjectInterest in this.CreatorProjectInterests.Where(aiof => !aiof.IsValid()))
            {
                this.ValidationResult.Add(creatorProjectInterest.ValidationResult);
            }
        }

        #endregion
    }

}
