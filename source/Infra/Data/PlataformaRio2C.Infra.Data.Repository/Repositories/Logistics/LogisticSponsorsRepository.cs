// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="LogisticSponsorsRepository.cs" company="Softo">
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
    #region Logistic Sponsor IQueryable Extensions

    /// <summary>
    /// LogisticSponsorsIQueryableExtensions
    /// </summary>
    internal static class LogisticSponsorsIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="logisticSponsorUid">The logistic sponsor uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByUid(this IQueryable<LogisticSponsor> query, Guid logisticSponsorUid)
        {
            return query.Where(ls => ls.Uid == logisticSponsorUid);
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByEditionId(this IQueryable<LogisticSponsor> query, int editionId, bool showAllEditions)
        {
            if (!showAllEditions)
            {
                query = query.Where(ls => ls.AttendeeLogisticSponsors.Any(als => als.EditionId == editionId && !als.IsDeleted));
            }

            return query;
        }

        internal static IQueryable<LogisticSponsor> FindByIsOther(this IQueryable<LogisticSponsor> query, int editionId, bool isOther = false)
        {
            return query.Where(o => o.AttendeeLogisticSponsors.Any(ac => ac.EditionId == editionId
                                                                         && !ac.IsDeleted
                                                                         && !ac.Edition.IsDeleted
                                                                         && ac.IsOther == isOther));
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByKeywords(this IQueryable<LogisticSponsor> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<LogisticSponsor>(false);
                var innerNameWhere = PredicateBuilder.New<LogisticSponsor>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerNameWhere = innerNameWhere.And(ls => ls.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> IsNotDeleted(this IQueryable<LogisticSponsor> query)
        {
            query = query.Where(ls => !ls.IsDeleted);

            return query;
        }
    }

    #endregion

    #region LogisticSponsorJsonDto IQueryable Extensions

    /// <summary>
    /// LogisticSponsorsBaseDtoIQueryableExtensions
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
        internal static async Task<IPagedList<LogisticSponsorJsonDto>> ToListPagedAsync(this IQueryable<LogisticSponsorJsonDto> query, int page, int pageSize)
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

    /// <summary>LogisticSponsorsRepository</summary>
    public class LogisticSponsorsRepository : Repository<PlataformaRio2CContext, LogisticSponsor>, ILogisticSponsorRepository
    {
        private PlataformaRio2CContext _context;

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorsRepository"/> class.</summary>
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

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="placeUid">The place uid.</param>
        /// <returns></returns>
        public async Task<LogisticSponsorDto> FindDtoAsync(int editionId, Guid placeUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(placeUid)
                                .Select(ls => new LogisticSponsorDto
                                {
                                    LogisticSponsor = ls,
                                    AttendeeLogisticSponsor = ls.AttendeeLogisticSponsors.FirstOrDefault(als => als.EditionId == editionId && !als.IsDeleted && !als.Edition.IsDeleted)
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the main information widget dto asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="placeUid">The place uid.</param>
        /// <returns></returns>
        public async Task<LogisticSponsorDto> FindMainInformationWidgetDtoAsync(int editionId, Guid placeUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(placeUid)
                                .Select(ls => new LogisticSponsorDto
                                {
                                    LogisticSponsor = ls,
                                    AttendeeLogisticSponsor = ls.AttendeeLogisticSponsors.FirstOrDefault(als => als.EditionId == editionId && !als.IsDeleted && !als.Edition.IsDeleted)
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all by data table asynchronous.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<LogisticSponsorJsonDto>> FindAllByDataTableAsync(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllEditions,
            int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords)
                                .FindByEditionId(editionId, showAllEditions);

            return await query
                            .DynamicOrder<LogisticSponsor>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("Name", "Name"),
                                },
                                new List<string> { "Name", "CreateDate", "UpdateDate" }, "Name")
                            .Select(c => new LogisticSponsorJsonDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                Name = c.Name,
                                IsOther = !c.AttendeeLogisticSponsors.Any(als => als.EditionId == editionId && !als.IsDeleted && !als.Edition.IsDeleted) ? null :
                                          (bool?)c.AttendeeLogisticSponsors.Any(als => als.EditionId == editionId && !als.IsDeleted && !als.Edition.IsDeleted && als.IsOther),
                                IsInCurrentEdition = c.AttendeeLogisticSponsors.Any(als => als.EditionId == editionId && !als.IsDeleted && !als.Edition.IsDeleted),
                                IsInOtherEdition = c.AttendeeLogisticSponsors.Any(als => als.EditionId != editionId && !als.IsDeleted && !als.Edition.IsDeleted),
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,                                
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts all by data table asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTableAsync(int editionId, bool showAllEditions)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions);

            return await query
                            .CountAsync();
        }

        public async Task<LogisticSponsorJsonDto> FindLogisticSponsorDtoByUid(Guid sponsorUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(sponsorUid);

            return await query
                            .Select(c => new LogisticSponsorJsonDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                Name = c.Name,
                                IsAirfareTicketRequired = c.IsAirfareTicketRequired
                            }).FirstOrDefaultAsync();
        }

        public async Task<List<LogisticSponsorJsonDto>> FindAllDtosByEditionUidAsync(int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByEditionId(editionId, false)
                                .FindByIsOther(editionId);

            return await query
                            .Select(c => new LogisticSponsorJsonDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                Name = c.Name,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                                IsOtherRequired = c.IsOtherRequired
                            }).ToListAsync();
        }

        public async Task<List<LogisticSponsorJsonDto>> FindAllDtosByIsOther(int editionId)
        {
            var query = this.GetBaseQuery(true)
                .FindByEditionId(editionId, false)
                .FindByIsOther(editionId, true);

            return await query
                .Select(c => new LogisticSponsorJsonDto
                {
                    Id = c.Id,
                    Uid = c.Uid,
                    Name = c.Name,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    IsOtherRequired = c.IsOtherRequired
                }).ToListAsync();
        }

        public async Task<Guid> GetByIsOthersRequired()
        {
            return await this.GetBaseQuery(true)
                                .Where(e => e.IsOtherRequired)
                                .Select(e => e.Uid)
                                .FirstOrDefaultAsync();
        }
    }
}