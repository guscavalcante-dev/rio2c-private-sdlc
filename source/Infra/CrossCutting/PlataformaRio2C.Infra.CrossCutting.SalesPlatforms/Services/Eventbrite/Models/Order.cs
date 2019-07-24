// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-24-2019
// ***********************************************************************
// <copyright file="Order.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>Order</summary>
    public class Order : ModelWithId
    {
        [JsonProperty("Created")]
        public string Created { get; set; }

        [JsonProperty("changed")]
        public string Changed { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time_remaining")]
        public string TimeRemaining { get; set; }

        [JsonProperty("event_id")]
        public string EventId { get; set; }
    }
}