//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Application
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="CollaboratorAppService.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using HtmlAgilityPack;
//using LinqKit;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
//using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
//using PlataformaRio2C.Infra.CrossCutting.Tools.Model;
//using PlataformaRio2C.Infra.Data.Context.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Net.Mail;
//using System.Net.Mime;
//using System.Text;

//namespace PlataformaRio2C.Application.Services
//{
//    /// <summary>CollaboratorAppService</summary>
//    public class CollaboratorAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Collaborator, CollaboratorBasicAppViewModel, CollaboratorDetailAppViewModel, CollaboratorEditAppViewModel, CollaboratorItemListAppViewModel>, ICollaboratorAppService
//    {
//        #region props

//        private readonly IRoleRepository _roleRepository;
//        private readonly ILanguageRepository _languageRepository;
//        private readonly ICollaboratorJobTitleRepository _collaboratorJobTitleRepository;
//        private readonly ICollaboratorMiniBioRepository _collaboratorMiniBioRepository;
//        private readonly IPlayerRepository _playerRepository;
//        private readonly IdentityAutenticationService _identityController;
//        private readonly IEditionRepository _eventRepository;
//        private readonly ICountryRepository _countryRepository;
//        private readonly IStateRepository _stateRepository;
//        private readonly ICityRepository _cityRepository;

//        #endregion

//        #region ctor

//        public CollaboratorAppService(ICollaboratorService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, IdentityAutenticationService identityController)
//            : base(unitOfWork, service)
//        {
//            _roleRepository = repositoryFactory.RoleRepository;
//            _languageRepository = repositoryFactory.LanguageRepository;
//            _collaboratorJobTitleRepository = repositoryFactory.CollaboratorJobTitleRepository;
//            _collaboratorMiniBioRepository = repositoryFactory.CollaboratorMiniBioRepository;
//            _playerRepository = repositoryFactory.PlayerRepository;
//            _eventRepository = repositoryFactory.EditionRepository;
//            _identityController = identityController;
//            _countryRepository = repositoryFactory.CountryRepository;
//            _stateRepository = repositoryFactory.StateRepository;
//            _cityRepository = repositoryFactory.CityRepository;

//        }

//        #endregion

//        #region Public methods            

//        public override CollaboratorEditAppViewModel GetEditViewModel()
//        {
//            var viewModel = base.GetEditViewModel();

//            LoadViewModelOptions(viewModel);

//            return viewModel;
//        }

//        public override CollaboratorEditAppViewModel GetByEdit(Guid uid)
//        {            
//            var result = base.GetByEdit(uid);

//            LoadViewModelOptions(result);

//            return result;
//        }

//        public override AppValidationResult Update(CollaboratorEditAppViewModel viewModel)
//        {
//            BeginTransaction();

//            var entity = service.Get(viewModel.Uid);

//            if (entity != null)
//            {
//                var entityAlter = viewModel.MapReverse(entity);
//                MapPlayersEntity(ref entityAlter, viewModel.Cast<CollaboratorPlayerEditAppViewModel>());

//                ValidationResult.Add(service.Update(entityAlter));
//            }

//            if (ValidationResult.IsValid)
//                Commit();

//            LoadViewModelOptions(viewModel);

//            return ValidationResult;
//        }

//        public CollaboratorAppViewModel GetByUserId(int id)
//        {
//            CollaboratorAppViewModel viewModel = null;

//            var s = service as ICollaboratorService;

//            var entity = s.GetByUserId(id);

//            if (entity != null)
//            {
//                viewModel = new CollaboratorAppViewModel(entity);
//            }

//            return viewModel;
//        }

//        public Collaborator GetByUserEmail(string email)
//        {
//            Collaborator viewModel = null;

//            var s = service as ICollaboratorService;

//            var entity = s.GetByUserEmail(email);

//            if (entity != null)
//            {
//                viewModel = entity;
//            }

//            return viewModel;
//        }

//        public List<State> listStates(string countryCode)
//        {
//            List<State> states = new List<State>();
//            states = _stateRepository.GetAll(a => a.Country.Code == countryCode).ToList();


//            return states;
//        }

//        public List<State> listStates(int countryCode)
//        {
//            List<State> states = new List<State>();
//            states = _stateRepository.GetAll(a => a.Country.Id == countryCode).ToList();


//            return states;
//        }

//        public List<Country> listCountries()
//        {
//            List<Country> countries = new List<Country>();
//            countries = _countryRepository.GetAll().ToList();

//            return countries;
//        }

//        public List<City> listCities(string stateCode)
//        {
//            List<City> cities = new List<City>();
//            //cities = _cityRepository.GetAll(a => a.State.StateCode == stateCode).ToList();


//            return cities;
//        }

//        public List<City> listCities(int stateCode)
//        {
//            List<City> cities = new List<City>();
//            cities = _cityRepository.GetAll(a => a.State.Id == stateCode).ToList();


//            return cities;
//        }

//        public CollaboratorEditAppViewModel GetEditByUserId(int id)
//        {
//            CollaboratorEditAppViewModel viewModel = null;

//            var s = service as ICollaboratorService;

//            var entity = s.GetByUserId(id);

//            if (entity != null)
//            {
//                viewModel = new CollaboratorEditAppViewModel(entity);

//                //populate all countries
//                viewModel.Countries = _countryRepository.GetAll();

//                if(viewModel.Address.Country != null && viewModel.Address.Country != 0)
//                {
//                    //populate all states according to Country ID
//                    viewModel.States = _stateRepository.GetAll(a => a.CountryId == 30).ToList();
//                    //viewModel.StateId = (entity.Address.StateId != null ? (int)entity.Address.StateId : 0);
//                    //viewModel.StateId = 0;

//                    //populate all cities according to State ID
//                    viewModel.Cities = _cityRepository.GetAll(a => a.StateId == viewModel.Address.StateId).ToList();
//                    //viewModel.CityId = (entity.Address.CityId != null ? (int)entity.Address.CityId : 0);
//                    //viewModel.CityId = 0;

//                }
//                else
//                {
//                    viewModel.States = null;
//                    viewModel.StateId = 0;
//                    viewModel.Cities = null;
//                    viewModel.CityId = 0;
//                }

//                viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList()).ToList();
//            }

//            return viewModel;
//        }

//        public CollaboratorDetailAppViewModel GetDetailByUserId(int id)
//        {
//            CollaboratorDetailAppViewModel viewModel = null;

//            var s = service as ICollaboratorService;

//            var entity = s.GetByUserId(id);



//            if (entity != null)
//            {
//                viewModel = new CollaboratorDetailAppViewModel(entity);
//            }

//            return viewModel;
//        }

//        public void MapEntity(ref Collaborator entity, IEnumerable<CollaboratorJobTitleAppViewModel> jobTitles, IEnumerable<CollaboratorMiniBioAppViewModel> miniBios)
//        {
//            MapJobTitles(ref entity, jobTitles);
//            MapMiniBios(ref entity, miniBios);
//        }

//        public void MapPlayersEntity(ref Collaborator entity, CollaboratorPlayerEditAppViewModel viewModel)
//        {
//            //if (entity.Players != null && entity.Players.Any())
//            //{
//            //    entity.Players.Clear();
//            //}

//            if (viewModel.Players != null && viewModel.Players.Any(e => e.Uid != Guid.Empty))
//            {
//                var playersFound = new List<Player>();
//                foreach (var playerRelationship in viewModel.Players)
//                {
//                    var player = _playerRepository.Get(playerRelationship.Uid);
//                    if (player != null)
//                    {
//                        playersFound.Add(player);
//                    }
//                }

//                entity.SetPlayers(playersFound);
//            }
//            else
//            {
//                entity.SetPlayers(null);
//            }
//        }

//        public IEnumerable<Tuple<bool, CollaboratorItemListAppViewModel, string>> SendInvitationCollaboratorsByEmails(string[] emails)
//        {
//            IList<Tuple<bool, CollaboratorItemListAppViewModel, string>> tupleresult = new List<Tuple<bool, CollaboratorItemListAppViewModel, string>>();

//            var eventRio2c = _eventRepository.Get(1);

//            if (emails != null && emails.Any())
//            {
//                foreach (var email in emails)
//                {
//                    var result = SendIntiveByEmail(email, eventRio2c);
//                    if (result.IsValid)
//                    {
//                        tupleresult.Add(new Tuple<bool, CollaboratorItemListAppViewModel, string>(true, new CollaboratorItemListAppViewModel() { Email = email }, ""));
//                    }
//                    else
//                    {
//                        tupleresult.Add(new Tuple<bool, CollaboratorItemListAppViewModel, string>(false, new CollaboratorItemListAppViewModel() { Email = email }, string.Join(",", result.Errors.Select(e => e.Message))));
//                    }
//                }

//            }

//            return tupleresult;
//        }

//        public ImageFileAppViewModel GetThumbImage(Guid uid)
//        {
//            var s = service as ICollaboratorService;
//            var entity = s.GetImage(uid);

//            //if (entity != null && entity.ImageId > 0)
//            //{
//            //    return ImageFileAppViewModel.GetThumbImage(entity.Image);
//            //}

//            return null;
//        }

//        public ImageFileAppViewModel GetImage(Guid uid)
//        {
//            var s = service as ICollaboratorService;
//            var entity = s.GetImage(uid);

//            //if (entity != null && entity.ImageId > 0)
//            //{
//            //    return new ImageFileAppViewModel(entity.Image);
//            //}

//            return null;
//        }

//        public CollaboratorStatusRegisterAppViewModel GetStatusRegisterCollaboratorByUserId(int id)
//        {
//            CollaboratorStatusRegisterAppViewModel viewModel = null;

//            var s = service as ICollaboratorService;

//            var entity = s.GetStatusRegisterCollaboratorByUserId(id);

//            if (entity != null)
//            {
//                viewModel = new CollaboratorStatusRegisterAppViewModel(entity);
//            }

//            return viewModel;
//        }

//        public IEnumerable<CollaboratorOptionAppViewModel> GetOptions(string term)
//        {
//            var s = service as ICollaboratorService;
//            //var entities = s.GetOptions(GetPredicateForGetCollaboratorsOptions(term)).OrderBy(e => e.Name).Take(5).ToList();

//            //if (entities != null && entities.Any())
//            //{
//            //    IEnumerable<CollaboratorOptionAppViewModel> results = CollaboratorOptionAppViewModel.MapList(entities).ToList();
//            //    return results;
//            //}

//            return new List<CollaboratorOptionAppViewModel>() { };
//        }

//        public IEnumerable<CollaboratorOptionMessageAppViewModel> GetOptionsChat(int userId)
//        {
//            var s = service as ICollaboratorService;

//            var entities = s.GetOptionsChat(userId);

//            if (entities != null && entities.Any())
//            {
//                return CollaboratorOptionMessageAppViewModel.MapList(entities);
//            }

//            return null;
//        }
//        #endregion

//        #region private methods          

//        private AppValidationResult SendIntiveByEmail(string email, Edition eventRio2c)
//        {

//            var result = new AppValidationResult();

//            string password = PasswordHelper.GetNewRandomPassword(8, false, true, true, false);

//            var collaborator = service.GetAll().FirstOrDefault(e => e.User.Email == email);

//            var resultOperationAddPassword = _identityController.AddPassword(collaborator.Id, password);

//            var currentPath = AppDomain.CurrentDomain.BaseDirectory;

//            if (resultOperationAddPassword.Succeeded)
//            {
//                try
//                {
//                    string message = CompileHtmlMessageInvitationToCollaborator();
//                    message = message.Replace("@{Message}", Texts.EmailInviteCollaborator);
//                    message = message.Replace("@{Name}", collaborator.FirstName);
//                    //message = message.Replace("@{UrlSite}", _systemParameterRepository.Get<string>(SystemParameterCodes.SiteUrl));
//                    message = message.Replace("@{NameEvent}", eventRio2c.Name);
//                    message = message.Replace("@{Email}", collaborator.User.Email);
//                    message = message.Replace("@{Password}", password);

//                    //MailMessage mail = new MailMessage(_systemParameterRepository.Get<string>(SystemParameterCodes.SmtpDefaultSenderEmail), collaborator.User.Email);
//                    //mail.Subject = _systemParameterRepository.Get<string>(SystemParameterCodes.EmailInviteCollaboratorSubject);
//                    //mail.IsBodyHtml = _systemParameterRepository.Get<bool>(SystemParameterCodes.SmtpIsBodyHtml);
//                    //mail.BodyEncoding = System.Text.Encoding.UTF8;


//                    //var hiddenCopy = _systemParameterRepository.Get<string>(SystemParameterCodes.EmailHiddenCopySender);

//                    //if (hiddenCopy != null && !string.IsNullOrWhiteSpace(hiddenCopy))
//                    //{
//                    //    var senders = hiddenCopy.Split(';');

//                    //    foreach (var sender in senders)
//                    //    {
//                    //        MailAddress bcc = new MailAddress(sender);
//                    //        mail.Bcc.Add(bcc);
//                    //    }
//                    //}

//                    //LinkedResource inlineHeader1 = new LinkedResource(string.Format("{0}/TemplatesEmail/img/header_email_1.jpg", currentPath), MediaTypeNames.Image.Jpeg);
//                    //inlineHeader1.ContentId = "ContentIdImgHeader1";
//                    //inlineHeader1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

//                    //AlternateView alternateView = AlternateView.CreateAlternateViewFromString(message, Encoding.GetEncoding("utf-8"), "text/html");
//                    //alternateView.LinkedResources.Add(inlineHeader1);

//                    //mail.AlternateViews.Add(alternateView);

//                    //mail.Priority = MailPriority.Normal;

//                    //SmtpClient client = new SmtpClient();
//                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;

//                    //client.Host = _systemParameterRepository.Get<string>(SystemParameterCodes.SmtpHost);
//                    //client.Port = _systemParameterRepository.Get<int>(SystemParameterCodes.SmtpPort);
//                    //client.EnableSsl = _systemParameterRepository.Get<bool>(SystemParameterCodes.SmtpEnableSsl);

//                    //if (!client.UseDefaultCredentials)
//                    //{
//                    //    client.Credentials = new System.Net.NetworkCredential(_systemParameterRepository.Get<string>(SystemParameterCodes.SmtpCredentialUser), _systemParameterRepository.Get<string>(SystemParameterCodes.SmtpCredentialPass));
//                    //}

//                    //client.UseDefaultCredentials = _systemParameterRepository.Get<bool>(SystemParameterCodes.SmtpUseDefaultCredentials);

//                    //client.Send(mail);
//                }
//                catch (Exception ex)
//                {
//                    result.Add(string.Format("Não foi possível enviar o e-mail. Motivo: {0} ", ex.Message));
//                }
//            }

//            return result;
//        }

//        private string CompileHtmlMessageInvitationToCollaborator()
//        {
//            HtmlDocument template = new HtmlDocument();

//            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
//            var pathTemplate = string.Format("{0}/TemplatesEmail/defaultTemplate.html", currentPath);

//            template.Load(pathTemplate);
//            return template.DocumentNode.InnerHtml;
//        }

//        private void MapJobTitles(ref Collaborator entity, IEnumerable<CollaboratorJobTitleAppViewModel> jobTitles)
//        {
//            //if (entity.JobTitles != null && entity.JobTitles.Any())
//            //{
//            //    _collaboratorJobTitleRepository.DeleteAll(entity.JobTitles);
//            //}

//            if (jobTitles != null && jobTitles.Any())
//            {
//                var entitiesJobTitles = new List<CollaboratorJobTitle>();
//                foreach (var jobTitleViewModel in jobTitles)
//                {
//                    var entityJobTitle = jobTitleViewModel.MapReverse();
//                    var language = _languageRepository.Get(e => e.Code == jobTitleViewModel.LanguageCode);
//                    //entityJobTitle.SetLanguage(language);
//                    entitiesJobTitles.Add(entityJobTitle);
//                }
//                entity.SetJobTitles(entitiesJobTitles);
//            }
//        }

//        private void MapMiniBios(ref Collaborator entity, IEnumerable<CollaboratorMiniBioAppViewModel> miniBios)
//        {
//            //if (entity.MiniBios != null && entity.MiniBios.Any())
//            //{
//            //    _collaboratorMiniBioRepository.DeleteAll(entity.MiniBios);
//            //}

//            if (miniBios != null && miniBios.Any())
//            {
//                var entitiesMiniBios = new List<CollaboratorMiniBio>();
//                foreach (var miniBiosViewModel in miniBios)
//                {
//                    var entityMiniBio = miniBiosViewModel.MapReverse();
//                    var language = _languageRepository.Get(e => e.Code == miniBiosViewModel.LanguageCode);
//                    //entityMiniBio.SetLanguage(language);
//                    entitiesMiniBios.Add(entityMiniBio);
//                }
//                entity.SetMiniBios(entitiesMiniBios);
//            }
//        }

//        private void LoadViewModelOptions(CollaboratorEditAppViewModel viewModel)
//        {
//            viewModel.LanguagesOptions = LanguageAppViewModel.MapList(_languageRepository.GetAll().ToList()).ToList();

//            var players = _playerRepository.GetAllWithHoldingSimple().OrderBy(e => e.Holding.Name).ThenBy(t => t.Name).ToList();

//            viewModel.PlayersOptions = new List<PlayerOptionAppViewModel>() { new PlayerOptionAppViewModel() { Name = "Selecione" } };
//            viewModel.PlayersOptions = viewModel.PlayersOptions.Concat(PlayerOptionAppViewModel.MapList(players).ToList());

//            viewModel.PlayersOptions = viewModel.PlayersOptions.Select(po =>
//            {
//                if (!string.IsNullOrWhiteSpace(po.HoldingName))
//                {
//                    po.Name = string.Format("{0} / {1}", po.HoldingName, po.Name);
//                }
//                return po;
//            });            
//        }      
        
//        private Expression<Func<Collaborator, bool>> GetPredicateForGetCollaboratorsOptions(string term)
//        {
//            var predicate = PredicateBuilder.New<Collaborator>(true);

//            if (!string.IsNullOrWhiteSpace(term))
//            {
//                term = term.Trim().ToLower();

//                var predicateTerm = PredicateBuilder.New<Collaborator>(false);

//                //predicateTerm = predicateTerm.Or(c => c.Name.ToLower().Contains(term));
//                //predicateTerm = predicateTerm.Or(c => c.Players.Any(p => p.Name.ToLower().Contains(term)));
//                //predicateTerm = predicateTerm.Or(c => c.Players.Any() && c.Players.Select(p => p.Holding.Name.ToLower()).Any(h => h.Contains(term)));
//                //predicateTerm = predicateTerm.Or(c => c.ProducersEvents.Any() && c.ProducersEvents.Select(p => p.Producer.Name.ToLower()).Any(h => h.Contains(term)));

//                predicate = PredicateBuilder.And<Collaborator>(predicate, predicateTerm);
//            }

//            return predicate;
//        }
        
//        #endregion
//    }
//}
