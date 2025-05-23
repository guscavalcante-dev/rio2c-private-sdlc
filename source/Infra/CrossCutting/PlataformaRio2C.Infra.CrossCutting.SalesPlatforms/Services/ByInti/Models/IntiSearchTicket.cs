﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 06-21-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="IntiSearchTicket.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;
using System;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models
{
    public class IntiSearchTicket
    {
        [JsonProperty("unique_ticket")]
        public string UniqueTicket { get; set; }

        [JsonProperty("price_name")]
        public string PriceName { get; set; }

        [JsonProperty("event")]
        public Event Event { get; set; }

        [JsonProperty("dates")]
        public Dates Dates { get; set; }
    }

    public partial class Dates
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_datetime")]
        public DateTimeOffset StartDatetime { get; set; }

        [JsonProperty("end_datetime")]
        public DateTimeOffset EndDatetime { get; set; }
    }

    public partial class Event
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("establishment")]
        public Establishment Establishment { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("age_rating")]
        public AgeRating AgeRating { get; set; }
    }

    public partial class AgeRating
    {
        //[JsonProperty("name")]
        //public string Name { get; set; }

        //[JsonProperty("description")]
        //public string Description { get; set; }

        //[JsonProperty("image_url")]
        //public Uri ImageUrl { get; set; }
    }

    public partial class Establishment
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
