// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="MusicBandMember.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>MusicBandMember</summary>
    public class MusicBandMember : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 300;
        public static readonly int MusicInstrumentNameMinLength = 1;
        public static readonly int MusicInstrumentNameMaxLength = 100;

        public int MusicBandId { get; private set; }
        public string Name { get; private set; }
        public string MusicInstrumentName { get; private set; }

        public virtual MusicBand MusicBand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandMember"/> class.
        /// </summary>
        /// <param name="musicBandId">The music band identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="musicInstrumentName">Name of the music instrument.</param>
        public MusicBandMember(MusicBand musicBand, string name, string musicInstrumentName, int userId)
        {
            this.MusicBand = musicBand;
            this.Name = name;
            this.MusicInstrumentName = musicInstrumentName;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandMember"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="musicInstrumentName">Name of the music instrument.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBandMember(string name, string musicInstrumentName, int userId)
        {
            this.Name = name;
            this.MusicInstrumentName = musicInstrumentName;

            this.SetCreateDate(userId);
        }
        /// <summary>Initializes a new instance of the <see cref="MusicBandMember"/> class.</summary>
        protected MusicBandMember()
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
            this.ValidateMusicInstrumentName();

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

        /// <summary>Validates the name of the music instrument.</summary>
        public void ValidateMusicInstrumentName()
        {
            if (string.IsNullOrEmpty(this.MusicInstrumentName?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.MusicInstrumentName), new string[] { "MusicInstrumentName" }));
            }

            if (this.MusicInstrumentName?.Trim().Length < NameMinLength || this.MusicInstrumentName?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.MusicInstrumentName, MusicInstrumentNameMaxLength, MusicInstrumentNameMinLength), new string[] { "MusicInstrumentName" }));
            }
        }

        #endregion
    }
}