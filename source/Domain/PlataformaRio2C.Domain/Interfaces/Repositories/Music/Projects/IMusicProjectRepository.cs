// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-03-2023
// ***********************************************************************
// <copyright file="IMusicProjectRepository.cs" company="Softo">
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
    /// <summary>IMusicProjectRepository</summary>
    public interface IMusicProjectRepository : IRepository<MusicProject>
    {
        Task<IPagedList<MusicProjectDto>> FindAllDtosPagedAsync(
            int editionId,
            string searchKeywords,
            Guid? musicGenreUid,
            Guid? evaluationStatusUid,
            bool showBusinessRounds,
            int page,
            int pageSize,
            List<int?> evaluatorUserId,
            List<int?> commissionEvaluationStatusId
        );
        Task<IPagedList<MusicProjectJsonDto>> FindAllByDataTableAsync(int editionId, string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, bool showBusinessRounds, int page, int pageSize, List<Tuple<string, string>> sortColumns);
        Task<IPagedList<MusicProjectReportDto>> FindAllMusicProjectsReportByDataTable(int editionId, string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, bool showBusinessRounds, int page, int pageSize, List<Tuple<string, string>> sortColumns);
        Task<MusicProjectDto> FindDtoToEvaluateAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindDtoToEvaluateAsync(int musicProjectId);
        Task<MusicProjectDto> FindMainInformationWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindMembersWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindTeamMembersWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindReleasedProjectsWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindProjectResponsibleWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindClippingWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindVideoAndMusicWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindSocialNetworksWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindEvaluationGradeWidgetDtoAsync(Guid musicProjectUid, int userId);
        Task<MusicProjectDto> FindEvaluatorsWidgetDtoAsync(Guid musicProjectUid);
        Task<List<MusicProjectDto>> FindAllApprovedAttendeeMusicBandsAsync(int editionId);
        Task<int[]> FindAllApprovedAttendeeMusicBandsIdsAsync(int editionId);
        Task<int[]> FindAllMusicProjectsIdsAsync(int editionId);
        Task<int[]> FindAllMusicProjectsIdsPagedAsync(
            int editionId,
            string searchKeywords,
            Guid? musicGenreUid,
            Guid? evaluationStatusUid,
            bool showBusinessRounds,
            int page,
            int pageSize,
            List<int?> evaluatorUserId,
            List<int?> commissionEvaluationStatusId
           );
        Task<int> CountAsync(int editionId, bool showAllEditions = false);
        Task<int> CountPagedAsync(
            int editionId,
            string searchKeywords,
            Guid? musicGenreUid,
            Guid? evaluationStatusUid,
            bool showBusinessRounds,
            int page,
            int pageSize,
            List<int?> evaluatorUserId,
            List<int?> commissionEvaluationStatusId
        );
        Task<List<MusicBandGroupedByGenreDto>> FindEditionCountPieWidgetDto(int editionId);
    }
}