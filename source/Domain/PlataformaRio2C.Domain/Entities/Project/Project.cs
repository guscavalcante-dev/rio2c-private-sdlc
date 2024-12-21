// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-24-2024
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
        public DateTimeOffset? FinishDate { get; private set; }
        public int ProjectBuyerEvaluationsCount { get; private set; }
        public int CommissionEvaluationsCount { get; private set; }
        public decimal? CommissionGrade { get; private set; }
        public DateTimeOffset? LastCommissionEvaluationDate { get; private set; }

        public virtual ProjectType ProjectType { get; private set; }
        public virtual AttendeeOrganization SellerAttendeeOrganization { get; private set; }
        public virtual ProjectModality ProjectModality { get; private set; }

        public virtual ICollection<ProjectTitle> ProjectTitles { get; private set; }
        public virtual ICollection<ProjectLogLine> ProjectLogLines { get; private set; }
        public virtual ICollection<ProjectSummary> ProjectSummaries { get; private set; }
        public virtual ICollection<ProjectProductionPlan> ProjectProductionPlans { get; private set; }
        public virtual ICollection<ProjectAdditionalInformation> ProjectAdditionalInformations { get; private set; }
        public virtual ICollection<ProjectImageLink> ProjectImageLinks { get; private set; }
        public virtual ICollection<ProjectTeaserLink> ProjectTeaserLinks { get; private set; }
        public virtual ICollection<ProjectInterest> ProjectInterests { get; private set; }
        public virtual ICollection<ProjectTargetAudience> ProjectTargetAudiences { get; private set; }
        public virtual ICollection<ProjectBuyerEvaluation> ProjectBuyerEvaluations { get; private set; }
        public virtual ICollection<CommissionEvaluation> CommissionEvaluations { get; private set; }

        private bool IsAdmin = false;
        public int ProjectModalityId { get; private set; }

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
        /// <param name="projectModality">Modality of the project.</param>
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
            List<ProjectTitle> projectTitles,
            List<ProjectLogLine> projectLogLines,
            List<ProjectSummary> projectSummaries,
            List<ProjectProductionPlan> projectProductionPlans,
            List<ProjectAdditionalInformation> projectAdditionalInformations,
            List<ProjectInterest> projectInterests,
            List<TargetAudience> targetAudiences,
            string imageLink,
            string teaserLink,
            int userId,
            ProjectModality projectModality
        )
        {
            this.ProjectTypeId = projectType?.Id ?? 0;
            this.ProjectModalityId = projectModality?.Id ?? 0;
            this.ProjectType = projectType;
            this.ProjectModality = projectModality;
            this.SellerAttendeeOrganizationId = sellerAttendeeOrganization?.Id ?? 0;
            this.SellerAttendeeOrganization = sellerAttendeeOrganization;
            this.TotalPlayingTime = totalPlayingTime;
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime?.Trim();
            this.ValuePerEpisode = valuePerEpisode?.Trim();
            this.TotalValueOfProject = totalValueOfProject?.Trim();
            this.ValueAlreadyRaised = valueAlreadyRaised?.Trim();
            this.ValueStillNeeded = valueStillNeeded?.Trim();
            this.FinishDate = null;
            this.ProjectBuyerEvaluationsCount = 0;

            this.SynchronizeProjectTitles(projectTitles, userId);
            this.SynchronizeProjectLogLines(projectLogLines, userId);
            this.SynchronizeProjectSummaries(projectSummaries, userId);
            this.SynchronizeProjectProductionPlans(projectProductionPlans, userId);
            this.SynchronizeProjectAdditionalInformations(projectAdditionalInformations, userId);
            this.SynchronizeProjectInterests(projectInterests, userId);
            this.SynchronizeProjectTargetAudiences(targetAudiences, userId);
            this.SynchronizeProjectImageLinks(imageLink, userId);
            this.SynchronizeProjectTeaserLinks(teaserLink, userId);

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="Project"/> class.</summary>
        protected Project()
        {
        }

        /// <summary>
        /// Updates the main information.
        /// </summary>
        /// <param name="totalPlayingTime">The total playing time.</param>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="valuePerEpisode">The value per episode.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="valueAlreadyRaised">The value already raised.</param>
        /// <param name="valueStillNeeded">The value still needed.</param>
        /// <param name="projectTitles">The project titles.</param>
        /// <param name="projectLogLines">The project log lines.</param>
        /// <param name="projectSummaries">The project summaries.</param>
        /// <param name="projectProductionPlans">The project production plans.</param>
        /// <param name="projectAdditionalInformations">The project additional informations.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void UpdateMainInformation(
            string totalPlayingTime,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            string valuePerEpisode,
            string totalValueOfProject,
            string valueAlreadyRaised,
            string valueStillNeeded,
            List<ProjectTitle> projectTitles,
            List<ProjectLogLine> projectLogLines,
            List<ProjectSummary> projectSummaries,
            List<ProjectProductionPlan> projectProductionPlans,
            List<ProjectAdditionalInformation> projectAdditionalInformations,
            int userId,
            bool isAdmin,
            ProjectModality projectModality
        )
        {
            this.TotalPlayingTime = totalPlayingTime;
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime?.Trim();
            this.ValuePerEpisode = valuePerEpisode?.Trim();
            this.TotalValueOfProject = totalValueOfProject?.Trim();
            this.ValueAlreadyRaised = valueAlreadyRaised?.Trim();
            this.ValueStillNeeded = valueStillNeeded?.Trim();

            this.SynchronizeProjectTitles(projectTitles, userId);
            this.SynchronizeProjectLogLines(projectLogLines, userId);
            this.SynchronizeProjectSummaries(projectSummaries, userId);
            this.SynchronizeProjectProductionPlans(projectProductionPlans, userId);
            this.SynchronizeProjectAdditionalInformations(projectAdditionalInformations, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;

            this.IsAdmin = isAdmin;
            this.ProjectModalityId = projectModality.Id;
        }

        /// <summary>
        /// Updates the links.
        /// </summary>
        /// <param name="imageLink">The image link.</param>
        /// <param name="teaserLink">The teaser link.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void UpdateLinks(
            string imageLink,
            string teaserLink,
            int userId,
            bool isAdmin)
        {
            this.SynchronizeProjectImageLinks(imageLink, userId);
            this.SynchronizeProjectTeaserLinks(teaserLink, userId);
            base.SetUpdateDate(userId);

            this.IsAdmin = isAdmin;
        }

        /// <summary>Finishes the project.</summary>
        /// <param name="userId">The user identifier.</param>
        public void FinishProject(int userId)
        {
            this.FinishDate = DateTime.UtcNow;
            base.SetUpdateDate(userId);
        }

        /// <summary>Determines whether this instance is finished.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is finished; otherwise, <c>false</c>.</returns>
        public bool IsFinished()
        {
            return this.FinishDate.HasValue;
        }

        /// <summary>
        /// Gets the title dto by language code.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public string GetTitleByLanguageCode(string culture)
        {
            return this.ProjectTitles?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant())?.Value;
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void Delete(
            int userId,
            bool isAdmin)
        {
            this.DeleteProjectBuyerEvaluations(userId);

            base.Delete(userId);

            this.IsAdmin = isAdmin;
        }

        #region Buyer Evaluations

        /// <summary>
        /// Creates the project buyer evaluation.
        /// </summary>
        /// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        /// <param name="projectEvaluationStatus">The project evaluation status.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void CreateProjectBuyerEvaluation(
            AttendeeOrganization buyerAttendeeOrganization,
            ProjectEvaluationStatus projectEvaluationStatus,
            int userId,
            bool isAdmin)
        {
            if (this.ProjectBuyerEvaluations == null)
            {
                this.ProjectBuyerEvaluations = new List<ProjectBuyerEvaluation>();
            }

            var buyerEvaluation = this.GetProjectBuyerEvaluationByAttendeeOrganizationUid(buyerAttendeeOrganization?.Uid ?? Guid.Empty);
            if (buyerEvaluation == null)
            {
                this.ProjectBuyerEvaluations.Add(new ProjectBuyerEvaluation(this, buyerAttendeeOrganization, projectEvaluationStatus, userId));
            }
            else if (buyerEvaluation.IsDeleted)
            {
                buyerEvaluation.Restore(projectEvaluationStatus, userId);
            }

            this.UpdateProjectBuyerEvaluationCounts();

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;

            this.IsAdmin = isAdmin;
        }

        /// <summary>
        /// Deletes the project buyer evaluation.
        /// </summary>
        /// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void DeleteProjectBuyerEvaluation(
            AttendeeOrganization buyerAttendeeOrganization,
            int userId,
            bool isAdmin)
        {
            var buyerEvaluation = this.GetProjectBuyerEvaluationByAttendeeOrganizationUid(buyerAttendeeOrganization?.Uid ?? Guid.Empty);
            buyerEvaluation?.Delete(userId);

            this.UpdateProjectBuyerEvaluationCounts();

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;

            this.IsAdmin = isAdmin;
        }

        /// <summary>
        /// Deletes the project buyer evaluations.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteProjectBuyerEvaluations(int userId)
        {
            var buyerEvaluations = this.FindAllProjectBuyerEvaluationsNotDeleted();
            if (buyerEvaluations?.Any() != true)
            {
                return;
            }

            buyerEvaluations.ForEach(be => be.Delete(userId));

            this.UpdateProjectBuyerEvaluationCounts();
        }

        /// <summary>Updates the project buyer evaluation counts.</summary>
        public void UpdateProjectBuyerEvaluationCounts()
        {
            this.ProjectBuyerEvaluationsCount = this.RecountProjectBuyerEvaluations();
        }

        /// <summary>Recounts the project buyer evaluations.</summary>
        /// <returns></returns>
        public int RecountProjectBuyerEvaluations()
        {
            return this.ProjectBuyerEvaluations?.Count(be => !be.IsDeleted) ?? 0;
        }

        /// <summary>
        /// Accepts the project buyer evaluation.
        /// </summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="projectsApprovalLimitExceeded">if set to <c>true</c> [projects approval limit exceeded].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ProjectBuyerEvaluation AcceptProjectBuyerEvaluation(
            Guid attendeeOrganizationUid, 
            List<ProjectEvaluationStatus> projectEvaluationStatuses,
            bool projectsApprovalLimitExceeded,
            int userId)
        {
            var buyerEvaluation = this.GetProjectBuyerEvaluationByAttendeeOrganizationUid(attendeeOrganizationUid);
            buyerEvaluation?.Accept(projectEvaluationStatuses, projectsApprovalLimitExceeded, userId);

            return buyerEvaluation;
        }

        /// <summary>Refuses the project buyer evaluation.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <param name="projectEvaluationRefuseReason">The project evaluation refuse reason.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ProjectBuyerEvaluation RefuseProjectBuyerEvaluation(
            Guid attendeeOrganizationUid,
            ProjectEvaluationRefuseReason projectEvaluationRefuseReason,
            string reason,
            List<ProjectEvaluationStatus> projectEvaluationStatuses,
            int userId)
        {
            var buyerEvaluation = this.GetProjectBuyerEvaluationByAttendeeOrganizationUid(attendeeOrganizationUid);
            buyerEvaluation?.Refuse(projectEvaluationRefuseReason, reason, projectEvaluationStatuses, userId);

            return buyerEvaluation;
        }

        /// <summary>Gets the project buyer evaluation by attendee organization uid.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns></returns>
        private ProjectBuyerEvaluation GetProjectBuyerEvaluationByAttendeeOrganizationUid(Guid attendeeOrganizationUid)
        {
            return this.ProjectBuyerEvaluations?.FirstOrDefault(pbe => pbe.BuyerAttendeeOrganization.Uid == attendeeOrganizationUid);
        }

        /// <summary>
        /// Finds all project buyer evaluations not deleted.
        /// </summary>
        /// <returns></returns>
        private List<ProjectBuyerEvaluation> FindAllProjectBuyerEvaluationsNotDeleted()
        {
            return this.ProjectBuyerEvaluations?.Where(pbe => !pbe.IsDeleted)?.ToList();
        }

        #endregion

        #region Titles

        /// <summary>Synchronizes the project titles.</summary>
        /// <param name="titles">The titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectTitles(List<ProjectTitle> titles, int userId)
        {
            if (this.ProjectTitles == null)
            {
                this.ProjectTitles = new List<ProjectTitle>();
            }

            this.DeleteProjectTitles(titles, userId);

            if (titles?.Any() != true)
            {
                return;
            }

            foreach (var title in titles)
            {
                var titleDb = this.ProjectTitles.FirstOrDefault(d => d.Language.Code == title.Language.Code);
                if (titleDb != null)
                {
                    titleDb.Update(title);
                }
                else
                {
                    this.CreateProjectTitle(title);
                }
            }
        }

        /// <summary>Deletes the project titles.</summary>
        /// <param name="newTitles">The new titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteProjectTitles(List<ProjectTitle> newTitles, int userId)
        {
            var titlesToDelete = this.ProjectTitles.Where(db => newTitles?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var titleToDelete in titlesToDelete)
            {
                titleToDelete.Delete(userId);
            }
        }

        private void CreateProjectTitle(ProjectTitle title)
        {
            this.ProjectTitles.Add(title);
        }

        #endregion

        #region LogLines

        /// <summary>Synchronizes the project log lines.</summary>
        /// <param name="logLines">The log lines.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectLogLines(List<ProjectLogLine> logLines, int userId)
        {
            if (this.ProjectLogLines == null)
            {
                this.ProjectLogLines = new List<ProjectLogLine>();
            }

            this.DeleteProjectLogLines(logLines, userId);

            if (logLines?.Any() != true)
            {
                return;
            }

            foreach (var logLine in logLines)
            {
                var logLineDb = this.ProjectLogLines.FirstOrDefault(d => d.Language.Code == logLine.Language.Code);
                if (logLineDb != null)
                {
                    logLineDb.Update(logLine);
                }
                else
                {
                    this.CreateProjectLogLines(logLine);
                }
            }
        }

        private void DeleteProjectLogLines(List<ProjectLogLine> newLogLines, int userId)
        {
            var logLinesToDelete = this.ProjectLogLines.Where(db => newLogLines?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var logLineToDelete in logLinesToDelete)
            {
                logLineToDelete.Delete(userId);
            }
        }

        private void CreateProjectLogLines(ProjectLogLine logLine)
        {
            this.ProjectLogLines.Add(logLine);
        }

        #endregion

        #region Summaries

        /// <summary>Synchronizes the project summaries.</summary>
        /// <param name="summaries">The summaries.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectSummaries(List<ProjectSummary> summaries, int userId)
        {
            if (this.ProjectSummaries == null)
            {
                this.ProjectSummaries = new List<ProjectSummary>();
            }

            this.DeleteProjectSummaries(summaries, userId);

            if (summaries?.Any() != true)
            {
                return;
            }

            foreach (var summary in summaries)
            {
                var summaryDb = this.ProjectSummaries.FirstOrDefault(d => d.Language.Code == summary.Language.Code);
                if (summaryDb != null)
                {
                    summaryDb.Update(summary);
                }
                else
                {
                    this.CreateProjectSummary(summary);
                }
            }
        }

        /// <summary>Deletes the project summaries.</summary>
        /// <param name="newSummaries">The new summaries.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteProjectSummaries(List<ProjectSummary> newSummaries, int userId)
        {
            var summariesToDelete = this.ProjectSummaries.Where(db => newSummaries?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var summaryToDelete in summariesToDelete)
            {
                summaryToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the project summary.</summary>
        /// <param name="sumary">The sumary.</param>
        private void CreateProjectSummary(ProjectSummary sumary)
        {
            this.ProjectSummaries.Add(sumary);
        }

        #endregion

        #region Production Plans

        /// <summary>Synchronizes the project production plans.</summary>
        /// <param name="productionPlans">The production plans.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectProductionPlans(List<ProjectProductionPlan> productionPlans, int userId)
        {
            if (this.ProjectProductionPlans == null)
            {
                this.ProjectProductionPlans = new List<ProjectProductionPlan>();
            }

            this.DeleteProjectProductionPlans(productionPlans, userId);

            if (productionPlans?.Any() != true)
            {
                return;
            }

            foreach (var productionPlan in productionPlans)
            {
                var productionPlanDb = this.ProjectProductionPlans.FirstOrDefault(d => d.Language.Code == productionPlan.Language.Code);
                if (productionPlanDb != null)
                {
                    productionPlanDb.Update(productionPlan);
                }
                else
                {
                    this.CreateProjectProductionPlan(productionPlan);
                }
            }
        }

        /// <summary>Deletes the project production plans.</summary>
        /// <param name="newProductionPlans">The new production plans.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteProjectProductionPlans(List<ProjectProductionPlan> newProductionPlans, int userId)
        {
            var productionPlansToDelete = this.ProjectProductionPlans.Where(db => newProductionPlans?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var productionPlanToDelete in productionPlansToDelete)
            {
                productionPlanToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the project production plan.</summary>
        /// <param name="productionPlan">The production plan.</param>
        private void CreateProjectProductionPlan(ProjectProductionPlan productionPlan)
        {
            this.ProjectProductionPlans.Add(productionPlan);
        }

        #endregion

        #region Additional Informations

        /// <summary>Synchronizes the project additional informations.</summary>
        /// <param name="additionalInformations">The additional informations.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectAdditionalInformations(List<ProjectAdditionalInformation> additionalInformations, int userId)
        {
            if (this.ProjectAdditionalInformations == null)
            {
                this.ProjectAdditionalInformations = new List<ProjectAdditionalInformation>();
            }

            this.DeleteProjectAdditionalInformations(additionalInformations, userId);

            if (additionalInformations?.Any() != true)
            {
                return;
            }

            foreach (var additionalInformation in additionalInformations)
            {
                var additionalInformationDb = this.ProjectAdditionalInformations.FirstOrDefault(d => d.Language.Code == additionalInformation.Language.Code);
                if (additionalInformationDb != null)
                {
                    additionalInformationDb.Update(additionalInformation);
                }
                else
                {
                    this.CreateProjectAdditionalInformation(additionalInformation);
                }
            }
        }

        /// <summary>Deletes the project additional informations.</summary>
        /// <param name="newAdditionalInformations">The new additional informations.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteProjectAdditionalInformations(List<ProjectAdditionalInformation> newAdditionalInformations, int userId)
        {
            var additionalInformationsToDelete = this.ProjectAdditionalInformations.Where(db => newAdditionalInformations?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var additionalInformationToDelete in additionalInformationsToDelete)
            {
                additionalInformationToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the project additional information.</summary>
        /// <param name="additionalInformation">The additional information.</param>
        private void CreateProjectAdditionalInformation(ProjectAdditionalInformation additionalInformation)
        {
            this.ProjectAdditionalInformations.Add(additionalInformation);
        }

        #endregion

        #region Interests

        /// <summary>
        /// Updates the project interests.
        /// </summary>
        /// <param name="projectInterests">The project interests.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void UpdateProjectInterests(
            List<ProjectInterest> projectInterests,
            int userId,
            bool isAdmin)
        {
            this.SynchronizeProjectInterests(projectInterests, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;

            this.IsAdmin = isAdmin;
        }

        /// <summary>Synchronizes the project interests.</summary>
        /// <param name="projectInterests">The project interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectInterests(List<ProjectInterest> projectInterests, int userId)
        {
            if (this.ProjectInterests == null)
            {
                this.ProjectInterests = new List<ProjectInterest>();
            }

            this.DeleteProjectInterests(projectInterests, userId);

            if (projectInterests?.Any() != true)
            {
                return;
            }

            // Create or update interests
            foreach (var projectInterest in projectInterests)
            {
                var interestDb = this.ProjectInterests.FirstOrDefault(a => a.Interest.Uid == projectInterest.Interest.Uid);
                if (interestDb != null)
                {
                    interestDb.Update(projectInterest, userId);
                }
                else
                {
                    this.ProjectInterests.Add(projectInterest);
                }
            }
        }

        /// <summary>Deletes the project interests.</summary>
        /// <param name="newProjectInterests">The new project interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteProjectInterests(List<ProjectInterest> newProjectInterests, int userId)
        {
            var projectInterestsToDelete = this.ProjectInterests.Where(db => newProjectInterests?.Select(a => a.Interest.Uid)?.Contains(db.Interest.Uid) == false && !db.IsDeleted).ToList();
            foreach (var projectInterestToDelete in projectInterestsToDelete)
            {
                projectInterestToDelete.Delete(userId);
            }
        }

        #endregion

        #region Target Audiences

        /// <summary>
        /// Updates the project target audiences.
        /// </summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void UpdateProjectTargetAudiences(
            List<TargetAudience> targetAudiences,
            int userId,
            bool isAdmin)
        {
            this.SynchronizeProjectTargetAudiences(targetAudiences, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;

            this.IsAdmin = isAdmin;
        }

        /// <summary>Synchronizes the project target audiences.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        {
            if (this.ProjectTargetAudiences == null)
            {
                this.ProjectTargetAudiences = new List<ProjectTargetAudience>();
            }

            this.DeleteProjectTargetAudiences(targetAudiences, userId);

            if (targetAudiences?.Any() != true)
            {
                return;
            }

            // Create or update target audiences
            foreach (var targetAudience in targetAudiences)
            {
                var targetAudienceDb = this.ProjectTargetAudiences.FirstOrDefault(a => a.TargetAudience.Uid == targetAudience.Uid);
                if (targetAudienceDb != null)
                {
                    targetAudienceDb.Update(userId);
                }
                else
                {
                    this.CreateProjectTargetAudience(targetAudience, userId);
                }
            }
        }

        /// <summary>Deletes the project target audiences.</summary>
        /// <param name="newTargetAudiences">The new target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteProjectTargetAudiences(List<TargetAudience> newTargetAudiences, int userId)
        {
            var targetAudiencesToDelete = this.ProjectTargetAudiences.Where(db => newTargetAudiences?.Select(a => a.Uid)?.Contains(db.TargetAudience.Uid) == false && !db.IsDeleted).ToList();
            foreach (var targetAudienceToDelete in targetAudiencesToDelete)
            {
                targetAudienceToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the project target audience.</summary>
        /// <param name="targetAudience">The target audience.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateProjectTargetAudience(TargetAudience targetAudience, int userId)
        {
            this.ProjectTargetAudiences.Add(new ProjectTargetAudience(this, targetAudience, userId));
        }

        #endregion

        #region Image Links

        /// <summary>Synchronizes the project image links.</summary>
        /// <param name="imageLink">The image link.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectImageLinks(string imageLink, int userId)
        {
            if (this.ProjectImageLinks == null)
            {
                this.ProjectImageLinks = new List<ProjectImageLink>();
            }

            var imageLinkDb = this.ProjectImageLinks?.FirstOrDefault();
            if (!string.IsNullOrEmpty(imageLink))
            {
                if (imageLinkDb != null)
                {
                    imageLinkDb.Update(imageLink, userId);
                }
                else
                {
                    this.ProjectImageLinks.Add(new ProjectImageLink(imageLink, userId));
                }
            }
            else
            {
                imageLinkDb?.Delete(userId);
            }
        }

        #endregion

        #region Teaser Links

        /// <summary>Synchronizes the project teaser links.</summary>
        /// <param name="teaserLink">The teaser link.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectTeaserLinks(string teaserLink, int userId)
        {
            if (this.ProjectTeaserLinks == null)
            {
                this.ProjectTeaserLinks = new List<ProjectTeaserLink>();
            }

            var teaserLinkDb = this.ProjectTeaserLinks?.FirstOrDefault();
            if (!string.IsNullOrEmpty(teaserLink))
            {
                if (teaserLinkDb != null)
                {
                    teaserLinkDb.Update(teaserLink, userId);
                }
                else
                {
                    this.ProjectTeaserLinks.Add(new ProjectTeaserLink(teaserLink, userId));
                }
            }
            else
            {
                teaserLinkDb?.Delete(userId);
            }
        }

        #endregion

        #region Commission Evaluations

        /// <summary>
        /// Evaluates the specified evaluator user.
        /// </summary>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="grade">The grade.</param>
        public void AudiovisualComissionEvaluateProject(User evaluatorUser, decimal grade, bool isAdmin)
        {
            if (this.CommissionEvaluations == null)
                this.CommissionEvaluations = new List<CommissionEvaluation>();

            var existentCommissionEvaluation = this.GetCommissionEvaluationByEvaluatorId(evaluatorUser.Id);
            if (existentCommissionEvaluation != null)
            {
                existentCommissionEvaluation.Update(grade, evaluatorUser.Id);
            }
            else
            {
                this.CommissionEvaluations.Add(new CommissionEvaluation(
                    this,
                    evaluatorUser,
                    grade,
                    evaluatorUser.Id));
            }

            this.CommissionGrade = this.GetAverageEvaluation(this.SellerAttendeeOrganization.Edition);
            this.CommissionEvaluationsCount = this.GetCommissionEvaluationsTotalCount();
            this.LastCommissionEvaluationDate = DateTime.UtcNow;
            this.IsAdmin = isAdmin;
        }

        /// <summary>
        /// Recalculates the grade.
        /// </summary>
        public void RecalculateGrade(Edition edition)
        {
            this.CommissionGrade = this.GetAverageEvaluation(edition);
        }

        /// <summary>
        /// Gets the average evaluation.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private decimal? GetAverageEvaluation(Edition edition)
        {
            if (this.FindAllCommissionEvaluationsNotDeleted()?.Any() != true)
            {
                return null;
            }

            // Can only generate the 'AverageEvaluation' when the 'CommissionEvaluations' count 
            // is greater or equal than minimum necessary evaluations quantity
            if (this.GetCommissionEvaluationsTotalCount() >= edition.AudiovisualCommissionMinimumEvaluationsCount)
            {
                return this.FindAllCommissionEvaluationsNotDeleted().Sum(e => e.Grade) / this.FindAllCommissionEvaluationsNotDeleted().Count;
            }

            return null;
        }

        /// <summary>
        /// Gets the commission evaluation by evaluator identifier.
        /// </summary>
        /// <param name="evaluatorUserId">The evaluator user identifier.</param>
        /// <returns></returns>
        private CommissionEvaluation GetCommissionEvaluationByEvaluatorId(int evaluatorUserId)
        {
            return this.FindAllCommissionEvaluationsNotDeleted().FirstOrDefault(ce =>
                ce.Project.SellerAttendeeOrganization.EditionId == this.SellerAttendeeOrganization.EditionId &&
                ce.EvaluatorUserId == evaluatorUserId);
        }

        /// <summary>
        /// Gets the commission evaluations total count.
        /// </summary>
        /// <returns></returns>
        private int GetCommissionEvaluationsTotalCount()
        {
            return this.FindAllCommissionEvaluationsNotDeleted()
                .Count(ce => (ce.Project.SellerAttendeeOrganization.EditionId == this.SellerAttendeeOrganization.EditionId));
        }

        /// <summary>
        /// Finds all commission evaluations not deleted.
        /// </summary>
        /// <returns></returns>
        private List<CommissionEvaluation> FindAllCommissionEvaluationsNotDeleted()
        {
            return this.CommissionEvaluations?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Validations

        /// <summary>Gets the maximum sell projects count.</summary>
        /// <returns></returns>
        public int GetMaxSellProjectsCount()
        {
           return this.SellerAttendeeOrganization
                ?.Edition
                ?.AttendeeOrganizationMaxSellProjectsCount ?? 0;
        }

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
            this.ValidateProjectTitles();
            this.ValidateProjectLogLines();
            this.ValidateProjectSummaries();
            this.ValidateProjectProductionPlans();
            this.ValidateProjectAdditionalInformations();
            this.ValidateProjectImageLinks();
            this.ValidateProjectTeaserLinks();
            this.ValidateProjectBuyerEvaluations();
            this.ValidateCommissionEvaluations();

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
            this.ValidateProjectTitles();
            this.ValidateProjectLogLines();
            this.ValidateProjectSummaries();
            this.ValidateProjectProductionPlans();
            this.ValidateProjectAdditionalInformations();
            this.ValidateProjectImageLinks();
            this.ValidateProjectTeaserLinks();
            this.ValidateProjectInterests();
            this.ValidateProjectBuyerEvaluations();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Determines whether [is finish valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is finish valid]; otherwise, <c>false</c>.</returns>
        public bool IsFinishValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateProjectBuyerEvaluations();
            this.ValidateRequiredProjectBuyerEvaluations();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Determines whether [is update business round valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is update business round valid]; otherwise, <c>false</c>.</returns>
        public bool IsUpdateBusinessRoundValid(int sellProjectsCount)
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }
            this.ValidateBusinessRoundLimits(sellProjectsCount);
            return this.ValidationResult.IsValid;
        }

        /// <summary>Determines whether [is update pitching valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is update pitching valid]; otherwise, <c>false</c>.</returns>
        public bool IsUpdatePitchingValid(int sellProjectsCount)
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }
            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the business rounds limits.</summary>
        public void ValidateBusinessRoundLimits(int sellProjectsCount)
        {
            if (sellProjectsCount > this.GetMaxSellProjectsCount())
            {
                this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreateBusinessRoundProjectLimit, new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the pitching projects limits.</summary>
        public void ValidatePitchingLimits(int sellProjectsCount)
        {
            if (sellProjectsCount > this.GetMaxSellProjectsCount())
            {
                this.ValidationResult.Add(new ValidationError(Messages.IsNotPossibleCreatePitchingProjectLimit, new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the is finished.</summary>
        public void ValidateIsFinished()
        {
            if (!this.IsAdmin && this.IsFinished())
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

        /// <summary>Validates the project titles.</summary>
        public void ValidateProjectTitles()
        {
            if (this.ProjectTitles?.Any() != true)
            {
                return;
            }

            foreach (var projectTitle in this.ProjectTitles?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectTitle.ValidationResult);
            }
        }

        /// <summary>Validates the project log lines.</summary>
        public void ValidateProjectLogLines()
        {
            if (this.ProjectLogLines?.Any() != true)
            {
                return;
            }

            foreach (var projectLogLine in this.ProjectLogLines?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectLogLine.ValidationResult);
            }
        }

        /// <summary>Validates the project summaries.</summary>
        public void ValidateProjectSummaries()
        {
            if (this.ProjectSummaries?.Any() != true)
            {
                return;
            }

            foreach (var projectSummary in this.ProjectSummaries?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectSummary.ValidationResult);
            }
        }

        /// <summary>Validates the project production plans.</summary>
        public void ValidateProjectProductionPlans()
        {
            if (this.ProjectProductionPlans?.Any() != true)
            {
                return;
            }

            foreach (var projectProductionPlan in this.ProjectProductionPlans?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectProductionPlan.ValidationResult);
            }
        }

        /// <summary>Validates the project additional informations.</summary>
        public void ValidateProjectAdditionalInformations()
        {
            if (this.ProjectAdditionalInformations?.Any() != true)
            {
                return;
            }

            foreach (var projectAdditionalInformation in this.ProjectAdditionalInformations?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectAdditionalInformation.ValidationResult);
            }
        }

        /// <summary>Validates the project image links.</summary>
        public void ValidateProjectImageLinks()
        {
            if (this.ProjectImageLinks?.Any() != true)
            {
                return;
            }

            foreach (var projectImageLink in this.ProjectImageLinks?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectImageLink.ValidationResult);
            }
        }

        /// <summary>Validates the project teaser links.</summary>
        public void ValidateProjectTeaserLinks()
        {
            if (this.ProjectTeaserLinks?.Any() != true)
            {
                return;
            }

            foreach (var projectTeaserLink in this.ProjectTeaserLinks?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectTeaserLink.ValidationResult);
            }
        }

        public void ValidateProjectInterests()
        {
            if (this.ProjectProductionPlans?.Any() != true)
            {
                return;
            }

            foreach (var projectInterest in this.ProjectInterests?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectInterest.ValidationResult);
            }
        }

        /// <summary>Validates the project buyer evaluations.</summary>
        public void ValidateProjectBuyerEvaluations()
        {
            if (this.ProjectBuyerEvaluations?.Any() != true)
            {
                return;
            }

            if (this.ProjectBuyerEvaluationsCount > this.SellerAttendeeOrganization.GetProjectMaxBuyerEvaluationsCount())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.MaxProjectBuyersEvaluationsReached, this.SellerAttendeeOrganization.GetProjectMaxBuyerEvaluationsCount(), Labels.Players), new string[] { "ToastrError" }));
            }

            foreach (var projectBuyerEvaluation in this.ProjectBuyerEvaluations?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectBuyerEvaluation.ValidationResult);
            }
        }

        /// <summary>Validates the required project buyer evaluations.</summary>
        public void ValidateRequiredProjectBuyerEvaluations()
        {
            if (this.ProjectBuyerEvaluationsCount == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheProjectMustHaveOnePlayer, Labels.Player), new string[] { "ToastrError" }));
            }
        }

        /// <summary>
        /// Validates the commission evaluations.
        /// </summary>
        public void ValidateCommissionEvaluations()
        {
            if (this.CommissionEvaluations?.Any() != true)
            {
                return;
            }

            if (!this.IsPitching())
            {
                this.ValidationResult.Add(new ValidationError(Messages.ProjectCannotBeEvaluatedIsNotPitching, new string[] { "ToastrError" }));
            }

            foreach (var commissionEvaluation in this.CommissionEvaluations?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(commissionEvaluation.ValidationResult);
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
        
        public bool IsPitching()
        {
            return new int[] {
                ProjectModality.Both.Id,
                ProjectModality.Pitching.Id
            }.Contains(this.ProjectModalityId);
        }
    }
}

