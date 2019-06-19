using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.Services
{
    public class LanguageAppService : AppService<PlataformaRio2CContext, Domain.Entities.Language, LanguageAppViewModel, LanguageAppViewModel, LanguageAppViewModel, LanguageAppViewModel>, ILanguageAppService
    {
        public LanguageAppService(ILanguageService service, IUnitOfWork unitOfWork)
            : base(unitOfWork, service)
        {
        }
    }
}
