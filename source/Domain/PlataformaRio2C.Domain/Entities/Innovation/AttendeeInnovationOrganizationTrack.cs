﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-12-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-04-2022
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeInnovationOrganizationTrack.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeInnovationOrganizationTrack : Entity
    {
        public int AttendeeInnovationOrganizationId { get; set; }
        public int InnovationOrganizationTrackOptionId { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual AttendeeInnovationOrganization AttendeeInnovationOrganization { get; private set; }
        public virtual InnovationOrganizationTrackOption InnovationOrganizationTrackOption { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrack"/> class.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="innovationOrganizationTrackOption">The innovation organization track option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganizationTrack(
            AttendeeInnovationOrganization attendeeInnovationOrganization,
            InnovationOrganizationTrackOption innovationOrganizationTrackOption,
            string additionalInfo,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.InnovationOrganizationTrackOption = innovationOrganizationTrackOption;
            this.AdditionalInfo = additionalInfo;

            this.AttendeeInnovationOrganizationId = attendeeInnovationOrganization?.Id ?? 0;
            this.InnovationOrganizationTrackOptionId = innovationOrganizationTrackOption?.Id ?? 0;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrack"/> class.
        /// </summary>
        /// <param name="innovationOrganizationTrackOption">The innovation organization track option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganizationTrack(
            InnovationOrganizationTrackOption innovationOrganizationTrackOption,
            string additionalInfo,
            int userId)
        {
            this.InnovationOrganizationTrackOption = innovationOrganizationTrackOption;
            this.InnovationOrganizationTrackOptionId = innovationOrganizationTrackOption?.Id ?? 0;
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrack"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationTrack()
        {

        }

        /// <summary>
        /// Updates the specified attendee innovation organization.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="innovationOrganizationTrackOption">The innovation organization track option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            AttendeeInnovationOrganization attendeeInnovationOrganization,
            InnovationOrganizationTrackOption innovationOrganizationTrackOption,
            string additionalInfo,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.InnovationOrganizationTrackOption = innovationOrganizationTrackOption;
            this.AdditionalInfo = additionalInfo;

            this.AttendeeInnovationOrganizationId = attendeeInnovationOrganization?.Id ?? 0;
            this.InnovationOrganizationTrackOptionId = innovationOrganizationTrackOption?.Id ?? 0;

            this.SetUpdateDate(userId);
        }

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}
