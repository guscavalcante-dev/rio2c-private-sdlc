// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="LogisticAccommodationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Logistic Acommodation IQueryable Extensions

    /// <summary>
    /// LogisticAccommodationIQueryableExtensions
    /// </summary>
    internal static class LogisticAccommodationIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> FindByUid(this IQueryable<LogisticAccommodation> query, Guid uid)
        {
            return query.Where(la => la.Uid == uid);
        }

        /// <summary>Finds the by logistics uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> FindByLogisticsUid(this IQueryable<LogisticAccommodation> query, Guid uid)
        {
            return query.Where(la => la.Logistic.Uid == uid);
        }

        /// <summary>Finds the by attendee collaborator identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> FindByAttendeeCollaboratorId(this IQueryable<LogisticAccommodation> query, int attendeeCollaboratorId)
        {
            return query.Where(e => e.Logistic.AttendeeCollaboratorId == attendeeCollaboratorId);
        }

        /// <summary>Finds the by date range.</summary>
        /// <param name="query">The query.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> FindByDateRange(this IQueryable<LogisticAccommodation> query, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            query = query.Where(la => (la.CheckInDate >= startDate && la.CheckInDate <= endDate)
                                     || (la.CheckOutDate >= startDate && la.CheckOutDate <= endDate));

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> IsNotDeleted(this IQueryable<LogisticAccommodation> query)
        {
            query = query.Where(la => !la.IsDeleted);

            return query;
        }
    }

    #endregion

    #region LogisticAccommodationDto IQueryable Extensions

    /// <summary>
    /// CollaboratorBaseDtoIQueryableExtensions
    /// </summary>
    internal static class LogisticAccommodationDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<LogisticAccommodationDto>> ToListPagedAsync(this IQueryable<LogisticAccommodationDto> query, int page, int pageSize)
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

    /// <summary>LogisticAccommodationRepository</summary>
    public class LogisticAccommodationRepository : Repository<PlataformaRio2CContext, LogisticAccommodation>, ILogisticAccommodationRepository
    {
        public LogisticAccommodationRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<LogisticAccommodation> GetBaseQuery(bool @readonly = false)
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
        public Task<List<LogisticAccommodationDto>> FindAllDtosAsync(Guid logisticsUid)
        {
            var query = this.GetBaseQuery()
                            .FindByLogisticsUid(logisticsUid)
                            .Select(la => new LogisticAccommodationDto
                            {
                                LogisticAccommodation = la,
                                PlaceDto = new PlaceDto
                                {
                                    Place = la.AttendeePlace.Place,
                                    AddressDto = la.AttendeePlace.Place.Address == null || !la.AttendeePlace.Place.Address.IsDeleted ? null : new AddressDto
                                    {
                                        Address = la.AttendeePlace.Place.Address,
                                        City = la.AttendeePlace.Place.Address.City,
                                        State = la.AttendeePlace.Place.Address.State,
                                        Country = la.AttendeePlace.Place.Address.Country
                                    }
                                }
                            })
                            .OrderBy(lad => lad.LogisticAccommodation.CreateDate);

            return query
                        .ToListAsync();
        }

        /// <summary>Finds all schedule dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public Task<List<LogisticAccommodationDto>> FindAllScheduleDtosAsync(int editionId, int attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var query = this.GetBaseQuery()
                                .FindByAttendeeCollaboratorId(attendeeCollaboratorId)
                                .FindByDateRange(startDate, endDate)
                                .Select(la => new LogisticAccommodationDto
                                {
                                    LogisticAccommodation = la,
                                    PlaceDto = new PlaceDto
                                    {
                                        Place = la.AttendeePlace.Place
                                    }
                                });

            return query
                        .ToListAsync();
        }
    }
}