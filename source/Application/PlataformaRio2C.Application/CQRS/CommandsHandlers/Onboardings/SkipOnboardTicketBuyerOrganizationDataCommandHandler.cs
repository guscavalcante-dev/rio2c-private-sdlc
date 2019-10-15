// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-15-2019
// ***********************************************************************
// <copyright file="SkipOnboardTicketBuyerOrganizationDataCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SkipOnboardTicketBuyerOrganizationDataCommandHandler</summary>
    public class SkipOnboardTicketBuyerOrganizationDataCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<SkipOnboardTicketBuyerOrganizationData, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="SkipOnboardTicketBuyerOrganizationDataCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        public SkipOnboardTicketBuyerOrganizationDataCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
        }

        /// <summary>Handles the specified skip onboard ticket buyer organization data.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(SkipOnboardTicketBuyerOrganizationData cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var attendeeCollaborator = await this.attendeeCollaboratorRepo.GetAsync(ac => ac.Collaborator.Uid == cmd.CollaboratorUid);
            if (attendeeCollaborator == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM), new string[] { "CompanyName" }));
            }

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            attendeeCollaborator.SkipOnboardTicketBuyerCompanyData(cmd.UserId);

            if (!attendeeCollaborator.IsValid())
            {
                this.AppValidationResult.Add(attendeeCollaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.attendeeCollaboratorRepo.Update(attendeeCollaborator);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = attendeeCollaborator;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}