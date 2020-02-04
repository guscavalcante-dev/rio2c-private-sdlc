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

        /// <summary>Initializes a new instance of the <see cref="CollaboratorType"/> class.</summary>
        private CollaboratorEditionParticipation()
        {
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