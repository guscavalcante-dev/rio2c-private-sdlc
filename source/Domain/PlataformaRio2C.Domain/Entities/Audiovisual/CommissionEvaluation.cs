// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-28-2021
// ***********************************************************************
// <copyright file="CommissionEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// CommissionEvaluation
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class CommissionEvaluation : Entity
    {
        public int ProjectId { get; set; }
        public int EvaluatorUserId { get; set; }
        public decimal? Grade { get; set; }

        public virtual Project Project { get; private set; }
        public virtual User EvaluatorUser { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommissionEvaluation"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="grade">The grade.</param>
        /// <param name="userId">The user identifier.</param>
        public CommissionEvaluation(
            Project project,
            User evaluatorUser,
            decimal grade,
            int userId)
        {
            this.Project = project;
            this.EvaluatorUser = evaluatorUser;
            this.Grade = grade;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommissionEvaluation"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="userId">The user identifier.</param>
        public CommissionEvaluation(
            Project project,
            User evaluatorUser,
            int userId)
        {
            this.Project = project;
            this.EvaluatorUser = evaluatorUser;

            this.ProjectId = project?.Id ?? 0;
            this.EvaluatorUserId = evaluatorUser?.Id ?? 0;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommissionEvaluation"/> class.
        /// </summary>
        public CommissionEvaluation()
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
            base.SetUpdateDate(userId);
        }

        #region Valitations

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
