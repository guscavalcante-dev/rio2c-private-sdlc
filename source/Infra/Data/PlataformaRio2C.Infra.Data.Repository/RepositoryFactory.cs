// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="RepositoryFactory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Repository.Repositories;

namespace PlataformaRio2C.Infra.Data.Repository
{
    /// <summary>RepositoryFactory</summary>
    public class RepositoryFactory: IRepositoryFactory
    {
        private readonly PlataformaRio2CContext _context;

        /// <summary>Initializes a new instance of the <see cref="RepositoryFactory"/> class.</summary>
        /// <param name="context">The context.</param>
        public RepositoryFactory(PlataformaRio2CContext context)
        {
            _context = context;
        }

        private IHoldingDescriptionRepository _holdingDescriptionRepository;
        public IHoldingDescriptionRepository HoldingDescriptionRepository
        {
            get
            {
                return this._holdingDescriptionRepository ?? (this._holdingDescriptionRepository = new HoldingDescriptionRepository(_context));
            }
        }

        private IHoldingRepository _holdingRepository;
        public IHoldingRepository HoldingRepository
        {
            get
            {
                return this._holdingRepository ?? (this._holdingRepository = new HoldingRepository(_context));
            }
        }

        private IImageFileRepository _imageFileRepository;
        public IImageFileRepository ImageFileRepository
        {
            get
            {
                return this._imageFileRepository ?? (this._imageFileRepository = new ImageFileRepository(_context));
            }
        }

        private ILanguageRepository _languageRepository;
        public ILanguageRepository LanguageRepository
        {
            get
            {
                return this._languageRepository ?? (this._languageRepository = new LanguageRepository(_context));
            }
        }

        private IPlayerRepository _playerRepository;
        public IPlayerRepository PlayerRepository
        {
            get
            {
                return this._playerRepository ?? (this._playerRepository = new PlayerRepository(_context));
            }
        }

        private IPlayerDescriptionRepository _playerDescriptionRepository;
        public IPlayerDescriptionRepository PlayerDescriptionRepository
        {
            get
            {
                return this._playerDescriptionRepository ?? (this._playerDescriptionRepository = new PlayerDescriptionRepository(_context));
            }
        }

        private ICollaboratorRepository _collaboratorRepository;
        public ICollaboratorRepository CollaboratorRepository
        {
            get
            {
                return this._collaboratorRepository ?? (this._collaboratorRepository = new CollaboratorRepository(_context));
            }
        }

        private IInterestRepository _interestRepository;
        public IInterestRepository InterestRepository
        {
            get
            {
                return this._interestRepository ?? (this._interestRepository = new InterestRepository(_context));
            }
        }

        private IEditionRepository _editionRepository;
        public IEditionRepository EditionRepository
        {
            get
            {
                return this._editionRepository ?? (this._editionRepository = new EditionRepository(_context));
            }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                return this._userRepository ?? (this._userRepository = new UserRepository(_context));
            }
        }


        private IInterestGroupRepository _interestGroupRepository;
        public IInterestGroupRepository InterestGroupRepository
        {
            get
            {
                return this._interestGroupRepository ?? (this._interestGroupRepository = new InterestGroupRepository(_context));
            }
        }

        private IRoleRepository _roleRepository;
        public IRoleRepository RoleRepository
        {
            get
            {
                return this._roleRepository ?? (this._roleRepository = new RoleRepository(_context));
            }
        }

        private IActivityRepository _activityRepository;
        public IActivityRepository ActivityRepository
        {
            get
            {
                return this._activityRepository ?? (this._activityRepository = new ActivityRepository(_context));
            }
        }

        private ITargetAudienceRepository _targetAudienceRepository;
        public ITargetAudienceRepository TargetAudienceRepository
        {
            get
            {
                return this._targetAudienceRepository ?? (this._targetAudienceRepository = new TargetAudienceRepository(_context));
            }
        }

        private IPlayerRestrictionsSpecificsRepository _playerRestrictionsSpecificsRepository;
        public IPlayerRestrictionsSpecificsRepository PlayerRestrictionsSpecificsRepository
        {
            get
            {
                return this._playerRestrictionsSpecificsRepository ?? (this._playerRestrictionsSpecificsRepository = new PlayerRestrictionsSpecificsRepository(_context));
            }
        }

        private IPlayerActivityRepository _playerActivityRepository;
        public IPlayerActivityRepository PlayerActivityRepository
        {
            get
            {
                return this._playerActivityRepository ?? (this._playerActivityRepository = new PlayerActivityRepository(_context));
            }
        }

        private IPlayerTargetAudienceRepository _playerTargetAudienceRepository;
        public IPlayerTargetAudienceRepository PlayerTargetAudienceRepository
        {
            get
            {
                return this._playerTargetAudienceRepository ?? (this._playerTargetAudienceRepository = new PlayerTargetAudienceRepository(_context));
            }
        }

        private IProducerRepository _producerRepository;
        public IProducerRepository ProducerRepository
        {
            get
            {
                return this._producerRepository ?? (this._producerRepository = new ProducerRepository(_context));
            }
        }

        private ICollaboratorJobTitleRepository _collaboratorJobTitleRepository;
        public ICollaboratorJobTitleRepository CollaboratorJobTitleRepository
        {
            get
            {
                return this._collaboratorJobTitleRepository ?? (this._collaboratorJobTitleRepository = new CollaboratorJobTitleRepository(_context));
            }
        }

        private ICollaboratorMiniBioRepository _collaboratorMiniBioRepository;
        public ICollaboratorMiniBioRepository CollaboratorMiniBioRepository
        {
            get
            {
                return this._collaboratorMiniBioRepository ?? (this._collaboratorMiniBioRepository = new CollaboratorMiniBioRepository(_context));
            }
        }

        private IUserRoleRepository _userRoleRepository;
        public IUserRoleRepository UserRoleRepository
        {
            get
            {
                return this._userRoleRepository ?? (this._userRoleRepository = new UserRoleRepository(_context));
            }
        }

        private IPlayerInterestRepository _playerInterestRepository;
        public IPlayerInterestRepository PlayerInterestRepository
        {
            get
            {
                return this._playerInterestRepository ?? (this._playerInterestRepository = new PlayerInterestRepository(_context));
            }
        }


        private IProducerDescriptionRepository  _producerDescriptionRepository;
        public IProducerDescriptionRepository ProducerDescriptionRepository
        {
            get
            {
                return this._producerDescriptionRepository ?? (this._producerDescriptionRepository = new ProducerDescriptionRepository(_context));
            }
        }

        private IProducerActivityRepository _producerActivityRepository;
        public IProducerActivityRepository ProducerActivityRepository
        {
            get
            {
                return this._producerActivityRepository ?? (this._producerActivityRepository = new ProducerActivityRepository(_context));
            }
        }

        private IProducerTargetAudienceRepository _producerTargetAudienceRepository;
        public IProducerTargetAudienceRepository ProducerTargetAudienceRepository
        {
            get
            {
                return this._producerTargetAudienceRepository ?? (this._producerTargetAudienceRepository = new ProducerTargetAudienceRepository(_context));
            }
        }

        private IProjectTitleRepository _projectTitleRepository;
        public IProjectTitleRepository ProjectTitleRepository
        {
            get
            {
                return this._projectTitleRepository ?? (this._projectTitleRepository = new ProjectTitleRepository(_context));
            }
        }

        private IProjectLogLineRepository _projectLogLineRepository;
        public IProjectLogLineRepository ProjectLogLineRepository
        {
            get
            {
                return this._projectLogLineRepository ?? (this._projectLogLineRepository = new ProjectLogLineRepository(_context));
            }
        }

        private IProjectSummaryRepository _projectSummaryRepository;
        public IProjectSummaryRepository ProjectSummaryRepository
        {
            get
            {
                return this._projectSummaryRepository ?? (this._projectSummaryRepository = new ProjectSummaryRepository(_context));
            }
        }

        private IProjectProductionPlanRepository _projectProductionPlanRepository;
        public IProjectProductionPlanRepository ProjectProductionPlanRepository
        {
            get
            {
                return this._projectProductionPlanRepository ?? (this._projectProductionPlanRepository = new ProjectProductionPlanRepository(_context));
            }
        }

        private IProjectInterestRepository _projectInterestRepository;
        public IProjectInterestRepository ProjectInterestRepository
        {
            get
            {
                return this._projectInterestRepository ?? (this._projectInterestRepository = new ProjectInterestRepository(_context));
            }
        }

        private IProjectLinkImageRepository _projectLinkImageRepository;
        public IProjectLinkImageRepository ProjectLinkImageRepository
        {
            get
            {
                return this._projectLinkImageRepository ?? (this._projectLinkImageRepository = new ProjectLinkImageRepository(_context));
            }
        }

        private IProjectLinkTeaserRepository _projectLinkTeaserRepository;
        public IProjectLinkTeaserRepository ProjectLinkTeaserRepository
        {
            get
            {
                return this._projectLinkTeaserRepository ?? (this._projectLinkTeaserRepository = new ProjectLinkTeaserRepository(_context));
            }
        }

        private IProjectAdditionalInformationRepository _projectAdditionalInformationRepository;
        public IProjectAdditionalInformationRepository ProjectAdditionalInformationRepository
        {
            get
            {
                return this._projectAdditionalInformationRepository ?? (this._projectAdditionalInformationRepository = new ProjectAdditionalInformationRepository(_context));
            }
        }

        private IProjectPlayerRepository _projectPlayerRepository;
        public IProjectPlayerRepository ProjectPlayerRepository
        {
            get
            {
                return this._projectPlayerRepository ?? (this._projectPlayerRepository = new ProjectPlayerRepository(_context));
            }
        }

        private IProjectStatusRepository _projectStatusRepository;
        public IProjectStatusRepository ProjectStatusRepository
        {
            get
            {
                return this._projectStatusRepository ?? (this._projectStatusRepository = new ProjectStatusRepository(_context));
            }
        }

        private IProjectPlayerEvaluationRepository _projectPlayerEvaluationRepository;
        public IProjectPlayerEvaluationRepository ProjectPlayerEvaluationRepository
        {
            get
            {
                return this._projectPlayerEvaluationRepository ?? (this._projectPlayerEvaluationRepository = new ProjectPlayerEvaluationRepository(_context));
            }
        }


        private ILogisticsRepository _logisticsRepository;
        public ILogisticsRepository LogisticsRepository
        {
            get
            {
                return this._logisticsRepository ?? (this._logisticsRepository = new LogisticsRepository(_context));
            }
        }

        private IConferenceRepository _conferenceRepository;
        public IConferenceRepository ConferenceRepository
        {
            get
            {
                return this._conferenceRepository ?? (this._conferenceRepository = new ConferenceRepository(_context));
            }
        }


        private IConferenceLecturerRepository _conferenceLecturerRepository;
        public IConferenceLecturerRepository ConferenceLecturerRepository
        {
            get
            {
                return this._conferenceLecturerRepository ?? (this._conferenceLecturerRepository = new ConferenceLecturerRepository(_context));
            }
        }

        private IConferenceSynopsisRepository _conferenceSynopsisRepository;
        public IConferenceSynopsisRepository ConferenceSynopsisRepository
        {
            get
            {
                return this._conferenceSynopsisRepository ?? (this._conferenceSynopsisRepository = new ConferenceSynopsisRepository(_context));
            }
        }

        private IConferenceTitleRepository _conferenceTitleRepository;
        public IConferenceTitleRepository ConferenceTitleRepository
        {
            get
            {
                return this._conferenceTitleRepository ?? (this._conferenceTitleRepository = new ConferenceTitleRepository(_context));
            }
        }

        private IMessageRepository _messageRepository;
        public IMessageRepository MessageRepository
        {
            get
            {
                return this._messageRepository ?? (this._messageRepository = new MessageRepository(_context));
            }
        }

        private IRoomRepository _roomRepository;
        public IRoomRepository RoomRepository
        {
            get
            {
                return this._roomRepository ?? (this._roomRepository = new RoomRepository(_context));
            }
        }

        private ILecturerRepository _lecturerRepository;
        public ILecturerRepository LecturerRepository
        {
            get
            {
                return this._lecturerRepository ?? (this._lecturerRepository = new LecturerRepository(_context));
            }
        }

        private ILecturerJobTitleRepository _lecturerJobTitleRepository;
        public ILecturerJobTitleRepository LecturerJobTitleRepository
        {
            get
            {
                return this._lecturerJobTitleRepository ?? (this._lecturerJobTitleRepository = new LecturerJobTitleRepository(_context));
            }
        }

        private IRoleLecturerRepository _roleLecturerRepository;
        public IRoleLecturerRepository RoleLecturerRepository
        {
            get
            {
                return this._roleLecturerRepository ?? (this._roleLecturerRepository = new RoleLecturerRepository(_context));
            }
        }
        

        private IRoleLecturerTitleRepository _roleLecturerTitleRepository;
        public IRoleLecturerTitleRepository RoleLecturerTitleRepository
        {
            get
            {
                return this._roleLecturerTitleRepository ?? (this._roleLecturerTitleRepository = new RoleLecturerTitleRepository(_context));
            }
        }

        private INegotiationRepository _negotiationRepository;
        public INegotiationRepository NegotiationRepository
        {
            get
            {
                return this._negotiationRepository ?? (this._negotiationRepository = new NegotiationRepository(_context));
            }
        }

        private IRoomNameRepository _roomNameRepository;
        public IRoomNameRepository RoomNameRepository
        {
            get
            {
                return this._roomNameRepository ?? (this._roomNameRepository = new RoomNameRepository(_context));
            }
        }

        private INegotiationConfigRepository _negotiationConfigRepository;
        public INegotiationConfigRepository NegotiationConfigRepository
        {
            get
            {
                return this._negotiationConfigRepository ?? (this._negotiationConfigRepository = new NegotiationConfigRepository(_context));
            }
        }

        private INegotiationRoomConfigRepository _negotiationRoomConfigRepository;
        public INegotiationRoomConfigRepository NegotiationRoomConfigRepository
        {
            get
            {
                return this._negotiationRoomConfigRepository ?? (this._negotiationRoomConfigRepository = new NegotiationRoomConfigRepository(_context));
            }
        }

        private ICollaboratorProducerRepository _collaboratorProducerRepository;
        public ICollaboratorProducerRepository CollaboratorProducerRepository
        {
            get
            {
                return this._collaboratorProducerRepository ?? (this._collaboratorProducerRepository = new CollaboratorProducerRepository(_context));
            }
        }

        private IMailRepository _mailRepository;
        public IMailRepository MailRepository
        {
            get
            {
                return this._mailRepository ?? (this._mailRepository = new MailRepository(_context));
            }
        }

        private IMailCollaboratorRepository _mailCollaboratorRepository;
        public IMailCollaboratorRepository MailCollaboratorRepository
        {
            get
            {
                return this._mailCollaboratorRepository ?? (this._mailCollaboratorRepository = new MailCollaboratorRepository(_context));
            }
        }

        private ICountryRepository _countryRepository;
        public ICountryRepository  CountryRepository
        {
            get
            {
                return this._countryRepository ?? (this._countryRepository = new CountryRepository(_context));
            }
        }

        private IStateRepository _stateRepository;
        public IStateRepository StateRepository
        {
            get
            {
                return this._stateRepository ?? (this._stateRepository = new StateRepository(_context));
            }
        }

        private ICityRepository _cityRepository;
        public ICityRepository CityRepository
        {
            get
            {
                return this._cityRepository ?? (this._cityRepository = new CityRepository(_context));
            }
        }

        private IAddressRepository _addressRepository;
        public IAddressRepository AddressRepository
        {
            get
            {
                return this._addressRepository ?? (this._addressRepository = new AddressRepository(_context));
            }
        }


        private IQuizRepository _quizRepository;
        public IQuizRepository QuizRepository
        {
            get
            {
                return this._quizRepository ?? (this._quizRepository = new QuizRepository(_context));
            }
        }

        private IQuizAnswerRepository _quizAnswerRepository;
        public IQuizAnswerRepository QuizAnswerRepository
        {
            get
            {
                return this._quizAnswerRepository ?? (this._quizAnswerRepository = new QuizAnswerRepository(_context));
            }
        }

        private IQuizQuestionRepository _quizQuestionRepository;
        public IQuizQuestionRepository QuizQuestionRepository
        {
            get
            {
                return this._quizQuestionRepository ?? (this._quizQuestionRepository = new QuizQuestionRepository(_context));
            }
        }

        private IQuizOptionRepository _quizOptionRepository;
        public IQuizOptionRepository QuizOptionRepository
        {
            get
            {
                return this._quizOptionRepository ?? (this._quizOptionRepository = new QuizOptionRepository(_context));
            }
        }

        private ISpeakerRepository _speakerRepository;
        public ISpeakerRepository SpeakerRepository
        {
            get
            {
                return this._speakerRepository ?? (this._speakerRepository = new SpeakerRepository(_context));
            }
        }

        private IMusicalCommissionRepository _musicalCommissionRepository;
        public IMusicalCommissionRepository MusicalCommissionRepository
        {
            get
            {
                return this._musicalCommissionRepository ?? (this._musicalCommissionRepository = new MusicalCommissionRepository(_context));
            }
        }

        private ISalesPlatformRepository _salesPlatformRepository;
        public ISalesPlatformRepository SalesPlatformRepository
        {
            get
            {
                return this._salesPlatformRepository ?? (this._salesPlatformRepository = new SalesPlatformRepository(_context));
            }
        }

        private ISalesPlatformWebhookRequestRepository _salesPlatformWebhookRequestRepository;
        public ISalesPlatformWebhookRequestRepository SalesPlatformWebhookRequestRepository
        {
            get
            {
                return this._salesPlatformWebhookRequestRepository ?? (this._salesPlatformWebhookRequestRepository = new SalesPlatformWebhookRequestRepository(_context));
            }
        }
    }
}
