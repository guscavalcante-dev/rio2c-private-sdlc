// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="DeleteNegotiationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteNegotiationCommandHandler</summary>
    public class DeleteMusicBusinessRoundNegotiationCommandHandler : MusicBusinesRoundNegotiationBaseCommandHandler, IRequestHandler<DeleteMusicBusinessRoundNegotiation, AppValidationResult>
    {
        private readonly IMusicBusinessRoundNegotiationRepository _musicBusinessRoundNegotiationRepository;

        /// <summary>Initializes a new instance of the <see cref="DeleteMusicBusinessRoundNegotiationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        public DeleteMusicBusinessRoundNegotiationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMusicBusinessRoundNegotiationRepository musicBusinessRoundNegotiationRepository)
            : base(eventBus, uow, musicBusinessRoundNegotiationRepository)
        {
            _musicBusinessRoundNegotiationRepository = musicBusinessRoundNegotiationRepository;
        }

        /// <summary>Handles the specified delete negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteMusicBusinessRoundNegotiation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var negotiation = await this._musicBusinessRoundNegotiationRepository.FindByUidAsync(cmd.NegotiationUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            negotiation.Delete(cmd.UserId);
            if (!negotiation.IsValid())
            {
                this.AppValidationResult.Add(negotiation.ValidationResult);
                return this.AppValidationResult;
            }

            _musicBusinessRoundNegotiationRepository.Update(negotiation);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}