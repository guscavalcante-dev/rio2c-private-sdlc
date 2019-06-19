using PlataformaRio2C.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IConferenceRepository : IRepository<Conference>
    {
        IQueryable<Conference> GetAllBySchedule();
        IQueryable<Conference> GetAllBySchedule(Expression<Func<Conference, bool>> filter);
    }    
}
