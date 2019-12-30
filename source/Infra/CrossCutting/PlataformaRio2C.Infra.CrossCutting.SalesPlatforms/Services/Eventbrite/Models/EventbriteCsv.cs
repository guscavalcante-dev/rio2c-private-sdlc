// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 09-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-14-2019
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
        public string Name { get; private set; }

        [JsonProperty("Sobrenome")]
        public string LastName { get; private set; }

        [JsonProperty("E-mail")]
        public string Email { get; private set; }

        [JsonProperty("Tipo de ingresso")]
        public string TicketClassName { get; private set; }

        [JsonProperty("Quantidade")]
        public int Quantity { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="EventbriteCsv"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="ticketClassName">Name of the ticket class.</param>
        /// <param name="quantity">The quantity.</param>
        public EventbriteCsv(
            string name,
            string lastName,
            string email,
            string ticketClassName,
            int quantity)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Email = email;
            this.TicketClassName = ticketClassName;
            this.Quantity = quantity;
        }
    }
}