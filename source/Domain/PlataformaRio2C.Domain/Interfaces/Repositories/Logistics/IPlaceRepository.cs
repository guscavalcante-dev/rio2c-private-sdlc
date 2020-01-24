// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="IPlaceRepository.cs" company="Softo">
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
    /// <summary>
    /// IPlaceRepository
    /// </summary>
    public interface IPlaceRepository : IRepository<Place>
    {
        /// <summary>
        /// Finds all collaborators by collaborators uids.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <returns>Task&lt;List&lt;AdminAccessControlDto&gt;&gt;.</returns>
        Task<List<AdminAccessControlDto>> FindAllCollaboratorsByCollaboratorsUids(int editionId, List<Guid> collaboratorsUids);
        /// <summary>
        /// Finds the dto by uid and by edition identifier asynchronous.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>Task&lt;CollaboratorDto&gt;.</returns>
        Task<CollaboratorDto> FindDtoByUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        /// <summary>
        /// Finds all by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">if set to <c>true</c> [show highlights].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>Task&lt;IPagedList&lt;CollaboratorBaseDto&gt;&gt;.</returns>
        Task<IPagedList<CollaboratorBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, List<Guid> collaboratorsUids, string collaboratorTypeName, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, bool? showHighlights, int? editionId);
        /// <summary>
        /// Counts all by data table.
        /// </summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> CountAllByDataTable(string collaboratorTypeName, bool showAllEditions, int? editionId);
        /// <summary>
        /// Finds the by sales platform attendee identifier asynchronous.
        /// </summary>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <returns>Task&lt;Collaborator&gt;.</returns>
        Task<Collaborator> FindBySalesPlatformAttendeeIdAsync(string salesPlatformAttendeeId);

        #region Api

        /// <summary>
        /// Finds all public API paged.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="highlights">The highlights.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Task&lt;IPagedList&lt;CollaboratorApiListDto&gt;&gt;.</returns>
        Task<IPagedList<CollaboratorApiListDto>> FindAllPublicApiPaged(int editionId, string keywords, int? highlights, string collaboratorTypeName, int page, int pageSize);
        /// <summary>
        /// Finds the public API dto asynchronous.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns>Task&lt;SpeakerCollaboratorDto&gt;.</returns>
        Task<SpeakerCollaboratorDto> FindPublicApiDtoAsync(Guid collaboratorUid, int editionId, string collaboratorTypeName);
        /// <summary>
        /// Finds all dropdown API list dto paged.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Task&lt;IPagedList&lt;CollaboratorApiListDto&gt;&gt;.</returns>
        Task<IPagedList<CollaboratorApiListDto>> FindAllDropdownApiListDtoPaged(int editionId, string keywords, string collaboratorTypeName, int page, int pageSize);
        /// <summary>
        /// Finds all by hightlight position.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns>Task&lt;List&lt;Collaborator&gt;&gt;.</returns>
        Task<List<Collaborator>> FindAllByHightlightPosition(int editionId, Guid collaboratorTypeUid, int apiHighlightPosition, Guid? organizationUid);

        #endregion

        #region Old

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Collaborator.</returns>
        Collaborator GetById(int id);
        /// <summary>
        /// Gets the status register collaborator by user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Collaborator.</returns>
        Collaborator GetStatusRegisterCollaboratorByUserId(int id);
        /// <summary>
        /// Gets the with producer by user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Collaborator.</returns>
        Collaborator GetWithProducerByUserId(int id);
        /// <summary>
        /// Gets the with player and producer user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Collaborator.</returns>
        Collaborator GetWithPlayerAndProducerUserId(int id);
        /// <summary>
        /// Gets the with player and producer uid.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Collaborator.</returns>
        Collaborator GetWithPlayerAndProducerUid(Guid id);
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns>Collaborator.</returns>
        Collaborator GetImage(Guid uid);
        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>IEnumerable&lt;Collaborator&gt;.</returns>
        IEnumerable<Collaborator> GetOptions(Expression<Func<Collaborator, bool>> filter);
        /// <summary>
        /// Gets the collaborator producer options.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>IEnumerable&lt;Collaborator&gt;.</returns>
        IEnumerable<Collaborator> GetCollaboratorProducerOptions(Expression<Func<Collaborator, bool>> filter);
        /// <summary>
        /// Gets the collaborator player options.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>IEnumerable&lt;Collaborator&gt;.</returns>
        IEnumerable<Collaborator> GetCollaboratorPlayerOptions(Expression<Func<Collaborator, bool>> filter);
        /// <summary>
        /// Gets the options chat.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IEnumerable&lt;Collaborator&gt;.</returns>
        IEnumerable<Collaborator> GetOptionsChat(int userId);
        /// <summary>
        /// Gets the by schedule.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Collaborator.</returns>
        Collaborator GetBySchedule(Expression<Func<Collaborator, bool>> filter);

        #endregion
    }    
}