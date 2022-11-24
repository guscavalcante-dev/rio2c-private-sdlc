// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 11-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="SymplaPayload.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla.Models
{
    /// <summary>
    /// SymplaPayload is 'Participant' in Sympla platform
    /// </summary>
    public class SymplaPayload
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
        /// The original payload received by API, without deserialization.
        /// </summary>
        public string OriginalPayload { get; set; }

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

        #endregion
    }

    #region Helper classes

    public class SymplaPagedPayload
    {
        [JsonProperty("data")]
        public List<SymplaPayload> Payloads;

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