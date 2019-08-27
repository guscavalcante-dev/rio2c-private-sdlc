// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="PlayerAppService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LinqKit;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Application.Services
{
    /// <summary>PlayerAppService</summary>
    public class PlayerAppService : AppService<PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext, Player, PlayerBasicAppViewModel, PlayerDetailAppViewModel, PlayerEditAppViewModel, PlayerItemListAppViewModel>, IPlayerAppService
    {
        #region props

        private readonly ICollaboratorService _collaboratorService;
        private readonly ILanguageRepository _languageRepository;
        private readonly IInterestRepository _interestRepository;
        private readonly IHoldingRepository _holdingRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly ITargetAudienceRepository _targetAudienceRepository;
        private readonly IPlayerActivityRepository _playerActivityRepository;
        private readonly IPlayerTargetAudienceRepository _playerTargetAudienceRepository;
        private readonly IEditionRepository _eventRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IPlayerInterestService _playerInterestService;
        private readonly IPlayerRestrictionsSpecificsService _playerRestrictionsSpecificsService;
        private readonly IPlayerRestrictionsSpecificsRepository _playerRestrictionsSpecificsRepository;

        #endregion

        #region ctor     

        public PlayerAppService(IPlayerService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, ICollaboratorService collaboratorService, IPlayerInterestService playerInterestService, IPlayerRestrictionsSpecificsService playerRestrictionsSpecificsService)
            : base(unitOfWork, service)
        {
            _languageRepository = repositoryFactory.LanguageRepository;
            _holdingRepository = repositoryFactory.HoldingRepository;
            _activityRepository = repositoryFactory.ActivityRepository;
            _targetAudienceRepository = repositoryFactory.TargetAudienceRepository;
            _playerActivityRepository = repositoryFactory.PlayerActivityRepository;
            _playerTargetAudienceRepository = repositoryFactory.PlayerTargetAudienceRepository;
            _interestRepository = repositoryFactory.InterestRepository;
            _eventRepository = repositoryFactory.EditionRepository;
            _playerRepository = repositoryFactory.PlayerRepository;
            _playerRestrictionsSpecificsRepository = repositoryFactory.PlayerRestrictionsSpecificsRepository;

            _collaboratorService = collaboratorService;
            _playerRestrictionsSpecificsService = playerRestrictionsSpecificsService;
            _playerInterestService = playerInterestService;

            _countryRepository = repositoryFactory.CountryRepository;
            _stateRepository = repositoryFactory.StateRepository;
            _cityRepository = repositoryFactory.CityRepository;

        }

        #endregion

        #region Public methods       

        public override PlayerEditAppViewModel GetEditViewModel()
        {
            var viewModel = new PlayerEditAppViewModel();
            viewModel.Countries = _countryRepository.GetAll();

            var activities = _activityRepository.GetAll().ToList();
            var targetAudienceOptions = _targetAudienceRepository.GetAll().ToList();

            viewModel.TargetAudience = PlayerTargetAudienceAppViewModel.MapList(targetAudienceOptions);
            viewModel.Activitys = PlayerActivityAppViewModel.MapList(activities);

            LoadViewModelOptions(viewModel);

            return viewModel;
        }

        public override PlayerEditAppViewModel GetByEdit(Guid uid)
        {
            PlayerEditAppViewModel vm = null;

            var entity = service.Get(uid);

            if (entity != null)
            {
                var countries = _countryRepository.GetAll().ToList();
                var addressEntity = entity.Address;

                List<State> states = null;
                //if (addressEntity.CountryId != null)
                //{
                //    states = _stateRepository.GetAll(a => a.CountryId == addressEntity.CountryId).ToList();
                //}

                List<City> cities = null;
                //if (addressEntity.StateId != null)
                //{
                //    cities = _cityRepository.GetAll(a => a.StateId == addressEntity.StateId).ToList();
                //}

                //addressEntity.Countries = countries;
                //addressEntity.States = states;
                //addressEntity.Cities = cities;
                vm = new PlayerEditAppViewModel(entity);

                var activities = _activityRepository.GetAll().ToList();
                var targetAudienceOptions = _targetAudienceRepository.GetAll().ToList();

                vm.Activitys = PlayerActivityAppViewModel.MapList(activities, entity).ToList();
                vm.TargetAudience = PlayerTargetAudienceAppViewModel.MapList(targetAudienceOptions, entity).ToList();
                vm.Address = new AddressAppViewModel(addressEntity);
                vm.Countries = countries;

                LoadViewModelOptions(vm);
            }

            return vm;
        }

        public override AppValidationResult Create(PlayerEditAppViewModel viewModel)
        {
            LoadViewModelOptions(viewModel);

            BeginTransaction();
            int countryId = (viewModel.Address.Country == null ? 0 : (int)viewModel.Address.Country);
            int cityId = (viewModel.Address.CityId == null ? 0 : (int)viewModel.Address.CityId);
            int stateId = (viewModel.Address.StateId == null ? 0 : (int)viewModel.Address.StateId);

            var entity = viewModel.MapReverse();
            var _activities = _activityRepository.GetAll();
            var entitysActivities = new List<PlayerActivity>();

            //entity.Address.SetCity(cityId);
            //entity.Address.SetCountry(countryId);
            //entity.Address.SetState(stateId);

            foreach (var activityViewModel in viewModel.Activitys)
            {
                if (activityViewModel.Selected)
                {
                    entitysActivities.Add(activityViewModel.MapReverse(entity, _activities.FirstOrDefault(e => e.Id == activityViewModel.ActivityId)));
                }
            }

            entity.SetPlayerActivitys(entitysActivities);

            var _targetAudiencieOptions = _targetAudienceRepository.GetAll();
            var entitiesTargetAudience = new List<PlayerTargetAudience>();
            foreach (var targetAudienceViewModel in viewModel.TargetAudience)
            {
                if (targetAudienceViewModel.Selected)
                {
                    entitiesTargetAudience.Add(targetAudienceViewModel.MapReverse(entity, _targetAudiencieOptions.FirstOrDefault(e => e.Id == targetAudienceViewModel.TargetAudienceId)));
                }
            }

            entity.SetPlayerTargetAudience(entitiesTargetAudience);

            ValidationResult.Add(service.Create(entity));

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;


        }

        public override AppValidationResult Update(PlayerEditAppViewModel viewModel)
        {
            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll());
            viewModel.HoldingsOptions = HoldingItemListAppViewModel.MapList(_holdingRepository.GetAllSimple());


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

                //entityAlter.Address.SetCountry(countryId);

                _playerActivityRepository.DeleteAll(entity.PlayerActivitys);
                _playerTargetAudienceRepository.DeleteAll(entity.PlayerTargetAudience);

                var _activities = _activityRepository.GetAll();
                var entitysActivities = new List<PlayerActivity>();

                foreach (var activityViewModel in viewModel.Activitys)
                {
                    if (activityViewModel.Selected)
                    {
                        entitysActivities.Add(activityViewModel.MapReverse(entity, _activities.FirstOrDefault(e => e.Id == activityViewModel.ActivityId)));
                    }
                }

                entityAlter.SetPlayerActivitys(entitysActivities);

                var _targetAudiencieOptions = _targetAudienceRepository.GetAll();
                var entitiesTargetAudience = new List<PlayerTargetAudience>();

                foreach (var targetAudienceViewModel in viewModel.TargetAudience)
                {
                    if (targetAudienceViewModel.Selected)
                    {
                        entitiesTargetAudience.Add(targetAudienceViewModel.MapReverse(entity, _targetAudiencieOptions.FirstOrDefault(e => e.Id == targetAudienceViewModel.TargetAudienceId)));
                    }
                }

                entityAlter.SetPlayerTargetAudience(entitiesTargetAudience);

                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;

        }

        public AppValidationResult UpdateByPortal(PlayerEditAppViewModel viewModel)
        {
            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());
            viewModel.HoldingsOptions = HoldingItemListAppViewModel.MapList(_holdingRepository.GetAllSimple().ToList());
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {

                //entity.Address.SetCity((int)viewModel.Address.CityId);
                //entity.Address.SetCountry((int)viewModel.Address.CountryId);
                //entity.Address.SetState((int)viewModel.Address.StateId);

                _playerActivityRepository.DeleteAll(entity.PlayerActivitys);
                _playerTargetAudienceRepository.DeleteAll(entity.PlayerTargetAudience);

                //entity.SetAddress(null);
                var entityAlter = viewModel.MapReverse(entity);
                //entityAlter.Address.SetCountry((int)viewModel.Address.CountryId);

                var _activities = _activityRepository.GetAll();
                var entitysActivities = new List<PlayerActivity>();

                var testActivity = viewModel.Activitys.Any(e => e.Selected);
                if (!testActivity)
                {
                    ValidationResult.Add(string.Format(Messages.TheFieldIsRequired, Labels.Activity), "Activitys");
                }

                var testTargetAudience = viewModel.TargetAudience.Any(e => e.Selected);
                if (!testTargetAudience)
                {
                    ValidationResult.Add(string.Format(Messages.TheFieldIsRequired, Labels.TargetAudience), "TargetAudience");
                }

                foreach (var activityViewModel in viewModel.Activitys)
                {
                    if (activityViewModel.Selected)
                    {
                        entitysActivities.Add(activityViewModel.MapReverse(entity, _activities.FirstOrDefault(e => e.Id == activityViewModel.ActivityId)));
                    }
                }

                entityAlter.SetPlayerActivitys(entitysActivities);

                var _targetAudiencieOptions = _targetAudienceRepository.GetAll();
                var entitiesTargetAudience = new List<PlayerTargetAudience>();
                foreach (var targetAudienceViewModel in viewModel.TargetAudience)
                {
                    if (targetAudienceViewModel.Selected)
                    {
                        entitiesTargetAudience.Add(targetAudienceViewModel.MapReverse(entity, _targetAudiencieOptions.FirstOrDefault(e => e.Id == targetAudienceViewModel.TargetAudienceId)));
                    }
                }

                entityAlter.SetPlayerTargetAudience(entitiesTargetAudience);

                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
            {
                Commit();
            }
            else
            {
                if (entity != null)
                {
                    //entity.Address.SetZipCode(null);
                }
            }


            return ValidationResult;
        }

        public IEnumerable<PlayerAppViewModel> GetAllByUserId(int id)
        {
            IList<PlayerAppViewModel> viewModel = new List<PlayerAppViewModel>();

            var entity = _collaboratorService.GetByUserId(id);

            //if (entity != null && entity.Players != null && entity.Players.Any())
            //{
            //    var playersId = entity.Players.Select(e => e.Id);

            //    var players = _playerRepository.GetAll(e => playersId.Contains(e.Id));

            //    if (players != null && players.Any())
            //    {
            //        var interest = _interestRepository.GetAll().ToList();
            //        var languages = _languageRepository.GetAll().ToList();
            //        var activities = _activityRepository.GetAll().ToList();
            //        var targetAudienceOptions = _targetAudienceRepository.GetAll().ToList();

            //        foreach (var player in players)
            //        {
            //            var playerViewModel = new PlayerAppViewModel(player);
            //            playerViewModel.LanguagesOptions = LanguageAppViewModel.MapList(languages).ToList();
            //            playerViewModel.Interests = PlayerInterestAppViewModel.MapList(interest, player).ToList();
            //            playerViewModel.Activitys = PlayerActivityAppViewModel.MapList(activities, player).ToList();
            //            playerViewModel.TargetAudience = PlayerTargetAudienceAppViewModel.MapList(targetAudienceOptions, player).ToList();
            //            viewModel.Add(playerViewModel);
            //        }
            //    }
            //}

            return viewModel;
        }

        public PlayerAppViewModel GetAllByUserId(int id, Guid playerUid)
        {
            var playersResult = GetAllByUserId(id);

            return playersResult.FirstOrDefault(e => e.Uid == playerUid);
        }

        public AppValidationResult SaveInterests(PlayerAppViewModel player)
        {
            player.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll()).ToList();

            var playerEntity = _playerRepository.Get(player.Uid);
            var eventEntity = _eventRepository.Get(e => e.Id == 1);
            var _interests = _interestRepository.GetAll();
            var entitys = new List<PlayerInterest>();

            var interestGrouped = player.Interests.GroupBy(g => g.InterestGroupName);
            foreach (var interestGroup in interestGrouped)
            {
                var test = interestGroup.Any(e => e.Selected);
                if (!test && !interestGroup.First().InterestGroupName.Contains("Buyer"))
                {
                    ValidationResult.Add(string.Format(Messages.SelectAtLeastOneGroupOption, interestGroup.First().InterestGroupName));
                }
            }

            foreach (var interest in player.Interests)
            {
                if (interest.Selected)
                {
                    entitys.Add(interest.MapReverse(playerEntity, _interests.FirstOrDefault(e => e.Id == interest.InterestId), eventEntity));
                }
            }

            if (playerEntity.Interests != null && playerEntity.Interests.Any())
            {
                _playerInterestService.DeleteAll(playerEntity.Interests);
            }

            if (entitys.Any())
            {
                foreach (var item in entitys)
                {
                    ValidationResult.Add(_playerInterestService.Create(item));
                }
            }

            var entitysAddPlayerRestrictionsSpecifics = new List<PlayerRestrictionsSpecifics>();
            foreach (var item in player.RestrictionsSpecifics)
            {
                entitysAddPlayerRestrictionsSpecifics.Add(item.MapReverse());
            }

            if (playerEntity.RestrictionsSpecifics != null && playerEntity.RestrictionsSpecifics.Any())
            {
                _playerRestrictionsSpecificsRepository.DeleteAll(playerEntity.RestrictionsSpecifics);
            }

            if (entitysAddPlayerRestrictionsSpecifics.Any())
            {
                foreach (var item in entitysAddPlayerRestrictionsSpecifics)
                {
                    item.SetPlayer(playerEntity);
                    item.SetLanguage(_languageRepository.Get(e => e.Code == item.LanguageCode));
                }

                playerEntity.SetRestrictionsSpecifics(entitysAddPlayerRestrictionsSpecifics);


                ValidationResult.Add(new PlayerIsInterestsIsConsistent().Valid(playerEntity));

                var resultCreateRestrictionsSpecificsService = _playerRestrictionsSpecificsService.CreateAll(entitysAddPlayerRestrictionsSpecifics);
                ValidationResult.Add(resultCreateRestrictionsSpecificsService);
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public IEnumerable<PlayerDetailAppViewModel> GetAllDetailByUserId(int id)
        {
            IList<PlayerDetailAppViewModel> viewModel = new List<PlayerDetailAppViewModel>();

            var entity = _collaboratorService.GetByUserId(id);

            //if (entity != null && entity.Players != null && entity.Players.Any())
            //{
            //    var playersId = entity.Players.Select(e => e.Id);

            //    var players = _playerRepository.GetAllNoImage(e => playersId.Contains(e.Id)).ToList();

            //    if (players != null && players.Any())
            //    {
            //        var interest = _interestRepository.GetAll().ToList();
            //        var eventRio2c = _eventRepository.Get(e => e.Id == 1);
            //        var activities = _activityRepository.GetAll().ToList();
            //        var targetAudienceOptions = _targetAudienceRepository.GetAll().ToList();

            //        foreach (var player in players)
            //        {
            //            var playerViewModel = new PlayerDetailAppViewModel(player);

            //            viewModel.Add(playerViewModel);
            //        }
            //    }
            //}

            return viewModel;
        }

        public PlayerDetailAppViewModel GetAllDetailByUserId(int id, Guid playerUid)
        {
            var playersResult = GetAllDetailByUserId(id);

            return playersResult.FirstOrDefault(e => e.Uid == playerUid);
        }

        public IEnumerable<PlayerEditAppViewModel> GetAllEditByUserId(int id)
        {
            IList<PlayerEditAppViewModel> viewModel = new List<PlayerEditAppViewModel>();

            var entity = _collaboratorService.GetWithPlayerAndProducerUserId(id);

            //if (entity != null && entity.Players != null && entity.Players.Any())
            //{
            //    var playersId = entity.Players.Select(e => e.Id);
            //    var players = _playerRepository.GetAll(e => playersId.Contains(e.Id)).ToList();

            //    if (entity != null && players != null && players.Any())
            //    {
            //        var interest = _interestRepository.GetAll().ToList();
            //        var eventRio2c = _eventRepository.Get(e => e.Id == 1);
            //        var languages = _languageRepository.GetAll().ToList();
            //        var activities = _activityRepository.GetAll().ToList();
            //        var targetAudienceOptions = _targetAudienceRepository.GetAll().ToList();

            //        var countries = _countryRepository.GetAll().ToList();

            //        foreach (var player in players)
            //        {
            //            var addressEntity = player.Address;

            //            List<State> states = null;
            //            //if (addressEntity.CountryId != null)
            //            //{
            //            //    states = _stateRepository.GetAll(a => a.CountryId == addressEntity.CountryId).ToList();
            //            //}

            //            List<City> cities = null;
            //            //if (addressEntity.StateId != null)
            //            //{
            //            //    cities = _cityRepository.GetAll(a => a.StateId == addressEntity.StateId).ToList();
            //            //}

            //            //addressEntity.Countries = countries;
            //            //addressEntity.States = states;
            //            //addressEntity.Cities = cities;

            //            var playerViewModel = new PlayerEditAppViewModel(player);
            //            playerViewModel.LanguagesOptions = LanguageAppViewModel.MapList(languages);
            //            playerViewModel.Activitys = PlayerActivityAppViewModel.MapList(activities, player).ToList();
            //            playerViewModel.TargetAudience = PlayerTargetAudienceAppViewModel.MapList(targetAudienceOptions, player).ToList();
            //            playerViewModel.Address = new AddressAppViewModel(addressEntity);
            //            playerViewModel.Countries = countries;

            //            viewModel.Add(playerViewModel);
            //        }
            //    }
            //}

            return viewModel;
        }

        public PlayerEditAppViewModel GetAllEditByUserId(int id, Guid playerUid)
        {
            var playersResult = GetAllEditByUserId(id).FirstOrDefault(e => e.Uid == playerUid); ;

            PlayerEditAppViewModel viewModel = null;

            if (playersResult != null)
            {
                //viewModel = new PlayerEditAppViewModel();
                playersResult.Countries = _countryRepository.GetAll();

                if (playersResult.Address.Country != null && playersResult.Address.Country != 0)
                {
                    //populate all states according to Country ID
                    playersResult.States = _stateRepository.GetAll(a => a.CountryId == 30).ToList();
                    playersResult.StateId = (playersResult.Address.StateId != null ? (int)playersResult.Address.StateId : 0);

                    //populate all cities according to State ID
                    playersResult.Cities = _cityRepository.GetAll(a => a.StateId == playersResult.StateId).ToList();
                    playersResult.CityId = (playersResult.Address.CityId != null ? (int)playersResult.Address.CityId : 0);

                }
            }

            return playersResult;
        }

        public IEnumerable<PlayerSelectOptionAppViewModel> GetAllOption(PlayerSelectOptionFilterAppViewModel filter, int userId)
        {
            var entity = _collaboratorService.GetByUserId(userId);
            IEnumerable<int> playersId = new List<int>();

            //if (entity != null)
            //{
            //    playersId = entity.Players.Select(e => e.Id);
            //}

            var entities = _playerRepository.GetAllNoImageWithInterest(GetPredicateForGetAllOption(filter, playersId)).OrderBy(e => e.Name).ToList();
            if (entities != null && entities.Any())
            {
                IEnumerable<PlayerSelectOptionAppViewModel> results = PlayerSelectOptionAppViewModel.MapList(entities).ToList();
                return results;
            }

            return new List<PlayerSelectOptionAppViewModel>() { };
        }

        public IEnumerable<GroupPlayerSelectOptionAppViewModel> GetAllOptionGroupByHolding(PlayerSelectOptionFilterAppViewModel filter, int userId)
        {
            var entity = _collaboratorService.GetByUserId(userId);

            IEnumerable<int> playersId = new List<int>();
            //if (entity != null)
            //{
            //    playersId = entity.Players.Select(e => e.Id);
            //}

            var entities = _playerRepository.GetAllNoImageWithInterest(GetPredicateForGetAllOption(filter, playersId)).OrderBy(e => e.Holding.Name).ToList();
            var groupEntities = entities.GroupBy(e => e.Holding.Name).ToList();
            if (groupEntities != null && groupEntities.Any())
            {
                return GroupPlayerSelectOptionAppViewModel.MapList(groupEntities).ToList();
            }

            return new List<GroupPlayerSelectOptionAppViewModel>() { };
        }

        public IEnumerable<PlayerSelectOptionAppViewModel> GetAllOptionByUser(PlayerSelectOptionFilterAppViewModel filter, int userId)
        {
            var entity = _collaboratorService.GetByUserId(userId);
            IEnumerable<int> playersId = new List<int>();
            //if (entity != null)
            //{
            //    playersId = entity.Players.Select(e => e.Id);
            //}

            var entities = _playerRepository.GetAllNoImageWithInterest(GetPredicateForGetAllOptionByUser(filter, playersId)).OrderBy(e => e.Name).ToList();
            if (entities != null && entities.Any())
            {
                IEnumerable<PlayerSelectOptionAppViewModel> results = PlayerSelectOptionAppViewModel.MapList(entities).ToList();
                return results;
            }

            return new List<PlayerSelectOptionAppViewModel>() { };
        }

        public IEnumerable<PlayerProducerAreaAppViewModel> GetAllWithGenres(PlayerSelectOptionFilterAppViewModel filter, int userId)
        {
            var entity = _collaboratorService.GetByUserId(userId);

            IEnumerable<int> playersId = new List<int>();

            //if (entity != null)
            //{
            //    playersId = entity.Players.Select(e => e.Id);
            //}

            var entities = _playerRepository.GetAllNoImageWithInterest(GetPredicateForGetAllOption(filter, playersId)).OrderBy(e => e.Name).ToList();
            if (entities != null && entities.Any())
            {
                IEnumerable<PlayerProducerAreaAppViewModel> results = PlayerProducerAreaAppViewModel.MapList(entities).ToList();
                return results;
            }

            return new List<PlayerProducerAreaAppViewModel>() { };
        }

        public IEnumerable<GroupPlayerAppViewModel> GetAllWithGenresGroupByHolding(PlayerSelectOptionFilterAppViewModel filter, int userId)
        {
            var entity = _collaboratorService.GetByUserId(userId);
            IEnumerable<int> playersId = new List<int>();

            //if (entity != null)
            //{
            //    playersId = entity.Players.Select(e => e.Id);
            //}

            var entities = _playerRepository.GetAllNoImageWithInterest(GetPredicateForGetAllOption(filter, playersId)).OrderBy(e => e.Holding.Name).ToList();
            var groupEntities = entities.GroupBy(e => e.Holding.Name).ToList();
            if (groupEntities != null && groupEntities.Any())
            {
                return GroupPlayerAppViewModel.MapList(groupEntities).ToList(); ;
            }

            return new List<GroupPlayerAppViewModel>() { };
        }

        public ImageFileAppViewModel GetThumbImage(Guid playerUid)
        {
            var entity = _playerRepository.GetImage(e => e.Uid == playerUid);

            if (entity != null && entity.ImageId > 0)
            {
                return PlayerSelectOptionAppViewModel.GetThumbImage(entity.Image);
            }

            return null;
        }

        public PlayerEditInterstsAppViewModel GetEditIntersts(Guid playerUid)
        {
            PlayerEditInterstsAppViewModel result = null;

            var playerEntity = service.Get(e => e.Uid == playerUid);

            if (playerEntity != null)
            {
                var interest = _interestRepository.GetAll().ToList();
                var languages = _languageRepository.GetAll().ToList();

                var playerViewModel = new PlayerEditInterstsAppViewModel(playerEntity);
                playerViewModel.LanguagesOptions = LanguageAppViewModel.MapList(languages);
                playerViewModel.Interests = PlayerInterestAppViewModel.MapList(interest, playerEntity).ToList();
                result = playerViewModel;
            }

            return result;
        }

        public AppValidationResult UpdateEditIntersts(PlayerEditInterstsAppViewModel playerinterestViewModel)
        {
            playerinterestViewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll()).ToList();

            var playerEntity = _playerRepository.Get(playerinterestViewModel.Uid);
            var eventEntity = _eventRepository.Get(e => e.Id == 1);
            var interests = _interestRepository.GetAll();
            var entitys = new List<PlayerInterest>();


            foreach (var interest in playerinterestViewModel.Interests)
            {
                if (interest.Selected)
                {
                    entitys.Add(interest.MapReverse(playerEntity, interests.FirstOrDefault(e => e.Id == interest.InterestId), eventEntity));
                }
            }

            if (playerEntity.Interests != null && playerEntity.Interests.Any())
            {
                _playerInterestService.DeleteAll(playerEntity.Interests);
            }

            if (entitys.Any())
            {
                foreach (var item in entitys)
                {
                    ValidationResult.Add(_playerInterestService.Create(item));
                }
            }

            var entitysAddPlayerRestrictionsSpecifics = new List<PlayerRestrictionsSpecifics>();
            foreach (var item in playerinterestViewModel.RestrictionsSpecifics)
            {
                entitysAddPlayerRestrictionsSpecifics.Add(item.MapReverse());
            }

            if (playerEntity.RestrictionsSpecifics != null && playerEntity.RestrictionsSpecifics.Any())
            {
                _playerRestrictionsSpecificsRepository.DeleteAll(playerEntity.RestrictionsSpecifics);
            }

            if (entitysAddPlayerRestrictionsSpecifics.Any())
            {
                foreach (var item in entitysAddPlayerRestrictionsSpecifics)
                {
                    item.SetPlayer(playerEntity);
                    item.SetLanguage(_languageRepository.Get(e => e.Code == item.LanguageCode));
                }

                playerEntity.SetRestrictionsSpecifics(entitysAddPlayerRestrictionsSpecifics);


                ValidationResult.Add(new PlayerIsInterestsIsConsistent().Valid(playerEntity));

                var resultCreateRestrictionsSpecificsService = _playerRestrictionsSpecificsService.CreateAll(entitysAddPlayerRestrictionsSpecifics);
                ValidationResult.Add(resultCreateRestrictionsSpecificsService);
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public PlayerDetailWithInterestAppViewModel GetByDetailsWithInterests(Guid playerUid)
        {
            PlayerDetailWithInterestAppViewModel vm = null;
            var s = service as IPlayerService;

            var entity = s.GetWithInterests(playerUid);

            if (entity != null)
            {
                var interest = _interestRepository.GetAll().ToList();
                var languages = _languageRepository.GetAll().ToList();
                var activities = _activityRepository.GetAll().ToList();
                var targetAudienceOptions = _targetAudienceRepository.GetAll().ToList();

                vm = new PlayerDetailWithInterestAppViewModel(entity);

                vm.LanguagesOptions = LanguageAppViewModel.MapList(languages).ToList();
                //vm.Interests = PlayerInterestAppViewModel.MapList(interest, entity).ToList();
                //vm.Activitys = PlayerActivityAppViewModel.MapList(activities, entity).ToList();
                //vm.TargetAudience = PlayerTargetAudienceAppViewModel.MapList(targetAudienceOptions, entity).ToList();             
            }

            return vm;
        }

        #endregion

        #region Private methods

        private void LoadViewModelOptions(PlayerEditAppViewModel viewModel)
        {
            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());
            viewModel.HoldingsOptions = HoldingItemListAppViewModel.MapList(_holdingRepository.GetAllSimple().OrderBy(e => e.Name).ToList());


        }


        private Expression<Func<Player, bool>> GetPredicateForGetAllOption(PlayerSelectOptionFilterAppViewModel filter, IEnumerable<int> idsPlayersExclude)
        {
            var predicate = PredicateBuilder.New<Player>(true);

            var predicateExcludesIds = PredicateBuilder.New<Player>(false);

            predicateExcludesIds = predicateExcludesIds.Or(p => !idsPlayersExclude.Contains(p.Id));

            predicate = PredicateBuilder.And<Player>(predicate, predicateExcludesIds);

            var predicateZipCode = PredicateBuilder.New<Player>(false);

            //predicateZipCode = predicateZipCode.Or(p => p.Address != null && p.Address.ZipCode != null);

            predicate = PredicateBuilder.And<Player>(predicate, predicateZipCode);

            var predicateInterest = PredicateBuilder.New<Player>(false);

            predicateInterest = predicateInterest.Or(p => p.Interests.Any());

            predicate = PredicateBuilder.And<Player>(predicate, predicateInterest);

            if (filter != null)
            {
                if (filter.Genres != null && filter.Genres.Any())
                {
                    var predicateGenres = PredicateBuilder.New<Player>(false);

                    predicateGenres = predicateGenres.Or(p => p.Interests.Any(i => filter.Genres.Any(g => g.Contains(i.Interest.Name))));

                    predicate = PredicateBuilder.And<Player>(predicate, predicateGenres);
                }

                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    var predicateName = PredicateBuilder.New<Player>(false);

                    predicateName = predicateName.Or(p => p.Name.Contains(filter.Name) || p.Holding.Name.Contains(filter.Name));

                    predicate = PredicateBuilder.And<Player>(predicate, predicateName);
                }
            }

            return predicate;
        }

        private Expression<Func<Player, bool>> GetPredicateForGetAllOptionByUser(PlayerSelectOptionFilterAppViewModel filter, IEnumerable<int> idsPlayersInclude)
        {
            var predicate = PredicateBuilder.New<Player>(true);

            var predicateIncludesIds = PredicateBuilder.New<Player>(false);

            predicateIncludesIds = predicateIncludesIds.Or(p => idsPlayersInclude.Contains(p.Id));

            predicate = PredicateBuilder.And<Player>(predicate, predicateIncludesIds);

            var predicateZipCode = PredicateBuilder.New<Player>(false);

            //predicateZipCode = predicateZipCode.Or(p => p.Address != null && p.Address.ZipCode != null);

            predicate = PredicateBuilder.And<Player>(predicate, predicateZipCode);

            var predicateInterest = PredicateBuilder.New<Player>(false);

            predicateInterest = predicateInterest.Or(p => p.Interests.Any());

            predicate = PredicateBuilder.And<Player>(predicate, predicateInterest);

            if (filter != null)
            {
                if (filter.Genres != null && filter.Genres.Any())
                {
                    var predicateGenres = PredicateBuilder.New<Player>(false);

                    predicateGenres = predicateGenres.Or(p => p.Interests.Any(i => filter.Genres.Any(g => g.Contains(i.Interest.Name))));

                    predicate = PredicateBuilder.And<Player>(predicate, predicateGenres);
                }
            }

            return predicate;
        }

        public IEnumerable<object> GetAllApi()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerSelectOptionAppViewModel> GetAllPlayersWithImages()
        {

            var entities = _playerRepository.GetImageAll().ToList();

            if (entities != null && entities.Any())
            {
                return entities.Select(e =>
                {
                    var vm = new PlayerSelectOptionAppViewModel(e, true);

                    return vm;
                });
            }

            return null;
        }

        public ImageFileAppViewModel GetImage(Guid uid)
        {
            var entity = _playerRepository.GetImage(e => e.Uid == uid);

            if (entity != null && entity.ImageId > 0)
            {
                return new ImageFileAppViewModel(entity.Image);
            }

            return null;
        }







        #endregion
    }
}
