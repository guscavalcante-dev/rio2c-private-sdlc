// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-29-2020
// ***********************************************************************
// <copyright file="IMusicProjectRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IMusicProjectRepository</summary>
    public interface IMusicProjectRepository : IRepository<MusicProject>
    {
        Task<IPagedList<MusicProjectDto>> FindAllDtosToEvaluateAsync(int editionId, string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, int page, int pageSize);
        Task<MusicProjectDto> FindDtoToEvaluateAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindMainInformationWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindMembersWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindTeamMembersWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindReleasedProjectsWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindProjectResponsibleWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindClippingWidgetDtoAsync(Guid musicProjectUid);
        Task<MusicProjectDto> FindVideoAndMusicWidgetDtoAsync(Guid musicProjectUid);
    }
}