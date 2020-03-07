// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="ProjectBuyerEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectBuyerEvaluation</summary>
    public class ProjectBuyerEvaluation : Entity
    {
        public static readonly int ReasonMinLength = 1;
        public static readonly int ReasonMaxLength = 500;

        public int ProjectId { get; private set; }
        public int BuyerAttendeeOrganizationId { get; private set; }
        public int ProjectEvaluationStatusId { get; private set; }
        public int? ProjectEvaluationRefuseReasonId { get; private set; }
        public string Reason { get; private set; }
        public int SellerUserId { get; private set; }
        public int? BuyerEvaluationUserId { get; private set; }
        public DateTimeOffset? EvaluationDate { get; private set; }
        public DateTimeOffset? BuyerEmailSendDate { get; private set; }

        public virtual Project Project { get; private set; }
        public virtual AttendeeOrganization BuyerAttendeeOrganization { get; private set; }
        //public virtual ProjectEvaluationStatus EvaluationStatus { get; private set; }
        public virtual User SellerUser { get; private set; }
        public virtual User BuyerEvaluationUser { get; private set; }
        public virtual ProjectEvaluationStatus ProjectEvaluationStatus { get; private set; }
        public virtual ProjectEvaluationRefuseReason ProjectEvaluationRefuseReason { get; private set; }

        public virtual ICollection<Negotiation> Negotiations { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectBuyerEvaluation"/> class.</summary>
        /// <param name="project">The project.</param>
        /// <param name="buyerAttendeeOrganization">The buyer attendee organization.</param>
        /// <param name="projectEvaluationStatus">The project evaluation status.</param>
        /// <param name="userId">The user identifier.</param>
        public ProjectBuyerEvaluation(Project project, AttendeeOrganization buyerAttendeeOrganization, ProjectEvaluationStatus projectEvaluationStatus, int userId)
        {
            this.ProjectId = project?.Id ?? 0;
            this.Project = project;
            this.BuyerAttendeeOrganizationId = buyerAttendeeOrganization?.Id ?? 0;
            this.BuyerAttendeeOrganization = buyerAttendeeOrganization;
            this.ProjectEvaluationStatusId = projectEvaluationStatus?.Id ?? 0;
            this.ProjectEvaluationStatus = projectEvaluationStatus;
            this.Reason = null;
            this.SellerUserId = userId;
            this.BuyerEvaluationUserId = null;
            this.EvaluationDate = null;

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectBuyerEvaluation"/> class.</summary>
        protected ProjectBuyerEvaluation()
        {
        }

        /// <summary>Restores the specified project evaluation status.</summary>
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

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.SellerUserId = userId;

            this.IsDeleted = true;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Accepts the specified project evaluation statuses.</summary>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userId">The user identifier.</param>
        public void Accept(List<ProjectEvaluationStatus> projectEvaluationStatuses, int userId)
        {
            var projectEvaluationStatus = projectEvaluationStatuses?.FirstOrDefault(pes => pes.Code == ProjectEvaluationStatus.Accepted.Code);
            this.ProjectEvaluationStatusId = projectEvaluationStatus?.Id ?? 0;
            this.ProjectEvaluationStatus = projectEvaluationStatus;

            this.ProjectEvaluationRefuseReasonId = null;
            this.ProjectEvaluationRefuseReason = null;
            this.Reason = null;
            this.BuyerEvaluationUserId = userId;
            this.EvaluationDate = DateTime.UtcNow;
           
            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Refuses the specified project evaluation refuse reason.</summary>
        /// <param name="projectEvaluationRefuseReason">The project evaluation refuse reason.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="projectEvaluationStatuses">The project evaluation statuses.</param>
        /// <param name="userId">The user identifier.</param>
        public void Refuse(ProjectEvaluationRefuseReason projectEvaluationRefuseReason, string reason, List<ProjectEvaluationStatus> projectEvaluationStatuses, int userId)
        {
            var projectEvaluationStatus = projectEvaluationStatuses?.FirstOrDefault(pes => pes.Code == ProjectEvaluationStatus.Refused.Code);
            this.ProjectEvaluationStatusId = projectEvaluationStatus?.Id ?? 0;
            this.ProjectEvaluationStatus = projectEvaluationStatus;

            this.ProjectEvaluationRefuseReasonId = projectEvaluationRefuseReason?.Id;
            this.ProjectEvaluationRefuseReason = projectEvaluationRefuseReason;
            this.Reason = reason?.Trim();
            this.BuyerEvaluationUserId = userId;
            this.EvaluationDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Sends the buyer email.</summary>
        public void SendBuyerEmail()
        {
            this.BuyerEmailSendDate = DateTime.UtcNow;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateProject();
            this.ValidateBuyerAttendeeOrganization();
            this.ValidateRefuseReason();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Determines whether [is evaluation valid].</summary>
        /// <returns>
        ///   <c>true</c> if [is evaluation valid]; otherwise, <c>false</c>.</returns>
        public bool IsEvaluationValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateRefuseReason();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the project.</summary>
        public void ValidateProject()
        {
            if (this.Project == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM), new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the buyer attendee organization.</summary>
        public void ValidateBuyerAttendeeOrganization()
        {
            if (this.BuyerAttendeeOrganization == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF), new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the project evaluation status.</summary>
        public void ValidateProjectEvaluationStatus()
        {
            if (this.ProjectEvaluationStatus == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Status, Labels.FoundM), new string[] { "ToastrError" }));
            }
        }

        /// <summary>Validates the refuse reason.</summary>
        public void ValidateRefuseReason()
        {
            //if (this.ProjectEvaluationStatus?.Code == ProjectEvaluationStatus.Refused.Code && this.ProjectEvaluationRefuseReason == null)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Reason), new string[] { "Reason" }));
            //}

            //if (this.ProjectEvaluationRefuseReason?.HasAdditionalInfo == true && string.IsNullOrEmpty(this.Reason?.Trim()))
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Reason), new string[] { "Reason" }));
            //}

            if (this.Reason?.Trim().Length < ReasonMinLength || this.Reason?.Trim().Length > ReasonMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, ReasonMaxLength, ReasonMinLength), new string[] { "Reason" }));
            }
        }

        #endregion

        #region Old methods

        //public ProjectBuyerEvaluation(ProjectPlayer projectPlayer, ProjectStatus status, User evaluationdUser)
        //{
        //    SetProjectPlayer(projectPlayer);
        //    SetProjectStatus(status);
        //    SetEvaluationdUser(evaluationdUser);
        //}

        //public void SetProjectPlayer(ProjectPlayer projectPlayer)
        //{
        //    ProjectPlayer = projectPlayer;

        //    if (projectPlayer != null)
        //    {
        //        ProjectPlayerId = projectPlayer.Id;
        //    }
        //}

        //public void SetProjectStatus(ProjectStatus status)
        //{
        //    Status = status;

        //    if (status != null)
        //    {
        //        StatusId = status.Id;
        //    }
        //}

        //public void SetEvaluationdUser(User evaluationdUser)
        //{
        //    EvaluationdUser = evaluationdUser;

        //    if (evaluationdUser != null)
        //    {
        //        EvaluationUserId = evaluationdUser.Id;
        //    }
        //}

        //public void SetReason(string value)
        //{
        //    Reason = value;
        //}

        //public override bool IsValid()
        //{

        //    ValidationResult = new ValidationResult();

        //    if (this.Status != null && this.Status.Code  != null && this.Status.Code == StatusProjectCodes.Rejected.ToString())
        //    {
        //        ValidationResult.Add(new ProjectPlayerEvaluationRecuseIsConsistent().Valid(this));
        //    }

        //    return ValidationResult.IsValid;
        //}

        #endregion
    }
}