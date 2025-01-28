// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-01-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="GetAudiovisualMaximumAvailableSlotsByEditionIdResponseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Application.CQRS.Dtos
{
    public class GetAudiovisualMaximumAvailableSlotsByEditionIdResponseDto
    {
        /// <value>
        /// The maximum automatic slots available by edition.
        /// </value>
        public int MaximumAutomaticSlotsAvailableByEdition { get; private set; }

        /// <value>
        /// The maximum manual slots available by edition.
        /// </value>
        public int MaximumManualSlotsAvailableByEdition { get; private set; }

        /// <value>
        /// The maximum available slots by edition. Consequently, the maximum number of business rounds an edition can hold.
        /// </value>
        public int TotalAvailablSlotsByEdition => this.MaximumAutomaticSlotsAvailableByEdition + this.MaximumManualSlotsAvailableByEdition;

        /// <value>
        /// The maximum available slots by player. Consequently, the maximum number of projects a player can accept.
        /// </value>
        public int MaximumAvailableSlotsByPlayer { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAudiovisualMaximumAvailableSlotsByEditionIdResponseDto"/> class.
        /// </summary>
        /// <param name="maximumAutomaticSlotsAvailableByEdition">The maximum automatic slots available by edition.</param>
        /// <param name="maximumManualSlotsAvailableByEdition">The maximum manual slots available by edition.</param>
        /// <param name="maximumAvailableSlotsByPlayer">The maximum available slots by player.</param>
        public GetAudiovisualMaximumAvailableSlotsByEditionIdResponseDto(
            int maximumAutomaticSlotsAvailableByEdition,
            int maximumManualSlotsAvailableByEdition,
            int maximumAvailableSlotsByPlayer)
        {
            this.MaximumAutomaticSlotsAvailableByEdition = maximumAutomaticSlotsAvailableByEdition;
            this.MaximumManualSlotsAvailableByEdition = maximumManualSlotsAvailableByEdition;
            this.MaximumAvailableSlotsByPlayer = maximumAvailableSlotsByPlayer;
        }
    }
}
