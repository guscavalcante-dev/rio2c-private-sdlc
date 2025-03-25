// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-01-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="GetAudiovisualNegotiationAvailableSlotsCountByEditionIdQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Dtos;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>
    /// GetAudiovisualNegotiationAvailableSlotsCountByEditionIdQueryHandler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler&lt;PlataformaRio2C.Application.CQRS.Queries.GetAudiovisualNegotiationAvailableSlotsCountByEditionId, System.Int32&gt;" />
    public class GetAudiovisualNegotiationAvailableSlotsCountByEditionIdQueryHandler : IRequestHandler<GetAudiovisualNegotiationAvailableSlotsCountByEditionId, GetAudiovisualNegotiationAvailableSlotsCountByEditionIdResponseDto>
    {
        private readonly INegotiationConfigRepository negotiationConfigRepo;
        private readonly IOrganizationRepository organizationRepo;
        private readonly IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAudiovisualNegotiationAvailableSlotsCountByEditionIdQueryHandler" /> class.
        /// </summary>
        /// <param name="negotiationConfigRepository">The repository.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="projectBuyerEvaluationRepo">The project buyer evaluation repo.</param>
        public GetAudiovisualNegotiationAvailableSlotsCountByEditionIdQueryHandler(
            INegotiationConfigRepository negotiationConfigRepository,
            IOrganizationRepository organizationRepository,
            IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo)
        {
            this.negotiationConfigRepo = negotiationConfigRepository;
            this.organizationRepo = organizationRepository;
            this.projectBuyerEvaluationRepo = projectBuyerEvaluationRepo;
        }

        /// <summary>Handles the specified count all organizations asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GetAudiovisualNegotiationAvailableSlotsCountByEditionIdResponseDto> Handle(GetAudiovisualNegotiationAvailableSlotsCountByEditionId cmd, CancellationToken cancellationToken)
        {
            var negotiationConfigDtos = await this.negotiationConfigRepo.FindAllByEditionIdAsync(cmd.EditionId, ProjectType.AudiovisualBusinessRound.Id);

            // Get only NegotiationConfigs that have AutomaticTables configured
            var automaticTablesNegotiationConfigDtos = negotiationConfigDtos
                .Where(nc => nc.NegotiationRoomConfigDtos
                                .Any(dto => dto.NegotiationRoomConfig.CountAutomaticTables > 0 &&
                                            !dto.NegotiationRoomConfig.Room.IsVirtualMeeting))
                .ToList();

            // Get only NegotiationConfigs that have ManualTables configured
            var manualTablesNegotiationConfigDtos = negotiationConfigDtos
                .Where(nc => nc.NegotiationRoomConfigDtos
                                .Any(dto => dto.NegotiationRoomConfig.CountManualTables > 0 &&
                                            !dto.NegotiationRoomConfig.Room.IsVirtualMeeting))
                .ToList();

            // Get the maximum slots available in Edition
            int maximumAutomaticSlotsInEdition = automaticTablesNegotiationConfigDtos.Sum(dto => dto.NegotiationConfig.CountMaximumAutomaticSlotsInEdition());
            int maximumManualSlotsInEdition = manualTablesNegotiationConfigDtos.Sum(dto => dto.NegotiationConfig.CountMaximumManualSlotsInEdition());

            // Get accepted project buyer evaluations count in Edition
            int acceptedProjectBuyerEvaluationsCount = await projectBuyerEvaluationRepo.CountAcceptedProjectBuyerEvaluationsByEditionIdAsync(cmd.EditionId);

            // Get the reamining automatic slots in Edition
            var remainingAutomaticSlotsInEdition = maximumAutomaticSlotsInEdition - acceptedProjectBuyerEvaluationsCount;

            // Get Players count in Edition
            int audiovisualPlayersCount = await organizationRepo.CountAllByDataTable(OrganizationType.AudiovisualPlayer.Uid, false, cmd.EditionId);

            // Get the maximum slots by Player
            decimal maximumSlotsByPlayer = audiovisualPlayersCount > 0 ?
                maximumAutomaticSlotsInEdition / audiovisualPlayersCount :
                0;

            maximumSlotsByPlayer = Math.Floor(maximumSlotsByPlayer);

            return new GetAudiovisualNegotiationAvailableSlotsCountByEditionIdResponseDto(
                maximumAutomaticSlotsInEdition,
                maximumManualSlotsInEdition,
                (int)maximumSlotsByPlayer,
                remainingAutomaticSlotsInEdition
            );
        }
    }
}