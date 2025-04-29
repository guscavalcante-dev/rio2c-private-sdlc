// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 25-02-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 25-02-2025
// ***********************************************************************
// <copyright file="AttendeeMusicBusinessRoundNegotiationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    public class AttendeeMusicBusinessRoundNegotiationCollaborator : Entity
    {
        public AttendeeMusicBusinessRoundNegotiationCollaborator()
        {
                
        }

        public int MusicBusinessRoundNegotiationId { get; private set; }
        public int AttendeeCollaboratorId { get; private set; }


        public MusicBusinessRoundNegotiation MusicBusinessRoundNegotiation { get; private set; }
        public AttendeeCollaborator AttendeeCollaborator { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeMusicBusinessRoundNegotiationCollaborator" /> class.
        /// </summary>
        /// <param name="musicBusinessRoundNegotiation">The music business round negotiation.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="userId">The user identifier responsible for creation.</param>
        public AttendeeMusicBusinessRoundNegotiationCollaborator(
            MusicBusinessRoundNegotiation musicBusinessRoundNegotiation,
            AttendeeCollaborator attendeeCollaborator,
            int userId)
        {
            this.MusicBusinessRoundNegotiation = musicBusinessRoundNegotiation;
            this.AttendeeCollaborator = attendeeCollaborator;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeMusicBusinessRoundNegotiationCollaborator"/> class.
        /// </summary>
        protected AttendeeMusicBusinessRoundNegotiationCollaborator(MusicBusinessRoundNegotiation negotiation)
        {
        }

        /// <summary>
        /// Updates the collaborator details.
        /// </summary>
        /// <param name="userId">The user identifier responsible for the update.</param>
        public void Update(int userId)
        {
            base.SetUpdateDate(userId);
        }

        #region Validations

        /// <summary>
        /// Validates if the entity is in a consistent state.
        /// </summary>
        /// <returns><c>true</c> if the entity is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}
