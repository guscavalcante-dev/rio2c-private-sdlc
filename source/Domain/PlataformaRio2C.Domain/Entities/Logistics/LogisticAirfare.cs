// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="LogisticAirfare.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>LogisticAirfare</summary>
    public class LogisticAirfare : Entity
    {
        public int LogisticId { get; private set; }
        public bool IsNational { get; private set; }
        public bool IsArrival { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string TicketNumber { get; private set; }
        public string AdditionalInfo { get; private set; }
        public DateTimeOffset DepartureDate { get; private set; }
        public DateTimeOffset ArrivalDate { get; private set; }
        public DateTimeOffset? TicketUploadDate { get; private set; }

        public virtual Logistics Logistics { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticAirfare"/> class.</summary>
        /// <param name="logistics">The logistics.</param>
        /// <param name="isNational">The is national.</param>
        /// <param name="isArrival">The is arrival.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="ticketNumber">The ticket number.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <param name="userId">The user identifier.</param>
        public LogisticAirfare(Logistics logistics, bool? isNational, bool? isArrival, string from, string to, string ticketNumber, string additionalInfo, DateTimeOffset? departureDate, DateTimeOffset? arrivalDate, int userId)
        {
            this.Logistics = logistics;
            this.IsNational = isNational ?? false;
            this.IsArrival = isArrival ?? false;
            this.From = from?.Trim();
            this.To = to?.Trim();

            if (departureDate.HasValue)
            {
                this.DepartureDate = departureDate.Value;
            }

            if (arrivalDate.HasValue)
            {
                this.ArrivalDate = arrivalDate.Value;
            }

            this.TicketNumber = ticketNumber?.Trim();
            this.AdditionalInfo = additionalInfo?.Trim();

            this.Logistics = logistics;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="LogisticAirfare"/> class.</summary>
        protected LogisticAirfare()
        {
        }

        /// <summary>Updates the specified is national.</summary>
        /// <param name="isNational">The is national.</param>
        /// <param name="isArrival">The is arrival.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="ticketNumber">The ticket number.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(bool? isNational, bool? isArrival, string from, string to, string ticketNumber, string additionalInfo, DateTimeOffset? departureDate, DateTimeOffset? arrivalDate, int userId)
        {
            this.IsNational = isNational ?? false;
            this.IsArrival = isArrival ?? false;
            this.From = from?.Trim();
            this.To = to?.Trim();

            if (departureDate.HasValue)
            {
                this.DepartureDate = departureDate.Value;
            }

            if (arrivalDate.HasValue)
            {
                this.ArrivalDate = arrivalDate.Value;
            }

            this.TicketNumber = ticketNumber?.Trim();
            this.AdditionalInfo = additionalInfo?.Trim();

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
    }
}