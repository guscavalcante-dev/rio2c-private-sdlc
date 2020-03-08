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
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeePlaces</summary>
    public class LogisticAccommodation : Entity
    {
        public int LogisticId { get; private set; }
        public int AttendeePlaceId { get; private set; }
        public string AdditionalInfo { get; private set; }
        public DateTimeOffset CheckInDate { get; private set; }
        public DateTimeOffset CheckOutDate { get; private set; }

        public virtual AttendeePlace AttendeePlace { get; private set; }
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

        protected LogisticAccommodation()
        {
        }

        public LogisticAccommodation(string additionalInfo, DateTimeOffset checkInDate, DateTimeOffset checkOutDate, AttendeePlace attendeePlace, Logistics logistics, int userId)
        {
            this.AdditionalInfo = additionalInfo;
            this.CheckInDate = checkInDate;
            this.CheckOutDate = checkOutDate;
            this.AttendeePlace = attendeePlace;
            this.Logistics = logistics;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        public void Update(string additionalInfo, DateTimeOffset? checkInDate, DateTimeOffset? checkOutDate, AttendeePlace attendeePlace, int userId)
        {
            if(checkInDate.HasValue)
                this.CheckInDate = checkInDate.Value;
            if(checkOutDate.HasValue)
                this.CheckOutDate = checkOutDate.Value;

            this.AdditionalInfo = additionalInfo;
            this.AttendeePlace = attendeePlace;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

    }
}