// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-28-2021
// ***********************************************************************
// <copyright file="CreateStartupCommandHandler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateStartupCommandHandler</summary>
    public class CreateInnovationOrganizationCommandHandler : InnovationOrganizationBaseCommandHandler, IRequestHandler<CreateInnovationOrganization, AppValidationResult>
    {
        private readonly IEditionRepository editionRepository;
        private readonly IInnovationOptionRepository innovationOptionRepository;
        private readonly ICollaboratorRepository collaboratorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganizationCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationRepo">The innovation organization repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        public CreateInnovationOrganizationCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IInnovationOrganizationRepository innovationOrganizationRepo,
            IInnovationOptionRepository innovationOptionRepo,
            IEditionRepository editionRepo,
            ICollaboratorRepository collaboratorRepo
            )
            : base(commandBus, uow, innovationOrganizationRepo)
        {
            this.editionRepository = editionRepo;
            this.innovationOptionRepository = innovationOptionRepo;
            this.collaboratorRepository = collaboratorRepo;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateInnovationOrganization cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var innovationOrganizationApiDto = cmd.InnovationOrganizationApiDto;

            innovationOrganizationApiDto.CompanyExperiences = innovationOrganizationApiDto.CompanyExperiences.Select(ta =>
                                                    new InnovationOptionsApiDto()
                                                    {
                                                        Id = ta.Id,
                                                        InnovationOption = innovationOptionRepository.Get(ta.Id)
                                                    }).ToList();

            innovationOrganizationApiDto.ProductsOrServices = innovationOrganizationApiDto.ProductsOrServices.Select(ta =>
                                                    new InnovationOptionsApiDto()
                                                    {
                                                        Id = ta.Id,
                                                        InnovationOption = innovationOptionRepository.Get(ta.Id)
                                                    }).ToList();

            innovationOrganizationApiDto.TechnologyExperiences = innovationOrganizationApiDto.TechnologyExperiences.Select(ta =>
                                                   new InnovationOptionsApiDto()
                                                   {
                                                       Id = ta.Id,
                                                       InnovationOption = innovationOptionRepository.Get(ta.Id)
                                                   }).ToList();

            innovationOrganizationApiDto.CompanyObjectives = innovationOrganizationApiDto.CompanyObjectives.Select(ta =>
                                                   new InnovationOptionsApiDto()
                                                   {
                                                       Id = ta.Id,
                                                       InnovationOption = innovationOptionRepository.Get(ta.Id)
                                                   }).ToList();

            var edition = await editionRepository.FindByIdAsync(cmd.EditionId ?? 0);

            var collaborator = await collaboratorRepository.FindByEmailAsync(innovationOrganizationApiDto.Email);
            if (collaborator == null)
            {
                #region Creates new Collaborator and User

                var createCollaboratorCommand = new CreateTinyCollaborator();

                createCollaboratorCommand.UpdateBaseProperties(
                    innovationOrganizationApiDto.ResponsibleName,
                    null,
                    innovationOrganizationApiDto.Email,
                    innovationOrganizationApiDto.PhoneNumber,
                    innovationOrganizationApiDto.CellPhone,
                    innovationOrganizationApiDto.Document);

                createCollaboratorCommand.UpdatePreSendProperties(
                    "Music", //"Music" is fixed because in [dbo].[MigrateMusicProjects] procedure, its is fixed too!
                    cmd.UserId,
                    cmd.UserUid,
                    edition.Id,
                    edition.Uid,
                    "");

                var commandResult = await base.CommandBus.Send(createCollaboratorCommand);
                if (!commandResult.IsValid)
                {
                    throw new DomainException(commandResult.Errors.Select(e => e.Message).FirstOrDefault().ToString());
                }

                collaborator = commandResult.Data as Collaborator;

                #endregion
            }

            var innovationOrganization = new InnovationOrganization(
               innovationOrganizationApiDto.Name,
               innovationOrganizationApiDto.Document,
               innovationOrganizationApiDto.ServiceName,
               innovationOrganizationApiDto.FoundersNames,
               innovationOrganizationApiDto.FoundationDate,
               innovationOrganizationApiDto.AccumulatedRevenue,
               innovationOrganizationApiDto.Description,
               innovationOrganizationApiDto.Curriculum,
               0, //TODO: WorkDedicationId vem de onde?
               innovationOrganizationApiDto.BusinessDefinition,
               innovationOrganizationApiDto.Website,
               innovationOrganizationApiDto.BusinessFocus,
               innovationOrganizationApiDto.MarketSize,
               innovationOrganizationApiDto.BusinessEconomicModel,
               innovationOrganizationApiDto.BusinessOperationalModel,
               innovationOrganizationApiDto.BusinessDifferentials,
               innovationOrganizationApiDto.CompetingCompanies,
               innovationOrganizationApiDto.BusinessStage,
               innovationOrganizationApiDto.PresentationUploadDate,
                cmd.UserId);

            if (!innovationOrganization.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganization.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationRepository.Create(innovationOrganization);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = innovationOrganization;

            return this.AppValidationResult;
        }
    }
}