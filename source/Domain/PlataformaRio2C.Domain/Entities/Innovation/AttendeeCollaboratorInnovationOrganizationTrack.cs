﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-12-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-12-2021
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInnovationOrganizationTrack.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeCollaboratorInnovationOrganizationTrack.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeCollaboratorInnovationOrganizationTrack : Entity
    {
        public int AttendeeCollaboratorId { get; set; }
        public int InnovationOrganizationTrackOptionId { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual InnovationOrganizationTrackOption InnovationOrganizationTrackOption { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorInnovationOrganizationTrack"/> class.
        /// </summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="innovationOrganizationTrackOption">The innovation organization track option.</param>
        /// <param name="addtionalInfo">The addtional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorInnovationOrganizationTrack(
            AttendeeCollaborator attendeeCollaborator,
            InnovationOrganizationTrackOption innovationOrganizationTrackOption,
            string addtionalInfo,
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.InnovationOrganizationTrackOption = innovationOrganizationTrackOption;

            this.AttendeeCollaboratorId = attendeeCollaborator?.Id ?? 0;
            this.InnovationOrganizationTrackOptionId = innovationOrganizationTrackOption?.Id ?? 0;

            this.AdditionalInfo = addtionalInfo;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorInnovationOrganizationTrack"/> class.
        /// </summary>
        /// <param name="innovationOrganizationTrackOption">The innovation organization track option.</param>
        /// <param name="addtionalInfo">The addtional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorInnovationOrganizationTrack(
            InnovationOrganizationTrackOption innovationOrganizationTrackOption,
            string addtionalInfo,
            int userId)
        {
            this.InnovationOrganizationTrackOption = innovationOrganizationTrackOption;
            this.InnovationOrganizationTrackOptionId = innovationOrganizationTrackOption?.Id ?? 0;
            this.AdditionalInfo = addtionalInfo;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorInnovationOrganizationTrack"/> class.
        /// </summary>
        public AttendeeCollaboratorInnovationOrganizationTrack()
        {
        }

        /// <summary>
        /// Updates the specified user identifier.
        /// </summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string additionalInfo, int userId)
        {
            this.AdditionalInfo = additionalInfo;

            this.SetUpdateDate(userId);
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
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
