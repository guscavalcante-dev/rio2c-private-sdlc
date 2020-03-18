// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="AttendeeLogisticSponsorsRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region IQueryable Extensions

    /// <summary>
    /// AttendeeLogisticSponsorsIQueryableExtensions
    /// </summary>
    internal static class AttendeeLogisticSponsorsIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeLogisticSponsorUid">The attendee logistic sponsor uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeLogisticSponsor> FindByUid(this IQueryable<AttendeeLogisticSponsor> query, Guid attendeeLogisticSponsorUid)
        {
            return query.Where(als => als.Uid == attendeeLogisticSponsorUid);
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeLogisticSponsor> FindByEditionId(this IQueryable<AttendeeLogisticSponsor> query, bool showAllEditions, int? editionId)
        {
            if (!showAllEditions && editionId.HasValue)
            {
                query = query.Where(als => als.EditionId == editionId && !als.Edition.IsDeleted);
            }

            return query;
        }

        /// <summary>Finds the by is other.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="isOther">if set to <c>true</c> [is other].</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeLogisticSponsor> FindByIsOther(this IQueryable<AttendeeLogisticSponsor> query, int editionId, bool isOther = false)
        {
            return query.Where(als => als.IsOther == isOther);
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeLogisticSponsor> FindByKeywords(this IQueryable<AttendeeLogisticSponsor> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<AttendeeLogisticSponsor>(false);
                var innerNameWhere = PredicateBuilder.New<AttendeeLogisticSponsor>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerNameWhere = innerNameWhere.And(als => als.LogisticSponsor.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is other required].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeLogisticSponsor> IsOtherRequired(this IQueryable<AttendeeLogisticSponsor> query)
        {
            query = query.Where(als => als.LogisticSponsor.IsOtherRequired);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeLogisticSponsor> IsNotDeleted(this IQueryable<AttendeeLogisticSponsor> query)
        {
            query = query.Where(als => !als.IsDeleted && !als.LogisticSponsor.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeLogisticSponsorsRepository</summary>
    public class AttendeeLogisticSponsorsRepository : Repository<PlataformaRio2CContext, AttendeeLogisticSponsor>, IAttendeeLogisticSponsorRepository
    {
        private PlataformaRio2CContext _context;

        /// <summary>Initializes a new instance of the <see cref="AttendeeLogisticSponsorsRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeLogisticSponsorsRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }
        
        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeLogisticSponsor> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all base dtos by is other asnyc.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="isOther">if set to <c>true</c> [is other].</param>
        /// <returns></returns>
        public async Task<List<AttendeeLogisticSponsorJsonDto>> FindAllBaseDtosByIsOtherAsnyc(int editionId, bool isOther)
        {
            var query = this.GetBaseQuery(true)
                                    .FindByEditionId(false, editionId)
                                    .FindByIsOther(editionId, isOther)
                                    .Select(c => new AttendeeLogisticSponsorJsonDto
                                    {
                                        Id = c.Id,
                                        Uid = c.Uid,
                                        Name = c.LogisticSponsor.Name,
                                        CreateDate = c.CreateDate,
                                        UpdateDate = c.UpdateDate,
                                        IsOthers = c.IsOther,
                                        IsOtherRequired = c.LogisticSponsor.IsOtherRequired
                                    });

            return await query
                            .ToListAsync();
        }

        /// <summary>Finds the other dto asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeLogisticSponsorJsonDto> FindOtherDtoAsync(int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .IsOtherRequired()
                                .Select(c => new AttendeeLogisticSponsorJsonDto
                                {
                                    Id = c.Id,
                                    Uid = c.Uid,
                                    Name = c.LogisticSponsor.Name,
                                    CreateDate = c.CreateDate,
                                    UpdateDate = c.UpdateDate,
                                    IsOthers = c.IsOther,
                                    IsOtherRequired = c.LogisticSponsor.IsOtherRequired
                                });

            return await query
                            .FirstOrDefaultAsync();
        }
    }
}