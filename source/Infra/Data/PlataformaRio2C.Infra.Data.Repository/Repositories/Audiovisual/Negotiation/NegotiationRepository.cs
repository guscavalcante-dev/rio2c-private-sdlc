// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
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
using EFBulkInsert;

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