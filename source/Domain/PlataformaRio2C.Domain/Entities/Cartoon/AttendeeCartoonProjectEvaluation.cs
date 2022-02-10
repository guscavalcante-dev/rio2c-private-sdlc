// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 02-04-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-04-2022
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeCartoonProjectEvaluation</summary>
    public class AttendeeCartoonProjectEvaluation : Entity
    {
        public int AttendeeCartoonProjectId { get; private set; }
        public int EvaluatorUserId { get; private set; }
        public decimal Grade { get; private set; }

        public virtual AttendeeCartoonProject AttendeeCartoonProject { get; private set; }
        public virtual User EvaluatorUser { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCartoonProjectEvaluation"/> class.
        /// </summary>
        /// <param name="attendeeCartoonProject">The attendee cartoon project.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCartoonProjectEvaluation(
            AttendeeCartoonProject attendeeCartoonProject,
            User evaluatorUser, 
            decimal grade,
            int userId)
        {
            this.AttendeeCartoonProject = attendeeCartoonProject;
            this.EvaluatorUser = evaluatorUser;
            this.AttendeeCartoonProjectId = attendeeCartoonProject.Id;
            this.EvaluatorUserId = evaluatorUser.Id;
            this.Grade = grade;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCartoonProjectEvaluation"/> class.
        /// </summary>
        protected AttendeeCartoonProjectEvaluation()
        {
        }

        /// <summary>
        /// Updates the specified grade.
        /// </summary>
        /// <param name="grade">The grade.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(decimal grade, int userId)
        {
            this.Grade = grade;
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
        {
            base.Delete(userId);
            this.AttendeeCartoonProject.RecalculateGrade();
            this.AttendeeCartoonProject.RecalculateVotesCount();
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateGrade();
            this.ValidateEvaluatorUser();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the grade.
        /// </summary>
        public void ValidateGrade()
        {
            if (this.Grade < 0 || this.Grade > 10)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Grade, "10", "0"), new string[] { "Grade" }));
            }
        }

        /// <summary>
        /// Validates the evaluator user.
        /// </summary>
        public void ValidateEvaluatorUser()
        {
            if (this.EvaluatorUser == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.EvaluatorUser), new string[] { "EvaluatorUserId" }));
            }
        }

        #endregion
    }
}