// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-30-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-12-2020
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
    public class DeleteLogisticRequest : BaseCommand
    {
        public Guid Uid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteLogisticSponsors"/> class.</summary>
        public DeleteLogisticRequest()
        {
        }   
    }
}