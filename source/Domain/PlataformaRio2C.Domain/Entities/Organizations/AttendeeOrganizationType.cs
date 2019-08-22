// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-21-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeOrganizationType</summary>
    public class AttendeeOrganizationType : Entity
    {
        public int AttendeeOrganizationId { get; private set; }
        public int OrganizationTypeId { get; private set; }

        public virtual AttendeeOrganization AttendeeOrganization { get; private set; }
        public virtual OrganizationType OrganizationType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationType"/> class.</summary>
        /// <param name="attendeeOrganization">The attendee organization.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeOrganizationType(AttendeeOrganization attendeeOrganization, OrganizationType organizationType, int userId)
        {
            this.AttendeeOrganization = attendeeOrganization;
            this.OrganizationType = organizationType;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganization"/> class.</summary>
        protected AttendeeOrganizationType()
        {
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Restores the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Restore(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
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