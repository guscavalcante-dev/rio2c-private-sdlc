// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Daniel Giese Rodrigues
// Created          : 01-23-2025
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 01-31-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectActivity.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

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

        /// <summary>Initializes a new instance of the <see cref="OrganizationActivity"/> class.</summary>
        /// <param name="activity">The activity.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBusinessRoundProjectActivity(Activity activity, string additionalInfo, int userId)
        {
            this.Activity = activity;
            this.ActivityId = activity?.Id ?? 0;
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundProjectActivity"/> class.</summary>
        /// <param name="activity">The activity.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBusinessRoundProjectActivity(int musicBusinessRoundProjectId, Activity activity, string additionalInfo, int userId)
        {
            this.MusicBusinessRoundProjectId = musicBusinessRoundProjectId;
            this.Activity = activity;
            this.ActivityId = activity?.Id ?? 0;
            this.AdditionalInfo = additionalInfo?.Trim();
            this.UpdateUserId = userId;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        public MusicBusinessRoundProjectActivity()
        {
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

        /// <summary>Updates the music business round project activity.</summary>
        public void Update(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }
    }
}
