// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 02-04-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-04-2022
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeCartoonProjectCollaborator</summary>
    public class AttendeeCartoonProjectCollaborator : Entity
    {
        public int AttendeeCartoonProjectId { get; private set; }
        public int AttendeeCollaboratorId { get; private set; }

        public virtual AttendeeCartoonProject AttendeeCartoonProject { get; private set; }
        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProjectCollaborator"/> class.</summary>
        /// <param name="attendeeCartoonProject">The attendee cartoon project.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCartoonProjectCollaborator(AttendeeCartoonProject attendeeCartoonProject, AttendeeCollaborator attendeeCollaborator, int userId)
        {
            this.AttendeeCartoonProject = attendeeCartoonProject;
            this.AttendeeCollaborator = attendeeCollaborator;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProjectCollaborator"/> class.</summary>
        public AttendeeCartoonProjectCollaborator()
        {
        }

        /// <summary>Updates the specified attendee organization.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
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