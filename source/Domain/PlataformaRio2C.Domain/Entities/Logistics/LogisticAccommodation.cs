// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="LogisticAccommodation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>LogisticAccommodation</summary>
    public class LogisticAccommodation : Entity
    {
        public int LogisticId { get; private set; }
        public int AttendeePlaceId { get; private set; }
        public string AdditionalInfo { get; private set; }
        public DateTimeOffset CheckInDate { get; private set; }
        public DateTimeOffset CheckOutDate { get; private set; }

        public virtual Logistic Logistic { get; private set; }
        public virtual AttendeePlace AttendeePlace { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticAccommodation"/> class.</summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="attendeePlace">The attendee place.</param>
        /// <param name="logistic">The logistic.</param>
        /// <param name="userId">The user identifier.</param>
        public LogisticAccommodation(string additionalInfo, DateTimeOffset checkInDate, DateTimeOffset checkOutDate, AttendeePlace attendeePlace, Logistic logistic, int userId)
        {
            this.AdditionalInfo = additionalInfo;
            this.CheckInDate = checkInDate;
            this.CheckOutDate = checkOutDate;
            this.AttendeePlace = attendeePlace;
            this.Logistic = logistic;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="LogisticAccommodation"/> class.</summary>
        protected LogisticAccommodation()
        {
        }

        /// <summary>Updates the specified additional information.</summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="attendeePlace">The attendee place.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string additionalInfo, DateTimeOffset? checkInDate, DateTimeOffset? checkOutDate, AttendeePlace attendeePlace, int userId)
        {
            if (checkInDate.HasValue)
            {
                this.CheckInDate = checkInDate.Value;
            }

            if (checkOutDate.HasValue)
            {
                this.CheckOutDate = checkOutDate.Value;
            }

            this.AdditionalInfo = additionalInfo;
            this.AttendeePlace = attendeePlace;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
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

            if (this.CheckInDate == DateTimeOffset.MinValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.CheckInDate)));
            }

            if (this.CheckOutDate == DateTimeOffset.MinValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.CheckOutDate)));
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}