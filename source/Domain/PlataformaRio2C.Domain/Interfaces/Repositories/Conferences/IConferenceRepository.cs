﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 05-06-2025
// ***********************************************************************
// <copyright file="IConferenceRepository.cs" company="Softo">
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
    /// <summary>IConferenceRepository</summary>
    public interface IConferenceRepository : IRepository<Conference>
    {
        Task<ConferenceDto> FindDtoAsync(Guid conferenceUid, int editionId);
        Task<ConferenceDto> FindMainInformationWidgetDtoAsync(Guid conferenceUid, int editionId);
        Task<ConferenceDto> FindTracksAndPresentationFormatsWidgetDtoAsync(Guid conferenceUid, int editionId);
        Task<ConferenceDto> FindParticipantsWidgetDtoAsync(Guid conferenceUid, int editionId);
        Task<IPagedList<ConferenceJsonDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> conferencesUids, int editionId, int languageId);
        Task<int> CountAllByDataTable(bool showAllEditions, int editionId);
        Task<List<Conference>> FindAllForGenerateNegotiationsAsync(Guid editionUid);
        Task<List<ConferenceDto>> FindAllScheduleDtosAsync(int editionId, int attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate, bool showMyConferences, bool showAllConferences);
        Task<List<ConferenceDto>> FindAllByAttendeeCollaboratorIdAsync(int attendeeCollaboratorId);
        Task<List<ConferenceDto>> FindConferencesDtoByParticipantAsync(Guid participantUid, int editionId);

        #region Api

        Task<IPagedList<ConferenceDto>> FindAllPublicApiPaged(int editionId, string keywords, List<DateTimeOffset> editionDates, List<Guid> editionEventsUids, List<Guid> roomsUids, List<Guid> tracksUids, List<Guid> pillarsUids, List<Guid> presentationFormatsUids, DateTime? modifiedAfterDate, bool showDeleted, int page, int pageSize);
        Task<ConferenceDto> FindApiDtoByUidAsync(Guid conferenceUid, int editionId);
        Task<ConferenceDto> FindApiConfigurationWidgetDtoByConferenceUidAndByEditionIdAsync(Guid conferenceUid, int editionId);
        Task<List<ConferenceDto>> FindAllApiConfigurationWidgetDtoByHighlight(int editionEventId);
        Task<List<Conference>> FindAllByHighlightPosition(string apiHighlightPosition, int editionEventId);

        #endregion
    }
}
