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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateStartupCommandHandler</summary>
    public class CreateInnovationOrganizationCommandHandler : InnovationOrganizationBaseCommandHandler, IRequestHandler<CreateInnovationOrganization, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IInnovationOptionRepository innovationOptionRepo;
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IWorkDedicationRepository workDedicationRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganizationCommandHandler" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationRepository">The innovation organization repo.</param>
        /// <param name="innovationOptionRepository">The innovation option repo.</param>
        /// <param name="editionRepo">The edition repo.</param>
        /// <param name="collaboratorRepository">The collaborator repo.</param>
        /// <param name="workDedicationRepository">The work dedication repo.</param>
        public CreateInnovationOrganizationCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            IInnovationOrganizationRepository innovationOrganizationRepository,
            IInnovationOptionRepository innovationOptionRepository,
            IEditionRepository editionRepository,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IWorkDedicationRepository workDedicationRepository,
            IFileRepository fileRepository
            )
            : base(commandBus, uow, innovationOrganizationRepository)
        {
            this.editionRepo = editionRepository;
            this.innovationOptionRepo = innovationOptionRepository;
            this.collaboratorRepo = collaboratorRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.workDedicationRepo = workDedicationRepository;
            this.fileRepo = fileRepository;
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

            var workDedication = await workDedicationRepo.FindByUidAsync(cmd.WorkDedicationUid);
            if (workDedication == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.WorkDedication, Labels.FoundF), new string[] { "ToastrError" }));
            }

            var edition = await editionRepo.FindByIdAsync(cmd.EditionId ?? 0);
            if (edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Edition, Labels.FoundF), new string[] { "ToastrError" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            cmd.CompanyExperiences = cmd.CompanyExperiences.Select(ta =>
                                                    new InnovationOptionApiDto()
                                                    {
                                                        Uid = ta.Uid,
                                                        InnovationOption = innovationOptionRepo.Get(ta.Uid)
                                                    }).ToList();

            cmd.ProductsOrServices = cmd.ProductsOrServices.Select(ta =>
                                                    new InnovationOptionApiDto()
                                                    {
                                                        Uid = ta.Uid,
                                                        InnovationOption = innovationOptionRepo.Get(ta.Uid)
                                                    }).ToList();

            cmd.TechnologyExperiences = cmd.TechnologyExperiences.Select(ta =>
                                                    new InnovationOptionApiDto()
                                                    {
                                                        Uid = ta.Uid,
                                                        InnovationOption = innovationOptionRepo.Get(ta.Uid)
                                                    }).ToList();

            cmd.CompanyObjectives = cmd.CompanyObjectives.Select(ta =>
                                                   new InnovationOptionApiDto()
                                                   {
                                                       Uid = ta.Uid,
                                                       InnovationOption = innovationOptionRepo.Get(ta.Uid)
                                                   }).ToList();

            var innovationOptions = cmd.CompanyExperiences.Select(iodto => iodto.InnovationOption)
                                                            .Concat(cmd.ProductsOrServices.Select(iodto => iodto.InnovationOption))
                                                            .Concat(cmd.TechnologyExperiences.Select(iodto => iodto.InnovationOption))
                                                            .Concat(cmd.CompanyObjectives.Select(iodto => iodto.InnovationOption)).ToList();

            var collaborator = await collaboratorRepo.FindByEmailAsync(cmd.Email);
            if (collaborator == null)
            {
                #region Creates new Collaborator and User

                var createCollaboratorCommand = new CreateTinyCollaborator();

                createCollaboratorCommand.UpdateBaseProperties(
                    cmd.ResponsibleName,
                    null,
                    cmd.Email,
                    cmd.PhoneNumber,
                    cmd.CellPhone,
                    cmd.Document);

                createCollaboratorCommand.UpdatePreSendProperties(
                    CollaboratorType.Innovation.Name,
                    cmd.UserId,
                    cmd.UserUid,
                    edition.Id,
                    edition.Uid,
                    "");

                var commandResult = await base.CommandBus.Send(createCollaboratorCommand);
                if (!commandResult.IsValid)
                {
                    var currentValidationResult = new ValidationResult();
                    foreach (var error in commandResult?.Errors)
                    {
                        currentValidationResult.Add(new ValidationError(error.Message));
                    }

                    if (!currentValidationResult.IsValid)
                    {
                        this.AppValidationResult.Add(currentValidationResult);
                        return this.AppValidationResult;
                    }
                }

                collaborator = commandResult.Data as Collaborator;

                #endregion
            }

            var innovationOrganization = new InnovationOrganization(
                   edition,
                   collaborator.GetAttendeeCollaboratorByEditionId(edition.Id),
                   workDedication,
                   innovationOptions,
                   cmd.Name,
                   cmd.Document,
                   cmd.ServiceName,
                   cmd.FoundersNames,
                   cmd.FoundationDate,
                   cmd.AccumulatedRevenue,
                   cmd.Description,
                   cmd.Curriculum,
                   cmd.BusinessDefinition,
                   cmd.Website,
                   cmd.BusinessFocus,
                   cmd.MarketSize,
                   cmd.BusinessEconomicModel,
                   cmd.BusinessOperationalModel,
                   cmd.BusinessDifferentials,
                   cmd.CompetingCompanies,
                   cmd.BusinessStage,
                   !string.IsNullOrEmpty(cmd.PresentationFile),
                   cmd.UserId);

            if (!innovationOrganization.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganization.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationRepository.Create(innovationOrganization);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = innovationOrganization;

            if (!string.IsNullOrEmpty(cmd.PresentationFile))
            {
                var fileBytes = Convert.FromBase64String(cmd.PresentationFile);
                this.fileRepo.Upload(
                    new MemoryStream(fileBytes),
                    FileMimeType.Pdf,
                    cmd.PresentationFileName,
                    FileRepositoryPathType.InnovationOrganizationPresentationFile);
            }

            return this.AppValidationResult;
        }
    }
}