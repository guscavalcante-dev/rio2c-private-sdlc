//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class CountryService : Service<Country>, ICountryService
//    {
//        public CountryService(ICountryRepository repository, IRepositoryFactory repositoryFactory)
//            : base(repository)
//        {
//        }

//        public IEnumerable<Country> GetCountries()
//        {
//            return _repository.GetAllSimple();
//        }
//    }
//}
