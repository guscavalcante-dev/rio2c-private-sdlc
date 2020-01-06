// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="DeleteConference.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteRoom</summary>
    public class DeleteRoom : BaseCommand
    {
        public Guid RoomUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteRoom"/> class.</summary>
        public DeleteRoom()
        {
        }
    }
}