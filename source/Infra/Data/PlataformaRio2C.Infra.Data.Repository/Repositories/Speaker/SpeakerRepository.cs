using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository
{
    public class SpeakerRepository : Repository<PlataformaRio2CContext, Speaker>, ISpeakerRepository
    {
        private PlataformaRio2CContext _context;

        public SpeakerRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        public override void Create(Speaker entity)
        {
            base.Create(entity);
        }
    }
}
