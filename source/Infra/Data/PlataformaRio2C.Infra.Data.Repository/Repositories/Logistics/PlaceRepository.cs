// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="PlaceRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Place IQueryable Extensions

    /// <summary>
    /// PlaceIQueryableExtensions
    /// </summary>
    internal static class PlaceIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="placeUid">The place uid.</param>
        /// <returns></returns>
        internal static IQueryable<Place> FindByUid(this IQueryable<Place> query, Guid placeUid)
        {
            return query.Where(p => p.Uid == placeUid);
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<Place> FindByEditionId(this IQueryable<Place> query, int editionId, bool showAllEditions)
        {
            if (!showAllEditions)
            {
                query = query.Where(p => showAllEditions || p.AttendeePlaces.Any(ap => ap.EditionId == editionId && !ap.IsDeleted));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Place> FindByKeywords(this IQueryable<Place> query, string keywords, int languageId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Place>(false);
                var innerPlaceNameWhere = PredicateBuilder.New<Place>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerPlaceNameWhere = innerPlaceNameWhere.And(p => p.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerPlaceNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Place> IsNotDeleted(this IQueryable<Place> query)
        {
            query = query.Where(p => !p.IsDeleted);

            return query;
        }
    }

    #endregion

    #region PlaceJsonDto IQueryable Extensions

    /// <summary>
    /// PlaceJsonDtoIQueryableExtensions
    /// </summary>
    internal static class PlaceJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<PlaceJsonDto>> ToListPagedAsync(this IQueryable<PlaceJsonDto> query, int page, int pageSize)
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

    /// <summary>PlaceRepository</summary>
    public class PlaceRepository : Repository<PlataformaRio2CContext, Place>, IPlaceRepository
    {
        public PlaceRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Place> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="placeUid">The place uid.</param>
        /// <returns></returns>
        public async Task<PlaceDto> FindDtoAsync(Guid placeUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(placeUid)
                                .Select(p => new PlaceDto
                                {
                                    Place = p
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the main information widget dto asynchronous.</summary>
        /// <param name="placeUid">The place uid.</param>
        /// <returns></returns>
        public async Task<PlaceDto> FindMainInformationWidgetDtoAsync(Guid placeUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(placeUid)
                                .Select(p => new PlaceDto
                                {
                                    Place = p
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<PlaceJsonDto>> FindAllByDataTableAsync(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            int editionId,
            bool showAllEditions,
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, languageId)
                                .FindByEditionId(editionId, showAllEditions);

            return await query
                            .DynamicOrder<Place>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "CreateDate", "UpdateDate", "Name" }, "CreateDate")
                            .Select(p => new PlaceJsonDto
                            {
                                Id = p.Id,
                                Uid = p.Uid,
                                Name = p.Name,
                                IsHotel = p.IsHotel,
                                IsAirport = p.IsAirport,
                                IsInCurrentEdition = p.AttendeePlaces.Any(ap => ap.EditionId == editionId && !ap.IsDeleted && !ap.Edition.IsDeleted),
                                IsInOtherEdition = p.AttendeePlaces.Any(ap => ap.EditionId != editionId && !ap.IsDeleted && !ap.Edition.IsDeleted),
                                CreateDate = p.CreateDate,
                                UpdateDate = p.UpdateDate
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts all by data table.</summary>
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
    }
}
