using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    public class RoleLecturerAppService : AppService<PlataformaRio2CContext, Domain.Entities.RoleLecturer, RoleLecturerAppViewModel, RoleLecturerAppViewModel, RoleLecturerEditAppViewModel, RoleLecturerAppViewModel>, IRoleLecturerAppService
    {
        private ILanguageRepository _languageRepository;
        private IRoleLecturerTitleRepository _roleLecturerTitleRepository;

        public RoleLecturerAppService(IRoleLecturerService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
            : base(unitOfWork, service)
        {
            _languageRepository = repositoryFactory.LanguageRepository;
            _roleLecturerTitleRepository = repositoryFactory.RoleLecturerTitleRepository;
        }

        public override RoleLecturerEditAppViewModel GetEditViewModel()
        {
            var viewModel = new RoleLecturerEditAppViewModel();

            LoadViewModelOptions(viewModel);

            return viewModel;
        }

        public override AppValidationResult Create(RoleLecturerEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = viewModel.MapReverse();
            MapEntityTitles(ref entity, viewModel.Titles);
            ValidationResult.Add(service.Create(entity));

            if (ValidationResult.IsValid)
                Commit();

            LoadViewModelOptions(viewModel);

            return ValidationResult;
        }

        public override AppValidationResult Update(RoleLecturerEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);
                MapEntityTitles(ref entityAlter, viewModel.Titles);

                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
                Commit();

            if (!ValidationResult.IsValid)
            {
                LoadViewModelOptions(viewModel);
            }

            return ValidationResult;
        }

        

        public override RoleLecturerEditAppViewModel GetByEdit(Guid uid)
        {
            var viewModel = base.GetByEdit(uid);

            LoadViewModelOptions(viewModel);

            return viewModel;
        }


        private void LoadViewModelOptions(RoleLecturerEditAppViewModel viewModel)
        {
            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());
        }

        private void MapEntityTitles(ref RoleLecturer entity, IEnumerable<RoleLecturerTitleAppViewModel> titlesViewModel)
        {
            if (entity.Titles != null && entity.Titles.Any())
            {
                _roleLecturerTitleRepository.DeleteAll(entity.Titles);
            }

            if (titlesViewModel != null && titlesViewModel.Any())
            {
                var entitiesTitles = new List<RoleLecturerTitle>();
                foreach (var titleViewModel in titlesViewModel)
                {
                    var entityTitle = titleViewModel.MapReverse();
                    var language = _languageRepository.Get(e => e.Code == titleViewModel.LanguageCode);
                    entityTitle.SetLanguage(language);
                    entitiesTitles.Add(entityTitle);
                }
                entity.SetTitles(entitiesTitles);
            }
        }

    }
}
