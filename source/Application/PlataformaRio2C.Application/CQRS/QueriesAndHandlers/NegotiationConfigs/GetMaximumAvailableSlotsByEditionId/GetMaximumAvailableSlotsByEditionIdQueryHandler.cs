// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-01-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="GetMaximumAvailableSlotsByEditionIdQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>
    /// GetMaximumAvailableSlotsByEditionIdQueryHandler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler&lt;PlataformaRio2C.Application.CQRS.Queries.GetMaximumAvailableSlotsByEditionIdQuery, System.Int32&gt;" />
    public class GetMaximumAvailableSlotsByEditionIdQueryHandler : IRequestHandler<GetMaximumAvailableSlotsByEditionIdQuery, GetMaximumAvailableSlotsByEditionIdResponseDto>
    {
        private readonly INegotiationConfigRepository repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMaximumAvailableSlotsByEditionIdQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetMaximumAvailableSlotsByEditionIdQueryHandler(INegotiationConfigRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified count all organizations asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GetMaximumAvailableSlotsByEditionIdResponseDto> Handle(GetMaximumAvailableSlotsByEditionIdQuery cmd, CancellationToken cancellationToken)
        {
            var negotiationConfigDtos = await this.repo.FindAllByEditionIdAsync(cmd.EditionId);

            // Get only NegotiationConfigs that have AutomaticTables configured
            var automaticTablesNegotiationConfigDtos = negotiationConfigDtos
                .Where(nc => nc.NegotiationRoomConfigDtos
                    .Any(nrc => nrc.NegotiationRoomConfig.CountAutomaticTables > 0))
                    .ToList();

            // Get only NegotiationConfigs that have ManualTables configured
            var manualTablesNegotiationConfigDtos = negotiationConfigDtos
                .Where(nc => nc.NegotiationRoomConfigDtos
                    .Any(nrc => nrc.NegotiationRoomConfig.CountManualTables > 0))
                    .ToList();

            return new GetMaximumAvailableSlotsByEditionIdResponseDto(
                automaticTablesNegotiationConfigDtos.Sum(dto => dto.NegotiationConfig.GetMaxAutomaticSlotsCountByEdition()),
                manualTablesNegotiationConfigDtos.Sum(dto => dto.NegotiationConfig.GetMaxManualSlotsCountByEdition()),
                negotiationConfigDtos.Sum(dto => dto.NegotiationConfig.GetMaxSlotsCountByPlayer())
            );
        }
    }
}