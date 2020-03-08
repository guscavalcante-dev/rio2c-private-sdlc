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
    public class LogisticAirfare : Entity
    {
        public int LogisticId { get; private set; }
        public bool IsNational { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string TicketNumber { get; private set; }
        public string AdditionalInfo { get; private set; }
        public DateTimeOffset DepartureDate { get; private set; }
        public DateTimeOffset ArrivalDate { get; private set; }
        public DateTimeOffset? TicketUploadDate { get; private set; }

        protected LogisticAirfare()
        {
        }

        public virtual Logistics Logistics { get; private set; }

        public LogisticAirfare(Logistics logistics, bool? isNational, string from, string to, string ticketNumber, string additionalInfo, DateTimeOffset? departureDate, DateTimeOffset? arrivalDate, int userId)
        {
            if (departureDate.HasValue)
                this.DepartureDate = departureDate.Value;

            if (arrivalDate.HasValue)
                this.ArrivalDate = arrivalDate.Value;

            if(isNational.HasValue)
                this.IsNational = isNational.Value;

            this.Logistics = logistics;
            this.From = from;
            this.To = to;
            this.TicketNumber = ticketNumber;
            this.AdditionalInfo = additionalInfo;
                
            this.Logistics = logistics;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
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

            if (this.ArrivalDate == DateTimeOffset.MinValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.ArrivalDate)));
            }

            if (this.DepartureDate == DateTimeOffset.MinValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.DepartureDate)));
            }

            if (this.Logistics == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Logistics)));
            }

            return this.ValidationResult.IsValid;
        }

        #endregion

        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        public void Update(bool? isNational, string from, string to, string ticketNumber, string additionalInfo, DateTimeOffset? departureDate, DateTimeOffset? arrivalDate, int userId)
        {
            if (departureDate.HasValue)
                this.DepartureDate = departureDate.Value;

            if (arrivalDate.HasValue)
                this.ArrivalDate = arrivalDate.Value;

            if (isNational.HasValue)
                this.IsNational = isNational.Value;
            
            this.From = from;
            this.To = to;
            this.TicketNumber = ticketNumber;
            this.AdditionalInfo = additionalInfo;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }
    }
}