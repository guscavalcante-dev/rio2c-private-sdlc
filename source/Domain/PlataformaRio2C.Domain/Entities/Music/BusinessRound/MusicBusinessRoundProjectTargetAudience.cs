// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-18-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectTargetAudience.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicBusinessRoundProjectTargetAudience : Entity
    {
        public MusicBusinessRoundProjectTargetAudience(
            int musicBusinessRoundProjectId,
            TargetAudience targetAudience,
            int userId,
            string additionalInfo)
        {
            this.MusicBusinessRoundProjectId = musicBusinessRoundProjectId;
            this.TargetAudienceId = targetAudience.Id;
            this.TargetAudience = targetAudience;
            this.AdditionalInfo = additionalInfo;
            this.UpdateUserId = userId;
            this.CreateUserId = userId;
            this.CreateDate = System.DateTimeOffset.Now;
        }

        public static readonly int AdditionalInfoMaxLength = 200;
        public int MusicBusinessRoundProjectId { get; private set; }
        public int TargetAudienceId { get; private set; }
        public virtual MusicBusinessRoundProject MusicBusinessRoundProject { get; private set; }
        public virtual TargetAudience TargetAudience { get; private set; }
        public string AdditionalInfo { get; private set; }
        
        public MusicBusinessRoundProjectTargetAudience()
        {       
        }

        /// <summary>Updates the music business round project target audience.</summary>
        public void Update(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

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
    }
}
