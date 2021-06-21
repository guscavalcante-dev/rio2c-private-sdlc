// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-20-2021
// ***********************************************************************
// <copyright file="InterestRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Interest IQueryable Extensions

    /// <summary>
    /// InterestIQueryableExtensions
    /// </summary>
    internal static class InterestIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        internal static IQueryable<Interest> FindByUid(this IQueryable<Interest> query, Guid interestUid)
        {
            query = query.Where(i => i.Uid == interestUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="interestsUids">The interests uids.</param>
        /// <returns></returns>
        internal static IQueryable<Interest> FindByUids(this IQueryable<Interest> query, List<Guid> interestsUids)
        {
            if (interestsUids?.Any() == true)
            {
                query = query.Where(i => interestsUids.Contains(i.Uid));
            }

            return query;
        }

        /// <summary>Finds the by interest group uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="interestGroupUid">The interest group uid.</param>
        /// <returns></returns>
        internal static IQueryable<Interest> FindByInterestGroupUid(this IQueryable<Interest> query, Guid interestGroupUid)
        {
            query = query.Where(i => i.InterestGroup.Uid == interestGroupUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Interest> IsNotDeleted(this IQueryable<Interest> query)
        {
            query = query.Where(i => !i.IsDeleted
                                     && !i.InterestGroup.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Interest> Order(this IQueryable<Interest> query)
        {
            query = query.OrderBy(i => i.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>InterestRepository</summary>
    public class InterestRepository : Repository<PlataformaRio2CContext, Interest>, IInterestRepository
    {
        /// <summary>Initializes a new instance of the <see cref="InterestRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InterestRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Interest> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all dto asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<InterestDto>> FindAllDtosAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .Order()
                            .Select(i => new InterestDto
                            {
                                Interest = i,
                                InterestGroup = i.InterestGroup
                            })
                            .ToListAsync();
        }

        /// <summary>Finds all dtos by interest group uid asynchronous.</summary>
        /// <param name="interestGroupUid">The interest group uid.</param>
        /// <returns></returns>
        public async Task<List<InterestDto>> FindAllDtosByInterestGroupUidAsync(Guid interestGroupUid)
        {
            var query = this.GetBaseQuery()
                                .FindByInterestGroupUid(interestGroupUid);

            return await query
                        .Order()
                        .Select(i => new InterestDto
                        {
                            Interest = i,
                            InterestGroup = i.InterestGroup
                        })
                        .ToListAsync();
        }

        /// <summary>Finds all grouped by interest groups asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<IGrouping<InterestGroup, Interest>>> FindAllGroupedByInterestGroupsAsync()
        {
            var query = this.GetBaseQuery()
                                    .GroupBy(i => i.InterestGroup);

            return await query
                            .OrderBy(ig => ig.Key.DisplayOrder)
                            .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="interestsUids">The interests uids.</param>
        /// <returns></returns>
        public async Task<List<Interest>> FindAllByUidsAsync(List<Guid> interestsUids)
        {
            var query = this.GetBaseQuery()
                                .Order()
                                .FindByUids(interestsUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all by interest group uid asynchronous.
        /// </summary>
        /// <param name="interestGroupUid">The interest group uid.</param>
        /// <returns></returns>
        public async Task<List<Interest>> FindAllByInterestGroupUidAsync(Guid interestGroupUid)
        {
            var query = this.GetBaseQuery()
                .FindByInterestGroupUid(interestGroupUid);

            return await query
                .Order()
                .ToListAsync();
        }

        #region Old

        public override IQueryable<Interest> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.InterestGroup);

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public override IQueryable<Interest> GetAll(Expression<Func<Interest, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }


        public override Interest Get(Expression<Func<Interest, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public override Interest Get(Guid uid)
        {
            return this.GetAll().FirstOrDefault(e => e.Uid == uid);
        }

        public override Interest Get(object id)
        {
            return this.dbSet
                .Include(i => i.InterestGroup)
                .SingleOrDefault(x => x.Id == (int)id);
            
        }

        #endregion
    }
}