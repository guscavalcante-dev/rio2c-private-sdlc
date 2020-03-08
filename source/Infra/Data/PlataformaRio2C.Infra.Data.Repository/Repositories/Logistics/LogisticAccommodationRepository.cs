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
    #region IQueryable Extensions

    /// <summary>
    /// CollaboratorIQueryableExtensions
    /// </summary>
    internal static class LogisticAccommodationIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> IsNotDeleted(this IQueryable<LogisticAccommodation> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> FindByUid(this IQueryable<LogisticAccommodation> query, Guid uid)
        {
            return query.Where(e => e.Uid == uid);
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> FindByLogisticsUid(this IQueryable<LogisticAccommodation> query, Guid uid)
        {
            return query.Where(e => e.Logistics.Uid == uid);
        }
    }

    #endregion

    #region LogisticAccommodationDto IQueryable Extensions

    /// <summary>
    /// CollaboratorBaseDtoIQueryableExtensions
    /// </summary>
    internal static class LogisticAccommodationDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<LogisticAccommodationDto>> ToListPagedAsync(this IQueryable<LogisticAccommodationDto> query, int page, int pageSize)
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

    public class LogisticAccommodationRepository : Repository<PlataformaRio2CContext, LogisticAccommodation>, ILogisticAccommodationRepository
    {
        public LogisticAccommodationRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<LogisticAccommodation> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        public Task<List<LogisticAccommodationDto>> FindAllDtosPaged(Guid logisticsUid)
        {
            return this.GetBaseQuery()
                .FindByLogisticsUid(logisticsUid)
                .Select(e => new LogisticAccommodationDto()
                {
                    Id = e.Id,
                    Uid = e.Uid,
                    AdditionalInfo = e.AdditionalInfo,
                    AttendeePlace = e.AttendeePlace.Place.Name,
                    CheckOutDate = e.CheckOutDate,
                    CheckInDate = e.CheckInDate,
                    CreateDate = e.CreateDate,
                    UpdateDate = e.UpdateDate
                })
                .OrderBy(e => e.CreateDate)
                .ToListAsync();
        }
    }
}
