using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IPlayerService : IService<Player>
    {
        Player GetWithInterests(Guid uid);
    }
}
