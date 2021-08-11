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
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
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
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

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
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository
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
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
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
                       cmd.AttendeeInnovationOrganizationFounderApiDtos,
                       cmd.AttendeeInnovationOrganizationCompetitorApiDtos,
                       cmd.InnovationOrganizationExperienceOptionApiDtos,
                       cmd.InnovationOrganizationObjectivesOptionApiDtos,
                       cmd.InnovationOrganizationTechnologyOptionApiDtos,
                       cmd.InnovationOrganizationTrackOptionApiDtos,
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
                       !string.IsNullOrEmpty(cmd.ImageFile), //TODO: cmd.CropperImage?.ImageFile != null
                       string.IsNullOrEmpty(cmd.ImageFile),  //TODO: cmd.CropperImage?.IsImageDeleted == true
                       cmd.AttendeeInnovationOrganizationFounderApiDtos,
                       cmd.AttendeeInnovationOrganizationCompetitorApiDtos,
                       cmd.InnovationOrganizationExperienceOptionApiDtos,
                       cmd.InnovationOrganizationObjectivesOptionApiDtos,
                       cmd.InnovationOrganizationTechnologyOptionApiDtos,
                       cmd.InnovationOrganizationTrackOptionApiDtos,
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
                    FileMimeType.Pdf,
                    innovationOrganization.GetAttendeeInnovationOrganizationByEditionId(editionDto.Edition.Id).Uid + FileType.Pdf,
                    FileRepositoryPathType.InnovationOrganizationPresentationFile);
            }

            //Uploads the Image
            if (!string.IsNullOrEmpty(cmd.ImageFile))
            {

                var fileBytes = Convert.FromBase64String(cmd.ImageFile);
                this.fileRepo.Upload(
                    new MemoryStream(fileBytes),
                    FileMimeType.Png,
                    innovationOrganization.GetAttendeeInnovationOrganizationByEditionId(editionDto.Edition.Id).Uid + FileType.Pdf,
                    FileRepositoryPathType.OrganizationImage);

                //TODO: Should be used ImageHelper instead of fileRepo!
                //ImageHelper.UploadOriginalAndCroppedImages(
                //   innovationOrganization.Uid,
                //   cmd.ImageFile,
                //   200,//cmd.CropperImage.DataX,
                //   200,//cmd.CropperImage.DataY,
                //   200,//cmd.CropperImage.DataWidth,
                //   200,//cmd.CropperImage.DataHeight,
                //   FileRepositoryPathType.OrganizationImage);
            }

            return this.AppValidationResult;
        }
    }
}