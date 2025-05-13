// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 25-02-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 25-02-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectBuyerEvaluation.cs" company="Softo">
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
    public class MusicBusinessRoundProjectBuyerEvaluation : Entity
    {
        public static readonly int ReasonMinLength = 1;
        public static readonly int ReasonMaxLength = 1500;

        public int MusicBusinessRoundProjectId { get; private set; }
        public int BuyerAttendeeOrganizationId { get; private set; }
        public int? ProjectEvaluationStatusId { get; private set; }
        public int? ProjectEvaluationRefuseReasonId { get; private set; }
        public string Reason { get; private set; }
        public int SellerUserId { get; private set; }
        public int? BuyerEvaluationUserId { get; private set; }
        public DateTimeOffset? EvaluationDate { get; private set; }
        public DateTimeOffset? BuyerEmailSendDate { get; private set; }
        public bool IsVirtualMeeting { get; private set; }
        public int? AttendeeCollaboratorId { get; set; }

        public virtual MusicBusinessRoundProject MusicBusinessRoundProject { get; private set; }
        public virtual AttendeeOrganization BuyerAttendeeOrganization { get; private set; }
        public virtual ProjectEvaluationStatus ProjectEvaluationStatus { get; private set; }
        public virtual ProjectEvaluationRefuseReason ProjectEvaluationRefuseReason { get; private set; }
        public virtual User SellerUser { get; private set; }
        public virtual User BuyerEvaluationUser { get; private set; }
        public virtual ICollection<MusicBusinessRoundNegotiation> MusicBusinessRoundNegotiations { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectBuyerEvaluation"/> class.
        /// </summary>
        /// <param name="musicBusinessRoundProject">The music business round project.</param>
        /// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        /// <param name="projectEvaluationStatus">The project evaluation status.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBusinessRoundProjectBuyerEvaluation(
                MusicBusinessRoundProject musicBusinessRoundProject,
                AttendeeOrganization buyerAttendeeOrganization,
                ProjectEvaluationStatus projectEvaluationStatus,
                int userId)
        {
            this.MusicBusinessRoundProjectId = musicBusinessRoundProject?.Id ?? 0;
            this.MusicBusinessRoundProject = musicBusinessRoundProject;
            this.BuyerAttendeeOrganizationId = buyerAttendeeOrganization?.Id ?? 0;
            this.BuyerAttendeeOrganization = buyerAttendeeOrganization;
            this.ProjectEvaluationStatusId = projectEvaluationStatus?.Id ?? 0;
            this.ProjectEvaluationStatus = projectEvaluationStatus;
            this.Reason = null;
            this.SellerUserId = userId;
            this.BuyerEvaluationUserId = null;
            this.EvaluationDate = null;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectBuyerEvaluation"/> class.
        /// </summary>
        public MusicBusinessRoundProjectBuyerEvaluation()
        {
        }

        #region Evaluation

        /// <summary>
        /// Accepts the specified project evaluation statuses.
        /// </summary>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="projectsApprovalLimitExceeded">if set to <c>true</c> [projects approval limit exceeded].</param>
        /// <param name="userId">The user identifier.</param>
        public void Accept(
            List<ProjectEvaluationStatus> projectEvaluationStatuses,
            bool projectsApprovalLimitExceeded,
            int userId)
        {
            var projectEvaluationStatus = projectEvaluationStatuses?.FirstOrDefault(pes => pes.Code == ProjectEvaluationStatus.Accepted.Code);
            this.ProjectEvaluationStatusId = projectEvaluationStatus?.Id ?? 0;
            this.ProjectEvaluationStatus = projectEvaluationStatus;

            this.ProjectEvaluationRefuseReasonId = null;
            this.ProjectEvaluationRefuseReason = null;
            this.Reason = null;
            this.BuyerEvaluationUserId = userId;
            this.EvaluationDate = DateTime.UtcNow;

            //if (projectsApprovalLimitExceeded)
            //{
            //    this.IsVirtualMeeting = true;
            //}

            this.SetUpdateDate(userId);
        }

        /// <summary>
        /// Refuses the specified project evaluation refuse reason.
        /// </summary>
        /// <param name="projectEvaluationRefuseReason">The project evaluation refuse reason.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userId">The user identifier.</param>
        public void Refuse(
            ProjectEvaluationRefuseReason projectEvaluationRefuseReason,
            string reason, List<ProjectEvaluationStatus>
            projectEvaluationStatuses,
            int userId)
        {
            var projectEvaluationStatus = projectEvaluationStatuses?.FirstOrDefault(pes => pes.Code == ProjectEvaluationStatus.Refused.Code);
            this.ProjectEvaluationStatusId = projectEvaluationStatus?.Id ?? 0;
            this.ProjectEvaluationStatus = projectEvaluationStatus;

            this.ProjectEvaluationRefuseReasonId = projectEvaluationRefuseReason?.Id;
            this.ProjectEvaluationRefuseReason = projectEvaluationRefuseReason;
            this.Reason = reason?.Trim();
            this.BuyerEvaluationUserId = userId;
            this.EvaluationDate = DateTime.UtcNow;
            //this.IsVirtualMeeting = false;

            base.SetUpdateDate(userId);
        }

        /// <summary>
        /// Restores the specified project evaluation status.
        /// </summary>
        /// <param name="projectEvaluationStatus">The project evaluation status.</param>
        /// <param name="userId">The user identifier.</param>
        public void Restore(ProjectEvaluationStatus projectEvaluationStatus, int userId)
        {
            this.ProjectEvaluationStatusId = projectEvaluationStatus?.Id ?? 0;
            this.ProjectEvaluationStatus = projectEvaluationStatus;
            this.Reason = null;
            this.SellerUserId = userId;
            this.BuyerEvaluationUserId = null;
            this.EvaluationDate = null;

            base.SetUpdateDate(userId);
        }

        #endregion

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateRefuseReason();

            return this.ValidationResult.IsValid;
        }

        public bool IsEvaluationValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateRefuseReason();

            return this.ValidationResult.IsValid;
        }

        public void ValidateRefuseReason()
        {
            if (this.Reason?.Trim().Length < ReasonMinLength || this.Reason?.Trim().Length > ReasonMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, ReasonMaxLength, ReasonMinLength), new string[] { "Reason" }));
            }
        }

        #endregion
    }
}
