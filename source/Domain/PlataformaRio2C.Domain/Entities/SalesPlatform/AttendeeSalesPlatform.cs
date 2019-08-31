// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="AttendeeSalesPlatform.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeSalesPlatform</summary>
    public class AttendeeSalesPlatform : Entity
    {
        public int EditionId { get; private set; }
        public int SalesPlatformId { get; private set; }
        public string SalesPlatformEventid { get; private set; }
        public bool IsActive { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual SalesPlatform SalesPlatform { get; private set; }

        public virtual ICollection<AttendeeSalesPlatformTicketType> AttendeeSalesPlatformTicketTypes { get; private set; }

        //public AttendeeSalesPlatform(Edition edition, List<AttendeeOrganization> attendeeOrganizations, Collaborator collaborator, int userId)
        //{
        //    this.Edition = edition;
        //    this.Collaborator = collaborator;
        //    this.IsDeleted = false;
        //    this.CreateDate = this.UpdateDate = DateTime.Now;
        //    this.CreateUserId = this.UpdateUserId = userId;
        //    this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, userId);
        //}

        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatform"/> class.</summary>
        protected AttendeeSalesPlatform()
        {
        }

        ///// <summary>Updates the specified edition.</summary>
        ///// <param name="edition">The edition.</param>
        ///// <param name="attendeeOrganizations">The attendee organizations.</param>
        ///// <param name="userId">The user identifier.</param>
        //public void Update(Edition edition, List<AttendeeOrganization> attendeeOrganizations, int userId)
        //{
        //    this.IsDeleted = false;
        //    this.UpdateDate = DateTime.Now;
        //    this.UpdateUserId = userId;
        //    this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, userId);
        //}

        ///// <summary>Deletes the specified user identifier.</summary>
        ///// <param name="userId">The user identifier.</param>
        //public void Delete(int userId)
        //{
        //    this.IsDeleted = true;
        //    this.UpdateDate = DateTime.Now;
        //    this.UpdateUserId = userId;
        //    this.DeleteAttendeeOrganizationCollaborators(new List<AttendeeOrganization>(), userId);
        //}

        //#region Attendee Organization Collaborators

        ///// <summary>Synchronizes the attendee organization collaborators.</summary>
        ///// <param name="attendeeOrganizations">The attendee organizations.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void SynchronizeAttendeeOrganizationCollaborators(List<AttendeeOrganization> attendeeOrganizations, int userId)
        //{
        //    if (this.AttendeeOrganizationCollaborators == null)
        //    {
        //        this.AttendeeOrganizationCollaborators = new List<AttendeeOrganizationCollaborator>();
        //    }

        //    this.DeleteAttendeeOrganizationCollaborators(attendeeOrganizations, userId);

        //    if (attendeeOrganizations?.Any() != true)
        //    {
        //        return;
        //    }

        //    // Create or update descriptions
        //    foreach (var attendeeOrganizaion in attendeeOrganizations)
        //    {
        //        var attendeeOrganizationCollaboratorDb = this.AttendeeOrganizationCollaborators.FirstOrDefault(aoc => aoc.AttendeeOrganizationId == attendeeOrganizaion.Id);
        //        if (attendeeOrganizationCollaboratorDb != null)
        //        {
        //            attendeeOrganizationCollaboratorDb.Update(userId);
        //        }
        //        else
        //        {
        //            this.CreateAttendeeOrganizationCollaborator(attendeeOrganizaion, userId);
        //        }
        //    }
        //}

        ///// <summary>Deletes the attendee organization collaborators.</summary>
        ///// <param name="newAttendeeOrganizations">The new attendee organizations.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void DeleteAttendeeOrganizationCollaborators(List<AttendeeOrganization> newAttendeeOrganizations, int userId)
        //{
        //    var attendeeOrganizationCollaboratorToDelete = this.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted 
        //                                                                                                       && newAttendeeOrganizations?.Select(nao => nao.Id)?.Contains(aoc.AttendeeOrganizationId) == false)
        //                                                                                         .ToList();
        //    foreach (var attendeeOrganizationCollaborator in attendeeOrganizationCollaboratorToDelete)
        //    {
        //        attendeeOrganizationCollaborator.Delete(userId);
        //    }
        //}

        ///// <summary>Creates the attendee organization collaborator.</summary>
        ///// <param name="attendeeOrganization">The attendee organization.</param>
        ///// <param name="userId">The user identifier.</param>
        //private void CreateAttendeeOrganizationCollaborator(AttendeeOrganization attendeeOrganization, int userId)
        //{
        //    this.AttendeeOrganizationCollaborators.Add(new AttendeeOrganizationCollaborator(attendeeOrganization, this, userId));
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