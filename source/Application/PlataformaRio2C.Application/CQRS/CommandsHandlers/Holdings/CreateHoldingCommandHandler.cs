// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-15-2019
// ***********************************************************************
// <copyright file="CreateHoldingCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateHoldingCommandHandler</summary>
    public class CreateHoldingCommandHandler : IRequestHandler<CreateHolding, Guid?>
    {
        private readonly IMediator eventBus;
        private readonly IUnitOfWork uow;
        private readonly IHoldingRepository holdingRepo;
        private readonly IFileRepository fileRepo;

        public CreateHoldingCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IHoldingRepository holdingRepository,
            IFileRepository fileRepository)
        {
            this.eventBus = eventBus;
            this.uow = uow;
            this.holdingRepo = holdingRepository;
            this.fileRepo = fileRepository;
        }

        /// <summary>Handles the specified create holding.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Guid?> Handle(CreateHolding cmd, CancellationToken cancellationToken)
        {
            this.uow.BeginTransaction();

            ////Validation
            //AssertionConcern.AssertArgumentNotEmpty(obj.Title, "O Titulo não pode ser vazio");
            //AssertionConcern.AssertArgumentNotEmpty(obj.Localization, "O Localização não pode ser vazio");
            //AssertionConcern.AssertArgumentNotEmpty(obj.Description, "O Descrição não pode ser vazio");
            //AssertionConcern.AssertArgumentNotNull(obj.Active, "O Ativo não pode ser vazio");
            //AssertionConcern.AssertArgumentNotEmpty(obj.Serial, "O Serial não pode ser vazio");
            //AssertionConcern.AssertArgumentFalse(_repository.HasExists(obj.Serial), "Ja existe cadastro com esse serial");

            var holding = new Holding(
                cmd.HoldingUid, 
                cmd.Name, 
                cmd.UserId, 
                cmd.ImageFile != null);
            this.holdingRepo.Create(holding);

            var result = this.uow.SaveChanges();

            if (holding.IsImageUploaded)
            {
                ImageHelper.UploadOriginalAndCroppedImages(
                    holding.Uid,
                    cmd.ImageFile,
                    cmd.CropperImageDataX,
                    cmd.CropperImageDataY,
                    cmd.CropperImageDataWidth,
                    cmd.CropperImageDataHeight,
                    FileRepositoryPathType.HoldingImage);
            }


            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            return holding.Uid;

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}