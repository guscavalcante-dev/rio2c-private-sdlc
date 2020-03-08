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
    internal static class AttendeePlaceIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> IsNotDeleted(this IQueryable<AttendeePlace> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> FindByUid(this IQueryable<AttendeePlace> query, Guid uid)
        {
            return query.Where(e => e.Uid == uid);
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> FindByEditionId(this IQueryable<AttendeePlace> query, int editionId)
        {
            return query.Where(e => e.EditionId == editionId);
        }
    }

    #endregion
    
    public class AttendeePlacesRepository : Repository<PlataformaRio2CContext, AttendeePlace>, IAttendeePlacesRepository
    {
        public AttendeePlacesRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeePlace> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        public Task<List<AttendeePlaceDto>> FindAllDtosByEdition(int editionId)
        {
            return this.GetBaseQuery()
                .FindByEditionId(editionId)
                .Select(e => new AttendeePlaceDto()
                {
                    Id = e.Id,
                    Name = e.Place.Name
                })
                .OrderBy(e => e.Name)
                .ToListAsync();
        }
    }
}
