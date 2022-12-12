// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 11-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="SymplaParticipant.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla.Models
{
    /// <summary>
    /// SymplaPayload is 'Participant' in Sympla platform
    /// </summary>
    public class SymplaParticipant
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("order_status")]
        public string OrderStatus { get; set; }

        [JsonProperty("order_discount")]
        public object OrderDiscount { get; set; }

        [JsonProperty("ticket_number")]
        public string TicketNumber { get; set; }

        [JsonProperty("ticket_num_qr_code")]
        public string TicketNumQrCode { get; set; }

        [JsonProperty("ticket_name")]
        public string TicketName { get; set; }

        [JsonProperty("ticket_sale_price")]
        public int TicketSalePrice { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("custom_form")]
        public List<object> CustomForm { get; set; }

        [JsonProperty("checkin")]
        public List<Checkin> Checkin { get; set; }

        #region Custom Fields

        /// <summary>
        /// This property doesn't exists in original SymplaParticipant JSON. It's manually populated.
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        /// <summary>
        /// This property doesn't exists in original SymplaParticipant JSON. It's manually populated.
        /// </summary>
        [JsonProperty("updated_date")]
        public string UpdatedDateString { get; set; }

        [JsonIgnore]
        public DateTime UpdatedDate => DateTime.ParseExact(this.UpdatedDateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        /// <summary>
        /// The original payload string received by API, without deserialization.
        /// </summary>
        [JsonIgnore]
        public string PayloadString { get; set; }

        /// <summary>
        /// Gets the sales platform action.
        /// </summary>
        /// <returns></returns>
        public string GetSalesPlatformAction()
        {
            switch (this.OrderStatus.ToUpperInvariant())
            {
                case SymplaAction.TicketSold:
                case SymplaAction.ParticipantUpdated:
                case SymplaAction.TicketCancelled:
                    return SalesPlatformAction.AttendeeUpdated;

                // Other Updates
                default:
                    return this.OrderStatus;
            }
        }

        /// <summary>
        /// Gets the sales platform attendee status.
        /// </summary>
        /// <returns></returns>
        public string GetSalesPlatformAttendeeStatus()
        {
            switch (this.OrderStatus?.ToUpperInvariant())
            {
                case SymplaAction.TicketSold:
                case SymplaAction.ParticipantUpdated:
                    return SalesPlatformAttendeeStatus.Attending;

                case SymplaAction.TicketCancelled:
                    return SalesPlatformAttendeeStatus.NotAttending;

                // Other Updates
                default:
                    return this.OrderStatus;
            }
        }

        /// <summary>
        /// Updates the participant.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        public void UpdateParticipantAndCancelTicket(string firstName, string lastName, string email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;

            this.OrderStatus = SymplaAction.TicketCancelled;
            this.UpdatedDateString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        #endregion
    }

    #region Helper classes

    public class SymplaParticipantsPaged
    {
        [JsonProperty("data")]
        public List<SymplaParticipant> Participants;

        [JsonProperty("pagination")]
        public Pagination Pagination;

        [JsonProperty("sort")]
        public Sort Sort;
    }

    public class Checkin
    {
        [JsonProperty("checkin_id")]
        public int CheckinId { get; set; }

        [JsonProperty("check_in")]
        public bool CheckIn { get; set; }

        [JsonProperty("check_in_date")]
        public object CheckInDate { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("has_next")]
        public bool HasNext { get; set; }

        [JsonProperty("has_prev")]
        public bool HasPrev { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        [JsonProperty("total_page")]
        public int TotalPage { get; set; }
    }

    public class Sort
    {
        [JsonProperty("field_sort")]
        public string FieldSort { get; set; }

        [JsonProperty("sort")]
        public string SortAscDesc { get; set; }
    }

    #endregion
}