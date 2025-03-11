// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-18-2025
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 01-31-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjects.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicBusinessRoundProject : Entity
    {
        public static readonly int PlayerCategoriesThatHaveOrHadContractMaxLength = 300;
        public static readonly int AttachmentUrlMaxLength = 300;

        public int SellerAttendeeCollaboratorId { get; private set; }
        public string PlayerCategoriesThatHaveOrHadContract { get; private set; }
        public string AttachmentUrl { get; private set; }
        public DateTimeOffset? FinishDate { get; private set; }
        public int ProjectBuyerEvaluationsCount { get; private set; }

        [NotMapped]
        private bool IsAdmin = false;

        public virtual AttendeeCollaborator SellerAttendeeCollaborator { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectTargetAudience> MusicBusinessRoundProjectTargetAudiences { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectInterest> MusicBusinessRoundProjectInterests { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectPlayerCategory> MusicBusinessRoundProjectPlayerCategories { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectActivity> MusicBusinessRoundProjectActivities { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectExpectationsForMeeting> MusicBusinessRoundProjectExpectationsForMeetings { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectBuyerEvaluation> MusicBusinessRoundProjectBuyerEvaluations { get; private set; }
        public object SellerAttendeeOrganization { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProject"/> class.
        /// </summary>
        /// <param name="sellerAttendeeCollaboratorId">The seller attendee collaborator identifier.</param>
        /// <param name="playerCategoriesThatHaveOrHadContract">The player categories that have or had contract.</param>
        /// <param name="attachmentUrl">The attachment URL.</param>
        /// <param name="finishDate">The finish date.</param>
        /// <param name="projectBuyerEvaluationsCount">The project buyer evaluations count.</param>
        /// <param name="sellerAttendeeCollaborator">The seller attendee collaborator.</param>
        /// <param name="musicBusinessRoundProjectTargetAudience">The music business round project target audience.</param>
        /// <param name="musicBusinessRoundProjectInterests">The music business round project interests.</param>
        /// <param name="playerCategories">The player categories.</param>
        /// <param name="musicBusinessRoundProjectActivities">The music business round project activities.</param>
        /// <param name="musicBusinessRoundProjectExpectationsForMeetings">The music business round project expectations for meetings.</param>
        /// <param name="musicBusinessRoundProjectBuyerEvaluations">The music business round project buyer evaluations.</param>
        public MusicBusinessRoundProject(
            int sellerAttendeeCollaboratorId,
            string playerCategoriesThatHaveOrHadContract,
            string attachmentUrl,
            DateTimeOffset? finishDate,
            int projectBuyerEvaluationsCount,
            AttendeeCollaborator sellerAttendeeCollaborator,
            ICollection<MusicBusinessRoundProjectTargetAudience> musicBusinessRoundProjectTargetAudience,
            ICollection<MusicBusinessRoundProjectInterest> musicBusinessRoundProjectInterests,
            ICollection<MusicBusinessRoundProjectPlayerCategory> playerCategories,
            ICollection<MusicBusinessRoundProjectActivity> musicBusinessRoundProjectActivities,
            ICollection<MusicBusinessRoundProjectExpectationsForMeeting> musicBusinessRoundProjectExpectationsForMeetings,
            ICollection<MusicBusinessRoundProjectBuyerEvaluation> musicBusinessRoundProjectBuyerEvaluations)
        {
            this.SellerAttendeeCollaboratorId = sellerAttendeeCollaboratorId;
            this.PlayerCategoriesThatHaveOrHadContract = playerCategoriesThatHaveOrHadContract;
            this.AttachmentUrl = attachmentUrl;
            this.FinishDate = finishDate;
            this.ProjectBuyerEvaluationsCount = projectBuyerEvaluationsCount;
            this.SellerAttendeeCollaborator = sellerAttendeeCollaborator;
            this.MusicBusinessRoundProjectTargetAudiences = musicBusinessRoundProjectTargetAudience ?? new List<MusicBusinessRoundProjectTargetAudience>();
            this.MusicBusinessRoundProjectInterests = musicBusinessRoundProjectInterests ?? new List<MusicBusinessRoundProjectInterest>();
            this.MusicBusinessRoundProjectPlayerCategories = playerCategories ?? new List<MusicBusinessRoundProjectPlayerCategory>();
            this.MusicBusinessRoundProjectActivities = musicBusinessRoundProjectActivities ?? new List<MusicBusinessRoundProjectActivity>();
            this.MusicBusinessRoundProjectExpectationsForMeetings = musicBusinessRoundProjectExpectationsForMeetings ?? new List<MusicBusinessRoundProjectExpectationsForMeeting>();
            this.MusicBusinessRoundProjectBuyerEvaluations = musicBusinessRoundProjectBuyerEvaluations ?? new List<MusicBusinessRoundProjectBuyerEvaluation>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProject"/> class.
        /// </summary>
        /// <param name="sellerAttendeeCollaboratorId">The seller attendee collaborator identifier.</param>
        /// <param name="playerCategoriesThatHaveOrHadContract">The player categories that have or had contract.</param>
        /// <param name="attachmentUrl">The attachment URL.</param>
        /// <param name="finishDate">The finish date.</param>
        /// <param name="musicBusinessRoundProjectTargetAudience">The music business round project target audience.</param>
        /// <param name="musicBusinessRoundProjectInterests">The music business round project interests.</param>
        /// <param name="playerCategories">The player categories.</param>
        /// <param name="musicBusinessRoundProjectActivities">The music business round project activities.</param>
        /// <param name="musicBusinessRoundProjectExpectationsForMeetings">The music business round project expectations for meetings.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBusinessRoundProject(
            int sellerAttendeeCollaboratorId,
            string playerCategoriesThatHaveOrHadContract,
            string attachmentUrl,
            DateTimeOffset? finishDate,
            ICollection<TargetAudience> musicBusinessRoundProjectTargetAudience,
            ICollection<MusicBusinessRoundProjectInterest> musicBusinessRoundProjectInterests,
            ICollection<PlayerCategory> playerCategories,
            ICollection<Activity> musicBusinessRoundProjectActivities,
            ICollection<MusicBusinessRoundProjectExpectationsForMeeting> musicBusinessRoundProjectExpectationsForMeetings,
            int userId)
        {
            this.SellerAttendeeCollaboratorId = sellerAttendeeCollaboratorId;
            this.PlayerCategoriesThatHaveOrHadContract = playerCategoriesThatHaveOrHadContract;
            this.AttachmentUrl = attachmentUrl;
            this.FinishDate = finishDate;

            // casting TargetAudience into MusicBusinessRoundProjectTargetAudience
            this.MusicBusinessRoundProjectTargetAudiences = musicBusinessRoundProjectTargetAudience?
                .Select(targetAudience => new MusicBusinessRoundProjectTargetAudience(
                    this.Id,              
                    targetAudience,
                    userId,
                    string.Empty          
                )).ToList() ?? new List<MusicBusinessRoundProjectTargetAudience>();

            // casting playerCategories into MusicBusinessRoundProjectPlayerCategory
            this.MusicBusinessRoundProjectPlayerCategories = playerCategories?
              .Select(playerCategory => new MusicBusinessRoundProjectPlayerCategory(
                  this.Id,             
                  playerCategory,
                  string.Empty,
                  userId
              )).ToList() ?? new List<MusicBusinessRoundProjectPlayerCategory>();

            // casting Activity into MusicBusinessRoundProjectActivity
            this.MusicBusinessRoundProjectActivities = musicBusinessRoundProjectActivities?
                .Select(activity => new MusicBusinessRoundProjectActivity(
                    activity,          
                    string.Empty,      
                    userId              
                )).ToList() ?? new List<MusicBusinessRoundProjectActivity>();

            this.MusicBusinessRoundProjectInterests = musicBusinessRoundProjectInterests ?? new List<MusicBusinessRoundProjectInterest>();
            this.MusicBusinessRoundProjectExpectationsForMeetings = musicBusinessRoundProjectExpectationsForMeetings ?? new List<MusicBusinessRoundProjectExpectationsForMeeting>();
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProject" /> class.
        /// </summary>
        /// <param name="musicProjectId">The music project identifier.</param>
        public MusicBusinessRoundProject(int musicProjectId)
        {
            this.Id = musicProjectId;
        }

        public MusicBusinessRoundProject()
        {
        }

        /// <summary>
        /// Determines whether this instance is finished.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is finished; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFinished()
        {
            return this.FinishDate.HasValue;
        }

        /// <summary>
        /// Updates the main information.
        /// </summary>
        /// <param name="expectationsForMeeting">The expectations for meeting.</param>
        /// <param name="attachmentUrl">The attachment URL.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            List<MusicBusinessRoundProjectExpectationsForMeeting> expectationsForMeeting,
            string attachmentUrl,
            int userId)
        {
            this.AttachmentUrl = attachmentUrl?.Trim();
            this.SynchronizeExpectationsForMeeting(expectationsForMeeting, userId);
        }

        public void UpdatePlayerCategoriesThatHaveOrHadContract(string value)
        {
            this.PlayerCategoriesThatHaveOrHadContract = value;
        }

        #region MusicBusinessRoundProjectTargetAudience

        /// <summary>Synchronizes the target audiences.</summary>
        /// <param name="targetAudiences">The music business project target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        {
            if (this.MusicBusinessRoundProjectTargetAudiences == null)
            {
                this.MusicBusinessRoundProjectTargetAudiences = new List<MusicBusinessRoundProjectTargetAudience>();
            }

            this.DeleteTargetAudiences(targetAudiences, userId);

            if (targetAudiences?.Any() != true)
            {
                return;
            }

            // Create or update target audiences
            foreach (var targetAudience in targetAudiences)
            {
                var targetAudienceDb = this.MusicBusinessRoundProjectTargetAudiences.FirstOrDefault(a => a.TargetAudience.Uid == targetAudience.Uid);
                if (targetAudienceDb != null)
                {
                    targetAudienceDb.Update(userId);
                }
                else
                {
                    this.MusicBusinessRoundProjectTargetAudiences.Add(
                        new MusicBusinessRoundProjectTargetAudience(
                            this.Id,
                            targetAudience,
                            userId,
                            string.Empty
                        )
                    );
                }
            }
        }

        /// <summary>
        /// Updates the target audiences.
        /// </summary>
        /// <param name="targetAudiences">The music business project target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateTargetAudiences(
           List<TargetAudience> targetAudiences,
            int userId)
        {
            this.SynchronizeTargetAudiences(targetAudiences, userId);
            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the expectations for meeting.</summary>
        /// <param name="targetAudiences">The music business project target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteTargetAudiences(List<TargetAudience> targetAudiences, int userId)
        {
            var targetAudiencesToDelete = this.MusicBusinessRoundProjectTargetAudiences.Where(db => targetAudiences?.Select(a => a.Uid)?.Contains(db.TargetAudience.Uid) == false && !db.IsDeleted).ToList();
            foreach (var targetAudienceToDelete in targetAudiencesToDelete)
            {
                targetAudienceToDelete.Delete(userId);
            }
        }

        #endregion

        #region MusicBusinessRoundProjectPlayerCategories

        /// <summary>Synchronizes the player categories.</summary>
        /// <param name="playerCategories">The music business project player categories.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizePlayerCategories(List<PlayerCategory> playerCategories, int userId)
        {
            if (this.MusicBusinessRoundProjectPlayerCategories == null)
            {
                this.MusicBusinessRoundProjectPlayerCategories = new List<MusicBusinessRoundProjectPlayerCategory>();
            }

            this.DeletePlayerCategories(playerCategories, userId);

            if (playerCategories?.Any() != true)
            {
                return;
            }

            // Create or update player category
            foreach (var playerCategory in playerCategories)
            {
                var playerCategoryDb = this.MusicBusinessRoundProjectPlayerCategories.FirstOrDefault(a => a.PlayerCategory.Uid == playerCategory.Uid);
                if (playerCategoryDb != null)
                {
                    playerCategoryDb.Update(userId);
                }
                else
                {
                    this.MusicBusinessRoundProjectPlayerCategories.Add(
                        new MusicBusinessRoundProjectPlayerCategory(
                            this.Id,
                            playerCategory,
                            string.Empty,
                            userId
                        )
                    );
                }
            }
        }

        /// <summary>
        /// Updates the target player categories.
        /// </summary>
        /// <param name="playerCategories">The music business project player categories.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdatePlayerCategories(
           List<PlayerCategory> playerCategories,
            int userId)
        {
            this.SynchronizePlayerCategories(playerCategories, userId);
            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the player categories.</summary>
        /// <param name="playerCategories">The player categories.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeletePlayerCategories(List<PlayerCategory> playerCategories, int userId)
        {
            var playerCategoriesToDelete = this.MusicBusinessRoundProjectPlayerCategories.Where(db => playerCategories?.Select(a => a.Uid)?.Contains(db.PlayerCategory.Uid) == false && !db.IsDeleted).ToList();
            foreach (var playerCategoryToDelete in playerCategoriesToDelete)
            {
                playerCategoryToDelete.Delete(userId);
            }
        }

        #endregion

        #region MusicBusinessRoundProjectInterest

        /// <summary>Synchronizes the interests.</summary>
        /// <param name="musicBusinessRoundProjectInterests">The music business project interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeInterests(List<MusicBusinessRoundProjectInterest> musicBusinessRoundProjectInterests, int userId)
        {
            if (this.MusicBusinessRoundProjectInterests == null)
            {
                this.MusicBusinessRoundProjectInterests = new List<MusicBusinessRoundProjectInterest>();
            }

            this.DeleteInterests(musicBusinessRoundProjectInterests, userId);

            if (musicBusinessRoundProjectInterests?.Any() != true)
            {
                return;
            }

            // Create or update interests
            foreach (var musicBusinessRoundProjectInterest in musicBusinessRoundProjectInterests)
            {
                var musicBusinessRoundProjectInterestDb = this.MusicBusinessRoundProjectInterests.FirstOrDefault(a => a.Interest.Uid == musicBusinessRoundProjectInterest.Interest.Uid);
                if (musicBusinessRoundProjectInterestDb != null)
                {
                    musicBusinessRoundProjectInterestDb.Update(userId);
                }
                else
                {
                    this.MusicBusinessRoundProjectInterests.Add(musicBusinessRoundProjectInterest);
                }
            }
        }

        /// <summary>
        /// Updates the interests.
        /// </summary>
        /// <param name="musicBusinessRoundProjectInterests">The music business project interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateInterests(
            List<MusicBusinessRoundProjectInterest> musicBusinessRoundProjectInterests,
            int userId)
        {
            this.SynchronizeInterests(musicBusinessRoundProjectInterests, userId);
            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the expectations for meeting.</summary>
        /// <param name="musicBusinessRoundProjectInterests">The music business project interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteInterests(List<MusicBusinessRoundProjectInterest> musicBusinessRoundProjectInterests, int userId)
        {
            var interestsToDelete = this.MusicBusinessRoundProjectInterests.Where(db => musicBusinessRoundProjectInterests?.Select(a => a.Interest.Uid)?.Contains(db.Interest.Uid) == false && !db.IsDeleted).ToList();
            foreach (var interestToDelete in interestsToDelete)
            {
                interestToDelete.Delete(userId);
            }
        }

        #endregion

        #region MusicBusinessRoundProjectActivities

        /// <summary>Synchronizes the activities.</summary>
        /// <param name="activities">The music business project activities.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeActivities(List<Activity> activities, int userId)
        {
            if (this.MusicBusinessRoundProjectActivities == null)
            {
                this.MusicBusinessRoundProjectActivities = new List<MusicBusinessRoundProjectActivity>();
            }

            this.DeleteActivities(activities, userId);

            if (activities?.Any() != true)
            {
                return;
            }

            // Create or update activities
            foreach (var activity in activities)
            {
                var activityDb = this.MusicBusinessRoundProjectActivities.FirstOrDefault(a => a.Activity.Uid == activity.Uid);
                if (activityDb != null)
                {
                    activityDb.Update(userId);
                }
                else
                {
                    this.MusicBusinessRoundProjectActivities.Add(
                        new MusicBusinessRoundProjectActivity(
                            this.Id,
                            activity,
                            string.Empty,
                            userId
                        )
                    );
                }
            }
        }

        /// <summary>
        /// Updates the target activities.
        /// </summary>
        /// <param name="activities">The music business project activities.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateActivities(
           List<Activity> activities,
            int userId)
        {
            this.SynchronizeActivities(activities, userId);
            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the activities.</summary>
        /// <param name="targetAudiences">The activities.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteActivities(List<Activity> activities, int userId)
        {
            var activitiesToDelete = this.MusicBusinessRoundProjectActivities.Where(db => activities?.Select(a => a.Uid)?.Contains(db.Activity.Uid) == false && !db.IsDeleted).ToList();
            foreach (var activityToDelete in activitiesToDelete)
            {
                activityToDelete.Delete(userId);
            }
        }

        #endregion

        #region MusicBusinessRoundProjectExpectationsForMeeting

        /// <summary>Synchronizes the expectations for meeting.</summary>
        /// <param name="expectationsForMeeting">The expectations for meeting.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeExpectationsForMeeting(List<MusicBusinessRoundProjectExpectationsForMeeting> expectationsForMeeting, int userId)
        {
            if (this.MusicBusinessRoundProjectExpectationsForMeetings == null)
            {
                this.MusicBusinessRoundProjectExpectationsForMeetings = new List<MusicBusinessRoundProjectExpectationsForMeeting>();
            }

            this.DeleteExpectationsForMeeting(expectationsForMeeting, userId);

            if (expectationsForMeeting?.Any() != true)
            {
                return;
            }

            foreach (var expectationForMeeting in expectationsForMeeting)
            {
                var expectationForMeetingDb = this.MusicBusinessRoundProjectExpectationsForMeetings.FirstOrDefault(d => d.Language.Code == expectationForMeeting.Language.Code);
                if (expectationForMeetingDb != null)
                {
                    expectationForMeetingDb.Update(expectationForMeeting);
                }
                else
                {
                    this.CreateExpectationForMeeting(expectationForMeeting);
                }
            }
        }

        /// <summary>Creates the expectation for meeting.</summary>
        /// <param name="expectationForMeeting">The expectation for meeting.</param>
        private void CreateExpectationForMeeting(MusicBusinessRoundProjectExpectationsForMeeting expectationForMeeting)
        {
            this.MusicBusinessRoundProjectExpectationsForMeetings.Add(expectationForMeeting);
        }

        /// <summary>Deletes the expectations for meeting.</summary>
        /// <param name="expectationsForMeeting">The new expectation for meeting.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteExpectationsForMeeting(List<MusicBusinessRoundProjectExpectationsForMeeting> expectationsForMeeting, int userId)
        {
            var expectationsForMeetingToDelete = this.MusicBusinessRoundProjectExpectationsForMeetings
                .Where(db => expectationsForMeeting?
                                .Select(d => d.Language.Code)?
                                .Contains(db.Language.Code) == false 
                                && !db.IsDeleted)
                .ToList();
            foreach (var expectationForMeeting in expectationsForMeetingToDelete)
            {
                expectationForMeeting.Delete(userId);
            }
        }

        #endregion

        #region Buyer Evaluations

        /// <summary>
        /// Creates the music business round project buyer evaluation.
        /// </summary>
        /// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        /// <param name="projectEvaluationStatus">The project evaluation status.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void CreateMusicBusinessRoundProjectBuyerEvaluation(
            AttendeeOrganization buyerAttendeeOrganization,
            ProjectEvaluationStatus projectEvaluationStatus,
            int userId,
            bool isAdmin)
        {
            if (this.MusicBusinessRoundProjectBuyerEvaluations == null)
            {
                this.MusicBusinessRoundProjectBuyerEvaluations = new List<MusicBusinessRoundProjectBuyerEvaluation>();
            }

            var buyerEvaluation = this.GetMusicBusinessRoundProjectBuyerEvaluationByAttendeeOrganizationUid(buyerAttendeeOrganization?.Uid ?? Guid.Empty);
            if (buyerEvaluation == null)
            {
                this.MusicBusinessRoundProjectBuyerEvaluations.Add(new MusicBusinessRoundProjectBuyerEvaluation(this, buyerAttendeeOrganization, projectEvaluationStatus, userId));
            }
            else if (buyerEvaluation.IsDeleted)
            {
                buyerEvaluation.Restore(projectEvaluationStatus, userId);
            }

            this.UpdateMusicBusinessRoundProjectBuyerEvaluationCounts();

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
            base.SetUpdateDate(userId);
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

        /// <summary>Validates the project buyer evaluations.</summary>
        public void ValidateProjectBuyerEvaluations()
        {
            if (this.MusicBusinessRoundProjectBuyerEvaluations?.Any() != true)
            {
                return;
            }

            if (this.ProjectBuyerEvaluationsCount > this.SellerAttendeeCollaborator.Edition.ProjectMaxBuyerEvaluationsCount)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.MaxProjectBuyersEvaluationsReached, this.SellerAttendeeCollaborator.Edition.ProjectMaxBuyerEvaluationsCount, Labels.Players), new string[] { "ToastrError" }));
            }

            foreach (var projectBuyerEvaluation in this.MusicBusinessRoundProjectBuyerEvaluations?.Where(t => !t.IsValid())?.ToList())
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
        /// Deletes the music business round project buyer evaluation.
        /// </summary>
        /// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public void DeleteMusicBusinessRoundProjectBuyerEvaluation(
            AttendeeOrganization buyerAttendeeOrganization,
            int userId,
            bool isAdmin)
        {
            var buyerEvaluation = this.GetMusicBusinessRoundProjectBuyerEvaluationByAttendeeOrganizationUid(buyerAttendeeOrganization?.Uid ?? Guid.Empty);
            buyerEvaluation?.Delete(userId);

            this.UpdateMusicBusinessRoundProjectBuyerEvaluationCounts();

            this.SetUpdateDate(userId);

            this.IsAdmin = isAdmin;
        }

        /// <summary>
        /// Deletes the music business round project buyer evaluations.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteMusicBusinessRoundProjectBuyerEvaluations(int userId)
        {
            var buyerEvaluations = this.FindAllMusicBusinessRoundProjectBuyerEvaluationsNotDeleted();
            if (buyerEvaluations?.Any() != true)
            {
                return;
            }

            buyerEvaluations.ForEach(be => be.Delete(userId));

            this.UpdateMusicBusinessRoundProjectBuyerEvaluationCounts();
        }

        /// <summary>Updates the project buyer evaluation counts.</summary>
        public void UpdateMusicBusinessRoundProjectBuyerEvaluationCounts()
        {
            this.ProjectBuyerEvaluationsCount = this.RecountMusicBusinessRoundProjectBuyerEvaluations();
        }

        /// <summary>Recounts the project buyer evaluations.</summary>
        /// <returns></returns>
        public int RecountMusicBusinessRoundProjectBuyerEvaluations()
        {
            return this.MusicBusinessRoundProjectBuyerEvaluations?.Count(be => !be.IsDeleted) ?? 0;
        }

        /// <summary>
        /// Accepts the project buyer evaluation.
        /// </summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="projectsApprovalLimitExceeded">if set to <c>true</c> [projects approval limit exceeded].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public MusicBusinessRoundProjectBuyerEvaluation AcceptMusicBusinessRoundProjectBuyerEvaluation(
            Guid attendeeOrganizationUid,
            List<ProjectEvaluationStatus> projectEvaluationStatuses,
            bool projectsApprovalLimitExceeded,
            int userId)
        {
            var buyerEvaluation = this.GetMusicBusinessRoundProjectBuyerEvaluationByAttendeeOrganizationUid(attendeeOrganizationUid);
            buyerEvaluation?.Accept(projectEvaluationStatuses, projectsApprovalLimitExceeded, userId);

            return buyerEvaluation;
        }

        /// <summary>
        /// Refuses the project buyer evaluation.
        /// </summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <param name="projectEvaluationRefuseReason">The project evaluation refuse reason.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public MusicBusinessRoundProjectBuyerEvaluation RefuseMusicBusinessRoundProjectBuyerEvaluation(
            Guid attendeeOrganizationUid,
            ProjectEvaluationRefuseReason projectEvaluationRefuseReason,
            string reason,
            List<ProjectEvaluationStatus> projectEvaluationStatuses,
            int userId)
        {
            var buyerEvaluation = this.GetMusicBusinessRoundProjectBuyerEvaluationByAttendeeOrganizationUid(attendeeOrganizationUid);
            buyerEvaluation?.Refuse(projectEvaluationRefuseReason, reason, projectEvaluationStatuses, userId);

            return buyerEvaluation;
        }

        /// <summary>
        /// Gets the project buyer evaluation by attendee organization uid.
        /// </summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns></returns>
        private MusicBusinessRoundProjectBuyerEvaluation GetMusicBusinessRoundProjectBuyerEvaluationByAttendeeOrganizationUid(Guid attendeeOrganizationUid)
        {
            return this.MusicBusinessRoundProjectBuyerEvaluations?.FirstOrDefault(pbe => pbe.BuyerAttendeeOrganization.Uid == attendeeOrganizationUid);
        }

        /// <summary>
        /// Finds all project buyer evaluations not deleted.
        /// </summary>
        /// <returns></returns>
        private List<MusicBusinessRoundProjectBuyerEvaluation> FindAllMusicBusinessRoundProjectBuyerEvaluationsNotDeleted()
        {
            return this.MusicBusinessRoundProjectBuyerEvaluations?.Where(pbe => !pbe.IsDeleted)?.ToList();
        }

        #endregion

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidatePlayerCategoriesThatHaveOrHadContract();
            this.ValidateAttachmentUrl();

            return this.ValidationResult.IsValid;
        }

        private void ValidatePlayerCategoriesThatHaveOrHadContract()
        {
            if (this.AttachmentUrl?.Trim().Length > AttachmentUrlMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Attachments, AttachmentUrlMaxLength, 1), new string[] { "AttachmentUrl" }));
            }
        }
        private void ValidateAttachmentUrl()
        {
            if (this.PlayerCategoriesThatHaveOrHadContract?.Trim().Length > PlayerCategoriesThatHaveOrHadContractMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.PlayerCategoriesThatHaveOrHadContract, PlayerCategoriesThatHaveOrHadContractMaxLength, 1), new string[] { "PlayerCategoriesThatHaveOrHadContract" }));
            }
        }

        #endregion
    }
}