// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="ReleasedMusicProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ReleasedMusicProject</summary>
    public class ReleasedMusicProject : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 200;
        public static readonly int YearMaxLength = 300;

        public int MusicBandId { get; private set; }
        public string Name { get; private set; }
        public string Year { get; private set; }

        public virtual MusicBand MusicBand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleasedMusicProject"/> class.
        /// </summary>
        /// <param name="musicBand">The music band.</param>
        /// <param name="name">The name.</param>
        /// <param name="year">The year.</param>
        /// <param name="userId">The user identifier.</param>
        public ReleasedMusicProject(MusicBand musicBand, string name, string year, int userId)
        {
            this.MusicBand = musicBand;
            this.Name = name;
            this.Year = year;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ReleasedMusicProject"/> class.</summary>
        protected ReleasedMusicProject()
        {
        }

        /// <summary>Gets the name abbreviation.</summary>
        /// <returns></returns>
        public string GetNameAbbreviation()
        {
            return this.Name?.GetTwoLetterCode();
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateMusicBand();
            this.ValidateName();
            this.ValidateYear();

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

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        /// <summary>Validates the year.</summary>
        public void ValidateYear()
        {
            if (!string.IsNullOrEmpty(this.Year) && this.Year?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Year, YearMaxLength, 1), new string[] { "Year" }));
            }
        }

        #endregion
    }
}