// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="CollaboratorProducerAppService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using HtmlAgilityPack;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.CrossCutting.Tools.Model;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using PlataformaRio2C.Domain.Entities.Specifications;

namespace PlataformaRio2C.Application.Services
{
    /// <summary>CollaboratorProducerAppService</summary>
    public class CollaboratorProducerAppService : AppService<PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext, Collaborator, CollaboratorBasicAppViewModel, CollaboratorDetailAppViewModel, CollaboratorProducerEditAppViewModel, CollaboratorProducerItemListAppViewModel>, ICollaboratorProducerAppService
    {
        #region props

        private readonly ICollaboratorService _collaboratorService;
        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IEditionRepository _eventRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly IdentityAutenticationService _identityController;
        private readonly IEmailAppService _emailAppService;
        private readonly IApiSymplaAppService _apiSymplaAppService;

        #endregion

        #region ctor
        public CollaboratorProducerAppService(ICollaboratorProducerService service, IUnitOfWork unitOfWork, ICollaboratorService collaboratorService, IRepositoryFactory repositoryFactory, IdentityAutenticationService identityController, ISystemParameterRepository systemParameterRepository, IEmailAppService emailAppService, ICollaboratorAppService collaboratorAppService, IApiSymplaAppService apiSymplaAppService)
            : base(unitOfWork, service)
        {
            _collaboratorService = collaboratorService;
            _eventRepository = repositoryFactory.EditionRepository;
            _languageRepository = repositoryFactory.LanguageRepository;
            _roleRepository = repositoryFactory.RoleRepository;
            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
            _identityController = identityController;
            _systemParameterRepository = systemParameterRepository;
            _emailAppService = emailAppService;
            _collaboratorAppService = collaboratorAppService;
            _apiSymplaAppService = apiSymplaAppService;
            _producerRepository = repositoryFactory.ProducerRepository;
        }

        #endregion

        #region Public methods

        public AppValidationResult PreRegister(PreRegistrationAppViewModel viewModel)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            viewModel.Cnpj = Producer.GetLintCnpj(viewModel.Cnpj);

           

            //ValidationResult.Add(new PlayerIsInterestsIsConsistent().Valid(playerEntity));

            var entityEvent = _eventRepository.Get(e => e.Name.Contains("2018"));

            string host = HttpContext.Current.Request.Url.Host;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            String strPathAndQuery = HttpContext.Current.Request.Url.Authority;

            if (currentCulture != null && currentCulture.Name == "pt-BR")
            {
                if (string.IsNullOrWhiteSpace(viewModel.Cnpj))
                {
                    ValidationResult.Add(new Domain.Validation.ValidationResult().Add(string.Format(Messages.TheFieldIsRequired, Labels.CNPJ)));
                    return ValidationResult;
                }

                if (!ProducerCnpjIsValid.IsCnpj(viewModel.Cnpj))
                {
                    ValidationResult.Add(new Domain.Validation.ValidationResult().Add(Messages.VatNumberIsInvalid));
                    return ValidationResult;
                }

                //var testConfirmPayment = AsyncHelpers.RunSync<bool>(() => _apiSymplaAppService.ConfirmPaymentByCnpj(viewModel.Email, viewModel.Cnpj));
                ////var testConfirmPayment = AsyncHelpers.RunSync<bool>(() => _ticketController.ConfirmPaymentByCnpj(viewModel.Email, viewModel.Cnpj));

                //if (!testConfirmPayment)
                //{
                //    ValidationResult.Add(new Domain.Validation.ValidationResult().Add(Texts.PaymentNotConfirmed));
                //    return ValidationResult;
                //}
            }
            else
            {
                if (string.IsNullOrWhiteSpace(viewModel.CompanyName))
                {
                    ValidationResult.Add(new Domain.Validation.ValidationResult().Add(string.Format(Messages.TheFieldIsRequired, Labels.CompanyName)));
                    return ValidationResult;
                }

                //var testConfirmPayment = AsyncHelpers.RunSync<bool>(() => _apiSymplaAppService.ConfirmPaymentByCompanyName(viewModel.Email, viewModel.CompanyName));

                //if (!testConfirmPayment)
                //{
                //    ValidationResult.Add(new Domain.Validation.ValidationResult().Add(Texts.PaymentNotConfirmed));
                //    return ValidationResult;
                //}
            }

            Collaborator entityCollaborator = _collaboratorRepository.Get(e => e.User.Email == viewModel.Email);
            bool collaboratorIsAlreadyRegisteredProducer = CheckCollaboratorIsAlreadyRegistered(entityCollaborator, entityEvent);


            if (collaboratorIsAlreadyRegisteredProducer)
            {
                ValidationResult.Add(new Domain.Validation.ValidationResult().Add(string.Format(Texts.RegisterAlreadyConfirmed, HttpContext.Current.Request.Url.Authority)));
                return ValidationResult;
            }

            var producerService = service as ICollaboratorProducerService;

            if (currentCulture != null && currentCulture.Name == "pt-BR")
            {
                ValidationResult.Add(producerService.PreRegister(viewModel.Email, viewModel.Cnpj, viewModel.Name, entityEvent));
            }
            else
            {
                ValidationResult.Add(producerService.PreRegister(viewModel.Email, viewModel.CompanyName, viewModel.Name, entityEvent));
            }

            if (ValidationResult.IsValid)
            {
                Commit();

                if (entityCollaborator == null)
                {
                    entityCollaborator = _collaboratorRepository.Get(e => e.User.Email == viewModel.Email);
                    SendEmailConfirmationNewRegister(entityCollaborator, false);
                }
                else if (!collaboratorIsAlreadyRegisteredProducer)
                {
                    entityCollaborator = _collaboratorRepository.Get(e => e.User.Email == viewModel.Email);
                    var hasPassword = !string.IsNullOrWhiteSpace(entityCollaborator.User.PasswordHash);
                    SendEmailConfirmationNewRegister(entityCollaborator, hasPassword);
                }
            }

            return ValidationResult;
        }

        public Task<bool> ConfirmPayment(string email, string cnpj)
        {
            return _apiSymplaAppService.ConfirmPaymentByCnpj(email, cnpj);
        }

        public ProducerAppViewModel GetProducersByUserId(int id, Guid producerUid)
        {
            var playersResult = GetProducersByUserId(id);

            return playersResult.FirstOrDefault(e => e.Uid == producerUid);
        }

        public IEnumerable<ProducerAppViewModel> GetProducersByUserId(int id)
        {
            IList<ProducerAppViewModel> viewModel = new List<ProducerAppViewModel>();

            var entityCollaborator = _collaboratorService.GetByUserId(id);

            if (entityCollaborator != null)
            {
                var producers = entityCollaborator.ProducersEvents.Select(e => e.Producer).Distinct();
                if (producers != null)
                {
                    foreach (var producer in producers)
                    {
                        viewModel.Add(new ProducerAppViewModel(producer));
                    }
                }
            }

            return viewModel;
        }

        public AppValidationResult UpdateByPortal(CollaboratorProducerEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);



                entityAlter.Address.SetZipCode(viewModel.Address.ZipCode);
                if (viewModel.Address.CountryId != null)
                {
                    entityAlter.Address.SetCountry((int)viewModel.Address.CountryId);
                }
                if (viewModel.Address.StateId != null)
                {
                    entityAlter.Address.SetState((int)viewModel.Address.StateId);
                }
                if (viewModel.Address.CityId != null)
                {
                    entityAlter.Address.SetCity((int)viewModel.Address.CityId);
                }

                entityAlter.Address.SetAddressValue(viewModel.Address.AddressValue);
                entityAlter.Address.SetCity(viewModel.Address.City);
                entityAlter.Address.SetState(viewModel.Address.State);


                MapEntityUserRole(ref entityAlter);
                _collaboratorAppService.MapEntity(ref entityAlter, viewModel.JobTitles, viewModel.MiniBios);

                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
                Commit();

            if (!ValidationResult.IsValid)
            {
                viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll()).ToList();
            }

            return ValidationResult;
        }


        public override CollaboratorProducerEditAppViewModel GetByEdit(Guid uid)
        {
            var viewModel = base.GetByEdit(uid);
                        
            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll()).ToList();

            return viewModel;
        }

        public override AppValidationResult Update(CollaboratorProducerEditAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);
                MapEntityUserRole(ref entityAlter);
                _collaboratorAppService.MapEntity(ref entityAlter, viewModel.JobTitles, viewModel.MiniBios);

                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
                Commit();

            if (!ValidationResult.IsValid)
            {                
                viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll()).ToList();
            }

            return ValidationResult;
        }

        public ImageFileAppViewModel GetImage(Guid uid)
        {
            var s = service as ICollaboratorService;
            var entity = s.GetImage(uid);

            if (entity != null && entity.ImageId > 0 && entity.ProducersEvents != null && entity.ProducersEvents.Any())
            {
                return new ImageFileAppViewModel(entity.Image);
            }

            return null;
        }

        public ImageFileAppViewModel GetThumbImage(Guid uid)
        {
            var s = service as ICollaboratorService;
            var entity = s.GetImage(uid);

            if (entity != null && entity.ImageId > 0 && entity.ProducersEvents != null && entity.ProducersEvents.Any())
            {
                return ImageFileAppViewModel.GetThumbImage(entity.Image);
            }

            return null;
        }





        #endregion

        #region Private methods

        private bool CheckCollaboratorIsAlreadyRegistered(Collaborator entityCollaborator, Edition entityEvent)
        {
            if (entityCollaborator != null)
            {
                return entityCollaborator.ProducersEvents.Any(e => e.EventId == entityEvent.Id && e.CollaboratorId == entityCollaborator.Id);
            }

            return false;
        }

        private void SendEmailConfirmationNewRegister(Collaborator collaborator, bool alreadyRegistered)
        {
            if (collaborator != null)
            {
                string message = CompileHtmlMessageInvitationToCollaborator();
                var eventRio2c = _eventRepository.Get(1);
                try
                {
                    if (!alreadyRegistered)
                    {
                        string password = PasswordHelper.GetNewRandomPassword(8, false, true, true, false);
                        var resultOperationAddPassword = _identityController.AddPassword(collaborator.UserId, password);

                        if (resultOperationAddPassword.Succeeded)
                        {
                            message = message.Replace("@{Message}", Texts.WelcomeText);
                            message = message.Replace("@{Name}", collaborator.Name);
                            message = message.Replace("@{UrlSite}", _systemParameterRepository.Get<string>(SystemParameterCodes.SiteUrl));
                            message = message.Replace("@{NameEvent}", eventRio2c.Name);
                            message = message.Replace("@{Email}", collaborator.User.Email);
                            message = message.Replace("@{Password}", password);

                            _emailAppService.SeendEmailTemplateDefault(collaborator.User.Email, Texts.RegisterPlatform, message);
                        }
                    }
                    else
                    {
                        message = message.Replace("@{Message}", Texts.WelcomeTextProducer);
                        message = message.Replace("@{Name}", collaborator.Name);
                        message = message.Replace("@{UrlSite}", _systemParameterRepository.Get<string>(SystemParameterCodes.SiteUrl));
                        message = message.Replace("@{NameEvent}", eventRio2c.Name);
                        message = message.Replace("@{Email}", collaborator.User.Email);
                        _emailAppService.SeendEmailTemplateDefault(collaborator.User.Email, Texts.RegisterPlatform, message);
                    }
                }
                catch (Exception)
                {
                    ValidationResult.Add(new Domain.Validation.ValidationResult().Add(Messages.ErrorSendingEmail));
                }
            }
        }

        private string CompileHtmlMessageInvitationToCollaborator()
        {
            HtmlDocument template = new HtmlDocument();

            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            var pathTemplate = string.Format("{0}/TemplatesEmail/defaultTemplate.html", currentPath);

            template.Load(pathTemplate);
            return template.DocumentNode.InnerHtml;
        }

        private void MapEntityUserRole(ref Collaborator entity)
        {
            if (entity.User != null && (entity.User.Roles == null || !entity.User.Roles.Any()))
            {
                var rolePlayer = _roleRepository.Get(e => e.Name == "Producer");
                entity.User.Roles = new List<Role>() { rolePlayer };
            }
            else if (entity.User.Roles != null && !entity.User.Roles.Any(e => e.Name == "Producer"))
            {
                var rolePlayer = _roleRepository.Get(e => e.Name == "Producer");
                entity.User.Roles.Add(rolePlayer);
            }
        }


        #endregion
    }
}
