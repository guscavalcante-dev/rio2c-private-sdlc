﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="CountryRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Country IQueryable Extensions

    /// <summary>
    /// CountryIQueryableExtensions
    /// </summary>
    internal static class CountryIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Country> IsNotDeleted(this IQueryable<Country> query)
        {
            query = query.Where(h => !h.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the country by name.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static IQueryable<Country> FindByName(this IQueryable<Country> query, string name)
        {
            query = query.Where(c => c.Name.Contains(name));

            return query;
        }
    }

    #endregion

    #region CountryBaseDto IQueryable Extensions

    /// <summary>
    /// CountryDtoIQueryableExtensions
    /// </summary>
    internal static class CountryDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<CountryBaseDto>> ToListPagedAsync(this IQueryable<CountryBaseDto> query, int page, int pageSize)
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

    /// <summary>CountryRepository</summary>
    public class CountryRepository : Repository<PlataformaRio2CContext, Country>, ICountryRepository
    {
        /// <summary>Initializes a new instance of the <see cref="CountryRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public CountryRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Country> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<CountryBaseDto>> FindAllBaseDtosAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .Select(c => new CountryBaseDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                Name = c.Name,
                                Code = c.Code,
                                CompanyNumberMask = c.CompanyNumberMask,
                                ZipCodeMask = c.ZipCodeMask,
                                PhoneNumberMask = c.PhoneNumberMask,
                                MobileMask = c.MobileMask,
                                IsCompanyNumberRequired = c.IsCompanyNumberRequired,
                                CreateDate = c.CreateDate,
                                CreateUserId = c.CreateUserId,
                                UpdateDate = c.UpdateDate,
                                UpdateUserId = c.UpdateUserId,
                                Ordering = (c.Code == "BR" ? 0 : 1)
                            })
                            .OrderBy(c => c.Ordering).ThenBy(c => c.Name)
                            .ToListAsync();
        }

        /// <summary>
        /// Finds the country by name asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<Country> FindByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var query = this.GetBaseQuery()
                            .FindByName(name);

            return await query.FirstOrDefaultAsync();
        }
    }
}