// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 03-31-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-31-2021
// ***********************************************************************
// <copyright file="EvaluateMusicBandCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>EvaluateMusicBandCommandHandler</summary>
    public class EvaluateMusicBandCommandHandler : MusicBandBaseCommandHandler, IRequestHandler<EvaluateMusicBand, AppValidationResult>
    {
        private readonly IMusicBandRepository musicBandRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IUserRepository userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluateMusicBandCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicBandRepo">The music band repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        /// <param name="userRepo">The user repo.</param>
        public EvaluateMusicBandCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IMusicBandRepository musicBandRepo,
            IEditionRepository editionRepo,
            IUserRepository userRepo
            )
            : base(commandBus, uow, musicBandRepo)
        {
            this.musicBandRepo = musicBandRepo;
            this.editionRepo = editionRepo;
            this.userRepo = userRepo;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="TargetAudienceApiDto"></exception>
        public async Task<AppValidationResult> Handle(EvaluateMusicBand cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            if (!cmd.Grade.HasValue)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Grade, "10", "1"), new string[] { nameof(EvaluateMusicBand.Grade) })));
                return this.AppValidationResult;
            }

            var editionDto = await editionRepo.FindDtoAsync(cmd.EditionId.Value);
            if (editionDto.IsMusicPitchingComissionEvaluationOpen() != true)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(Texts.ForbiddenErrorMessage, new string[] { "ToastrError" })));
                return this.AppValidationResult;
            }

            var musicBand = await musicBandRepo.FindByIdAsync(cmd.MusicBandId.Value);
            musicBand.Evaluate(
                editionDto.Edition,
                await userRepo.FindByIdAsync(cmd.UserId),
                cmd.Grade.Value);

            if (!musicBand.IsValid())
            {
                this.AppValidationResult.Add(musicBand.ValidationResult);
                return this.AppValidationResult;
            }

            this.MusicBandRepo.Update(musicBand);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = musicBand;

            return this.AppValidationResult;
        }
    }
}