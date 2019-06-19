using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class CountryRepository: Repository<PlataformaRio2CContext, PlataformaRio2C.Domain.Entities.Country>, ICountryRepository
    {
        public CountryRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }
    }
}
