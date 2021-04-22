// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-19-2020
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
        Task<CollaboratorDto> FindDtoByUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<IPagedList<CollaboratorBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, string[] collaboratorTypeNames, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, bool? showHighlights, int? editionId);
        Task<int> CountAllByDataTable(string collaboratorTypeName, bool showAllEditions, int? editionId);
        Task<Collaborator> FindBySalesPlatformAttendeeIdAsync(string salesPlatformAttendeeId);
        Task<Collaborator> FindByEmailAsync(string email);
        Task<IPagedList<CollaboratorApiListDto>> FindAllDropdownApiListDtoPaged(int editionId, string keywords, bool filterByProjectsInNegotiation, string collaboratorTypeName, bool showAllParticipants, int page, int pageSize);
        Task<IPagedList<LogisticJsonDto>> FindAllLogisticsByDatatable(int editionId, int page, int pageSize, string searchValue, List<Tuple<string, string>> getSortColumns, bool showAllParticipants, bool showAllSponsors);

        #region Api

        Task<IPagedList<CollaboratorApiListDto>> FindAllPublicApiPaged(int editionId, string keywords, int? highlights, string collaboratorTypeName, int page, int pageSize);
        Task<SpeakerCollaboratorDto> FindPublicApiDtoAsync(Guid collaboratorUid, int editionId, string collaboratorTypeName);
        Task<List<Collaborator>> FindAllByHightlightPosition(int editionId, Guid collaboratorTypeUid, int apiHighlightPosition, Guid? organizationUid);

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