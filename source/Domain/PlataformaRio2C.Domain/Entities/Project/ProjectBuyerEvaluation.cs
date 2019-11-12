// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-12-2019
// ***********************************************************************
// <copyright file="ProjectBuyerEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectBuyerEvaluation</summary>
    public class ProjectBuyerEvaluation : Entity
    {
        public static readonly int ReasonMinLength = 1;
        public static readonly int ReasonMaxLength = 1500;

        public int ProjectId { get; private set; }
        public int BuyerAttendeeOrganizationId { get; private set; }
        public int? ProjectEvaluationStatusId { get; private set; }
        public string Reason { get; private set; }
        public bool IsSent { get; private set; }
        public int SellerUserId { get; private set; }
        public int? BuyerEvaluationUserId { get; private set; }
        public DateTime? EvaluationDate { get; private set; }

        public virtual Project Project { get; private set; }
        public virtual AttendeeOrganization BuyerAttendeeOrganization { get; private set; }
        //public virtual ProjectEvaluationStatus EvaluationStatus { get; private set; }
        public virtual User SellerUser { get; private set; }
        public virtual User BuyerEvaluationUser { get; private set; }
        public virtual ProjectEvaluationStatus ProjectEvaluationStatus { get; private set; }

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
            this.IsSent = false;
            this.SellerUserId = userId;
            this.BuyerEvaluationUserId = null;
            this.EvaluationDate = null;

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.Now;
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
            this.IsSent = false;
            this.SellerUserId = userId;
            this.BuyerEvaluationUserId = null;
            this.EvaluationDate = null;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.SellerUserId = userId;

            this.IsDeleted = true;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
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

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the project.</summary>
        public void ValidateProject()
        {
            if (this.Project == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Project, Labels.FoundM)));
            }
        }

        /// <summary>Validates the buyer attendee organization.</summary>
        public void ValidateBuyerAttendeeOrganization()
        {
            if (this.BuyerAttendeeOrganization == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF)));
            }
        }

        /// <summary>Validates the project evaluation status.</summary>
        public void ValidateProjectEvaluationStatus()
        {
            if (this.ProjectEvaluationStatus == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Status, Labels.FoundM)));
            }
        }

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