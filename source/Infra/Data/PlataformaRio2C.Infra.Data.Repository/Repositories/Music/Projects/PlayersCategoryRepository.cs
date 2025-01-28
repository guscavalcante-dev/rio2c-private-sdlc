using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.BusinessRoundProjects;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories.Music.Projects
{
    #region Activity IQueryable Extensions

    /// <summary>
    /// ActivityIQueryableExtensions
    /// </summary>
    internal static class PlayersCategoryRepositoryIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="activityUid">The activity uid.</param>
        /// <returns></returns>
        internal static IQueryable<PlayerCategory> FindByUid(this IQueryable<PlayerCategory> query, Guid playersCategoryUid)
        {
            query = query.Where(a => a.Uid == playersCategoryUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="playersCategoryUid">The playersCategoryUid uids.</param>
        /// <returns></returns>
        internal static IQueryable<PlayerCategory> FindByUids(this IQueryable<PlayerCategory> query, List<Guid> playersCategoryUid)
        {
            if (playersCategoryUid?.Any() == true)
            {
                query = query.Where(a => playersCategoryUid.Contains(a.Uid));
            }

            return query;
        }

        /// <summary>
        /// Finds the by project type identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <returns></returns>
        internal static IQueryable<PlayerCategory> FindByProjectTypeId(this IQueryable<PlayerCategory> query, int projectTypeId)
        {
            query = query.Where(a => a.ProjectTypeId == projectTypeId);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<PlayerCategory> IsNotDeleted(this IQueryable<PlayerCategory> query)
        {
            query = query.Where(a => !a.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<PlayerCategory> Order(this IQueryable<PlayerCategory> query)
        {
            query = query.OrderBy(a => a.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>ActivityRepository</summary>
    public class PlayersCategoryRepository : Repository<PlataformaRio2CContext, PlayerCategory>, IPlayersCategoryRepository
    {
        /// <summary>Initializes a new instance of the <see cref="PlayersCategoryRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public PlayersCategoryRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<PlayerCategory> GetBaseQuery(bool @readonly = false)
        {
            var consult = dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }


        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<PlayerCategory>> FindAllByProjectTypeIdAsync(int projectTypeId)
        {
            var query = GetBaseQuery()
                                .FindByProjectTypeId(projectTypeId);

            return await query
                            .Order()
                            .ToListAsync();
        }

        public async Task<List<PlayerCategory>> FindAllByUidsAsync(List<Guid> playersCategoryUid)
        {
            var query = GetBaseQuery()
                                .FindByUids(playersCategoryUid);

            return await query
                            .Order()
                            .ToListAsync();
        }
    }
}
