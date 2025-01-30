// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-21-2024
// ***********************************************************************
// <copyright file="ICollaboratorRepository.cs" company="Softo">
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
    /// <summary>ICollaboratorRepository</summary>
    public interface ICollaboratorRepository : IRepository<Collaborator>
    {
        Task<List<AdminAccessControlDto>> FindAllCollaboratorsByCollaboratorsUids(int editionId, List<Guid> collaboratorsUids);
        Task<CollaboratorDto> FindDtoByUidAndByEditionIdAsync(Guid collaboratorUid, int editionId, string userInterfaceLanguage);
        Task<int> CountAllByDataTable(string collaboratorTypeName, string organizationTypeName, bool showAllEditions, int? editionId);
        Task<Collaborator> FindBySalesPlatformAttendeeIdAsync(string salesPlatformAttendeeId);
        Task<CollaboratorDto> FindByEmailAsync(string email, int? editionId);
        Task<CollaboratorDto> FindByDocumentAsync(string document, int? editionId);
        Task<IPagedList<CollaboratorDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, string[] collaboratorTypeNames, bool showAllEditions, bool showAllParticipants, bool? showHighlights, int? editionId);
        Task<IPagedList<LogisticJsonDto>> FindAllLogisticsByDatatable(int editionId, int page, int pageSize, string searchValue, List<Tuple<string, string>> getSortColumns, bool showAllParticipants, bool showAllSponsors);
        Task<IPagedList<CollaboratorDto>> FindAllAdminsByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, string collaboratorTypeName, string roleName, bool showAllEditions, bool showAllParticipants, string userInterfaceLanguage, int? editionId);

        #region Audiovisual Commissions

        Task<IPagedList<CollaboratorDto>> FindAllAudiovisualCommissionMembersByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, string[] collaboratorTypeNames, bool showAllEditions, bool showAllParticipants, int? editionId, List<Guid?> interestsUids);
        Task<IPagedList<CollaboratorDto>> FindAllAudiovisualCommissionMembersApiPaged(int? editionId, string keywords, int page, int pageSize);
        Task<CollaboratorDto> FindAudiovisualCommissionMemberApi(Guid collaboratorUid, int editionId);

        #endregion

        #region Innovation Commissions

        Task<IPagedList<CollaboratorDto>> FindAllInnovationCommissionsByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, string[] collaboratorTypeNames, bool showAllEditions, bool showAllParticipants, bool? showHighlights, int? editionId, List<Guid?> innovationOrganizationTrackOptionsUids);

        #endregion

        #region Music Commissions

        Task<IPagedList<CollaboratorDto>> FindAllMusicCommissionMembersApiPaged(int? editionId, string keywords, int page, int pageSize);
        Task<CollaboratorDto> FindMusicCommissionMemberApi(Guid collaboratorUid, int editionId);
        Task<List<CollaboratorDto>> FindMusicCommissionMembers(int editionId);

        #endregion

        #region Creator Commissions

        Task<IPagedList<CollaboratorDto>> FindAllCreatorCommissionsByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, string[] collaboratorTypeNames, bool showAllEditions, bool showAllParticipants, bool? showHighlights, int? editionId);

        #endregion

        #region Players Executives

        Task<IPagedList<PlayerExecutiveReportDto>> FindAllPlayersExecutivesReportByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllEditions,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId,
            string collaboratorTypeNames
        );

        #endregion

        #region Music Players Executives

        Task<IPagedList<MusicPlayerCollaboratorApiDto>> FindAllMusicPlayersExecutivesPublicApiPaged(int editionId, string keywords, List<Guid> activitiesUids, List<Guid> targetAudiencesUids, List<Guid> interestsUids, DateTime? modifiedAfterDate, bool showDetails, bool showDeleted, int page, int pageSize);
        Task<MusicPlayerCollaboratorApiDto> FindMusicPlayerExecutivePublicApiDtoByUid(Guid collaboratorUid, int editionId);

        #endregion

        #region Innovation Players Executives

        Task<IPagedList<InnovationPlayerCollaboratorApiDto>> FindAllInnovationPlayersExecutivesPublicApiPaged(int editionId, string keywords, List<Guid> activitiesUids, List<Guid> targetAudiencesUids, List<Guid> interestsUids, DateTime? modifiedAfterDate, bool showDetails, bool showDeleted, int page, int pageSize);
        Task<InnovationPlayerCollaboratorApiDto> FindInnovationPlayerExecutivePublicApiDtoByUid(Guid collaboratorUid, int editionId);

        #endregion

        #region Speakers

        Task<IPagedList<CollaboratorApiListDto>> FindAllSpeakersApiListDtoPaged(int editionId, string keywords, bool filterByProjectsInNegotiation, string collaboratorTypeName, bool showAllParticipants, int page, int pageSize);
        Task<IPagedList<CollaboratorDto>> FindAllSpeakersByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllEditions,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId,
            bool? showNotPublishableToApi,
            List<Guid?> roomsUids,
            bool exportToExcel = false
        );
        Task<IPagedList<SpeakerCollaboratorApiDto>> FindAllSpeakersPublicApiPaged(int editionId, string keywords, int? highlights, List<Guid?> conferencesUids, List<DateTimeOffset?> conferencesDates, List<Guid?> roomsUids, string collaboratorTypeName, DateTime? modifiedAfterDate, bool showDetails, bool showDeleted, bool skipIsApiDisplayEnabledVerification, int page, int pageSize);
        Task<SpeakerCollaboratorApiDto> FindSpeakerPublicApiDtoByUid(Guid collaboratorUid, int editionId, string collaboratorTypeName);

        #endregion

        #region Agenda

        Task<IPagedList<CollaboratorDto>> FindAllWithAgendaByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, string[] collaboratorTypeNames, string userInterfaceLanguage, int? editionId);
        Task<int> CountAllWithAgendaByDataTable(bool showAllEditions, int? editionId);
        Task<List<CollaboratorDto>> FindAllCollaboratorDtosWithAgendaByUids(int editionId, List<Guid> collaboratorsUids);

        #endregion

        #region Api

        Task<IPagedList<CollaboratorApiListDto>> FindAllDropdownApiListDtoPaged(int editionId, string keywords, bool filterByProjectsInNegotiation, string collaboratorTypeName, bool showAllParticipants, int page, int pageSize);
        Task<List<Collaborator>> FindAllByHightlightPosition(int editionId, Guid collaboratorTypeUid, int apiHighlightPosition, Guid? organizationUid);

        #endregion
    }    
}