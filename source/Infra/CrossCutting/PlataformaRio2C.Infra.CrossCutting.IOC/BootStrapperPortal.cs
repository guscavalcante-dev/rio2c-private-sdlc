using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Services;
using PlataformaRio2C.Infra.Data.Repository.Repositories;
using SimpleInjector;

namespace PlataformaRio2C.Infra.CrossCutting.IOC
{
    public static class BootStrapperPortal
    {
        public static void RegisterServices(Container container)
        {
            container.Register<IPlayerService, PlayerService>(Lifestyle.Scoped);
            //container.Register<ICountryService, CountryService>(Lifestyle.Scoped);

        }
    }
}
