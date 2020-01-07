// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdatePresentationFormatMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdatePresentationFormatMainInformationCommandHandler</summary>
    public class UpdatePresentationFormatMainInformationCommandHandler : PresentationFormatBaseCommandHandler, IRequestHandler<UpdatePresentationFormatMainInformation, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdatePresentationFormatMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="presentationFormatRepository">The presentation format repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdatePresentationFormatMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IPresentationFormatRepository presentationFormatRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, presentationFormatRepository)
        {
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update presentation format main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdatePresentationFormatMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var presentationFormat = await this.GetPresentationFormatByUid(cmd.PresentationFormatUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            presentationFormat.UpdateMainInformation(
                cmd.Names?.Select(d => new PresentationFormatName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);
            if (!presentationFormat.IsValid())
            {
                this.AppValidationResult.Add(presentationFormat.ValidationResult);
                return this.AppValidationResult;
            }

            this.PresentationFormatRepo.Update(presentationFormat);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}