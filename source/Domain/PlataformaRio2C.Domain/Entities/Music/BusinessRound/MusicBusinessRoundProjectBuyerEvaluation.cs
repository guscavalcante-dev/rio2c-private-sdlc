// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-18-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectBuyerEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicBusinessRoundProjectBuyerEvaluation : Entity
    {
        public static readonly int ReasonMaxLength = 1500;

        public int MusicBusinessRoundProjectId { get; private set; }
        public int BuyerAttendeeCollaboratorId { get; private set; }
        public int ProjectEvaluationStatusId { get; private set; }
        public int ProjectEvaluationRefuseReasonId { get; private set; }
        public string Reason { get; private set; }
        public int SellerUserId { get; private set; }
        public int BuyerEvaluationUserId { get; private set; }
        public DateTimeOffset? EvaluationDate { get; private set; }
        public DateTimeOffset? BuyerEmailSendDate { get; private set; }

        public virtual MusicBusinessRoundProject MusicBusinessRoundProject { get; private set; }
        public virtual AttendeeCollaborator BuyerAttendeeCollaborator { get; private set; }
        public virtual ProjectEvaluationStatus ProjectEvaluationStatus { get; private set; }
        public virtual ProjectEvaluationRefuseReason ProjectEvaluationRefuseReason { get; private set; }
        public virtual User SellerUser { get; private set; }
        public virtual User BuyerEvaluationUser { get; private set; }

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            //TODO: Implement validations here

            return this.ValidationResult.IsValid;
        }
    }
}