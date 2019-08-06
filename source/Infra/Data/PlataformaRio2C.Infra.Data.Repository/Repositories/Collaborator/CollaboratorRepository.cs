// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="CollaboratorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    /// <summary>CollaboratorRepository</summary>
    public class CollaboratorRepository : Repository<PlataformaRio2CContext, Collaborator>, ICollaboratorRepository
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public CollaboratorRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<Collaborator> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Address)
                                .Include(i => i.User)
                                .Include(i => i.MiniBios)
                                .Include(i => i.MiniBios.Select(j => j.Language))
                                .Include(i => i.JobTitles)
                                .Include(i => i.JobTitles.Select(j => j.Language))
                                .Include(i => i.Players)
                                .Include(i => i.Players.Select(e => e.Holding))
                                .Include(i => i.Players.Select(e => e.Address))
                                .Include(i => i.ProducersEvents)
                                //.Include(i => i.Countries)
                                //.Include(i => i.Country)
                                .Include(i => i.ProducersEvents.Select(e => e.Producer))
                                .Include(i => i.ProducersEvents.Select(e => e.Producer.Address));

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        //public override IQueryable<Collaborator> GetAll(Expression<Func<Collaborator, bool>> filter)
        //{
        //    return this.GetAll().Where(filter);
        //}


        //public override Collaborator Get(Expression<Func<Collaborator, bool>> filter)
        //{
        //    return this.GetAll().FirstOrDefault(filter);
        //}

        //public override Collaborator Get(Guid uid)
        //{
        //    return this.GetAll().FirstOrDefault(e => e.Uid == uid);
        //}

        /// <summary>Método que remove a entidade do Contexto</summary>
        /// <param name="entity">Entidade</param>
        public override void Delete(Collaborator entity)
        {
            var UserRepository = new UserRepository(_context);

            entity.Players.Clear();

            if (entity.Image != null)
            {
                _context.Entry(entity.Image).State = EntityState.Deleted;
            }

            if (entity.User != null)
            {
                UserRepository.Delete(entity.User);
            }

            if (entity.Address != null)
            {
                _context.Entry(entity.Address).State = EntityState.Deleted;
            }

            if (entity.JobTitles != null && entity.JobTitles.Any())
            {
                var items = entity.JobTitles.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }

            if (entity.MiniBios != null && entity.MiniBios.Any())
            {
                var items = entity.MiniBios.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }

            if (entity.ProducersEvents != null && entity.ProducersEvents.Any())
            {
                entity.ProducersEvents.Clear();
            }

            base.Delete(entity);
        }

        /// <summary>Gets the status register collaborator by user identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Collaborator GetStatusRegisterCollaboratorByUserId(int id)
        {
            return this.dbSet
                                .Include(i => i.Address)
                                .Include(i => i.Players)
                                .Include(i => i.Players.Select(j => j.Address))
                                .Include(i => i.Players.Select(j => j.Interests))
                                .Include(i => i.ProducersEvents)
                                //.Include(i => i.Countries)
                                //.Include(i => i.Country)
                                .Include(i => i.ProducersEvents.Select(pe => pe.Producer))
                                .Include(i => i.ProducersEvents.Select(pe => pe.Producer.Address))
                                .FirstOrDefault(e => e.UserId == id);
        }

        /// <summary>Gets the with producer by user identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Collaborator GetWithProducerByUserId(int id)
        {
            return this.dbSet
                .Include(i => i.ProducersEvents)
                .Include(i => i.ProducersEvents.Select(e => e.Edition))
                .Include(i => i.ProducersEvents.Select(e => e.Producer))
                //.Include(i => i.Countries)
                //.Include(i => i.Country)
                .FirstOrDefault(e => e.UserId == id);
        }

        /// <summary>Gets the image.</summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public Collaborator GetImage(Guid uid)
        {
            return this.dbSet
                              .Include(i => i.Image)
                              .FirstOrDefault(e => e.Uid == uid);
        }

        /// <summary>Gets the with player and producer user identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Collaborator GetWithPlayerAndProducerUserId(int id)
        {
            return this.dbSet
                       .Include(i => i.Players)
                       .Include(i => i.ProducersEvents.Select(pe => pe.Producer))
                       .Include(i => i.Address)
                       .FirstOrDefault(e => e.UserId == id);
        }

        /// <summary>Gets the with player and producer uid.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Collaborator GetWithPlayerAndProducerUid(Guid id)
        {
            return this.dbSet
                       .Include(i => i.Players)
                       .Include(i => i.Players.Select(e => e.Holding))
                       .Include(i => i.ProducersEvents)
                       .Include(i => i.ProducersEvents.Select(pe => pe.Producer))
                       .FirstOrDefault(e => e.Uid == id);
        }

        /// <summary>Gets the options.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetOptions(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                                .Include(i => i.Players)
                                .Include(i => i.Players.Select(e => e.Holding))
                                .Include(i => i.ProducersEvents)
                                .Include(i => i.ProducersEvents.Select(e => e.Producer))
                                //.Include(i => i.Countries)
                                //.Include(i => i.Country)
                                .AsNoTracking()
                                .Where(filter);

        }

        /// <summary>Gets the collaborator producer options.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetCollaboratorProducerOptions(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                             .Include(i => i.ProducersEvents)
                             .Include(i => i.ProducersEvents.Select(e => e.Producer))
                             //.Include(i => i.Countries)
                             //.Include(i => i.Country)
                             .AsNoTracking()
                             .Where(filter);
        }

        /// <summary>Gets the collaborator player options.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetCollaboratorPlayerOptions(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                             .Include(i => i.Players)
                             .Include(i => i.Players.Select(e => e.Holding))
                             //.Include(i => i.Countries)
                             //.Include(i => i.Country)
                             .AsNoTracking()
                             .Where(filter);
        }

        /// <summary>Gets the options chat.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetOptionsChat(int userId)
        {
            return this.dbSet
                            .Include(i => i.User)
                            .Include(i => i.Players)
                            .Include(i => i.JobTitles)
                            .Include(i => i.JobTitles.Select(e => e.Language))
                            .Include(i => i.Players.Select(e => e.Holding))
                            .Include(i => i.ProducersEvents)
                            .Include(i => i.ProducersEvents.Select(e => e.Edition))
                            .Include(i => i.ProducersEvents.Select(e => e.Producer))
                            .AsNoTracking()
                            .Where(e => e.UserId != userId);
        }

        /// <summary>Gets the by schedule.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Collaborator GetBySchedule(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                                .Include(i => i.ProducersEvents)
                                .Include(i => i.ProducersEvents.Select(e => e.Edition))
                                .Include(i => i.ProducersEvents.Select(e => e.Producer))
                                .Include(i => i.ProducersEvents.Select(e => e.Producer.EventsCollaborators))
                                .Include(i => i.ProducersEvents.Select(e => e.Producer.EventsCollaborators.Select(ev => ev.Collaborator)))
                                .Include(i => i.Players)
                                .Include(i => i.Players.Select(e => e.Collaborators))
                                .FirstOrDefault(filter);
        }

        public Collaborator GetById(int id)
        {
            return this.GetAll().FirstOrDefault(e => e.Id == id);
        }


        /// <summary>Gets all simple.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public override IQueryable<Collaborator> GetAllSimple(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                            .Include(i => i.User)
                            .Include(i => i.Players)
                            //.Include(i => i.Countries)
                            //.Include(i => i.Country)
                            .Include(i => i.Players.Select(e => e.Holding));
        }

        /// <summary>Gets all simple.</summary>
        /// <returns></returns>
        public override IQueryable<Collaborator> GetAllSimple()
        {
            return this.dbSet
                            .Include(i => i.User)
                            .Include(i => i.Players)
                            //.Include(i => i.Countries)
                            //.Include(i => i.Country)
                            .Include(i => i.Players.Select(e => e.Holding));
        }
    }
}