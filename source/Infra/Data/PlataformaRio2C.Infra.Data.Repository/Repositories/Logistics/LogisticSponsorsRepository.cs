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
        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByEditionId(this IQueryable<LogisticSponsor> query, bool showAllEditions, int? editionId)
        {
            if (!showAllEditions && editionId.HasValue)
            {
                query = query.Where(o => o.AttendeeLogisticSponsors.Any(ac => ac.EditionId == editionId
                                                                           && !ac.IsDeleted
                                                                           && !ac.Edition.IsDeleted));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByKeywords(this IQueryable<LogisticSponsor> query, string keywords, int? editionId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<LogisticSponsor>(false);
                var innerNameWhere = PredicateBuilder.New<LogisticSponsor>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerNameWhere = innerNameWhere.And(t => t.Name.Contains(keyword));
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
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticSponsor> FindByUid(this IQueryable<LogisticSponsor> query, Guid uid)
        {
            return query.Where(e => e.Uid == uid);
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
            bool showAllEditions,
            int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByEditionId(showAllEditions, editionId);

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
        
        public async Task<LogisticSponsorBaseDto> FindLogisticSponsorDtoByUid(Guid sponsorUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(sponsorUid);

            return await query
                            .Select(c => new LogisticSponsorBaseDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                Name = c.Name,
                                IsAirfareTicketRequired = c.IsAirfareTicketRequired
                            }).FirstOrDefaultAsync();

        }

        public async Task<List<LogisticSponsorBaseDto>> FindAllDtosByEditionUidAsync(int editionId)
        {
            var query = this.GetBaseQuery(true)
                .FindByEditionId(false, editionId);

            return await query
                .Select(c => new LogisticSponsorBaseDto
                {
                    Id = c.Id,
                    Uid = c.Uid,
                    Name = c.Name,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    IsOtherRequired = c.IsOtherRequired
                }).ToListAsync();
        }
    }
}