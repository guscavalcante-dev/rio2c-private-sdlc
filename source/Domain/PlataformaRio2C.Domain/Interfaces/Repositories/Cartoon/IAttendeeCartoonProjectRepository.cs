// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-08-2022
// ***********************************************************************
// <copyright file="IAttendeeCartoonProjectRepository.cs" company="Softo">
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
    /// <summary>IAttendeeCartoonProjectRepository</summary>
    public interface IAttendeeCartoonProjectRepository : IRepository<AttendeeCartoonProject>
    {
        Task<AttendeeCartoonProject> FindByIdAsync(int AttendeeCartoonProjectId);
        Task<AttendeeCartoonProject> FindByUidAsync(Guid AttendeeCartoonProjectUid);
        Task<List<AttendeeCartoonProject>> FindAllByIdsAsync(List<int?> AttendeeCartoonProjectIds);
        Task<List<AttendeeCartoonProject>> FindAllByUidsAsync(List<Guid?> AttendeeCartoonProjectUids);
        Task<AttendeeCartoonProject> FindByTitleAndEditionIdAsync(string title, int editionId);
        Task<List<AttendeeCartoonProject>> FindAllByEditionIdAsync(int editionId);
        Task<int[]> FindAllApprovedAttendeeCartoonProjectsIdsAsync(int editionId);
        Task<int> CountAsync(int editionId, bool showAllEditions = false);
        Task<IPagedList<AttendeeCartoonProjectDto>> FindAllDtosPagedAsync(int editionId, string searchKeywords, List<Guid?> projectFormatUids, Guid? evaluationStatusUid, int page, int pageSize);
        Task<IPagedList<AttendeeCartoonProjectJsonDto>> FindAllJsonDtosPagedAsync(int editionId, string searchKeywords, List<Guid?> projectFormatUids, Guid? evaluationStatusUid, int page, int pageSize, List<Tuple<string, string>> sortColumns);
        Task<AttendeeCartoonProjectDto> FindEvaluationGradeWidgetDtoAsync(Guid attendeeCartoonProjectUid, int userId);
        Task<AttendeeCartoonProjectDto> FindEvaluatorsWidgetDtoAsync(Guid attendeeCartoonProjectUid);
        //
        Task<AttendeeCartoonProjectDto> FindDtoToEvaluateAsync(Guid attendeeCartoonProjectUid);
        Task<AttendeeCartoonProjectDto> FindDtoToEvaluateAsync(int attendeeCartoonProjectId);
        Task<int[]> FindAllCartoonProjectsIdsPagedAsync(int editionId, string searchKeywords, List<Guid?> projectFormatUids, Guid? evaluationStatusUid, int page, int pageSize);
        Task<int> CountPagedAsync(int editionId, string searchKeywords, List<Guid?> projectFormatUids, Guid? evaluationStatusUid, int page, int pageSize);
        Task<AttendeeCartoonProjectDto> FindMainInformationWidgetDtoAsync(Guid attendeeCartoonProjectUid);
        Task<AttendeeCartoonProjectDto> FindBusinessInformationWidgetDtoAsync(Guid attendeeCartoonProjectUid);


    }
}