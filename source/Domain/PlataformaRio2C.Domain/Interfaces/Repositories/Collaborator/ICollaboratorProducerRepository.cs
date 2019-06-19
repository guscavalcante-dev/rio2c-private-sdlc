using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface ICollaboratorProducerRepository : IRepository<CollaboratorProducer>
    {
        IQueryable<Collaborator> GetAllCollaborators(bool @readonly = false);
    }    
}
