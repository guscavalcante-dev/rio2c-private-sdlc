using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Services;
using PlataformaRio2C.Infra.Data.Repository.Repositories;
using SimpleInjector;

namespace PlataformaRio2C.Infra.CrossCutting.IOC
{
    public static class BootStrapperAdmin
    {
        public static void RegisterServices(Container container)
        {
            container.Register<IPlayerService, PlayerAdminService>(Lifestyle.Scoped);
            container.Register<Application.Interfaces.Services.INegotiationConfigService, Application.Services.NegotiationConfigService>(Lifestyle.Scoped);
            container.Register<Domain.Interfaces.INegotiationConfigService, Domain.Services.NegotiationConfigService>(Lifestyle.Scoped);
            container.Register<INegotiationConfigRepository, NegotiationConfigRepository>(Lifestyle.Scoped);


            container.Register<IMailAppService, MailAppService>(Lifestyle.Scoped);
            container.Register<IMailService, MailService>(Lifestyle.Scoped);
            container.Register<IMailRepository, MailRepository>(Lifestyle.Scoped);
            container.Register<IMailCollaboratorRepository, MailCollaboratorRepository>(Lifestyle.Scoped);
        }       
    }    
}
