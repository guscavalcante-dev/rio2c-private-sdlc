using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    public class CollaboratorPlayerAppService : AppService<PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext, Collaborator, CollaboratorBasicAppViewModel, CollaboratorDetailAppViewModel, CollaboratorPlayerEditAppViewModel, CollaboratorPlayerItemListAppViewModel>, ICollaboratorPlayerAppService
    {
        #region props       
        private readonly IRoleRepository _roleRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;

        #endregion

        #region ctor

        public CollaboratorPlayerAppService(ICollaboratorPlayerService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, ICollaboratorAppService collaboratorAppService)
            : base(unitOfWork, service)
        {
            _languageRepository = repositoryFactory.LanguageRepository;
            _playerRepository = repositoryFactory.PlayerRepository;
            _collaboratorAppService = collaboratorAppService;
            _roleRepository = repositoryFactory.RoleRepository;
            _countryRepository = repositoryFactory.CountryRepository;
            _stateRepository = repositoryFactory.StateRepository;
            _cityRepository = repositoryFactory.CityRepository;
        }

        #endregion

        #region public methods        

        public override CollaboratorPlayerEditAppViewModel GetEditViewModel()
        {
            var viewModel = base.GetEditViewModel();
            viewModel.Countries = _countryRepository.GetAll();


            LoadViewModelOptions(viewModel);

            return viewModel;
        }

        public override AppValidationResult Create(CollaboratorPlayerEditAppViewModel viewModel)
        {
            BeginTransaction();
            int countryId = (viewModel.Address.Country == null ? 0 : (int)viewModel.Address.Country);
            int cityId = (viewModel.Address.CityId == null ? 0 : (int)viewModel.Address.CityId);
            int stateId = (viewModel.Address.StateId == null ? 0 : (int)viewModel.Address.StateId);

            var entity = viewModel.MapReverse();
            MapEntity(ref entity, viewModel);
            _collaboratorAppService.MapEntity(ref entity, viewModel.JobTitles, viewModel.MiniBios);
            //entity.Address.SetCity(cityId);
            //entity.Address.SetCountry(countryId);
            //entity.Address.SetState(stateId);

            ValidationResult.Add(service.Create(entity));

            if (ValidationResult.IsValid)
                Commit();

            LoadViewModelOptions(viewModel);

            return ValidationResult;
        }

        public override CollaboratorPlayerEditAppViewModel GetByEdit(Guid uid)
        {
            var viewModel = base.GetByEdit(uid);

            viewModel.Countries = _countryRepository.GetAll();
            viewModel.CountryId = (viewModel.Address.CountryId == null ? 0 : (int)viewModel.Address.CountryId);
            viewModel.StateId = (viewModel.Address.StateId == null ? 0 : (int)viewModel.Address.StateId);
            viewModel.CityId = (viewModel.Address.CityId == null ? 0 : (int)viewModel.Address.CityId);

            if (viewModel.Address.CountryId != null)
            {
                viewModel.States = _stateRepository.GetAll(a => a.CountryId == viewModel.Address.CountryId).ToList();
            }

            if (viewModel.Address.StateId != null)
            {
                viewModel.Cities = _cityRepository.GetAll(a => a.StateId == viewModel.Address.StateId).ToList();
            }

            LoadViewModelOptions(viewModel);

            return viewModel;
        }

        public override AppValidationResult Update(CollaboratorPlayerEditAppViewModel viewModel)
        {
            BeginTransaction();
            int countryId = (viewModel.Address.Country == null ? 0 : (int)viewModel.Address.Country);
            int cityId = (viewModel.Address.CityId == null ? 0 : (int)viewModel.Address.CityId);
            int stateId = (viewModel.Address.StateId == null ? 0 : (int)viewModel.Address.StateId);

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                //entity.Address.SetCity(cityId);
                //entity.Address.SetCountry(countryId);
                //entity.Address.SetState(stateId);

                var entityAlter = viewModel.MapReverse(entity);

                MapEntity(ref entity, viewModel);
                _collaboratorAppService.MapEntity(ref entityAlter, viewModel.JobTitles, viewModel.MiniBios);

                //entityAlter.Address.SetCountry(countryId);

                ValidationResult.Add(service.Update(entityAlter));
            }

            if (ValidationResult.IsValid)
                Commit();

            LoadViewModelOptions(viewModel);

            return ValidationResult;
        }

        public AppValidationResult UpdateByPortal(CollaboratorPlayerEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var email = entity.User.Email;
                var entityAlter = viewModel.MapReverse(entity);

                _collaboratorAppService.MapEntity(ref entityAlter, viewModel.JobTitles, viewModel.MiniBios);
                entityAlter.User.Email = email;
                entityAlter.User.UserName = email;

                viewModel.User.Email = email;
                viewModel.User.UserName = email;

                var collaboratorService = service as ICollaboratorPlayerService;

                ValidationResult.Add(collaboratorService.UpdateByPortal(entityAlter));

                if (entityAlter != null)
                {
                    viewModel.Players = PlayerCollaboratorAppViewModel.MapList(entityAlter.Players.ToList());
                }

                if (ValidationResult.IsValid)
                {
                    Commit();
                }
                else
                {
                    //entityAlter.Address.SetZipCode(null);
                }
            }

            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());

            return ValidationResult;
        }

        public void MapEntity(ref Collaborator entity, CollaboratorPlayerEditAppViewModel viewModel)
        {
            MapEntityUserRole(ref entity);

            if (entity.Players != null && entity.Players.Any())
            {
                entity.Players.Clear();
            }

            if (viewModel.Players != null && viewModel.Players.Any(e => e.Uid != Guid.Empty))
            {
                var playersFound = new List<Player>();
                foreach (var playerRelationship in viewModel.Players)
                {
                    var player = _playerRepository.Get(playerRelationship.Uid);
                    if (player != null)
                    {
                        playersFound.Add(player);
                    }
                }

                entity.SetPlayers(playersFound);
            }
            else
            {
                entity.SetPlayers(null);
            }
        }

        public ImageFileAppViewModel GetImage(Guid uid)
        {
            var s = service as ICollaboratorService;
            var entity = s.GetImage(uid);

            if (entity != null && entity.ImageId > 0 && entity.Players != null && entity.Players.Any())
            {
                return new ImageFileAppViewModel(entity.Image);
            }

            return null;
        }

        public ImageFileAppViewModel GetThumbImage(Guid uid)
        {
            var s = service as ICollaboratorService;
            var entity = s.GetImage(uid);

            if (entity != null && entity.ImageId > 0 && entity.Players != null && entity.Players.Any())
            {
                return ImageFileAppViewModel.GetThumbImage(entity.Image);
            }

            return null;
        }

        #endregion

        #region private methods       

        private void LoadViewModelOptions(CollaboratorPlayerEditAppViewModel viewModel)
        {
            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll()).ToList();

            var players = _playerRepository.GetAllWithHoldingSimple().OrderBy(e => e.Holding.Name).ThenBy(t => t.Name).ToList();

            viewModel.PlayersOptions = new List<PlayerOptionAppViewModel>() { new PlayerOptionAppViewModel() { Name = "Selecione" } };
            viewModel.PlayersOptions = viewModel.PlayersOptions.Concat(PlayerOptionAppViewModel.MapList(players).ToList());

            viewModel.PlayersOptions = viewModel.PlayersOptions.Select(po =>
            {
                if (!string.IsNullOrWhiteSpace(po.HoldingName))
                {
                    po.Name = string.Format("{0} / {1}", po.HoldingName, po.Name);
                }
                return po;
            });
        }

        private void MapEntityUserRole(ref Collaborator entity)
        {
            if (entity.User != null && (entity.User.Roles == null || !entity.User.Roles.Any()))
            {
                var rolePlayer = _roleRepository.Get(e => e.Name == "Player");
                entity.User.Roles = new List<Role>() { rolePlayer };
            }
            else if (entity.User.Roles != null && !entity.User.Roles.Any(e => e.Name == "Player"))
            {
                var rolePlayer = _roleRepository.Get(e => e.Name == "Player");
                entity.User.Roles.Add(rolePlayer);
            }
        }

        #endregion
    }
}
