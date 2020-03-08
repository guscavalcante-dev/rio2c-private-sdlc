// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-30-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-30-2020
// ***********************************************************************
// <copyright file="DeleteLogisticSponsors.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteLogisticSponsors</summary>
    public class DeleteLogisticAirfare : BaseCommand
    {
        public Guid Uid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteLogisticSponsors"/> class.</summary>
        public DeleteLogisticAirfare()
        {
        }   
    }
}