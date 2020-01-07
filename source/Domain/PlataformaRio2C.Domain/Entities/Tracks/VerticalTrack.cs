// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="VerticalTrack.cs" company="Softo">
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
    /// <summary>VerticalTrack</summary>
    public class VerticalTrack : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 500;

        public string Name { get; private set; }
        public int DisplayOrder { get; private set; }

        public virtual ICollection<ConferenceVerticalTrack> ConferenceVerticalTracks { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="VerticalTrack"/> class.</summary>
        /// <param name="verticalTrackUid">The vertical track uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="verticalTrackNames">The vertical track names.</param>
        /// <param name="userId">The user identifier.</param>
        public VerticalTrack(
            Guid verticalTrackUid,
            Edition edition,
            List<VerticalTrackName> verticalTrackNames,
            int userId)
        {
            //this.Uid = verticalTrackUid;
            //this.EditionId = edition?.Id ?? 0;
            //this.Edition = edition;
            this.UpdateName(verticalTrackNames);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="VerticalTrack"/> class.</summary>
        protected VerticalTrack()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="verticalTrackNames">The vertical track names.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            List<VerticalTrackName> verticalTrackNames,
            int userId)
        {
            this.UpdateName(verticalTrackNames);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.DeleteConferencesVerticalTracks(userId);

            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Vertical Track Names

        /// <summary>Updates the name.</summary>
        /// <param name="verticalTrackNames">The vertical track names.</param>
        private void UpdateName(List<VerticalTrackName> verticalTrackNames)
        {
            var name = string.Empty;
            foreach (var languageCode in Language.CodesOrder)
            {
                name += (!string.IsNullOrEmpty(name) ? " "  + Language.Separator + " " : String.Empty) + 
                        verticalTrackNames?.FirstOrDefault(vtc => vtc.Language.Code == languageCode)?.Value;
            }

            this.Name = name;
        }

        #endregion

        #region Conference Vertical Tracks

        /// <summary>Deletes the conferences vertical tracks.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferencesVerticalTracks(int userId)
        {
            if (this.ConferenceVerticalTracks?.Any() != true)
            {
                return;
            }

            foreach (var conferenceVerticalTrack in this.ConferenceVerticalTracks.Where(c => !c.IsDeleted))
            {
                conferenceVerticalTrack.Delete(userId);
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