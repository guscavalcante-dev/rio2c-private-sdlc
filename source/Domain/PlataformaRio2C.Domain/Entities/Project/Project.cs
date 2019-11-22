// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
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
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Project</summary>
    public class Project : Entity
    {
        public static readonly int TotalPlayingTimeMinLength = 1;
        public static readonly int TotalPlayingTimeMaxLength = 10;
        public static readonly int EachEpisodePlayingTimeMinLength = 1;
        public static readonly int EachEpisodePlayingTimeMaxLength = 10;
        public static readonly int ValuePerEpisodeMinLength = 1;
        public static readonly int ValuePerEpisodeMaxLength = 50;
        public static readonly int TotalValueOfProjectMinLength = 1;
        public static readonly int TotalValueOfProjectMaxLength = 50;
        public static readonly int ValueAlreadyRaisedMinLength = 1;
        public static readonly int ValueAlreadyRaisedMaxLength = 50;
        public static readonly int ValueStillNeededMinLength = 1;
        public static readonly int ValueStillNeededMaxLength = 50;

        public int ProjectTypeId { get; private set; }
        public int SellerAttendeeOrganizationId { get; private set; }
        public string TotalPlayingTime { get; private set; }
        public int? NumberOfEpisodes { get; private set; }
        public string EachEpisodePlayingTime { get; private set; }
        public string ValuePerEpisode { get; private set; }
        public string TotalValueOfProject { get; private set; }
        public string ValueAlreadyRaised { get; private set; }
        public string ValueStillNeeded { get; private set; }
        public bool IsPitching { get; private set; }
        public DateTime? FinishDate { get; private set; }
        public int ProjectBuyerEvaluationsCount { get; private set; }

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
        /// <param name="totalPlayingTime">The total playing time.</param>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="valuePerEpisode">The value per episode.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="valueAlreadyRaised">The value already raised.</param>
        /// <param name="valueStillNeeded">The value still needed.</param>
        /// <param name="isPitching">if set to <c>true</c> [is pitching].</param>
        /// <param name="projectTitles">The project titles.</param>
        /// <param name="projectLogLines">The project log lines.</param>
        /// <param name="projectSummaries">The project summaries.</param>
        /// <param name="projectProductionPlans">The project production plans.</param>
        /// <param name="projectAdditionalInformations">The project additional informations.</param>
        /// <param name="projectInterests">The project interests.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="imageLink">The image link.</param>
        /// <param name="teaserLink">The teaser link.</param>
        /// <param name="userId">The user identifier.</param>
        public Project(
            ProjectType projectType,
            AttendeeOrganization sellerAttendeeOrganization,
            string totalPlayingTime,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            string valuePerEpisode,
            string totalValueOfProject,
            string valueAlreadyRaised,
            string valueStillNeeded,
            bool isPitching,
            List<ProjectTitle> projectTitles,
            List<ProjectLogLine> projectLogLines,
            List<ProjectSummary> projectSummaries,
            List<ProjectProductionPlan> projectProductionPlans,
            List<ProjectAdditionalInformation> projectAdditionalInformations,
            List<ProjectInterest> projectInterests,
            List<TargetAudience> targetAudiences,
            string imageLink,
            string teaserLink,
            int userId)
        {
            this.ProjectTypeId = projectType?.Id ?? 0;
            this.ProjectType = projectType;
            this.SellerAttendeeOrganizationId = sellerAttendeeOrganization?.Id ?? 0;
            this.SellerAttendeeOrganization = sellerAttendeeOrganization;
            this.TotalPlayingTime = totalPlayingTime;
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime?.Trim();
            this.ValuePerEpisode = valuePerEpisode?.Trim();
            this.TotalValueOfProject = totalValueOfProject?.Trim();
            this.ValueAlreadyRaised = valueAlreadyRaised?.Trim();
            this.ValueStillNeeded = valueStillNeeded?.Trim();
            this.IsPitching = isPitching;
            this.FinishDate = null;
            this.ProjectBuyerEvaluationsCount = 0;

            this.SynchronizeTitles(projectTitles, userId);
            this.SynchronizeLogLines(projectLogLines, userId);
            this.SynchronizeSummaries(projectSummaries, userId);
            this.SynchronizeProductionPlans(projectProductionPlans, userId);
            this.SynchronizeAdditionalInformations(projectAdditionalInformations, userId);
            this.SynchronizeInterests(projectInterests, userId);
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
        /// <param name="totalPlayingTime">The total playing time.</param>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="valuePerEpisode">The value per episode.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="valueAlreadyRaised">The value already raised.</param>
        /// <param name="valueStillNeeded">The value still needed.</param>
        /// <param name="isPitching">if set to <c>true</c> [is pitching].</param>
        /// <param name="projectTitles">The project titles.</param>
        /// <param name="projectLogLines">The project log lines.</param>
        /// <param name="projectSummaries">The project summaries.</param>
        /// <param name="projectProductionPlans">The project production plans.</param>
        /// <param name="projectAdditionalInformations">The project additional informations.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            string totalPlayingTime,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            string valuePerEpisode,
            string totalValueOfProject,
            string valueAlreadyRaised,
            string valueStillNeeded,
            bool isPitching,
            List<ProjectTitle> projectTitles,
            List<ProjectLogLine> projectLogLines,
            List<ProjectSummary> projectSummaries,
            List<ProjectProductionPlan> projectProductionPlans,
            List<ProjectAdditionalInformation> projectAdditionalInformations,
            int userId)
        {
            this.TotalPlayingTime = totalPlayingTime;
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime?.Trim();
            this.ValuePerEpisode = valuePerEpisode?.Trim();
            this.TotalValueOfProject = totalValueOfProject?.Trim();
            this.ValueAlreadyRaised = valueAlreadyRaised?.Trim();
            this.ValueStillNeeded = valueStillNeeded?.Trim();
            this.IsPitching = isPitching;

            this.SynchronizeTitles(projectTitles, userId);
            this.SynchronizeLogLines(projectLogLines, userId);
            this.SynchronizeSummaries(projectSummaries, userId);
            this.SynchronizeProductionPlans(projectProductionPlans, userId);
            this.SynchronizeAdditionalInformations(projectAdditionalInformations, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Updates the links.</summary>
        /// <param name="imageLink">The image link.</param>
        /// <param name="teaserLink">The teaser link.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateLinks(
            string imageLink,
            string teaserLink,
            int userId)
        {
            this.SynchronizeImageLinks(imageLink, userId);
            this.SynchronizeTeaserLinks(teaserLink, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Finishes the project.</summary>
        /// <param name="userId">The user identifier.</param>
        public void FinishProject(int userId)
        {
            this.FinishDate = DateTime.Now;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Determines whether this instance is finished.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is finished; otherwise, <c>false</c>.</returns>
        public bool IsFinished()
        {
            return this.FinishDate.HasValue;
        }

        #region Buyer Evaluations

        /// <summary>Creates the buyer evaluation.</summary>
        /// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        /// <param name="projectEvaluationStatus">The project evaluation status.</param>
        /// <param name="userId">The user identifier.</param>
        public void CreateBuyerEvaluation(
            AttendeeOrganization buyerAttendeeOrganization,
            ProjectEvaluationStatus projectEvaluationStatus,
            int userId)
        {
            if (this.BuyerEvaluations == null)
            {
                this.BuyerEvaluations = new List<ProjectBuyerEvaluation>();
            }

            var buyerEvaluation = this.GetBuyerEvaluationByAttendeeOrganizationUid(buyerAttendeeOrganization?.Uid ?? Guid.Empty);
            if (buyerEvaluation == null)
            {
                this.BuyerEvaluations.Add(new ProjectBuyerEvaluation(this, buyerAttendeeOrganization, projectEvaluationStatus, userId));
            }
            else if (buyerEvaluation.IsDeleted)
            {
                buyerEvaluation.Restore(projectEvaluationStatus, userId);
            }

            this.UpdateBuyerEvaluationCounts();

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

            this.UpdateBuyerEvaluationCounts();

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Updates the buyer evaluation counts.</summary>
        public void UpdateBuyerEvaluationCounts()
        {
            this.ProjectBuyerEvaluationsCount = this.RecountBuyerEvaluations();
        }

        /// <summary>Recounts the buyer evaluations.</summary>
        /// <returns></returns>
        public int RecountBuyerEvaluations()
        {
            return this.BuyerEvaluations?.Count(be => !be.IsDeleted) ?? 0;
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
        /// <param name="projectInterests">The project interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateInterests(
            List<ProjectInterest> projectInterests,
            int userId)
        {
            this.SynchronizeInterests(projectInterests, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Synchronizes the interests.</summary>
        /// <param name="projectInterests">The project interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeInterests(List<ProjectInterest> projectInterests, int userId)
        {
            if (this.Interests == null)
            {
                this.Interests = new List<ProjectInterest>();
            }

            this.DeleteInterests(projectInterests, userId);

            if (projectInterests?.Any() != true)
            {
                return;
            }

            // Create or update interests
            foreach (var projectInterest in projectInterests)
            {
                var interestDb = this.Interests.FirstOrDefault(a => a.Interest.Uid == projectInterest.Interest.Uid);
                if (interestDb != null)
                {
                    interestDb.Update(projectInterest, userId);
                }
                else
                {
                    this.Interests.Add(projectInterest);
                }
            }
        }

        /// <summary>Deletes the interests.</summary>
        /// <param name="newProjectInterests">The new project interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteInterests(List<ProjectInterest> newProjectInterests, int userId)
        {
            var projectInterestsToDelete = this.Interests.Where(db => newProjectInterests?.Select(a => a.Interest.Uid)?.Contains(db.Interest.Uid) == false && !db.IsDeleted).ToList();
            foreach (var projectInterestToDelete in projectInterestsToDelete)
            {
                projectInterestToDelete.Delete(userId);
            }
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

            this.ValidateIsFinished();
            this.ValidateTotalPlayingTime();
            this.ValidateEachEpisodePlayingTime();
            this.ValidateValuePerEpisode();
            this.ValidateTotalValueOfProject();
            this.ValidateValueAlreadyRaised();
            this.ValidateValueStillNeeded();
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

        /// <summary>Determines whether [is create valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is create valid]; otherwise, <c>false</c>.</returns>
        public bool IsCreateValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateTotalPlayingTime();
            this.ValidateEachEpisodePlayingTime();
            this.ValidateValuePerEpisode();
            this.ValidateTotalValueOfProject();
            this.ValidateValueAlreadyRaised();
            this.ValidateValueStillNeeded();
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

        /// <summary>Determines whether [is finish valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is finish valid]; otherwise, <c>false</c>.</returns>
        public bool IsFinishValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateBuyerEvaluations();
            this.ValidateRequiredBuyerEvaluations();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the is finished.</summary>
        public void ValidateIsFinished()
        {
            if (this.IsFinished())
            {
                this.ValidationResult.Add(new ValidationError(Messages.ProjectIsFinishedCannotBeUpdated, new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the total playing time.</summary>
        public void ValidateTotalPlayingTime()
        {
            if (string.IsNullOrEmpty(this.TotalPlayingTime?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.TotalPlayingTime), new string[] { "TotalPlayingTime" }));
            }

            if (this.TotalPlayingTime?.Trim().Length < TotalPlayingTimeMinLength || this.TotalPlayingTime?.Trim().Length > TotalPlayingTimeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.TotalPlayingTime, TotalPlayingTimeMaxLength, TotalPlayingTimeMinLength), new string[] { "TotalPlayingTime" }));
            }
        }

        /// <summary>Validates the each episode playing time.</summary>
        public void ValidateEachEpisodePlayingTime()
        {
            if (!string.IsNullOrEmpty(this.EachEpisodePlayingTime) && this.EachEpisodePlayingTime?.Trim().Length > EachEpisodePlayingTimeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.EachEpisodePlayingTime, EachEpisodePlayingTimeMaxLength, EachEpisodePlayingTimeMinLength), new string[] { "EachEpisodePlayingTime" }));
            }
        }

        /// <summary>Validates the value per episode.</summary>
        public void ValidateValuePerEpisode()
        {
            if (!string.IsNullOrEmpty(this.ValuePerEpisode) && this.ValuePerEpisode?.Trim().Length > ValuePerEpisodeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.ValuePerEpisode, ValuePerEpisodeMaxLength, ValuePerEpisodeMinLength), new string[] { "ValuePerEpisode" }));
            }
        }
        /// <summary>Validates the total value of project.</summary>
        public void ValidateTotalValueOfProject()
        {
            if (!string.IsNullOrEmpty(this.TotalValueOfProject) && this.TotalValueOfProject?.Trim().Length > TotalValueOfProjectMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.TotalValueOfProject, TotalValueOfProjectMaxLength, TotalValueOfProjectMinLength), new string[] { "TotalValueOfProject" }));
            }
        }
        /// <summary>Validates the value already raised.</summary>
        public void ValidateValueAlreadyRaised()
        {
            if (!string.IsNullOrEmpty(this.ValueAlreadyRaised) && this.ValueAlreadyRaised?.Trim().Length > ValueAlreadyRaisedMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.ValueAlreadyRaised, ValueAlreadyRaisedMaxLength, ValueAlreadyRaisedMinLength), new string[] { "ValueAlreadyRaised" }));
            }
        }
        /// <summary>Validates the value still needed.</summary>
        public void ValidateValueStillNeeded()
        {
            if (!string.IsNullOrEmpty(this.ValueStillNeeded) && this.ValueStillNeeded?.Trim().Length > ValueStillNeededMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.ValueStillNeeded, ValueStillNeededMaxLength, ValueStillNeededMinLength), new string[] { "ValueStillNeeded" }));
            }
        }

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

            if (this.ProjectBuyerEvaluationsCount > this.SellerAttendeeOrganization.GetProjectMaxBuyerEvaluationsCount())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.MaxProjectBuyersEvaluationsReached, this.SellerAttendeeOrganization.GetProjectMaxBuyerEvaluationsCount(), Labels.Players), new string[] { "ToastrError" }));
            }

            foreach (var buyerEvaluation in this.BuyerEvaluations?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(buyerEvaluation.ValidationResult);
            }
        }

        /// <summary>Validates the required buyer evaluations.</summary>
        public void ValidateRequiredBuyerEvaluations()
        {
            if (this.ProjectBuyerEvaluationsCount == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheProjectMustHaveOnePlayer, Labels.Player), new string[] { "ToastrError" }));
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

