// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="ProjectBuyerEvaluationEmailDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectBuyerEvaluationEmailDto</summary>
    public class ProjectBuyerEvaluationEmailDto
    {
        public ProjectBuyerEvaluation ProjectBuyerEvaluation { get; set; }
        public Project Project { get; set; }
        public AttendeeOrganization SellerAttendeeOrganization { get; set; }
        public Organization SellerOrganization { get; set; }
        public IEnumerable<EmailRecipientDto> EmailRecipientsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectBuyerEvaluationEmailDto"/> class.</summary>
        public ProjectBuyerEvaluationEmailDto()
        {
        }
    }
}