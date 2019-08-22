// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-21-2019
// ***********************************************************************
// <copyright file="DeleteHolding.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteHolding</summary>
    public class DeleteHolding : BaseCommand
    {
        public Guid HoldingUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteHolding"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public DeleteHolding(HoldingDto entity)
        {
            this.HoldingUid = entity.Uid;
        }

        /// <summary>Initializes a new instance of the <see cref="DeleteHolding"/> class.</summary>
        public DeleteHolding()
        {
        }
    }
}
