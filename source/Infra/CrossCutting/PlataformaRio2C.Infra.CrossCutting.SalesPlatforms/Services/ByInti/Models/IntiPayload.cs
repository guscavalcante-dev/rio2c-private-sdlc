// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 06-21-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="IntiPayload.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using System;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models
{
    public class IntiPayload
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("price_name")]
        public string PriceName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_date")]
        public string EventDate { get; set; }

        [JsonProperty("ticket_type")]
        public string TicketType { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("seat")]
        public string Seat { get; set; }

        [JsonProperty("validator_code")]
        public string ValidatorCode { get; set; }

        [JsonProperty("block_name")]
        public string BlockName { get; set; }

        [JsonProperty("completed_at")]
        public DateTime? CompletedAt { get; set; }

        [JsonProperty("canceled_at")]
        public DateTime? CancelledAt { get; set; }

        [JsonProperty("extra_values")]
        public IntiSaleOrCancellationExtraValue[] ExtraValues { get; set; }

        [JsonProperty("discount")]
        public IntiSaleOrCancellationDiscount[] Discount { get; set; }

        [JsonProperty("relationships")]
        public IntiSaleOrCancellationRelationships Relationships { get; set; }

        /// <summary>
        /// Gets the sales platform attendee status.
        /// </summary>
        /// <returns></returns>
        public string GetSalesPlatformAction()
        {
            switch (this.Action.ToLowerInvariant())
            {
                case IntiAction.TicketSold:
                case IntiAction.ParticipantUpdated:
                case IntiAction.TicketCancelled:
                    return SalesPlatformAction.AttendeeUpdated;

                // Other Updates
                default:
                    return Action;
            }
        }

        /// <summary>Gets the sales platform attendee status.</summary>
        /// <returns></returns>
        public string GetSalesPlatformAttendeeStatus()
        {
            switch (this.Action.ToLowerInvariant())
            {
                case IntiAction.TicketSold:
                case IntiAction.ParticipantUpdated:
                    return SalesPlatformAttendeeStatus.Attending;

                case IntiAction.TicketCancelled:
                    return SalesPlatformAttendeeStatus.NotAttending;

                // Other Updates
                default:
                    return this.Action;
            }
        }
    }

    /// <summary>
    /// IntiSaleOrCancellationExtraValue
    /// </summary>
    public class IntiSaleOrCancellationExtraValue
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// IntiSaleOrCancellationDiscount
    /// </summary>
    public class IntiSaleOrCancellationDiscount
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// IntiSaleOrCancellationRelationships
    /// </summary>
    public class IntiSaleOrCancellationRelationships
    {
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("event_date_id")]
        public string EventDateId { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("buyer_id")]
        public string BuyerId { get; set; }
    }
}