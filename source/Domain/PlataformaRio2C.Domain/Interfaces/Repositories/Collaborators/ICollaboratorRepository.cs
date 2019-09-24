// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="ICollaboratorRepository.cs" company="Softo">
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
    /// <summary>ICollaboratorRepository</summary>
    public interface ICollaboratorRepository : IRepository<Collaborator>
    {
        Task<CollaboratorDto> FindDtoByUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<IPagedList<CollaboratorBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, Guid organizationTypeId, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, int? editionId);
        Task<int> CountAllByDataTable(Guid organizationTypeId, bool showAllEditions, int? editionId);
        Task<Collaborator> FindBySalesPlatformAttendeeIdAsync(string salesPlatformAttendeeId);

        #region Old

        Collaborator GetById(int id);
        Collaborator GetStatusRegisterCollaboratorByUserId(int id);
        Collaborator GetWithProducerByUserId(int id);
        Collaborator GetWithPlayerAndProducerUserId(int id);
        Collaborator GetWithPlayerAndProducerUid(Guid id);
        Collaborator GetImage(Guid uid);
        IEnumerable<Collaborator> GetOptions(Expression<Func<Collaborator, bool>> filter);
        IEnumerable<Collaborator> GetCollaboratorProducerOptions(Expression<Func<Collaborator, bool>> filter);
        IEnumerable<Collaborator> GetCollaboratorPlayerOptions(Expression<Func<Collaborator, bool>> filter);
        IEnumerable<Collaborator> GetOptionsChat(int userId);
        Collaborator GetBySchedule(Expression<Func<Collaborator, bool>> filter);

        #endregion
    }    
}