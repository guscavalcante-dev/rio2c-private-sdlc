// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-18-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
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

        private bool IsAdmin = false;

        public virtual AttendeeCollaborator SellerAttendeeCollaborator { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectTargetAudience> MusicBusinessRoundProjectTargetAudiences { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectInterest> MusicBusinessRoundProjectInterests { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectPlayerCategory> MusicBusinessRoundProjectPlayerCategories { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectActivity> MusicBusinessRoundProjectActivities { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectExpectationsForMeeting> MusicBusinessRoundProjectExpectationsForMeetings { get; private set; }
        public virtual ICollection<MusicBusinessRoundProjectBuyerEvaluation> MusicBusinessRoundProjectBuyerEvaluations { get; private set; }

        public MusicBusinessRoundProject()
        {
            
        }

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
            ICollection<MusicBusinessRoundProjectBuyerEvaluation> musicBusinessRoundProjectBuyerEvaluations
)
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
            ICollection<MusicBusinessRoundProjectActivity> musicBusinessRoundProjectActivities,
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

            this.MusicBusinessRoundProjectInterests = musicBusinessRoundProjectInterests ?? new List<MusicBusinessRoundProjectInterest>();


            this.MusicBusinessRoundProjectActivities = musicBusinessRoundProjectActivities ?? new List<MusicBusinessRoundProjectActivity>();
            this.MusicBusinessRoundProjectExpectationsForMeetings = musicBusinessRoundProjectExpectationsForMeetings ?? new List<MusicBusinessRoundProjectExpectationsForMeeting>();
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }
        

        public bool IsFinished()
        {
            return this.FinishDate.HasValue;
        }

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