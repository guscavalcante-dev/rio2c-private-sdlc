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
    public class CollaboratorRepository : Repository<PlataformaRio2CContext, Collaborator>, ICollaboratorRepository
    {
        public CollaboratorRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

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

        public Collaborator GetWithProducerByUserId(int id)
        {
            return this.dbSet
                .Include(i => i.ProducersEvents)
                .Include(i => i.ProducersEvents.Select(e => e.Event))
                .Include(i => i.ProducersEvents.Select(e => e.Producer))
                //.Include(i => i.Countries)
                //.Include(i => i.Country)
                .FirstOrDefault(e => e.UserId == id);
        }

        public Collaborator GetImage(Guid uid)
        {
            return this.dbSet
                              .Include(i => i.Image)
                              .FirstOrDefault(e => e.Uid == uid);
        }

        public Collaborator GetWithPlayerAndProducerUserId(int id)
        {
            return this.dbSet
                       .Include(i => i.Players)
                       .Include(i => i.ProducersEvents.Select(pe => pe.Producer))
                       .Include(i => i.Address)
                       .FirstOrDefault(e => e.UserId == id);
        }

        public Collaborator GetWithPlayerAndProducerUid(Guid id)
        {
            return this.dbSet
                       .Include(i => i.Players)
                       .Include(i => i.Players.Select(e => e.Holding))
                       .Include(i => i.ProducersEvents)
                       .Include(i => i.ProducersEvents.Select(pe => pe.Producer))
                       .FirstOrDefault(e => e.Uid == id);
        }

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

        public IEnumerable<Collaborator> GetOptionsChat(int userId)
        {
            return this.dbSet
                            .Include(i => i.User)
                            .Include(i => i.Players)
                            .Include(i => i.JobTitles)
                            .Include(i => i.JobTitles.Select(e => e.Language))
                            .Include(i => i.Players.Select(e => e.Holding))
                            .Include(i => i.ProducersEvents)
                            .Include(i => i.ProducersEvents.Select(e => e.Event))
                            .Include(i => i.ProducersEvents.Select(e => e.Producer))
                            .AsNoTracking()
                            .Where(e => e.UserId != userId);
        }

        public Collaborator GetBySchedule(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                                .Include(i => i.ProducersEvents)
                                .Include(i => i.ProducersEvents.Select(e => e.Event))
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


        public override IQueryable<Collaborator> GetAllSimple(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                            .Include(i => i.User)
                            .Include(i => i.Players)
                            //.Include(i => i.Countries)
                            //.Include(i => i.Country)
                            .Include(i => i.Players.Select(e => e.Holding));
        }

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