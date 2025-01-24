// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Daniel Giese Rodrigues
// Created          : 01-23-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-23-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectActivity.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicBusinessRoundProjectActivity : Entity
    {
        public static readonly int AdditionalInfoMaxLength = 200;

        public int MusicBusinessRoundProjectId { get; private set; }
        public int ActivityId { get; private set; }
        public virtual MusicBusinessRoundProject MusicBusinessRoundProject { get; private set; }
        public virtual Activity Activity { get; private set; }
        public string AdditionalInfo { get; private set; }

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateAdditionalInfo();

            return this.ValidationResult.IsValid;
        }

        private void ValidateAdditionalInfo()
        {
            if (this.AdditionalInfo?.Trim().Length > AdditionalInfoMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(
                    string.Format(Messages.PropertyBetweenLengths, Labels.AdditionalInfo, AdditionalInfoMaxLength, 1),
                    new string[] { "AdditionalInfo" }));
            }
        }

        // Factory method to set the private properties
        public static MusicBusinessRoundProjectActivity Create(
            int musicBusinessRoundProjectId,
            int activityId,
            string additionalInfo)
        {
            return new MusicBusinessRoundProjectActivity
            {
                MusicBusinessRoundProjectId = musicBusinessRoundProjectId,
                ActivityId = activityId,
                AdditionalInfo = additionalInfo
            };
        }
    }
}
