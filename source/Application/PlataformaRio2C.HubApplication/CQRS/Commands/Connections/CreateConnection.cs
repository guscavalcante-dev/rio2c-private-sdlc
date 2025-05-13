// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="CreateConnection.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.CQRS.Commands;
using System;

namespace PlataformaRio2C.HubApplication.CQRS.Commands
{
    /// <summary>CreateConnection</summary>
    public class CreateConnection : BaseCommand
    {
        public Guid ConnectionId { get; set; }
        public string UserName { get; set; }
        public string UserAgent { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateConnection"/> class.</summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="userAgent">The user agent.</param>
        public CreateConnection(
            Guid connectionId,
            string userName,
            string userAgent)
        {
            this.ConnectionId = connectionId;
            this.UserName = userName;
            this.UserAgent = userAgent;
        }
    }
}