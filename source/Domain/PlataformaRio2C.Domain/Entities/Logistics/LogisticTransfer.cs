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
    public class LogisticTransfer : Entity
    {
        public int LogisticId { get; private set; }
        public int FromAttendeePlaceId { get; private set; }
        public int ToAttendeePlaceId { get; private set; }
        public string AdditionalInfo { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public virtual AttendeePlace FromAttendeePlace { get; private set; }
        public virtual AttendeePlace ToAttendeePlace { get; private set; }
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

            if (this.Date == DateTimeOffset.MinValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Date)));
            }

            return this.ValidationResult.IsValid;
        }

        #endregion

        protected LogisticTransfer()
        {
        }

        public LogisticTransfer(string additionalInfo, DateTimeOffset? date, AttendeePlace fromAttendeePlace, AttendeePlace toAttendeePlace, Logistics logistics, int userId)
        {
            this.AdditionalInfo = additionalInfo;
            this.FromAttendeePlace = fromAttendeePlace;
            this.ToAttendeePlace = toAttendeePlace;
            this.Logistics = logistics;
            if (date.HasValue)
                this.Date = date.Value;

            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        public void Update(string additionalInfo, DateTimeOffset date, AttendeePlace fromAttendeePlace, AttendeePlace toAttendeePlace, int userId)
        {
            this.AdditionalInfo = additionalInfo;
            this.Date = date;
            this.FromAttendeePlace = fromAttendeePlace;
            this.ToAttendeePlace = toAttendeePlace;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }
    }
}