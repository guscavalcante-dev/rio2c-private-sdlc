// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-24-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="AttendeePlaces.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeePlaces</summary>
    public class AttendeePlace : Entity
    {
        public int EditionId { get; private set; }
        public int PlaceId { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Place Place { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeePlace"/> class.</summary>
        protected AttendeePlace()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}