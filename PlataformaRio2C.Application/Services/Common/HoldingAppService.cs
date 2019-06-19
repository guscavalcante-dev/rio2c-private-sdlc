using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    public class HoldingAppService : AppService<PlataformaRio2CContext, Domain.Entities.Holding, HoldingAppViewModel, HoldingAppViewModel, HoldingAppViewModel, HoldingItemListAppViewModel>, IHoldingAppService
    {
        private ILanguageRepository _languageRepository;

        public HoldingAppService(IHoldingService service, IUnitOfWork unitOfWork, ILanguageRepository languageRepository)
            : base(unitOfWork, service)
        {
            _languageRepository = languageRepository;
        }

        public override HoldingAppViewModel GetEditViewModel()
        {
            var viewModel = new HoldingAppViewModel();

            LoadViewModelOptions(viewModel);

            return viewModel;
        }

        public override AppValidationResult Create(HoldingAppViewModel viewModel)
        {
            LoadViewModelOptions(viewModel);

            return base.Create(viewModel);
        }


        public override AppValidationResult Update(HoldingAppViewModel viewModel)
        {
            LoadViewModelOptions(viewModel);

            return base.Update(viewModel);
        }

        public override HoldingAppViewModel Get(Guid uid)
        {
            var viewModel = base.Get(uid);

            LoadViewModelOptions(viewModel);

            return viewModel;
        }

        private void LoadViewModelOptions(HoldingAppViewModel viewModel)
        {
            viewModel.Languages = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());
        }
    }
}
