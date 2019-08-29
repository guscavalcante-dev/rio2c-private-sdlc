// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

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
            this.CreateDate = this.UpdateDate = DateTime.Now;
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
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            //attendeeOrganization.Restore();
            //this.SynchronizeAttendeeOrganizationTypes(organizationType, userId);
        }

        ///// <summary>Deletes the specified organization type.</summary>
        ///// <param name="organizationType">Type of the organization.</param>
        ///// <param name="userId">The user identifier.</param>
        //public void Delete(OrganizationType organizationType, int userId)
        //{
        //    this.UpdateDate = DateTime.Now;
        //    this.UpdateUserId = userId;

        //    foreach (var attendeeOrganizationType in this.FindAllAttendeeOrganizationTypesNotDeleted(organizationType))
        //    {
        //        attendeeOrganizationType?.Delete(userId);
        //    }

        //    if (this.FindAllAttendeeOrganizationTypesNotDeleted(organizationType)?.Any() == false)
        //    {
        //        this.IsDeleted = true;
        //    }
        //}

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        //#region Attendee Organization Types

        ///// <summary>Synchronizes the attendee organization types.</summary>
        ///// <param name="organizationType">Type of the organization.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void SynchronizeAttendeeOrganizationTypes(OrganizationType organizationType, int userId)
        //{
        //    if (this.AttendeeOrganizationTypes == null)
        //    {
        //        this.AttendeeOrganizationTypes = new List<AttendeeOrganizationType>();
        //    }

        //    if (organizationType == null)
        //    {
        //        return;
        //    }

        //    var attendeeOrganizationType = this.AttendeeOrganizationTypes.FirstOrDefault(aot => aot.OrganizationTypeId == organizationType.Id);
        //    if (attendeeOrganizationType != null)
        //    {
        //        attendeeOrganizationType.Restore(userId);
        //    }
        //    else
        //    {
        //        this.AttendeeOrganizationTypes.Add(new AttendeeOrganizationType(this, organizationType, userId));

        //    }
        //}

        ///// <summary>Finds all attendee organization types not deleted.</summary>
        ///// <param name="organizationType">Type of the organization.</param>
        ///// <returns></returns>
        //private List<AttendeeOrganizationType> FindAllAttendeeOrganizationTypesNotDeleted(OrganizationType organizationType)
        //{
        //    return this.AttendeeOrganizationTypes?.Where(aot => (organizationType == null || aot.OrganizationType.Uid == organizationType.Uid) && !aot.IsDeleted)?.ToList();
        //}

        //#endregion

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