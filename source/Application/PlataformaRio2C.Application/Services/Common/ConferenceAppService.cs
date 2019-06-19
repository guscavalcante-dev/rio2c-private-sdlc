using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Application.ViewModels.Site;
using PlataformaRio2C.Application.ViewModels.Api;

namespace PlataformaRio2C.Application.Services
{
    public class ConferenceAppService : AppService<PlataformaRio2CContext, Conference, ConferenceAppViewModel, ViewModels.ConferenceDetailAppViewModel, ConferenceEditAppViewModel, ViewModels.ConferenceItemListAppViewModel>, IConferenceAppService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IConferenceTitleRepository _conferenceTitleRepository;
        private readonly IConferenceSynopsisRepository _conferenceSynopsisRepository;
        private readonly IConferenceLecturerRepository _conferenceLecturerRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILecturerJobTitleRepository _lecturerJobTitleRepository;
        private readonly IRoleLecturerRepository _roleLecturerRepository;

        #region ctor

        public ConferenceAppService(IConferenceService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
            : base(unitOfWork, service)
        {
            _languageRepository = repositoryFactory.LanguageRepository;
            _conferenceTitleRepository = repositoryFactory.ConferenceTitleRepository;
            _conferenceSynopsisRepository = repositoryFactory.ConferenceSynopsisRepository;
            _conferenceLecturerRepository = repositoryFactory.ConferenceLecturerRepository;
            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
            _roomRepository = repositoryFactory.RoomRepository;
            _lecturerJobTitleRepository = repositoryFactory.LecturerJobTitleRepository;
            _roleLecturerRepository = repositoryFactory.RoleLecturerRepository;
        }

        #endregion

        #region public methods      

        public override ConferenceEditAppViewModel GetEditViewModel()
        {
            var viewModel = new ConferenceEditAppViewModel();

            return GetDefaultOptions(viewModel);
        }

        public override AppValidationResult Create(ConferenceEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = viewModel.MapReverse();

            MapEntity(ref entity, ref viewModel);

            ValidationResult.Add(service.Create(entity));

            if (ValidationResult.IsValid)
                Commit();

            if (!ValidationResult.IsValid)
            {
                viewModel.Languages = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());

                if (viewModel.Lecturers != null && viewModel.Lecturers.Any(e => e.ImageUpload != null))
                {
                    foreach (var item in viewModel.Lecturers)
                    {
                        item.ImageUpload = null;
                    }
                }
            }

            return ValidationResult;
        }

        public override ConferenceEditAppViewModel GetByEdit(Guid uid)
        {
            ConferenceEditAppViewModel vm = null;

            var entity = service.Get(uid);

            if (entity != null)
            {
                vm = new ConferenceEditAppViewModel(entity);
                vm.Languages = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());
            }

            return vm;
        }

        public override AppValidationResult Update(ConferenceEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);

                MapEntity(ref entity, ref viewModel);

                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
                Commit();

            if (!ValidationResult.IsValid)
            {
                viewModel.Languages = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());

                if (viewModel.Lecturers != null && viewModel.Lecturers.Any(e => e.ImageUpload != null))
                {
                    foreach (var item in viewModel.Lecturers)
                    {
                        item.ImageUpload = null;
                    }
                }
            }

            return ValidationResult;
        }

        public ConferenceEditAppViewModel GetDefaultOptions(ConferenceEditAppViewModel viewModel)
        {
            viewModel.Languages = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());
            return viewModel;
        }

        public IEnumerable<ViewModels.Site.ConferenceItemListAppViewModel> GetAllByPortal()
        {
            var entities = service.GetAll(true);
            if (entities != null && entities.Any())
            {
                IEnumerable<ViewModels.Site.ConferenceItemListAppViewModel> results = ViewModels.Site.ConferenceItemListAppViewModel.MapList(entities);
                return results.ToList();
            }

            return new List<ViewModels.Site.ConferenceItemListAppViewModel>() { };
        }

        public ImageFileAppViewModel GetLecturerThumbImage(Guid uid)
        {
            var s = service as IConferenceService;
            var entity = s.GetLecturerImage(uid);

            if (entity != null)
            {
                if (entity.Collaborator != null && entity.Collaborator.ImageId > 0)
                {
                    return ImageFileAppViewModel.GetThumbImage(entity.Collaborator.Image);
                }
                else if (entity.Lecturer != null && entity.Lecturer.ImageId > 0)
                {
                    return ImageFileAppViewModel.GetThumbImage(entity.Lecturer.Image);
                }
            }

            return null;
        }

        public ImageFileAppViewModel GetLecturerImage(Guid uid)
        {
            var s = service as IConferenceService;
            var entity = s.GetLecturerImage(uid);

            if (entity != null)
            {
                if (entity.Collaborator != null && entity.Collaborator.ImageId > 0)
                {
                    return new ImageFileAppViewModel(entity.Collaborator.Image);
                }
                else if (entity.Lecturer != null && entity.Lecturer.ImageId > 0)
                {
                    return new ImageFileAppViewModel(entity.Lecturer.Image);
                }
            }


            return null;
        }
        #endregion

        #region private methods
        private void MapEntity(ref Conference entity, ref ConferenceEditAppViewModel viewModel)
        {
            MapEntityRoom(ref entity, viewModel);
            MapEntityLecturers(ref entity, viewModel.Lecturers);
            MapEntityTitles(ref entity, viewModel.Titles);
            MapEntitySynopses(ref entity, viewModel.Synopses);
        }
        private void MapEntityRoom(ref Conference entity, ConferenceEditAppViewModel viewModel)
        {
            if (viewModel.Room != Guid.Empty)
            {
                var roomEntity = _roomRepository.Get(viewModel.Room);
                if (roomEntity != null)
                {
                    entity.SetRoom(roomEntity);
                }
            }
        }
        private void MapEntityLecturers(ref Conference entity, IEnumerable<ViewModels.LecturerAppViewModel> lecturers)
        {
            if (lecturers != null && lecturers.Any())
            {
                lecturers = lecturers.Where(e => (e.IsPreRegistered && e.Collaborator != null && e.Collaborator.Uid != Guid.Empty) || (!e.IsPreRegistered && !string.IsNullOrWhiteSpace(e.Name))).ToList();

                if (entity.Lecturers != null && entity.Lecturers.Any())
                {
                    _conferenceLecturerRepository.DeleteAll(entity.Lecturers);
                }

                if (lecturers != null && lecturers.Any())
                {
                    var entitiestitles = new List<ConferenceLecturer>();

                    foreach (var lecturerViewModel in lecturers)
                    {
                        var entityConferenceLecturer = lecturerViewModel.MapReverse();

                        var entityRoleLecturer = _roleLecturerRepository.Get(lecturerViewModel.RoleLecturerUid);
                        if (entityRoleLecturer != null)
                        {
                            entityConferenceLecturer.SetRoleLecturer(entityRoleLecturer);
                        }

                        if (lecturerViewModel.IsPreRegistered && lecturerViewModel.Collaborator != null)
                        {
                            var collaborator = _collaboratorRepository.Get(e => e.Uid == lecturerViewModel.Collaborator.Uid);
                            if (collaborator != null)
                            {
                                entityConferenceLecturer.SetCollaborator(collaborator);

                                lecturerViewModel.Collaborator = new CollaboratorOptionAppViewModel(collaborator);
                            }                           
                        }
                        else if (!lecturerViewModel.IsPreRegistered)
                        {
                            var entityLecturer = entityConferenceLecturer.Lecturer;
                            MapJobTitles(ref entityLecturer, lecturerViewModel.JobTitles);
                        }

                        entityConferenceLecturer.SetConference(entity);
                        _conferenceLecturerRepository.Create(entityConferenceLecturer);
                    }

                    entity.SetLecturers(entitiestitles);
                }
            }
        }
        private void MapJobTitles(ref Lecturer entityLecturer, IEnumerable<LecturerJobTitleAppViewModel> jobTitles)
        {
            if (entityLecturer.JobTitles != null && entityLecturer.JobTitles.Any())
            {
                _lecturerJobTitleRepository.DeleteAll(entityLecturer.JobTitles);
            }

            if (jobTitles != null && jobTitles.Any())
            {
                var entitiesJobTitles = new List<LecturerJobTitle>();
                foreach (var jobTitleViewModel in jobTitles)
                {
                    var entityJobTitle = jobTitleViewModel.MapReverse();
                    var language = _languageRepository.Get(e => e.Code == jobTitleViewModel.LanguageCode);
                    entityJobTitle.SetLanguage(language);
                    entitiesJobTitles.Add(entityJobTitle);
                }
                entityLecturer.SetJobTitles(entitiesJobTitles);
            }
        }
        private void MapEntityTitles(ref Conference entity, IEnumerable<ConferenceTitleAppViewModel> titles)
        {
            if (entity.Titles != null && entity.Titles.Any())
            {
                _conferenceTitleRepository.DeleteAll(entity.Titles);
            }

            if (titles != null && titles.Any())
            {
                var entitiestitles = new List<ConferenceTitle>();
                foreach (var titleViewModel in titles)
                {
                    var entityMiniBio = titleViewModel.MapReverse();
                    var language = _languageRepository.Get(e => e.Code == titleViewModel.LanguageCode);
                    entityMiniBio.SetLanguage(language);
                    entityMiniBio.SetConference(entity);
                    entitiestitles.Add(entityMiniBio);

                    _conferenceTitleRepository.Create(entityMiniBio);
                }
                entity.SetTitles(entitiestitles);
            }
        }
        private void MapEntitySynopses(ref Conference entity, IEnumerable<ConferenceSynopsisAppViewModel> synopses)
        {
            if (entity.Synopses != null && entity.Synopses.Any())
            {
                _conferenceSynopsisRepository.DeleteAll(entity.Synopses);
            }

            if (synopses != null && synopses.Any())
            {
                var entitiesSynopsis = new List<ConferenceSynopsis>();
                foreach (var ConferenceSynopsisViewModel in synopses)
                {
                    var entityMiniBio = ConferenceSynopsisViewModel.MapReverse();
                    var language = _languageRepository.Get(e => e.Code == ConferenceSynopsisViewModel.LanguageCode);
                    entityMiniBio.SetLanguage(language);
                    entityMiniBio.SetConference(entity);
                    entitiesSynopsis.Add(entityMiniBio);

                    _conferenceSynopsisRepository.Create(entityMiniBio);
                }
                entity.SetSynopses(entitiesSynopsis);
            }

        }

        public IEnumerable<ViewModels.Api.ConferenceItemListAppViewModel> GetAllByApi()
        {
            var entities = service.GetAll(true);
            if (entities != null && entities.Any())
            {
                IEnumerable<ViewModels.Api.ConferenceItemListAppViewModel> results = ViewModels.Api.ConferenceItemListAppViewModel.MapList(entities);
                return results.ToList();
            }

            return new List<ViewModels.Api.ConferenceItemListAppViewModel>() { };
        }

        public ViewModels.Api.ConferenceDetailAppViewModel GetByApi(Guid uid)
        {
            ViewModels.Api.ConferenceDetailAppViewModel vm = null;

            var entity = service.Get(uid);

            if (entity != null)
            {
                vm = new ViewModels.Api.ConferenceDetailAppViewModel(entity);
                
            }
            return vm;
        }
        #endregion

    }
}
