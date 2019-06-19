using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Services
{
    public class PlayerInterestService : Service<PlayerInterest>, IPlayerInterestService
    {
        public PlayerInterestService(IPlayerInterestRepository repository)
            :base(repository)
        {

        }
    }
}
