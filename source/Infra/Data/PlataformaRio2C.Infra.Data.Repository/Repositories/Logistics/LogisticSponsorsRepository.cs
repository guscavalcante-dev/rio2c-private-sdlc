// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="SpeakerRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    
    #region Collaborator IQueryable Extensions

    /// <summary>
    /// CollaboratorIQueryableExtensions
    /// </summary>
    internal static class LogisticSponsorsIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByUid(this IQueryable<LogisticSponsor> query, Guid collaboratorUid)
        {
            query = query.Where(c => c.Uid == collaboratorUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByUids(this IQueryable<LogisticSponsor> query, List<Guid> collaboratorsUids)
        {
            if (collaboratorsUids?.Any() == true)
            {
                query = query.Where(c => collaboratorsUids.Contains(c.Uid));
            }

            return query;
        }

        /// <summary>Finds the by sales platform attendee identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindBySalesPlatformAttendeeId(this IQueryable<LogisticSponsor> query, string salesPlatformAttendeeId)
        {
            //query = query.Where(c => c.AttendeeCollaborators.Any(ac => ac.AttendeeCollaboratorTickets.Any(act => !act.IsDeleted
            //                                                                                                     && act.SalesPlatformAttendeeId == salesPlatformAttendeeId)));

            return query;
        }

        /// <summary>Finds the by collaborator type name and by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByCollaboratorTypeNameAndByEditionId(this IQueryable<LogisticSponsor> query, string collaboratorTypeName, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, int? editionId)
        {
            //query = query.Where(c => c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
            //                                                           && !ac.IsDeleted
            //                                                           && !ac.Edition.IsDeleted
            //                                                           && (showAllParticipants
            //                                                               || (showAllExecutives && ac.AttendeeOrganizationCollaborators
            //                                                                                                .Any(aoc => !aoc.IsDeleted))
            //                                                               || (!showAllExecutives && ac.AttendeeCollaboratorTypes
            //                                                                                                .Any(act => !act.IsDeleted
            //                                                                                                            && !act.CollaboratorType.IsDeleted
            //                                                                                                            && act.CollaboratorType.Name == collaboratorTypeName)))));

            return query;
        }

        /// <summary>Finds the by organization type uid and by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByOrganizationTypeUidAndByEditionId(this IQueryable<LogisticSponsor> query, Guid organizationTypeUid, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, int? editionId)
        {
            //query = query.Where(c => c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
            //                                                           && !ac.IsDeleted
            //                                                           && !ac.Edition.IsDeleted
            //                                                           && (showAllParticipants
            //                                                               || ac.AttendeeOrganizationCollaborators
            //                                                                       .Any(aoc => !aoc.IsDeleted
            //                                                                                   && !aoc.AttendeeOrganization.IsDeleted
            //                                                                                   && aoc.AttendeeOrganization.AttendeeOrganizationTypes
            //                                                                                           .Any(aot => !aot.IsDeleted
            //                                                                                                       && (showAllExecutives || aot.OrganizationType.Uid == organizationTypeUid))))));

            return query;
        }

        /// <summary>Finds the by highlights.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByHighlights(this IQueryable<LogisticSponsor> query, string collaboratorTypeName, bool? showHighlights)
        {
            //if (showHighlights.HasValue && showHighlights.Value)
            //{
            //    query = query.Where(o => o.AttendeeCollaborators.Any(ac => !ac.IsDeleted
            //                                                               && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
            //                                                                                                          && act.CollaboratorType.Name == collaboratorTypeName
            //                                                                                                          && act.IsApiDisplayEnabled
            //                                                                                                          && act.ApiHighlightPosition.HasValue)));
            //}

            return query;
        }

        /// <summary>Finds the by API highlights.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="highlights">The highlights.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByApiHighlights(this IQueryable<LogisticSponsor> query, string collaboratorTypeName, int? highlights)
        {
            //if (highlights == 0 || highlights == 1)
            //{
            //    query = query.Where(o => o.AttendeeCollaborators.Any(ac => !ac.IsDeleted
            //                                                               && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
            //                                                                                                          && act.CollaboratorType.Name == collaboratorTypeName
            //                                                                                                          && act.IsApiDisplayEnabled
            //                                                                                                          && (highlights.Value == 1 ? act.ApiHighlightPosition.HasValue : !act.ApiHighlightPosition.HasValue))));
            //}

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByEditionId(this IQueryable<LogisticSponsor> query, bool showAllEditions, int? editionId)
        {
            //if (!showAllEditions && editionId.HasValue)
            //{
            //    query = query.Where(o => o.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
            //                                                               && !ac.IsDeleted
            //                                                               && !ac.Edition.IsDeleted));
            //}

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByKeywords(this IQueryable<LogisticSponsor> query, string keywords, int? editionId)
        {
            //if (!string.IsNullOrEmpty(keywords))
            //{
            //    var outerWhere = PredicateBuilder.New<Collaborator>(false);
            //    var innerExecutiveBadgeNameWhere = PredicateBuilder.New<Collaborator>(true);
            //    var innerExecutiveNameWhere = PredicateBuilder.New<Collaborator>(true);
            //    var innerExecutiveEmailWhere = PredicateBuilder.New<Collaborator>(true);
            //    var innerOrganizationNameWhere = PredicateBuilder.New<Collaborator>(true);
            //    var innerHoldingNameWhere = PredicateBuilder.New<Collaborator>(true);

            //    foreach (var keyword in keywords.Split(' '))
            //    {
            //        if (!string.IsNullOrEmpty(keyword))
            //        {
            //            innerExecutiveBadgeNameWhere = innerExecutiveBadgeNameWhere.And(c => c.Badge.Contains(keyword));
            //            innerExecutiveNameWhere = innerExecutiveNameWhere.And(c => c.User.Name.Contains(keyword));
            //            innerExecutiveEmailWhere = innerExecutiveEmailWhere.And(c => c.User.Email.Contains(keyword));
            //            innerOrganizationNameWhere = innerOrganizationNameWhere
            //                                            .And(c => c.AttendeeCollaborators.Any(ac => (!editionId.HasValue || ac.EditionId == editionId)
            //                                                                                        && !ac.IsDeleted
            //                                                                                        && !ac.Edition.IsDeleted
            //                                                                                        && ac.AttendeeOrganizationCollaborators
            //                                                                                                .Any(aoc => !aoc.IsDeleted
            //                                                                                                            && !aoc.AttendeeOrganization.IsDeleted
            //                                                                                                            && !aoc.AttendeeOrganization.Organization.IsDeleted
            //                                                                                                            && aoc.AttendeeOrganization.Organization.Name.Contains(keyword))));
            //            innerHoldingNameWhere = innerHoldingNameWhere
            //                                            .And(c => c.AttendeeCollaborators.Any(ac => (!editionId.HasValue || ac.EditionId == editionId)
            //                                                                                        && !ac.IsDeleted
            //                                                                                        && !ac.Edition.IsDeleted
            //                                                                                        && ac.AttendeeOrganizationCollaborators
            //                                                                                            .Any(aoc => !aoc.IsDeleted
            //                                                                                                        && !aoc.AttendeeOrganization.IsDeleted
            //                                                                                                        && !aoc.AttendeeOrganization.Organization.IsDeleted
            //                                                                                                        && !aoc.AttendeeOrganization.Organization.Holding.IsDeleted
            //                                                                                                        && aoc.AttendeeOrganization.Organization.Holding.Name.Contains(keyword))));

            //        }
            //    }

            //    outerWhere = outerWhere.Or(innerExecutiveBadgeNameWhere);
            //    outerWhere = outerWhere.Or(innerExecutiveNameWhere);
            //    outerWhere = outerWhere.Or(innerExecutiveEmailWhere);
            //    outerWhere = outerWhere.Or(innerOrganizationNameWhere);
            //    outerWhere = outerWhere.Or(innerHoldingNameWhere);
            //    query = query.Where(outerWhere);
            //}

            return query;
        }

        /// <summary>Determines whether [is API display enabled] [the specified edition identifier].</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> IsApiDisplayEnabled(this IQueryable<LogisticSponsor> query, int editionId, string collaboratorTypeName)
        {
            //query = query.Where(c => c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
            //                                                           && ac.AttendeeCollaboratorTypes.Any(aot => !aot.IsDeleted
            //                                                                                                      && aot.CollaboratorType.Name == collaboratorTypeName
            //                                                                                                      && aot.IsApiDisplayEnabled)));

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> IsNotDeleted(this IQueryable<LogisticSponsor> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }
    }

    #endregion
    
    #region CollaboratorBaseDto IQueryable Extensions

    /// <summary>
    /// CollaboratorBaseDtoIQueryableExtensions
    /// </summary>
    internal static class LogisticSponsorsBaseDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<LogisticSponsorBaseDto>> ToListPagedAsync(this IQueryable<LogisticSponsorBaseDto> query, int page, int pageSize)
        {
            page++;

            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    //#region CollaboratorApiListDto IQueryable Extensions

    ///// <summary>
    ///// CollaboratorApiListDtoIQueryableExtensions
    ///// </summary>
    //internal static class CollaboratorApiListDtoIQueryableExtensions
    //{
    //    /// <summary>Converts to listpagedasync.</summary>
    //    /// <param name="query">The query.</param>
    //    /// <param name="page">The page.</param>
    //    /// <param name="pageSize">Size of the page.</param>
    //    /// <returns></returns>
    //    internal static async Task<IPagedList<CollaboratorApiListDto>> ToListPagedAsync(this IQueryable<CollaboratorApiListDto> query, int page, int pageSize)
    //    {
    //        // Page the list
    //        var pagedList = await query.ToPagedListAsync(page, pageSize);
    //        if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
    //            pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

    //        return pagedList;
    //    }
    //}

    //#endregion
    /// <summary>SpeakerRepository</summary>
    public class LogisticSponsorsRepository : Repository<PlataformaRio2CContext, LogisticSponsor>, ILogisticSponsorRepository
    {
        private PlataformaRio2CContext _context;

        /// <summary>Initializes a new instance of the <see cref="SpeakerRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public LogisticSponsorsRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }
        
        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<LogisticSponsor> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        public async Task<IPagedList<LogisticSponsorBaseDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns, 
            List<Guid> collaboratorsUids,
            string collaboratorTypeName,
            bool showAllEditions,
            bool showAllExecutives,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeName, showAllEditions, showAllExecutives, showAllParticipants, editionId)
                                .FindByUids(collaboratorsUids)
                                .FindByHighlights(collaboratorTypeName, showHighlights);

            return await query
                            .DynamicOrder<LogisticSponsor>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("Name", "Name"),
                                },
                                new List<string> { "Name", "CreateDate", "UpdateDate" }, "Name")
                            .Select(c => new LogisticSponsorBaseDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                Name = c.Name,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,                                
                                IsInCurrentEdition = c.AttendeeLogisticSponsors.Any(o => !o.IsDeleted
                                                                                && o.EditionId == editionId
                                                                                && !o.Edition.IsDeleted
                                                                                && !o.IsDeleted)
                            })
                            .ToListPagedAsync(page, pageSize);
        }
    }
}