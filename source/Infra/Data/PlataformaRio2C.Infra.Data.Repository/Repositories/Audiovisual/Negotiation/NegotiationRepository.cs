// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="NegotiationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using Z.EntityFramework.Plus;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Negotiation IQueryable Extensions

    /// <summary>
    /// NegotiationIQueryableExtensions
    /// </summary>
    internal static class NegotiationIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByUid(this IQueryable<Negotiation> query, Guid negotiationUid)
        {
            query = query.Where(n => n.Uid == negotiationUid);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByEditionId(this IQueryable<Negotiation> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(n => (showAllEditions || n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.EditionId == editionId));

            return query;
        }

        /// <summary>Finds the by buyer attendee organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByBuyerOrganizationUid(this IQueryable<Negotiation> query, Guid? buyerOrganizationUid)
        {
            if (buyerOrganizationUid.HasValue)
            {
                query = query.Where(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization.Uid == buyerOrganizationUid);
            }

            return query;
        }

        /// <summary>Finds the by seller attendee organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindBySellerOrganizationUid(this IQueryable<Negotiation> query, Guid? sellerOrganizationUid)
        {
            if (sellerOrganizationUid.HasValue)
            {
                query = query.Where(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization.Uid == sellerOrganizationUid);
            }

            return query;
        }

        /// <summary>Finds the by project keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByProjectKeywords(this IQueryable<Negotiation> query, string projectKeywords)
        {
            if (!string.IsNullOrEmpty(projectKeywords))
            {
                var outerWhere = PredicateBuilder.New<Negotiation>(false);
                var innerProjectTitleWhere = PredicateBuilder.New<Negotiation>(true);

                foreach (var keyword in projectKeywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerProjectTitleWhere = innerProjectTitleWhere.Or(n => n.ProjectBuyerEvaluation.Project.ProjectTitles.Any(pt => !pt.IsDeleted && pt.Value.Contains(keyword)));
                    }
                }

                outerWhere = outerWhere.Or(innerProjectTitleWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Finds the by date.</summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationDate">The negotiation date.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByDate(this IQueryable<Negotiation> query, DateTime? negotiationDate)
        {
            if (negotiationDate.HasValue)
            {
                query = query.Where(n => DbFunctions.TruncateTime(n.StartDate) == DbFunctions.TruncateTime(negotiationDate));
            }

            return query;
        }

        /// <summary>Finds the by room uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByRoomUid(this IQueryable<Negotiation> query, Guid? roomUid)
        {
            if (roomUid.HasValue)
            {
                query = query.Where(n => n.Room.Uid == roomUid);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> IsNotDeleted(this IQueryable<Negotiation> query)
        {
            query = query.Where(n => !n.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>NegotiationRepository</summary>
    public class NegotiationRepository : Repository<PlataformaRio2CContext, Negotiation>, INegotiationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="NegotiationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public NegotiationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Negotiation> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the scheduled widget dto asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <param name="negotiationDate">The negotiation date.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        public async Task<List<NegotiationGroupedByDateDto>> FindScheduledWidgetDtoAsync(
            int editionId, 
            Guid? buyerOrganizationUid,
            Guid? sellerOrganizationUid, 
            string projectKeywords, 
            DateTime? negotiationDate, 
            Guid? roomUid)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByBuyerOrganizationUid(buyerOrganizationUid)
                                .FindBySellerOrganizationUid(sellerOrganizationUid)
                                .FindByProjectKeywords(projectKeywords)
                                .FindByDate(negotiationDate)
                                .FindByRoomUid(roomUid)
                                .Include(n => n.Room)
                                .Include(n => n.Room.RoomNames)
                                .Include(n => n.Room.RoomNames.Select(rn => rn.Language))
                                .Include(n => n.ProjectBuyerEvaluation)
                                .Include(n => n.ProjectBuyerEvaluation.Project)
                                .Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles)
                                .Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles.Select(pt => pt.Language))
                                .Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization)
                                .Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization)
                                .Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization)
                                .Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization);

            return (await query.ToListAsync())
                                .GroupBy(n => n.StartDate.ToUserTimeZone().Date)
                                .Select(nd => new NegotiationGroupedByDateDto(nd.Key, nd.ToList()))
                                .OrderBy(ngd => ngd.Date)
                                .ToList();
        }

        /// <summary>Truncates this instance.</summary>
        public void Truncate()
        {
            this._context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "TRUNCATE TABLE [dbo].[Negotiations]");
        }

        /// <summary>Creates multiple entities</summary>
        /// <param name="entities">Entities</param>
        public override void CreateAll(IEnumerable<Negotiation> entities)
        {
            try
            {
                this._context.BulkInsert(entities);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #region Old methods

        //public override IQueryable<Negotiation> GetAll(bool @readonly = false)
        //{
        //    var consult = this.dbSet;
        //                        //.Include(i => i.Player)
        //                        //.Include(i => i.Project)
        //                        //.Include(i => i.Project.ProjectTitles.Select(e => e.Language))
        //                        //.Include(i => i.Project.Producer)
        //                        //.Include(i => i.Room);
        //                        //.Include(i => i.Room.Names)
        //                        //.Include(i => i.Room.Names.Select(e => e.Language));

        //    return @readonly
        //                  ? consult.AsNoTracking()
        //                  : consult;
        //}

        //public override IQueryable<Negotiation> GetAllSimple()
        //{
        //    return this.dbSet
        //                       //.Include(i => i.Player)
        //                       //.Include(i => i.Project)
        //                       //.Include(i => i.Project.Producer)
        //                       //.Include(i => i.Room)
        //                       //.Include(i => i.Room.Names)
        //                       //.Include(i => i.Room.Names.Select(e => e.Language))
        //                       .AsNoTracking();

        //}


        //public IEnumerable<Player> GetAllPlayers()
        //{
        //    return this.dbSet
        //                       .Include(i => i.Player)
        //                       .Include(i => i.Player.Holding)
        //                       .AsNoTracking()
        //                       .ToList()
        //                       .Select(e => e.Player)
        //                       .GroupBy(e => e.Id)
        //                       .Select(e => e.First());

        //}

        //public IEnumerable<Producer> GetAllProducers()
        //{
        //    return null;
        //    //return this.dbSet
        //    //                   .Include(i => i.Project)
        //    //                   .Include(i => i.Project.Producer)
        //    //                   .AsNoTracking()
        //    //                   .ToList()
        //    //                   .Select(e => e.Project.Producer)
        //    //                   .GroupBy(e => e.Id)
        //    //                   .Select(e => e.First());
        //}

        //public IQueryable<Negotiation> GetAllBySchedule(Expression<Func<Negotiation, bool>> filter)
        //{
        //    return this.dbSet
        //                       //.Include(i => i.Player)
        //                       //.Include(i => i.Player.Holding)
        //                       //.Include(i => i.Project)
        //                       //.Include(i => i.Project.ProjectTitles)
        //                       //.Include(i => i.Project.ProjectTitles.Select(e => e.Language))
        //                       //.Include(i => i.Project.Producer)
        //                       //.Include(i => i.Room)
        //                       //.Include(i => i.Room.Names)
        //                       //.Include(i => i.Room.Names.Select(e => e.Language))
        //                       .Where(filter);
        //}

        #endregion
    }
}