// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicBandGenre.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>MusicBandGenre</summary>
    public class MusicBandGenre : Entity
    {
        public static readonly int AdditionalInfoMinLength = 1;
        public static readonly int AdditionalInfoMaxLength = 200;

        public int MusicBandId { get; private set; }
        public int MusicGenreId { get; private set; }
        public string AdditionalInfo { get; private set; }

        public virtual MusicBand MusicBand { get; private set; }
        public virtual MusicGenre MusicGenre { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandGenre"/> class.
        /// </summary>
        /// <param name="musicBand">The music band.</param>
        /// <param name="musicGenre">The music genre.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBandGenre(MusicBand musicBand, MusicGenre musicGenre, string additionalInfo, int userId)
        {
            this.MusicBand = musicBand;
            this.MusicGenre = musicGenre;
            this.AdditionalInfo = additionalInfo;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBandGenre"/> class.</summary>
        protected MusicBandGenre()
        {
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
            this.ValidateAdditionalInfo();

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
            if (this.MusicGenre == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.MusicGenre), new string[] { "MusicGenreId" }));
            }
        }

        /// <summary>Validates the additional information.</summary>
        public void ValidateAdditionalInfo()
        {
            if (!this.MusicGenre.HasAdditionalInfo)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.AdditionalInfo?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.AdditionalInfo), new string[] { "AdditionalInfo" }));
            }

            if (this.AdditionalInfo?.Trim().Length < AdditionalInfoMinLength || this.AdditionalInfo?.Trim().Length > AdditionalInfoMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AdditionalInfo, AdditionalInfoMaxLength, AdditionalInfoMinLength), new string[] { "AdditionalInfo" }));
            }
        }

        #endregion
    }
}