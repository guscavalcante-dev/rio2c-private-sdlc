// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="EventbriteAttendee.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>EventbriteAttendee</summary>
    public class EventbriteAttendee : EventbriteModelWithId
    {
        [JsonProperty("Created")]
        public string Created { get; set; }

        [JsonProperty("changed")]
        public string Changed { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("profile")]
        public EventbriteAttendeeProfile Profile { get; set; }

        [JsonProperty("checked_in")]
        public bool CheckedIn { get; set; }

        [JsonProperty("cancelled")]
        public bool Cancelled { get; set; }

        [JsonProperty("refunded")]
        public bool Refunded { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("ticket_class_id")]
        public string TicketClassId { get; set; }

        [JsonProperty("ticket_class_name")]
        public string TicketClassName { get; set; }

        [JsonProperty("delivery_method")]
        public string DeliveryMethod { get; set; }

        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>Gets the sales platform attendee status.</summary>
        /// <returns></returns>
        public string GetSalesPlatformAttendeeStatus()
        {
            switch (Status.ToLowerInvariant())
            {
                case EventbriteAttendeeStatus.Attending:
                    return SalesPlatformAttendeeStatus.Attending;

                case EventbriteAttendeeStatus.NotAttending:
                    return SalesPlatformAttendeeStatus.NotAttending;

                case EventbriteAttendeeStatus.Unpaid:
                    return SalesPlatformAttendeeStatus.Unpaid;

                // Other Updates
                default:
                    return Status;
            }
        }
    }

    /// <summary>EventbriteAttendeeProfile</summary>
    public class EventbriteAttendeeProfile
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastMame { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }

        [JsonProperty("cell_phone")]
        public string CellPhone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("job_title")]
        public string JobTitle { get; set; }
    }
}