using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class CollaboratorProducerRepository : Repository<PlataformaRio2CContext, CollaboratorProducer>, ICollaboratorProducerRepository
    {
        public CollaboratorProducerRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override IQueryable<CollaboratorProducer> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                 .Include(i => i.Event)
                                .Include(i => i.Producer)
                                .Include(i => i.Collaborator)
                                .Include(i => i.Collaborator.ProducersEvents)
                                .Include(i => i.Collaborator.ProducersEvents.Select(p => p.Event))
                                .Include(i => i.Collaborator.ProducersEvents.Select(p => p.Producer))
                                .Include(i => i.Collaborator.User)
                                .Include(i => i.Collaborator.User.UserUseTerms);


            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public IQueryable<Collaborator> GetAllCollaborators(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Event)
                                .Include(i => i.Producer)
                                .Include(i => i.Collaborator)
                                .Include(i => i.Collaborator.ProducersEvents)
                                .Include(i => i.Collaborator.ProducersEvents.Select(p => p.Event))
                                .Include(i => i.Collaborator.ProducersEvents.Select(p => p.Producer))
                                .Include(i => i.Collaborator.User)
                                .Include(i => i.Collaborator.User.UserUseTerms)
                                .Select(e => e.Collaborator);


            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }


    }
}
