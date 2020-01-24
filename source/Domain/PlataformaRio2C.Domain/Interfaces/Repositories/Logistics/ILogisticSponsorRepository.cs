// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="ILogisticSponsorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ILogisticSponsorRepository</summary>
    public interface ILogisticSponsorRepository : IRepository<LogisticSponsor>
    {
        //Task<List<AdminAccessControlDto>> FindAllCollaboratorsByCollaboratorsUids(int editionId, List<Guid> collaboratorsUids);
        //Task<CollaboratorDto> FindDtoByUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<IPagedList<LogisticSponsorBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, string collaboratorTypeName, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, bool? showHighlights, int? editionId);
        //Task<int> CountAllByDataTable(string collaboratorTypeName, bool showAllEditions, int? editionId);
        //Task<Collaborator> FindBySalesPlatformAttendeeIdAsync(string salesPlatformAttendeeId);

        //#region Api

        //Task<IPagedList<CollaboratorApiListDto>> FindAllPublicApiPaged(int editionId, string keywords, int? highlights, string collaboratorTypeName, int page, int pageSize);
        //Task<SpeakerCollaboratorDto> FindPublicApiDtoAsync(Guid collaboratorUid, int editionId, string collaboratorTypeName);
        //Task<IPagedList<CollaboratorApiListDto>> FindAllDropdownApiListDtoPaged(int editionId, string keywords, string collaboratorTypeName, int page, int pageSize);
        //Task<List<Collaborator>> FindAllByHightlightPosition(int editionId, Guid collaboratorTypeUid, int apiHighlightPosition, Guid? organizationUid);

        //#endregion

        //#region Old

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

        //#endregion
    }    
}