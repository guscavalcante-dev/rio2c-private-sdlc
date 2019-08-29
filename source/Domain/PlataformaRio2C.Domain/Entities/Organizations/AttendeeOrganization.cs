// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-29-2019
// ***********************************************************************
// <copyright file="AttendeeOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeOrganization</summary>
    public class AttendeeOrganization : Entity
    {
        public int EditionId { get; private set; }
        public int OrganizationId { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Organization Organization { get; private set; }

        public virtual ICollection<AttendeeOrganizationType> AttendeeOrganizationTypes { get; private set; }
        public virtual ICollection<AttendeeOrganizationCollaborator> AttendeeOrganizationCollaborators { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganization"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeOrganization(Edition edition, Organization organization, OrganizationType organizationType, int userId)
        {
            this.Edition = edition;
            this.Organization = organization;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeAttendeeOrganizationTypes(organizationType, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganization"/> class.</summary>
        protected AttendeeOrganization()
        {
        }

        /// <summary>Deletes the specified organization type.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(OrganizationType organizationType, int userId)
        {
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.DeleteOrganizationType(organizationType, userId);
            this.DeleteAttendeeOrganizationCollaborators(userId);

            if (this.FindAllAttendeeOrganizationTypesNotDeleted(organizationType)?.Any() != true
                && this.FindAllAttendeeOrganizationCollaboratorsNotDeleted()?.Any() != true)
            {
                this.IsDeleted = true;
            }
        }

        /// <summary>Restores the specified organization type.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void Restore(OrganizationType organizationType, int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.SynchronizeAttendeeOrganizationTypes(organizationType, userId);
        }

        #region Attendee Organization Types

        /// <summary>Synchronizes the attendee organization types.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeOrganizationTypes(OrganizationType organizationType, int userId)
        {
            if (this.AttendeeOrganizationTypes == null)
            {
                this.AttendeeOrganizationTypes = new List<AttendeeOrganizationType>();
            }

            if (organizationType == null)
            {
                return;
            }

            var attendeeOrganizationType = this.AttendeeOrganizationTypes.FirstOrDefault(aot => aot.OrganizationTypeId == organizationType.Id);
            if (attendeeOrganizationType != null)
            {
                attendeeOrganizationType.Restore(userId);
            }
            else
            {
                this.AttendeeOrganizationTypes.Add(new AttendeeOrganizationType(this, organizationType, userId));
            }
        }

        /// <summary>Deletes the type of the organization.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteOrganizationType(OrganizationType organizationType, int userId)
        {
            foreach (var attendeeOrganizationType in this.FindAllAttendeeOrganizationTypesNotDeleted(organizationType))
            {
                attendeeOrganizationType?.Delete(userId);
            }
        }

        /// <summary>Finds all attendee organization types not deleted.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <returns></returns>
        private List<AttendeeOrganizationType> FindAllAttendeeOrganizationTypesNotDeleted(OrganizationType organizationType)
        {
            return this.AttendeeOrganizationTypes?.Where(aot => (organizationType == null || aot.OrganizationType.Uid == organizationType.Uid) && !aot.IsDeleted)?.ToList();
        }

        #endregion

        #region Attendee Organization Collaborators

        /// <summary>Deletes the attendee organization collaborators.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeOrganizationCollaborators(int userId)
        {
            foreach (var attendeeOrganizationCollaborator in this.FindAllAttendeeOrganizationCollaboratorsNotDeleted())
            {
                attendeeOrganizationCollaborator.Delete(userId);
            }
        }

        /// <summary>Finds all attendee organization collaborators not deleted.</summary>
        /// <returns></returns>
        private List<AttendeeOrganizationCollaborator> FindAllAttendeeOrganizationCollaboratorsNotDeleted()
        {
            return this.AttendeeOrganizationCollaborators?.Where(aoc => !aoc.IsDeleted)?.ToList();
        }

        #endregion

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