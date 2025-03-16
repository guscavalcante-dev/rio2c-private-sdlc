// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-01-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="GetAudiovisualNegotiationAvailableSlotsCountByEditionIdResponseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Application.CQRS.Dtos
{
    public class GetAudiovisualNegotiationAvailableSlotsCountByEditionIdResponseDto
    {
        /// <summary>
        /// Gets the maximum automatic slots count in edition. This value never changes.
        /// </summary>
        public int MaximumAutomaticSlotsCountInEdition { get; private set; }

        /// <summary>
        /// Gets the maximum manual slots count in edition. This value never changes.
        /// </summary>
        public int MaximumManualSlotsCountInEdition { get; private set; }

        /// <summary>
        /// Gets the maximum slots count in edition (AutomaticSlots + ManualSlots). This value never changes.
        /// </summary>
        public int MaximumSlotsCountInEdition => this.MaximumAutomaticSlotsCountInEdition + this.MaximumManualSlotsCountInEdition;

        /// <summary>
        /// Gets the maximum slots by player. This value never changes.
        /// </summary>
        public int MaximumSlotsByPlayer { get; private set; }

        /// <summary>
        /// Gets the remaining automatic slots in edition. (MaximumAutomaticSlotsCountInEdition - AcceptedProjectBuyerEvaluationsCount)
        /// </summary>
        public int RemainingAutomaticSlotsInEdition { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAudiovisualNegotiationAvailableSlotsCountByEditionIdResponseDto" /> class.
        /// </summary>
        /// <param name="maximumAutomaticSlotsInEdition">The maximum automatic slots in edition.</param>
        /// <param name="maximumManualSlotsInEdition">The maximum manual slots in edition.</param>
        /// <param name="maximumSlotsByPlayer">The maximum slots by player.</param>
        /// <param name="remainingAutomaticSlotsInEdition">The remaining automatic slots in edition.</param>
        public GetAudiovisualNegotiationAvailableSlotsCountByEditionIdResponseDto(
            int maximumAutomaticSlotsInEdition,
            int maximumManualSlotsInEdition,
            int maximumSlotsByPlayer,
            int remainingAutomaticSlotsInEdition)
        {
            this.MaximumAutomaticSlotsCountInEdition = maximumAutomaticSlotsInEdition;
            this.MaximumManualSlotsCountInEdition = maximumManualSlotsInEdition;
            this.MaximumSlotsByPlayer = maximumSlotsByPlayer;
            this.RemainingAutomaticSlotsInEdition = remainingAutomaticSlotsInEdition;
        }
    }
}
