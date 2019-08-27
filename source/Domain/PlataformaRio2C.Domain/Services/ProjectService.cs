// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="ProjectService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Services
{
    /// <summary>ProjectService</summary>
    public class ProjectService : Service<Project>, IProjectService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IProjectPlayerRepository _projectPlayerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IProjectPlayerEvaluationRepository _projectPlayerEvaluationRepository;

        public ProjectService(IProjectRepository repository, IRepositoryFactory repositoryFactory)
            : base(repository)
        {
            _playerRepository = repositoryFactory.PlayerRepository;
            _userRepository = repositoryFactory.UserRepository;
            _projectPlayerRepository = repositoryFactory.ProjectPlayerRepository;
            _projectStatusRepository = repositoryFactory.ProjectStatusRepository;
            _projectPlayerEvaluationRepository = repositoryFactory.ProjectPlayerEvaluationRepository;
            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
            _producerRepository = repositoryFactory.ProducerRepository;
        }

        public override ValidationResult Create(Project entity)
        {
            if (MaxCountProjectInValid(entity, "Create"))
            {
                return _validationResult;
            }

            if (entity.Producer.Projects.Any(e => e.Titles.Any(t => !string.IsNullOrWhiteSpace(t.Value) && entity.Titles.Where(et => !string.IsNullOrWhiteSpace(et.Value)).Select(et => et.Value.Trim().ToLower()).Contains(t.Value.Trim().ToLower()))))
            {
                _validationResult.Add(new ValidationError(Messages.ThereIsalreadyProjectRegisteredWithThisTitle, new string[] { "ProjectSubmitted" }));
                return _validationResult;
            }

            return base.Create(entity);
        }

        public ValidationResult UpdateByAdmin(Project entity)
        {
           

            if (entity.Producer.Projects.Any(e => e.Titles.Any(t => !string.IsNullOrWhiteSpace(t.Value) && entity.Titles.Where(et => !string.IsNullOrWhiteSpace(et.Value)).Select(et => et.Value.Trim().ToLower()).Contains(t.Value.Trim().ToLower())) && e.Id != entity.Id))
            {
                _validationResult.Add(new ValidationError(Messages.ThereIsalreadyProjectRegisteredWithThisTitle, new string[] { "ProjectSubmitted" }));
                return _validationResult;
            }

            if (MaxCountProjectInValid(entity, "Update"))
            {
                return _validationResult;
            }

            return base.Update(entity);
        }

        public override ValidationResult Update(Project entity)
        {
            if (entity.PlayersRelated.Any(e => e.Sent))
            {
                _validationResult.Add(new ValidationError(Messages.CanNotEditASubmittedProjectForPlayerEvaluation, new string[] { "ProjectSubmitted" }));

                return _validationResult;
            }

            if (entity.Producer.Projects.Any(e => e.Titles.Any(t => !string.IsNullOrWhiteSpace(t.Value) && entity.Titles.Where(et => !string.IsNullOrWhiteSpace(et.Value)).Select(et => et.Value.Trim().ToLower()).Contains(t.Value.Trim().ToLower())) && e.Id != entity.Id))
            {
                _validationResult.Add(new ValidationError(Messages.ThereIsalreadyProjectRegisteredWithThisTitle, new string[] { "ProjectSubmitted" }));
                return _validationResult;
            }

            if (MaxCountProjectInValid(entity, "Update"))
            {
                return _validationResult;
            }

            return base.Update(entity);
        }

        public ValidationResult SavePlayerSelection(Project entity, Guid uidPlayer, int userId)
        {
            var entityPlayer = _playerRepository.GetSimple(e => e.Uid == uidPlayer);
            var entityUser = _userRepository.Get(e => e.Id == userId);
            CheckPreValidationBySelectionPlayer(entityPlayer, entityUser);
            CheckMaxPlayerBySelection(entity);

            if (_validationResult.IsValid)
            {
                var entityProjectPlayer = new ProjectPlayer(entity, entityPlayer);
                entityProjectPlayer.SetSavedUser(entityUser);

                if (entity.PlayersRelated != null)
                {
                    entity.PlayersRelated.Add(entityProjectPlayer);
                }
            }

            return _validationResult;
        }

        public ValidationResult RemovePlayerSelection(Project entity, Guid uidPlayer, int userId)
        {
            var entityPlayer = _playerRepository.GetSimple(e => e.Uid == uidPlayer);
            var entityUser = _userRepository.Get(e => e.Id == userId);
            CheckPreValidationBySelectionPlayer(entityPlayer, entityUser);

            if (_validationResult.IsValid)
            {
                var entityProjectPlayer = entity.PlayersRelated.FirstOrDefault(e => e.PlayerId == entityPlayer.Id);
                _projectPlayerRepository.Delete(entityProjectPlayer);

                if (entityProjectPlayer != null)
                {
                    entity.PlayersRelated.Remove(entityProjectPlayer);
                }
            }

            return _validationResult;
        }

        public ValidationResult SendToPlayers(Project entity, Guid[] uidsPlayers, int userId)
        {
            if (entity.PlayersRelated.Any(e => e.Sent))
            {
                _validationResult.Add(Messages.ThisProjectHasAlreadyBeenSubmittedForTheEvaluationOfPlayers);
                return _validationResult;
            }

            var r = _repository as IProjectRepository;
            var numberMaxPlayerPerProject = r.GetMaxNumberPlayerPerProject();
            if (uidsPlayers != null && uidsPlayers.Count() > numberMaxPlayerPerProject)
            {
                _validationResult.Add(string.Format(Messages.YouCanSendUpToXPlayers, numberMaxPlayerPerProject));
            }

            var entityUser = _userRepository.Get(e => e.Id == userId);
            if (entityUser == null)
            {
                _validationResult.Add(Messages.UserNotFound);
            }

            var playersSavedNotSubmit = entity.PlayersRelated.Select(e => e.Player.Uid).Except(uidsPlayers);
            var playersSubmitNotSaved = uidsPlayers.Except(entity.PlayersRelated.Select(e => e.Player.Uid));
            if (playersSavedNotSubmit != null && playersSavedNotSubmit.Any())
            {
                foreach (var itemUid in playersSavedNotSubmit)
                {
                    var entityProjectPlayer = entity.PlayersRelated.FirstOrDefault(e => e.Player.Uid == itemUid);

                    _projectPlayerRepository.Delete(entityProjectPlayer);

                    if (entityProjectPlayer != null)
                    {
                        entity.PlayersRelated.Remove(entityProjectPlayer);
                    }
                }
            }

            if (playersSubmitNotSaved != null && playersSubmitNotSaved.Any())
            {
                foreach (var itemUid in playersSubmitNotSaved)
                {

                    var entityPlayer = _playerRepository.Get(e => e.Uid == itemUid);
                    if (entityPlayer == null)
                    {
                        _validationResult.Add(Messages.PlayerNotFound);
                    }

                    var entityProjectPlayer = new ProjectPlayer(entity, entityPlayer);
                    entityProjectPlayer.SetSavedUser(entityUser);

                    if (entity.PlayersRelated != null)
                    {
                        entity.PlayersRelated.Add(entityProjectPlayer);
                    }
                }
            }

            foreach (var playerRelated in entity.PlayersRelated)
            {
                playerRelated.SetSendingUser(entityUser);
            }

            return _validationResult;
        }

        public Project GetSimpleWithProducer(Expression<Func<Project, bool>> filter)
        {
            var r = _repository as IProjectRepository;
            return r.GetSimpleWithProducer(filter);
        }

        public Project GetSimpleWithPlayers(Expression<Func<Project, bool>> filter)
        {
            var r = _repository as IProjectRepository;
            return r.GetSimpleWithPlayers(filter);
        }

        public ValidationResult AcceptByPlayer(Project entity, Guid uidPlayer, int userId)
        {
            var r = _repository as IProjectRepository;
            var dateMaximumDateForEvaluation = Convert.ToDateTime(r.GetMaximumDateForEvaluation());
            if (dateMaximumDateForEvaluation < DateTime.Now)
            {
                _validationResult.Add(Messages.EvaluationPeriodClosed);
            }
            else
            {
                var status = _projectStatusRepository.Get(e => e.Code == "Accepted");
                var user = _userRepository.Get(userId);
                var playerRelated = entity.PlayersRelated.FirstOrDefault(e => e.Player.Uid == uidPlayer);

                if (user != null && playerRelated != null && status != null)
                {
                    var existEvaluation = _projectPlayerEvaluationRepository.Get(e => e.ProjectPlayerId == playerRelated.Id);
                    ProjectPlayerEvaluation projectPlayerEvaluation = null;

                    if (existEvaluation == null)
                    {
                        projectPlayerEvaluation = new ProjectPlayerEvaluation(playerRelated, status, user);

                        _projectPlayerEvaluationRepository.Create(projectPlayerEvaluation);
                    }
                    else
                    {

                        projectPlayerEvaluation = existEvaluation;
                        projectPlayerEvaluation.SetProjectStatus(status);
                        _projectPlayerEvaluationRepository.Update(projectPlayerEvaluation);

                    }



                    playerRelated.SetEvaluation(projectPlayerEvaluation);
                }
            }

            return _validationResult;
        }

        public ValidationResult RejectByPlayer(Project entity, Guid uidPlayer, int userId, string reason)
        {
            var r = _repository as IProjectRepository;
            var dateMaximumDateForEvaluation = Convert.ToDateTime(r.GetMaximumDateForEvaluation());
            if (dateMaximumDateForEvaluation < DateTime.Now)
            {
                _validationResult.Add(Messages.EvaluationPeriodClosed);
            }
            else
            {
                var status = _projectStatusRepository.Get(e => e.Code == "Rejected");
                var user = _userRepository.Get(userId);
                var playerRelated = entity.PlayersRelated.FirstOrDefault(e => e.Player.Uid == uidPlayer);

                if (user != null && playerRelated != null && status != null)
                {
                    var existEvaluation = _projectPlayerEvaluationRepository.Get(e => e.ProjectPlayerId == playerRelated.Id);
                    ProjectPlayerEvaluation projectPlayerEvaluation;

                    if (existEvaluation == null)
                    {
                        projectPlayerEvaluation = new ProjectPlayerEvaluation(playerRelated, status, user);
                        projectPlayerEvaluation.SetReason(reason);

                        _projectPlayerEvaluationRepository.Create(projectPlayerEvaluation);
                    }
                    else
                    {
                        projectPlayerEvaluation = existEvaluation;
                        projectPlayerEvaluation.SetProjectStatus(status);
                        projectPlayerEvaluation.SetReason(reason);

                        _projectPlayerEvaluationRepository.Update(projectPlayerEvaluation);
                    }

                    var ValidationEntity = projectPlayerEvaluation as IEntity;
                    if (ValidationEntity != null && !ValidationEntity.IsValid())
                        ValidationResult.Add(ValidationEntity.ValidationResult);

                    playerRelated.SetEvaluation(projectPlayerEvaluation);
                }
            }

            return _validationResult;
        }
        public IEnumerable<Project> GetAllByAdmin()
        {
            var r = _repository as IProjectRepository;

            return r.GetAllByAdmin();
        }

        public IEnumerable<Project> GetAllExcel()
        {
            var r = _repository as IProjectRepository;

            return r.GetAllExcel();
        }

        public IEnumerable<Project> GetDataExcel()
        {
            var r = _repository as IProjectRepository;

            return r.GetDataExcel();
        }

        public bool ExceededProjectMaximumPerProducer(int userId)
        {
            var collaborator = _collaboratorRepository.GetWithProducerByUserId(userId);
            //if (collaborator != null && collaborator.ProducersEvents != null)
            //{
            //    var producerId = collaborator.ProducersEvents.Where(e => e.Edition.Name.Contains("2018")).Select(e => e.ProducerId).FirstOrDefault();
            //    var producer = _producerRepository.Get(producerId);

            //    var r = _repository as IProjectRepository;
            //    var numberMaxProject = r.GetMaxNumberProjectPerProducer();

            //    if (producer.Projects != null && producer.Projects.Any())
            //    {
            //        return producer.Projects.Count >= numberMaxProject;
            //    }
            //}

            return false;
        }

        public Project GetWithPlayerSelection(Guid uid)
        {
            var r = _repository as IProjectRepository;

            return r.GetWithPlayerSelection(uid);
        }


        #region private methods

        private bool MaxCountProjectInValid(Project entity, string operation)
        {
            var r = _repository as IProjectRepository;
            var numberMaxProject = r.GetMaxNumberProjectPerProducer();
            if (entity.Producer != null && entity.Producer.Projects != null)
            {
                int countProjects = entity.Producer.Projects.Count;

                if (operation == "Create" && countProjects >= numberMaxProject)
                {
                    _validationResult.Add(new ValidationError(string.Format(Messages.ProducerMaxProjects, numberMaxProject), new string[] { "ProducerMaxProject" }));
                    return true;
                }

                if (operation == "Update" && countProjects > numberMaxProject)
                {
                    _validationResult.Add(new ValidationError(string.Format(Messages.ProducerMaxProjects, numberMaxProject), new string[] { "ProducerMaxProject" }));
                    return true;
                }
            }

            return false;
        }

        private void CheckPreValidationBySelectionPlayer(Player entityPlayer, User entityUser)
        {
            if (entityPlayer == null)
            {
                _validationResult.Add(Messages.PlayerNotFound);
            }

            if (entityUser == null)
            {
                _validationResult.Add(Messages.UserNotFound);
            }
        }

        private void CheckMaxPlayerBySelection(Project entity)
        {
            var r = _repository as IProjectRepository;
            var numberMaxPlayerPerProject = r.GetMaxNumberPlayerPerProject();

            if (entity.PlayersRelated.Count == numberMaxPlayerPerProject)
            {
                _validationResult.Add(string.Format(Messages.YouCanSelectUpToXPlayers, numberMaxPlayerPerProject));
            }
        }

        public bool PreRegistrationProducerDisabled()
        {
            throw new NotImplementedException();
        }

        public bool RegistrationDisabled()
        {
            throw new NotImplementedException();
        }

        public bool SendToPlayersDisabled()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAllOption(Expression<Func<Project, bool>> filter)
        {
            var r = _repository as IProjectRepository;

            return r.GetAllOption(filter).OrderBy(e => e.Producer.Name).Take(10).ToList();            
        }

        public int CountUnsent()
        {
            var r = _repository as IProjectRepository;

            return r.CountUnsent();
        }

        public int CountSent()
        {
            var r = _repository as IProjectRepository;

            return r.CountSent();
        }





        #endregion

    }
}
