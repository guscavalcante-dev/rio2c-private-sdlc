// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="HorizontalTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>HorizontalTrack</summary>
    public class HorizontalTrack : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 500;

        public string Name { get; private set; }
        public int DisplayOrder { get; private set; }

        public virtual ICollection<ConferenceHorizontalTrack> ConferenceHorizontalTracks { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="HorizontalTrack"/> class.</summary>
        /// <param name="horizontalTrackUid">The horizontal track uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="horizontalTrackNames">The horizontal track names.</param>
        /// <param name="userId">The user identifier.</param>
        public HorizontalTrack(
            Guid horizontalTrackUid,
            Edition edition,
            List<HorizontalTrackName> horizontalTrackNames,
            int userId)
        {
            //this.Uid = horizontalTrackUid;
            //this.EditionId = edition?.Id ?? 0;
            //this.Edition = edition;
            this.UpdateName(horizontalTrackNames);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="HorizontalTrack"/> class.</summary>
        protected HorizontalTrack()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="horizontalTrackNames">The horizontal track names.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            List<HorizontalTrackName> horizontalTrackNames,
            int userId)
        {
            this.UpdateName(horizontalTrackNames);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.DeleteConferencesHorizontalTracks(userId);

            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Horizontal Track Names

        /// <summary>Updates the name.</summary>
        /// <param name="horizontalTrackNames">The horizontal track names.</param>
        private void UpdateName(List<HorizontalTrackName> horizontalTrackNames)
        {
            var name = string.Empty;
            foreach (var languageCode in Language.CodesOrder)
            {
                name += (!string.IsNullOrEmpty(name) ? " " + Language.Separator + " " : String.Empty) +
                        horizontalTrackNames?.FirstOrDefault(vtc => vtc.Language.Code == languageCode)?.Value;
            }

            this.Name = name;
        }

        #endregion

        #region Conference Horizontal Tracks

        /// <summary>Deletes the conferences horizontal tracks.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferencesHorizontalTracks(int userId)
        {
            if (this.ConferenceHorizontalTracks?.Any() != true)
            {
                return;
            }

            foreach (var conferenceHorizontalTrack in this.ConferenceHorizontalTracks.Where(c => !c.IsDeleted))
            {
                conferenceHorizontalTrack.Delete(userId);
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

            this.ValidateName();

            return this.ValidationResult.IsValid;
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

        #endregion
    }
}