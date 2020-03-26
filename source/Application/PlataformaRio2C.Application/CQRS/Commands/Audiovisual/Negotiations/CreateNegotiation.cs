// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-23-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="CreateNegotiation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateNegotiation</summary>
    public class CreateNegotiation : BaseCommand
    {
        public Guid? BuyerOrganizationUid { get; set; }
        public Guid? ProjectUid { get; set; }
        public Guid? NegotiationConfigUid { get; set; }
        public Guid? NegotiationRoomConfigUid { get; set; }
        public string StartTime { get; set; }
        public int? RoundNumber { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateNegotiation"/> class.</summary>
        public CreateNegotiation()
        {
        }
    }
}