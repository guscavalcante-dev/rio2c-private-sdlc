using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface INegotiationRepository : IRepository<Negotiation>
    {
        IEnumerable<Player> GetAllPlayers();
        IEnumerable<Producer> GetAllProducers();
        IQueryable<Negotiation> GetAllBySchedule(Expression<Func<Negotiation, bool>> filter);        
        void Truncate();
    }    
}
