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
        private readonly IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepo;
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IWorkDedicationRepository workDedicationRepo;
        private readonly IFileRepository fileRepo;
        private readonly IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;
        private readonly IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepo;
        private readonly IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepo;

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
            IAttendeeInnovationOrganizationRepository attendeeInnovationOrganizationRepository,
            IEditionRepository editionRepository,
            ICollaboratorRepository collaboratorRepository,
            IWorkDedicationRepository workDedicationRepository,
            IFileRepository fileRepository,
            IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepository,
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository
            )
            : base(commandBus, uow, innovationOrganizationRepository)
        {
            this.attendeeInnovationOrganizationRepo = attendeeInnovationOrganizationRepository;
            this.editionRepo = editionRepository;
            this.collaboratorRepo = collaboratorRepository;
            this.workDedicationRepo = workDedicationRepository;
            this.fileRepo = fileRepository;
            this.innovationOrganizationExperienceOptionRepo = innovationOrganizationExperienceOptionRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
            this.innovationOrganizationTechnologyOptionRepo = innovationOrganizationTechnologyOptionRepository;
            this.innovationOrganizationObjectivesOptionRepo = innovationOrganizationObjectivesOptionRepository;
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

            var editionDto = await editionRepo.FindDtoAsync(cmd.EditionId ?? 0);
            if (editionDto?.Edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Edition, Labels.FoundF), new string[] { "ToastrError" }));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            if (editionDto.IsInnovationProjectSubmitEnded())
            {
                this.ValidationResult.Add(new ValidationError(Messages.ProjectSubmitPeriodClosed, new string[] { "ToastrError" }));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            //var workDedication = await workDedicationRepo.FindByUidAsync(cmd.WorkDedicationUid);
            //if (workDedication == null)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.WorkDedication, Labels.FoundF), new string[] { "ToastrError" }));
            //}

            var existentAttendeeInnovationOrganization = await attendeeInnovationOrganizationRepo.FindByDocumentAndEditionIdAsync(cmd.Document, cmd.EditionId.Value);
            if (existentAttendeeInnovationOrganization != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.Startup, Labels.Document, cmd.Document), new string[] { "ToastrError" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            cmd.InnovationOrganizationExperienceOptionApiDtos = cmd.InnovationOrganizationExperienceOptionApiDtos.Select(ta =>
                                                                    new InnovationOrganizationExperienceOptionApiDto()
                                                                    {
                                                                        Uid = ta.Uid,
                                                                        AdditionalInfo = ta.AdditionalInfo,
                                                                        InnovationOrganizationExperienceOption = this.innovationOrganizationExperienceOptionRepo.FindByUid(ta.Uid)
                                                                    }).ToList();

            cmd.InnovationOrganizationTrackOptionApiDtos = cmd.InnovationOrganizationTrackOptionApiDtos.Select(ta =>
                                                                new InnovationOrganizationTrackOptionApiDto()
                                                                {
                                                                    Uid = ta.Uid,
                                                                    AdditionalInfo = ta.AdditionalInfo,
                                                                    InnovationOrganizationTrackOption = this.innovationOrganizationTrackOptionRepo.FindByUid(ta.Uid)
                                                                }).ToList();

            cmd.InnovationOrganizationObjectivesOptionApiDtos = cmd.InnovationOrganizationObjectivesOptionApiDtos.Select(ta =>
                                                                   new InnovationOrganizationObjectivesOptionApiDto()
                                                                   {
                                                                       Uid = ta.Uid,
                                                                       AdditionalInfo = ta.AdditionalInfo,
                                                                       InnovationOrganizationObjectivesOption = this.innovationOrganizationObjectivesOptionRepo.FindByUid(ta.Uid)
                                                                   }).ToList();

            cmd.InnovationOrganizationTechnologyOptionApiDtos = cmd.InnovationOrganizationTechnologyOptionApiDtos.Select(ta =>
                                                                    new InnovationOrganizationTechnologyOptionApiDto()
                                                                    {
                                                                        Uid = ta.Uid,
                                                                        AdditionalInfo = ta.AdditionalInfo,
                                                                        InnovationOrganizationTechnologyOption = this.innovationOrganizationTechnologyOptionRepo.FindByUid(ta.Uid)
                                                                    }).ToList();

            cmd.AttendeeInnovationOrganizationFounderApiDtos = cmd.AttendeeInnovationOrganizationFounderApiDtos.Select(ta =>
                                                                    new AttendeeInnovationOrganizationFounderApiDto()
                                                                    {
                                                                       Curriculum = ta.Curriculum,
                                                                       FullName = ta.FullName,
                                                                       WorkDedication = this.workDedicationRepo.FindByUid(ta.WorkDedicationUid)   
                                                                    }).ToList();


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
                    editionDto.Id,
                    editionDto.Uid,
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
                   editionDto.Edition,
                   collaborator.GetAttendeeCollaboratorByEditionId(editionDto.Id),
                   cmd.Name,
                   cmd.Document,
                   cmd.ServiceName,
                   cmd.FoundationDate,
                   cmd.Description,
                   cmd.Website,
                   cmd.AccumulatedRevenue,
                   cmd.BusinessDefinition,
                   cmd.BusinessFocus,
                   cmd.MarketSize,
                   cmd.BusinessEconomicModel,
                   cmd.BusinessOperationalModel,
                   cmd.BusinessDifferentials,
                   cmd.BusinessStage,
                   !string.IsNullOrEmpty(cmd.PresentationFile),
                   cmd.AttendeeInnovationOrganizationFounderApiDtos,
                   cmd.AttendeeInnovationOrganizationCompetitorApiDtos,
                   cmd.InnovationOrganizationExperienceOptionApiDtos,
                   cmd.InnovationOrganizationObjectivesOptionApiDtos,
                   cmd.InnovationOrganizationTechnologyOptionApiDtos,
                   cmd.InnovationOrganizationTrackOptionApiDtos,
                   cmd.UserId);

            if (!innovationOrganization.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganization.ValidationResult);
                return this.AppValidationResult;
            }

            this.InnovationOrganizationRepo.Create(innovationOrganization);
            this.Uow.SaveChanges();

            this.AppValidationResult.Data = innovationOrganization;

            if (!string.IsNullOrEmpty(cmd.PresentationFile))
            {
                var fileBytes = Convert.FromBase64String(cmd.PresentationFile);
                this.fileRepo.Upload(
                    new MemoryStream(fileBytes),
                    FileMimeType.Pdf,
                    innovationOrganization.Uid + FileType.Pdf,
                    FileRepositoryPathType.InnovationOrganizationPresentationFile);
            }

            return this.AppValidationResult;
        }
    }
}