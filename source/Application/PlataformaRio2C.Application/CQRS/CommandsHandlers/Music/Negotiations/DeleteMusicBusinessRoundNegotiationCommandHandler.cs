//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Application
//// Author           : Rafael Ribeiro 
//// Created          : 05-03-2025
////
//// Last Modified By : Rafael Ribeiro 
//// Last Modified On : 05-03-2025
//// ***********************************************************************
//// <copyright file="DeleteNegotiationCommandHandler.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using System.Threading;
//using System.Threading.Tasks;
//using MediatR;
//using PlataformaRio2C.Application.CQRS.Commands;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.Data.Context.Interfaces;

//namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
//{
//    /// <summary>DeleteNegotiationCommandHandler</summary>
//    public class DeleteMusicNegotiationCommandHandler : NegotiationBaseCommandHandler, IRequestHandler<DeleteNegotiation, AppValidationResult>
//    {
//        /// <summary>Initializes a new instance of the <see cref="DeleteNegotiationCommandHandler"/> class.</summary>
//        /// <param name="eventBus">The event bus.</param>
//        /// <param name="uow">The uow.</param>
//        /// <param name="negotiationRepository">The negotiation repository.</param>
//        public DeleteMusicNegotiationCommandHandler(
//            IMediator eventBus,
//            IUnitOfWork uow,
//            INegotiationRepository negotiationRepository)
//            : base(eventBus, uow, negotiationRepository)
//        {
//        }

//        /// <summary>Handles the specified delete negotiation.</summary>
//        /// <param name="cmd">The command.</param>
//        /// <param name="cancellationToken">The cancellation token.</param>
//        /// <returns></returns>
//        public async Task<AppValidationResult> Handle(DeleteNegotiation cmd, CancellationToken cancellationToken)
//        {
//            this.Uow.BeginTransaction();

//            var negotiation = await this.GetNegotiationByUid(cmd.NegotiationUid);

//            #region Initial validations

//            if (!this.ValidationResult.IsValid)
//            {
//                this.AppValidationResult.Add(this.ValidationResult);
//                return this.AppValidationResult;
//            }

//            #endregion

//            negotiation.Delete(cmd.UserId);
//            if (!negotiation.IsValid())
//            {
//                this.AppValidationResult.Add(negotiation.ValidationResult);
//                return this.AppValidationResult;
//            }

//            this.NegotiationRepo.Update(negotiation);
//            this.Uow.SaveChanges();

//            return this.AppValidationResult;

//            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

//            //return Task.FromResult(propertyId); // use it when the methed is not async
//        }
//    }
//}