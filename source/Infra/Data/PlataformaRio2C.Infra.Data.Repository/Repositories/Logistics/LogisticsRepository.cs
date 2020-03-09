using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region IQueryable Extensions

    /// <summary>
    /// CollaboratorIQueryableExtensions
    /// </summary>
    internal static class LogisticsIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Logistics> IsNotDeleted(this IQueryable<Logistics> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Logistics> FindByUid(this IQueryable<Logistics> query, Guid uid)
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

    public class LogisticsRepository : Repository<PlataformaRio2CContext, Logistics>, ILogisticsRepository
    {
        public LogisticsRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Logistics> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        public override IQueryable<Logistics> GetAll(bool @readonly = false)
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

        public async Task<IPagedList<LogisticRequestBaseDto>> FindAllByDataTable(int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllParticipants,
            bool showAllSponsored)
        {
            var query = this.GetBaseQuery();

            return await query
                .DynamicOrder<Logistics>(
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

        public Task<LogisticRequestBaseDto> GetDto(Guid id, Language language)
        {
            var query = this.GetBaseQuery();

            return query.FindByUid(id).Select(e => new LogisticRequestBaseDto()
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
