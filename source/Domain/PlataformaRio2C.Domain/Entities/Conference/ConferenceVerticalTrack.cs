// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="ConferenceVerticalTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ConferenceVerticalTrack</summary>
    public class ConferenceVerticalTrack : Entity
    {
        public int ConferenceId { get; private set; }
        public int VerticalTrackId { get; private set; }

        public virtual Conference Conference { get; private set; }
        public virtual VerticalTrack VerticalTrack { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceVerticalTrack"/> class.</summary>
        /// <param name="conferenceVerticalTrackId">The conference vertical track identifier.</param>
        /// <param name="conference">The conference.</param>
        /// <param name="verticalTrack">The vertical track.</param>
        /// <param name="userId">The user identifier.</param>
        public ConferenceVerticalTrack(
            Guid conferenceVerticalTrackId,
            Conference conference,
            VerticalTrack verticalTrack,
            int userId)
        {
            //this.Uid = conferenceVerticalTrackId;
            this.ConferenceId = conference?.Id ?? 0;
            this.Conference = conference;
            this.VerticalTrackId = verticalTrack?.Id ?? 0;
            this.VerticalTrack = verticalTrack;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceVerticalTrack"/> class.</summary>
        protected ConferenceVerticalTrack()
        {
        }

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
            this.ValidateVerticalTrack();

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

        /// <summary>Validates the vertical track.</summary>
        public void ValidateVerticalTrack()
        {
            if (this.VerticalTrack == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Track), new string[] { "VerticalTrack" }));
            }
        }

        #endregion
    }
}