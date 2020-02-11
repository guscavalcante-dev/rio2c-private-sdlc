// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="DeletePillar.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeletePillar</summary>
    public class DeletePillar : BaseCommand
    {
        public Guid PillarUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeletePillar"/> class.</summary>
        public DeletePillar()
        {
        }
    }
}