// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
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
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Logistic> IsNotDeleted(this IQueryable<Logistic> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<Logistic> FindByUid(this IQueryable<Logistic> query, Guid uid)
        {
            return query.Where(e => e.Uid == uid);
        }
    }

    #endregion

    #region LogisticRequestBaseDto IQueryable Extensions

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
        internal static async Task<IPagedList<LogisticRequestBaseDto>> ToListPagedAsync(this IQueryable<LogisticRequestBaseDto> query, int page, int pageSize)
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

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<Logistic> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet;
            //.Include(i => i.Collaborator.Players)
            //.Include(i => i.Collaborator.Players.Select(e => e.Holding))
            //.Include(i => i.Collaborator.ProducersEvents)
            //.Include(i => i.Collaborator.ProducersEvents.Select(e => e.Producer));

            return @readonly
                      ? consult.AsNoTracking()
                      : consult;
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showAllSponsored">if set to <c>true</c> [show all sponsored].</param>
        /// <returns></returns>
        public async Task<IPagedList<LogisticRequestBaseDto>> FindAllByDataTable(int page,
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
                .Select(c => new LogisticRequestBaseDto
                {
                    Id = c.Id,
                    Uid = c.Uid,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                })
                .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Gets the dto.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public Task<LogisticRequestBaseDto> GetDto(Guid id, Language language)
        {
            var query = this.GetBaseQuery();

            return query.FindByUid(id).Select(e => new LogisticRequestBaseDto
            {
                CollaboratorUid = e.AttendeeCollaborator.Collaborator.Uid,
                Name = e.AttendeeCollaborator.Collaborator.FirstName + " " +
                       e.AttendeeCollaborator.Collaborator.LastNames,
                Id = e.Id,
                Uid = e.Uid,
                AccommodationSponsor = e.AccommodationSponsor.LogisticSponsor.Name,
                AirfareSponsor = e.AirfareSponsor.LogisticSponsor.Name,
                AirportTransferSponsor = e.AirportTransferSponsor.LogisticSponsor.Name,
                AdditionalInfo = e.AdditionalInfo,
                TransferCity = e.IsCityTransferRequired,
                IsVehicleDisposalRequired = e.IsVehicleDisposalRequired,
                CreateDate = e.CreateDate,
                CreateUser = e.CreateUser.Name,
                CollaboratorPillars = e.AttendeeCollaborator.ConferenceParticipants
                                            .SelectMany(cp => cp.Conference.ConferencePillars)
                                            .Select(p => p.Pillar)
                                            .ToList(),
                CollaboratorRoles = e.AttendeeCollaborator.ConferenceParticipants
                                        .Select(cp => cp.ConferenceParticipantRole)
                                        .SelectMany(cp => cp.ConferenceParticipantRoleTitles.Where(t => t.LanguageId == language.Id))
                                        .Select(cp => cp.Value)
                                        .ToList()

            }).FirstOrDefaultAsync();
        }
    }
}