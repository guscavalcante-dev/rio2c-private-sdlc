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
    internal static class LogisticTransferIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> IsNotDeleted(this IQueryable<LogisticTransfer> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> FindByUid(this IQueryable<LogisticTransfer> query, Guid uid)
        {
            return query.Where(e => e.Uid == uid);
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> FindByLogisticsUid(this IQueryable<LogisticTransfer> query, Guid uid)
        {
            return query.Where(e => e.Logistics.Uid == uid);
        }
    }

    #endregion
    
    public class LogisticTransferRepository : Repository<PlataformaRio2CContext, LogisticTransfer>, ILogisticTransferRepository
    {
        public LogisticTransferRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<LogisticTransfer> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        public Task<List<LogisticTransferDto>> FindAllDtosPaged(Guid logisticsUid)
        {
            return this.GetBaseQuery()
                .FindByLogisticsUid(logisticsUid)
                .Select(e => new LogisticTransferDto()
                {
                    AdditionalInfo = e.AdditionalInfo,
                    Id = e.Id,
                    Uid = e.Uid,
                    CreateDate = e.CreateDate,
                    Date = e.Date,
                    FromAttendeePlace = e.FromAttendeePlace.Place.Name,
                    ToAttendeePlace = e.ToAttendeePlace.Place.Name,
                    UpdateDate = e.UpdateDate
                })
                .OrderBy(e => e.CreateDate)
                .ToListAsync();
        }
    }
}
