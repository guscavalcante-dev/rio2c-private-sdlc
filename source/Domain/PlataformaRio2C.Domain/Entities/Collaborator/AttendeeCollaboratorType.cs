// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="AttendeeCollaboratorType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeCollaboratorType</summary>
    public class AttendeeCollaboratorType : Entity
    {
        public int AttendeeCollaboratorId { get; private set; }
        public int CollaboratorTypeId { get; private set; }
        public bool IsApiDisplayEnabled { get; private set; }
        public int? ApiHighlightPosition { get; private set; }
        public DateTimeOffset? TermsAcceptanceDate { get; private set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual CollaboratorType CollaboratorType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorType"/> class.</summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorType(
            AttendeeCollaborator attendeeCollaborator,
            CollaboratorType collaboratorType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.CollaboratorType = collaboratorType;
            this.UpdateApiConfigurations(isApiDisplayEnabled, apiHighlightPosition);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorType"/> class.</summary>
        protected AttendeeCollaboratorType()
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