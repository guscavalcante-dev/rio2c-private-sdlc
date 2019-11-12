// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-12-2019
// ***********************************************************************
// <copyright file="DeleteProjectBuyerEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteProjectBuyerEvaluation</summary>
    public class DeleteProjectBuyerEvaluation : BaseCommand
    {
        public Guid? ProjectUid { get; set; }
        public Guid? AttendeeOrganizationUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteProjectBuyerEvaluation"/> class.</summary>
        public DeleteProjectBuyerEvaluation()
        {
        }
    }
}