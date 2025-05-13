// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="AttendeeCartoonProject.cs" company="Softo">
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
    /// <summary>AttendeeCartoonProject</summary>
    public class AttendeeCartoonProject : Entity
    {
        public int EditionId { get; private set; }
        public int CartoonProjectId { get; private set; }
        public decimal? Grade { get; private set; }
        public int EvaluationsCount { get; private set; }
        public DateTimeOffset? LastEvaluationDate { get; private set; }
        public DateTimeOffset? EvaluationEmailSendDate { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual CartoonProject CartoonProject { get; private set; }

        public virtual ICollection<AttendeeCartoonProjectCollaborator> AttendeeCartoonProjectCollaborators { get; private set; }
        public virtual ICollection<AttendeeCartoonProjectEvaluation> AttendeeCartoonProjectEvaluations { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProject"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="cartoonProject">The cartoon project.</param>
        /// <param name="evaluatorUser">The evalutor user.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCartoonProject(
            Edition edition,
            CartoonProject cartoonProject,
            int userId)
        {
            this.Edition = edition;
            this.CartoonProject = cartoonProject;
            this.EditionId = edition.Id;

            this.SetCreateDate(userId);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProject"/> class.</summary>
        protected AttendeeCartoonProject()
        {
        }


        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
        {

            this.DeleteAttendeeCartonProjectdCollaborators(userId);
            if (this.FindAllAttendeeCartoonProjectsCollaboratorsNotDeleted()?.Any() == true)
            {
                return;
            }

            base.Delete(userId);
        }

        #region Attendee Cartoon Project Collaborators

        /// <summary>Deletes the attendee cartoon project collaborators.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCartonProjectdCollaborators(int userId)
        {
            foreach (var attendeCollaborator in this.FindAllAttendeeCartoonProjectsCollaboratorsNotDeleted())
            {
                attendeCollaborator.Delete(userId);
            }
        }

        /// <summary>Finds all attendee cartoon project collaborators not deleted.</summary>
        /// <returns></returns>
        private List<AttendeeCartoonProjectCollaborator> FindAllAttendeeCartoonProjectsCollaboratorsNotDeleted()
        {
            return this.AttendeeCartoonProjectCollaborators?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Evaluation

        /// <summary>
        /// Evaluates the specified evaluation user.
        /// </summary>
        /// <param name="evaluatorUser">The evaluation user.</param>
        /// <param name="grade">The grade.</param>
        public void Evaluate(User evaluatorUser, decimal grade)
        {
            this.Grade = grade;
            if (this.AttendeeCartoonProjectEvaluations == null)
                this.AttendeeCartoonProjectEvaluations = new List<AttendeeCartoonProjectEvaluation>();

            var existentEvaluation = this.GetAttendeeCartoonProjectEvaluationByEvaluatorId(evaluatorUser.Id);
            if (existentEvaluation != null)
            {
                existentEvaluation.Update(grade, evaluatorUser.Id);
            }
            else
            {
                this.AttendeeCartoonProjectEvaluations.Add(new AttendeeCartoonProjectEvaluation(
                    this,
                    evaluatorUser,
                    grade,
                    evaluatorUser.Id));
            }

            this.Grade = this.GetAverageEvaluation();
            this.EvaluationsCount = this.GetAttendeeCartoonProjectEvaluationTotalCount();
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
            this.EvaluationsCount = this.GetAttendeeCartoonProjectEvaluationTotalCount();
        }

        /// <summary>
        /// Gets the average evaluation.
        /// </summary>
        /// <returns></returns>
        private decimal? GetAverageEvaluation()
        {
            if (this.FindAllAttendeeCartoonProjectEvaluationsNotDeleted()?.Any() != true)
            {
                return null;
            }

            // Can only generate the 'AverageEvaluation' when the 'AttendeeCartoonProjectEvaluation' count 
            // is greater or equal than minimum necessary evaluations quantity
            if (this.GetAttendeeCartoonProjectEvaluationTotalCount() >= this.Edition?.CartoonCommissionMinimumEvaluationsCount)
            {
                return this.FindAllAttendeeCartoonProjectEvaluationsNotDeleted().Sum(e => e.Grade) / this.FindAllAttendeeCartoonProjectEvaluationsNotDeleted().Count;
            }

            return null;
        }

        /// <summary>
        /// Gets the attendee cartoon projects evaluation by evaluator identifier.
        /// </summary>
        /// <param name="evaluatorUserId">The evaluator user identifier.</param>
        /// <returns></returns>
        private AttendeeCartoonProjectEvaluation GetAttendeeCartoonProjectEvaluationByEvaluatorId(int evaluatorUserId)
        {
            return this.FindAllAttendeeCartoonProjectEvaluationsNotDeleted().FirstOrDefault(ambe =>
                ambe.AttendeeCartoonProject.EditionId == this.EditionId &&
                ambe.EvaluatorUserId == evaluatorUserId);
        }

        /// <summary>
        /// Gets the attendee cartoon project  evaluation total count.
        /// </summary>
        /// <returns></returns>
        private int GetAttendeeCartoonProjectEvaluationTotalCount()
        {
            return this.FindAllAttendeeCartoonProjectEvaluationsNotDeleted()
                .Count(ambe => ambe.AttendeeCartoonProject.EditionId == this.EditionId);
        }

        /// <summary>
        /// Gets the not deleted cartoon project evaluations.
        /// OBS.: Allways use this method to calc Average and others! Evaluations calcs cannot consider deleted Evaluations!
        /// </summary>
        /// <returns></returns>
        private List<AttendeeCartoonProjectEvaluation> FindAllAttendeeCartoonProjectEvaluationsNotDeleted()
        {
            return this.AttendeeCartoonProjectEvaluations?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();
            this.ValidateAttendeeCartoonProjectsEvaluations();
            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the attendee cartoon projects.
        /// </summary>
        private void ValidateAttendeeCartoonProjectsEvaluations()
        {
            if (this.AttendeeCartoonProjectEvaluations?.Any() != true)
            {
                return;
            }

            foreach (var attendeeCartoonProject in this.AttendeeCartoonProjectEvaluations.Where(aiof => !aiof.IsValid()))
            {
                this.ValidationResult.Add(attendeeCartoonProject.ValidationResult);
            }
        }


        #endregion
    }
}