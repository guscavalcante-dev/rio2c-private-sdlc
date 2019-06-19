using LazyCache;
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Services;
using PlataformaRio2C.Infra.CrossCutting.Identity.Configuration;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Services.Log;
using PlataformaRio2C.Infra.Data.Repository;
using PlataformaRio2C.Infra.Data.Repository.Repositories;
using SimpleInjector;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace PlataformaRio2C.Infra.CrossCutting.IOC
{
    public static class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            ResolveBase(container);
            ResolveHolding(container);
            ResolvePlayer(container);
            ResolveCollaborator(container);
            ResolveCollaboratorProducer(container);
            ResolveCollaboratorPlayer(container);
            ResolveUser(container);
            ResolveInterestGroup(container);
            ResolveInterest(container);

            container.Register<IEventRepository, EventRepository>(Lifestyle.Scoped);

            container.Register<IRoleRepository, RoleRepository>(Lifestyle.Scoped);

            container.Register<PlataformaRio2C.Infra.CrossCutting.SystemParameter.PlataformaRio2CContext>(Lifestyle.Scoped);
            container.Register<ISystemParameterRepository, SystemParameterRepository>(Lifestyle.Scoped);

            container.Register<ISystemParameterCollection, SystemParameterCollection>(Lifestyle.Scoped);

            container.Register<ISystemParameterAppService, SystemParameterAppService>(Lifestyle.Scoped);
            container.Register<IUnitOfWorkSystemParameter, SystemParameter.UnitOfWorkWithLog<SystemParameter.PlataformaRio2CContext>>(Lifestyle.Scoped);

            container.Register<IProducerAppService, ProducerAppService>(Lifestyle.Scoped);
            container.Register<IProducerService, ProducerService>(Lifestyle.Scoped);
            container.Register<IProducerRepository, ProducerRepository>(Lifestyle.Scoped);

            container.Register<IEmailAppService, EmailAppService>(Lifestyle.Scoped);

            container.Register<IMessageAppService, MessageAppService>(Lifestyle.Scoped);
            container.Register<IMessageService, MessageService>(Lifestyle.Scoped);
            container.Register<IMessageRepository, MessageRepository>(Lifestyle.Scoped);

            container.Register<IProjectService, ProjectService>(Lifestyle.Scoped);
            container.Register<IProjectAppService, ProjectAppService>(Lifestyle.Scoped);
            container.Register<IProjectRepository, ProjectRepository>(Lifestyle.Scoped);

            container.Register<IErrorMessageService, ErrorMessageService>(Lifestyle.Scoped);


            container.Register<IRoomService, RoomService>(Lifestyle.Scoped);
            container.Register<IRoomRepository, RoomRepository>(Lifestyle.Scoped);
            container.Register<IRoomAppService, RoomAppService>(Lifestyle.Scoped);

            container.Register<IRoleLecturerService, RoleLecturerService>(Lifestyle.Scoped);
            container.Register<IRoleLecturerRepository, RoleLecturerRepository>(Lifestyle.Scoped);
            container.Register<IRoleLecturerAppService, RoleLecturerAppService>(Lifestyle.Scoped);

            container.Register<IDashboardAppService, DashboardAppService>(Lifestyle.Scoped);

            container.Register<INegotiationService, NegotiationService>(Lifestyle.Scoped);
            container.Register<INegotiationRepository, NegotiationRepository>(Lifestyle.Scoped);
            container.Register<INegotiationAppService, NegotiationAppService>(Lifestyle.Scoped);

            container.Register<IScheduleAppService, ScheduleAppService>(Lifestyle.Scoped);

            container.Register<IQuizAnswerAppService, QuizAnswerAppService>(Lifestyle.Scoped);
            container.Register<IQuizAnswerService, QuizAnswerService>(Lifestyle.Scoped);
            container.Register<IQuizAnswerRepository, QuizAnswerRepository>(Lifestyle.Scoped);


        }

        public static void ResolveBase(Container container)
        {
            container.Register<Data.Context.PlataformaRio2CContext>(Lifestyle.Scoped);
            container.Register<IRepositoryFactory, RepositoryFactory>(Lifestyle.Scoped);
            container.Register<Data.Context.Interfaces.IUnitOfWork, Data.Context.Models.UnitOfWorkWithLog<Data.Context.PlataformaRio2CContext>>(Lifestyle.Scoped);
            container.Register<ILogService>(() => new LogService(true), Lifestyle.Scoped);
            container.Register<Identity.Context.PlataformaRio2CContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser, int>>(() => new CustomUserStore<ApplicationUser>(container.GetInstance<Identity.Context.PlataformaRio2CContext>()), Lifestyle.Scoped);
            container.Register<IRoleStore<CustomRole, int>>(() => new CustomRoleStore<ApplicationUser>(container.GetInstance<Identity.Context.PlataformaRio2CContext>()), Lifestyle.Scoped);
            container.Register<ApplicationUserManager<ApplicationUser>>(() => new ApplicationUserManager<ApplicationUser>(container.GetInstance<IUserStore<ApplicationUser, int>>(), container.GetInstance<IdentityServicesSetup>()), Lifestyle.Scoped);
            container.Register<ApplicationSignInManager<ApplicationUser>>(Lifestyle.Scoped);
            container.Register<IdentityAutenticationService>(Lifestyle.Scoped);
            container.Register<IdentityServicesSetup>(() => MakeIdentityServicesSetup(container), Lifestyle.Scoped);

            container.Register<IImageFileRepository, ImageFileRepository>(Lifestyle.Scoped);
            container.Register<ILanguageRepository, LanguageRepository>(Lifestyle.Scoped);

            container.Register<IUserRoleRepository, UserRoleRepository>(Lifestyle.Scoped);

            container.Register<ILanguageAppService, LanguageAppService>(Lifestyle.Scoped);
            container.Register<ILanguageService, LanguageService>(Lifestyle.Scoped);

            container.Register<IUserAppService, UserAppService>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);

            container.Register<IActivityRepository, ActivityRepository>(Lifestyle.Scoped);
            container.Register<ITargetAudienceRepository, TargetAudienceRepository>(Lifestyle.Scoped);

            container.Register<IPlayerRestrictionsSpecificsService, PlayerRestrictionsSpecificsService>(Lifestyle.Scoped);

            container.Register<ILogisticsAppService, LogisticsAppService>(Lifestyle.Scoped);
            container.Register<ILogisticsService, LogisticsService>(Lifestyle.Scoped);

            container.Register<ILogisticsRepository, LogisticsRepository>(Lifestyle.Scoped);

            container.Register<IConferenceAppService, ConferenceAppService>(Lifestyle.Scoped);
            container.Register<IConferenceRepository, ConferenceRepository>(Lifestyle.Scoped);
            container.Register<IConferenceService, ConferenceService>(Lifestyle.Scoped);

            container.Register<ISpeakerRepository, SpeakerRepository>(Lifestyle.Scoped);
            container.Register<ISpeakerService, SpeakerService>(Lifestyle.Scoped);
            container.Register<ISpeakerAppService, SpeakerAppService>(Lifestyle.Scoped);

            container.Register<IMusicalCommissionRepository, MusicalCommissionRepository>(Lifestyle.Scoped);
            container.Register<IMusicalCommissionService, MusicalCommissionService>(Lifestyle.Scoped);
            container.Register<IMusicalCommissionAppService, MusicalCommissionAppService>(Lifestyle.Scoped);
        }

        public static void ResolveHolding(Container container)
        {
            container.Register<IHoldingRepository, HoldingRepository>(Lifestyle.Scoped);
            container.Register<IHoldingService, HoldingService>(Lifestyle.Scoped);
            container.Register<IHoldingAppService, HoldingAppService>(Lifestyle.Scoped);
            container.Register<IHoldingDescriptionRepository, HoldingDescriptionRepository>(Lifestyle.Scoped);
        }

        public static void ResolveCollaborator(Container container)
        {
            container.Register<ICollaboratorService, CollaboratorService>(Lifestyle.Scoped);
            container.Register<ICollaboratorAppService, CollaboratorAppService>(Lifestyle.Scoped);
            container.Register<ICollaboratorRepository, CollaboratorRepository>(Lifestyle.Scoped);
            container.Register<ICollaboratorJobTitleRepository, CollaboratorJobTitleRepository>(Lifestyle.Scoped);
            container.Register<ICollaboratorMiniBioRepository, CollaboratorMiniBioRepository>(Lifestyle.Scoped);

            container.Register<ICountryRepository, CountryRepository>(Lifestyle.Scoped);
            container.Register<ICountryService, CountryService>(Lifestyle.Scoped);
            container.Register<ICountryAppService, CountryAppService>(Lifestyle.Scoped);
        }

        public static void ResolveCollaboratorPlayer(Container container)
        {
            container.Register<ICollaboratorPlayerService, CollaboratorPlayerService>(Lifestyle.Scoped);
            container.Register<ICollaboratorPlayerAppService, CollaboratorPlayerAppService>(Lifestyle.Scoped);
        }

        public static void ResolveCollaboratorProducer(Container container)
        {
            container.Register<IApiSymplaAppService, ApiSymplaAppService>(Lifestyle.Scoped);

            container.Register<ICollaboratorProducerService, CollaboratorProducerService>(Lifestyle.Scoped);
            container.Register<ICollaboratorProducerAppService, CollaboratorProducerAppService>(Lifestyle.Scoped);
        }

        public static void ResolvePlayer(Container container)
        {
            container.Register<IPlayerAppService, PlayerAppService>(Lifestyle.Scoped);
            container.Register<IPlayerRepository, PlayerRepository>(Lifestyle.Scoped);
            container.Register<IPlayerDescriptionRepository, PlayerDescriptionRepository>(Lifestyle.Scoped);
            container.Register<IPlayerInterestService, PlayerInterestService>(Lifestyle.Scoped);
            container.Register<IPlayerInterestRepository, PlayerInterestRepository>(Lifestyle.Scoped);
            container.Register<IPlayerRestrictionsSpecificsRepository, PlayerRestrictionsSpecificsRepository>(Lifestyle.Scoped);
            container.Register<IPlayerActivityRepository, PlayerActivityRepository>(Lifestyle.Scoped);
            container.Register<IPlayerTargetAudienceRepository, PlayerTargetAudienceRepository>(Lifestyle.Scoped);
        }


        public static void ResolveUser(Container container)
        {
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IUserUseTermAppService, UserUseTermAppService>(Lifestyle.Scoped);
            container.Register<IUserUseTermRepository, UserUseTermRepository>(Lifestyle.Scoped);
            container.Register<IUserUseTermService, UserUseTermService>(Lifestyle.Scoped);
        }

        public static void ResolveInterestGroup(Container container)
        {
            container.Register<IInterestGroupAppService, InterestGroupAppService>(Lifestyle.Scoped);
            container.Register<IInterestGroupService, InterestGroupService>(Lifestyle.Scoped);
            container.Register<IInterestGroupRepository, InterestGroupRepository>(Lifestyle.Scoped);
        }

        public static void ResolveInterest(Container container)
        {
            container.Register<IInterestAppService, InterestAppService>(Lifestyle.Scoped);
            container.Register<IInterestService, InterestService>(Lifestyle.Scoped);
            container.Register<IInterestRepository, InterestRepository>(Lifestyle.Scoped);
        }


        private static IdentityServicesSetup MakeIdentityServicesSetup(Container container)
        {
            IAppCache cache = new CachingService();

            var setup = cache.Get<IdentityServicesSetup>("MakeIdentityServicesSetup-setup");

            //if (setup == null)
            //{
            //    var repository = container.GetInstance<ISystemParameterRepository>();
            //    var allSystemParameter = repository.GetAll().ToList();

            //    var teste = allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmtpIsBodyHtml);

            setup = new IdentityServicesSetup
            {
                EmailSetup = new IdentityEmailSetup(),
                SmsSetup = new IdentitySmsSetup()
            };

            //setup.EmailSetup.IsBodyHtml = allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmtpIsBodyHtml).GetValue<bool>();
            //setup.EmailSetup.UsesCredentials = allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmtpUseDefaultCredentials).GetValue<bool>();
            //setup.EmailSetup.Host = allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmtpHost).GetValue<string>();
            //setup.EmailSetup.Port = allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmtpPort).GetValue<int>();
            //setup.EmailSetup.From = new MailAddress(allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmtpDefaultSenderEmail).GetValue<string>(), allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmtpDefaultSenderName).GetValue<string>());

            //if (setup.EmailSetup.UsesCredentials)
            //{
            //    setup.EmailSetup.Credential = new NetworkCredential(allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmtpCredentialUser).GetValue<string>(),
            //                                                        allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmtpCredentialPass).GetValue<string>());
            //}

            //setup.EmailSetup.TwoFactorProviderSubject = allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.EmailTwoFactorSubject).GetValue<string>();
            //setup.EmailSetup.TwoFactorProviderMessageFormat = allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.EmailTwoFactorMessageFormat).GetValue<string>();

            //setup.SmsSetup.UrlGatewaySms = allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmsUrlGateway).GetValue<string>();
            //setup.SmsSetup.TwoFactorProviderBodyFormat = allSystemParameter.FirstOrDefault(e => e.Code == SystemParameterCodes.SmsTwoFactorBodyFormat).GetValue<string>();

            //setup = cache.GetOrAdd("MakeIdentityServicesSetup-setup", () => setup, DateTimeOffset.Now.AddMinutes(10));
        //}

        return setup;
    }
}
}
