// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-15-2024
// ***********************************************************************
// <copyright file="INegotiationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>INegotiationRepository</summary>
    public interface INegotiationRepository : IRepository<Negotiation>
    {
        Task<Negotiation> FindByIdAsync(int negotiationId);
        Task<Negotiation> FindByUidAsync(Guid negotiationUid);
        Task<NegotiationDto> FindDtoAsync(Guid negotiationUid);
        Task<NegotiationDto> FindMainInformationWidgetDtoAsync(Guid negotiationUid);
        Task<NegotiationDto> FindVirtualMeetingWidgetDtoAsync(Guid negotiationUid);
        Task<List<NegotiationGroupedByDateDto>> FindScheduledWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? roomUid, bool showParticipants);
        Task<List<Negotiation>> FindNegotiationsByEditionIdAsync(int editionId);
        Task<List<Negotiation>> FindManualScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false);
        Task<List<Negotiation>> FindAutomaticScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false);
        Task<List<Negotiation>> FindScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false);
        Task<List<NegotiationDto>> FindAllScheduledNegotiationsDtosAsync(int editionId, int? attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate);
        Task<List<NegotiationReportGroupedByDateDto>> FindReportWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? roomUid, bool showParticipants);
        Task<List<NegotiationGroupedByDateDto>> FindCollaboratorScheduledWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? attendeeCollaboratorUid);

        void Truncate();
    }
}