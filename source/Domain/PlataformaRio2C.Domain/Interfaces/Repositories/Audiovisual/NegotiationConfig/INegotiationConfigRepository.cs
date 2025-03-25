// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-03-2024
// ***********************************************************************
// <copyright file="INegotiationConfigRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>INegotiationConfigRepository</summary>
    public interface INegotiationConfigRepository : IRepository<NegotiationConfig>
    {
        Task<NegotiationConfigDto> FindMainInformationWidgetDtoAsync(Guid negotiationConfigUid, int projectTypeId);
        Task<NegotiationConfigDto> FindRoomsWidgetDtoAsync(Guid negotiationConfigUid, int projectTypeId);
        Task<IPagedList<NegotiationConfigJsonDto>> FindAllJsonDtosPagedAsync(int page, int pageSize, List<Tuple<string, string>> sortColumns, string keywords, Guid? musicGenreUid, Guid? evaluationStatusUid, string languageCode, int editionId, int projectTypeId);
        Task<int> CountAsync(int editionId, int projectTypeId, bool showAllEditions = false);
        Task<List<NegotiationConfig>> FindAllForGenerateNegotiationsAsync(int editionId, int projectTypeId);
        Task<List<NegotiationConfigDto>> FindAllDatesDtosAsync(int editionId, string customFilter, bool buyerAttendeeOrganizationAcceptsVirtualMeeting, int projectTypeId);
        Task<List<NegotiationConfigDto>> FindAllRoomsDtosAsync(int editionId, Guid negotiationConfigUid, string customFilter, bool buyerAttendeeOrganizationAcceptsVirtualMeeting, int projectTypeId);
        Task<NegotiationConfigDto> FindAllTimesDtosAsync(int editionId, Guid negotiationRoomConfigUid, string customFilter, bool buyerAttendeeOrganizationAcceptsVirtualMeeting, int projectTypeId);
        Task<List<NegotiationConfigDto>> FindAllByEditionIdAsync(int editionId, int projectTypeId);
        Task<int> CountNegotiationConfigsWithVirtualRoomConfiguredAsync(int editionId, int projectTypeId);
        Task<int> CountNegotiationConfigsWithPresentialRoomConfiguredAsync(int editionId, int projectTypeId);
    }
}