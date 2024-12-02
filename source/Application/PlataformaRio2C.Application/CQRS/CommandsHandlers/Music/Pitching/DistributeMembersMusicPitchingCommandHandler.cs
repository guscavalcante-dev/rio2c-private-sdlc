// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 11-22-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-22-2024
// ***********************************************************************
// <copyright file="DistributeMembersMusicPitchingCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DistributeMembersMusicPitchingCommandHandler</summary>
    public class DistributeMembersMusicPitchingCommandHandler : MusicBandBaseCommandHandler, IRequestHandler<DistributeMembersMusicPitching, AppValidationResult>
    {
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IUserRepository userRepo;
        private readonly IAttendeeMusicBandRepository attendeeMusicBandRepo;
        private readonly ICollaboratorRepository collaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistributeMembersMusicPitchingCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicBandRepo">The music band repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        /// <param name="userRepo">The user repo.</param>
        /// <param name="attendeeMusicBandRepo">The attendee music band repo.</param>
        /// <param name="collaboratorRepo">The collaborator repo.</param>
        public DistributeMembersMusicPitchingCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IMusicBandRepository musicBandRepo,
            IEditionRepository editionRepo,
            IUserRepository userRepo,
            IAttendeeMusicBandRepository attendeeMusicBandRepo,
            ICollaboratorRepository collaboratorRepository
        )
            : base(commandBus, uow, musicBandRepo)
        {
            this.musicBandRepo = musicBandRepo;
            this.editionRepo = editionRepo;
            this.userRepo = userRepo;
            this.attendeeMusicBandRepo = attendeeMusicBandRepo;
            this.collaboratorRepo = collaboratorRepository;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DistributeMembersMusicPitching cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var editionDto = await editionRepo.FindDtoAsync(cmd.EditionId.Value);
            if (editionDto.IsMusicPitchingComissionEvaluationOpen() != true)
            {
                this.AppValidationResult.Add(
                    this.ValidationResult.Add(
                        new ValidationError(
                            Texts.ForbiddenErrorMessage,
                            new string[] { "ToastrError" }
                        )
                    )
                );
                return this.AppValidationResult;
            }

            var distributedMembersCount = await this.attendeeMusicBandRepo.CountByEvaluatorUsersAsync(editionDto.Id);
            if (distributedMembersCount > 0)
            {
                this.AppValidationResult.Add(
                    this.ValidationResult.Add(
                        new ValidationError(
                            string.Format(Messages.ThereAreDistributedMembersByEdition),
                            new string[] { "ToastrError" }
                        )
                    )
                );
                return this.AppValidationResult;
            }

            var attendeeMusicBands = await this.attendeeMusicBandRepo.FindAllByEditionIdAsync(editionDto.Id);
            var members = await this.collaboratorRepo.FindMusicCommissionMembers(
                editionDto.Id
            );

            var memberIndex = 0;
            foreach (var attendeeMusicBand in attendeeMusicBands)
            {
                if (memberIndex >= members.Count || memberIndex >= attendeeMusicBands.Count)
                {
                    memberIndex = 0;
                }
                var member = members[memberIndex];
                memberIndex++;
                this.attendeeMusicBandRepo.Update(attendeeMusicBand);
                attendeeMusicBand.UpdateEvaluatorUserId(member.UserBaseDto.Id);
            }

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = new object();
            return this.AppValidationResult;
        }
    }
}