// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 11-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="SymplaOrder.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla.Models
{
    public class SymplaOrder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("event_id")]
        public int EventId { get; set; }

        [JsonProperty("order_date")]
        public string OrderDate { get; set; }

        [JsonProperty("order_status")]
        public string OrderStatus { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("order_total_sale_price")]
        public int OrderTotalSalePrice { get; set; }

        [JsonProperty("buyer_first_name")]
        public string BuyerFirstName { get; set; }

        [JsonProperty("buyer_last_name")]
        public string BuyerLastName { get; set; }

        [JsonProperty("buyer_email")]
        public string BuyerEmail { get; set; }

        [JsonProperty("discount_code")]
        public object DiscountCode { get; set; }

        [JsonProperty("invoice_info")]
        public InvoiceInfo InvoiceInfo { get; set; }

        [JsonProperty("updated_date")]
        public string UpdatedDateString { get; set; }

        [JsonProperty("approved_date")]
        public string ApprovedDateString { get; set; }

        #region Custom Fields

        [JsonIgnore]
        public DateTime UpdatedDate => DateTime.ParseExact(this.UpdatedDateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        #endregion
    }

    #region Helper classes

    public class SymplaOrdersPaged
    {
        [JsonProperty("data")]
        public List<SymplaOrder> SymplaOrders;

        [JsonProperty("pagination")]
        public Pagination Pagination;

        [JsonProperty("sort")]
        public Sort Sort;
    }

    public class InvoiceInfo
    {
        [JsonProperty("doc_type")]
        public object DocType { get; set; }

        [JsonProperty("doc_number")]
        public object DocNumber { get; set; }

        [JsonProperty("client_name")]
        public object ClientName { get; set; }

        [JsonProperty("address_zip_code")]
        public object AddressZipCode { get; set; }

        [JsonProperty("address_street")]
        public object AddressStreet { get; set; }

        [JsonProperty("address_street_number")]
        public object AddressStreetNumber { get; set; }

        [JsonProperty("address_street2")]
        public object AddressStreet2 { get; set; }

        [JsonProperty("address_neighborhood")]
        public object AddressNeighborhood { get; set; }

        [JsonProperty("address_city")]
        public object AddressCity { get; set; }

        [JsonProperty("address_state")]
        public object AddressState { get; set; }

        [JsonProperty("mei")]
        public int Mei { get; set; }
    }

    #endregion
}