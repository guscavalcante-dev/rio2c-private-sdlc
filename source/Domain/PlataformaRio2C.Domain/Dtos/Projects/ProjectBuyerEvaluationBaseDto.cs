// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-24-2021
// ***********************************************************************
// <copyright file="ProjectBuyerEvaluationBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectBuyerEvaluationBaseDto</summary>
    public class ProjectBuyerEvaluationBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTimeOffset? EvaluationDate { get; set; }
        public DateTimeOffset? BuyerEmailSendDate { get; set; }
        public int SellerUserId { get; set; }
        public int? BuyerEvaluationUserId { get; set; }
        public string Reason { get; set; }

        public ProjectBaseDto ProjectBaseDto { get; set; }
        public AttendeeOrganizationBaseDto BuyerAttendeeOrganizationBaseDto { get; set; }
        public ProjectEvaluationStatusBaseDto ProjectEvaluationStatusBaseDtos { get; set; }
        public ProjectEvaluationRefuseReasonBaseDto ProjectEvaluationRefuseReasonBaseDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectBuyerEvaluationBaseDto"/> class.</summary>
        public ProjectBuyerEvaluationBaseDto()
        {
        }
    }
}