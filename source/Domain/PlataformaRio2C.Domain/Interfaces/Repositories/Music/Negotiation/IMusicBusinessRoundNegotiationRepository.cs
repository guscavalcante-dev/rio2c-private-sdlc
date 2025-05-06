// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Daniel Giese Rodrigues
// Created          : 05-06-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 05-06-2025
// ***********************************************************************
// <copyright file="IMusicBusinessRoundNegotiationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces.Repositories
{
    public interface IMusicBusinessRoundNegotiationRepository : IRepository<MusicBusinessRoundNegotiation>
    {
        Task<int> CountNegotiationNotScheduledAsync(int editionId, bool showAllEditions = false);
        Task<List<MusicBusinessRoundProjectBuyerEvaluationDto>> FindUnscheduledWidgetDtoAsync(int editionId);
        Task<MusicBusinessRoundNegotiation> FindByIdAsync(int negotiationId);
        Task<MusicBusinessRoundNegotiation> FindByUidAsync(Guid negotiationUid);
        Task<MusicBusinessRoundNegotiationDto> FindDtoAsync(Guid negotiationUid);
        Task<MusicBusinessRoundNegotiationDto> FindMainInformationWidgetDtoAsync(Guid negotiationUid);
        Task<MusicBusinessRoundNegotiationDto> FindVirtualMeetingWidgetDtoAsync(Guid negotiationUid);
        Task<List<MusicBusinessRoundNegotiationGroupedByDateDto>> FindScheduledWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? roomUid,string type, bool showParticipants);
        Task<List<MusicBusinessRoundNegotiation>> FindAllByEditionIdAsync(int editionId);
        Task<List<MusicBusinessRoundNegotiation>> FindManualScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false);
        Task<List<MusicBusinessRoundNegotiation>> FindAutomaticScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false);
        Task<List<MusicBusinessRoundNegotiation>> FindScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false);
        Task<List<MusicBusinessRoundNegotiationDto>> FindAllScheduledNegotiationsDtosAsync(int editionId, int? attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate);
        Task<List<MusicBusinessRoundNegotiationReportGroupedByDateDto>> FindReportWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? roomUid, bool showParticipants);
        Task<List<MusicBusinessRoundNegotiationGroupedByDateDto>> FindCollaboratorScheduledWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? attendeeCollaboratorUid);

    }
}
