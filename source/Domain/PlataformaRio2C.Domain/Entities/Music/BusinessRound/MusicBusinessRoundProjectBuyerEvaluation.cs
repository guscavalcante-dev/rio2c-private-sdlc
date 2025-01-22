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
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicBusinessRoundProjectBuyerEvaluation : Entity
    {
        public static readonly int ReasonMaxLength = 1500;

        public int MusicBusinessRoundProjectId { get; private set; }
        public int BuyerAttendeeOrganizationId { get; private set; }
        public int? ProjectEvaluationStatusId { get; private set; }
        public int? ProjectEvaluationRefuseReasonId { get; private set; }
        public string Reason { get; private set; }
        public int SellerUserId { get; private set; }
        public int? BuyerEvaluationUserId { get; private set; }
        public DateTimeOffset? EvaluationDate { get; private set; }
        public DateTimeOffset? BuyerEmailSendDate { get; private set; }

        public virtual MusicBusinessRoundProject MusicBusinessRoundProject { get; private set; }
        public virtual AttendeeOrganization BuyerAttendeeOrganization { get; private set; }
        public virtual ProjectEvaluationStatus ProjectEvaluationStatus { get; private set; }
        public virtual ProjectEvaluationRefuseReason ProjectEvaluationRefuseReason { get; private set; }
        public virtual User SellerUser { get; private set; }
        public virtual User BuyerEvaluationUser { get; private set; }

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateReason();

            return this.ValidationResult.IsValid;
        }

        private void ValidateReason()
        {
            if (this.Reason?.Trim().Length > ReasonMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(
                    string.Format(Messages.PropertyBetweenLengths, Labels.Reason, ReasonMaxLength, 1),
                    new string[] { "Reason" }));
            }
        }
    }
}
