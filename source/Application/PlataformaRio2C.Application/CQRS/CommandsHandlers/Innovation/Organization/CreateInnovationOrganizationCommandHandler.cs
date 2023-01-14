// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-14-2023
// ***********************************************************************
// <copyright file="CreateInnovationOrganizationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>
    /// CreateInnovationOrganizationCommandHandler
    /// </summary>
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
        private readonly IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository innovationOrganizationSustainableDevelopmentObjectivesOptionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganizationCommandHandler" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="innovationOrganizationRepository">The innovation organization repo.</param>
        /// <param name="attendeeInnovationOrganizationRepository">The attendee innovation organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="collaboratorRepository">The collaborator repo.</param>
        /// <param name="workDedicationRepository">The work dedication repo.</param>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="innovationOrganizationExperienceOptionRepository">The innovation organization experience option repository.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        /// <param name="innovationOrganizationTechnologyOptionRepository">The innovation organization technology option repository.</param>
        /// <param name="innovationOrganizationObjectivesOptionRepository">The innovation organization objectives option repository.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOptionRepository">The innovation sustainable development objectives option repository.</param>
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
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository,
            IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository innovationOrganizationSustainableDevelopmentObjectivesOptionRepository
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
            this.innovationOrganizationSustainableDevelopmentObjectivesOptionRepo = innovationOrganizationSustainableDevelopmentObjectivesOptionRepository;
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

            var existentAttendeeInnovationOrganization = await attendeeInnovationOrganizationRepo.FindByDocumentAndEditionIdAsync(cmd.Document, cmd.EditionId.Value);
            if (existentAttendeeInnovationOrganization != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.Startup, Labels.Document, cmd.Document), new string[] { "ToastrError" }));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            var listsValidationResult = this.UpdateListsAndValidate(cmd);
            if (!listsValidationResult.IsValid)
            {
                this.AppValidationResult.Add(listsValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var collaboratorDto = await collaboratorRepo.FindByEmailAsync(cmd.Email, editionDto.Id);
            if (collaboratorDto == null)
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

                #endregion
            }
            else
            {
                #region Updates Collaborator and User

                var updateCollaboratorCommand = new UpdateTinyCollaborator(collaboratorDto, true);
                updateCollaboratorCommand.UpdateBaseProperties(
                    cmd.ResponsibleName,
                    null,
                    cmd.Email,
                    cmd.PhoneNumber,
                    cmd.CellPhone,
                    cmd.Document);

                updateCollaboratorCommand.UpdatePreSendProperties(
                    CollaboratorType.Innovation.Name,
                    cmd.UserId,
                    cmd.UserUid,
                    editionDto.Id,
                    editionDto.Uid,
                    "");

                var commandResult = await base.CommandBus.Send(updateCollaboratorCommand);
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

                #endregion
            }
            collaboratorDto = await collaboratorRepo.FindByEmailAsync(cmd.Email, editionDto.Id);

            var innovationOrganization = await this.InnovationOrganizationRepo.FindByDocumentAsync(cmd.Document);
            if (innovationOrganization == null)
            {
                #region Creates new Innovation Organization

                innovationOrganization = new InnovationOrganization(
                       editionDto.Edition,
                       collaboratorDto.EditionAttendeeCollaborator,
                       cmd.Name,
                       cmd.Document,
                       cmd.ServiceName,
                       cmd.Description,
                       cmd.Website,
                       cmd.AccumulatedRevenue,
                       cmd.BusinessDefinition,
                       cmd.BusinessFocus,
                       cmd.MarketSize,
                       cmd.BusinessEconomicModel,
                       cmd.BusinessOperationalModel,
                       cmd.VideoUrl,
                       cmd.BusinessDifferentials,
                       cmd.BusinessStage,
                       !string.IsNullOrEmpty(cmd.PresentationFile),
                       !string.IsNullOrEmpty(cmd.ImageFile),
                       cmd.PresentationFile?.GetBase64FileExtension(),
                       cmd.AttendeeInnovationOrganizationFounderApiDtos,
                       cmd.AttendeeInnovationOrganizationCompetitorApiDtos,
                       cmd.InnovationOrganizationExperienceOptionApiDtos,
                       cmd.InnovationOrganizationObjectivesOptionApiDtos,
                       cmd.InnovationOrganizationTechnologyOptionApiDtos,
                       cmd.InnovationOrganizationTrackOptionApiDtos,
                       cmd.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos,
                       cmd.WouldYouLikeParticipateBusinessRound,
                       cmd.AccumulatedRevenueForLastTwelveMonths,
                       cmd.FoundationYear,
                       cmd.UserId);

                this.InnovationOrganizationRepo.Create(innovationOrganization);

                #endregion
            }
            else
            {
                #region Updates Innovation Organization

                innovationOrganization.Update(
                       editionDto.Edition,
                       collaboratorDto.EditionAttendeeCollaborator,
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
                       cmd.VideoUrl,
                       cmd.BusinessDifferentials,
                       cmd.BusinessStage,
                       !string.IsNullOrEmpty(cmd.PresentationFile),
                       !string.IsNullOrEmpty(cmd.ImageFile), 
                       string.IsNullOrEmpty(cmd.ImageFile),
                       cmd.PresentationFile?.GetBase64FileExtension(),
                       cmd.AttendeeInnovationOrganizationFounderApiDtos,
                       cmd.AttendeeInnovationOrganizationCompetitorApiDtos,
                       cmd.InnovationOrganizationExperienceOptionApiDtos,
                       cmd.InnovationOrganizationObjectivesOptionApiDtos,
                       cmd.InnovationOrganizationTechnologyOptionApiDtos,
                       cmd.InnovationOrganizationTrackOptionApiDtos,
                       cmd.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos,
                       cmd.WouldYouLikeParticipateBusinessRound,
                       cmd.AccumulatedRevenueForLastTwelveMonths,
                       cmd.FoundationYear,
                       cmd.UserId);

                this.InnovationOrganizationRepo.Update(innovationOrganization);

                #endregion
            }

            if (!innovationOrganization.IsValid())
            {
                this.AppValidationResult.Add(innovationOrganization.ValidationResult);
                return this.AppValidationResult;
            }

            var result = this.Uow.SaveChanges();

            if (!result.Success)
            {
                foreach (var validationResult in result.ValidationResults)
                {
                    this.ValidationResult.Add(validationResult.ErrorMessage);
                }

                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            this.AppValidationResult.Data = innovationOrganization;

            //Uploads the Presentation File
            if (!string.IsNullOrEmpty(cmd.PresentationFile))
            {
                var fileBytes = Convert.FromBase64String(cmd.PresentationFile);
                this.fileRepo.Upload(
                    new MemoryStream(fileBytes),
                    cmd.PresentationFile.GetBase64FileMimeType(),
                    innovationOrganization.GetAttendeeInnovationOrganizationByEditionId(editionDto.Edition.Id).Uid + cmd.PresentationFile.GetBase64FileExtension(),
                    FileRepositoryPathType.InnovationOrganizationPresentationFile);
            }

            //Uploads the Image
            if (!string.IsNullOrEmpty(cmd.ImageFile))
            {
                ImageHelper.UploadOriginalAndThumbnailImages(
                   innovationOrganization.Uid,
                   cmd.ImageFile,
                   FileRepositoryPathType.OrganizationImage);
            }

            return this.AppValidationResult;
        }

        /// <summary>
        /// Updates the lists and validate.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        private ValidationResult UpdateListsAndValidate(CreateInnovationOrganization cmd)
        {
            ValidationResult validationResult = new ValidationResult();

            // ---------------------------------------------------
            // InnovationOrganizationExperienceOptionApiDtos
            // ---------------------------------------------------
            cmd.InnovationOrganizationExperienceOptionApiDtos = cmd.InnovationOrganizationExperienceOptionApiDtos?.Select(dto =>
                                                                    new InnovationOrganizationExperienceOptionApiDto()
                                                                    {
                                                                        Uid = dto.Uid,
                                                                        AdditionalInfo = dto.AdditionalInfo,
                                                                        InnovationOrganizationExperienceOption = this.innovationOrganizationExperienceOptionRepo.FindByUid(dto.Uid)
                                                                    }).ToList();

            if (cmd.InnovationOrganizationExperienceOptionApiDtos.Any(dto => dto.InnovationOrganizationExperienceOption == null))
            {
                var uidsNotFound = cmd.InnovationOrganizationExperienceOptionApiDtos.Where(dto => dto.InnovationOrganizationExperienceOption == null).Select(dto => dto.Uid);

                validationResult.Add(new ValidationError(string.Format(
                    Messages.EntityNotAction,
                    $@"{InnovationOrganizationApiDto.GetJsonPropertyAttributeName(nameof(InnovationOrganizationApiDto.InnovationOrganizationExperienceOptionApiDtos))}: {string.Join(", ", uidsNotFound)}",
                    Labels.FoundM)));
            }

            // ---------------------------------------------------
            // InnovationOrganizationTrackOptionApiDtos
            // ---------------------------------------------------
            cmd.InnovationOrganizationTrackOptionApiDtos = cmd.InnovationOrganizationTrackOptionApiDtos?.Select(dto =>
                                                                new InnovationOrganizationTrackOptionApiDto()
                                                                {
                                                                    Uid = dto.Uid,
                                                                    AdditionalInfo = dto.AdditionalInfo,
                                                                    InnovationOrganizationTrackOption = this.innovationOrganizationTrackOptionRepo.FindByUid(dto.Uid ?? Guid.Empty)
                                                                }).ToList();

            if (cmd.InnovationOrganizationTrackOptionApiDtos.Any(dto => dto.InnovationOrganizationTrackOption == null))
            {
                var uidsNotFound = cmd.InnovationOrganizationTrackOptionApiDtos.Where(dto => dto.InnovationOrganizationTrackOption == null).Select(dto => dto.Uid);

                validationResult.Add(new ValidationError(string.Format(
                    Messages.EntityNotAction,
                    $@"{InnovationOrganizationApiDto.GetJsonPropertyAttributeName(nameof(InnovationOrganizationApiDto.InnovationOrganizationTrackOptionApiDtos))}: {string.Join(", ", uidsNotFound)}",
                    Labels.FoundM)));
            }

            // ---------------------------------------------------
            // InnovationOrganizationObjectivesOptionApiDtos 
            // ---------------------------------------------------
            cmd.InnovationOrganizationObjectivesOptionApiDtos = cmd.InnovationOrganizationObjectivesOptionApiDtos?.Select(dto =>
                                                                   new InnovationOrganizationObjectivesOptionApiDto()
                                                                   {
                                                                       Uid = dto.Uid,
                                                                       AdditionalInfo = dto.AdditionalInfo,
                                                                       InnovationOrganizationObjectivesOption = this.innovationOrganizationObjectivesOptionRepo.FindByUid(dto.Uid)
                                                                   }).ToList();

            if (cmd.InnovationOrganizationObjectivesOptionApiDtos.Any(dto => dto.InnovationOrganizationObjectivesOption == null))
            {
                var uidsNotFound = cmd.InnovationOrganizationObjectivesOptionApiDtos.Where(dto => dto.InnovationOrganizationObjectivesOption == null).Select(dto => dto.Uid);

                validationResult.Add(new ValidationError(string.Format(
                    Messages.EntityNotAction,
                    $@"{InnovationOrganizationApiDto.GetJsonPropertyAttributeName(nameof(InnovationOrganizationApiDto.InnovationOrganizationObjectivesOptionApiDtos))}: {string.Join(", ", uidsNotFound)}",
                    Labels.FoundM)));
            }

            // --------------------------------------------------------------------
            // InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos 
            // --------------------------------------------------------------------
            cmd.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos = cmd.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos?.Select(dto =>
                                                                   new InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto()
                                                                   {
                                                                       Uid = dto.Uid,
                                                                       AdditionalInfo = dto.AdditionalInfo,
                                                                       InnovationOrganizationSustainableDevelopmentObjectivesOption = this.innovationOrganizationSustainableDevelopmentObjectivesOptionRepo.FindByUid(dto.Uid)
                                                                   }).ToList();

            if (cmd.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos.Any(dto => dto.InnovationOrganizationSustainableDevelopmentObjectivesOption == null))
            {
                var uidsNotFound = cmd.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos.Where(dto => dto.InnovationOrganizationSustainableDevelopmentObjectivesOption == null).Select(dto => dto.Uid);

                validationResult.Add(new ValidationError(string.Format(
                    Messages.EntityNotAction,
                    $@"{InnovationOrganizationApiDto.GetJsonPropertyAttributeName(nameof(InnovationOrganizationApiDto.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos))}: {string.Join(", ", uidsNotFound)}",
                    Labels.FoundM)));
            }

            // ---------------------------------------------------
            // InnovationOrganizationTechnologyOptionApiDtos
            // ---------------------------------------------------
            cmd.InnovationOrganizationTechnologyOptionApiDtos = cmd.InnovationOrganizationTechnologyOptionApiDtos?.Select(dto =>
                                                                    new InnovationOrganizationTechnologyOptionApiDto()
                                                                    {
                                                                        Uid = dto.Uid,
                                                                        AdditionalInfo = dto.AdditionalInfo,
                                                                        InnovationOrganizationTechnologyOption = this.innovationOrganizationTechnologyOptionRepo.FindByUid(dto.Uid)
                                                                    }).ToList();

            if (cmd.InnovationOrganizationTechnologyOptionApiDtos.Any(dto => dto.InnovationOrganizationTechnologyOption == null))
            {
                var uidsNotFound = cmd.InnovationOrganizationTechnologyOptionApiDtos.Where(dto => dto.InnovationOrganizationTechnologyOption == null).Select(dto => dto.Uid);

                validationResult.Add(new ValidationError(string.Format(
                    Messages.EntityNotAction,
                    $@"{InnovationOrganizationApiDto.GetJsonPropertyAttributeName(nameof(InnovationOrganizationApiDto.InnovationOrganizationTechnologyOptionApiDtos))}: {string.Join(", ", uidsNotFound)}",
                    Labels.FoundM)));
            }

            // ---------------------------------------------------
            // AttendeeInnovationOrganizationFounderApiDtos
            // ---------------------------------------------------
            cmd.AttendeeInnovationOrganizationFounderApiDtos = cmd.AttendeeInnovationOrganizationFounderApiDtos?.Select(dto =>
                                                                    new AttendeeInnovationOrganizationFounderApiDto()
                                                                    {
                                                                        Curriculum = dto.Curriculum,
                                                                        FullName = dto.FullName,
                                                                        WorkDedication = this.workDedicationRepo.FindByUid(dto.WorkDedicationUid)
                                                                    }).ToList();

            if (cmd.AttendeeInnovationOrganizationFounderApiDtos.Any(dto => dto.WorkDedication == null))
            {
                var uidsNotFound = cmd.AttendeeInnovationOrganizationFounderApiDtos.Where(dto => dto.WorkDedication == null).Select(dto => dto.WorkDedicationUid);

                validationResult.Add(new ValidationError(string.Format(
                    Messages.EntityNotAction,
                    $@"{Labels.WorkDedication}: {string.Join(", ", uidsNotFound)}",
                    Labels.FoundM)));
            }

            return validationResult;
        }
    }
}