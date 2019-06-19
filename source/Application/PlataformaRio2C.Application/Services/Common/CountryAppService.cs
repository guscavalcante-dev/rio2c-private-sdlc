using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Application.ViewModels.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.Services
{
    public class CountryAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Country, CountryBasicAppViewModel, CountryBasicAppViewModel, CountryBasicAppViewModel, CountryBasicAppViewModel>, ICountryAppService
    {
        public CountryAppService(ICountryService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
            : base(unitOfWork, service)
        {
            //_roleRepository = repositoryFactory.RoleRepository;
            //_languageRepository = repositoryFactory.LanguageRepository;
            //_collaboratorJobTitleRepository = repositoryFactory.CollaboratorJobTitleRepository;
            //_collaboratorMiniBioRepository = repositoryFactory.CollaboratorMiniBioRepository;
            //_playerRepository = repositoryFactory.PlayerRepository;
            //_eventRepository = repositoryFactory.EventRepository;
            //_systemParameterRepository = systemParameterRepository;
            //_identityController = identityController;
            //_countryRepository = repositoryFactory.CountryRepository;

        }

        //public CollaboratorBasicAppViewModel getAll()
        //{

        //}
    }
}