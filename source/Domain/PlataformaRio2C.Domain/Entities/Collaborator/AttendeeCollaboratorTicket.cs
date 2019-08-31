// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicket.cs" company="Softo">
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
    /// <summary>AttendeeCollaboratorTicket</summary>
    public class AttendeeCollaboratorTicket : Entity
    {
        public static readonly int SalesPlatformAttendeeIdMinLength = 1;
        public static readonly int SalesPlatformAttendeeIdMaxLength = 40;
        public static readonly int FirstNameMinLength = 1;
        public static readonly int FirstNameMaxLength = 100;
        public static readonly int LastNamesMinLength = 1;
        public static readonly int LastNamesMaxLength = 200;
        public static readonly int CellPhoneMinLength = 1;
        public static readonly int CellPhoneMaxLength = 30;
        public static readonly int JobTitleMinLength = 1;
        public static readonly int JobTitleMaxLength = 30;

        public int AttendeeCollaboratorId { get; private set; }
        public int AttendeeSalesPlatformTicketTypeId { get; private set; }
        public string SalesPlatformAttendeeId { get; private set; }
        public string FirstName { get; private set; }
        public string LastNames { get; private set; }
        public string CellPhone { get; private set; }
        public string JobTitle { get; private set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; private set; }

        //public AttendeeSalesPlatform(Edition edition, List<AttendeeOrganization> attendeeOrganizations, Collaborator collaborator, int userId)
        //{
        //    this.Edition = edition;
        //    this.Collaborator = collaborator;
        //    this.IsDeleted = false;
        //    this.CreateDate = this.UpdateDate = DateTime.Now;
        //    this.CreateUserId = this.UpdateUserId = userId;
        //    this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, userId);
        //}

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTicket"/> class.</summary>
        protected AttendeeCollaboratorTicket()
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

            this.ValidateSalesPlatformAttendeeId();
            this.ValidateFirstName();
            this.ValidateLastNames();
            this.ValidateCellPhone();
            this.ValidateJobTitle();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the sales platform attendee identifier.</summary>
        public void ValidateSalesPlatformAttendeeId()
        {
            if (string.IsNullOrEmpty(this.SalesPlatformAttendeeId?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Attendee Id"), new string[] { "SalesPlatformAttendeeId" }));
            }

            if (this.SalesPlatformAttendeeId?.Trim().Length < SalesPlatformAttendeeIdMinLength || this.SalesPlatformAttendeeId?.Trim().Length > SalesPlatformAttendeeIdMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Attendee Id", SalesPlatformAttendeeIdMaxLength, SalesPlatformAttendeeIdMinLength), new string[] { "SalesPlatformAttendeeId" }));
            }
        }

        /// <summary>Validates the first name.</summary>
        public void ValidateFirstName()
        {
            if (string.IsNullOrEmpty(this.FirstName?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Ticket Class"), new string[] { "FirstName" }));
            }

            if (this.FirstName?.Trim().Length < FirstNameMinLength || this.FirstName?.Trim().Length > FirstNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, FirstNameMaxLength, FirstNameMinLength), new string[] { "FirstName" }));
            }
        }

        /// <summary>Validates the last names.</summary>
        public void ValidateLastNames()
        {
            if (this.LastNames?.Trim().Length < LastNamesMinLength || this.LastNames?.Trim().Length > LastNamesMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.LastNames, LastNamesMaxLength, LastNamesMinLength), new string[] { "LastNames" }));
            }
        }

        /// <summary>Validates the cell phone.</summary>
        public void ValidateCellPhone()
        {
            if (this.CellPhone?.Trim().Length < CellPhoneMinLength || this.CellPhone?.Trim().Length > CellPhoneMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.CellPhone, CellPhoneMaxLength, CellPhoneMinLength), new string[] { "CellPhone" }));
            }
        }

        /// <summary>Validates the job title.</summary>
        public void ValidateJobTitle()
        {
            if (this.JobTitle?.Trim().Length < JobTitleMinLength || this.JobTitle?.Trim().Length > JobTitleMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.JobTitle, JobTitleMaxLength, JobTitleMinLength), new string[] { "JobTitle" }));
            }
        }

        #endregion
    }
}