// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-30-2021
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-10-2024
// ***********************************************************************
// <copyright file="AttendeeMusicBandEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeMusicBandEvaluation</summary>
    public class AttendeeMusicBandEvaluation : Entity
    {
        public int AttendeeMusicBandId { get; private set; }
        public int EvaluatorUserId { get; private set; }
        public decimal Grade { get; private set; }

        public virtual AttendeeMusicBand AttendeeMusicBand { get; private set; }
        public virtual User EvaluatorUser { get; private set; }
        public virtual ProjectEvaluationStatus CommissionEvaluationStatus { get; private set; }
        public int? CommissionEvaluationStatusId { get; private set; }
        public DateTimeOffset? CommissionEvaluationDate { get; private set; }
        public int? CuratorEvaluationStatusId { get; private set; }
        public DateTimeOffset? CuratorEvaluationDate { get; private set; }
        public int? PopularEvaluationStatusId { get; private set; }
        public DateTimeOffset? PopularEvaluationDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeMusicBandEvaluation"/> class.
        /// </summary>
        /// <param name="attendeeMusicBand">The attendee music band.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeMusicBandEvaluation(
            AttendeeMusicBand attendeeMusicBand,
            User evaluatorUser, 
            decimal grade,
            int userId)
        {
            this.AttendeeMusicBand = attendeeMusicBand;
            this.EvaluatorUser = evaluatorUser;
            this.AttendeeMusicBandId = attendeeMusicBand.Id;
            this.EvaluatorUserId = evaluatorUser.Id;
            this.Grade = grade;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeMusicBandEvaluation"/> class.
        /// </summary>
        /// <param name="attendeeMusicBand">The attendee music band.</param>
        /// <param name="evaluatorUser">The evaluator user.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="commissionEvaluationStatus">The project evaluation status.</param>
        public AttendeeMusicBandEvaluation(
            AttendeeMusicBand attendeeMusicBand,
            User evaluatorUser,
            int userId
        )
        {
            this.AttendeeMusicBand = attendeeMusicBand;
            this.EvaluatorUser = evaluatorUser;
            this.AttendeeMusicBandId = attendeeMusicBand.Id;
            this.EvaluatorUserId = evaluatorUser.Id;
            this.Grade = 0;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeMusicBandEvaluation"/> class.
        /// </summary>
        protected AttendeeMusicBandEvaluation()
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
        /// Updates the specified grade.
        /// </summary>
        /// <param name="commissionEvaluationStatus">The grade.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateCommissionEvaluation(ProjectEvaluationStatus commissionEvaluationStatus, int userId)
        {
            this.CommissionEvaluationStatusId = commissionEvaluationStatus.Id;
            this.CommissionEvaluationDate = DateTime.UtcNow;
            base.SetUpdateDate(userId);
        }

        /// <summary>
        /// Updates the specified grade.
        /// </summary>
        /// <param name="curatorEvaluationStatusId">The grade.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateCuratorEvaluation(ProjectEvaluationStatus curatorEvaluationStatusId, int userId)
        {
            this.CuratorEvaluationStatusId = curatorEvaluationStatusId.Id;
            this.CuratorEvaluationDate = DateTime.UtcNow;
            base.SetUpdateDate(userId);
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
        {
            base.Delete(userId);
            this.AttendeeMusicBand.RecalculateGrade();
            this.AttendeeMusicBand.RecalculateVotesCount();
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