// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-23-2021
// ***********************************************************************
// <copyright file="ProjectCommissionEvaluationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectCommissionEvaluationDto</summary>
    public class ProjectCommissionEvaluationDto
    {
        public ProjectDto ProjectDto { get; set; }
        public CommissionEvaluation CommissionEvaluation { get; set; }
        public User EvaluatorUser { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectCommissionEvaluationDto"/> class.</summary>
        public ProjectCommissionEvaluationDto()
        {
        }
    }
}