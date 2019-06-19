using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class NegotiationRoomConfigRepository : Repository<PlataformaRio2CContext, NegotiationRoomConfig>, INegotiationRoomConfigRepository
    {
        public NegotiationRoomConfigRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }       
    }
}
