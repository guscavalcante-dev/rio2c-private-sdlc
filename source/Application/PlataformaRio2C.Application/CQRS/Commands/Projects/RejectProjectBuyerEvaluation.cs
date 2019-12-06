// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-12-2019
// ***********************************************************************
// <copyright file="RejectProjectBuyerEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>RejectProjectBuyerEvaluation</summary>
    public class RejectProjectBuyerEvaluation : BaseCommand
    {
        public Guid? ProjectUid { get; set; }
        public ProjectBuyerEvaluationData ProjectBuyerEvaluationData { get; set; }


        /// <summary>Initializes a new instance of the <see cref="UpdateProjectBuyerEvaluation"/> class.</summary>
        public RejectProjectBuyerEvaluation(ProjectBuyerEvaluationData projectBuyerEvaluationData)
        {
            this.ProjectBuyerEvaluationData = projectBuyerEvaluationData;
        }
    }
}