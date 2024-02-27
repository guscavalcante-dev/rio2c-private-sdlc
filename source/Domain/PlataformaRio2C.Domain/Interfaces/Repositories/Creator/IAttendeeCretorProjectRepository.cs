// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="IAttendeeCreatorProjectRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeCreatorProjectRepository</summary>
    public interface IAttendeeCreatorProjectRepository : IRepository<AttendeeCreatorProject>
    {
        Task<AttendeeCreatorProjectDto> FindByIdAsync(int attendeeCreatorProjectId);
        Task<AttendeeCreatorProjectDto> FindByUidAsync(Guid attendeeCreatorProjectUid);
        Task<List<AttendeeCreatorProjectDto>> FindAllByIdsAsync(List<int?> attendeeCreatorProjectIds);
        Task<List<AttendeeCreatorProjectDto>> FindAllByUidsAsync(List<Guid?> attendeeCreatorProjectUids);
        Task<AttendeeCreatorProjectDto> FindByTitleAndEditionIdAsync(string title, int editionId);
        Task<List<AttendeeCreatorProject>> FindAllByEditionIdAsync(int editionId);
        Task<int[]> FindAllApprovedAttendeeCreatorProjectsIdsAsync(int editionId);
        Task<int> CountAsync(int editionId, bool showAllEditions = false);

        Task<IPagedList<AttendeeCreatorProjectDto>> FindAllDtosPagedAsync(int editionId, string searchKeywords, Guid? evaluationStatusUid, int page, int pageSize);
        Task<AttendeeCreatorProjectDto> FindDtoToEvaluateAsync(Guid attendeeCreatorProjectUid);
        Task<AttendeeCreatorProjectDto> FindDtoToEvaluateAsync(int attendeeCreatorProjectId);
        Task<int[]> FindAllCreatorProjectsIdsPagedAsync(int editionId, string searchKeywords, Guid? evaluationStatusUid, int page, int pageSize);
        Task<int> CountPagedAsync(int editionId, string searchKeywords,  Guid? evaluationStatusUid, int page, int pageSize);
    }
}