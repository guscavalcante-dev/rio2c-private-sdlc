// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-26-2024
// ***********************************************************************
// <copyright file="AttendeeCreatorProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    public class AttendeeCreatorProject : Entity
    {
        public int CreatorProjectId { get; private set; }
        public int EditionId { get; private set; }
        public decimal? Grade { get; private set; }
        public int EvaluationsCount { get; private set; }
        public DateTimeOffset? LastEvaluationDate { get; private set; }
        public DateTimeOffset? EvaluationEmailSendDate { get; private set; }

        public virtual CreatorProject CreatorProject { get; private set; }
        public virtual Edition Edition { get; private set; }
        public virtual ICollection<AttendeeCreatorProjectEvaluation> AttendeeCreatorProjectEvaluations { get; private set; }

        #region Evaluation

        /// <summary>
        /// Evaluates the specified evaluation user.
        /// </summary>
        /// <param name="evaluatorUser">The evaluation user.</param>
        /// <param name="grade">The grade.</param>
        public void Evaluate(User evaluatorUser, decimal grade)
        {
            this.Grade = grade;

            if (this.AttendeeCreatorProjectEvaluations == null)
                this.AttendeeCreatorProjectEvaluations = new List<AttendeeCreatorProjectEvaluation>();

            var existentEvaluation = this.GetAttendeeCreatorProjectEvaluationByEvaluatorId(evaluatorUser.Id);
            if (existentEvaluation != null)
            {
                existentEvaluation.Update(grade, evaluatorUser);
            }
            else
            {
                this.AttendeeCreatorProjectEvaluations.Add(new AttendeeCreatorProjectEvaluation(
                    this,
                    evaluatorUser,
                    grade));
            }

            this.Grade = this.GetAverageEvaluation();
            this.EvaluationsCount = this.GetAttendeeCreatorProjectEvaluationTotalCount();
            this.LastEvaluationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Recalculates the grade.
        /// </summary>
        public void RecalculateGrade()
        {
            this.Grade = this.GetAverageEvaluation();
        }

        /// <summary>
        /// Recalculates the votes count.
        /// </summary>
        public void RecalculateVotesCount()
        {
            this.EvaluationsCount = this.GetAttendeeCreatorProjectEvaluationTotalCount();
        }

        /// <summary>
        /// Gets the average evaluation.
        /// </summary>
        /// <returns></returns>
        private decimal? GetAverageEvaluation()
        {
            var attendeeCreatorProjectEvaluations = this.FindAllAttendeeCreatorProjectEvaluationsNotDeleted();
            if (attendeeCreatorProjectEvaluations?.Any() != true)
            {
                return null;
            }

            // Can only generate the 'AverageEvaluation' when the 'AttendeeCreatorProjectEvaluation' count 
            // is greater or equal than minimum necessary evaluations quantity
            if (this.GetAttendeeCreatorProjectEvaluationTotalCount() >= this.Edition?.CreatorCommissionMinimumEvaluationsCount)
            {
                return attendeeCreatorProjectEvaluations.Sum(e => e.Grade) / attendeeCreatorProjectEvaluations.Count;
            }

            return null;
        }

        /// <summary>
        /// Gets the attendee creator project evaluation by evaluator identifier.
        /// </summary>
        /// <param name="evaluatorUserId">The evaluator user identifier.</param>
        /// <returns></returns>
        private AttendeeCreatorProjectEvaluation GetAttendeeCreatorProjectEvaluationByEvaluatorId(int evaluatorUserId)
        {
            return this.FindAllAttendeeCreatorProjectEvaluationsNotDeleted().FirstOrDefault(ambe =>
                ambe.AttendeeCreatorProject.EditionId == this.EditionId &&
                ambe.EvaluatorUserId == evaluatorUserId);
        }

        /// <summary>
        /// Gets the attendee creator project evaluation total count.
        /// </summary>
        /// <returns></returns>
        private int GetAttendeeCreatorProjectEvaluationTotalCount()
        {
            return this.FindAllAttendeeCreatorProjectEvaluationsNotDeleted()
                .Count(ambe => ambe.AttendeeCreatorProject.EditionId == this.EditionId);
        }

        /// <summary>
        /// Finds all attendee creator project evaluations not deleted.
        /// </summary>
        /// <returns></returns>
        private List<AttendeeCreatorProjectEvaluation> FindAllAttendeeCreatorProjectEvaluationsNotDeleted()
        {
            return this.AttendeeCreatorProjectEvaluations?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateAttendeeCreatorProjectEvaluations();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the commission evaluations.
        /// </summary>
        public void ValidateAttendeeCreatorProjectEvaluations()
        {
            if (this.AttendeeCreatorProjectEvaluations?.Any() != true)
            {
                return;
            }

            foreach (var commissionEvaluation in this.AttendeeCreatorProjectEvaluations?.Where(t => !t.IsValid())?.ToList())
            {
                this.ValidationResult.Add(commissionEvaluation.ValidationResult);
            }
        }

        #endregion
    }
}
