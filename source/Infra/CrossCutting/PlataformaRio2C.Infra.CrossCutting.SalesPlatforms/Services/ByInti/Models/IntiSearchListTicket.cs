// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 06-21-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="IntiSearchListTicket.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models
{
    class IntiSearchListTicket
    {
        [JsonProperty("tickets")]
        public List<Ticket> Tickets { get; set; }


        public partial class Ticket
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("event")]
            public Event Event { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("price_name")]
            public string PriceName { get; set; }

            [JsonProperty("amount")]
            public string Amount { get; set; }

            [JsonProperty("seat_location")]
            public SeatLocation SeatLocation { get; set; }

            [JsonProperty("chair_name")]
            public string ChairName { get; set; }
        }

        public partial class Event
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("dateTime")]
            public DateTimeOffset DateTime { get; set; }

            [JsonProperty("endDateTime")]
            public DateTimeOffset EndDateTime { get; set; }
        }

        public partial class SeatLocation
        {
            [JsonProperty("sector")]
            public object Sector { get; set; }

            [JsonProperty("region")]
            public object Region { get; set; }

            [JsonProperty("row")]
            public object Row { get; set; }

            [JsonProperty("number")]
            public object Number { get; set; }
        }
    }
}

