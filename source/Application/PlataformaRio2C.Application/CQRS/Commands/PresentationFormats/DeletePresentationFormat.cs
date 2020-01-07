// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="DeletePresentationFormat.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeletePresentationFormat</summary>
    public class DeletePresentationFormat : BaseCommand
    {
        public Guid PresentationFormatUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeletePresentationFormat"/> class.</summary>
        public DeletePresentationFormat()
        {
        }
    }
}