// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="EventbriteEvent.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>EventbriteEvent</summary>
    public class EventbriteEvent : EventbriteModelWithId
    {
        [JsonProperty("name")]
        public EventbriteEventName Name { get; set; }

        [JsonProperty("description")]
        public EventbriteEventDescription Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        //[JsonProperty("start")]
        //public string Start { get; set; }

        //[JsonProperty("End")]
        //public string End { get; set; }

        [JsonProperty("Created")]
        public string Created { get; set; }

        [JsonProperty("changed")]
        public string Changed { get; set; }

        [JsonProperty("capacity")]
        public int? Capacity { get; set; }
    }

    /// <summary>EventbriteEventName</summary>
    public class EventbriteEventName
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }

    /// <summary>EventbriteEventDescription</summary>
    public class EventbriteEventDescription
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }
}