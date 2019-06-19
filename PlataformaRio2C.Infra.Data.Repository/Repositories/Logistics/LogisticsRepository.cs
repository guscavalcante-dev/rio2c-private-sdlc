using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class LogisticsRepository : Repository<PlataformaRio2CContext, Logistics>, ILogisticsRepository
    {
        public LogisticsRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override IQueryable<Logistics> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Collaborator)
                                .Include(i => i.Collaborator.Players)
                                .Include(i => i.Collaborator.Players.Select(e => e.Holding))
                                .Include(i => i.Collaborator.ProducersEvents)
                                .Include(i => i.Collaborator.ProducersEvents.Select(e => e.Producer));

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }
    }
}
