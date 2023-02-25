// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-08-2023
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
        Task<IPagedList<CollaboratorBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, string[] collaboratorTypeNames, string[] organizationTypeNames, bool showAllEditions, bool showAllParticipants, bool? showHighlights, int? editionId);
        Task<IPagedList<CollaboratorApiListDto>> FindAllDropdownApiListDtoPaged(int editionId, string keywords, bool filterByProjectsInNegotiation, string collaboratorTypeName, bool showAllParticipants, int page, int pageSize);
        Task<IPagedList<CollaboratorApiListDto>> FindAllSpeakersApiListDtoPaged(int editionId, string keywords, bool filterByProjectsInNegotiation, string collaboratorTypeName, bool showAllParticipants, int page, int pageSize);
        Task<IPagedList<LogisticJsonDto>> FindAllLogisticsByDatatable(int editionId, int page, int pageSize, string searchValue, List<Tuple<string, string>> getSortColumns, bool showAllParticipants, bool showAllSponsors);
        Task<IPagedList<CollaboratorBaseDto>> FindAllAdminsByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, string collaboratorTypeName, string roleName, bool showAllEditions, bool showAllParticipants, string userInterfaceLanguage, int? editionId);
        Task<IPagedList<CollaboratorBaseDto>> FindAllInnovationCommissionsByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, string[] collaboratorTypeNames, bool showAllEditions, bool showAllParticipants, bool? showHighlights, int? editionId, List<Guid?> innovationOrganizationTrackOptionsUids);
        Task<IPagedList<CollaboratorBaseDto>> FindAllAudiovisualCommissionsByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, string[] collaboratorTypeNames, bool showAllEditions, bool showAllParticipants, bool? showHighlights, int? editionId, List<Guid?> interestsUids);

        #region Api

        Task<IPagedList<CollaboratorApiListDto>> FindAllPublicApiPaged(int editionId, string keywords, int? highlights, List<Guid?> conferencesUids, List<DateTimeOffset?> conferencesDates, List<Guid?> roomsUids, string collaboratorTypeName, int page, int pageSize);
        Task<SpeakerCollaboratorDto> FindPublicApiDtoAsync(Guid collaboratorUid, int editionId, string collaboratorTypeName);
        Task<List<Collaborator>> FindAllByHightlightPosition(int editionId, Guid collaboratorTypeUid, int apiHighlightPosition, Guid? organizationUid);
        Task<List<CollaboratorDto>> FindAllSpeakersByEditionId(int editionId);

        #endregion

        #region Old

        //Collaborator GetById(int id);
        //Collaborator GetStatusRegisterCollaboratorByUserId(int id);
        //Collaborator GetWithProducerByUserId(int id);
        //Collaborator GetWithPlayerAndProducerUserId(int id);
        //Collaborator GetWithPlayerAndProducerUid(Guid id);
        //Collaborator GetImage(Guid uid);
        //IEnumerable<Collaborator> GetOptions(Expression<Func<Collaborator, bool>> filter);
        //IEnumerable<Collaborator> GetCollaboratorProducerOptions(Expression<Func<Collaborator, bool>> filter);
        //IEnumerable<Collaborator> GetCollaboratorPlayerOptions(Expression<Func<Collaborator, bool>> filter);
        //IEnumerable<Collaborator> GetOptionsChat(int userId);
        //Collaborator GetBySchedule(Expression<Func<Collaborator, bool>> filter);

        #endregion
    }    
}