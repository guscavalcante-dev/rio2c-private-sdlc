// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-07-2023
// ***********************************************************************
// <copyright file="SpeakersApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using Swashbuckle.Swagger.Annotations;
using AppValidationResult = PlataformaRio2C.Application.AppValidationResult;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// Class for speakers endpoints
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0")]
    public class SpeakersApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        private readonly ICollaboratorRepository collaboratorRepo;
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakersApiController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public SpeakersApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            ICollaboratorRepository collaboratorRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IFileRepository fileRepository)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
            this.collaboratorRepo = collaboratorRepository;
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.fileRepo = fileRepository;
        }

        #region List

        /// <summary>
        /// Get the Speakers
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("speakers"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Speakers([FromUri] SpeakersApiRequest request)
        {
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

            var collaboratorsApiDtos = await this.collaboratorRepo.FindAllSpeakersApiPaged(
                edition.Id,
                request?.Keywords,
                request?.Highlights,
                request?.ConferencesUids?.ToListNullableGuid(','),
                request?.ConferencesDates?.ToListNullableDateTimeOffset(',', "yyyy-MM-dd", true),
                request?.ConferencesRoomsUids?.ToListNullableGuid(','),
                Domain.Constants.CollaboratorType.Speaker,
                request?.Page ?? 1,
                request?.PageSize ?? 10);

            return await Json(new SpeakersApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = collaboratorsApiDtos.HasPreviousPage,
                HasNextPage = collaboratorsApiDtos.HasNextPage,
                TotalItemCount = collaboratorsApiDtos.TotalItemCount,
                PageCount = collaboratorsApiDtos.PageCount,
                PageNumber = collaboratorsApiDtos.PageNumber,
                PageSize = collaboratorsApiDtos.PageSize,
                Speakers = collaboratorsApiDtos?.Select(c => new SpeakerListApiItem
                {
                    Uid = c.Uid,
                    BadgeName = c.BadgeName?.Trim(),
                    Name = c.Name?.Trim(),
                    HighlightPosition = c.ApiHighlightPosition,
                    Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true, "_500x500") : null,
                    JobTitle = c.GetCollaboratorJobTitleBaseDtoByLanguageCode(requestLanguage?.Code ?? defaultLanguage?.Code)?.Value?.Trim(),
                    Conferences = c.ConferencesDtos.Select(co => new SpeakerConferenceBaseApiResponse
                    {
                        Uid = co.Uid,
                        Date = co.StartDate.ToBrazilTimeZone().ToString("yyyy-MM-dd"),
                        StartTime = co.StartDate.ToBrazilTimeZone().ToString("HH:mm"),
                        EndTime = co.EndDate.ToBrazilTimeZone().ToString("HH:mm"),
                        Title = co.GetConferenceTitleDtoByLanguageCode(requestLanguage?.Code)?.ConferenceTitle?.Value?.Trim() ??
                                co.GetConferenceTitleDtoByLanguageCode(defaultLanguage?.Code)?.ConferenceTitle?.Value?.Trim(),
                        Room = co.RoomDto != null ? new RoomBaseApiResponse
                        {
                            Uid = co.RoomDto.Uid,
                            Name = co.RoomDto.GetRoomNameByLanguageCode(requestLanguage?.Code ?? defaultLanguage?.Code)?.RoomName?.Value?.Trim()
                        } : null,

                    })?.ToList()
                })?.ToList()
            });
        }

        #endregion

        #region Report

        /// <summary>
        /// Send the Speakers report by email
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("speakersReport"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> SpeakersReport([FromUri] SpeakersReportApiRequest request)
        {
            var validationResult = new AppValidationResult();

            try
            {
                #region Initial Validations

                if (request?.Key?.ToLowerInvariant() != ConfigurationManager.AppSettings["SendSpeakersReportApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

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

                // Get default application user
                var applicationUser = await identityController.FindByEmailAsync(Domain.Entities.User.BatchProcessUser.Email);
                if (applicationUser == null)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = Messages.UserNotFound } });
                }

                if (!string.IsNullOrEmpty(request?.Culture))
                {
                    var requestCulture = new CultureInfo(request.Culture);
                    Thread.CurrentThread.CurrentCulture = requestCulture;
                    Thread.CurrentThread.CurrentUICulture = requestCulture;
                    CultureInfo.DefaultThreadCurrentCulture = requestCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = requestCulture;
                }

                #endregion

                validationResult = await this.commandBus.Send(new SendSpeakersReport(
                        request.SendToEmails.ToArray(),
                        applicationUser.Id,
                        applicationUser.Uid,
                        edition.Id,
                        edition.Uid,
                        requestLanguage?.Code ?? defaultLanguage?.Code));
            }
            catch (DomainException ex)
            {
                return await Json(new
                {
                    status = ApiStatus.Error,
                    message = $"{ex.Message}{(ex.InnerException != null ? " - " + ex.GetInnerMessage() : "")}",
                    errors = validationResult?.Errors?.Select(e => new { e.Code, e.Message })
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }

            return await Json(new ApiBaseResponse
            {
                Status = ApiStatus.Success,
                Message = string.Format(Messages.EntitySentSuccessfullyToEmails, Labels.SpeakersReport, request.SendToEmails.ToString())
            });
        }

        #endregion

        #region Details

        /// <summary>
        /// Get the Speaker details
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("speaker/{uid?}"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Speaker([FromUri] SpeakerApiRequest request)
        {
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

            var speakerCollaboratorApiDto = await this.collaboratorRepo.FindSpeakerApi(
                request?.Uid ?? Guid.Empty,
                edition.Id,
                Domain.Constants.CollaboratorType.Speaker);
            if (speakerCollaboratorApiDto == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = "Speaker not found." } });
            }

            #region Social networks

            var socialNetworks = new List<SpeakerSocialNetworkApiResponse>();

            if (!string.IsNullOrEmpty(speakerCollaboratorApiDto.Linkedin))
            {
                socialNetworks.Add(new SpeakerSocialNetworkApiResponse { Slug = "LinkedIn", Url = speakerCollaboratorApiDto.Linkedin.GetLinkedinUrl() });
            }

            if (!string.IsNullOrEmpty(speakerCollaboratorApiDto.Twitter))
            {
                socialNetworks.Add(new SpeakerSocialNetworkApiResponse { Slug = "Twitter", Url = speakerCollaboratorApiDto.Twitter.GetTwitterUrl() });
            }

            if (!string.IsNullOrEmpty(speakerCollaboratorApiDto.Instagram))
            {
                socialNetworks.Add(new SpeakerSocialNetworkApiResponse { Slug = "Instagram", Url = speakerCollaboratorApiDto.Instagram.GetInstagramUrl() });
            }

            if (!string.IsNullOrEmpty(speakerCollaboratorApiDto.Youtube))
            {
                socialNetworks.Add(new SpeakerSocialNetworkApiResponse { Slug = "YouTube", Url = speakerCollaboratorApiDto.Youtube.GetUrlWithProtocol() });
            }

            #endregion

            return await Json(new SpeakerApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                Uid = speakerCollaboratorApiDto.Uid,
                BadgeName = speakerCollaboratorApiDto.BadgeName?.Trim(),
                Name = speakerCollaboratorApiDto.Name?.Trim(),
                HighlightPosition = speakerCollaboratorApiDto.ApiHighlightPosition,
                Picture = speakerCollaboratorApiDto.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, speakerCollaboratorApiDto.Uid, speakerCollaboratorApiDto.ImageUploadDate, true, "_500x500") : null,
                MiniBio = speakerCollaboratorApiDto.GetCollaboratorMiniBioBaseDtoByLanguageCode(request?.Culture)?.Value?.Trim(),
                JobTitle = speakerCollaboratorApiDto.GetCollaboratorJobTitleBaseDtoByLanguageCode(request?.Culture)?.Value?.Trim(),
                Site = speakerCollaboratorApiDto.Website?.GetUrlWithProtocol(),
                SocialNetworks = socialNetworks,
                Tracks = speakerCollaboratorApiDto.TracksDtos?.Select(td => new TrackBaseApiResponse
                {
                    Uid = td.Track.Uid,
                    Name = td.Track.GetNameByLanguageCode(request?.Culture),
                    Color = td.Track.Color
                })?.OrderBy(t => t.Name)?.ToList(),
                Companies = speakerCollaboratorApiDto.OrganizationsDtos?.Select(od => new SpeakerOrganizationApiResponse
                {
                    Uid = od.Uid,
                    TradeName = od.TradeName,
                    CompanyName = od.CompanyName,
                    Picture = od.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, od.Uid, od.ImageUploadDate, true) : null
                })?.OrderBy(c => c.TradeName)?.ToList(),
                Conferences = speakerCollaboratorApiDto.ConferencesDtos?.Select(c => new SpeakerConferenceApiResponse
                {
                    Uid = c.Conference.Uid,
                    Event = new EditionEventBaseApiResponse
                    {
                        Uid = c.EditionEvent.Uid,
                        Name = c.EditionEvent.Name.GetSeparatorTranslation(requestLanguage?.Code ?? defaultLanguage?.Code, Language.Separator)?.Trim()
                    },
                    Title = c.GetConferenceTitleDtoByLanguageCode(requestLanguage?.Code)?.ConferenceTitle?.Value?.Trim() ??
                            c.GetConferenceTitleDtoByLanguageCode(defaultLanguage?.Code)?.ConferenceTitle?.Value?.Trim(),
                    Synopsis = c.GetConferenceSynopsisDtoByLanguageCode(requestLanguage?.Code)?.ConferenceSynopsis?.Value?.Trim() ??
                               c.GetConferenceSynopsisDtoByLanguageCode(defaultLanguage?.Code)?.ConferenceSynopsis?.Value?.Trim(),
                    Date = c.Conference.StartDate.ToBrazilTimeZone().ToString("yyyy-MM-dd"),
                    StartTime = c.Conference.StartDate.ToBrazilTimeZone().ToString("HH:mm"),
                    EndTime = c.Conference.EndDate.ToBrazilTimeZone().ToString("HH:mm"),
                    DurationMinutes = (int)((c.Conference.EndDate - c.Conference.StartDate).TotalMinutes),
                    Room = c.RoomDto != null ? new RoomBaseApiResponse
                    {
                        Uid = c.RoomDto.Room.Uid,
                        Name = c.RoomDto.GetRoomNameByLanguageCode(requestLanguage?.Code ?? defaultLanguage?.Code)?.RoomName?.Value?.Trim()
                    } : null
                })?.ToList()
            });
        }

        #endregion
    }
}