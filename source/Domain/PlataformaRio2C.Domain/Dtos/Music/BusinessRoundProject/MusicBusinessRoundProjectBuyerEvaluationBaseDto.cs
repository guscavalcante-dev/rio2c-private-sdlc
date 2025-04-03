// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese Rodrigues
// Created          : 03-14-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 03-14-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectBuyerEvaluationBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBusinessRoundProjectBuyerEvaluationBaseDto</summary>
    public class MusicBusinessRoundProjectBuyerEvaluationBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTimeOffset? EvaluationDate { get; set; }
        public DateTimeOffset? BuyerEmailSendDate { get; set; }
        public int SellerUserId { get; set; }
        public int? BuyerEvaluationUserId { get; set; }
        public string Reason { get; set; }
        public MusicBusinessRoundProjectBaseDto MusicBusinessProjectBaseDto { get; set; }
        public AttendeeOrganizationBaseDto BuyerAttendeeOrganizationBaseDto { get; set; }
        public AttendeeOrganizationBaseDto SellerAttendeeOrganizationBaseDto { get; set; }
        public ProjectEvaluationStatusBaseDto ProjectEvaluationStatusBaseDtos { get; set; }
        public ProjectEvaluationRefuseReasonBaseDto ProjectEvaluationRefuseReasonBaseDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundProjectBuyerEvaluationBaseDto"/> class.</summary>
        public MusicBusinessRoundProjectBuyerEvaluationBaseDto()
        {
        }
    }
}
