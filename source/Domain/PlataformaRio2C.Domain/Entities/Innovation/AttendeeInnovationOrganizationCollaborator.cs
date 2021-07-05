// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeInnovationOrganization.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeInnovationOrganizationCollaborator : Entity
    {
        public int AttendeeInnovationOrganizationId { get; set; }
        public int AttendeeCollaboratorId { get; set; }

        public virtual AttendeeInnovationOrganization AttendeeInnovationOrganization { get; private set; }
        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationCollaborator"/> class.</summary>
        /// <param name="attendeeInnovationOrganization">The attendee music band.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganizationCollaborator(
            AttendeeInnovationOrganization attendeeInnovationOrganization, 
            AttendeeCollaborator attendeeCollaborator, 
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.AttendeeCollaborator = attendeeCollaborator;
            this.AttendeeInnovationOrganizationId = attendeeInnovationOrganization.Id;
            this.AttendeeCollaboratorId = attendeeCollaborator.Id;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationCollaborator"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationCollaborator()
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

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
