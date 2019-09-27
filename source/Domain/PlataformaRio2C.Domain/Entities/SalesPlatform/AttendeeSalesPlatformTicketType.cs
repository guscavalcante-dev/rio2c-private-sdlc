// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="AttendeeSalesPlatformTicketType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeSalesPlatformTicketType</summary>
    public class AttendeeSalesPlatformTicketType : Entity
    {
        public static readonly int TicketClassIdMinLength = 1;
        public static readonly int TicketClassIdMaxLength = 30;
        public static readonly int TicketClassNameMinLength = 1;
        public static readonly int TicketClassNameMaxLength = 200;

        public int AttendeeSalesPlatformId { get; private set; }
        public int CollaboratorTypeId { get; private set; }
        public string TicketClassId { get; private set; }
        public string TicketClassName { get; private set; }

        public virtual AttendeeSalesPlatform AttendeeSalesPlatform { get; private set; }
        public virtual CollaboratorType CollaboratorType { get; private set; }

        public virtual ICollection<AttendeeCollaboratorTicket> AttendeeCollaboratorTickets { get; private set; }

        //public virtual ICollection<AttendeeOrganizationCollaborator> AttendeeOrganizationCollaborators { get; private set; }

        //public AttendeeSalesPlatform(Edition edition, List<AttendeeOrganization> attendeeOrganizations, Collaborator collaborator, int userId)
        //{
        //    this.Edition = edition;
        //    this.Collaborator = collaborator;
        //    this.IsDeleted = false;
        //    this.CreateDate = this.UpdateDate = DateTime.Now;
        //    this.CreateUserId = this.UpdateUserId = userId;
        //    this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, userId);
        //}

        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformTicketType"/> class.</summary>
        protected AttendeeSalesPlatformTicketType()
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
            this.ValidationResult = new ValidationResult();

            this.ValidateTicketClassId();
            this.ValidateTicketClassName();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the ticket class identifier.</summary>
        public void ValidateTicketClassId()
        {
            if (string.IsNullOrEmpty(this.TicketClassId?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Ticket Class Id"), new string[] { "TicketClassId" }));
            }

            if (this.TicketClassId?.Trim().Length < TicketClassIdMinLength || this.TicketClassId?.Trim().Length > TicketClassIdMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Ticket Class Id", TicketClassIdMaxLength, TicketClassIdMinLength), new string[] { "TicketClassId" }));
            }
        }

        /// <summary>Validates the name of the ticket class.</summary>
        public void ValidateTicketClassName()
        {
            if (string.IsNullOrEmpty(this.TicketClassName?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Ticket Class"), new string[] { "TicketClassName" }));
            }

            if (this.TicketClassName?.Trim().Length < TicketClassNameMinLength || this.TicketClassName?.Trim().Length > TicketClassNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Ticket Class", TicketClassNameMaxLength, TicketClassNameMinLength), new string[] { "TicketClassName" }));
            }
        }

        #endregion
    }
}