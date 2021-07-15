// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-28-2021
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
using PlataformaRio2C.Domain.Entities;
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
    [System.Web.Http.RoutePrefix("api/v1.0")]
    public class InnovationApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        private readonly IEditionRepository editionRepository;
        private readonly IWorkDedicationRepository workDedicationRepository;
        private readonly IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepo;
        private readonly IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepo;
        private readonly IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepo;
        private readonly IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationApiController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        public InnovationApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IEditionRepository editionRepo,
            IWorkDedicationRepository workDedicationRepo,
            IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepository,
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
            this.editionRepository = editionRepo;
            this.workDedicationRepository = workDedicationRepo;
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
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("CreateStartup/{key?}")]
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

                var currentEdition = await editionRepository.FindByIsCurrentAsync();
                if (currentEdition == null)
                {
                    throw new DomainException(Messages.CurrentEditionNotFound);
                }

                #endregion

                var innovationOrganizationApiDto = JsonConvert.DeserializeObject<InnovationOrganizationApiDto>(request.Content.ReadAsStringAsync().Result);
                if (!innovationOrganizationApiDto.IsValid())
                {
                    validationResult.Add(innovationOrganizationApiDto.ValidationResult);
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var cmd = new CreateInnovationOrganization(innovationOrganizationApiDto);
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
                    message = (ex.InnerException != null ? ex.GetInnerMessage() : ""),
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

        #region Lists

        /// <summary>
        /// Gets the work dedications.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetWorkDedications/{key?}")]
        public async Task<IHttpActionResult> GetWorkDedications(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateStartupApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var workDedications = await this.workDedicationRepository.FindAllAsync();

                return await Json(new WorkDedicationsApiResponse
                {
                    WorkDedications = workDedications.Select(mbt => new BaseListItemApiResponse()
                    {
                        Uid = mbt.Uid,
                        Name = mbt.Name
                    })?.ToList(),
                    Status = ApiStatus.Success
                });
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
        }

        /// <summary>
        /// Gets the innovation options.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="innovationOptionGroupUid">The innovation option group uid.</param>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetInnovationOrganizationExperienceOptions/{key?}")]
        public async Task<IHttpActionResult> GetInnovationOrganizationExperienceOptions(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateStartupApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var innovationOrganizationExperienceOptions = await this.innovationOrganizationExperienceOptionRepo.FindAllAsync();

                return await Json(new InnovationOrganizationOptionApiBaseResponse
                {
                    BaseListItemApiResponses = innovationOrganizationExperienceOptions.Select(mbt => new BaseListItemApiResponse()
                    {
                        Uid = mbt.Uid,
                        Name = mbt.Name
                    })?.ToList(),
                    Status = ApiStatus.Success
                });
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
        }

        /// <summary>
        /// Gets the innovation options.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="innovationOptionGroupUid">The innovation option group uid.</param>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetInnovationOrganizationTrackOptions/{key?}")]
        public async Task<IHttpActionResult> GetInnovationOrganizationTrackOptions(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateStartupApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var innovationOrganizationTrackOptions = await this.innovationOrganizationTrackOptionRepo.FindAllAsync();

                return await Json(new InnovationOrganizationOptionApiBaseResponse
                {
                    BaseListItemApiResponses = innovationOrganizationTrackOptions.Select(mbt => new BaseListItemApiResponse()
                    {
                        Uid = mbt.Uid,
                        Name = mbt.Name
                    })?.ToList(),
                    Status = ApiStatus.Success
                });
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
        }

        /// <summary>
        /// Gets the innovation options.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="innovationOptionGroupUid">The innovation option group uid.</param>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetInnovationOrganizationTechnologyOptions/{key?}")]
        public async Task<IHttpActionResult> GetInnovationOrganizationTechnologyOptions(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateStartupApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var innovationOrganizationTechnologyOptions = await this.innovationOrganizationTechnologyOptionRepo.FindAllAsync();

                return await Json(new InnovationOrganizationOptionApiBaseResponse
                {
                    BaseListItemApiResponses = innovationOrganizationTechnologyOptions.Select(mbt => new BaseListItemApiResponse()
                    {
                        Uid = mbt.Uid,
                        Name = mbt.Name
                    })?.ToList(),
                    Status = ApiStatus.Success
                });
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
        }

        /// <summary>
        /// Gets the innovation options.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="innovationOptionGroupUid">The innovation option group uid.</param>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetInnovationOrganizationObjectivesOptions/{key?}")]
        public async Task<IHttpActionResult> GetInnovationOrganizationObjectivesOptions(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateStartupApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var innovationOrganizationObjectivesOptions = await this.innovationOrganizationObjectivesOptionRepo.FindAllAsync();

                return await Json(new InnovationOrganizationOptionApiBaseResponse
                {
                    BaseListItemApiResponses = innovationOrganizationObjectivesOptions.Select(mbt => new BaseListItemApiResponse()
                    {
                        Uid = mbt.Uid,
                        Name = mbt.Name
                    })?.ToList(),
                    Status = ApiStatus.Success
                });
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage() });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }
        }

        #endregion
    }
}
