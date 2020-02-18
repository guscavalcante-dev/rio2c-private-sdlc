// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="TicketType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CollaboratorGender</summary>
    public class CollaboratorEditionParticipation : Entity
    {
        public int CollaboratorId { get; private set; }
        public int EditionId { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Collaborator Collaborator { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorEditionParticipation"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public CollaboratorEditionParticipation(Edition edition, Collaborator collaborator, int userId)
        {
            this.EditionId = edition.Id;
            this.Edition = edition;
            this.CollaboratorId = collaborator.Id;
            this.Collaborator = collaborator;

            this.IsDeleted = false;
            this.CreateDate = UpdateDate = DateTime.UtcNow;
            this.CreateUserId = UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorType"/> class.</summary>
        private CollaboratorEditionParticipation()
        {
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        internal void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Undeletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        internal void Undelete(int userId)
        {
            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();
            
            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}