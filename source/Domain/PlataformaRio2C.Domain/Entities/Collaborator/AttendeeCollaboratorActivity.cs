// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 12-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorActivity.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class AttendeeCollaboratorActivity : Entity
    {
        public int AttendeeCollaboratorId { get; set; }
        public int ActivityId { get; set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual Activity Activity { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorActivity"/> class.
        /// </summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorActivity(
            AttendeeCollaborator attendeeCollaborator,
            Activity activity,
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.Activity = activity;

            this.AttendeeCollaboratorId = attendeeCollaborator?.Id ?? 0;
            this.ActivityId = activity?.Id ?? 0;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorActivity"/> class.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorActivity(
            Activity activity,
            string additionalInfo,
            int userId)
        {
            this.Activity = activity;
            this.ActivityId = activity?.Id ?? 0;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorActivity"/> class.
        /// </summary>
        public AttendeeCollaboratorActivity()
        {
        }

        /// <summary>
        /// Updates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
            this.IsDeleted = false;
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
