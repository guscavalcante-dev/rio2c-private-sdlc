// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 06-21-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="IntiSearchEvent.cs" company="Softo">
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
using System.Globalization;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models
{    

    public partial class IntiSearchEvent
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("sales_start_datetime")]
        public DateTimeOffset SalesStartDatetime { get; set; }

        [JsonProperty("map_url")]
        public object MapUrl { get; set; }

        [JsonProperty("featured")]
        public bool Featured { get; set; }

        [JsonProperty("banner")]
        public Uri Banner { get; set; }

        [JsonProperty("icon")]
        public Uri Icon { get; set; }

        [JsonProperty("age_rating")]
        public AgeRating AgeRating { get; set; }

        [JsonProperty("sponsors")]
        public List<object> Sponsors { get; set; }

        [JsonProperty("dates")]
        public List<Date> Dates { get; set; }

        [JsonProperty("categories")]
        public List<object> Categories { get; set; }

        [JsonProperty("images")]
        public List<object> Images { get; set; }

        [JsonProperty("establishment")]
        public object Establishment { get; set; }

        [JsonProperty("promotions")]
        public List<Promotion> Promotions { get; set; }
    }

    public partial class AgeRating
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_url")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("min")]
        public long Min { get; set; }

        [JsonProperty("max")]
        public long Max { get; set; }
    }

    public partial class Date
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("extra_informations")]
        public string ExtraInformations { get; set; }

        [JsonProperty("start_datetime")]
        public DateTimeOffset StartDatetime { get; set; }

        [JsonProperty("end_datetime")]
        public DateTimeOffset EndDatetime { get; set; }

        [JsonProperty("opening_time")]
        public object OpeningTime { get; set; }

        [JsonProperty("capacity")]
        public long Capacity { get; set; }
    }

    public partial class Promotion
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("available")]
        public long Available { get; set; }

        [JsonProperty("sales_start_date")]
        public DateTimeOffset SalesStartDate { get; set; }

        [JsonProperty("sales_end_date")]
        public DateTimeOffset SalesEndDate { get; set; }
    }

    public enum Status { Publicado };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Publicado")
            {
                return Status.Publicado;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            if (value == Status.Publicado)
            {
                serializer.Serialize(writer, "Publicado");
                return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
