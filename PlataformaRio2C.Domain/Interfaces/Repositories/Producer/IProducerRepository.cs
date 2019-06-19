using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IProducerRepository : IRepository<Producer>
    {
        Producer GetByCnpj(string cnpj);
        Producer GetByName(string name);
        Producer GetImage(Guid uid);


        System.Linq.IQueryable<Collaborator> GetAllCollaborators(Expression<Func<Producer, bool>> filter);

        IQueryable<Producer> GetAllWithAddress();

    }    
}
