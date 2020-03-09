// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="DeleteNegotiationConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteNegotiationConfig</summary>
    public class DeleteNegotiationConfig : BaseCommand
    {
        public Guid NegotiationConfigUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteNegotiationConfig"/> class.</summary>
        public DeleteNegotiationConfig()
        {
        }
    }
}