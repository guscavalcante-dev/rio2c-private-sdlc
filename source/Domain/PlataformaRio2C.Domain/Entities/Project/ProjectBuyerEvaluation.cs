// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-04-2019
// ***********************************************************************
// <copyright file="ProjectBuyerEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectBuyerEvaluation</summary>
    public class ProjectBuyerEvaluation : Entity
    {
        public static readonly int ReasonMinLength = 1;
        public static readonly int ReasonMaxLength = 1500;

        public int BuyerAttendeeOrganizationId { get; private set; }
        public int ProjectId { get; private set; }
        public int? ProjectEvaluationStatusId { get; private set; }
        public string Reason { get; private set; }
        public bool IsSent { get; private set; }
        public int SellerUserId { get; private set; }
        public int? BuyerEvaluationUserId { get; private set; }
        public DateTime? EvaluationDate { get; private set; }

        public virtual AttendeeOrganization BuyerAttendeeOrganization { get; private set; }
        //public virtual ProjectEvaluationStatus EvaluationStatus { get; private set; }
        public virtual User SellerUser { get; private set; }
        public virtual User BuyerEvaluationUser { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectBuyerEvaluation"/> class.</summary>
        protected ProjectBuyerEvaluation()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            //this.ValidateName();
            //this.ValidateDescriptions();

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
