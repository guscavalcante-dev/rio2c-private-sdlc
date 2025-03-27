// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="DeleteMusicBusinessRoundNegotiation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteMusicBusinessRoundNegotiation</summary>
    public class DeleteMusicBusinessRoundNegotiation : BaseCommand
    {
        public Guid NegotiationUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteMusicBusinessRoundNegotiation"/> class.</summary>
        public DeleteMusicBusinessRoundNegotiation()
        {
        }
    }
}