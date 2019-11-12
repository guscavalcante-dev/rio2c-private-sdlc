// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-12-2019
// ***********************************************************************
// <copyright file="Project.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Project</summary>
    public class Project : Entity
    {
        public static readonly int SendPlayerCountMax = 5;

        public int ProjectTypeId { get; private set; }
        public int SellerAttendeeOrganizationId { get; private set; }
        public int? NumberOfEpisodes { get; private set; }
        public string EachEpisodePlayingTime { get; private set; }
        public int? ValuePerEpisode { get; private set; }
        public int? TotalValueOfProject { get; private set; }
        public int? ValueAlreadyRaised { get; private set; }
        public int? ValueStillNeeded { get; private set; }
        public bool IsPitching { get; private set; }

        public virtual ProjectType ProjectType { get; private set; }
        public virtual AttendeeOrganization SellerAttendeeOrganization { get; private set; }

        public virtual ICollection<ProjectTitle> Titles { get; private set; }
        public virtual ICollection<ProjectLogLine> LogLines { get; private set; }
        public virtual ICollection<ProjectSummary> Summaries { get; private set; }
        public virtual ICollection<ProjectProductionPlan> ProductionPlans { get; private set; }
        public virtual ICollection<ProjectAdditionalInformation> AdditionalInformations { get; private set; }
        public virtual ICollection<ProjectImageLink> ImageLinks { get; private set; }
        public virtual ICollection<ProjectTeaserLink> TeaserLinks { get; private set; }
        public virtual ICollection<ProjectInterest> Interests { get; private set; }
        public virtual ICollection<ProjectTargetAudience> TargetAudiences { get; private set; }
        public virtual ICollection<ProjectBuyerEvaluation> BuyerEvaluations { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Project"/> class.</summary>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="sellerAttendeeOrganization">The seller attendee organization.</param>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="valuePerEpisode">The value per episode.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="valueAlreadyRaised">The value already raised.</param>
        /// <param name="valueStillNeeded">The value still needed.</param>
        /// <param name="isPitching">if set to <c>true</c> [is pitching].</param>
        /// <param name="titles">The titles.</param>
        /// <param name="logLines">The log lines.</param>
        /// <param name="summaries">The summaries.</param>
        /// <param name="productionPlans">The production plans.</param>
        /// <param name="additionalInformations">The additional informations.</param>
        /// <param name="interests">The interests.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="imageLink">The image link.</param>
        /// <param name="teaserLink">The teaser link.</param>
        /// <param name="userId">The user identifier.</param>
        public Project(
            ProjectType projectType,
            AttendeeOrganization sellerAttendeeOrganization,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            int? valuePerEpisode,
            int? totalValueOfProject,
            int? valueAlreadyRaised,
            int? valueStillNeeded,
            bool isPitching,
            List<ProjectTitle> titles,
            List<ProjectLogLine> logLines,
            List<ProjectSummary> summaries,
            List<ProjectProductionPlan> productionPlans,
            List<ProjectAdditionalInformation> additionalInformations,
            List<Interest> interests,
            List<TargetAudience> targetAudiences,
            string imageLink,
            string teaserLink,
            int userId)
        {
            this.ProjectTypeId = projectType?.Id ?? 0;
            this.ProjectType = projectType;
            this.SetSellerAttendeeOrganization(projectType, sellerAttendeeOrganization, userId);
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime;
            this.ValuePerEpisode = valuePerEpisode;
            this.TotalValueOfProject = totalValueOfProject;
            this.ValueAlreadyRaised = valueAlreadyRaised;
            this.ValueStillNeeded = valueStillNeeded;
            this.IsPitching = isPitching;

            this.SynchronizeTitles(titles, userId);
            this.SynchronizeLogLines(logLines, userId);
            this.SynchronizeSummaries(summaries, userId);
            this.SynchronizeProductionPlans(productionPlans, userId);
            this.SynchronizeAdditionalInformations(additionalInformations, userId);
            this.SynchronizeInterests(interests, userId);
            this.SynchronizeTargetAudiences(targetAudiences, userId);
            this.SynchronizeImageLinks(imageLink, userId);
            this.SynchronizeTeaserLinks(teaserLink, userId);

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.Now;
        }

        /// <summary>Initializes a new instance of the <see cref="Project"/> class.</summary>
        protected Project()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="valuePerEpisode">The value per episode.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="valueAlreadyRaised">The value already raised.</param>
        /// <param name="valueStillNeeded">The value still needed.</param>
        /// <param name="isPitching">if set to <c>true</c> [is pitching].</param>
        /// <param name="titles">The titles.</param>
        /// <param name="logLines">The log lines.</param>
        /// <param name="summaries">The summaries.</param>
        /// <param name="productionPlans">The production plans.</param>
        /// <param name="additionalInformations">The additional informations.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            int? valuePerEpisode,
            int? totalValueOfProject,
            int? valueAlreadyRaised,
            int? valueStillNeeded,
            bool isPitching,
            List<ProjectTitle> titles,
            List<ProjectLogLine> logLines,
            List<ProjectSummary> summaries,
            List<ProjectProductionPlan> productionPlans,
            List<ProjectAdditionalInformation> additionalInformations,
            int userId)
        {
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime;
            this.ValuePerEpisode = valuePerEpisode;
            this.TotalValueOfProject = totalValueOfProject;
            this.ValueAlreadyRaised = valueAlreadyRaised;
            this.ValueStillNeeded = valueStillNeeded;
            this.IsPitching = isPitching;

            this.SynchronizeTitles(titles, userId);
            this.SynchronizeLogLines(logLines, userId);
            this.SynchronizeSummaries(summaries, userId);
            this.SynchronizeProductionPlans(productionPlans, userId);
            this.SynchronizeAdditionalInformations(additionalInformations, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        #region Seller Attendee Organization

        /// <summary>Sets the seller attendee organization.</summary>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="sellerAttendeeOrganization">The seller attendee organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void SetSellerAttendeeOrganization(ProjectType projectType, AttendeeOrganization sellerAttendeeOrganization, int userId)
        {
            this.SellerAttendeeOrganizationId = sellerAttendeeOrganization?.Id ?? 0;
            this.SellerAttendeeOrganization = sellerAttendeeOrganization;

            var organizationType = projectType?.OrganizationTypes?.FirstOrDefault(ot => ot.IsSeller);
            this.SellerAttendeeOrganization?.SynchronizeAttendeeOrganizationTypes(organizationType, userId);
        }

        #endregion

        #region Buyer Evaluations

        /// <summary>Creates the buyer evaluation.</summary>
        /// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void CreateBuyerEvaluation(
            AttendeeOrganization buyerAttendeeOrganization,
            int userId)
        {
            if (this.BuyerEvaluations == null)
            {
                this.BuyerEvaluations = new List<ProjectBuyerEvaluation>();
            }

            var buyerEvaluation = this.GetBuyerEvaluationByAttendeeOrganizationUid(buyerAttendeeOrganization?.Uid ?? Guid.Empty);
            if (buyerEvaluation == null)
            {
                this.BuyerEvaluations.Add(new ProjectBuyerEvaluation(this, buyerAttendeeOrganization, userId));
            }
            else
            {
                buyerEvaluation.Update(userId);
            }

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Deletes the buyer evaluation.</summary>
        /// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteBuyerEvaluation(
            AttendeeOrganization buyerAttendeeOrganization,
            int userId)
        {
            var buyerEvaluation = this.GetBuyerEvaluationByAttendeeOrganizationUid(buyerAttendeeOrganization?.Uid ?? Guid.Empty);
            buyerEvaluation?.Delete(userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Gets the buyer evaluation by attendee organization uid.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns></returns>
        private ProjectBuyerEvaluation GetBuyerEvaluationByAttendeeOrganizationUid(Guid attendeeOrganizationUid)
        {
            return this.BuyerEvaluations?.FirstOrDefault(be => be.BuyerAttendeeOrganization.Uid == attendeeOrganizationUid);
        }

        #endregion

        #region Titles

        /// <summary>Synchronizes the titles.</summary>
        /// <param name="titles">The titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeTitles(List<ProjectTitle> titles, int userId)
        {
            if (this.Titles == null)
            {
                this.Titles = new List<ProjectTitle>();
            }

            this.DeleteTitles(titles, userId);

            if (titles?.Any() != true)
            {
                return;
            }

            foreach (var title in titles)
            {
                var titleDb = this.Titles.FirstOrDefault(d => d.Language.Code == title.Language.Code);
                if (titleDb != null)
                {
                    titleDb.Update(title);
                }
                else
                {
                    this.CreateTitle(title);
                }
            }
        }

        /// <summary>Deletes the titles.</summary>
        /// <param name="newTitles">The new titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteTitles(List<ProjectTitle> newTitles, int userId)
        {
            var titlesToDelete = this.Titles.Where(db => newTitles?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var titleToDelete in titlesToDelete)
            {
                titleToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the title.</summary>
        /// <param name="title">The title.</param>
        private void CreateTitle(ProjectTitle title)
        {
            this.Titles.Add(title);
        }

        #endregion

        #region LogLines

        /// <summary>Synchronizes the log lines.</summary>
        /// <param name="logLines">The log lines.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeLogLines(List<ProjectLogLine> logLines, int userId)
        {
            if (this.LogLines == null)
            {
                this.LogLines = new List<ProjectLogLine>();
            }

            this.DeleteLogLines(logLines, userId);

            if (logLines?.Any() != true)
            {
                return;
            }

            foreach (var logLine in logLines)
            {
                var logLineDb = this.LogLines.FirstOrDefault(d => d.Language.Code == logLine.Language.Code);
                if (logLineDb != null)
                {
                    logLineDb.Update(logLine);
                }
                else
                {
                    this.CreateLogLines(logLine);
                }
            }
        }

        /// <summary>Deletes the log lines.</summary>
        /// <param name="newLogLines">The new log lines.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteLogLines(List<ProjectLogLine> newLogLines, int userId)
        {
            var logLinesToDelete = this.LogLines.Where(db => newLogLines?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var logLineToDelete in logLinesToDelete)
            {
                logLineToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the log lines.</summary>
        /// <param name="logLine">The log line.</param>
        private void CreateLogLines(ProjectLogLine logLine)
        {
            this.LogLines.Add(logLine);
        }

        #endregion

        #region Summaries

        /// <summary>Synchronizes the summaries.</summary>
        /// <param name="summaries">The summaries.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeSummaries(List<ProjectSummary> summaries, int userId)
        {
            if (this.Summaries == null)
            {
                this.Summaries = new List<ProjectSummary>();
            }

            this.DeleteSummaries(summaries, userId);

            if (summaries?.Any() != true)
            {
                return;
            }

            foreach (var summary in summaries)
            {
                var summaryDb = this.Summaries.FirstOrDefault(d => d.Language.Code == summary.Language.Code);
                if (summaryDb != null)
                {
                    summaryDb.Update(summary);
                }
                else
                {
                    this.CreateSummary(summary);
                }
            }
        }

        /// <summary>Deletes the summaries.</summary>
        /// <param name="newSummaries">The new summaries.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteSummaries(List<ProjectSummary> newSummaries, int userId)
        {
            var summariesToDelete = this.Summaries.Where(db => newSummaries?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var summaryToDelete in summariesToDelete)
            {
                summaryToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the summary.</summary>
        /// <param name="sumary">The sumary.</param>
        private void CreateSummary(ProjectSummary sumary)
        {
            this.Summaries.Add(sumary);
        }

        #endregion

        #region Production Plans

        /// <summary>Synchronizes the production plans.</summary>
        /// <param name="productionPlans">The production plans.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProductionPlans(List<ProjectProductionPlan> productionPlans, int userId)
        {
            if (this.ProductionPlans == null)
            {
                this.ProductionPlans = new List<ProjectProductionPlan>();
            }

            this.DeleteProductionPlans(productionPlans, userId);

            if (productionPlans?.Any() != true)
            {
                return;
            }

            foreach (var productionPlan in productionPlans)
            {
                var productionPlanDb = this.ProductionPlans.FirstOrDefault(d => d.Language.Code == productionPlan.Language.Code);
                if (productionPlanDb != null)
                {
                    productionPlanDb.Update(productionPlan);
                }
                else
                {
                    this.CreateProductionPlan(productionPlan);
                }
            }
        }

        /// <summary>Deletes the production plans.</summary>
        /// <param name="newProductionPlans">The new production plans.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteProductionPlans(List<ProjectProductionPlan> newProductionPlans, int userId)
        {
            var productionPlansToDelete = this.ProductionPlans.Where(db => newProductionPlans?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var productionPlanToDelete in productionPlansToDelete)
            {
                productionPlanToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the production plan.</summary>
        /// <param name="productionPlan">The production plan.</param>
        private void CreateProductionPlan(ProjectProductionPlan productionPlan)
        {
            this.ProductionPlans.Add(productionPlan);
        }

        #endregion

        #region Additional Informations

        /// <summary>Synchronizes the additional informations.</summary>
        /// <param name="additionalInformations">The additional informations.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAdditionalInformations(List<ProjectAdditionalInformation> additionalInformations, int userId)
        {
            if (this.AdditionalInformations == null)
            {
                this.AdditionalInformations = new List<ProjectAdditionalInformation>();
            }

            this.DeleteAdditionalInformations(additionalInformations, userId);

            if (additionalInformations?.Any() != true)
            {
                return;
            }

            foreach (var additionalInformation in additionalInformations)
            {
                var additionalInformationDb = this.AdditionalInformations.FirstOrDefault(d => d.Language.Code == additionalInformation.Language.Code);
                if (additionalInformationDb != null)
                {
                    additionalInformationDb.Update(additionalInformation);
                }
                else
                {
                    this.CreateAdditionalInformation(additionalInformation);
                }
            }
        }

        /// <summary>Deletes the additional informations.</summary>
        /// <param name="newAdditionalInformations">The new additional informations.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAdditionalInformations(List<ProjectAdditionalInformation> newAdditionalInformations, int userId)
        {
            var additionalInformationsToDelete = this.AdditionalInformations.Where(db => newAdditionalInformations?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var additionalInformationToDelete in additionalInformationsToDelete)
            {
                additionalInformationToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the additional information.</summary>
        /// <param name="additionalInformation">The additional information.</param>
        private void CreateAdditionalInformation(ProjectAdditionalInformation additionalInformation)
        {
            this.AdditionalInformations.Add(additionalInformation);
        }

        #endregion

        #region Interests

        /// <summary>Updates the interests.</summary>
        /// <param name="interests">The interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateInterests(
            List<Interest> interests,
            int userId)
        {
            this.SynchronizeInterests(interests, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Synchronizes the interests.</summary>
        /// <param name="interests">The interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeInterests(List<Interest> interests, int userId)
        {
            if (this.Interests == null)
            {
                this.Interests = new List<ProjectInterest>();
            }

            this.DeleteInterests(interests, userId);

            if (interests?.Any() != true)
            {
                return;
            }

            // Create or update interests
            foreach (var interest in interests)
            {
                var interestDb = this.Interests.FirstOrDefault(a => a.Interest.Uid == interest.Uid);
                if (interestDb != null)
                {
                    interestDb.Update(userId);
                }
                else
                {
                    this.CreateInterest(interest, userId);
                }
            }
        }

        /// <summary>Deletes the interests.</summary>
        /// <param name="newInterests">The new interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteInterests(List<Interest> newInterests, int userId)
        {
            var interestsToDelete = this.Interests.Where(db => newInterests?.Select(a => a.Uid)?.Contains(db.Interest.Uid) == false && !db.IsDeleted).ToList();
            foreach (var interestToDelete in interestsToDelete)
            {
                interestToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the interest.</summary>
        /// <param name="intestest">The intestest.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateInterest(Interest intestest, int userId)
        {
            this.Interests.Add(new ProjectInterest(this, intestest, userId));
        }

        #endregion

        #region Target Audiences

        /// <summary>Updates the target audiences.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        {
            this.SynchronizeTargetAudiences(targetAudiences, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Synchronizes the target audiences.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        {
            if (this.TargetAudiences == null)
            {
                this.TargetAudiences = new List<ProjectTargetAudience>();
            }

            this.DeleteTargetAudiences(targetAudiences, userId);

            if (targetAudiences?.Any() != true)
            {
                return;
            }

            // Create or update target audiences
            foreach (var targetAudience in targetAudiences)
            {
                var targetAudienceDb = this.TargetAudiences.FirstOrDefault(a => a.TargetAudience.Uid == targetAudience.Uid);
                if (targetAudienceDb != null)
                {
                    targetAudienceDb.Update(userId);
                }
                else
                {
                    this.CreateTargetAudience(targetAudience, userId);
                }
            }
        }

        /// <summary>Deletes the target audiences.</summary>
        /// <param name="newTargetAudiences">The new target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteTargetAudiences(List<TargetAudience> newTargetAudiences, int userId)
        {
            var targetAudiencesToDelete = this.TargetAudiences.Where(db => newTargetAudiences?.Select(a => a.Uid)?.Contains(db.TargetAudience.Uid) == false && !db.IsDeleted).ToList();
            foreach (var targetAudienceToDelete in targetAudiencesToDelete)
            {
                targetAudienceToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the target audience.</summary>
        /// <param name="targetAudience">The target audience.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateTargetAudience(TargetAudience targetAudience, int userId)
        {
            this.TargetAudiences.Add(new ProjectTargetAudience(this, targetAudience, userId));
        }

        #endregion

        #region Image Links

        /// <summary>Synchronizes the image links.</summary>
        /// <param name="imageLink">The image link.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeImageLinks(string imageLink, int userId)
        {
            if (this.ImageLinks == null)
            {
                this.ImageLinks = new List<ProjectImageLink>();
            }

            var imageLinkDb = this.ImageLinks?.FirstOrDefault();
            if (!string.IsNullOrEmpty(imageLink))
            {
                if (imageLinkDb != null)
                {
                    imageLinkDb.Update(imageLink, userId);
                }
                else
                {
                    this.ImageLinks.Add(new ProjectImageLink(imageLink, userId));
                }
            }
            else
            {
                imageLinkDb?.Delete(userId);
            }
        }

        #endregion

        #region Teaser Links

        /// <summary>Synchronizes the teaser links.</summary>
        /// <param name="teaserLink">The teaser link.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeTeaserLinks(string teaserLink, int userId)
        {
            if (this.TeaserLinks == null)
            {
                this.TeaserLinks = new List<ProjectTeaserLink>();
            }

            var teaserLinkDb = this.TeaserLinks?.FirstOrDefault();
            if (!string.IsNullOrEmpty(teaserLink))
            {
                if (teaserLinkDb != null)
                {
                    teaserLinkDb.Update(teaserLink, userId);
                }
                else
                {
                    this.TeaserLinks.Add(new ProjectTeaserLink(teaserLink, userId));
                }
            }
            else
            {
                teaserLinkDb?.Delete(userId);
            }
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            //this.ValidateName();
            this.ValidateTitles();
            this.ValidateLogLines();
            this.ValidateSummaries();
            this.ValidateProductionPlans();
            this.ValidateAdditionalInformations();
            this.ValidateImageLinks();
            this.ValidateTeaserLinks();
            this.ValidateBuyerEvaluations();

            return this.ValidationResult.IsValid;
        }

        ///// <summary>Validates the name.</summary>
        //public void ValidateName()
        //{
        //    if (string.IsNullOrEmpty(this.Name?.Trim()))
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
        //    }

        //    if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
        //    }
        //}

        ///// <summary>Validates the descriptions.</summary>
        //public void ValidateDescriptions()
        //{
        //    foreach (var description in this.Descriptions?.Where(d => !d.IsValid())?.ToList())
        //    {
        //        this.ValidationResult.Add(description.ValidationResult);
        //    }
        //}

        /// <summary>Validates the titles.</summary>
        public void ValidateTitles()
        {
            if (this.Titles?.Any() != true)
            {
                return;
            }

            foreach (var title in this.Titles?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(title.ValidationResult);
            }
        }

        /// <summary>Validates the log lines.</summary>
        public void ValidateLogLines()
        {
            if (this.LogLines?.Any() != true)
            {
                return;
            }

            foreach (var logLine in this.LogLines?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(logLine.ValidationResult);
            }
        }

        /// <summary>Validates the summaries.</summary>
        public void ValidateSummaries()
        {
            if (this.Summaries?.Any() != true)
            {
                return;
            }

            foreach (var summary in this.Summaries?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(summary.ValidationResult);
            }
        }

        /// <summary>Validates the production plans.</summary>
        public void ValidateProductionPlans()
        {
            if (this.ProductionPlans?.Any() != true)
            {
                return;
            }

            foreach (var productionPlan in this.ProductionPlans?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(productionPlan.ValidationResult);
            }
        }

        /// <summary>Validates the additional informations.</summary>
        public void ValidateAdditionalInformations()
        {
            if (this.AdditionalInformations?.Any() != true)
            {
                return;
            }

            foreach (var additionalInformation in this.AdditionalInformations?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(additionalInformation.ValidationResult);
            }
        }

        /// <summary>Validates the image links.</summary>
        public void ValidateImageLinks()
        {
            if (this.ImageLinks?.Any() != true)
            {
                return;
            }

            foreach (var imageLink in this.ImageLinks?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(imageLink.ValidationResult);
            }
        }

        /// <summary>Validates the teaser links.</summary>
        public void ValidateTeaserLinks()
        {
            if (this.TeaserLinks?.Any() != true)
            {
                return;
            }

            foreach (var teaserLink in this.TeaserLinks?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(teaserLink.ValidationResult);
            }
        }

        /// <summary>Validates the buyer evaluations.</summary>
        public void ValidateBuyerEvaluations()
        {
            if (this.BuyerEvaluations?.Any() != true)
            {
                return;
            }

            foreach (var buyerEvaluation in this.BuyerEvaluations?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(buyerEvaluation.ValidationResult);
            }
        }

        #endregion

        #region Old methods

        //public Project(IEnumerable<ProjectTitle> titles)
        //{
        //    SetTitles(titles);
        //}

        //public void SetTitles(IEnumerable<ProjectTitle> titles)
        //{
        //    if (titles != null)
        //    {
        //        Titles = titles.ToList();
        //    }
        //    else
        //    {
        //        Titles = null;
        //    }
        //}

        //public void SetLogLines(IEnumerable<ProjectLogLine> entities)
        //{
        //    if (entities != null)
        //    {
        //        LogLines = entities.ToList();
        //    }
        //    else
        //    {
        //        LogLines = null;
        //    }
        //}

        //public void SetSummaries(IEnumerable<ProjectSummary> entities)
        //{
        //    if (entities != null)
        //    {
        //        Summaries = entities.ToList();
        //    }
        //    else
        //    {
        //        Summaries = null;
        //    }
        //}

        //public void SetProductionPlans(IEnumerable<ProjectProductionPlan> entities)
        //{
        //    if (entities != null)
        //    {
        //        ProductionPlans = entities.ToList();
        //    }
        //    else
        //    {
        //        ProductionPlans = null;
        //    }
        //}

        //public void SetInterests(IEnumerable<ProjectInterest> entities)
        //{
        //    if (entities != null)
        //    {
        //        Interests = entities.ToList();
        //    }
        //    else
        //    {
        //        Interests = null;
        //    }
        //}

        //public void SetLinksImage(IEnumerable<ProjectLinkImage> entities)
        //{
        //    if (entities != null)
        //    {
        //        LinksImage = entities.ToList();
        //    }
        //    else
        //    {
        //        LinksImage = null;
        //    }
        //}

        //public void SetLinksTeaser(IEnumerable<ProjectLinkTeaser> entities)
        //{
        //    if (entities != null)
        //    {
        //        LinksTeaser = entities.ToList();
        //    }
        //    else
        //    {
        //        LinksTeaser = null;
        //    }
        //}

        //public void SetAdditionalInformations(IEnumerable<ProjectAdditionalInformation> entities)
        //{
        //    if (entities != null)
        //    {
        //        AdditionalInformations = entities.ToList();
        //    }
        //    else
        //    {
        //        AdditionalInformations = null;
        //    }
        //}

        //public void SetNumberOfEpisodes(int value)
        //{
        //    NumberOfEpisodes = value;
        //}

        //public void SetEachEpisodePlayingTime(string value)
        //{
        //    EachEpisodePlayingTime = value;
        //}

        //public void SetValuePerEpisode(string value)
        //{
        //    ValuePerEpisode = value;
        //}

        //public void SetTotalValueOfProject(string value)
        //{
        //    TotalValueOfProject = value;
        //}

        //public void SetValueAlreadyRaised(string value)
        //{
        //    ValueAlreadyRaised = value;
        //}

        //public void SetValueStillNeeded(string value)
        //{
        //    ValueStillNeeded = value;
        //}

        //public void SetPitching(bool? value)
        //{
        //    Pitching = value;
        //}

        //public void SetProducer(Producer producer)
        //{
        //    Producer = producer;
        //    if (producer != null)
        //    {
        //        ProducerId = producer.Id;
        //    }
        //}

        //public string GetName()
        //{
        //    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

        //    string titlePt = Titles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
        //    string titleEn = Titles.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

        //    if (currentCulture != null && currentCulture.Name == "pt-BR" && !string.IsNullOrWhiteSpace(titlePt))
        //    {
        //        return titlePt;
        //    }
        //    else if (!string.IsNullOrWhiteSpace(titleEn))
        //    {
        //        return titleEn;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        #endregion
    }
}
