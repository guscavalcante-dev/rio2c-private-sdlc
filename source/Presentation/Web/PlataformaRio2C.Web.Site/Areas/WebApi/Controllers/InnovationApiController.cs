// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2021
// ***********************************************************************
// <copyright file="InnovationApiController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Newtonsoft.Json;
using PlataformaRio2C.Application;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// InnovationApiController
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0/innovation")]
    public class InnovationApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IWorkDedicationRepository workDedicationRepo;
        private readonly IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;
        private readonly IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepo;
        private readonly IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationApiController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="workDedicationRepository">The work dedication repository.</param>
        /// <param name="innovationOrganizationExperienceOptionRepository">The innovation organization experience option repository.</param>
        /// <param name="innovationOrganizationTrackOptionRepository">The innovation organization track option repository.</param>
        /// <param name="innovationOrganizationTechnologyOptionRepository">The innovation organization technology option repository.</param>
        /// <param name="innovationOrganizationObjectivesOptionRepository">The innovation organization objectives option repository.</param>
        public InnovationApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IWorkDedicationRepository workDedicationRepository,
            IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepository,
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.workDedicationRepo = workDedicationRepository;
            this.innovationOrganizationExperienceOptionRepo = innovationOrganizationExperienceOptionRepository;
            this.innovationOrganizationTrackOptionRepo = innovationOrganizationTrackOptionRepository;
            this.innovationOrganizationTechnologyOptionRepo = innovationOrganizationTechnologyOptionRepository;
            this.innovationOrganizationObjectivesOptionRepo = innovationOrganizationObjectivesOptionRepository;
        }

        /// <summary>
        /// Creates the startup.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("createstartup/{key?}")]
        public async Task<IHttpActionResult> CreateStartup(string key, HttpRequestMessage request)
        {
            var validationResult = new AppValidationResult();

            try
            {
                #region Initial Validations

                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateStartupApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var applicationUser = await identityController.FindByEmailAsync(Domain.Entities.User.BatchProcessUser.Email);
                if (applicationUser == null)
                {
                    throw new DomainException(Messages.UserNotFound);
                }

                var currentEdition = await editionRepo.FindByIsCurrentAsync();
                if (currentEdition == null)
                {
                    throw new DomainException(Messages.CurrentEditionNotFound);
                }

                #endregion

                var innovationOrganizationApiDto = JsonConvert.DeserializeObject<InnovationOrganizationApiDto>(request.Content.ReadAsStringAsync().Result);

                if (innovationOrganizationApiDto == null)
                {
                    throw new DomainException(Messages.IncorrectJsonStructure);
                }

                if (!innovationOrganizationApiDto.IsValid())
                {
                    validationResult.Add(innovationOrganizationApiDto.ValidationResult);
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var cmd = new CreateInnovationOrganization(
                    innovationOrganizationApiDto.Name,
                    innovationOrganizationApiDto.Document,
                    innovationOrganizationApiDto.ServiceName,
                    innovationOrganizationApiDto.FoundationDate,
                    innovationOrganizationApiDto.AccumulatedRevenue,
                    innovationOrganizationApiDto.Description,
                    innovationOrganizationApiDto.BusinessDefinition,
                    innovationOrganizationApiDto.Website,
                    innovationOrganizationApiDto.BusinessFocus,
                    innovationOrganizationApiDto.MarketSize,
                    innovationOrganizationApiDto.BusinessEconomicModel,
                    innovationOrganizationApiDto.BusinessOperationalModel,
                    innovationOrganizationApiDto.ImageFile,
                    innovationOrganizationApiDto.VideoUrl,
                    innovationOrganizationApiDto.BusinessDifferentials,
                    innovationOrganizationApiDto.BusinessStage,
                    innovationOrganizationApiDto.ResponsibleName,
                    innovationOrganizationApiDto.Email,
                    innovationOrganizationApiDto.PhoneNumber,
                    innovationOrganizationApiDto.CellPhone,
                    innovationOrganizationApiDto.PresentationFile,
                    innovationOrganizationApiDto.PresentationFileName,
                    innovationOrganizationApiDto.AttendeeInnovationOrganizationFounderApiDtos,
                    innovationOrganizationApiDto.AttendeeInnovationOrganizationCompetitorApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationExperienceOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationTrackOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationObjectivesOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationTechnologyOptionApiDtos);

                cmd.UpdatePreSendProperties(
                    applicationUser.Id,
                    applicationUser.Uid,
                    currentEdition.Id,
                    currentEdition.Uid,
                    ""); //TODO: Implements User interface language?

                validationResult = await this.commandBus.Send(cmd);
                if (!validationResult.IsValid)
                {
                    throw new DomainException();
                }
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
            catch (JsonSerializationException ex)
            {
                return await Json(new
                {
                    status = ApiStatus.Error,
                    message = ex.GetInnerMessage(),
                    errors = validationResult?.Errors?.Select(e => new { e.Code, e.Message })
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }

            return await Json(new { status = ApiStatus.Success, message = string.Format(Messages.EntityActionSuccessfull, Labels.Startup, Labels.CreatedF) });
        }

        /// <summary>
        /// Get innovation API filters
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("filters")]
        public async Task<IHttpActionResult> Filters([FromUri] InnovationFiltersApiRequest request)
        {
            try
            {
                #region Initial Validations

                var activeEditions = await this.editionRepo.FindAllByIsActiveAsync(false);
                if (activeEditions?.Any() == false)
                {
                    return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00001", Message = "No active editions found." } });
                }

                // Get edition from request otherwise get current
                var edition = request?.Edition.HasValue == true ? activeEditions?.FirstOrDefault(e => e.UrlCode == request.Edition) :
                                                                  activeEditions?.FirstOrDefault(e => e.IsCurrent);
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

                var innovationOrganizationExperienceOptions = await this.innovationOrganizationExperienceOptionRepo.FindAllAsync();
                var innovationOrganizationTechnologyOptions = await this.innovationOrganizationTechnologyOptionRepo.FindAllAsync();
                var innovationOrganizationObjectivesOptions = await this.innovationOrganizationObjectivesOptionRepo.FindAllAsync();
                var innovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllAsync();
                var workDedications = await this.workDedicationRepo.FindAllAsync();

                return await Json(new InnovationFiltersApiResponse
                {
                    InnovationOrganizationExperienceOptions = innovationOrganizationExperienceOptions.Select(ioeo => new ApiListItemBaseResponse()
                    {
                        Uid = ioeo.Uid,
                        Name = ioeo.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),

                    InnovationOrganizationTechnologyOptions = innovationOrganizationTechnologyOptions.Select(ioto => new ApiListItemBaseResponse()
                    {
                        Uid = ioto.Uid,
                        Name = ioto.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),

                    InnovationOrganizationObjectivesOptions = innovationOrganizationObjectivesOptions.Select(iooo => new ApiListItemBaseResponse()
                    {
                        Uid = iooo.Uid,
                        Name = iooo.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),

                    InnovationOrganizationTrackOptions = innovationOrganizationTrackOptions.Select(ioto => new InnovationOrganizationTrackOptionListItemApiResponse()
                    {
                        Uid = ioto.Uid,
                        Name = ioto.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code),
                        Description = ioto.GetDesctiptionTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),

                    WorkDedications = workDedications.Select(wd => new ApiListItemBaseResponse()
                    {
                        Uid = wd.Uid,
                        Name = wd.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),
                    
                    Status = ApiStatus.Success
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = "Innovation filters api failed." } });
            }
        }
    }
}
