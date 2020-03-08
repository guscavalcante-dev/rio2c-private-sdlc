// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-27-2020
// ***********************************************************************
// <copyright file="CreateConference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateLogisticSponsors</summary>
    public class UpdateLogisticAirfare : CreateLogisticAirfare
    {
        public Guid Uid { get; set; }


        public UpdateLogisticAirfare(Guid uid, string from, string to, bool? isNational, DateTimeOffset? departure, DateTimeOffset? arrival, string ticketNumber, string additionalInfo)
        {
            this.Uid = uid;
            this.From = from;
            this.To = to;
            this.IsNational = isNational;
            this.Departure = departure;
            this.Arrival = arrival;
            this.TicketNumber = ticketNumber;
            this.AdditionalInfo = additionalInfo;
        }

        public UpdateLogisticAirfare()
        {
        }

        #region Private Methods


        #endregion
    }
}