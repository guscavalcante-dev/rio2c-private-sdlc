// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2021
// ***********************************************************************
// <copyright file="CreateCartoonProjectCommandHandler.cs" company="Softo">
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
    /// CreateCartoonProjectCommandHandler
    /// </summary>
    public class CreateCartoonProjectCommandHandler : CartoonProjectBaseCommandHandler, IRequestHandler<CreateCartoonProject, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly ICartoonProjectFormatRepository cartoonProjectFormatRepo;
        private readonly IAttendeeCartoonProjectRepository attendeeCartoonProjectRepo;

        public CreateCartoonProjectCommandHandler(
            IMediator commandBus,
            IUnitOfWork uow,
            ICartoonProjectRepository cartoonProjectRepository,
            IEditionRepository editionRepository,
            ICollaboratorRepository collaboratorRepository,
            ICartoonProjectFormatRepository cartoonProjectFormatRepository,
            IAttendeeCartoonProjectRepository attendeeCartoonProjectRepository
            )
            : base(commandBus, uow, cartoonProjectRepository)
        {
            this.editionRepo = editionRepository;
            this.collaboratorRepo = collaboratorRepository;
            this.cartoonProjectFormatRepo = cartoonProjectFormatRepository;
            this.attendeeCartoonProjectRepo = attendeeCartoonProjectRepository;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateCartoonProject cmd, CancellationToken cancellationToken)
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

            if (editionDto.IsCartoonProjectSubmitEnded())
            {
                this.ValidationResult.Add(new ValidationError(Messages.ProjectSubmitPeriodClosed, new string[] { "ToastrError" }));
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            var existentAttendeeCartoonProject = await attendeeCartoonProjectRepo.FindByTitleAndEditionIdAsync(cmd.Title, cmd.EditionId.Value);
            if (existentAttendeeCartoonProject != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.Project, Labels.Title, cmd.Title), new string[] { "ToastrError" }));
            }

            var cartoonProjectFormat = this.cartoonProjectFormatRepo.FindByUid(cmd.CartoonProjectFormatUid);
            if (cartoonProjectFormat == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Format, Labels.FoundM, cmd.CartoonProjectFormatUid), new string[] { "ToastrError" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            //Don't worry about cartoonProjectCreatorApiDto null validation because its validated previously by CartoonProjectApiDto.ValidateCartoonProjectCreators().
            var cartoonProjectCreatorApiDto = cmd.CartoonProjectCreatorApiDtos.FirstOrDefault(i => i.IsResponsible);

            var collaboratorDto = await collaboratorRepo.FindByEmailAsync(cartoonProjectCreatorApiDto.Email, editionDto.Id);
            if (collaboratorDto == null)
            {
                #region Creates new Collaborator and User

                var createCollaboratorCommand = new CreateTinyCollaborator();
                createCollaboratorCommand.UpdateBaseProperties(
                    cartoonProjectCreatorApiDto.FirstName,
                    cartoonProjectCreatorApiDto.LastName,
                    cartoonProjectCreatorApiDto.Email,
                    cartoonProjectCreatorApiDto.PhoneNumber,
                    cartoonProjectCreatorApiDto.CellPhone,
                    cartoonProjectCreatorApiDto.Document);

                createCollaboratorCommand.UpdatePreSendProperties(
                    CollaboratorType.Cartoon.Name,
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
                    cartoonProjectCreatorApiDto.FirstName,
                    cartoonProjectCreatorApiDto.LastName,
                    cartoonProjectCreatorApiDto.Email,
                    cartoonProjectCreatorApiDto.PhoneNumber,
                    cartoonProjectCreatorApiDto.CellPhone,
                    cartoonProjectCreatorApiDto.Document);

                updateCollaboratorCommand.UpdatePreSendProperties(
                    CollaboratorType.Cartoon.Name,
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
            collaboratorDto = await collaboratorRepo.FindByEmailAsync(cartoonProjectCreatorApiDto.Email, editionDto.Id);

            var cartoonProject = await this.CartoonProjectRepo.FindByTitleAsync(cmd.Title);
            if (cartoonProject == null)
            {
                #region Creates new Cartoon Project

                cartoonProject = new CartoonProject(
                       editionDto.Edition,
                       cmd.Title,
                       cmd.Logline,
                       cmd.Summary,
                       cmd.Motivation,
                       cmd.ProductionPlan,
                       cmd.ProjectTeaserUrl,
                       cmd.ProjectBibleUrl,
                       cmd.NumberOfEpisodes,
                       cmd.EachEpisodePlayingTime,
                       cmd.TotalValueOfProject,
                       cartoonProjectFormat,
                       cmd.CartoonProjectCompanyApiDto,
                       cmd.CartoonProjectCreatorApiDtos,
                       cmd.UserId);

                this.CartoonProjectRepo.Create(cartoonProject);

                #endregion
            }
            else
            {
                #region Updates Cartoon Project

                cartoonProject.Update(
                       editionDto.Edition,
                       cmd.Title,
                       cmd.Logline,
                       cmd.Summary,
                       cmd.Motivation,
                       cmd.ProductionPlan,
                       cmd.ProjectTeaserUrl,
                       cmd.ProjectBibleUrl,
                       cmd.NumberOfEpisodes,
                       cmd.EachEpisodePlayingTime,
                       cmd.TotalValueOfProject,
                       cartoonProjectFormat,
                       cmd.CartoonProjectCompanyApiDto,
                       cmd.CartoonProjectCreatorApiDtos,
                       cmd.UserId);

                this.CartoonProjectRepo.Update(cartoonProject);

                #endregion
            }

            if (!cartoonProject.IsValid())
            {
                this.AppValidationResult.Add(cartoonProject.ValidationResult);
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

            this.AppValidationResult.Data = cartoonProject;

            //Uploads the Presentation File
            //if (!string.IsNullOrEmpty(cmd.PresentationFile))
            //{
            //    var fileBytes = Convert.FromBase64String(cmd.PresentationFile);
            //    this.fileRepo.Upload(
            //        new MemoryStream(fileBytes),
            //        cmd.PresentationFile.GetBase64FileMimeType(),
            //        innovationOrganization.GetAttendeeCartoonProjectByEditionId(editionDto.Edition.Id).Uid + cmd.PresentationFile.GetBase64FileExtension(),
            //        FileRepositoryPathType.CartoonProjectPresentationFile);
            //}

            //Uploads the Image
            //if (!string.IsNullOrEmpty(cmd.ImageFile))
            //{
            //    ImageHelper.UploadOriginalAndThumbnailImages(
            //       innovationOrganization.Uid,
            //       cmd.ImageFile,
            //       FileRepositoryPathType.OrganizationImage);
            //}

            return this.AppValidationResult;
        }
    }
}