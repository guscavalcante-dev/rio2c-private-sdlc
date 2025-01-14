// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 09-21-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-16-2023
// ***********************************************************************
// <copyright file="AudiovisualApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// Class for audiovisual endpoints
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0/audiovisual")]
    public class AudiovisualApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudiovisualApiController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public AudiovisualApiController(
            IMediator commandBus,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IFileRepository fileRepository)
        {
            this.commandBus = commandBus;
            this.collaboratorRepo = collaboratorRepository;
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>
        /// Get the Audiovisual Commission Members
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("commissions"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Commissions([FromUri] AudiovisualCommissionsApiRequest request)
        {
            #region Basic API Validations

            var editions = await this.editionRepo.FindAllByIsActiveAsync(false);
            if (editions?.Any() == false)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." } });
            }

            // Get edition from request otherwise get current
            var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
                                                              editions?.FirstOrDefault(e => e.IsCurrent);
            if (edition == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." } });
            }

            // Get language from request otherwise get default
            var languages = await this.languageRepo.FindAllDtosAsync();
            var requestLanguage = languages?.FirstOrDefault(l => l.Code == request?.Culture);
            var defaultLanguage = languages?.FirstOrDefault(l => l.IsDefault);
            if (requestLanguage == null && defaultLanguage == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "No active languages found." } });
            }

            #endregion

            var collaboratorDtos = await this.collaboratorRepo.FindAllAudiovisualCommissionMembersApiPaged(
                edition.Id,
                request?.Keywords,
                request?.Page ?? 1,
                request?.PageSize ?? 10);


            return await Json(new AudiovisualCommissionsApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = collaboratorDtos.HasPreviousPage,
                HasNextPage = collaboratorDtos.HasNextPage,
                TotalItemCount = collaboratorDtos.TotalItemCount,
                PageCount = collaboratorDtos.PageCount,
                PageNumber = collaboratorDtos.PageNumber,
                PageSize = collaboratorDtos.PageSize,
                Commissions = collaboratorDtos?.Select(c => new AudiovisualCommissionListApiItem
                {
                    Uid = c.Uid,
                    Name = c.FullName?.Trim(),
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true, "_500x500") : null,
                    JobTitle = c.GetCollaboratorJobTitleBaseDtoByLanguageCode(requestLanguage?.Code ?? defaultLanguage?.Code)?.Value?.Trim(),
                    OrganizationsNames = c.AttendeeOrganizationBasesDtos?.Select(ao => ao.OrganizationBaseDto?.Name ?? "-")?.ToString(", ")
                })?.ToList()
            });
        }

        #endregion

        #region Details

        /// <summary>
        /// Get the Audiovisual Comission Member details
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("commissions/details/{uid?}"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> CommissionDetails([FromUri] AudiovisualCommissionApiRequest request)
        {
            #region Basic API Validations

            var editions = await this.editionRepo.FindAllByIsActiveAsync(false);
            if (editions?.Any() == false)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." } });
            }

            // Get edition from request otherwise get current
            var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
                                                              editions?.FirstOrDefault(e => e.IsCurrent);
            if (edition == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." } });
            }

            // Get language from request otherwise get default
            var languages = await this.languageRepo.FindAllDtosAsync();
            var requestLanguage = languages?.FirstOrDefault(l => l.Code == request?.Culture);
            var defaultLanguage = languages?.FirstOrDefault(l => l.IsDefault);
            if (requestLanguage == null && defaultLanguage == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00003", Message = "No active languages found." } });
            }

            #endregion

            var collaboratorDto = await this.collaboratorRepo.FindAudiovisualCommissionMemberApi(
                request?.Uid ?? Guid.Empty,
                edition.Id);
            if (collaboratorDto == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = "Commission Member not found." } });
            }

            return await Json(new AudiovisualCommissionApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                Uid = collaboratorDto.Uid,
                Name = collaboratorDto.FullName,
                Picture = collaboratorDto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, collaboratorDto.Uid, collaboratorDto.ImageUploadDate, true, "_500x500") : null,
                JobTitle = collaboratorDto.GetCollaboratorJobTitleBaseDtoByLanguageCode(requestLanguage?.Code ?? defaultLanguage?.Code)?.Value?.Trim(),
                OrganizationsNames = collaboratorDto.AttendeeOrganizationBasesDtos.Select(ao => ao.OrganizationBaseDto.Name ?? "-")?.ToString(", "),
                MiniBio = collaboratorDto.GetMiniBioBaseDtoByLanguageCode(request.Culture ?? defaultLanguage.Code)?.Value?.Trim()
            });
        }

        #endregion
    }
}