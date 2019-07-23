// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-23-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-23-2019
// ***********************************************************************
// <copyright file="Event.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>Event</summary>
    public class Event : ModelWithId
    {
        [JsonProperty("name")]
        public EventName Name { get; set; }

        [JsonProperty("description")]
        public EventDescription Description { get; set; }

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

    /// <summary>EventName</summary>
    public class EventName
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }

    /// <summary>EventDescription</summary>
    public class EventDescription
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }
}