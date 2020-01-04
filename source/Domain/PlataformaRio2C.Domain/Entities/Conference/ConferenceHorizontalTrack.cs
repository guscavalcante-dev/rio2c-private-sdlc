// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="ConferenceHorizontalTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ConferenceHorizontalTrack</summary>
    public class ConferenceHorizontalTrack : Entity
    {
        public int ConferenceId { get; private set; }
        public int HorizontalTrackId { get; private set; }

        public virtual Conference Conference { get; private set; }
        public virtual HorizontalTrack HorizontalTrack { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceHorizontalTrack"/> class.</summary>
        /// <param name="conferenceHorizontalTrackId">The conference horizontal track identifier.</param>
        /// <param name="conference">The conference.</param>
        /// <param name="horizontalTrack">The horizontal track.</param>
        /// <param name="userId">The user identifier.</param>
        public ConferenceHorizontalTrack(
            Guid conferenceHorizontalTrackId,
            Conference conference,
            HorizontalTrack horizontalTrack,
            int userId)
        {
            //this.Uid = conferenceHorizontalTrackId;
            this.ConferenceId = conference?.Id ?? 0;
            this.Conference = conference;
            this.HorizontalTrackId = horizontalTrack?.Id ?? 0;
            this.HorizontalTrack = horizontalTrack;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceHorizontalTrack"/> class.</summary>
        protected ConferenceHorizontalTrack()
        {
        }

        /// <summary>Updates the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateConference();
            this.ValidateHorizontalTrack();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the conference.</summary>
        public void ValidateConference()
        {
            if (this.Conference == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Conference), new string[] { "Conference" }));
            }
        }

        /// <summary>Validates the horizontal track.</summary>
        public void ValidateHorizontalTrack()
        {
            if (this.HorizontalTrack == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Track), new string[] { "HorizontalTrack" }));
            }
        }

        #endregion
    }
}