// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-26-2024
// ***********************************************************************
// <copyright file="AttendeeCreatorProjectEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    public class AttendeeCreatorProjectEvaluation : Entity
    {
        public int AttendeeCreatorProjectId { get; private set; }
        public int EvaluatorUserId { get; private set; }
        public decimal Grade { get; private set; }

        public AttendeeCreatorProject AttendeeCreatorProject { get; private set; }
        public User EvaluatorUser { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCreatorProjectEvaluation" /> class.
        /// </summary>
        /// <param name="attendeeCreatorProject">The attendee creator project.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="grade">The grade.</param>
        public AttendeeCreatorProjectEvaluation(
            AttendeeCreatorProject attendeeCreatorProject,
            User evaluatorUser,
            decimal grade)
        {
            this.AttendeeCreatorProject = attendeeCreatorProject;
            this.EvaluatorUser = evaluatorUser;
            this.AttendeeCreatorProjectId = attendeeCreatorProject.Id;
            this.EvaluatorUserId = evaluatorUser.Id;
            this.Grade = grade;

            this.SetCreateDate(evaluatorUser.Id);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCreatorProjectEvaluation"/> class.
        /// </summary>
        public AttendeeCreatorProjectEvaluation()
        {
        }

        /// <summary>
        /// Updates the specified grade.
        /// </summary>
        /// <param name="grade">The grade.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        public void Update(decimal grade, User evaluatorUser)
        {
            this.Grade = grade;
            this.EvaluatorUser = evaluatorUser;
            this.SetUpdateDate(evaluatorUser.Id);
        }

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateGrade();

            return this.ValidationResult.IsValid;
        }

        public void ValidateGrade()
        {
            if (this.Grade < 0 || this.Grade > 10)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Grade, "10", "0"), new string[] { "Grade" }));
            }
        }

        #endregion
    }
}
