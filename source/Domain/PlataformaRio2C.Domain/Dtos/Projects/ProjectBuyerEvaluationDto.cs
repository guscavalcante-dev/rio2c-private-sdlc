// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="ProjecProjectBuyerEvaluationDtotInterestDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectBuyerEvaluationDto</summary>
    public class ProjectBuyerEvaluationDto
    {
        public ProjectBuyerEvaluation ProjectBuyerEvaluation { get; set; }
        public AttendeeOrganizationDto BuyerAttendeeOrganizationDto { get; set; }
        public ProjectEvaluationStatus ProjectEvaluationStatus { get; set; }
        public ProjectEvaluationRefuseReason ProjectEvaluationRefuseReason { get; set; }
        public ProjectDto ProjectDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectBuyerEvaluationDto"/> class.</summary>
        public ProjectBuyerEvaluationDto()
        {
        }
    }
}