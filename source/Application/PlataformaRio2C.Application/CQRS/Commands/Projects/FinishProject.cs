// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-12-2019
// ***********************************************************************
// <copyright file="FinishProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>FinishProject</summary>
    public class FinishProject : BaseCommand
    {
        public Guid? ProjectUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="FinishProject"/> class.</summary>
        public FinishProject()
        {
        }
    }
}