// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-07-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeInnovationOrganizationEvaluation.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeInnovationOrganizationEvaluation : Entity
    {
        public int AttendeeInnovationOrganizationId { get; set; }
        public int EvaluatorUserId { get; set; }
        public decimal? Grade { get; set; }

        public virtual AttendeeInnovationOrganization AttendeeInnovationOrganization { get; private set; }
        public virtual User EvaluatorUser { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationEvaluation"/> class.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="grade">The grade.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganizationEvaluation(
            AttendeeInnovationOrganization attendeeInnovationOrganization,
            User evaluatorUser,
            decimal grade,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.EvaluatorUser = evaluatorUser;
            this.Grade = grade;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationEvaluation"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationEvaluation()
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

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
        {
            base.Delete(userId);
            this.AttendeeInnovationOrganization.RecalculateGrade();
            this.AttendeeInnovationOrganization.RecalculateVotesCount();
        }

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

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
