﻿using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class InterestGroupRepository : Repository<PlataformaRio2CContext, InterestGroup>, IInterestGroupRepository
    {
        public InterestGroupRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}
