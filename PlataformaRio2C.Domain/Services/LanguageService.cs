using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Services
{
    public class LanguageService : Service<Language>, ILanguageService
    {
        public LanguageService(ILanguageRepository repository)
            :base(repository)
        {

        }
    }
}
