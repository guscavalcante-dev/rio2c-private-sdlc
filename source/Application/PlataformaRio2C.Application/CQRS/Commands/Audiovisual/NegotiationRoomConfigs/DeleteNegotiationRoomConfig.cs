// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-05-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="DeleteNegotiationRoomConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteNegotiationRoomConfig</summary>
    public class DeleteNegotiationRoomConfig : BaseCommand
    {
        public Guid? NegotiationConfigUid { get; set; }
        public Guid? NegotiationRoomConfigUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteNegotiationRoomConfig"/> class.</summary>
        public DeleteNegotiationRoomConfig()
        {
        }
    }
}