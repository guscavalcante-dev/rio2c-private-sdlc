// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-30-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="DeleteLogistic.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteLogistic</summary>
    public class DeleteLogistic : BaseCommand
    {
        public Guid Uid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteLogistic"/> class.</summary>
        public DeleteLogistic()
        {
        }
    }
}