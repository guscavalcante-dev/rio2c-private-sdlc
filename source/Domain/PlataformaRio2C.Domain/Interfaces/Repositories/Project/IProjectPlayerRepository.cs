using PlataformaRio2C.Domain.Entities;
using System.Linq;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IProjectPlayerRepository : IRepository<ProjectPlayer>
    {
        IQueryable<ProjectPlayer> GetAllForProccessSchedule();
        int CountRejected();
        int CountAccepted();
        int CountOnEvaluation();
        int CountUnsent();
    }    
}
