// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="DeleteConnection.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.CQRS.Commands;
using System;

namespace PlataformaRio2C.HubApplication.CQRS.Commands
{
    /// <summary>DeleteConnection</summary>
    public class DeleteConnection : BaseCommand
    {
        public Guid ConnectionId { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteConnection"/> class.</summary>
        /// <param name="connectionId">The connection identifier.</param>
        public DeleteConnection(Guid connectionId)
        {
            this.ConnectionId = connectionId;
        }
    }
}