// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicBandTargetAudience.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>MusicBandTargetAudience</summary>
    public class MusicBandTargetAudience : Entity
    {
        public int MusicBandId { get; private set; }
        public int TargetAudienceId { get; private set; }

        public virtual MusicBand MusicBand { get; private set; }
        public virtual TargetAudience TargetAudience { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandTargetAudience"/> class.</summary>
        /// <param name="musicBand">The music band.</param>
        /// <param name="targetAudience">The target audience.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBandTargetAudience(MusicBand musicBand, TargetAudience targetAudience, int userId)
        {
            this.MusicBand = musicBand;
            this.MusicBandId = musicBand?.Id ?? 0;
            this.TargetAudience = targetAudience;
            this.TargetAudienceId = targetAudience?.Id ?? 0;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBandTargetAudience"/> class.</summary>
        protected MusicBandTargetAudience()
        {
        }

        /// <summary>Updates the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateMusicBand();
            this.ValidateMusicGenre();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the music band.</summary>
        public void ValidateMusicBand()
        {
            if (this.MusicBand == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.MusicBand), new string[] { "MusicBandId" }));
            }
        }

        /// <summary>Validates the music genre.</summary>
        public void ValidateMusicGenre()
        {
            if (this.TargetAudience == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.TargetAudience), new string[] { "TargetAudienceId" }));
            }
        }

        #endregion
    }
}