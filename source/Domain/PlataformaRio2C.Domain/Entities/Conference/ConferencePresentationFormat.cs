// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ConferencePresentationFormat.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ConferencePresentationFormat</summary>
    public class ConferencePresentationFormat : Entity
    {
        public int ConferenceId { get; private set; }
        public int PresentationFormatId { get; private set; }

        public virtual Conference Conference { get; private set; }
        public virtual PresentationFormat PresentationFormat { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ConferencePresentationFormat"/> class.</summary>
        /// <param name="conferencePresentationFormatId">The conference presentation format identifier.</param>
        /// <param name="conference">The conference.</param>
        /// <param name="presentationFormat">The presentation format.</param>
        /// <param name="userId">The user identifier.</param>
        public ConferencePresentationFormat(
            Guid conferencePresentationFormatId,
            Conference conference,
            PresentationFormat presentationFormat,
            int userId)
        {
            //this.Uid = conferencePresentationFormatId;
            this.ConferenceId = conference?.Id ?? 0;
            this.Conference = conference;
            this.PresentationFormatId = presentationFormat?.Id ?? 0;
            this.PresentationFormat = presentationFormat;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferencePresentationFormat"/> class.</summary>
        protected ConferencePresentationFormat()
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
            this.ValidatePresentationFormat();

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

        /// <summary>Validates the presentation format.</summary>
        public void ValidatePresentationFormat()
        {
            if (this.PresentationFormat == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Track), new string[] { "PresentationFormat" }));
            }
        }

        #endregion
    }
}