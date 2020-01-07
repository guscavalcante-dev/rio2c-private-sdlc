// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="IConferenceRepository.cs" company="Softo">
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
    /// <summary>IConferenceRepository</summary>
    public interface IConferenceRepository : IRepository<Conference>
    {
        Task<ConferenceDto> FindDtoAsync(Guid conferenceUid, int editionId);
        Task<ConferenceDto> FindMainInformationWidgetDtoAsync(Guid conferenceUid, int editionId);
        Task<ConferenceDto> FindTracksAndPresentationFormatsWidgetDtoAsync(Guid conferenceUid, int editionId);
        Task<ConferenceDto> FindParticipantsWidgetDtoAsync(Guid conferenceUid, int editionId);
        Task<IPagedList<ConferenceJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> conferencesUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);

        //#region Old Methods

        //IQueryable<Conference> GetAllBySchedule();
        //IQueryable<Conference> GetAllBySchedule(Expression<Func<Conference, bool>> filter);

        //#endregion
    }    
}
