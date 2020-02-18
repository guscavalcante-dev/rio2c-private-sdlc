// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="AttendeeOrganizationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeOrganizationCollaborator</summary>
    public class AttendeeOrganizationCollaborator : Entity
    {
        public int AttendeeOrganizationId { get; private set; }
        public int AttendeeCollaboratorId { get; private set; }

        public virtual AttendeeOrganization AttendeeOrganization { get; private set; }
        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationCollaborator"/> class.</summary>
        /// <param name="attendeeOrganization">The attendee organization.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeOrganizationCollaborator(AttendeeOrganization attendeeOrganization, AttendeeCollaborator attendeeCollaborator, int userId)
        {
            this.AttendeeOrganization = attendeeOrganization;
            this.AttendeeCollaborator = attendeeCollaborator;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationCollaborator"/> class.</summary>
        public AttendeeOrganizationCollaborator()
        {
        }

        /// <summary>Updates the specified attendee organization.</summary>
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
            return true;
        }

        #endregion
    }
}