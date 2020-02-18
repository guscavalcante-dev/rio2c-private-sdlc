// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
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
        public bool IsApiDisplayEnabled { get; private set; }
        public int? ApiHighlightPosition { get; private set; }

        public virtual AttendeeOrganization AttendeeOrganization { get; private set; }
        public virtual OrganizationType OrganizationType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationType"/> class.</summary>
        /// <param name="attendeeOrganization">The attendee organization.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeOrganizationType(
            AttendeeOrganization attendeeOrganization, 
            OrganizationType organizationType, 
            bool? isApiDisplayEnabled, 
            int? apiHighlightPosition, 
            int userId)
        {
            this.AttendeeOrganization = attendeeOrganization;
            this.OrganizationType = organizationType;
            this.UpdateApiConfigurations(isApiDisplayEnabled, apiHighlightPosition);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganization"/> class.</summary>
        protected AttendeeOrganizationType()
        {
        }

        /// <summary>Updates the specified is API display enabled.</summary>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(bool? isApiDisplayEnabled, int? apiHighlightPosition, int userId)
        {
            this.UpdateApiConfigurations(isApiDisplayEnabled, apiHighlightPosition);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.UpdateApiConfigurations(false, null);

            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the API highlight position.</summary>
        /// <param name="userId">The user identifier.</param>
        public void DeleteApiHighlightPosition(int userId)
        {
            this.ApiHighlightPosition = null;

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Updates the API configurations.</summary>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        private void UpdateApiConfigurations(bool? isApiDisplayEnabled, int? apiHighlightPosition)
        {
            if (!isApiDisplayEnabled.HasValue)
            {
                return;
            }

            this.IsApiDisplayEnabled = isApiDisplayEnabled.Value;
            this.ApiHighlightPosition = isApiDisplayEnabled.Value ? apiHighlightPosition : null;
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