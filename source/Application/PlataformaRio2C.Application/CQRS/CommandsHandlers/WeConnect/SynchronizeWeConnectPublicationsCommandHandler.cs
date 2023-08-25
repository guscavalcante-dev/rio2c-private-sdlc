// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-11-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-11-2023
// ***********************************************************************
// <copyright file="SynchronizeWeConnectPublicationsCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SynchronizeWeConnectPublicationsCommandHandler</summary>
    public class SynchronizeWeConnectPublicationsCommandHandler : BaseCommandHandler, IRequestHandler<SynchronizeWeConnectPublications, AppValidationResult>
    {
        private readonly ISocialMediaPlatformRepository socialMediaPlatformRepo;
        private readonly ISocialMediaPlatformServiceFactory socialMediaPlatformServiceFactory;
        private readonly IWeConnectPublicationRepository weConnectPublicationRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizeWeConnectPublicationsCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="socialMediaPlatformRepository">The social media platform repository.</param>
        /// <param name="socialMediaPlatformServiceFactory">The social media platform service factory.</param>
        /// <param name="weConnectPublicationRepository">The we connect publication repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public SynchronizeWeConnectPublicationsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ISocialMediaPlatformRepository socialMediaPlatformRepository,
            ISocialMediaPlatformServiceFactory socialMediaPlatformServiceFactory,
            IWeConnectPublicationRepository weConnectPublicationRepository,
            IFileRepository fileRepository)
            : base(eventBus, uow)
        {
            this.socialMediaPlatformRepo = socialMediaPlatformRepository;
            this.socialMediaPlatformServiceFactory = socialMediaPlatformServiceFactory;
            this.weConnectPublicationRepo = weConnectPublicationRepository;
            this.fileRepo = fileRepository;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        public async Task<AppValidationResult> Handle(SynchronizeWeConnectPublications cmd, CancellationToken cancellationToken)
        {
            base.Uow.BeginTransaction();

            var socialMediaPlatformDto = await this.socialMediaPlatformRepo.FindDtoByNameAsync(cmd.SocialMediaPlatformName);
            if (socialMediaPlatformDto == null)
            {
                throw new DomainException(string.Format("Social Media Platform {0} not found", cmd.SocialMediaPlatformName));
            }

            var socialMediaPlatformService = this.socialMediaPlatformServiceFactory.Get(socialMediaPlatformDto);
            var socialMediaPlatformPublicationDtos = socialMediaPlatformService.GetPosts();

            // Http client to download social media image or video
            HttpClientHandler handler = new HttpClientHandler { Credentials = new System.Net.NetworkCredential() };
            HttpClient client = new HttpClient(handler);

            foreach (var socialMediaPlatformPublicationDto in socialMediaPlatformPublicationDtos.OrderByDescending(dto => dto.CreatedAt))
            {
                var weConnectPublication = await this.weConnectPublicationRepo.FindBySocialMediaPlatformPublicationIdAsync(socialMediaPlatformPublicationDto.Id);
                if (weConnectPublication == null)
                {
                    weConnectPublication = new WeConnectPublication(
                        socialMediaPlatformPublicationDto.Id,
                        socialMediaPlatformPublicationDto.PublicationText,
                        socialMediaPlatformPublicationDto.IsVideo,
                        false,
                        !string.IsNullOrEmpty(socialMediaPlatformPublicationDto.PublicationMediaUrl),
                        socialMediaPlatformPublicationDto.CreatedAt,
                        await socialMediaPlatformRepo.FindByNameAsync(socialMediaPlatformDto.Name),
                        cmd.UserId);

                    if (!weConnectPublication.IsValid())
                    {
                        base.AppValidationResult.Add(weConnectPublication.ValidationResult);
                        return base.AppValidationResult;
                    }

                    this.weConnectPublicationRepo.Create(weConnectPublication);

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

                    this.AppValidationResult.Data = weConnectPublication;

                    // Uploads the Image or Video
                    if (!string.IsNullOrEmpty(socialMediaPlatformPublicationDto.PublicationMediaUrl))
                    {
                        var fileBytes = await client.GetByteArrayAsync(socialMediaPlatformPublicationDto.PublicationMediaUrl);
                        var fileBase64 = Convert.ToBase64String(fileBytes);

                        if (socialMediaPlatformPublicationDto.IsVideo == true)
                        {
                            this.fileRepo.Upload(
                                new MemoryStream(fileBytes),
                                FileMimeType.Mp4,
                                weConnectPublication.Uid + FileType.Mp4,
                                FileRepositoryPathType.WeConnectMediaFile);
                        }
                        else
                        {
                            ImageHelper.UploadOriginalAndThumbnailImages(
                                weConnectPublication.Uid,
                                fileBase64,
                                FileRepositoryPathType.WeConnectMediaFile);
                        }
                    }
                }
            }

            return base.AppValidationResult;
        }

    }
}