// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="Conference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Conference</summary>
    public class Conference : Entity
    {
        //public static readonly int LocalMinLength = 2;
        //public static readonly int LocalMaxLength = 1000;
        //public static readonly int InfoMaxLength = 3000;

        public int EditionId { get; private set; }
        public int? RoomId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        //public string Info { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Room Room { get; private set; }

        public virtual ICollection<ConferenceTitle> ConferenceTitles { get; private set; }
        public virtual ICollection<ConferenceSynopsis> ConferenceSynopses { get; private set; }
        //public virtual ICollection<ConferenceLecturer> Lecturers { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Conference"/> class.</summary>
        protected Conference()
        {

        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateEdition();
            this.ValidateDates();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the edition.</summary>
        public void ValidateEdition()
        {
            if (this.Edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Edition), new string[] { "Edition" }));
            }
        }

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            if (this.StartDate < this.Edition.StartDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.StartDate), new string[] { "StartDate" }));
            }

            if (this.EndDate > this.Edition.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.EndDate), new string[] { "EndDate" }));
            }

            if (this.StartDate > this.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.StartDate), new string[] { "StartDate" }));
            }
        }

        #endregion
    }
}