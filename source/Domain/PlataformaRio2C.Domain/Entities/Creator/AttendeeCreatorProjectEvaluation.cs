// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-26-2024
// ***********************************************************************
// <copyright file="AttendeeCreatorProjectEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class AttendeeCreatorProjectEvaluation : Entity
    {
        public int AttendeeCreatorProjectId { get; private set; }
        public int EvaluatorUserId { get; private set; }
        public decimal Grade { get; private set; }

        public AttendeeCreatorProject AttendeeCreatorProject { get; private set; }
        public User EvaluatorUser { get; private set; }

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateGrade();
            this.ValidateEvaluatorUser();

            return this.ValidationResult.IsValid;
        }

        public void ValidateGrade()
        {
            if (this.Grade < 0 || this.Grade > 10)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Grade, "10", "0"), new string[] { "Grade" }));
            }
        }

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
