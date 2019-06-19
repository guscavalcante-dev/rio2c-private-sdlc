using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System;
using System.Linq.Expressions;
using LinqKit;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ProducerRepository : Repository<PlataformaRio2CContext, Producer>, IProducerRepository
    {
        public ProducerRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override IQueryable<Producer> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Image)                                
                                .Include(i => i.Address)
                                .Include(i => i.Descriptions)
                                .Include(i => i.Descriptions.Select( e=> e.Language))
                                .Include(i => i.ProducerActivitys)
                                .Include(i => i.ProducerTargetAudience)
                                .Include(i => i.EventsCollaborators)
                                .Include(i => i.Projects);

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public override IQueryable<Producer> GetAll(Expression<Func<Producer, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public override Producer Get(Expression<Func<Producer, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public override Producer Get(object id)
        {
            return this.dbSet
                  .Include(i => i.Image)
                                .Include(i => i.Address)
                                .Include(i => i.Descriptions)
                                .Include(i => i.ProducerActivitys)
                                .Include(i => i.ProducerTargetAudience)
                                .Include(i => i.EventsCollaborators)
                                .Include(i => i.Projects)
                .FirstOrDefault(x => x.Id == (int)id);

        }

        public override Producer Get(Guid uid)
        {
            return this.dbSet
                  .Include(i => i.Image)
                                .Include(i => i.Address)
                                .Include(i => i.Descriptions)
                                .Include(i => i.Descriptions.Select(e => e.Language))
                                .Include(i => i.ProducerActivitys)
                                .Include(i => i.ProducerActivitys.Select(e => e.Activity))
                                .Include(i => i.ProducerTargetAudience)
                                .Include(i => i.ProducerTargetAudience.Select(e => e.TargetAudience))
                                .Include(i => i.EventsCollaborators)
                                .Include(i => i.EventsCollaborators.Select(e => e.Collaborator))
                                .Include(i => i.EventsCollaborators.Select(e => e.Collaborator.JobTitles))
                                .Include(i => i.EventsCollaborators.Select(e => e.Collaborator.JobTitles.Select(j => j.Language)))
                .FirstOrDefault(x => x.Uid == uid);

        }
       

        public override void Delete(Producer entity)
        {
            if (entity.Image != null)
            {
                _context.Entry(entity.Image).State = EntityState.Deleted;
            }
            
            if (entity.Descriptions != null && entity.Descriptions.Any())
            {
                foreach (var item in entity.Descriptions.ToList())
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }               

                entity.Descriptions.Clear();
            }          

            base.Delete(entity);
        }

        public Producer GetByCnpj(string cnpj)
        {
            var lintCnpj = Producer.GetLintCnpj(cnpj);         
            return dbSet.ToList().FirstOrDefault(e => e.LintCnpj == lintCnpj);
        }

        public Producer GetByName(string name)
        {            
            return dbSet.FirstOrDefault(e => e.Name == name);
        }

        public Producer GetImage(Guid uid)
        {
            return this.dbSet
                            .Include(i => i.Image)
                            .FirstOrDefault(e => e.Uid == uid);
        }

        public IQueryable<Collaborator> GetAllCollaborators(Expression<Func<Producer, bool>> filter)
        {
            return this.dbSet

                             .Include(i => i.EventsCollaborators)
                             .Include(i => i.EventsCollaborators.Select(c => c.Collaborator))
                             .Where(filter)
                             .SelectMany(e => e.EventsCollaborators.Select(ec => ec.Collaborator))
                             .AsNoTracking();            
        }

        public IQueryable<Producer> GetAllWithAddress()
        {
            return this.dbSet
                .Include(i => i.Address)
                .AsNoTracking();
        }
    }
}
