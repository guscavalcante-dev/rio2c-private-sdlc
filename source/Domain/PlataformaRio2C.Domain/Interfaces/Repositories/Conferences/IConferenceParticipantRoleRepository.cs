// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="IConferenceParticipantRoleRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IConferenceParticipantRoleRepository</summary>
    public interface IConferenceParticipantRoleRepository : IRepository<ConferenceParticipantRole>
    {
        Task<ConferenceParticipantRoleDto> FindDtoAsync(Guid conferenceParticipantRoleUid, int editionId);
        Task<ConferenceParticipantRoleDto> FindConferenceWidgetDtoAsync(Guid conferenceParticipantRoleUid, int editionId);
        Task<ConferenceParticipantRoleDto> FindParticipantsWidgetDtoAsync(Guid conferenceParticipantRoleUid, int editionId);
        Task<ConferenceParticipantRole> FindByUidAsync(Guid conferenceParticipantRoleUid);
        Task<List<ConferenceParticipantRoleDto>> FindAllDtoByEditionIdAsync(int editionId);
        Task<IPagedList<ConferenceParticipantRoleJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> conferenceParticipantRoleUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
    }
}