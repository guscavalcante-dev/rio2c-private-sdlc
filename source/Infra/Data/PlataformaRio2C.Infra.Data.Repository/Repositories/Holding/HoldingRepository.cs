// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-18-2019
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
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="holdingUid">The holding uid.</param>
        /// <returns></returns>
        internal static IQueryable<Holding> FindByUid(this IQueryable<Holding> query, Guid holdingUid)
        {
            query = query.Where(h => h.Uid == holdingUid);

            return query;
        }

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

    #region HoldingBaseDto IQueryable Extensions

    /// <summary>
    /// HoldingBaseDtoIQueryableExtensions
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
        internal static async Task<IPagedList<HoldingBaseDto>> ToListPagedAsync(this IQueryable<HoldingBaseDto> query, int page, int pageSize)
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

        /// <summary>Finds the dto by uid asynchronous.</summary>
        /// <param name="holdingUid">The holding uid.</param>
        /// <returns></returns>
        public async Task<HoldingDto> FindDtoByUidAsync(Guid holdingUid)
        {
            var query = this.GetAll()
                                .FindByUid(holdingUid);

            return await query
                            .Select(h => new HoldingDto
                            {
                                Id = h.Id,
                                Uid = h.Uid,
                                Name = h.Name,
                                IsImageUploaded = h.IsImageUploaded,
                                CreateDate = h.CreateDate,
                                CreateUserId = h.CreateUserId,
                                UpdateDate = h.UpdateDate,
                                UpdateUserId = h.UpdateUserId,
                                //Creator = h.Creator,
                                UpdaterDto = new UserBaseDto
                                {
                                    Uid = h.Updater.Uid,
                                    Name = h.Updater.Name,
                                    Email =h.Updater.Email 
                                },
                                DescriptionsDtos = h.Descriptions.Select(d => new HoldingDescriptionDto
                                {
                                    Id = d.Id,
                                    Uid = d.Uid,
                                    Value = d.Value,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = d.Language.Id,
                                        Uid = d.Language.Uid,
                                        Name = d.Language.Name,
                                        Code = d.Language.Code
                                    }
                                })
                            }).FirstOrDefaultAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<HoldingBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, bool showAllEditions, int? editionId)
        {
            var query = this.GetAll()
                                .FindByKeywords(keywords)
                                .FindByEditionId(showAllEditions, editionId);

            return await query
                            .Select(h => new HoldingBaseDto
                            {
                                Id = h.Id,
                                Uid = h.Uid,
                                Name = h.Name,
                                IsImageUploaded = h.IsImageUploaded,
                                CreateDate = h.CreateDate,
                                UpdateDate = h.UpdateDate,
                            })
                            .DynamicOrder<HoldingBaseDto>(sortColumns, new List<string> { "name", "createDate", "updateDate" }, "name")
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts all by data table.</summary>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(bool showAllEditions, int? editionId)
        {
            var query = this.GetAll()
                                .FindByEditionId(showAllEditions, editionId);

            return await query.CountAsync();
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