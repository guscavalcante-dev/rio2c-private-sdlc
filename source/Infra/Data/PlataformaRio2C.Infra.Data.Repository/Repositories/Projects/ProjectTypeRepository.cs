// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="ProjectTypeRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LinqKit;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Project Type IQueryable Extensions

    /// <summary>
    /// ProjectTypeIQueryableExtensions
    /// </summary>
    internal static class ProjectTypeIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectTypeUid">The project type uid.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectType> FindByUid(this IQueryable<ProjectType> query, Guid projectTypeUid)
        {
            query = query.Where(pt => pt.Uid == projectTypeUid);

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectType> FindByKeywords(this IQueryable<ProjectType> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var predicate = PredicateBuilder.New<ProjectType>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        predicate = predicate.And(pt => pt.Name.Contains(keyword));
                    }
                }

                query = query.AsExpandable().Where(predicate);
            }

            return query;
        }
    }

    #endregion

    //#region OrganizationBaseDto IQueryable Extensions

    ///// <summary>
    ///// OrganizationBaseDtoIQueryableExtensions
    ///// </summary>
    //internal static class OrganizationBaseDtoIQueryableExtensions
    //{
    //    /// <summary>
    //    /// To the list paged.
    //    /// </summary>
    //    /// <param name="query">The query.</param>
    //    /// <param name="page">The page.</param>
    //    /// <param name="pageSize">Size of the page.</param>
    //    /// <returns></returns>
    //    internal static async Task<IPagedList<OrganizationBaseDto>> ToListPagedAsync(this IQueryable<OrganizationBaseDto> query, int page, int pageSize)
    //    {
    //        page++;

    //        // Page the list
    //        var pagedList = await query.ToPagedListAsync(page, pageSize);
    //        if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
    //            pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

    //        return pagedList;
    //    }
    //}

    //#endregion

    /// <summary>ProjectTypeRepository</summary>
    public class ProjectTypeRepository : Repository<PlataformaRio2CContext, ProjectType>, IProjectTypeRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectTypeRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ProjectTypeRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<ProjectType> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet;
            //.Include(i => i.Descriptions)
            //.Include(i => i.Descriptions.Select(t => t.Language));


            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }
    }
}