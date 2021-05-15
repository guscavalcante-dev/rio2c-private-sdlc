// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-30-2020
// ***********************************************************************
// <copyright file="INegotiationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>INegotiationRepository</summary>
    public interface INegotiationRepository : IRepository<Negotiation>
    {
        Task<Negotiation> FindByIdAsync(int negotiationId);
        Task<Negotiation> FindByUidAsync(Guid negotiationUid);
        Task<NegotiationDto> FindDtoAsync(Guid negotiationUid);

        Task<List<NegotiationGroupedByDateDto>> FindScheduledWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? roomUid);
        Task<List<Negotiation>> FindNegotiationsByEditionIdAsync(int editionId);
        Task<List<NegotiationDto>> FindAllScheduleDtosAsync(int editionId, int? attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate);
        Task<List<NegotiationReportGroupedByDateDto>> FindReportWidgetDtoAsync(int editionId, Guid? buyerOrganizationUid, Guid? sellerOrganizationUid, string projectKeywords, DateTime? negotiationDate, Guid? roomUid);
        void Truncate();
    }    
}