// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="HoldingRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Holding IQueryable Extensions

    /// <summary>
    /// HoldingIQueryableExtensions
    /// </summary>
    internal static class HoldingIQueryableExtensions
    {
        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Holding> FindByEditionId(this IQueryable<Holding> query, bool showAllEditions, int? editionId)
        {
            if (!showAllEditions && editionId.HasValue)
            {
                query = query.Where(h => h.Organizations.Any(o => o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId)));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<Holding> FindByKeywords(this IQueryable<Holding> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var predicate = PredicateBuilder.New<Holding>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        predicate = predicate.And(h => h.Name.Contains(keyword));
                    }
                }

                query = query.AsExpandable().Where(predicate);
            }

            return query;
        }
    }

    #endregion

    #region HoldingListDto IQueryable Extensions

    /// <summary>
    /// HoldingListDtoIQueryableExtensions
    /// </summary>
    internal static class HoldingListDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<HoldingListDto>> ToListPagedAsync(this IQueryable<HoldingListDto> query, int page, int pageSize)
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

    /// <summary></summary>
    public class HoldingRepository : Repository<PlataformaRio2CContext, Holding>, IHoldingRepository
    {
        /// <summary>Initializes a new instance of the <see cref="HoldingRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public HoldingRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<Holding> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                .Include(i => i.Descriptions)
                .Include(i => i.Descriptions.Select(t => t.Language));


            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        /// <summary>Gets all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<Holding>> GetAllAsync()
        {
            var query = this.GetAll();

            return await query.ToListAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<HoldingListDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, bool showAllEditions, int? editionId)
        {
            var query = this.GetAll()
                                .FindByKeywords(keywords)
                                .FindByEditionId(showAllEditions, editionId);

            return await query
                            .Select(h => new HoldingListDto
                            {
                                Uid = h.Uid,
                                Name = h.Name,
                                IsImageUploaded = h.IsImageUploaded,
                                CreateDate = h.CreateDate,
                                CreateUserId = h.CreateUserId,
                                UpdateDate = h.UpdateDate,
                                UpdateUserId = h.UpdateUserId,
                                //Creator = h.Creator,
                                //Updated = h.Updater,
                                //DescriptionsDtos = h.Descriptions.Select(d => new HoldingDescriptionBaseDto
                                //{
                                //    Uid = d.Uid,
                                //    Value = d.Value,
                                //    LanguageDto = new LanguageBaseDto
                                //    {
                                //        Uid = d.Language.Uid,
                                //        Name = d.Language.Name,
                                //        Code = d.Language.Code
                                //    }
                                //})
                            })
                            .DynamicOrder<HoldingListDto>(sortColumns, new List<string> { "name", "createDate" }, "name")
                            .ToListPagedAsync(page, pageSize);
        }

        public override IQueryable<Holding> GetAll(Expression<Func<Holding, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public override Holding Get(Expression<Func<Holding, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public override Holding Get(object id)
        {
            return this.dbSet
                            //.Include(i => i.Image)
                            .SingleOrDefault(x => x.Id == (int)id);
        }
    }
}