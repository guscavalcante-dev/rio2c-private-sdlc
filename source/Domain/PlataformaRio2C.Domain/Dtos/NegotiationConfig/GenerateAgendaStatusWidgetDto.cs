// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 10-24-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-24-2024
// ***********************************************************************
// <copyright file="GenerateAgendaStatusWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Dtos
{
    public class GenerateAgendaStatusWidgetDto
    {
        public int NegotiationConfigsWithPresentialRoomConfiguredCount { get; private set; }
        public int NegotiationConfigsWithVirtualRoomConfiguredCount { get; private set; }

        public bool HasNegotiationConfigWithPresentialRoomConfigured => this.NegotiationConfigsWithPresentialRoomConfiguredCount > 0;
        public bool HasNegotiationConfigWithVirtualRoomConfigured => this.NegotiationConfigsWithVirtualRoomConfiguredCount > 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateAgendaStatusWidgetDto"/> class.
        /// </summary>
        /// <param name="negotiationConfigsWithPresentialRoomConfiguredCount">The negotiation configs with presential room configured count.</param>
        /// <param name="negotiationConfigsWithVirtualRoomConfiguredCount">The negotiation configs with virtual room configured count.</param>
        public GenerateAgendaStatusWidgetDto(
            int negotiationConfigsWithPresentialRoomConfiguredCount,
            int negotiationConfigsWithVirtualRoomConfiguredCount)
        {
            this.NegotiationConfigsWithPresentialRoomConfiguredCount = negotiationConfigsWithPresentialRoomConfiguredCount;
            this.NegotiationConfigsWithVirtualRoomConfiguredCount = negotiationConfigsWithVirtualRoomConfiguredCount;
        }

        public string GetMessage()
        {
            if (!HasNegotiationConfigWithPresentialRoomConfigured && !HasNegotiationConfigWithVirtualRoomConfigured)
                return $"{Labels.PresentialRooms} {Labels.And} {Labels.VirtualRooms}";
            else if (!HasNegotiationConfigWithPresentialRoomConfigured)
                return Labels.PresentialRooms;
            else if (!HasNegotiationConfigWithVirtualRoomConfigured)
                return Labels.VirtualRooms;
            else
                return Labels.Rooms;
        }
    }
}
