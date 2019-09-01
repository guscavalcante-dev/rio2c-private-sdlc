// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="EventbriteOrder.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>EventbriteOrder</summary>
    public class EventbriteOrder : EventbriteModelWithId
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

        [JsonProperty("attendees")]
        public List<EventbriteAttendee> Attendees { get; set; }
    }
}