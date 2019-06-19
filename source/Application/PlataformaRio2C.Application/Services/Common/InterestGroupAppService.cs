using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.Services
{
    public class InterestGroupAppService : AppService<PlataformaRio2CContext, Domain.Entities.InterestGroup, InterestGroupAppViewModel, InterestGroupAppViewModel, InterestGroupAppViewModel, InterestGroupAppViewModel>, IInterestGroupAppService
    {
        public InterestGroupAppService(IInterestGroupService service, IUnitOfWork unitOfWork)
            : base(unitOfWork, service)
        {
        }

        public override InterestGroupAppViewModel GetEditViewModel()
        {
            var viewModel = new InterestGroupAppViewModel();

            return viewModel;
        }
    }
}
