// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 04-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-27-2023
// ***********************************************************************
// <copyright file="AttendeeNegotiationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeNegotiationCollaborator</summary>
    public class AttendeeNegotiationCollaborator : Entity
    {
        public int NegotiationId { get; private set; }
        public int AttendeeCollaboratorId { get; private set; }
       
        public Negotiation Negotiation { get; private set; }
        public AttendeeCollaborator AttendeeCollaborator { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeNegotiationCollaborator"/> class.
        /// </summary>
        /// <param name="negotiation">The negotiation.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        public AttendeeNegotiationCollaborator(
            Negotiation negotiation, 
            AttendeeCollaborator attendeeCollaborator,
            int userId)
        {
            this.Negotiation = negotiation;
            this.AttendeeCollaborator = attendeeCollaborator;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeNegotiationCollaborator"/> class.
        /// </summary>
        protected AttendeeNegotiationCollaborator()
        {
        }

        /// <summary>
        /// Updates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
            this.SetUpdateDate(userId);
        }

        #region Validations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}