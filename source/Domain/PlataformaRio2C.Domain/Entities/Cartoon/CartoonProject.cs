// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProject.cs" company="Softo">
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
    /// <summary>CartoonProject</summary>
    public class CartoonProject : Entity
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

        public int SellerAttendeeOrganizationId { get; private set; }
        public string TotalPlayingTime { get; private set; }
        public int? NumberOfEpisodes { get; private set; }
        public string EachEpisodePlayingTime { get; private set; }
        public string ValuePerEpisode { get; private set; }
        public string TotalValueOfProject { get; private set; }
        public string ValueAlreadyRaised { get; private set; }
        public string ValueStillNeeded { get; private set; }
        public bool IsPitching { get; private set; }
        public DateTimeOffset? FinishDate { get; private set; }
        public int ProjectBuyerEvaluationsCount { get; private set; }
        public int CommissionEvaluationsCount { get; private set; }
        public decimal? CommissionGrade { get; private set; }
        public DateTimeOffset? LastCommissionEvaluationDate { get; private set; }

        public virtual AttendeeOrganization SellerAttendeeOrganization { get; private set; }

        public virtual ICollection<CartoonProjectTitle> CartoonProjectTitles { get; private set; }
        public virtual ICollection<CartoonProjectLogLine> CartoonProjectLogLines { get; private set; }
        public virtual ICollection<CartoonProjectSummary> CartoonProjectSummaries { get; private set; }
        public virtual ICollection<CartoonProjectProductionPlan> CartoonProjectProductionPlans { get; private set; }
        public virtual ICollection<CartoonProjectBibleLink> CartoonProjectBibleLinks { get; private set; }
        public virtual ICollection<CartoonProjectTeaserLink> CartoonProjectTeaserLinks { get; private set; }
        public virtual ICollection<ProjectBuyerEvaluation> CartoonProjectBuyerEvaluations { get; private set; }
        public virtual ICollection<CommissionEvaluation> CartoonProjectCommissionEvaluations { get; private set; }

        //public virtual ICollection<CartoonProjectAdditionalInformation> CartoonProjectAdditionalInformations { get; private set; }
        //public virtual ICollection<CartoonProjectInterest> CartoonProjectInterests { get; private set; }

        private bool IsAdmin = false;


        public CartoonProject(
            AttendeeOrganization sellerAttendeeOrganization,
            string totalPlayingTime,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            string valuePerEpisode,
            string totalValueOfProject,
            string valueAlreadyRaised,
            string valueStillNeeded,
            bool isPitching,
            List<CartoonProjectTitle> cartoonProjectTitles,
            List<CartoonProjectLogLine> cartoonProjectLogLines,
            List<CartoonProjectSummary> cartoonProjectSummaries,
            List<CartoonProjectProductionPlan> cartoonProjectProductionPlans,
            //List<CartoonProjectAdditionalInformation> projectAdditionalInformations,
            //List<CartoonProjectInterest> projectInterests,
            string bibleLink,
            string teaserLink,
            int userId)
        {
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

            this.SynchronizeProjectTitles(cartoonProjectTitles, userId);
            this.SynchronizeProjectLogLines(cartoonProjectLogLines, userId);
            this.SynchronizeProjectSummaries(cartoonProjectSummaries, userId);
            this.SynchronizeProjectProductionPlans(cartoonProjectProductionPlans, userId);
            //this.SynchronizeProjectAdditionalInformations(projectAdditionalInformations, userId);
            //this.SynchronizeProjectInterests(projectInterests, userId);
            this.SynchronizeProjectBibleLinks(bibleLink, userId);
            this.SynchronizeProjectTeaserLinks(teaserLink, userId);

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="CartoonProject"/> class.</summary>
        protected CartoonProject()
        {
        }

        public void UpdateMainInformation(
            string totalPlayingTime,
            int? numberOfEpisodes,
            string eachEpisodePlayingTime,
            string valuePerEpisode,
            string totalValueOfProject,
            string valueAlreadyRaised,
            string valueStillNeeded,
            bool isPitching,
            List<CartoonProjectTitle> cartoonProjectTitles,
            List<CartoonProjectLogLine> cartoonProjectLogLines,
            List<CartoonProjectSummary> cartoonProjectSummaries,
            List<CartoonProjectProductionPlan> cartoonProjectProductionPlans,
            //List<CartoonProjectAdditionalInformation> cartoonProjectAdditionalInformations,
            int userId,
            bool isAdmin)
        {
            this.TotalPlayingTime = totalPlayingTime;
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime?.Trim();
            this.ValuePerEpisode = valuePerEpisode?.Trim();
            this.TotalValueOfProject = totalValueOfProject?.Trim();
            this.ValueAlreadyRaised = valueAlreadyRaised?.Trim();
            this.ValueStillNeeded = valueStillNeeded?.Trim();
            this.IsPitching = isPitching;

            this.SynchronizeProjectTitles(cartoonProjectTitles, userId);
            this.SynchronizeProjectLogLines(cartoonProjectLogLines, userId);
            this.SynchronizeProjectSummaries(cartoonProjectSummaries, userId);
            this.SynchronizeProjectProductionPlans(cartoonProjectProductionPlans, userId);
            //this.SynchronizeProjectAdditionalInformations(cartoonProjectAdditionalInformations, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;

            this.IsAdmin = isAdmin;
        }

        /// <summary>Updates the links.</summary>
        /// <param name="imageLink">The image link.</param>
        /// <param name="bibleLink">The bible link.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void UpdateLinks(
            string imageLink,
            string bibleLink,
            int userId,
            bool isAdmin)
        {
            this.SynchronizeProjectBibleLinks(imageLink, userId);
            this.SynchronizeProjectTeaserLinks(bibleLink, userId);

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;

            this.IsAdmin = isAdmin;
        }

        /// <summary>Finishes the project.</summary>
        /// <param name="userId">The user identifier.</param>
        public void FinishProject(int userId)
        {
            this.FinishDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
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
            return this.CartoonProjectTitles?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant())?.Value;
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

            this.IsDeleted = true;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;

            this.IsAdmin = isAdmin;
        }

        #region Buyer Evaluations

        ///// <summary>
        ///// Creates the project buyer evaluation.
        ///// </summary>
        ///// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        ///// <param name="projectEvaluationStatus">The project evaluation status.</param>
        ///// <param name="userId">The user identifier.</param>
        ///// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        //public void CreateProjectBuyerEvaluation(
        //    AttendeeOrganization buyerAttendeeOrganization,
        //    ProjectEvaluationStatus projectEvaluationStatus,
        //    int userId,
        //    bool isAdmin)
        //{
        //    if (this.CartoonProjectBuyerEvaluations == null)
        //    {
        //        this.CartoonProjectBuyerEvaluations = new List<ProjectBuyerEvaluation>();
        //    }

        //    var buyerEvaluation = this.GetProjectBuyerEvaluationByAttendeeOrganizationUid(buyerAttendeeOrganization?.Uid ?? Guid.Empty);
        //    if (buyerEvaluation == null)
        //    {
        //        this.CartoonProjectBuyerEvaluations.Add(new ProjectBuyerEvaluation(this, buyerAttendeeOrganization, projectEvaluationStatus, userId));
        //    }
        //    else if (buyerEvaluation.IsDeleted)
        //    {
        //        buyerEvaluation.Restore(projectEvaluationStatus, userId);
        //    }

        //    this.UpdateProjectBuyerEvaluationCounts();

        //    this.IsDeleted = false;
        //    this.UpdateUserId = userId;
        //    this.UpdateDate = DateTime.UtcNow;

        //    this.IsAdmin = isAdmin;
        //}

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
            return this.CartoonProjectBuyerEvaluations?.Count(be => !be.IsDeleted) ?? 0;
        }

        /// <summary>Accepts the project buyer evaluation.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ProjectBuyerEvaluation AcceptProjectBuyerEvaluation(Guid attendeeOrganizationUid, List<ProjectEvaluationStatus> projectEvaluationStatuses, int userId)
        {
            var buyerEvaluation = this.GetProjectBuyerEvaluationByAttendeeOrganizationUid(attendeeOrganizationUid);
            buyerEvaluation?.Accept(projectEvaluationStatuses, userId);

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
            return this.CartoonProjectBuyerEvaluations?.FirstOrDefault(pbe => pbe.BuyerAttendeeOrganization.Uid == attendeeOrganizationUid);
        }

        /// <summary>
        /// Finds all project buyer evaluations not deleted.
        /// </summary>
        /// <returns></returns>
        private List<ProjectBuyerEvaluation> FindAllProjectBuyerEvaluationsNotDeleted()
        {
            return this.CartoonProjectBuyerEvaluations?.Where(pbe => !pbe.IsDeleted)?.ToList();
        }

        #endregion

        #region Titles

        /// <summary>Synchronizes the project titles.</summary>
        /// <param name="titles">The titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectTitles(List<CartoonProjectTitle> titles, int userId)
        {
            if (this.CartoonProjectTitles == null)
            {
                this.CartoonProjectTitles = new List<CartoonProjectTitle>();
            }

            this.DeleteProjectTitles(titles, userId);

            if (titles?.Any() != true)
            {
                return;
            }

            foreach (var title in titles)
            {
                var titleDb = this.CartoonProjectTitles.FirstOrDefault(d => d.Language.Code == title.Language.Code);
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
        private void DeleteProjectTitles(List<CartoonProjectTitle> newTitles, int userId)
        {
            var titlesToDelete = this.CartoonProjectTitles.Where(db => newTitles?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var titleToDelete in titlesToDelete)
            {
                titleToDelete.Delete(userId);
            }
        }

        private void CreateProjectTitle(CartoonProjectTitle title)
        {
            this.CartoonProjectTitles.Add(title);
        }

        #endregion

        #region LogLines

        /// <summary>Synchronizes the project log lines.</summary>
        /// <param name="logLines">The log lines.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectLogLines(List<CartoonProjectLogLine> logLines, int userId)
        {
            if (this.CartoonProjectLogLines == null)
            {
                this.CartoonProjectLogLines = new List<CartoonProjectLogLine>();
            }

            this.DeleteProjectLogLines(logLines, userId);

            if (logLines?.Any() != true)
            {
                return;
            }

            foreach (var logLine in logLines)
            {
                var logLineDb = this.CartoonProjectLogLines.FirstOrDefault(d => d.Language.Code == logLine.Language.Code);
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

        private void DeleteProjectLogLines(List<CartoonProjectLogLine> newLogLines, int userId)
        {
            var logLinesToDelete = this.CartoonProjectLogLines.Where(db => newLogLines?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var logLineToDelete in logLinesToDelete)
            {
                logLineToDelete.Delete(userId);
            }
        }

        private void CreateProjectLogLines(CartoonProjectLogLine logLine)
        {
            this.CartoonProjectLogLines.Add(logLine);
        }

        #endregion

        #region Summaries

        /// <summary>Synchronizes the project summaries.</summary>
        /// <param name="summaries">The summaries.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectSummaries(List<CartoonProjectSummary> summaries, int userId)
        {
            if (this.CartoonProjectSummaries == null)
            {
                this.CartoonProjectSummaries = new List<CartoonProjectSummary>();
            }

            this.DeleteProjectSummaries(summaries, userId);

            if (summaries?.Any() != true)
            {
                return;
            }

            foreach (var summary in summaries)
            {
                var summaryDb = this.CartoonProjectSummaries.FirstOrDefault(d => d.Language.Code == summary.Language.Code);
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
        private void DeleteProjectSummaries(List<CartoonProjectSummary> newSummaries, int userId)
        {
            var summariesToDelete = this.CartoonProjectSummaries.Where(db => newSummaries?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var summaryToDelete in summariesToDelete)
            {
                summaryToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the project summary.</summary>
        /// <param name="sumary">The sumary.</param>
        private void CreateProjectSummary(CartoonProjectSummary sumary)
        {
            this.CartoonProjectSummaries.Add(sumary);
        }

        #endregion

        #region Production Plans

        /// <summary>Synchronizes the project production plans.</summary>
        /// <param name="productionPlans">The production plans.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectProductionPlans(List<CartoonProjectProductionPlan> productionPlans, int userId)
        {
            if (this.CartoonProjectProductionPlans == null)
            {
                this.CartoonProjectProductionPlans = new List<CartoonProjectProductionPlan>();
            }

            this.DeleteProjectProductionPlans(productionPlans, userId);

            if (productionPlans?.Any() != true)
            {
                return;
            }

            foreach (var productionPlan in productionPlans)
            {
                var productionPlanDb = this.CartoonProjectProductionPlans.FirstOrDefault(d => d.Language.Code == productionPlan.Language.Code);
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
        private void DeleteProjectProductionPlans(List<CartoonProjectProductionPlan> newProductionPlans, int userId)
        {
            var productionPlansToDelete = this.CartoonProjectProductionPlans.Where(db => newProductionPlans?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
            foreach (var productionPlanToDelete in productionPlansToDelete)
            {
                productionPlanToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the project production plan.</summary>
        /// <param name="productionPlan">The production plan.</param>
        private void CreateProjectProductionPlan(CartoonProjectProductionPlan productionPlan)
        {
            this.CartoonProjectProductionPlans.Add(productionPlan);
        }

        #endregion

        #region Additional Informations (Disabled)

        ///// <summary>Synchronizes the project additional informations.</summary>
        ///// <param name="additionalInformations">The additional informations.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void SynchronizeProjectAdditionalInformations(List<ProjectAdditionalInformation> additionalInformations, int userId)
        //{
        //    if (this.ProjectAdditionalInformations == null)
        //    {
        //        this.ProjectAdditionalInformations = new List<ProjectAdditionalInformation>();
        //    }

        //    this.DeleteProjectAdditionalInformations(additionalInformations, userId);

        //    if (additionalInformations?.Any() != true)
        //    {
        //        return;
        //    }

        //    foreach (var additionalInformation in additionalInformations)
        //    {
        //        var additionalInformationDb = this.ProjectAdditionalInformations.FirstOrDefault(d => d.Language.Code == additionalInformation.Language.Code);
        //        if (additionalInformationDb != null)
        //        {
        //            additionalInformationDb.Update(additionalInformation);
        //        }
        //        else
        //        {
        //            this.CreateProjectAdditionalInformation(additionalInformation);
        //        }
        //    }
        //}

        ///// <summary>Deletes the project additional informations.</summary>
        ///// <param name="newAdditionalInformations">The new additional informations.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void DeleteProjectAdditionalInformations(List<ProjectAdditionalInformation> newAdditionalInformations, int userId)
        //{
        //    var additionalInformationsToDelete = this.ProjectAdditionalInformations.Where(db => newAdditionalInformations?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
        //    foreach (var additionalInformationToDelete in additionalInformationsToDelete)
        //    {
        //        additionalInformationToDelete.Delete(userId);
        //    }
        //}

        ///// <summary>Creates the project additional information.</summary>
        ///// <param name="additionalInformation">The additional information.</param>
        //private void CreateProjectAdditionalInformation(ProjectAdditionalInformation additionalInformation)
        //{
        //    this.ProjectAdditionalInformations.Add(additionalInformation);
        //}

        #endregion

        #region Interests (Disabled)

        ///// <summary>
        ///// Updates the project interests.
        ///// </summary>
        ///// <param name="projectInterests">The project interests.</param>
        ///// <param name="userId">The user identifier.</param>
        ///// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        //public void UpdateProjectInterests(
        //    List<ProjectInterest> projectInterests,
        //    int userId,
        //    bool isAdmin)
        //{
        //    this.SynchronizeProjectInterests(projectInterests, userId);

        //    this.IsDeleted = false;
        //    this.UpdateUserId = userId;
        //    this.UpdateDate = DateTime.UtcNow;

        //    this.IsAdmin = isAdmin;
        //}

        ///// <summary>Synchronizes the project interests.</summary>
        ///// <param name="projectInterests">The project interests.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void SynchronizeProjectInterests(List<ProjectInterest> projectInterests, int userId)
        //{
        //    if (this.ProjectInterests == null)
        //    {
        //        this.ProjectInterests = new List<ProjectInterest>();
        //    }

        //    this.DeleteProjectInterests(projectInterests, userId);

        //    if (projectInterests?.Any() != true)
        //    {
        //        return;
        //    }

        //    // Create or update interests
        //    foreach (var projectInterest in projectInterests)
        //    {
        //        var interestDb = this.ProjectInterests.FirstOrDefault(a => a.Interest.Uid == projectInterest.Interest.Uid);
        //        if (interestDb != null)
        //        {
        //            interestDb.Update(projectInterest, userId);
        //        }
        //        else
        //        {
        //            this.ProjectInterests.Add(projectInterest);
        //        }
        //    }
        //}

        ///// <summary>Deletes the project interests.</summary>
        ///// <param name="newProjectInterests">The new project interests.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void DeleteProjectInterests(List<ProjectInterest> newProjectInterests, int userId)
        //{
        //    var projectInterestsToDelete = this.ProjectInterests.Where(db => newProjectInterests?.Select(a => a.Interest.Uid)?.Contains(db.Interest.Uid) == false && !db.IsDeleted).ToList();
        //    foreach (var projectInterestToDelete in projectInterestsToDelete)
        //    {
        //        projectInterestToDelete.Delete(userId);
        //    }
        //}

        #endregion

        #region Target Audiences (Disabled)

        ///// <summary>
        ///// Updates the project target audiences.
        ///// </summary>
        ///// <param name="targetAudiences">The target audiences.</param>
        ///// <param name="userId">The user identifier.</param>
        ///// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        //public void UpdateProjectTargetAudiences(
        //    List<TargetAudience> targetAudiences,
        //    int userId,
        //    bool isAdmin)
        //{
        //    this.SynchronizeProjectTargetAudiences(targetAudiences, userId);

        //    this.IsDeleted = false;
        //    this.UpdateUserId = userId;
        //    this.UpdateDate = DateTime.UtcNow;

        //    this.IsAdmin = isAdmin;
        //}

        ///// <summary>Synchronizes the project target audiences.</summary>
        ///// <param name="targetAudiences">The target audiences.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void SynchronizeProjectTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        //{
        //    if (this.ProjectTargetAudiences == null)
        //    {
        //        this.ProjectTargetAudiences = new List<ProjectTargetAudience>();
        //    }

        //    this.DeleteProjectTargetAudiences(targetAudiences, userId);

        //    if (targetAudiences?.Any() != true)
        //    {
        //        return;
        //    }

        //    // Create or update target audiences
        //    foreach (var targetAudience in targetAudiences)
        //    {
        //        var targetAudienceDb = this.ProjectTargetAudiences.FirstOrDefault(a => a.TargetAudience.Uid == targetAudience.Uid);
        //        if (targetAudienceDb != null)
        //        {
        //            targetAudienceDb.Update(userId);
        //        }
        //        else
        //        {
        //            this.CreateProjectTargetAudience(targetAudience, userId);
        //        }
        //    }
        //}

        ///// <summary>Deletes the project target audiences.</summary>
        ///// <param name="newTargetAudiences">The new target audiences.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void DeleteProjectTargetAudiences(List<TargetAudience> newTargetAudiences, int userId)
        //{
        //    var targetAudiencesToDelete = this.ProjectTargetAudiences.Where(db => newTargetAudiences?.Select(a => a.Uid)?.Contains(db.TargetAudience.Uid) == false && !db.IsDeleted).ToList();
        //    foreach (var targetAudienceToDelete in targetAudiencesToDelete)
        //    {
        //        targetAudienceToDelete.Delete(userId);
        //    }
        //}

        ///// <summary>Creates the project target audience.</summary>
        ///// <param name="targetAudience">The target audience.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void CreateProjectTargetAudience(TargetAudience targetAudience, int userId)
        //{
        //    this.ProjectTargetAudiences.Add(new ProjectTargetAudience(this, targetAudience, userId));
        //}

        #endregion

        #region Bible Links

        /// <summary>Synchronizes the project image links.</summary>
        /// <param name="imageLink">The image link.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeProjectBibleLinks(string imageLink, int userId)
        {
            if (this.CartoonProjectBibleLinks == null)
            {
                this.CartoonProjectBibleLinks = new List<CartoonProjectBibleLink>();
            }

            var imageLinkDb = this.CartoonProjectBibleLinks?.FirstOrDefault();
            if (!string.IsNullOrEmpty(imageLink))
            {
                if (imageLinkDb != null)
                {
                    imageLinkDb.Update(imageLink, userId);
                }
                else
                {
                    this.CartoonProjectBibleLinks.Add(new CartoonProjectBibleLink(imageLink, userId));
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
            if (this.CartoonProjectTeaserLinks == null)
            {
                this.CartoonProjectTeaserLinks = new List<CartoonProjectTeaserLink>();
            }

            var teaserLinkDb = this.CartoonProjectTeaserLinks?.FirstOrDefault();
            if (!string.IsNullOrEmpty(teaserLink))
            {
                if (teaserLinkDb != null)
                {
                    teaserLinkDb.Update(teaserLink, userId);
                }
                else
                {
                    this.CartoonProjectTeaserLinks.Add(new CartoonProjectTeaserLink(teaserLink, userId));
                }
            }
            else
            {
                teaserLinkDb?.Delete(userId);
            }
        }

        #endregion

        #region Commission Evaluations

        ///// <summary>
        ///// Evaluates the specified evaluator user.
        ///// </summary>
        ///// <param name="evaluatorUser">The evaluator user.</param>
        ///// <param name="grade">The grade.</param>
        //public void AudiovisualComissionEvaluateProject(User evaluatorUser, decimal grade, bool isAdmin)
        //{
        //    if (this.CartoonProjectCommissionEvaluations == null)
        //        this.CartoonProjectCommissionEvaluations = new List<CommissionEvaluation>();

        //    var existentCommissionEvaluation = this.GetCommissionEvaluationByEvaluatorId(evaluatorUser.Id);
        //    if (existentCommissionEvaluation != null)
        //    {
        //        existentCommissionEvaluation.Update(grade, evaluatorUser.Id);
        //    }
        //    else
        //    {
        //        this.CartoonProjectCommissionEvaluations.Add(new CommissionEvaluation(
        //            this,
        //            evaluatorUser,
        //            grade,
        //            evaluatorUser.Id));
        //    }

        //    this.CommissionGrade = this.GetAverageEvaluation(this.SellerAttendeeOrganization.Edition);
        //    this.CommissionEvaluationsCount = this.GetCommissionEvaluationsTotalCount();
        //    this.LastCommissionEvaluationDate = DateTime.UtcNow;
        //    this.IsAdmin = isAdmin;
        //}

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
            return this.CartoonProjectCommissionEvaluations?.Where(aoc => !aoc.IsDeleted)?.ToList();
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
            if (this.CartoonProjectTitles?.Any() != true)
            {
                return;
            }

            foreach (var projectTitle in this.CartoonProjectTitles?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectTitle.ValidationResult);
            }
        }

        /// <summary>Validates the project log lines.</summary>
        public void ValidateProjectLogLines()
        {
            if (this.CartoonProjectLogLines?.Any() != true)
            {
                return;
            }

            foreach (var projectLogLine in this.CartoonProjectLogLines?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectLogLine.ValidationResult);
            }
        }

        /// <summary>Validates the project summaries.</summary>
        public void ValidateProjectSummaries()
        {
            if (this.CartoonProjectSummaries?.Any() != true)
            {
                return;
            }

            foreach (var projectSummary in this.CartoonProjectSummaries?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectSummary.ValidationResult);
            }
        }

        /// <summary>Validates the project production plans.</summary>
        public void ValidateProjectProductionPlans()
        {
            if (this.CartoonProjectProductionPlans?.Any() != true)
            {
                return;
            }

            foreach (var projectProductionPlan in this.CartoonProjectProductionPlans?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectProductionPlan.ValidationResult);
            }
        }

        /// <summary>Validates the project additional informations.</summary>
        public void ValidateProjectAdditionalInformations()
        {
            //if (this.ProjectAdditionalInformations?.Any() != true)
            //{
            //    return;
            //}

            //foreach (var projectAdditionalInformation in this.ProjectAdditionalInformations?.Where(t => !t.IsValid())?.ToList())
            //{
            //    this.ValidationResult.Add(projectAdditionalInformation.ValidationResult);
            //}
        }

        /// <summary>Validates the project image links.</summary>
        public void ValidateProjectImageLinks()
        {
            if (this.CartoonProjectBibleLinks?.Any() != true)
            {
                return;
            }

            foreach (var projectImageLink in this.CartoonProjectBibleLinks?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectImageLink.ValidationResult);
            }
        }

        /// <summary>Validates the project teaser links.</summary>
        public void ValidateProjectTeaserLinks()
        {
            if (this.CartoonProjectTeaserLinks?.Any() != true)
            {
                return;
            }

            foreach (var projectTeaserLink in this.CartoonProjectTeaserLinks?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(projectTeaserLink.ValidationResult);
            }
        }

        public void ValidateProjectInterests()
        {
            //if (this.ProjectProductionPlans?.Any() != true)
            //{
            //    return;
            //}

            //foreach (var projectInterest in this.ProjectInterests?.Where(t => !t.IsValid())?.ToList())
            //{
            //    this.ValidationResult.Add(projectInterest.ValidationResult);
            //}
        }

        /// <summary>Validates the project buyer evaluations.</summary>
        public void ValidateProjectBuyerEvaluations()
        {
            if (this.CartoonProjectBuyerEvaluations?.Any() != true)
            {
                return;
            }

            if (this.ProjectBuyerEvaluationsCount > this.SellerAttendeeOrganization.GetProjectMaxBuyerEvaluationsCount())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.MaxProjectBuyersEvaluationsReached, this.SellerAttendeeOrganization.GetProjectMaxBuyerEvaluationsCount(), Labels.Players), new string[] { "ToastrError" }));
            }

            foreach (var projectBuyerEvaluation in this.CartoonProjectBuyerEvaluations?.Where(t => !t.IsValid())?.ToList())
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
            if (this.CartoonProjectCommissionEvaluations?.Any() != true)
            {
                return;
            }

            if (!this.IsPitching)
            {
                this.ValidationResult.Add(new ValidationError(Messages.ProjectCannotBeEvaluatedIsNotPitching, new string[] { "ToastrError" }));
            }

            foreach (var commissionEvaluation in this.CartoonProjectCommissionEvaluations?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(commissionEvaluation.ValidationResult);
            }
        }

        #endregion
    }
}

