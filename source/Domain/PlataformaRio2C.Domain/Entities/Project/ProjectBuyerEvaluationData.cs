// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Sergio Almado Junior
// Created          : 12-03-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="ProjectBuyerEvaluationData.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectBuyerEvaluationUpdateData</summary>
    public class ProjectBuyerEvaluationData
    {
        public int ProjectEvaluationStatusId { get; set; }
        public string Reason { get; set; }
        public bool IsSent { get; set; }
        public int SellerUserId { get; set; }
        public int BuyerEvaluationUserId { get; set; }
        public DateTime EvaluationDate { get; set; }
        public Guid Uid { get; set; }
        public int BuyerAttendeeOrganizationId { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectBuyerEvaluationData"/> class.</summary>
        public ProjectBuyerEvaluationData()
        {
        }
    }
}