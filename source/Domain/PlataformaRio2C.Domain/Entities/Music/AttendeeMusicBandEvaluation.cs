// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-30-2021
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
        protected AttendeeMusicBandEvaluation()
        {
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Restores the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Restore(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
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