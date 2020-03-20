// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="LogisticTransferRepository.cs" company="Softo">
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

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Logistic Transfer IQueryable Extensions

    /// <summary>
    /// LogisticTransferIQueryableExtensions
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

        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> FindByUid(this IQueryable<LogisticTransfer> query, Guid uid)
        {
            return query.Where(e => e.Uid == uid);
        }

        /// <summary>Finds the by logistics uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> FindByLogisticsUid(this IQueryable<LogisticTransfer> query, Guid uid)
        {
            return query.Where(e => e.Logistic.Uid == uid);
        }
    }

    #endregion

    /// <summary>LogisticTransferRepository</summary>
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

        /// <summary>Finds all dtos asynchronous.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
        public Task<List<LogisticTransferDto>> FindAllDtosAsync(Guid logisticsUid)
        {
            var query = this.GetBaseQuery()
                                .FindByLogisticsUid(logisticsUid)
                                .Select(lt => new LogisticTransferDto
                                {
                                    LogisticTransfer = lt,
                                    FromPlaceDto = new PlaceDto
                                    {
                                        Place = lt.FromAttendeePlace.Place,
                                        AddressDto = lt.FromAttendeePlace.Place.Address == null || !lt.FromAttendeePlace.Place.Address.IsDeleted ? null : new AddressDto
                                        {
                                            Address = lt.FromAttendeePlace.Place.Address,
                                            City = lt.FromAttendeePlace.Place.Address.City,
                                            State = lt.FromAttendeePlace.Place.Address.State,
                                            Country = lt.FromAttendeePlace.Place.Address.Country
                                        }
                                    },
                                    ToPlaceDto = new PlaceDto
                                    {
                                        Place = lt.ToAttendeePlace.Place,
                                        AddressDto = lt.ToAttendeePlace.Place.Address == null || !lt.ToAttendeePlace.Place.Address.IsDeleted ? null : new AddressDto
                                        {
                                            Address = lt.ToAttendeePlace.Place.Address,
                                            City = lt.ToAttendeePlace.Place.Address.City,
                                            State = lt.ToAttendeePlace.Place.Address.State,
                                            Country = lt.ToAttendeePlace.Place.Address.Country
                                        }
                                    }
                                });

            return query
                        .OrderBy(ltd => ltd.LogisticTransfer.CreateDate)
                        .ToListAsync();
        }
    }
}