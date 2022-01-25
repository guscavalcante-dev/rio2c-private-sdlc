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
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Validation;

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

        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProject"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="musicBand">The music band.</param>
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
            base.Delete(userId);
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();
            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}