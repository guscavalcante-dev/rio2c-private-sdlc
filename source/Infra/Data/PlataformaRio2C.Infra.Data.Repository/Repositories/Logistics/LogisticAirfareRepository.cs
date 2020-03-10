// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="LogisticAirfareRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using Z.EntityFramework.Plus;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region LogisticAirfare IQueryable Extensions

    /// <summary>
    /// LogisticAirfareIQueryableExtensions
    /// </summary>
    internal static class LogisticAirfareIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="logisticAirfareUid">The logistic airfare uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAirfare> FindByUid(this IQueryable<LogisticAirfare> query, Guid logisticAirfareUid)
        {
            return query.Where(la => la.Uid == logisticAirfareUid);
        }

        /// <summary>Finds the by logistics uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="logisticUid">The logistic uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAirfare> FindByLogisticsUid(this IQueryable<LogisticAirfare> query, Guid logisticUid)
        {
            return query.Where(la => la.Logistic.Uid == logisticUid);
        }

        /// <summary>Finds the by edition uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAirfare> FindByEditionUid(this IQueryable<LogisticAirfare> query, Guid editionUid)
        {
            return query.Where(la => la.Logistic.AttendeeCollaborator.Edition.Uid == editionUid);
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAirfare> IsNotDeleted(this IQueryable<LogisticAirfare> query)
        {
            query = query.Where(la => !la.IsDeleted && !la.Logistic.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>LogisticAirfareRepository</summary>
    public class LogisticAirfareRepository : Repository<PlataformaRio2CContext, LogisticAirfare>, ILogisticAirfareRepository
    {
        public LogisticAirfareRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<LogisticAirfare> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all json dtos asynchronous.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
        public Task<List<LogisticAirfareJsonDto>> FindAllJsonDtosAsync(Guid logisticsUid)
        {
            var query = this.GetBaseQuery()
                            .FindByLogisticsUid(logisticsUid)
                            .Select(la => new LogisticAirfareJsonDto
                            {
                                AdditionalInfo = la.AdditionalInfo,
                                Id = la.Id,
                                Uid = la.Uid,
                                CreateDate = la.CreateDate,
                                ArrivalDate = la.ArrivalDate,
                                DepartureDate = la.DepartureDate,
                                IsNational = la.IsNational,
                                IsArrival = la.IsArrival,
                                From = la.From,
                                To = la.To,
                                TicketNumber = la.TicketNumber,
                                TicketUploadDate = la.TicketUploadDate,
                                UpdateDate = la.UpdateDate
                            });
                
            return query
                        .OrderBy(e => e.CreateDate)
                        .ToListAsync();
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="logisticAirfareUid">The logistic airfare uid.</param>
        /// <returns></returns>
        public async Task<LogisticAirfareDto> FindDtoAsync(Guid logisticAirfareUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(logisticAirfareUid)
                                .Select(la => new LogisticAirfareDto
                                {
                                    LogisticAirfare = la
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all for generate negotiations asynchronous.</summary>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        public async Task<List<LogisticAirfare>> FindAllForGenerateNegotiationsAsync(Guid editionUid)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionUid(editionUid)
                                .IncludeFilter(la => la.Logistic)
                                .IncludeFilter(la => la.Logistic.AttendeeCollaborator)
                                .IncludeFilter(la => la.Logistic.AttendeeCollaborator.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted));

            return await query
                            .OrderBy(la => la.DepartureDate)
                            .ToListAsync();
        }
    }
}