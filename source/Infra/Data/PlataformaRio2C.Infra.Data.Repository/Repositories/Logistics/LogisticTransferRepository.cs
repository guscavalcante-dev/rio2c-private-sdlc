// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="LogisticTransferRepository.cs" company="Softo">
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

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Logistic Transfer IQueryable Extensions

    /// <summary>
    /// LogisticTransferIQueryableExtensions
    /// </summary>
    internal static class LogisticTransferIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> FindByUid(this IQueryable<LogisticTransfer> query, Guid uid)
        {
            return query.Where(lt => lt.Uid == uid);
        }

        /// <summary>Finds the by logistics uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> FindByLogisticsUid(this IQueryable<LogisticTransfer> query, Guid uid)
        {
            return query.Where(lt => lt.Logistic.Uid == uid);
        }

        /// <summary>Finds the by attendee collaborator identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> FindByAttendeeCollaboratorId(this IQueryable<LogisticTransfer> query, int attendeeCollaboratorId)
        {
            return query.Where(lt => lt.Logistic.AttendeeCollaboratorId == attendeeCollaboratorId);
        }

        /// <summary>Finds the by date range.</summary>
        /// <param name="query">The query.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> FindByDateRange(this IQueryable<LogisticTransfer> query, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            query = query.Where(lt => lt.Date >= startDate && lt.Date <= endDate);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticTransfer> IsNotDeleted(this IQueryable<LogisticTransfer> query)
        {
            query = query.Where(lt => !lt.IsDeleted);

            return query;
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

        /// <summary>Finds all schedule dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public Task<List<LogisticTransferDto>> FindAllScheduleDtosAsync(int editionId, int attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var query = this.GetBaseQuery()
                                .FindByAttendeeCollaboratorId(attendeeCollaboratorId)
                                .FindByDateRange(startDate, endDate)
                                .Select(lt => new LogisticTransferDto
                                {
                                    LogisticTransfer = lt,
                                    FromPlaceDto = new PlaceDto
                                    {
                                        Place = lt.FromAttendeePlace.Place
                                    },
                                    ToPlaceDto = new PlaceDto
                                    {
                                        Place = lt.ToAttendeePlace.Place
                                    }
                                });

            return query
                        .ToListAsync();
        }
    }
}