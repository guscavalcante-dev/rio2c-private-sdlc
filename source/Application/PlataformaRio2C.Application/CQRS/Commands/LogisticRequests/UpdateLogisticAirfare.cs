// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="UpdateLogisticAirfare.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateLogisticAirfare</summary>
    public class UpdateLogisticAirfare : CreateLogisticAirfare
    {
        public Guid Uid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAirfare"/> class.</summary>
        /// <param name="logisticAirfareDto">The logistic airfare dto.</param>
        public UpdateLogisticAirfare(LogisticAirfareDto logisticAirfareDto)
        {
            this.Uid = logisticAirfareDto?.LogisticAirfare?.Uid ?? Guid.Empty;
            this.IsNational = logisticAirfareDto?.LogisticAirfare?.IsNational;
            this.IsArrival = logisticAirfareDto?.LogisticAirfare?.IsArrival;
            this.From = logisticAirfareDto?.LogisticAirfare?.From;
            this.To = logisticAirfareDto?.LogisticAirfare?.To;
            this.Departure = logisticAirfareDto?.LogisticAirfare?.DepartureDate.ToUserTimeZone();
            this.Arrival = logisticAirfareDto?.LogisticAirfare?.ArrivalDate.ToUserTimeZone();
            this.TicketNumber = logisticAirfareDto?.LogisticAirfare?.TicketNumber;
            this.AdditionalInfo = logisticAirfareDto?.LogisticAirfare?.AdditionalInfo;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticAirfare"/> class.</summary>
        public UpdateLogisticAirfare()
        {
        }
    }
}