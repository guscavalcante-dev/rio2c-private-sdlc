// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationEvaluationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationEvaluationDto</summary>
    public class AttendeeInnovationOrganizationEvaluationDto
    {
        public InnovationOrganization InnovationOrganization { get; set; }
        public AttendeeInnovationOrganization AttendeeInnovationOrganization { get; set; }
        public AttendeeInnovationOrganizationEvaluation AttendeeInnovationOrganizationEvaluation { get; set; }
        public User EvaluatorUser { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationEvaluationDto"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationEvaluationDto()
        {
        }
    }
}