using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    public class InterestAppService : AppService<PlataformaRio2CContext, Domain.Entities.Interest, InterestAppViewModel, InterestAppViewModel, InterestAppViewModel, InterestAppViewModel>, IInterestAppService
    {
        private readonly IInterestGroupRepository _interestGroupRepository;

        public InterestAppService(IInterestService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
            : base(unitOfWork, service)
        {
            _interestGroupRepository = repositoryFactory.InterestGroupRepository;
        }

        public override InterestAppViewModel GetEditViewModel()
        {
            var viewModel = new InterestAppViewModel();

            LoadViewModelOptions(viewModel);

            return viewModel;
        }

        public override AppValidationResult Update(InterestAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);
                entity.SetInterestGroup(_interestGroupRepository.Get(viewModel.GroupUid));
                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
                Commit();
         
            return ValidationResult;
        }


        public override InterestAppViewModel Get(Guid uid)
        {
            var viewModel = base.Get(uid);

            LoadViewModelOptions(viewModel);

            return viewModel;
        }


        public override AppValidationResult Create(InterestAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = viewModel.MapReverse();
            entity.SetInterestGroup(_interestGroupRepository.Get(viewModel.GroupUid));
            ValidationResult.Add(service.Create(entity));

            if (ValidationResult.IsValid)
                Commit();

            LoadViewModelOptions(viewModel);

            return ValidationResult;
        }

        private void LoadViewModelOptions(InterestAppViewModel viewModel)
        {
            viewModel.GroupOptions = InterestGroupAppViewModel.MapList(_interestGroupRepository.GetAll().OrderBy(e => e.Name).ToList());
        }
    }
}
