// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ConferenceTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ConferenceTrack</summary>
    public class ConferencePillar : Entity
    {
        public int ConferenceId { get; private set; }
        public int PillarId { get; private set; }

        public virtual Conference Conference { get; private set; }
        public virtual Pillar Pillar { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ConferencePillar"/> class.</summary>
        /// <param name="conference">The conference.</param>
        /// <param name="pillar">The pillar.</param>
        /// <param name="userId">The user identifier.</param>
        public ConferencePillar(Conference conference, Pillar pillar, int userId)
        {
            this.ConferenceId = conference?.Id ?? 0;
            this.Conference = conference;
            this.PillarId = pillar?.Id ?? 0;
            this.Pillar = pillar;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceTrack"/> class.</summary>
        protected ConferencePillar()
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

            this.ValidateConference();
            this.ValidatePillar();

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

        /// <summary>Validates the track.</summary>
        public void ValidatePillar()
        {
            if (this.Pillar == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Track), new string[] { "Track" }));
            }
        }



        #endregion
    }
}