using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    public class ProducerAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Producer, ProducerBasicAppViewModel, ProducerDetailAppViewModel, ProducerEditAppViewModel, ProducerItemListAppViewModel>, IProducerAppService
    {
        private readonly ICollaboratorService _collaboratorService;
        private readonly ILanguageRepository _languageRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly ITargetAudienceRepository _targetAudienceRepository;
        private readonly IProducerDescriptionRepository _producerDescriptionRepository;
        private readonly IProducerActivityRepository _producerActivityRepository;
        private readonly IProducerTargetAudienceRepository _producerTargetAudienceRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;


        public ProducerAppService(IProducerService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, ICollaboratorService collaboratorService)
            : base(unitOfWork, service)
        {
            _collaboratorService = collaboratorService;
            _languageRepository = repositoryFactory.LanguageRepository;
            _activityRepository = repositoryFactory.ActivityRepository;
            _targetAudienceRepository = repositoryFactory.TargetAudienceRepository;
            _producerDescriptionRepository = repositoryFactory.ProducerDescriptionRepository;
            _producerActivityRepository = repositoryFactory.ProducerActivityRepository;
            _producerTargetAudienceRepository = repositoryFactory.ProducerTargetAudienceRepository;
            _producerRepository = repositoryFactory.ProducerRepository;
            _countryRepository = repositoryFactory.CountryRepository;
            _stateRepository = repositoryFactory.StateRepository;
            _cityRepository = repositoryFactory.CityRepository;

        }

        public AppValidationResult UpdateByPortal(ProducerEditAppViewModel viewModel)
        {
            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll());

            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var cityId = viewModel.Address.CityId == null ? 0 : (int)viewModel.Address.CityId;
                var stateID = viewModel.Address.StateId == null ? 0 : (int)viewModel.Address.StateId;
                if (entity.Address == null)
                {
                    //var address = new Address();
                    //address.SetCountry((int)viewModel.Address.CountryId);
                    //if (address.CountryId == 30)
                    //{
                    //    address.SetCity(cityId);
                    //    address.SetState(stateID);
                    //    address.SetCity("");
                    //    address.SetState("");
                    //}
                    //else
                    //{
                    //    address.SetCity(viewModel.Address.City);
                    //    address.SetState(viewModel.Address.State);
                    //    address.SetCity(0);
                    //    address.SetState(0);
                    //}

                    //entity.SetAddress(address);

                }
                else
                {
                    //entity.Address.SetCountry((int)viewModel.Address.CountryId);

                    //if (entity.Address.CountryId == 30)
                    //{
                    //    entity.Address.SetCity(cityId);
                    //    entity.Address.SetState(stateID);
                    //    entity.Address.SetCity("");
                    //    entity.Address.SetState("");
                    //}
                    //else
                    //{
                    //    entity.Address.SetCity(viewModel.Address.City);
                    //    entity.Address.SetState(viewModel.Address.State);
                    //    entity.Address.SetCity(0);
                    //    entity.Address.SetState(0);
                    //}
                }

                var a = entity.Address.Uid.ToString();

                _producerActivityRepository.DeleteAll(entity.ProducerActivitys);
                _producerTargetAudienceRepository.DeleteAll(entity.ProducerTargetAudience);

                var entityAlter = viewModel.MapReverse(entity);
                entityAlter.SetCNPJ(entity.CNPJ);
                //entityAlter.Address.SetCountry((int)viewModel.Address.CountryId);
                MapDescriptions(ref entityAlter, viewModel.Descriptions);

                var _activities = _activityRepository.GetAll();
                var entitysActivities = new List<ProducerActivity>();

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

                entityAlter.SetProducerActivitys(entitysActivities);

                var _targetAudiencieOptions = _targetAudienceRepository.GetAll();
                var entitiesTargetAudience = new List<ProducerTargetAudience>();
                foreach (var targetAudienceViewModel in viewModel.TargetAudience)
                {
                    if (targetAudienceViewModel.Selected)
                    {
                        entitiesTargetAudience.Add(targetAudienceViewModel.MapReverse(entity, _targetAudiencieOptions.FirstOrDefault(e => e.Id == targetAudienceViewModel.TargetAudienceId)));
                    }
                }
                entityAlter.SetProducerTargetAudience(entitiesTargetAudience);

                var s = service as IProducerService;
                ValidationResult.Add(s.UpdateByPortal(entityAlter));
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

        public IEnumerable<ProducerAppViewModel> GetAllByUserId(int id)
        {
            IList<ProducerAppViewModel> viewModel = new List<ProducerAppViewModel>();

            var entityCollaborator = _collaboratorService.GetWithProducerByUserId(id);

            //if (entityCollaborator != null && entityCollaborator.ProducersEvents != null && entityCollaborator.ProducersEvents.Any())
            //{
            //    var producersId = entityCollaborator.ProducersEvents.Select(e => e.ProducerId).Distinct();
            //    var producers = _producerRepository.GetAll(e => producersId.Contains(e.Id));
            //    if (producers != null)
            //    {
            //        foreach (var producer in producers)
            //        {
            //            viewModel.Add(new ProducerAppViewModel(producer));
            //        }
            //    }
            //}

            return viewModel;
        }

        public ProducerAppViewModel GetByUserId(int id, Guid producerUid)
        {
            var playersResult = GetAllByUserId(id);

            return playersResult.FirstOrDefault(e => e.Uid == producerUid);
        }
        public ProducerDetailAppViewModel GetDetailByUserId(int id)
        {
            ProducerDetailAppViewModel viewModel = null;

            var entityCollaborator = _collaboratorService.GetWithProducerByUserId(id);

            //if (entityCollaborator != null && entityCollaborator.ProducersEvents != null && entityCollaborator.ProducersEvents.Any())
            //{
            //    var producersId = entityCollaborator.ProducersEvents.Select(e => e.ProducerId).Distinct();
            //    var producers = _producerRepository.GetAll(e => producersId.Contains(e.Id));
            //    if (producers.FirstOrDefault() != null)
            //    {
            //        viewModel = new ProducerDetailAppViewModel(producers.FirstOrDefault());
            //        var a = viewModel.Address.Uid.ToString();
            //    }
            //}
            //viewModel = new ProducerDetailAppViewModel(entityCollaborator.);

            return viewModel;
        }

        public ProducerDetailAppViewModel GetDetailByUserId(int id, Guid producerUid)
        {
            ProducerDetailAppViewModel viewModel = null;

            var entityCollaborator = _collaboratorService.GetWithProducerByUserId(id);

            //if (entityCollaborator != null && entityCollaborator.ProducersEvents != null && entityCollaborator.ProducersEvents.Any())
            //{
            //    var producersId = entityCollaborator.ProducersEvents.Select(e => e.ProducerId).Distinct();
            //    var producers = _producerRepository.GetAll(e => producersId.Contains(e.Id));
            //    if (producers.FirstOrDefault(e => e.Uid == producerUid) != null)
            //    {
            //        viewModel = new ProducerDetailAppViewModel(producers.FirstOrDefault(e => e.Uid == producerUid));
            //    }
            //}

            return viewModel;
        }

        public ProducerEditAppViewModel GetEditByUserId(int id)
        {
            ProducerEditAppViewModel viewModel = null;

            var entityCollaborator = _collaboratorService.GetWithProducerByUserId(id);

            //if (entityCollaborator != null && entityCollaborator.ProducersEvents != null && entityCollaborator.ProducersEvents.Any())
            //{
            //    var languages = _languageRepository.GetAll().ToList();
            //    var activities = _activityRepository.GetAll().ToList();
            //    var targetAudienceOptions = _targetAudienceRepository.GetAll().ToList();
            //    var producersId = entityCollaborator.ProducersEvents.Select(e => e.ProducerId).Distinct();
            //    var producers = _producerRepository.GetAll(e => producersId.Contains(e.Id));

            //    if (producers.FirstOrDefault() != null)
            //    {
            //        viewModel = new ProducerEditAppViewModel(producers.FirstOrDefault());
            //        viewModel.LanguagesOptions = LanguageAppViewModel.MapList(languages);
            //        viewModel.Activitys = ProducerActivityAppViewModel.MapList(activities, producers.FirstOrDefault()).ToList();
            //        viewModel.TargetAudience = ProducerTargetAudienceAppViewModel.MapList(targetAudienceOptions, producers.FirstOrDefault()).ToList();
            //    }
            //}

            return viewModel;
        }

        public ProducerEditAppViewModel GetEditByUserId(int id, Guid producerUid)
        {
            ProducerEditAppViewModel viewModel = null;
            var entityproducer = service.Get(producerUid);
            //var entityCollaborator = _collaboratorService.GetWithProducerByUserId(id);

            //if (entityCollaborator != null && entityCollaborator.ProducersEvents != null && entityCollaborator.ProducersEvents.Any())
            if (entityproducer != null)
            {
                var languages = _languageRepository.GetAll().ToList();
                var activities = _activityRepository.GetAll().ToList();
                var targetAudienceOptions = _targetAudienceRepository.GetAll().ToList();
                var countries = _countryRepository.GetAll().ToList();

                var producersId = entityproducer.Id; //entityCollaborator.ProducersEvents.Select(e => e.ProducerId).Distinct();
                var producers = _producerRepository.GetAll(e => e.Id == producersId).ToList();
                var addressEntity = entityproducer.Address;

                List<State> states = null;
                List<City> cities = null;

                if (addressEntity != null)
                {
                    //if (addressEntity.CountryId != null)
                    //{
                    //    states = _stateRepository.GetAll(a => a.CountryId == addressEntity.CountryId).ToList();
                    //}

                    //if (addressEntity.StateId != null)
                    //{
                    //    cities = _cityRepository.GetAll(a => a.StateId == addressEntity.StateId).ToList();
                    //}
                }
                else
                {
                    //addressEntity = new Address();
                }


                //addressEntity.Countries = countries;
                //addressEntity.States = states;
                //addressEntity.Cities = cities;

                if (producers.FirstOrDefault(e => e.Uid == producerUid) != null)
                {
                    viewModel = new ProducerEditAppViewModel(producers.FirstOrDefault(e => e.Uid == producerUid));
                    viewModel.LanguagesOptions = LanguageAppViewModel.MapList(languages);
                    viewModel.Activitys = ProducerActivityAppViewModel.MapList(activities, producers.FirstOrDefault(e => e.Uid == producerUid)).ToList();
                    viewModel.TargetAudience = ProducerTargetAudienceAppViewModel.MapList(targetAudienceOptions, producers.FirstOrDefault(e => e.Uid == producerUid)).ToList();
                    viewModel.Address = new AddressAppViewModel(addressEntity);
                    viewModel.Countries = countries;

                }
            }

            return viewModel;
        }

        public ImageFileAppViewModel GetThumbImage(Guid uid)
        {
            var s = service as IProducerService;
            var entity = s.GetImage(uid);

            if (entity != null && entity.ImageId > 0)
            {
                return ImageFileAppViewModel.GetThumbImage(entity.Image);
            }

            return null;
        }

        public ImageFileAppViewModel GetImage(Guid uid)
        {
            var s = service as IProducerService;
            var entity = s.GetImage(uid);

            if (entity != null && entity.ImageId > 0)
            {
                return ImageFileAppViewModel.GetThumbImage(entity.Image);
            }

            return null;
        }

        public override ProducerEditAppViewModel GetByEdit(Guid uid)
        {
            ProducerEditAppViewModel vm = null;

            var entity = service.Get(uid);

            if (entity != null)
            {
                vm = new ProducerEditAppViewModel(entity);

                var activities = _activityRepository.GetAll().ToList();
                var targetAudienceOptions = _targetAudienceRepository.GetAll().ToList();

                vm.Activitys = ProducerActivityAppViewModel.MapList(activities, entity).ToList();
                vm.TargetAudience = ProducerTargetAudienceAppViewModel.MapList(targetAudienceOptions, entity).ToList();

                vm.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());
            }

            return vm;
        }

        public override AppValidationResult Update(ProducerEditAppViewModel viewModel)
        {
            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll());

            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);

                _producerActivityRepository.DeleteAll(entity.ProducerActivitys);
                _producerTargetAudienceRepository.DeleteAll(entity.ProducerTargetAudience);

                var _activities = _activityRepository.GetAll();
                var entitysActivities = new List<ProducerActivity>();

                foreach (var activityViewModel in viewModel.Activitys)
                {
                    if (activityViewModel.Selected)
                    {
                        entitysActivities.Add(activityViewModel.MapReverse(entity, _activities.FirstOrDefault(e => e.Id == activityViewModel.ActivityId)));
                    }
                }

                entityAlter.SetProducerActivitys(entitysActivities);

                var _targetAudiencieOptions = _targetAudienceRepository.GetAll();
                var entitiesTargetAudience = new List<ProducerTargetAudience>();
                foreach (var targetAudienceViewModel in viewModel.TargetAudience)
                {
                    if (targetAudienceViewModel.Selected)
                    {
                        entitiesTargetAudience.Add(targetAudienceViewModel.MapReverse(entity, _targetAudiencieOptions.FirstOrDefault(e => e.Id == targetAudienceViewModel.TargetAudienceId)));
                    }
                }

                entityAlter.SetProducerTargetAudience(entitiesTargetAudience);

                MapDescriptions(ref entityAlter, viewModel.Descriptions);
                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;

        }


        private void MapDescriptions(ref Producer entity, IEnumerable<ProducerDescriptionAppViewModel> descriptions)
        {
            if (entity.Descriptions != null && entity.Descriptions.Any())
            {
                _producerDescriptionRepository.DeleteAll(entity.Descriptions);
            }

            if (descriptions != null && descriptions.Any())
            {
                var entitiesDescriptions = new List<ProducerDescription>();
                foreach (var descriptionViewModel in descriptions)
                {
                    var entityDescription = descriptionViewModel.MapReverse();
                    var language = _languageRepository.Get(e => e.Code == descriptionViewModel.LanguageCode);
                    entityDescription.SetLanguage(language);
                    entitiesDescriptions.Add(entityDescription);
                }
                entity.SetDescriptions(entitiesDescriptions);
            }
        }


    }
}
