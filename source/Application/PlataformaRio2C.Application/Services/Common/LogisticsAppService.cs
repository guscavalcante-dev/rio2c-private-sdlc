// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="LogisticsAppService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqKit;
using HtmlAgilityPack;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using System.Net.Mail;

namespace PlataformaRio2C.Application.Services
{
    /// <summary>LogisticsAppService</summary>
    public class LogisticsAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Logistics, LogisticsAppViewModel, LogisticsAppViewModel, LogisticsEditAppViewModel, LogisticsItemListAppViewModel>, ILogisticsAppService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IEditionRepository _eventRepository;
        private readonly IEmailAppService _emailAppService;

        /// <summary>Initializes a new instance of the <see cref="LogisticsAppService"/> class.</summary>
        /// <param name="service">The service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        /// <param name="emailAppService">The email application service.</param>
        /// <param name="systemParameterRepository">The system parameter repository.</param>
        public LogisticsAppService(ILogisticsService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, IEmailAppService emailAppService)
            : base(unitOfWork, service)
        {
            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
            _eventRepository = repositoryFactory.EditionRepository;
            _producerRepository = repositoryFactory.ProducerRepository;
            _emailAppService = emailAppService;
        }

        public override LogisticsEditAppViewModel GetEditViewModel()
        {
            var viewModel = new LogisticsEditAppViewModel();          

            return viewModel;
        }

        public override AppValidationResult Create(LogisticsEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = viewModel.MapReverse();

            var collaborator = _collaboratorRepository.Get(e => e.Uid == viewModel.CollaboratorUid);
            entity.SetCollaborator(collaborator);
            viewModel.SetCollaborator(collaborator);

            entity.SetEvent(_eventRepository.Get(e => e.Name.Contains("2018")));

            ValidationResult.Add(service.Create(entity));

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public override LogisticsEditAppViewModel GetByEdit(Guid uid)
        {
            LogisticsEditAppViewModel vm = null;

            var entity = service.Get(uid);

            if (entity != null)
            {
                vm = new LogisticsEditAppViewModel(entity);                
            }

            return vm;
        }


        public override AppValidationResult Update(LogisticsEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);

                var collaborator = _collaboratorRepository.Get(e => e.Uid == viewModel.CollaboratorUid);
                entityAlter.SetCollaborator(collaborator);
                viewModel.SetCollaborator(collaborator);

                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public IEnumerable<LogisticsCollaboratorAppViewModel> GetCollaboratorsOptions(string term)
        {

            var entities = _collaboratorRepository.GetAll(GetPredicateForGetCollaboratorsOptions(term)).OrderBy(e => e.FirstName).Take(5).ToList();
           
            if (entities != null && entities.Any())
            {
                IEnumerable<LogisticsCollaboratorAppViewModel> results = LogisticsCollaboratorAppViewModel.MapList(entities).ToList();
                return results;
            }

            return new List<LogisticsCollaboratorAppViewModel>() { };
        }


        private Expression<Func<Collaborator, bool>> GetPredicateForGetCollaboratorsOptions(string term)
        {
            

            var predicate = PredicateBuilder.New<Collaborator>(true);

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.Trim().ToLower();


                var predicateTerm = PredicateBuilder.New<Collaborator>(false);

                predicateTerm = predicateTerm.Or(c => c.FirstName.ToLower().Contains(term));
                //predicateTerm = predicateTerm.Or(c => c.Players.Any(p => p.Name.ToLower().Contains(term)));
                //predicateTerm = predicateTerm.Or(c => c.Players.Any() && c.Players.Select(p => p.Holding.Name.ToLower()).Any(h => h.Contains(term)));
                //predicateTerm = predicateTerm.Or(c => c.ProducersEvents.Any() && c.ProducersEvents.Select(p => p.Producer.Name.ToLower()).Any(h => h.Contains(term)));

                predicate = PredicateBuilder.And<Collaborator>(predicate, predicateTerm);
            }

            return predicate;
        }

        public LogisticsEditAppViewModel GetDefaultOptions(LogisticsEditAppViewModel viewModel)
        {
            if (viewModel.Collaborator == null && viewModel.CollaboratorUid != Guid.Empty)
            {
                var collaborator = _collaboratorRepository.Get(e => e.Uid == viewModel.CollaboratorUid);                
                viewModel.SetCollaborator(collaborator);
            }
            else
            {
                var vm = GetByEdit(viewModel.Uid);
                viewModel.Collaborator = vm.Collaborator;
                viewModel.CollaboratorUid = vm.CollaboratorUid;
            }

            return viewModel;
        }

        public AppValidationResult SendEmailToCollaborators(Guid uidsCollaborator, Attachment attachment, string textEmail)
        {
            if (uidsCollaborator != null)
            {
                var collaborator = _collaboratorRepository.GetWithPlayerAndProducerUid(uidsCollaborator);

                if (collaborator != null)
                {
                    IList<Tuple<bool, CollaboratorItemListAppViewModel, string>> tupleresult = new List<Tuple<bool, CollaboratorItemListAppViewModel, string>>();

                    var result = SendEmailSchedule(collaborator, attachment, textEmail);

                    if (result.IsValid)
                    {
                        tupleresult.Add(new Tuple<bool, CollaboratorItemListAppViewModel, string>(true, new CollaboratorItemListAppViewModel() { Email = collaborator.User.Email }, ""));
                    }
                    else
                    {
                        tupleresult.Add(new Tuple<bool, CollaboratorItemListAppViewModel, string>(false, new CollaboratorItemListAppViewModel() { Email = collaborator.User.Email }, string.Join(",", result.Errors.Select(e => e.Message))));
                    }

                    ValidationResult.Data = new { SendSuccess = tupleresult.Where(e => e.Item1).Select(e => e.Item2.Email), SendError = tupleresult.Where(e => !e.Item1).Select(e => new { Email = e.Item2.Email, Reason = e.Item3 }) };
                }
            }


            return ValidationResult;
        }

        private AppValidationResult SendEmailSchedule(Collaborator collaborator, Attachment attachment, string textEmail)
        {
            var result = new AppValidationResult();

            try
            {
                //var message = CompileHtmlMessageTemplateDefault();
                //message = message.Replace("@{Message}", Infra.CrossCutting.Resources.Texts.EmailAgenda);
                //message = message.Replace("@{Name}", collaborator.Name);
                //message = message.Replace("@{urlSistema}", _systemParameterRepository.Get<string>(SystemParameterCodes.SiteUrl));

                _emailAppService.SeendEmailTemplateDefault(collaborator.User.Email, "Rio2C - Logística", textEmail);
                //_emailAppService.SeendEmailTemplateDefault("vinitostes@outlook.com", "Rio2C - Logística", message, attachment);
            }
            catch (Exception ex)
            {
                result.Add(string.Format("Não foi possível enviar o e-mail. Motivo: {0} ", ex.Message));
            }

            return result;
        }

        private string CompileHtmlMessageTemplateDefault()
        {
            HtmlDocument template = new HtmlDocument();

            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            var pathTemplate = string.Format("{0}/TemplatesEmail/defaultTemplate.html", currentPath);

            template.Load(pathTemplate);
            return template.DocumentNode.InnerHtml;
        }
    }

    
}
