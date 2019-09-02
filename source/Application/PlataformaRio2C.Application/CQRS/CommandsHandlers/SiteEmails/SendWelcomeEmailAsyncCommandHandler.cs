// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="SendWelmcomeEmailAsyncCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SendWelmcomeEmailAsyncCommandHandler</summary>
    public class SendWelmcomeEmailAsyncCommandHandler : SiteMailerBaseCommandHandler, IRequestHandler<SendWelmcomeEmailAsync, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="SendWelmcomeEmailAsyncCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="mailerService">The mailer service.</param>
        public SendWelmcomeEmailAsyncCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            ISiteMailerService mailerService)
            : base(commandBus, uow, mailerService)
        {
        }

        /// <summary>Handles the specified send welmcome email asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SendWelmcomeEmailAsync cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            await this.MailerService.SendWelcomeEmail(cmd).SendAsync();

            //var holdingUid = Guid.NewGuid();

            //#region Initial validations

            //var existHoldingByName = this.HoldingRepo.Get(h => h.Name == cmd.Name && !h.IsDeleted);
            //if (existHoldingByName != null)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.AHolding, Labels.TheName, cmd.Name), new string[] { "Name" }));
            //}

            //if (!this.ValidationResult.IsValid)
            //{
            //    this.AppValidationResult.Add(this.ValidationResult);
            //    return this.AppValidationResult;
            //}

            //#endregion

            //var languageDtos = await this.languageRepo.FindAllDtosAsync();

            //var holding = new Holding(
            //    holdingUid,
            //    cmd.Name,
            //    cmd.CropperImage?.ImageFile != null,
            //    cmd.Descriptions?.Select(d => new HoldingDescription(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
            //    cmd.UserId);
            //if (!holding.IsValid())
            //{
            //    this.AppValidationResult.Add(holding.ValidationResult);
            //    return this.AppValidationResult;
            //}

            //this.HoldingRepo.Create(holding);
            //this.Uow.SaveChanges();
            //this.AppValidationResult.Data = holding;

            //if (cmd.CropperImage?.ImageFile != null)
            //{
            //    ImageHelper.UploadOriginalAndCroppedImages(
            //        holding.Uid,
            //        cmd.CropperImage.ImageFile,
            //        cmd.CropperImage.DataX,
            //        cmd.CropperImage.DataY,
            //        cmd.CropperImage.DataWidth,
            //        cmd.CropperImage.DataHeight,
            //        FileRepositoryPathType.HoldingImage);
            //}

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}