// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeCollaboratorType</summary>
    public class AttendeeCollaboratorType : Entity
    {
        public int AttendeeCollaboratorId { get; private set; }
        public int CollaboratorTypeId { get; private set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual CollaboratorType CollaboratorType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorType"/> class.</summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorType(
            AttendeeCollaborator attendeeCollaborator, 
            CollaboratorType collaboratorType, 
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.CollaboratorType = collaboratorType;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId= userId;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorType"/> class.</summary>
        protected AttendeeCollaboratorType()
        {
        }

        /// <summary>Updates the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
            if (!this.IsDeleted)
            {
                return;
            }

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.ValidateAttendeeCollaborator();
            this.ValidateCollaboratorType();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the attendee collaborator.</summary>
        public void ValidateAttendeeCollaborator()
        {
            if (this.AttendeeCollaborator == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Attendee Collaborator")));
            }
        }

        /// <summary>Validates the type of the collaborator.</summary>
        public void ValidateCollaboratorType()
        {
            if (this.CollaboratorType == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Collaborator Type")));
            }
        }

        #endregion
    }
}