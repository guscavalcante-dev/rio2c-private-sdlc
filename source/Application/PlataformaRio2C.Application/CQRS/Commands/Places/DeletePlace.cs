// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="DeletePlace.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeletePlace</summary>
    public class DeletePlace : BaseCommand
    {
        public Guid PlaceUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeletePlace"/> class.</summary>
        public DeletePlace()
        {
        }
    }
}