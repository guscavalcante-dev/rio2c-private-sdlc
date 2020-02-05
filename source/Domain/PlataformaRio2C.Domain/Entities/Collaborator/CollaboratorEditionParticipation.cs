// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="TicketType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CollaboratorGender</summary>
    public class CollaboratorEditionParticipation : Entity
    {
        public int CollaboratorId { get; private set; }
        public int EditionId { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Collaborator Collaborator { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorType"/> class.</summary>
        private CollaboratorEditionParticipation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorEditionParticipation"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaborator">The collaborator.</param>
        public CollaboratorEditionParticipation(Edition edition, Collaborator collaborator, int userId)
        {
            this.EditionId = edition.Id;
            this.Edition = edition;
            this.CollaboratorId = collaborator.Id;
            this.Collaborator = collaborator;
            this.CreateDate = UpdateDate = DateTime.Now;
            this.CreateUserId = UpdateUserId = userId;
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

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        internal void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        internal void Undelete(int userId)
        {
            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.Now;
        }

        #endregion
    }
}