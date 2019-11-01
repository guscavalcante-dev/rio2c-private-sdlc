// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="ProjectAppService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Application.Dtos;
using System.Linq.Expressions;
using LinqKit;
using OfficeOpenXml;
using System.Globalization;
using System.Threading;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Application.Services
{
    /// <summary>ProjectAppService</summary>
    public class ProjectAppService : AppService<PlataformaRio2CContext, Project, ProjectBasicAppViewModel, ProjectDetailAppViewModel, ProjectEditAppViewModel, ProjectItemListAppViewModel>, IProjectAppService
    {
        #region props

        private readonly ILanguageRepository _languageRepository;
        private readonly IEditionRepository _eventRepository;
        private readonly IInterestRepository _interestRepository;

        private readonly IProjectTitleRepository _projectTitleRepository;
        private readonly IProjectLogLineRepository _projectLogLineRepository;
        private readonly IProjectSummaryRepository _projectSummaryRepository;
        private readonly IProjectProductionPlanRepository _projectProductionPlanRepository;
        private readonly IProjectInterestRepository _projectInterestRepository;
        private readonly IProjectLinkImageRepository _projectLinkImageRepository;
        private readonly IProjectLinkTeaserRepository _projectLinkTeaserRepository;
        private readonly IProjectAdditionalInformationRepository _projectAdditionalInformationRepository;
        private readonly IProjectPlayerRepository _projectPlayerRepository;
        private readonly IProjectPlayerEvaluationRepository _projectPlayerEvaluationRepository;
        private readonly IProducerService _producerService;
        private readonly ICollaboratorService _collaboratorService;

        private readonly Infra.CrossCutting.SystemParameter.ISystemParameterRepository _systemParameterRepository;


        #endregion

        #region ctor

        public ProjectAppService(IProjectService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, ICollaboratorService collaboratorService, IProducerService producerService, Infra.CrossCutting.SystemParameter.ISystemParameterRepository systemParameterRepository)
            : base(unitOfWork, service)
        {
            _languageRepository = repositoryFactory.LanguageRepository;
            _projectTitleRepository = repositoryFactory.ProjectTitleRepository;
            _projectLogLineRepository = repositoryFactory.ProjectLogLineRepository;
            _projectSummaryRepository = repositoryFactory.ProjectSummaryRepository;
            _projectProductionPlanRepository = repositoryFactory.ProjectProductionPlanRepository;
            _eventRepository = repositoryFactory.EditionRepository;
            _interestRepository = repositoryFactory.InterestRepository;
            _projectInterestRepository = repositoryFactory.ProjectInterestRepository;
            _projectLinkImageRepository = repositoryFactory.ProjectLinkImageRepository;
            _projectLinkTeaserRepository = repositoryFactory.ProjectLinkTeaserRepository;
            _projectAdditionalInformationRepository = repositoryFactory.ProjectAdditionalInformationRepository;
            _projectPlayerRepository = repositoryFactory.ProjectPlayerRepository;
            _collaboratorService = collaboratorService;
            _producerService = producerService;

            _projectPlayerEvaluationRepository = repositoryFactory.ProjectPlayerEvaluationRepository;
            _systemParameterRepository = systemParameterRepository;
        }

        #endregion

        #region Public methods

        public AppValidationResult Create(ProjectEditAppViewModel viewModel, int userId)
        {
            var entityEvent = _eventRepository.Get(e => e.Name.Contains("2018"));
            var entityProducer = _producerService.GetByUserIdAndEventId(userId, entityEvent.Id);

            BeginTransaction();

            var entityProject = viewModel.MapReverse();
            MapEntity(ref entityProject, viewModel);
            //entityProject.SetProducer(entityProducer);

            ValidationResult.Add(service.Create(entityProject));

            if (ValidationResult.IsValid)
                Commit();

            viewModel.UIdCreate = entityProject.Uid;

            LoadViewModelOptions(viewModel, entityProject);

            return ValidationResult;
        }
        public AppValidationResult Update(ProjectEditAppViewModel viewModel, int userId)
        {
            BeginTransaction();

            var entityProject = service.Get(e => e.Uid == viewModel.Uid);
            if (entityProject != null)
            {
                var entityProjectAlter = viewModel.MapReverse(entityProject);
                MapEntity(ref entityProjectAlter, viewModel);

                ValidationResult.Add(service.Update(entityProject));

                if (ValidationResult.IsValid)
                    Commit();

                LoadViewModelOptions(viewModel, entityProjectAlter);
            }

            return ValidationResult;
        }

        public override AppValidationResult Update(ProjectEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entityProject = service.Get(e => e.Uid == viewModel.Uid);
            if (entityProject != null)
            {
                var entityProjectAlter = viewModel.MapReverse(entityProject);
                MapEntity(ref entityProjectAlter, viewModel);
                var s = service as IProjectService;
                ValidationResult.Add(s.UpdateByAdmin(entityProject));

                if (ValidationResult.IsValid)
                    Commit();

                LoadViewModelOptions(viewModel, entityProjectAlter);
            }

            return ValidationResult;
        }

        public IEnumerable<ProjectAdminItemListAppViewModel> AllByAdmin()
        {
            var s = service as IProjectService;
            var entities = s.GetAllByAdmin();
            if (entities != null && entities.Any())
            {
                IEnumerable<ProjectAdminItemListAppViewModel> results = ProjectAdminItemListAppViewModel.MapList(entities);
                return results.ToList();
            }

            return new List<ProjectAdminItemListAppViewModel>() { };
        }


        public ProjectEditAppViewModel GetEditByUserId(int id)
        {
            return GetEditViewModelProducerProject(id);
        }
        public ProjectEditAppViewModel GetEditViewModelProducerProject(int userId)
        {
            var viewModel = new ProjectEditAppViewModel();
            LoadViewModelOptions(viewModel, null);

            return viewModel;
        }
        public IEnumerable<ProjectItemListAppViewModel> GetAllByUserProducerId(int id)
        {
            IList<ProjectItemListAppViewModel> result = new List<ProjectItemListAppViewModel>();

            var entityEvent = _eventRepository.Get(e => e.Name.Contains("2018"));
            var entityCollaborator = _collaboratorService.GetByUserId(id);

            //if (entityCollaborator != null && entityCollaborator.ProducersEvents != null && entityCollaborator.ProducersEvents.Any(e => e.EventId == entityEvent.Id))
            //{
            //    var entityProducerEvent = entityCollaborator.ProducersEvents.Where(e => e.EventId == entityEvent.Id).FirstOrDefault();

            //    if (entityProducerEvent != null && entityProducerEvent.ProducerId > 0)
            //    {
            //        var entitiesProjects = service.GetAll(e => e.ProducerId == entityProducerEvent.ProducerId);

            //        if (entitiesProjects != null && entitiesProjects.Any())
            //        {
            //            foreach (var entityProducerProject in entitiesProjects)
            //            {
            //                var itemViewModel = new ProjectItemListAppViewModel(entityProducerProject);

            //                result.Add(itemViewModel);
            //            }
            //        }
            //    }
            //}

            return result;
        }
        public ProjectEditAppViewModel GetByEdit(Guid projectUid, int userId)
        {
            ProjectEditAppViewModel result = null;

            var entityEvent = _eventRepository.Get(e => e.Name.Contains("2018"));
            var entityCollaborator = _collaboratorService.GetByUserId(userId);

            //if (entityCollaborator != null && entityCollaborator.ProducersEvents != null && entityCollaborator.ProducersEvents.Any(e => e.EventId == entityEvent.Id))
            //{
            //    var entityProducer = entityCollaborator.ProducersEvents.Where(e => e.EventId == entityEvent.Id).Select(e => e.Producer).FirstOrDefault();
            //    if (entityProducer != null && entityProducer.Projects != null && entityProducer.Projects.Any())
            //    {
            //        var project = entityProducer.Projects.FirstOrDefault(e => e.Uid == projectUid);
            //        if (project != null)
            //        {
            //            result = new ProjectEditAppViewModel(project);
            //            LoadViewModelOptions(result, project);
            //        }
            //    }
            //}

            return result;
        }

        public override ProjectEditAppViewModel GetByEdit(Guid uid)
        {
            ProjectEditAppViewModel vm = null;

            var entity = service.Get(uid);

            if (entity != null)
            {
                vm = new ProjectEditAppViewModel(entity);
                LoadViewModelOptions(vm, entity);
            }

            return vm;
        }

        public ProjectDetailAppViewModel GetByDetails(Guid projectUid, int userId)
        {
            ProjectDetailAppViewModel result = null;

            var entityEvent = _eventRepository.Get(e => e.Name.Contains("2018"));
            var entityCollaborator = _collaboratorService.GetByUserId(userId);

            //if (entityCollaborator != null && entityCollaborator.ProducersEvents != null && entityCollaborator.ProducersEvents.Any(e => e.EventId == entityEvent.Id))
            //{
            //    var entityProducer = entityCollaborator.ProducersEvents.Where(e => e.EventId == entityEvent.Id).Select(e => e.Producer).FirstOrDefault();

            //    if (entityProducer != null && entityProducer.Projects != null && entityProducer.Projects.Any())
            //    {
            //        var project = service.Get(e => e.ProducerId == entityProducer.Id && e.Uid == projectUid);
            //        if (project != null)
            //        {
            //            result = new ProjectDetailAppViewModel(project);
            //        }
            //    }
            //}

            return result;
        }




        public AppValidationResult SavePlayerSelection(Guid playerUid, Guid uidProject, int userId)
        {
            BeginTransaction();

            var s = service as IProjectService;
            var entity = s.GetSimpleWithProducer(e => e.Uid == uidProject);

            if (entity != null)
            {

                ValidationResult.Add(s.SavePlayerSelection(entity, playerUid, userId));
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;

        }
        public AppValidationResult RemovePlayerSelection(Guid playerUid, Guid uidProject, int userId)
        {
            BeginTransaction();

            var s = service as IProjectService;
            var entity = s.GetSimpleWithProducer(e => e.Uid == uidProject);

            if (entity != null)
            {

                ValidationResult.Add(s.RemovePlayerSelection(entity, playerUid, userId));
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;

        }
        public override AppValidationResult Delete(Guid uid)
        {
            BeginTransaction();
            var entity = service.Get(e => e.Uid == uid);

            if (entity != null)
            {
                ValidationResult.Add(service.Delete(entity));
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }
        public void MapEntity(ref Project entity, ProjectEditAppViewModel project)
        {
            MapEntityTitles(ref entity, project.Titles);
            MapEntityLogLines(ref entity, project.LogLines);
            MapEntitySummaries(ref entity, project.Summaries);
            MapEntityProductionPlans(ref entity, project.ProductionPlans);
            MapEntityInterests(ref entity, project.InterestsSelected);
            //MapEntityLinksImage(ref entity, project.LinksImage);
            //MapEntityLinksTeaser(ref entity, project.LinksTeaser);
            MapEntityAdditionalInformations(ref entity, project.AdditionalInformations);
        }
        public AppValidationResult SendToPlayers(Guid uidProject, Guid[] uidsPlayers, int userId)
        {
            BeginTransaction();

            var s = service as IProjectService;
            var entity = s.GetSimpleWithProducer(e => e.Uid == uidProject);

            if (entity != null)
            {
                ValidationResult.Add(s.SendToPlayers(entity, uidsPlayers, userId));
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public bool ProjectSendToPlayer(Guid uidProject, int userId)
        {
            bool result = false;

            var entityEvent = _eventRepository.Get(e => e.Name.Contains("2018"));
            var entityCollaborator = _collaboratorService.GetByUserId(userId);

            //if (entityCollaborator != null && entityCollaborator.ProducersEvents != null && entityCollaborator.ProducersEvents.Any(e => e.EventId == entityEvent.Id))
            //{
            //    var entityProducer = entityCollaborator.ProducersEvents.Where(e => e.EventId == entityEvent.Id).Select(e => e.Producer).FirstOrDefault();
            //    if (entityProducer != null && entityProducer.Projects != null && entityProducer.Projects.Any())
            //    {
            //        var project = entityProducer.Projects.FirstOrDefault(e => e.Uid == uidProject);
            //        if (project != null)
            //        {
            //            result = project.PlayersRelated != null && project.PlayersRelated.Any(e => e.Sent);
            //        }
            //    }
            //}

            return result;
        }

        public IEnumerable<ProjectPlayerItemListAppViewModel> GetAllByUserPlayerId(ProjectPlayerFilterAppDto filter, int userId)
        {
            IList<ProjectPlayerItemListAppViewModel> result = new List<ProjectPlayerItemListAppViewModel>();
            var entityEvent = _eventRepository.Get(e => e.Name.Contains("2018"));
            var entityCollaborator = _collaboratorService.GetByUserId(userId);

            //if (entityCollaborator != null && entityCollaborator.Players != null && entityCollaborator.Players.Any())
            //{
            //    var idsPlayers = entityCollaborator.Players.Select(e => e.Id);

            //    var projectsPlayers = _projectPlayerRepository.GetAll(GetPredicateByPlayer(filter, idsPlayers));

            //    if (projectsPlayers != null && projectsPlayers.Any())
            //    {
            //        var listProjectsPlayers = projectsPlayers.ToList().GroupBy(e => e.ProjectId).Select(e => e.First()).OrderBy(e => e.DateSending);

            //        foreach (var project in listProjectsPlayers)
            //        {
            //            var itemViewModel = new ProjectPlayerItemListAppViewModel(project);
            //            itemViewModel.Players = itemViewModel.Players.Where(e => idsPlayers.Contains(e.Id)).ToList();

            //            result.Add(itemViewModel);
            //        }

            //        result = result.ToList();
            //    }
            //}

            return result;
        }

        public ICollection<SelectListItem> GetStatusOption()
        {
            //var types = default(StatusProjectCodes).ToEnumDescriptions(false);
            //types.Insert(0, new EnumItem { Id = -1, Description = Labels.All, Value = "All"});
            //return types.Select(e => new SelectListItem { Value = e.Value.ToString(), Text = default(StatusProjectCodes).GetResourceDescription(e.Value, Labels.ResourceManager), Selected = false }).ToList();


            var statusProjectCodesOptionsEnum = default(StatusProjectCodes).ToEnumDescriptions(false);
            var optionsenum = statusProjectCodesOptionsEnum.Select(e => new SelectListItem { Value = e.Value.ToString(), Text = default(StatusProjectCodes).GetResourceDescription(e.Value, Labels.ResourceManager), Selected = false }).ToList();
            var options = new List<SelectListItem>() { new SelectListItem { Text = Labels.All, Value = "All" } };
            options.AddRange(optionsenum.ToList());
            return options;
        }

        public ProjectPlayerDetailAppViewModel GetForEvaluationPlayer(int userId, Guid uidProject)
        {
            ProjectPlayerDetailAppViewModel result = null;

            var entityEvent = _eventRepository.Get(e => e.Name.Contains("2018"));
            var entityCollaborator = _collaboratorService.GetByUserId(userId);

            //if (entityCollaborator != null && entityCollaborator.Players != null && entityCollaborator.Players.Any())
            //{
            //    var idsPlayers = entityCollaborator.Players.Select(e => e.Id);

            //    var projectsPlayer = _projectPlayerRepository.GetAll(GetPredicateByPlayer(null, idsPlayers)).FirstOrDefault(e => e.Project.Uid == uidProject);

            //    if (projectsPlayer != null)
            //    {
            //        result = new ProjectPlayerDetailAppViewModel(projectsPlayer);
            //        result.Players = result.Players.Where(e => idsPlayers.Contains(e.Id)).OrderBy(e => e.Name);
            //    }
            //}

            return result;
        }

        public AppValidationResult AcceptByPlayer(Guid playerUid, Guid uidProject, int userId)
        {
            BeginTransaction();

            var s = service as IProjectService;
            var entity = s.GetSimpleWithPlayers(e => e.Uid == uidProject);

            if (entity != null)
            {
                ValidationResult.Add(s.AcceptByPlayer(entity, playerUid, userId));
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public AppValidationResult RejectByPlayer(Guid playerUid, Guid uidProject, int userId, string reason)
        {
            BeginTransaction();

            var s = service as IProjectService;
            var entity = s.GetSimpleWithPlayers(e => e.Uid == uidProject);

            if (entity != null)
            {
                ValidationResult.Add(s.RejectByPlayer(entity, playerUid, userId, reason));
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public bool ExceededProjectMaximumPerProducer(int userId)
        {
            var s = service as IProjectService;
            return s.ExceededProjectMaximumPerProducer(userId);
        }

        #endregion

        #region Private methdos

        private void MapEntityTitles(ref Project entity, IEnumerable<ProjectTitleAppViewModel> titlesViewModel)
        {
            if (entity.Titles != null && entity.Titles.Any())
            {
                _projectTitleRepository.DeleteAll(entity.Titles);
            }

            if (titlesViewModel != null && titlesViewModel.Any())
            {
                var entitiesTitles = new List<ProjectTitle>();
                foreach (var titleViewModel in titlesViewModel)
                {
                    var entityTitle = titleViewModel.MapReverse();
                    var language = _languageRepository.Get(e => e.Code == titleViewModel.LanguageCode);
                    //entityTitle.SetLanguage(language);
                    entitiesTitles.Add(entityTitle);
                }
                //entity.SetTitles(entitiesTitles);
            }
        }

        private void MapEntityLogLines(ref Project entity, IEnumerable<ProjectLogLineAppViewModel> listViewModel)
        {
            //if (entity.LogLines != null && entity.LogLines.Any())
            //{
            //    _projectLogLineRepository.DeleteAll(entity.LogLines);
            //}

            //if (listViewModel != null && listViewModel.Any())
            //{
            //    var entitiesList = new List<ProjectLogLine>();
            //    foreach (var itemViewModel in listViewModel)
            //    {
            //        var entityItem = itemViewModel.MapReverse();
            //        var language = _languageRepository.Get(e => e.Code == itemViewModel.LanguageCode);
            //        entityItem.SetLanguage(language);
            //        entitiesList.Add(entityItem);
            //    }
            //    entity.SetLogLines(entitiesList);
            //}
        }

        private void MapEntitySummaries(ref Project entity, IEnumerable<ProjectSummaryAppViewModel> listViewModel)
        {
            //if (entity.Summaries != null && entity.Summaries.Any())
            //{
            //    _projectSummaryRepository.DeleteAll(entity.Summaries);
            //}

            //if (listViewModel != null && listViewModel.Any())
            //{
            //    var entitiesList = new List<ProjectSummary>();
            //    foreach (var itemViewModel in listViewModel)
            //    {
            //        var entityItem = itemViewModel.MapReverse();
            //        var language = _languageRepository.Get(e => e.Code == itemViewModel.LanguageCode);
            //        entityItem.SetLanguage(language);
            //        entitiesList.Add(entityItem);
            //    }
            //    entity.SetSummaries(entitiesList);
            //}
        }

        private void MapEntityProductionPlans(ref Project entity, IEnumerable<ProjectProductionPlanAppViewModel> listViewModel)
        {
            //if (entity.ProductionPlans != null && entity.ProductionPlans.Any())
            //{
            //    _projectProductionPlanRepository.DeleteAll(entity.ProductionPlans);
            //}

            //if (listViewModel != null && listViewModel.Any())
            //{
            //    var entitiesList = new List<ProjectProductionPlan>();
            //    foreach (var itemViewModel in listViewModel)
            //    {
            //        var entityItem = itemViewModel.MapReverse();
            //        var language = _languageRepository.Get(e => e.Code == itemViewModel.LanguageCode);
            //        entityItem.SetLanguage(language);
            //        entitiesList.Add(entityItem);
            //    }
            //    entity.SetProductionPlans(entitiesList);
            //}
        }

        private void MapEntityInterests(ref Project entity, int[] listIdsSelected)
        {
            //if (listIdsSelected != null && listIdsSelected.Any())
            //{
            //    if (entity.Interests != null && entity.Interests.Any())
            //    {
            //        _projectInterestRepository.DeleteAll(entity.Interests);
            //    }

            //    var entitiesList = new List<ProjectInterest>();
            //    var _interests = _interestRepository.GetAll();

            //    foreach (var IdSelected in listIdsSelected)
            //    {

            //        var entitySelected = new ProjectInterest(entity, _interests.FirstOrDefault(e => e.Id == IdSelected));
            //        entitiesList.Add(entitySelected);
            //    }

            //    entity.SetInterests(entitiesList);
            //}
        }

        private void MapEntityLinksImage(ref Project entity, IEnumerable<ProjectLinkImageAppViewModel> listViewModel)
        {
            //if (entity.LinksImage != null && entity.LinksImage.Any())
            //{
            //    _projectLinkImageRepository.DeleteAll(entity.LinksImage);
            //}

            //if (listViewModel != null && listViewModel.Any())
            //{
            //    var entitiesList = new List<ProjectLinkImage>();
            //    foreach (var itemViewModel in listViewModel.Where(e => !string.IsNullOrWhiteSpace(e.Value)))
            //    {
            //        var entityItem = itemViewModel.MapReverse();
            //        entitiesList.Add(entityItem);
            //    }
            //    entity.SetLinksImage(entitiesList);
            //}
        }

        private void MapEntityLinksTeaser(ref Project entity, IEnumerable<ProjectLinkTeaserAppViewModel> listViewModel)
        {
            //if (entity.LinksTeaser != null && entity.LinksTeaser.Any())
            //{
            //    _projectLinkTeaserRepository.DeleteAll(entity.LinksTeaser);
            //}

            //if (listViewModel != null && listViewModel.Any())
            //{
            //    var entitiesList = new List<ProjectLinkTeaser>();
            //    foreach (var itemViewModel in listViewModel.Where(e => !string.IsNullOrWhiteSpace(e.Value)))
            //    {
            //        var entityItem = itemViewModel.MapReverse();
            //        entitiesList.Add(entityItem);
            //    }
            //    entity.SetLinksTeaser(entitiesList);
            //}
        }

        private void MapEntityAdditionalInformations(ref Project entity, IEnumerable<ProjectAdditionalInformationAppViewModel> listViewModel)
        {
            //if (entity.AdditionalInformations != null && entity.AdditionalInformations.Any())
            //{
            //    _projectAdditionalInformationRepository.DeleteAll(entity.AdditionalInformations);
            //}

            //if (listViewModel != null && listViewModel.Any())
            //{
            //    var entitiesList = new List<ProjectAdditionalInformation>();
            //    foreach (var itemViewModel in listViewModel)
            //    {
            //        var entityItem = itemViewModel.MapReverse();
            //        var language = _languageRepository.Get(e => e.Code == itemViewModel.LanguageCode);
            //        entityItem.SetLanguage(language);
            //        entitiesList.Add(entityItem);
            //    }
            //    entity.SetAdditionalInformations(entitiesList);
            //}
        }

        private void LoadViewModelOptions(ProjectEditAppViewModel viewModel, Project entity)
        {
            var interest = _interestRepository.GetAll();
            var entityEvent = _eventRepository.Get(e => e.Name.Contains("2018"));

            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList());
            viewModel.Interests = ProjectInterestAppViewModel.MapList(interest, entity, entityEvent).ToList();
        }

        private Expression<Func<ProjectPlayer, bool>> GetPredicateByPlayer(ProjectPlayerFilterAppDto filter, IEnumerable<int> idsPlayersInclude)
        {
            var predicate = PredicateBuilder.New<ProjectPlayer>(true);

            var predicateSent = PredicateBuilder.New<ProjectPlayer>(false);

            predicateSent = predicateSent.Or(p => p.Sent);

            predicate = PredicateBuilder.And<ProjectPlayer>(predicate, predicateSent);


            var predicateIncludesIds = PredicateBuilder.New<ProjectPlayer>(false);

            predicateIncludesIds = predicateIncludesIds.Or(p => idsPlayersInclude.Contains(p.PlayerId));

            predicate = PredicateBuilder.And<ProjectPlayer>(predicate, predicateIncludesIds);


            if (filter != null)
            {
                if (filter.Players != null && filter.Players.Any(i => i != Guid.Empty))
                {
                    var predicatePlayer = PredicateBuilder.New<ProjectPlayer>(false);

                    predicatePlayer = predicatePlayer.Or(p => filter.Players.Any(f => f == p.Player.Uid));

                    predicate = PredicateBuilder.And<ProjectPlayer>(predicate, predicatePlayer);
                }

                if (filter.Genres != null && filter.Genres.Any(i => !string.IsNullOrWhiteSpace(i)))
                {
                    var predicateGenres = PredicateBuilder.New<ProjectPlayer>(false);

                    //predicateGenres = predicateGenres.Or(p => p.Project.Interests.Any(e => filter.Genres.Contains(e.Interest.Name)));

                    predicate = PredicateBuilder.And<ProjectPlayer>(predicate, predicateGenres);
                }

                if (filter.Status != null && filter.Status.Any(i => !string.IsNullOrWhiteSpace(i)) && filter.Status.Any(i => i != "All"))
                {
                    var predicateStatus = PredicateBuilder.New<ProjectPlayer>(false);

                    if (!filter.Status.Any(e => e == "OnEvaluation"))
                    {
                        predicateStatus = predicateStatus.Or(p => p.Evaluation != null && filter.Status.Contains(p.Evaluation.Status.Code));
                    }
                    else
                    {
                        predicateStatus = predicateStatus.Or(p => p.Evaluation == null);
                    }

                    predicate = PredicateBuilder.And<ProjectPlayer>(predicate, predicateStatus);
                }
            }

            return predicate;
        }

        public ViewModels.Admin.ProjectDetailAppViewModel GetPlayerSelectionByUidProject(Guid uid)
        {
            ViewModels.Admin.ProjectDetailAppViewModel vm = null;

            var s = service as IProjectService;
            var entity = s.GetWithPlayerSelection(uid);

            //if (entity != null && entity.PlayersRelated.Any())
            //{
            //    vm = new ViewModels.Admin.ProjectDetailAppViewModel(entity);
            //}

            return vm;
        }

        public AppValidationResult DeleteProjectPlayer(int id)
        {
            BeginTransaction();

            var entity = _projectPlayerRepository.Get(id);

            if (entity != null)
            {
                _projectPlayerRepository.Delete(entity);
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public AppValidationResult ResetEvaluation(int id)
        {
            BeginTransaction();

            var entity = _projectPlayerRepository.Get(id);

            if (entity != null)
            {
                _projectPlayerEvaluationRepository.Delete(entity.Evaluation);
                //entity.SetEvaluation(null);
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }




        #endregion

        public bool PreRegistrationProducerDisabled()
        {
            return _systemParameterRepository.Get<bool>(Infra.CrossCutting.SystemParameter.SystemParameterCodes.ProjectsProducerPreRegistrationDisabled);
        }

        public bool RegistrationDisabled()
        {
            return _systemParameterRepository.Get<bool>(Infra.CrossCutting.SystemParameter.SystemParameterCodes.ProjectsRegisterDisabled);
        }

        public bool SendToPlayersDisabled()
        {
            return _systemParameterRepository.Get<bool>(Infra.CrossCutting.SystemParameter.SystemParameterCodes.ProjectsSendToPlayersDisabled);
        }

        public IEnumerable<ProjectOptionAppViewModel> GetAllOption(ProjectOptionFilterAppViewModel filter)
        {
            var s = service as IProjectService;

            var entities = s.GetAllOption(GetPredicate(filter));

            if (entities != null && entities.Any())
            {
                return ProjectOptionAppViewModel.MapList(entities);
            }

            return new List<ProjectOptionAppViewModel>();
        }

        private Expression<Func<Project, bool>> GetPredicate(ProjectOptionFilterAppViewModel filter)
        {
            var predicate = PredicateBuilder.New<Project>(true);

            if (!string.IsNullOrWhiteSpace(filter.Term))
            {
                var predicateTerm = PredicateBuilder.New<Project>(false);

                //predicateTerm = predicateTerm.Or(p => p.Titles.Any(t => t.Value.Contains(filter.Term)) || p.Producer.Name.Contains(filter.Term));
                //predicateTerm = predicateTerm.Or(p => p.Producer.Name.Contains(filter.Term));

                predicate = PredicateBuilder.And<Project>(predicate, predicateTerm);
            }

            return predicate;
        }

        public ExcelPackage DownloadExcelProject()
        {
            try
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                ExcelPackage excelFile = new ExcelPackage();

                ExcelWorksheet worksheetPlayers = excelFile.Workbook.Worksheets.Add(Labels.Players);

                //var emailsHidden = _systemParameterRepository.Get<string>(SystemParameterCodes.NetworkRio2CEmailsThatShouldBeHidden);

                var projects = GetAllExcel(); /*_collaboratorService.GetOptionsChat(0);*/

                int row = 1;
                int column = 1;

                int rowProjectProducer = 1;
                string producerName = "";

                // Config reader row
                worksheetPlayers.Cells[row, column, row, column + 20].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                worksheetPlayers.Cells[row, column, row, column + 20].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheetPlayers.Cells[row, column, row, column + 20].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheetPlayers.Cells[row, column, row, column + 20].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheetPlayers.Cells[row, column, row, column + 20].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheetPlayers.Cells[row, column, row, column + 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheetPlayers.Cells[row, column, row, column + 20].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
                worksheetPlayers.Cells[row, column, row, column + 20].Style.Font.Bold = true;

                worksheetPlayers.Column(1).Width = 40; //QTD Produtora
                worksheetPlayers.Column(2).Width = 40; //QTD Projetos por Produtora
                worksheetPlayers.Column(3).Width = 40; //Id Projeto
                worksheetPlayers.Column(4).Width = 40; //Produtora
                worksheetPlayers.Column(5).Width = 40; //Nome
                worksheetPlayers.Column(6).Width = 40; //E-mail
                worksheetPlayers.Column(7).Width = 40; //Título Português
                worksheetPlayers.Column(8).Width = 40; //Título Inglês
                worksheetPlayers.Column(9).Width = 40; //Pitching
                worksheetPlayers.Column(10).Width = 40; //Player Selecionado
                worksheetPlayers.Column(11).Width = 40; //Data do Cadastro
                worksheetPlayers.Column(12).Width = 40; //Data do Envio
                worksheetPlayers.Column(13).Width = 40; //Plataformas | Platfoms
                worksheetPlayers.Column(14).Width = 40; //Status do Projeto | Project Status
                worksheetPlayers.Column(15).Width = 40; //Está Buscando | Looking For
                worksheetPlayers.Column(16).Width = 40; //Formato | Format
                worksheetPlayers.Column(17).Width = 40; //Gênero | Genre
                worksheetPlayers.Column(18).Width = 40; //Subgênero | Sub-genre
                worksheetPlayers.Column(19).Width = 40; //Público alvo | Target audiencie
                worksheetPlayers.Column(20).Width = 140; //Sinopse Português
                worksheetPlayers.Column(21).Width = 140; //Sinopse Inglês
                //worksheetPlayers.Column(22).Width = 40; //Status
                //worksheetPlayers.Column(23).Width = 40; //Responsável


                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", "QTD", Labels.ProducerSingle);
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1} {2} {3}", "QTD", Labels.Projects, "Por", Labels.ProducerSingle);
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", "Id", Labels.Project);
                worksheetPlayers.Cells[row, column++].Value = Labels.ProducerSingle;
                worksheetPlayers.Cells[row, column++].Value = Labels.Name;
                worksheetPlayers.Cells[row, column++].Value = Labels.Email;
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Title, Labels.Portuguese);
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Title, Labels.English);
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0}", "Pitching");
                worksheetPlayers.Cells[row, column++].Value = Labels.PlayersSelectedToEvaluation;
                worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Date, "do Cadastro");
                worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                worksheetPlayers.Cells[row, column++].Value = Labels.SendDate;
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Platforms, "| Platforms");
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.ProjectStatus, "| Project Status");
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.MarketLookingFor, "| Looking For");
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Format, "| Format");
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Genre, "| Genre");
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.SubGenre, "| Sub-genre");
                worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.TargetAudience, "| Target audience");
                worksheetPlayers.Cells[row, column++].Value = "Sinopse Português";
                worksheetPlayers.Cells[row, column++].Value = "Sinopse Inglês";
                //worksheetPlayers.Cells[row, column++].Value = "Status";
                //worksheetPlayers.Cells[row, column++].Value = "Responsável";


                row++;

                foreach (var project in projects)
                {
                    column = 1;

                    worksheetPlayers.Cells[row, column++].Value = row - 1;                                       //QTD Produtora
                    if (string.IsNullOrEmpty(producerName) || producerName != project.ProducerName)
                    {
                        rowProjectProducer = 1;
                        producerName = project.ProducerName;
                        worksheetPlayers.Cells[row, column++].Value = rowProjectProducer;                                           //QTD Projetos por Produtora
                        rowProjectProducer++;
                    }
                    else
                    {
                        worksheetPlayers.Cells[row, column++].Value = rowProjectProducer;                                           //QTD Projetos por Produtora
                        rowProjectProducer++;
                    }

                    worksheetPlayers.Cells[row, column++].Value = project.Id;                                   //Id Projeto
                    worksheetPlayers.Cells[row, column++].Value = project.ProducerName;                         //Produtora
                    worksheetPlayers.Cells[row, column++].Value = project.Name;                                 //Nome
                    worksheetPlayers.Cells[row, column++].Value = project.Email;                                //E-mail
                    worksheetPlayers.Cells[row, column++].Value = project.TitlePt;                              //Título Português
                    worksheetPlayers.Cells[row, column++].Value = project.TitleEn;                              //Título Inglês
                    worksheetPlayers.Cells[row, column++].Value = (project.Pitching == true ? "SIM" : "NÃO");   //Pitching
                    worksheetPlayers.Cells[row, column++].Value = project.PlayerStatus;  //project.RelatedPlayers == null ? "" : string.Join(", ", project.RelatedPlayers.Select(x => x.Name)); //Player Selecionado
                    worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    worksheetPlayers.Cells[row, column++].Value = project.CreationDate.Date;                    //Data do Cadastro
                    worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    worksheetPlayers.Cells[row, column++].Value = project.DateSending;//project.RelatedPlayers.Select(;                                           //Data do Envio
                    worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Plataformas | Platforms").Select(x => x.InterestName)); //Plataformas | Platfoms                
                    worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Status do projeto | Project Status").Select(x => x.InterestName)); //Status do Projeto | Project Status
                    worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Está buscando | Looking For").Select(x => x.InterestName)); //Está Buscando | Looking For
                    worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Formato | Format").Select(x => x.InterestName)); //Formato | Format
                    worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Gênero | Genre").Select(x => x.InterestName)); //Gênero | Genre
                    worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Subgênero | Sub-genre").Select(x => x.InterestName)); //Subgênero | Sub-genre
                    worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Público alvo | Target audience").Select(x => x.InterestName)); //Público alvo | Target audiencie
                    worksheetPlayers.Cells[row, column++].Value = project.SinopsePt == null ? "" : Regex.Replace(project.SinopsePt, @"< (.|\n) *?>", string.Empty);
                    worksheetPlayers.Cells[row, column++].Value = project.SinopseEn == null ? "" : Regex.Replace(project.SinopseEn, @"< (.|\n) *?>", string.Empty);
                    //worksheetPlayers.Cells[row, column++].Value = project.Status == null ? "" : project.Status;
                    //worksheetPlayers.Cells[row, column++].Value = project.ResponsavelAprovacao  == null ? "" : project.ResponsavelAprovacao;

                    row++;
                }

                ExcelWorksheet worksheetProducers = excelFile.Workbook.Worksheets.Add(Labels.Producers);

                return excelFile;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ExcelPackage DownloadExcel()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            ExcelPackage excelFile = new ExcelPackage();

            ExcelWorksheet worksheetPlayers = excelFile.Workbook.Worksheets.Add(Labels.Players);

            //var emailsHidden = _systemParameterRepository.Get<string>(SystemParameterCodes.NetworkRio2CEmailsThatShouldBeHidden);

            var projects = GetDataExcel(); /*_collaboratorService.GetOptionsChat(0);*/

            int row = 1;
            int column = 1;

            // Config reader row
            worksheetPlayers.Cells[row, column, row, column + 21].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheetPlayers.Cells[row, column, row, column + 21].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheetPlayers.Cells[row, column, row, column + 21].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheetPlayers.Cells[row, column, row, column + 21].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheetPlayers.Cells[row, column, row, column + 21].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheetPlayers.Cells[row, column, row, column + 21].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetPlayers.Cells[row, column, row, column + 21].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
            worksheetPlayers.Cells[row, column, row, column + 21].Style.Font.Bold = true;

            worksheetPlayers.Column(1).Width = 40; //Produtor_ID
            worksheetPlayers.Column(2).Width = 40; //Produtor_Name
            worksheetPlayers.Column(3).Width = 40; //Produtor_Fantasia
            worksheetPlayers.Column(4).Width = 40; //Produtor_CNPJ
            worksheetPlayers.Column(5).Width = 40; // /
            worksheetPlayers.Column(6).Width = 40; //Project
            worksheetPlayers.Column(7).Width = 40; //NomeProjeto_Inglês
            worksheetPlayers.Column(8).Width = 40; //NomeProjeto_Português
            worksheetPlayers.Column(9).Width = 40; // /
            worksheetPlayers.Column(10).Width = 40; //PlayerName
            worksheetPlayers.Column(11).Width = 40; //PlayerCNPJ
            worksheetPlayers.Column(12).Width = 40; //Player_Fantasia
            worksheetPlayers.Column(13).Width = 40; // /
            worksheetPlayers.Column(14).Width = 40; //Projeto_Enviado
            worksheetPlayers.Column(15).Width = 40; //Data_Projeto_Salvo
            worksheetPlayers.Column(16).Width = 40; //Data_ProjetoEnviado
            worksheetPlayers.Column(17).Width = 40; //Avaliação_ID
            worksheetPlayers.Column(18).Width = 40; //StatusId
            worksheetPlayers.Column(19).Width = 40; //Reason
            worksheetPlayers.Column(20).Width = 40; //Code
            worksheetPlayers.Column(21).Width = 40; //Status


            worksheetPlayers.Cells[row, column++].Value = "Produtor_ID";
            worksheetPlayers.Cells[row, column++].Value = "Produtor_Name";
            worksheetPlayers.Cells[row, column++].Value = "Produtor_Fantasia";
            worksheetPlayers.Cells[row, column++].Value = "Produtor_CNPJ";
            worksheetPlayers.Cells[row, column++].Value = "/";
            worksheetPlayers.Cells[row, column++].Value = "Project";
            worksheetPlayers.Cells[row, column++].Value = "NomeProjeto_Inglês";
            worksheetPlayers.Cells[row, column++].Value = "NomeProjeto_Português";
            worksheetPlayers.Cells[row, column++].Value = "/";
            worksheetPlayers.Cells[row, column++].Value = "PlayerName";
            worksheetPlayers.Cells[row, column++].Value = "PlayerCNPJ";
            worksheetPlayers.Cells[row, column++].Value = "Player_Fantasia";
            worksheetPlayers.Cells[row, column++].Value = "/";
            worksheetPlayers.Cells[row, column++].Value = "Projeto_Enviado";
            worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            worksheetPlayers.Cells[row, column++].Value = "Data_Projeto_Salvo";
            worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            worksheetPlayers.Cells[row, column++].Value = "Data_ProjetoEnviado";
            worksheetPlayers.Cells[row, column++].Value = "Avaliação_ID";
            worksheetPlayers.Cells[row, column++].Value = "StatusId";
            worksheetPlayers.Cells[row, column++].Value = "Reason";
            worksheetPlayers.Cells[row, column++].Value = "Code";
            worksheetPlayers.Cells[row, column++].Value = "Status";

            row++;

            foreach (var project in projects)
            {
                column = 1;

                worksheetPlayers.Cells[row, column++].Value = project.ProducerId;                           //Produtor_ID
                worksheetPlayers.Cells[row, column++].Value = project.ProducerName;                         //Produtor_Name
                worksheetPlayers.Cells[row, column++].Value = project.ProducerTradeName;                    //Produtor_Fantasia
                worksheetPlayers.Cells[row, column++].Value = TextClean(project.ProducerCnpj);                         //Produtor_CNPJ
                worksheetPlayers.Cells[row, column++].Value = "/";                                          // /
                worksheetPlayers.Cells[row, column++].Value = project.Id;                                   //Project
                worksheetPlayers.Cells[row, column++].Value = project.TitleEn;                              //NomeProjeto_Inglês
                worksheetPlayers.Cells[row, column++].Value = project.TitlePt;                              //NomeProjeto_Português
                worksheetPlayers.Cells[row, column++].Value = "/";                                          // /
                worksheetPlayers.Cells[row, column++].Value = project.RelatedPlayers == null ? "" : project.RelatedPlayers.Select(x => x.Name).FirstOrDefault();        // PlayerName
                worksheetPlayers.Cells[row, column++].Value = project.RelatedPlayers == null ? "" : TextClean(project.RelatedPlayers.Select(x => x.Cnpj).FirstOrDefault());        //PlayerCNPJ
                worksheetPlayers.Cells[row, column++].Value = project.RelatedPlayers == null ? "" : project.RelatedPlayers.Select(x => x.TradeName).FirstOrDefault();   //Player_Fantasia
                worksheetPlayers.Cells[row, column++].Value = "/";                                          // /
                worksheetPlayers.Cells[row, column++].Value = project.Sent == true ? "Sim" : "Não";                                 //Projeto_Enviado

                worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                worksheetPlayers.Cells[row, column++].Value = project.DateSaved;                            //Data_Projeto_Salvo
                worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                worksheetPlayers.Cells[row, column++].Value = project.DateSending;                          //Data_ProjetoEnviado
                worksheetPlayers.Cells[row, column++].Value = ""; //Avaliação_ID
                worksheetPlayers.Cells[row, column++].Value = project.RelatedPlayers == null ? "" : project.RelatedPlayers.Select(x => x.Id).FirstOrDefault().ToString(); //StatusId
                worksheetPlayers.Cells[row, column++].Value = project.RelatedPlayers == null ? "" : project.RelatedPlayers.Select(x => x.Reason).FirstOrDefault();        //Reason 
                worksheetPlayers.Cells[row, column++].Value = project.RelatedPlayers == null ? "" : project.RelatedPlayers.Select(x => x.StatusCode).FirstOrDefault();    //Code
                worksheetPlayers.Cells[row, column++].Value = project.RelatedPlayers == null ? "" : project.RelatedPlayers.Select(x => x.Status).FirstOrDefault();        //Status

                row++;
            }


            ExcelWorksheet worksheetProducers = excelFile.Workbook.Worksheets.Add(Labels.Producers);

            return excelFile;
        }

        public ExcelPackage DownloadExcelProjectPitching()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            ExcelPackage excelFile = new ExcelPackage();

            ExcelWorksheet worksheetPlayers = excelFile.Workbook.Worksheets.Add(Labels.Players);

            //var emailsHidden = _systemParameterRepository.Get<string>(SystemParameterCodes.NetworkRio2CEmailsThatShouldBeHidden);

            var projects = GetAllExcel(); /*_collaboratorService.GetOptionsChat(0);*/
            projects = projects.Where(x => x.Pitching == true);

            int row = 1;
            int column = 1;

            int rowProjectProducer = 1;
            string producerName = "";

            // Config reader row
            worksheetPlayers.Cells[row, column, row, column + 19].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            worksheetPlayers.Cells[row, column, row, column + 19].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheetPlayers.Cells[row, column, row, column + 19].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheetPlayers.Cells[row, column, row, column + 19].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheetPlayers.Cells[row, column, row, column + 19].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheetPlayers.Cells[row, column, row, column + 19].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetPlayers.Cells[row, column, row, column + 19].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
            worksheetPlayers.Cells[row, column, row, column + 19].Style.Font.Bold = true;

            worksheetPlayers.Column(1).Width = 40; //QTD Produtora
            worksheetPlayers.Column(2).Width = 40; //QTD Projetos por Produtora
            worksheetPlayers.Column(3).Width = 40; //Id Projeto
            worksheetPlayers.Column(4).Width = 40; //Produtora
            worksheetPlayers.Column(5).Width = 40; //Nome
            worksheetPlayers.Column(6).Width = 40; //E-mail
            worksheetPlayers.Column(7).Width = 40; //Título Português
            worksheetPlayers.Column(8).Width = 40; //Título Inglês
            worksheetPlayers.Column(9).Width = 40; //Pitching
            worksheetPlayers.Column(10).Width = 40; //Player Selecionado
            worksheetPlayers.Column(11).Width = 40; //Data do Cadastro
            worksheetPlayers.Column(12).Width = 40; //Data do Envio
            worksheetPlayers.Column(13).Width = 40; //Plataformas | Platfoms
            worksheetPlayers.Column(14).Width = 40; //Status do Projeto | Project Status
            worksheetPlayers.Column(15).Width = 40; //Está Buscando | Looking For
            worksheetPlayers.Column(16).Width = 40; //Formato | Format
            worksheetPlayers.Column(17).Width = 40; //Gênero | Genre
            worksheetPlayers.Column(18).Width = 40; //Subgênero | Sub-genre
            worksheetPlayers.Column(19).Width = 40; //Público alvo | Target audiencie
            worksheetPlayers.Column(20).Width = 140; //Sinopse Português
            worksheetPlayers.Column(21).Width = 140; //Sinopse Inglês


            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", "QTD", Labels.ProducerSingle);
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1} {2} {3}", "QTD", Labels.Projects, "Por", Labels.ProducerSingle);
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", "Id", Labels.Project);
            worksheetPlayers.Cells[row, column++].Value = Labels.ProducerSingle;
            worksheetPlayers.Cells[row, column++].Value = Labels.Name;
            worksheetPlayers.Cells[row, column++].Value = Labels.Email;
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Title, Labels.Portuguese);
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Title, Labels.English);
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0}", "Pitching");
            worksheetPlayers.Cells[row, column++].Value = Labels.PlayersSelectedToEvaluation;
            worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Date, "do Cadastro");
            worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            worksheetPlayers.Cells[row, column++].Value = Labels.SendDate;
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Platforms, "| Platforms");
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.ProjectStatus, "| Project Status");
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.MarketLookingFor, "| Looking For");
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Format, "| Format");
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Genre, "| Genre");
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.SubGenre, "| Sub-genre");
            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.TargetAudience, "| Target audience");
            worksheetPlayers.Cells[row, column++].Value = "Sinopse Português";
            worksheetPlayers.Cells[row, column++].Value = "Sinopse Inglês";

            row++;

            foreach (var project in projects)
            {
                column = 1;

                worksheetPlayers.Cells[row, column++].Value = row - 1;                                       //QTD Produtora
                if (string.IsNullOrEmpty(producerName) || producerName != project.ProducerName)
                {
                    rowProjectProducer = 1;
                    producerName = project.ProducerName;
                    worksheetPlayers.Cells[row, column++].Value = rowProjectProducer;                                           //QTD Projetos por Produtora
                    rowProjectProducer++;
                }
                else
                {
                    worksheetPlayers.Cells[row, column++].Value = rowProjectProducer;                                           //QTD Projetos por Produtora
                    rowProjectProducer++;
                }

                worksheetPlayers.Cells[row, column++].Value = project.Id;                                   //Id Projeto
                worksheetPlayers.Cells[row, column++].Value = project.ProducerName;                         //Produtora
                worksheetPlayers.Cells[row, column++].Value = project.Name;                                 //Nome
                worksheetPlayers.Cells[row, column++].Value = project.Email;                                //E-mail
                worksheetPlayers.Cells[row, column++].Value = project.TitlePt;                              //Título Português
                worksheetPlayers.Cells[row, column++].Value = project.TitleEn;                              //Título Inglês
                worksheetPlayers.Cells[row, column++].Value = (project.Pitching == true ? "SIM" : "NÃO");   //Pitching
                worksheetPlayers.Cells[row, column++].Value = project.PlayerStatus;  //project.RelatedPlayers == null ? "" : string.Join(", ", project.RelatedPlayers.Select(x => x.Name)); //Player Selecionado
                worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                worksheetPlayers.Cells[row, column++].Value = project.CreationDate.Date;                    //Data do Cadastro
                worksheetPlayers.Cells[row, column].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                worksheetPlayers.Cells[row, column++].Value = project.DateSending;//project.RelatedPlayers.Select(;                                           //Data do Envio
                worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Plataformas | Platforms").Select(x => x.InterestName)); //Plataformas | Platfoms                
                worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Status do projeto | Project Status").Select(x => x.InterestName)); //Status do Projeto | Project Status
                worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Está buscando | Looking For").Select(x => x.InterestName)); //Está Buscando | Looking For
                worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Formato | Format").Select(x => x.InterestName)); //Formato | Format
                worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Gênero | Genre").Select(x => x.InterestName)); //Gênero | Genre
                worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Subgênero | Sub-genre").Select(x => x.InterestName)); //Subgênero | Sub-genre
                worksheetPlayers.Cells[row, column++].Value = project.Interest == null ? "" : string.Join(", ", project.Interest.Where(x => x.Interest.Group.Name == "Público alvo | Target audience").Select(x => x.InterestName)); //Público alvo | Target audiencie
                worksheetPlayers.Cells[row, column++].Value = project.SinopsePt == null ? "" : Regex.Replace(project.SinopsePt, @"< (.|\n) *?>", string.Empty);
                worksheetPlayers.Cells[row, column++].Value = project.SinopseEn == null ? "" : Regex.Replace(project.SinopseEn, @"< (.|\n) *?>", string.Empty);

                row++;
            }


            ExcelWorksheet worksheetProducers = excelFile.Workbook.Worksheets.Add(Labels.Producers);

            return excelFile;
        }

        public IEnumerable<ProjectExcelItemListAppViewModel> GetAllExcel()
        {
            var s = service as IProjectService;
            var entities = s.GetAllExcel();
            if (entities != null && entities.Any())
            {
                IEnumerable<ProjectExcelItemListAppViewModel> results = ProjectExcelItemListAppViewModel.MapList(entities);
                return results.ToList();
            }

            return new List<ProjectExcelItemListAppViewModel>() { };
        }

        public IEnumerable<ProjectExcelListAppViewModel> GetDataExcel()
        {
            var s = service as IProjectService;
            var entities = s.GetDataExcel();
            if (entities != null && entities.Any())
            {
                IEnumerable<ProjectExcelListAppViewModel> results = ProjectExcelListAppViewModel.MapList(entities);
                return results.ToList();
            }

            return new List<ProjectExcelListAppViewModel>() { };
        }

        private string TextClean(string strIn)
        {
            try
            {
                if (strIn != null)
                {
                    return Regex.Replace(strIn, @"[^\w\@]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));

                }
                return "";
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

    }
}

