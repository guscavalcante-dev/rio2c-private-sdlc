// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 09-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="EventbriteCsv.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>EventbriteCsv</summary>
    public class EventbriteCsv
    {
        //Nome,Sobrenome,E-mail,Tipo de ingresso,Quantidade

        [JsonProperty("Nome")]
        public string Name { get; set; }

        [JsonProperty("Sobrenome")]
        public string LastName { get; set; }

        [JsonProperty("E-mail")]
        public string Email { get; set; }

        [JsonProperty("Tipo de ingresso")]
        public string TicketClassName { get; set; }

        [JsonProperty("Quantidade")]
        public int Quantity { get; set; }
    }
}