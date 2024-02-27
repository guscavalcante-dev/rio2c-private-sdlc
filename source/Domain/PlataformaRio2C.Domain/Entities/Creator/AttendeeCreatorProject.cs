// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-26-2024
// ***********************************************************************
// <copyright file="AttendeeCreatorProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    public class AttendeeCreatorProject : Entity
    {
        public int CreatorProjectId { get; private set; }
        public int EditionId { get; private set; }
        public decimal Grade { get; private set; }
        public int EvaluationsCount { get; private set; }
        public DateTimeOffset LastEvaluationDate { get; private set; }
        public DateTimeOffset EvaluationEmailSendDate { get; private set; }

        public virtual CreatorProject CreatorProject { get; private set; }
        public virtual Edition Edition { get; private set; }
        public virtual ICollection<AttendeeCreatorProjectEvaluation> AttendeeCreatorProjectEvaluations { get; private set; }

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}
