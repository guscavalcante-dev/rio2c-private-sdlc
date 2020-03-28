// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="LogisticRepository.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Logistic IQueryable Extensions

    /// <summary>LogisticIQueryableExtensions</summary>
    internal static class LogisticIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="logisticUid">The logistic uid.</param>
        /// <returns></returns>
        internal static IQueryable<Logistic> FindByUid(this IQueryable<Logistic> query, Guid logisticUid)
        {
            return query.Where(l => l.Uid == logisticUid);
        }

        /// <summary>Finds the by attendee collaborator identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Logistic> FindByAttendeeCollaboratorId(this IQueryable<Logistic> query, int attendeeCollaboratorId)
        {
            query = query.Where(l => l.AttendeeCollaboratorId == attendeeCollaboratorId);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Logistic> IsNotDeleted(this IQueryable<Logistic> query)
        {
            query = query.Where(l => !l.IsDeleted);

            return query;
        }
    }

    #endregion

    #region LogisticJsonDto IQueryable Extensions

    /// <summary>
    /// CollaboratorBaseDtoIQueryableExtensions
    /// </summary>
    internal static class LogisticRequestBaseDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<LogisticJsonDto>> ToListPagedAsync(this IQueryable<LogisticJsonDto> query, int page, int pageSize)
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

    /// <summary>LogisticRepository</summary>
    public class LogisticRepository : Repository<PlataformaRio2CContext, Logistic>, ILogisticRepository
    {
        /// <summary>Initializes a new instance of the <see cref="LogisticRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public LogisticRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Logistic> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all by data table asynchronous.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showAllSponsored">if set to <c>true</c> [show all sponsored].</param>
        /// <returns></returns>
        public async Task<IPagedList<LogisticJsonDto>> FindAllByDataTableAsync(int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllParticipants,
            bool showAllSponsored)
        {
            var query = this.GetBaseQuery();

            return await query
                            .DynamicOrder<Logistic>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("CreateDate", "CreateDate"),
                                },
                                new List<string> { "CreateDate", "UpdateDate" }, "CreateDate")
                            .Select(l => new LogisticJsonDto
                            {
                                Id = l.Id,
                                Uid = l.Uid,
                                CreateDate = l.CreateDate,
                                UpdateDate = l.UpdateDate,
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="logisticUid">The logistic uid.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public Task<LogisticDto> FindDtoAsync(Guid logisticUid, Language language)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(logisticUid)
                                .Select(l => new LogisticDto
                                {
                                    Logistic = l,
                                    AttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                    {
                                        AttendeeCollaborator = l.AttendeeCollaborator,
                                        Collaborator = l.AttendeeCollaborator.Collaborator
                                    }
                                });

            return query
                        .FirstOrDefaultAsync();
        }

        /// <summary>Finds the main information widget dto asynchronous.</summary>
        /// <param name="logisticUid">The logistic uid.</param>
        /// <returns></returns>
        public Task<LogisticDto> FindMainInformationWidgetDtoAsync(Guid logisticUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(logisticUid)
                                .Select(l => new LogisticDto
                                {
                                    Logistic = l,
                                    AttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                    {
                                        AttendeeCollaborator = l.AttendeeCollaborator,
                                        Collaborator = l.AttendeeCollaborator.Collaborator,
                                        ConferenceParticipantDtos = l.AttendeeCollaborator.ConferenceParticipants.Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted).Select(cp => new ConferenceParticipantDto
                                        {
                                            ConferenceParticipant = cp,
                                            ConferenceDto = new ConferenceDto
                                            {
                                                Conference = cp.Conference,
                                                ConferencePillarDtos = cp.Conference.ConferencePillars.Where(cvt => !cvt.IsDeleted).Select(cvt => new ConferencePillarDto()
                                                {
                                                    ConferencePillar = cvt,
                                                    Pillar = cvt.Pillar
                                                })
                                            },
                                            ConferenceParticipantRoleDto = new ConferenceParticipantRoleDto
                                            {
                                                ConferenceParticipantRole = cp.ConferenceParticipantRole,
                                                ConferenceParticipantRoleTitleDtos = cp.ConferenceParticipantRole.ConferenceParticipantRoleTitles.Where(cprt => !cprt.IsDeleted).Select(cprt => new ConferenceParticipantRoleTitleDto
                                                {
                                                    ConferenceParticipantRoleTitle = cprt,
                                                    LanguageDto = new LanguageBaseDto
                                                    {
                                                        Id = cprt.Language.Id,
                                                        Uid = cprt.Language.Uid,
                                                        Name = cprt.Language.Name,
                                                        Code = cprt.Language.Code
                                                    }
                                                })
                                            }
                                        })
                                    },
                                    AirfareAttendeeLogisticSponsorDto = !l.IsAirfareSponsored || l.AirfareAttendeeLogisticSponsorId == null ? null :
                                         new AttendeeLogisticSponsorDto
                                         {
                                             AttendeeLogisticSponsor = l.AirfareAttendeeLogisticSponsor,
                                             LogisticSponsor = l.AirfareAttendeeLogisticSponsor.LogisticSponsor
                                         },
                                    AccommodationAttendeeLogisticSponsorDto = !l.IsAccommodationSponsored || l.AccommodationAttendeeLogisticSponsorId == null ? null :
                                        new AttendeeLogisticSponsorDto
                                        {
                                            AttendeeLogisticSponsor = l.AccommodationAttendeeLogisticSponsor,
                                            LogisticSponsor = l.AccommodationAttendeeLogisticSponsor.LogisticSponsor
                                        },
                                    AirportTransferAttendeeLogisticSponsorDto = !l.IsAirportTransferSponsored || l.AirportTransferAttendeeLogisticSponsorId == null ? null :
                                        new AttendeeLogisticSponsorDto
                                        {
                                            AttendeeLogisticSponsor = l.AirportTransferAttendeeLogisticSponsor,
                                            LogisticSponsor = l.AirportTransferAttendeeLogisticSponsor.LogisticSponsor
                                        },
                                    CreateUserDto = new UserDto
                                    {
                                        User = l.CreateUser,
                                        Collaborator = l.CreateUser.Collaborator
                                    }
                                });

            return query
                        .FirstOrDefaultAsync();
        }
    }
}