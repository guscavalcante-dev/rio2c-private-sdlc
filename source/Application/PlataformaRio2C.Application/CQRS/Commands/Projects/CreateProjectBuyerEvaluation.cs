// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-12-2019
// ***********************************************************************
// <copyright file="CreateProjectBuyerEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateProjectBuyerEvaluation</summary>
    public class CreateProjectBuyerEvaluation : BaseCommand
    {
        public Guid? ProjectUid { get; set; }
        public Guid? AttendeeOrganizationUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateProjectBuyerEvaluation"/> class.</summary>
        public CreateProjectBuyerEvaluation()
        {
        }
    }
}