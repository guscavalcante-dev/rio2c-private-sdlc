// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-08-2020
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
        private readonly IPresentationFormatRepository presentationFormatRepo;

        public ConferencesApiController(
            IConferenceRepository conferenceRepository,
            IEditionRepository editionRepository,
            IEditionEventRepository editionEventRepository,
            IRoomRepository roomRepository,
            ITrackRepository trackRepository,
            IPresentationFormatRepository presentationFormatRepository,


            ICollaboratorRepository collaboratorRepository,
            IFileRepository fileRepository)
        {
            this.conferenceRepo = conferenceRepository;
            this.editionRepo = editionRepository;
            this.editionEventRepo = editionEventRepository;
            this.roomRepo = roomRepository;
            this.trackRepo = trackRepository;
            this.presentationFormatRepo = presentationFormatRepository;
        }

        #region List

        //[HttpGet]
        //[Route("conferences")]
        //public async Task<IHttpActionResult> Conferences([FromUri]SpeakersApiRequest request)
        //{
        //    var editions = this.editionRepo.FindAllByIsActive(false);
        //    if (editions?.Any() == false)
        //    {
        //        return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." } });
        //    }

        //    // Get edition from request otherwise get current
        //    var edition = request?.Edition.HasValue == true ? editions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
        //                                                      editions?.FirstOrDefault(e => e.IsCurrent);
        //    if (edition == null)
        //    {
        //        return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00002", Message = "No editions found." } });
        //    }

        //    var collaboratorsApiDtos = await this.collaboratorRepo.FindAllPublicApiPaged(
        //        edition.Id,
        //        request?.Keywords,
        //        Domain.Constants.CollaboratorType.Speaker,
        //        request?.Page ?? 1,
        //        request?.PageSize ?? 10);

        //    return await Json(new SpeakersApiResponse
        //    {
        //        Status = ApiStatus.Success,
        //        Error = null,
        //        HasPreviousPage = collaboratorsApiDtos.HasPreviousPage,
        //        HasNextPage = collaboratorsApiDtos.HasNextPage,
        //        TotalItemCount = collaboratorsApiDtos.TotalItemCount,
        //        PageCount = collaboratorsApiDtos.PageCount,
        //        PageNumber = collaboratorsApiDtos.PageNumber,
        //        PageSize = collaboratorsApiDtos.PageSize,
        //        Speakers = collaboratorsApiDtos?.Select(c => new SpeakersApiListItem
        //        {
        //            Uid = c.Uid,
        //            BadgeName = c.BadgeName?.Trim(),
        //            Name = c.Name?.Trim(),
        //            HighlightPosition = c.ApiHighlightPosition,
        //            Picture = c.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.UserImage, c.Uid, c.ImageUploadDate, true) : null,
        //            MiniBio = c.GetCollaboratorMiniBioBaseDtoByLanguageCode(request?.Culture)?.Value?.Trim(),
        //            JobTitle = c.GetCollaboratorJobTitleBaseDtoByLanguageCode(request?.Culture)?.Value?.Trim(),
        //            Companies = c.OrganizationsDtos?.Select(od => new OrganizationBaseApiResponse
        //            {
        //                Uid = od.Uid,
        //                TradeName = od.TradeName,
        //                CompanyName = od.CompanyName,
        //                Picture = od.ImageUploadDate.HasValue ? this.fileRepo.GetImageUrl(FileRepositoryPathType.OrganizationImage, od.Uid, od.ImageUploadDate, true) : null
        //            })?.ToList()
        //        })?.ToList()
        //    });
        //}

        /// <summary>Filters for conferences.</summary>
        /// <returns></returns>
        [HttpGet]
        [Route("conferences/filters")]
        public async Task<IHttpActionResult> Filters([FromUri]ConferencesFiltersApiRequest request)
        {
            try
            {
                var editions = this.editionRepo.FindAllByIsActive(false);
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

                var editionEvents = await this.editionEventRepo.FindAllByEditionIdAsync(edition.Id);
                var roomDtos = await this.roomRepo.FindAllDtoByEditionIdAsync(edition.Id);
                var tracks = await this.trackRepo.FindAllByEditionIdAsync(edition.Id);
                var presentationFormats = await this.presentationFormatRepo.FindAllByEditionIdAsync(edition.Id);

                return await Json(new ConferencesFiltersApiResponse
                {
                    Status = ApiStatus.Success,
                    Error = null,
                    EditionDates = Enumerable.Range(0, 1 + edition.EndDate.Subtract(edition.StartDate).Days)
                                             .Select(offset => edition.StartDate.AddDays(offset).ToString("yyyy-MM-dd"))
                                             .ToList(),
                    EventsApiResponses = editionEvents?.Select(ee => new ConferencesFilterItemApiResponse
                    {
                        Uid = ee.Uid,
                        Name = ee.Name
                    })?.OrderBy(c => c.Name)?.ToList(),
                    RoomsApiResponses = roomDtos?.Select(rd => new ConferencesFilterItemApiResponse
                    {
                        Uid = rd.Room.Uid,
                        Name = rd.GetRoomNameByLanguageCode(request?.Culture)?.RoomName?.Value
                    })?.OrderBy(c => c.Name)?.ToList(),
                    TracksApiResponses = tracks?.Select(t => new ConferencesFilterItemApiResponse
                    {
                        Uid = t.Uid,
                        Name = t.Name.GetSeparatorTranslation(request?.Culture, Language.Separator)
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
    }
}