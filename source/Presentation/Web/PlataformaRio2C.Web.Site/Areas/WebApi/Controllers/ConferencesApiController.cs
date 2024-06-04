// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-08-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-04-2024
// ***********************************************************************
// <copyright file="ConferencesApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PlataformaRio2c.Infra.Data.FileRepository;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using Swashbuckle.Swagger.Annotations;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// Class for conferences endpoints
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0")]
    public class ConferencesApiController : BaseApiController
    {
        private readonly IConferenceRepository conferenceRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IEditionEventRepository editionEventRepo;
        private readonly IRoomRepository roomRepo;
        private readonly ITrackRepository trackRepo;
        private readonly IPillarRepository pillarRepo;
        private readonly IPresentationFormatRepository presentationFormatRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IFileRepository fileRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConferencesApiController" /> class.
        /// </summary>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="editionEventRepository">The edition event repository.</param>
        /// <param name="roomRepository">The room repository.</param>
        /// <param name="trackRepository">The track repository.</param>
        /// <param name="pillarRepo">The pillar repo.</param>
        /// <param name="presentationFormatRepository">The presentation format repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="fileRepository">The file repository.</param>
        public ConferencesApiController(
            IConferenceRepository conferenceRepository,
            IEditionRepository editionRepository,
            IEditionEventRepository editionEventRepository,
            IRoomRepository roomRepository,
            ITrackRepository trackRepository,
            IPillarRepository pillarRepo,
            IPresentationFormatRepository presentationFormatRepository,
            ILanguageRepository languageRepository,
            IFileRepository fileRepository)
        {
            this.conferenceRepo = conferenceRepository;
            this.editionRepo = editionRepository;
            this.editionEventRepo = editionEventRepository;
            this.roomRepo = roomRepository;
            this.trackRepo = trackRepository;
            this.presentationFormatRepo = presentationFormatRepository;
            this.languageRepo = languageRepository;
            this.fileRepo = fileRepository;
            this.pillarRepo = pillarRepo;
        }

        #region List

        /// <summary>
        /// Get the Conferences
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("conferences"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Conferences([FromUri]ConferencesApiRequest request)
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

            var conferenceDtos = await this.conferenceRepo.FindAllPublicApiPaged(
                edition.Id,
                request?.Keywords,
                request?.EditionDates?.ToListDateTimeOffset(',', "yyyy-MM-dd", true),
                request?.EventsUids?.ToListGuid(','),
                request?.RoomsUids?.ToListGuid(','),
                request?.TracksUids?.ToListGuid(','),
                request?.PillarsUids?.ToListGuid(','),
                request?.PresentationFormatsUids?.ToListGuid(','),
                request?.ModifiedAfterDate.ToUtcDateKind(),
                request?.ShowDeleted ?? false,
                request?.Page ?? 1,
                request?.PageSize ?? 10);

            return await Json(new ConferencesApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                HasPreviousPage = conferenceDtos.HasPreviousPage,
                HasNextPage = conferenceDtos.HasNextPage,
                TotalItemCount = conferenceDtos.TotalItemCount,
                PageCount = conferenceDtos.PageCount,
                PageNumber = conferenceDtos.PageNumber,
                PageSize = conferenceDtos.PageSize,
                Conferences = conferenceDtos?.Select(c => new ConferencesApiListItem
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
                    DurationMinutes = (int) ((c.Conference.EndDate - c.Conference.StartDate).TotalMinutes),
                    IsDeleted = c.Conference.IsDeleted,
                    Room = c.RoomDto != null ? new RoomBaseApiResponse
                    {
                        Uid = c.RoomDto.Room.Uid,
                        Name = c.RoomDto.GetRoomNameByLanguageCode(requestLanguage?.Code ?? defaultLanguage?.Code)?.RoomName?.Value?.Trim()
                    } : null,
                    Pillars = c.ConferencePillarDtos?.Select(ctd => new PillarBaseApiResponse()
                    {
                        Uid = ctd.Pillar.Uid,
                        Name = ctd.Pillar.Name?.GetSeparatorTranslation(requestLanguage?.Code ?? defaultLanguage?.Code, Language.Separator)?.Trim(),
                        Color = ctd.Pillar.Color
                    })?.ToList(),
                    Tracks = c.ConferenceTrackDtos?.Select(ctd => new TrackBaseApiResponse
                    {
                        Uid = ctd.Track.Uid,
                        Name = ctd.Track.Name?.GetSeparatorTranslation(requestLanguage?.Code ?? defaultLanguage?.Code, Language.Separator)?.Trim(),
                        Color = ctd.Track.Color
                    })?.ToList(),
                    PresentationFormats = c.ConferencePresentationFormatDtos?.Select(cpfd => new PresentationFormatBaseApiResponse
                    {
                        Uid = cpfd.PresentationFormat.Uid,
                        Name = cpfd.PresentationFormat.Name?.GetSeparatorTranslation(requestLanguage?.Code ?? defaultLanguage?.Code, Language.Separator)?.Trim()
                    })?.ToList()
                })?.ToList()
            });
        }

        /// <summary>
        /// Get the Conferences API filters
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("conferences/filters"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Filters([FromUri]ConferencesFiltersApiRequest request)
        {
            try
            {
                #region Initial Validations

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

                #endregion

                var editionEvents = await this.editionEventRepo.FindAllByEditionIdAsync(edition.Id);
                var roomDtos = await this.roomRepo.FindAllDtoByEditionIdAsync(edition.Id);
                var tracks = await this.trackRepo.FindAllByEditionIdAsync(edition.Id);
                var presentationFormats = await this.presentationFormatRepo.FindAllByEditionIdAsync(edition.Id);
                var pillars = await this.pillarRepo.FindAllByEditionIdAsync(edition.Id);

                return await Json(new ConferencesFiltersApiResponse
                {
                    Status = ApiStatus.Success,
                    Error = null,
                    EditionDates = Enumerable.Range(0, 1 + edition.EndDate.ToBrazilTimeZone().Subtract(edition.StartDate.ToBrazilTimeZone()).Days)
                                             .Select(offset => edition.StartDate.ToBrazilTimeZone().AddDays(offset).ToString("yyyy-MM-dd"))
                                             .ToList(),
                    EventsApiResponses = editionEvents?.Select(ee => new EditionEventApiResponse
                    {
                        Uid = ee.Uid,
                        Name = ee.Name.Trim(),
                        StartDate = ee.StartDate.ToBrazilTimeZone().ToString("yyyy-MM-dd"),
                        EndDate = ee.EndDate.ToBrazilTimeZone().ToString("yyyy-MM-dd"),
                        DurationDays = (int)((ee.EndDate - ee.StartDate).TotalDays) + 1
                    })?.OrderBy(c => c.Name)?.ToList(),
                    RoomsApiResponses = roomDtos?.Select(rd => new ConferencesFilterItemApiResponse
                    {
                        Uid = rd.Room.Uid,
                        Name = rd.GetRoomNameByLanguageCode(request?.Culture)?.RoomName?.Value
                    })?.OrderBy(c => c.Name)?.ToList(),
                    PillarsApiResponses = pillars?.Select(t => new PillarBaseApiResponse
                    {
                        Uid = t.Uid,
                        Name = t.Name.GetSeparatorTranslation(request?.Culture, Language.Separator),
                        Color = t.Color
                    })?.OrderBy(c => c.Name)?.ToList(),
                    TracksApiResponses = tracks?.Select(t => new TrackBaseApiResponse
                    {
                        Uid = t.Uid,
                        Name = t.Name.GetSeparatorTranslation(request?.Culture, Language.Separator),
                        Color = t.Color
                    })?.OrderBy(c => c.Name)?.ToList(),
                    PresentationFormatsApiResponses = presentationFormats?.Select(ta => new ConferencesFilterItemApiResponse
                    {
                        Uid = ta.Uid,
                        Name = ta.Name.GetSeparatorTranslation(request?.Culture, Language.Separator)
                    })?.OrderBy(c => c.Name)?.ToList()
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "Players filters api failed." } });
            }
        }

        #endregion

        #region Details

        /// <summary>
        /// Get the Conference details
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("conference/{uid?}"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Conference([FromUri]ConferenceApiRequest request)
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

            var collaboratorApiDto = await this.conferenceRepo.FindApiDtoByUidAsync(
                request?.Uid ?? Guid.Empty,
                edition.Id);
            if (collaboratorApiDto == null)
            {
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = "Conference not found." } });
            }

            return await Json(new ConferenceApiResponse
            {
                Status = ApiStatus.Success,
                Error = null,
                Uid = collaboratorApiDto.Conference.Uid,
                Event = new EditionEventBaseApiResponse
                {
                    Uid = collaboratorApiDto.EditionEvent.Uid,
                    Name = collaboratorApiDto.EditionEvent.Name.GetSeparatorTranslation(requestLanguage?.Code ?? defaultLanguage?.Code, Language.Separator)?.Trim()
                },
                Title = collaboratorApiDto.GetConferenceTitleDtoByLanguageCode(requestLanguage?.Code)?.ConferenceTitle?.Value?.Trim() ??
                        collaboratorApiDto.GetConferenceTitleDtoByLanguageCode(defaultLanguage?.Code)?.ConferenceTitle?.Value?.Trim(),
                Synopsis = collaboratorApiDto.GetConferenceSynopsisDtoByLanguageCode(requestLanguage?.Code)?.ConferenceSynopsis?.Value?.Trim() ??
                           collaboratorApiDto.GetConferenceSynopsisDtoByLanguageCode(defaultLanguage?.Code)?.ConferenceSynopsis?.Value?.Trim(),
                Date = collaboratorApiDto.Conference.StartDate.ToBrazilTimeZone().ToString("yyyy-MM-dd"),
                StartTime = collaboratorApiDto.Conference.StartDate.ToBrazilTimeZone().ToString("HH:mm"),
                EndTime = collaboratorApiDto.Conference.EndDate.ToBrazilTimeZone().ToString("HH:mm"),
                DurationMinutes = (int)((collaboratorApiDto.Conference.EndDate - collaboratorApiDto.Conference.StartDate).TotalMinutes),
                Room = collaboratorApiDto.RoomDto != null ? new RoomBaseApiResponse
                {
                    Uid = collaboratorApiDto.RoomDto.Room.Uid,
                    Name = collaboratorApiDto.RoomDto.GetRoomNameByLanguageCode(requestLanguage?.Code ?? defaultLanguage?.Code)?.RoomName?.Value?.Trim()
                } : null,
                Tracks = collaboratorApiDto.ConferenceTrackDtos?.Select(ctd => new TrackBaseApiResponse
                {
                    Uid = ctd.Track.Uid,
                    Name = ctd.Track.Name?.GetSeparatorTranslation(requestLanguage?.Code ?? defaultLanguage?.Code, Language.Separator)?.Trim(),
                    Color = ctd.Track.Color
                })?.ToList(),
                PresentationFormats = collaboratorApiDto.ConferencePresentationFormatDtos?.Select(cpfd => new PresentationFormatBaseApiResponse
                {
                    Uid = cpfd.PresentationFormat.Uid,
                    Name = cpfd.PresentationFormat.Name?.GetSeparatorTranslation(requestLanguage?.Code ?? defaultLanguage?.Code, Language.Separator)?.Trim()
                })?.ToList(),
                Speakers = collaboratorApiDto.ConferenceParticipantDtos?.Select(cpd => new ConferenceParticipantApiListItem
                {
                    Uid = cpd.AttendeeCollaboratorDto.Collaborator.Uid,
                    BadgeName = cpd.AttendeeCollaboratorDto.Collaborator.Badge?.Trim(),
                    Name = cpd.AttendeeCollaboratorDto.Collaborator.GetFullName()?.Trim(),
                    Role = cpd.ConferenceParticipantRoleDto.GetConferenceParticipantRoleTitleDtoByLanguageCode(requestLanguage?.Code)?.ConferenceParticipantRoleTitle?.Value?.Trim() ??
                           cpd.ConferenceParticipantRoleDto.GetConferenceParticipantRoleTitleDtoByLanguageCode(defaultLanguage?.Code)?.ConferenceParticipantRoleTitle?.Value?.Trim(),
                    Picture = cpd.AttendeeCollaboratorDto.Collaborator.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, cpd.AttendeeCollaboratorDto.Collaborator.Uid, cpd.AttendeeCollaboratorDto.Collaborator.ImageUploadDate, true) : null,
                    JobTitle = cpd.AttendeeCollaboratorDto.GetJobTitleDtoByLanguageCode(requestLanguage?.Code)?.Value?.Trim() ??
                               cpd.AttendeeCollaboratorDto.GetJobTitleDtoByLanguageCode(defaultLanguage?.Code)?.Value?.Trim(),
                    MiniBio = cpd.AttendeeCollaboratorDto.GetMiniBioDtoByLanguageCode(requestLanguage?.Code)?.Value?.Trim() ??
                              cpd.AttendeeCollaboratorDto.GetMiniBioDtoByLanguageCode(defaultLanguage?.Code)?.Value?.Trim(),
                    Companies = cpd.AttendeeCollaboratorDto.AttendeeOrganizationsDtos?.Select(aod => new OrganizationBaseApiResponse
                    {
                        Uid = aod.Organization.Uid,
                        TradeName = aod.Organization.TradeName,
                        CompanyName = aod.Organization.CompanyName,
                        Picture = aod.Organization.HasImage() ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, aod.Organization.Uid, aod.Organization.ImageUploadDate, true) : null
                    })?.ToList()
                })?.ToList()
            });
        }

        #endregion
    }
}