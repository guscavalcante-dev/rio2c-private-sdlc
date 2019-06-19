using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface ICountryService : IService<Country>
    {
        IEnumerable<Country> GetCountries();
    }
}
