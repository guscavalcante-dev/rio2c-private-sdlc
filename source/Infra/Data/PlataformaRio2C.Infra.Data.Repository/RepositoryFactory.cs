// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
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
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly PlataformaRio2CContext _context;

        /// <summary>Initializes a new instance of the <see cref="RepositoryFactory"/> class.</summary>
        /// <param name="context">The context.</param>
        public RepositoryFactory(PlataformaRio2CContext context)
        {
            _context = context;
        }

        private IHoldingRepository _holdingRepository;
        public IHoldingRepository HoldingRepository
        {
            get
            {
                return this._holdingRepository ?? (this._holdingRepository = new HoldingRepository(_context));
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

        #region Collaborator

        private ICollaboratorTypeRepository _collaboratorTypeRepository;
        public ICollaboratorTypeRepository CollaboratorTypeRepository
        {
            get
            {
                return this._collaboratorTypeRepository ?? (this._collaboratorTypeRepository = new CollaboratorTypeRepository(_context));
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

        #endregion

        #region User

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                return this._userRepository ?? (this._userRepository = new UserRepository(_context));
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


        private IUserRoleRepository _userRoleRepository;
        public IUserRoleRepository UserRoleRepository
        {
            get
            {
                return this._userRoleRepository ?? (this._userRoleRepository = new UserRoleRepository(_context));
            }
        }

        #endregion

        #region Interest & Interest Group

        private IInterestRepository _interestRepository;
        public IInterestRepository InterestRepository
        {
            get
            {
                return this._interestRepository ?? (this._interestRepository = new InterestRepository(_context));
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

        #endregion

        private IEditionRepository _editionRepository;
        public IEditionRepository EditionRepository
        {
            get
            {
                return this._editionRepository ?? (this._editionRepository = new EditionRepository(_context));
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

        private IProjectStatusRepository _projectStatusRepository;
        public IProjectStatusRepository ProjectStatusRepository
        {
            get
            {
                return this._projectStatusRepository ?? (this._projectStatusRepository = new ProjectStatusRepository(_context));
            }
        }

        #region Logistics

        private ILogisticRepository _logisticRepository;
        public ILogisticRepository LogisticRepository
        {
            get
            {
                return this._logisticRepository ?? (this._logisticRepository = new LogisticRepository(_context));
            }
        }

        private IAttendeePlacesRepository _attendeePlacesRepository;
        public IAttendeePlacesRepository AttendeePlacesRepository
        {
            get
            {
                return this._attendeePlacesRepository  ?? (this._attendeePlacesRepository = new AttendeePlacesRepository(_context));
            }
        }

        private IPlaceRepository _placesRepository;
        public IPlaceRepository PlaceRepository
        {
            get
            {
                return this._placesRepository ?? (this._placesRepository = new PlaceRepository(_context));
            }
        }

        private ILogisticAirfareRepository _logisticAirfareRepository;
        public ILogisticAirfareRepository LogisticAirfareRepository
        {
            get
            {
                return this._logisticAirfareRepository ?? (this._logisticAirfareRepository = new LogisticAirfareRepository(_context));
            }
        }
        
        private ILogisticAccommodationRepository _logisticAccommodationRepository;
        public ILogisticAccommodationRepository LogisticAccommodationRepository
        {
            get
            {
                return this._logisticAccommodationRepository ?? (this._logisticAccommodationRepository = new LogisticAccommodationRepository(_context));
            }
        }

        private ILogisticTransferRepository _logisticTransferRepository;
        public ILogisticTransferRepository LogisticTransferRepository
        {
            get
            {
                return this._logisticTransferRepository ?? (this._logisticTransferRepository = new LogisticTransferRepository(_context));
            }
        }

        #endregion Logistics

        private IConferenceRepository _conferenceRepository;
        public IConferenceRepository ConferenceRepository
        {
            get
            {
                return this._conferenceRepository ?? (this._conferenceRepository = new ConferenceRepository(_context));
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

        #region Room

        private IRoomRepository _roomRepository;
        public IRoomRepository RoomRepository
        {
            get
            {
                return this._roomRepository ?? (this._roomRepository = new RoomRepository(_context));
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

        #endregion

        #region Negotiation

        private INegotiationRepository _negotiationRepository;
        public INegotiationRepository NegotiationRepository
        {
            get
            {
                return this._negotiationRepository ?? (this._negotiationRepository = new NegotiationRepository(_context));
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

        #endregion

        #region Address

        private ICountryRepository _countryRepository;
        public ICountryRepository CountryRepository
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

        #endregion

        #region Quiz

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

        #endregion

        #region SalesPlatforms

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

        #endregion
    }
}
