// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="IRepositoryFactory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IRepositoryFactory
    {
        IImageFileRepository ImageFileRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IHoldingRepository HoldingRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        IPlayerDescriptionRepository PlayerDescriptionRepository { get; }
        ICollaboratorRepository CollaboratorRepository { get; }
        ICollaboratorProducerRepository CollaboratorProducerRepository { get; }
        IInterestRepository InterestRepository { get; }
        IInterestGroupRepository InterestGroupRepository { get; }
        IEditionRepository EditionRepository { get; }
        IUserRepository UserRepository { get; }
        IActivityRepository ActivityRepository { get; }
        ITargetAudienceRepository TargetAudienceRepository { get; }
        IPlayerRestrictionsSpecificsRepository PlayerRestrictionsSpecificsRepository { get; }        
        IPlayerActivityRepository PlayerActivityRepository { get; }
        IPlayerTargetAudienceRepository PlayerTargetAudienceRepository { get; }
        IRoleRepository RoleRepository { get; }
        IProducerRepository ProducerRepository { get; }
        ICollaboratorJobTitleRepository CollaboratorJobTitleRepository { get; }
        ICollaboratorMiniBioRepository CollaboratorMiniBioRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IPlayerInterestRepository PlayerInterestRepository { get; }
        IProducerDescriptionRepository ProducerDescriptionRepository { get; }
        IProducerActivityRepository ProducerActivityRepository { get; }
        IProducerTargetAudienceRepository ProducerTargetAudienceRepository { get; }
        
        //IProjectTitleRepository ProjectTitleRepository { get; }
        //IProjectLogLineRepository ProjectLogLineRepository { get; }
        //IProjectSummaryRepository ProjectSummaryRepository { get; }
        //IProjectProductionPlanRepository ProjectProductionPlanRepository { get; }
        //IProjectInterestRepository ProjectInterestRepository { get; }
        //IProjectLinkImageRepository ProjectLinkImageRepository { get; }
        //IProjectLinkTeaserRepository ProjectLinkTeaserRepository { get; }
        //IProjectAdditionalInformationRepository ProjectAdditionalInformationRepository { get; }
        //IProjectPlayerRepository ProjectPlayerRepository { get; }
        IProjectStatusRepository ProjectStatusRepository { get; }
        //IProjectPlayerEvaluationRepository ProjectPlayerEvaluationRepository { get; }
        ILogisticsRepository LogisticsRepository { get; }
        IConferenceRepository ConferenceRepository { get; }
        IConferenceLecturerRepository ConferenceLecturerRepository { get; }
        IConferenceSynopsisRepository ConferenceSynopsisRepository { get; }
        IConferenceTitleRepository ConferenceTitleRepository { get; }
        IMessageRepository MessageRepository { get; }
        IRoomRepository RoomRepository { get; }
        ILecturerRepository LecturerRepository { get; }
        ILecturerJobTitleRepository LecturerJobTitleRepository { get; }
        IRoleLecturerRepository RoleLecturerRepository { get; }
        IRoleLecturerTitleRepository RoleLecturerTitleRepository { get; }
        INegotiationRepository NegotiationRepository { get; }
        IRoomNameRepository RoomNameRepository { get; }
        INegotiationConfigRepository NegotiationConfigRepository { get; }

        INegotiationRoomConfigRepository NegotiationRoomConfigRepository { get; }

        IMailRepository MailRepository { get; }
        IMailCollaboratorRepository MailCollaboratorRepository { get; }

        ICountryRepository CountryRepository { get; }
        IStateRepository StateRepository { get; }
        ICityRepository CityRepository { get; }

        IAddressRepository AddressRepository { get; }

        IQuizRepository QuizRepository { get; }
        IQuizAnswerRepository QuizAnswerRepository { get; }
        IQuizOptionRepository QuizOptionRepository { get; }
        IQuizQuestionRepository QuizQuestionRepository { get; }

        ISpeakerRepository SpeakerRepository { get; }

        IMusicalCommissionRepository MusicalCommissionRepository { get; }
    }
}
