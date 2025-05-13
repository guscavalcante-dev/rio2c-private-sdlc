// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="Track.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Track</summary>
    public class Track : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 600;
        public static readonly int ColorMinLength = 1;
        public static readonly int ColorMaxLength = 10;

        public int EditionId { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual ICollection<ConferenceTrack> ConferenceTracks { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Track"/> class.</summary>
        /// <param name="trackUid">The track uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="trackNames">The track names.</param>
        /// <param name="color">The color.</param>
        /// <param name="userId">The user identifier.</param>
        public Track(
            Guid trackUid,
            Edition edition,
            List<TrackName> trackNames,
            string color,
            int userId)
        {
            //this.Uid = trackUid;
            this.EditionId = edition?.Id ?? 0;
            this.Edition = edition;
            this.UpdateName(trackNames);
            this.Color = color?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Track"/> class.</summary>
        protected Track()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="trackNames">The track names.</param>
        /// <param name="color">The color.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            List<TrackName> trackNames,
            string color,
            int userId)
        {
            this.UpdateName(trackNames);
            this.Color = color?.Trim();

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.DeleteConferencesTracks(userId);

            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Track Names

        /// <summary>Gets the name by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameByLanguageCode(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, '|');
        }

        /// <summary>Updates the name.</summary>
        /// <param name="trackNames">The track names.</param>
        private void UpdateName(List<TrackName> trackNames)
        {
            var name = string.Empty;
            foreach (var languageCode in Language.CodesOrder)
            {
                name += (!string.IsNullOrEmpty(name) ? " " + Language.Separator + " " : String.Empty) +
                        trackNames?.FirstOrDefault(vtc => vtc.Language.Code == languageCode)?.Value;
            }

            this.Name = name;
        }

        #endregion

        #region Conference Tracks

        /// <summary>Deletes the conferences tracks.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferencesTracks(int userId)
        {
            if (this.ConferenceTracks?.Any() != true)
            {
                return;
            }

            foreach (var conferenceTrack in this.ConferenceTracks.Where(c => !c.IsDeleted))
            {
                conferenceTrack.Delete(userId);
            }
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateEdition();
            this.ValidateName();
            this.ValidateColor();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the edition.</summary>
        public void ValidateEdition()
        {
            if (this.Edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Edition), new string[] { "Edition" }));
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
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Descriptions, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        /// <summary>Validates the color.</summary>
        public void ValidateColor()
        {
            if (string.IsNullOrEmpty(this.Color?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Color), new string[] { "Color" }));
            }

            if (this.Color?.Trim().Length < ColorMinLength || this.Color?.Trim().Length > ColorMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Descriptions, ColorMaxLength, ColorMinLength), new string[] { "Color" }));
            }
        }

        #endregion
    }
}