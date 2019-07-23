// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2019
// ***********************************************************************
// <copyright file="EventbriteSalesPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using EventbriteNET;
using Eventbrite;
using PlataformaRio2C.Application.Dtos;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services
{
    /// <summary>EventbriteSalesPlatformService</summary>
    public class EventbriteSalesPlatformService : ISalesPlatformService
    {
        private readonly string appKey;
        //private readonly string userKey;
        private readonly long eventId;
        //private EventbriteContext ctx;
        private readonly SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto;

        /// <summary>Initializes a new instance of the <see cref="EventbriteSalesPlatformService"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        public EventbriteSalesPlatformService(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
        {
            this.appKey = ConfigurationManager.AppSettings["EventbriteAppKey"];
            //this.userKey = ConfigurationManager.AppSettings["EventbriteUserKey"];
            var eventIdConfiguration = ConfigurationManager.AppSettings["EventbriteEventId"];

            this.salesPlatformWebhookRequestDto = salesPlatformWebhookRequestDto;

            //if (string.IsNullOrEmpty(this.appKey))
            //{
            //    throw new Exception("Eventbrite app key is required.");
            //}

            ////if (string.IsNullOrEmpty(this.userKey))
            ////{
            ////    throw new Exception("Eventbrite user key is required.");
            ////}

            //if (!long.TryParse(eventIdConfiguration, NumberStyles.None, null, out this.eventId))
            //{
            //    throw new Exception("Eventbrite event id is required.");
            //}

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //var teste = new EventbriteApi();
            //teste.SetOAuthToken(this.appKey);


            ////var request = new Eventbrite.Requests.GetUserOwnedEventsRequest(this.eventId);
            ////var teste2 = teste.Execute(request, CancellationToken.None).Result;
            //var teste3 = 1;

            //var evt = new Eventbrite.Entities.Event();
            //evt.Id = this.eventId;
            //evt.

            //teste.Execute<Eventbrite.Entities.Event>(evt);

            // Create the context object with your API details
            //this.ctx = new EventbriteContext(this.appKey, this.userKey);
            //this.ctx = new EventbriteContext(this.appKey);

            //// Instantiate Organizer entity with the desired organizer ID
            //var organizer = context.GetOrganizer(ORGANIZER_ID_HERE);

            //// Get all the events that the organizer has created
            //var events = organizer.Events.Values;

            //// Get the first event in the collection
            //var firstEvent = events.First();

            //// All the attendees in that event
            //var attendees = firstEvent.Attendees;

            //// All the tickets in that event
            //var tickets = firstEvent.Tickets.Values;
        }

        /// <summary>Executes the request.</summary>
        public void ExecuteRequest()
        {
            if (this.salesPlatformWebhookRequestDto == null)
            {
                throw new DomainException("The webhook request is required.");
            }

            var teste = 1;
        }

        public void GetLastEvent()
        {
            //var evt = this.ctx.GetEvent();
            //var attendees = evt.Attendees;
        }
    }
}
