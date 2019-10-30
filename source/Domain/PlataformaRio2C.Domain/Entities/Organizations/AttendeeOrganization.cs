// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-29-2019
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
        public DateTime? OnboardingStartDate { get; private set; }
        public DateTime? OnboardingFinishDate { get; private set; }
        public DateTime? OnboardingOrganizationDate { get; private set; }
        public DateTime? OnboardingInterestsDate { get; private set; }
        public DateTime? ProjectSubmissionOrganizationDate { get; private set; }
        public bool IsApiDisplayEnabled { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Organization Organization { get; private set; }

        public virtual ICollection<AttendeeOrganizationType> AttendeeOrganizationTypes { get; private set; }
        public virtual ICollection<AttendeeOrganizationCollaborator> AttendeeOrganizationCollaborators { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganization"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeOrganization(Edition edition, Organization organization, OrganizationType organizationType, bool? isApiDisplayEnabled, int userId)
        {
            this.Edition = edition;
            this.Organization = organization;
            this.UpdateApiDisplay(isApiDisplayEnabled);
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
            this.UpdateApiDisplay(false);
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
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="userId">The user identifier.</param>
        public void Restore(OrganizationType organizationType, bool? isApiDisplayEnabled, int userId)
        {
            this.UpdateApiDisplay(isApiDisplayEnabled);
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.SynchronizeAttendeeOrganizationTypes(organizationType, userId);
        }

        /// <summary>Updates the API display.</summary>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        private void UpdateApiDisplay(bool? isApiDisplayEnabled)
        {
            if (!isApiDisplayEnabled.HasValue)
            {
                return;
            }

            this.IsApiDisplayEnabled = isApiDisplayEnabled.Value;
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

        #region Onboarding

        /// <summary>Called when [player].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardPlayer(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.OnboardingStartDate = this.OnboardingOrganizationDate = DateTime.Now;
        }

        /// <summary>Called when [interests].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardInterests(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.OnboardingFinishDate = this.OnboardingInterestsDate = DateTime.Now;
        }

        /// <summary>Called when [t icket buyer].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardTIcketBuyer(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.OnboardingStartDate = this.OnboardingFinishDate = this.OnboardingOrganizationDate = DateTime.Now;
        }

        /// <summary>Called when [producer].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardProducer(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.OnboardingStartDate = this.OnboardingFinishDate = this.OnboardingOrganizationDate = this.ProjectSubmissionOrganizationDate = DateTime.Now;
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