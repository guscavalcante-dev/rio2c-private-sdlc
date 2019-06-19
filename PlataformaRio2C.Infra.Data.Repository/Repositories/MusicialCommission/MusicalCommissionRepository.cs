using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository
{
    public class MusicalCommissionRepository : Repository<PlataformaRio2CContext, MusicalCommission>, IMusicalCommissionRepository
    {
        private PlataformaRio2CContext _context;

        public MusicalCommissionRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
