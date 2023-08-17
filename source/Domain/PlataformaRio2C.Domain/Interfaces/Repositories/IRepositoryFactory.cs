// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
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
        ILanguageRepository LanguageRepository { get; }
        IHoldingRepository HoldingRepository { get; }

        #region Collaborator

        ICollaboratorRepository CollaboratorRepository { get; }
        ICollaboratorJobTitleRepository CollaboratorJobTitleRepository { get; }
        ICollaboratorMiniBioRepository CollaboratorMiniBioRepository { get; }

        #endregion

        #region User

        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }

        IUserRoleRepository UserRoleRepository { get; }

        #endregion

        #region Interest & Interest Group

        IInterestRepository InterestRepository { get; }
        IInterestGroupRepository InterestGroupRepository { get; }

        #endregion

        IEditionRepository EditionRepository { get; }
        IActivityRepository ActivityRepository { get; }
        ITargetAudienceRepository TargetAudienceRepository { get; }
        IProjectStatusRepository ProjectStatusRepository { get; }

        #region Logistic

        ILogisticRepository LogisticRepository { get; }
        ILogisticAirfareRepository LogisticAirfareRepository { get; }
        ILogisticAccommodationRepository LogisticAccommodationRepository { get; }
        ILogisticTransferRepository LogisticTransferRepository { get; }

        #endregion

        IConferenceRepository ConferenceRepository { get; }
        IMessageRepository MessageRepository { get; }

        #region Room

        IRoomRepository RoomRepository { get; }
        IRoomNameRepository RoomNameRepository { get; }

        #endregion

        #region Negotiation

        INegotiationRepository NegotiationRepository { get; }
        INegotiationConfigRepository NegotiationConfigRepository { get; }
        INegotiationRoomConfigRepository NegotiationRoomConfigRepository { get; }

        #endregion

        #region Address

        ICountryRepository CountryRepository { get; }
        IStateRepository StateRepository { get; }
        ICityRepository CityRepository { get; }
        IAddressRepository AddressRepository { get; }

        #endregion

        #region Quiz

        IQuizRepository QuizRepository { get; }
        IQuizAnswerRepository QuizAnswerRepository { get; }
        IQuizOptionRepository QuizOptionRepository { get; }
        IQuizQuestionRepository QuizQuestionRepository { get; }

        #endregion
    }
}
