﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="UpdateLogisticAirfare.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateLogisticAirfare</summary>
    public class UpdateLogisticAirfare : CreateLogisticAirfare
    {
        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAirfare"/> class.</summary>
        /// <param name="logisticAirfareDto">The logistic airfare dto.</param>
        public UpdateLogisticAirfare(LogisticAirfareDto logisticAirfareDto)
        {
            this.LogisticAirfareUid = logisticAirfareDto?.LogisticAirfare?.Uid ?? Guid.Empty;
            this.IsNational = logisticAirfareDto?.LogisticAirfare?.IsNational;
            this.IsArrival = logisticAirfareDto?.LogisticAirfare?.IsArrival;
            this.From = logisticAirfareDto?.LogisticAirfare?.From;
            this.To = logisticAirfareDto?.LogisticAirfare?.To;
            this.UpdateDate(logisticAirfareDto);
            this.TicketNumber = logisticAirfareDto?.LogisticAirfare?.TicketNumber;
            this.TicketUploadDate = logisticAirfareDto?.LogisticAirfare?.TicketUploadDate;
            this.IsTicketFileDeleted = false;
            this.AdditionalInfo = logisticAirfareDto?.LogisticAirfare?.AdditionalInfo;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAirfare"/> class.</summary>
        public UpdateLogisticAirfare()
        {
        }

        #region Private Methods

        /// <summary>Updates the date.</summary>
        /// <param name="logisticAirfareDto">The logistic airfare dto.</param>
        private void UpdateDate(LogisticAirfareDto logisticAirfareDto)
        {
            var departureDate = logisticAirfareDto?.LogisticAirfare?.DepartureDate.ToBrazilTimeZone();
            this.Departure = departureDate?.ToShortDateString() + " " + departureDate?.ToString("HH:mm");

            var arrivalDate = logisticAirfareDto?.LogisticAirfare?.ArrivalDate.ToBrazilTimeZone();
            this.Arrival = arrivalDate?.ToShortDateString() + " " + arrivalDate?.ToString("HH:mm");
        }

        #endregion
    }
}