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
    public class RoomAppService : AppService<PlataformaRio2CContext, Domain.Entities.Room, RoomAppViewModel, RoomAppViewModel, RoomEditAppViewModel, RoomAppViewModel>, IRoomAppService
    {
        private readonly IRoomNameRepository _roomNameRepository;
        private readonly ILanguageRepository _languageRepository;

        public RoomAppService(IRoomService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
            : base(unitOfWork, service)
        {
            _languageRepository = repositoryFactory.LanguageRepository;
            _roomNameRepository = repositoryFactory.RoomNameRepository;
        }

        public override RoomEditAppViewModel GetEditViewModel()
        {
            var viewModel = new RoomEditAppViewModel();

            LoadViewModelOptions(viewModel);

            return viewModel;
        }

        public override AppValidationResult Create(RoomEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = viewModel.MapReverse();
            MapEntityTitles(ref entity, viewModel.Names);
            ValidationResult.Add(service.Create(entity));

            if (ValidationResult.IsValid)
                Commit();

            LoadViewModelOptions(viewModel);

            return ValidationResult;
        }

        public override AppValidationResult Update(RoomEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);
                MapEntityTitles(ref entityAlter, viewModel.Names);

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



        public override RoomEditAppViewModel GetByEdit(Guid uid)
        {
            var viewModel = base.GetByEdit(uid);

            LoadViewModelOptions(viewModel);

            return viewModel;
        }


        private void LoadViewModelOptions(RoomEditAppViewModel viewModel)
        {
            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());
        }

        private void MapEntityTitles(ref Room entity, IEnumerable<RoomNameAppViewModel> namesViewModel)
        {
            if (entity.Names != null && entity.Names.Any())
            {
                _roomNameRepository.DeleteAll(entity.Names);
            }

            if (namesViewModel != null && namesViewModel.Any())
            {
                var entitiesTitles = new List<RoomName>();
                foreach (var titleViewModel in namesViewModel)
                {
                    var entityTitle = titleViewModel.MapReverse();
                    var language = _languageRepository.Get(e => e.Code == titleViewModel.LanguageCode);
                    entityTitle.SetLanguage(language);
                    entitiesTitles.Add(entityTitle);
                }
                entity.SetNames(entitiesTitles);
            }
        }
    }
}
