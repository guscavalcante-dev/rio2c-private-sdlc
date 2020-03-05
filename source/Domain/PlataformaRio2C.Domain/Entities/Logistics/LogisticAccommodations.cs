// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="AttendeePlaces.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeePlaces</summary>
    public class LogisticAccommodations : Entity
    {
        public int LogisticId { get; private set; }
        public int AttendeePlaceId { get; private set; }
        public string AdditionalInfo { get; private set; }
        public DateTimeOffset CheckInDate { get; private set; }
        public DateTimeOffset CheckOutDate { get; private set; }

        public virtual Place AttendeePlace { get; private set; }
        public virtual Logistics Logistics { get; private set; }

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