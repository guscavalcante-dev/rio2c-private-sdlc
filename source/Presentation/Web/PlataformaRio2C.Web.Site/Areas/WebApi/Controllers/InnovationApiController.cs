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
        private readonly IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository innovationOrganizationSustainableDevelopmentObjectivesOptionRepository;

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
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOptionRepository">The innovation organization sustainable development objectives option repository.</param>
        public InnovationApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IWorkDedicationRepository workDedicationRepository,
            IInnovationOrganizationExperienceOptionRepository innovationOrganizationExperienceOptionRepository,
            IInnovationOrganizationTrackOptionRepository innovationOrganizationTrackOptionRepository,
            IInnovationOrganizationTechnologyOptionRepository innovationOrganizationTechnologyOptionRepository,
            IInnovationOrganizationObjectivesOptionRepository innovationOrganizationObjectivesOptionRepository,
            IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository innovationOrganizationSustainableDevelopmentObjectivesOptionRepository

            )
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
            this.innovationOrganizationSustainableDevelopmentObjectivesOptionRepository = innovationOrganizationSustainableDevelopmentObjectivesOptionRepository;
        }

        /// <summary>
        ///  Create the startup
        /// </summary>
        /// <param name="key">A Key é obrigatório para a autorização</param>
        /// <param name="innovationOrganizationApiDto">Objeto innovationOrganizationApiDto é obrigatório.</param>
        /// <remarks>
        /// No objeto innovationOrganizationApiDto existem algums campos obrigatórios.
        /// 
        /// 
        /// Formulário Solicitado
        /// -----------------------------------------
        /// 1.	Identificação do Participante: 
        /// 
        /// a) Nome ou nome da Empresa*; Propriedade: name - * Obrigatório - Exemplo | "name": "ACME Toasters LTDA"
        /// 
        /// b) CPF ou CNPJ*; Propriedade: document - * Obrigatório  - Exemplo | "document": "49.587.570/0001-19"
        /// 
        /// c) Nome dos fundadores *; (Lista) Propriedade: founders:["fullName"] - Exemplo - "founders": [{"fullName": "First founder's name"}], - * Obrigatório - Ao menos um item na lista 
        /// 
        /// d) Nome do Representante*; Propriedade: responsibleName - * Obrigatório  - Exemplo | "responsibleName": "Startup Responsible's name"
        /// 
        /// e) E-mail e telefone ou celular de contato*; Propriedade: email e cellPhone - * Obrigatórios  - Exemplo | "email": "acmetoasters3000@gmail.com" | "cellPhone": "14998269754"
        /// 
        /// f) Curriculum * (no máximo até 700 caracteres com espaço); (Lista) Propriedade: founders:["curriculum"] - Exemplo - "founders": [{"curriculum": "First founder's name curriculum."}], - * Obrigatório - Ao menos um item na lista | Mesmo cenário da 1. c)
        /// 
        /// g) Dedicação * (pode marcar apenas um); (Lista) Propriedade: founders:["workDedicationUid"] - Exemplo - "founders": [{"workDedicationUid" "dcc9878d-ebc7-438c-8a0a-5952b75a8b54"}], - * Obrigatório - Ao menos um item na lista | Mesmo cenário da 1. c)
        /// 
        /// Logo da Startup *;  Propriedade: imageFile - * Obrigatório - Exemplo | "imageFile": "BASE_64"
        /// 
        /// ----------------------------------------
        /// 
        /// 2.	Produto ou Serviço:
        /// 
        /// a) Nome do Produto ou Serviço*; Propriedade: serviceName - * Obrigatório - Exemplo | "serviceName": "ACME toaster machine 3000"
        /// 
        /// b) Descreva sua startup em uma sentença (no máximo até 600 caracteres com espaço)*; Propriedade: description - * Obrigatório - Exemplo | "description": "My Employee (max 600 chars)"
        /// 
        /// c) Faturamento acumulado dos últimos 3 meses (em reais R$) *; Propriedade: accumulatedRevenueThreeMonths - * Obrigatório - Exemplo | "accumulatedRevenueThreeMonths": 3000
        /// 
        /// d)	Quais dessas experiências a empresa já participou? (pode marcar mais de uma opção) (Opcional); Propriedade: (Lista) companyExperiences - Exemplo | "companyExperiences" [{"uid": "82167c1d-7ca6-447f-80c7-ae9188add436","additionalInfo": ""   }]
        /// 
        /// e) Tecnologia usadas: (pode marcar mais de uma opção) (Opcional); Propriedade: (Lista) technologyExperiences - Exemplo | "technologyExperiences" [{"uid": "82167c1d-7ca6-447f-80c7-ae9188add436","additionalInfo": ""   }]
        /// 
        /// f) Especificação do foco principal do negócio (que problema resolve)*; Propriedade: businessFocus - * Obrigatório - Exemplo | "businessFocus": "My Business focus"
        /// 
        /// g) Especificação do impacto do negócio (tamanho do mercado)*; Propriedade: marketSize - * Obrigatório - Exemplo | "marketSize": "Market size"
        /// 
        /// h) Quais os diferenciais do negócio (qual a solução e impactos gerados)*; Propriedade: businessDifferentials - * Obrigatório - Exemplo | "businessDifferentials": "My Business differentials"
        /// 
        /// i)	Quem são seus concorrentes? Cite até 3*; Propriedade: (Lista) competingCompanies * Obrigatório - Exemplo | "competingCompanies" [{"name": "Skynet"   }]
        /// 
        /// j)	Especificação do estágio em que negócio se encontra*; Propriedade: businessStage - * Obrigatório - Exemplo | "businessStage": "My business stage"
        /// 
        /// k)	Modelo econômico do negócio (opcional); Propriedade: businessEconomicModel - Exemplo | "businessEconomicModel": "My business economic model"
        /// 
        /// l)	Modelo operacional do negócio (opcional); Propriedade: businessOperationalModel - Exemplo | "businessOperationalModel": "My business operational model"
        /// 
        /// m)	Site do negócio (se houver) (opcional); Propriedade: website - Exemplo | "website": "www.site.com.br"
        /// 
        /// n)	Pitch deck - apresentação em slides (Só serão aceitos materiais em formato pdf)*; Propriedade: presentationFile - * Obrigatório - Exemplo | "presentationFile": "BASE_64"
        /// 
        /// ----------------------------------------
        /// 
        /// 3)	Enquadre seu produto ou serviço num track abaixo*: ;  Propriedade: (Lista com uma opção apenas) tracks - Obrigatório - Exemplo | "tracks" [{"uid": "82167c1d-7ca6-447f-80c7-ae9188add436","additionalInfo": ""   }]
        /// 
        /// ----------------------------------------
        /// 
        /// 4)	Em qual(s) dos 17 Objetivos de Desenvolvimento Sustentável (ODS) sua startup se enquadra? (pode marcar mais de uma opção); (Opcional) ; Propriedade: (Lista) sustainableDevelopmentObjectives - Exemplo | "sustainableDevelopmentObjectives" [{"uid": "82167c1d-7ca6-447f-80c7-ae9188add436","additionalInfo": ""   }]  
        /// 
        /// ----------------------------------------
        /// 
        /// 5) Qual o seu principal objetivo em participar do Pitching? (pode marcar mais de uma opção): (Opcional); Propriedade: (Lista) companyObjectives - Exemplo | "companyObjectives" [{"uid": "82167c1d-7ca6-447f-80c7-ae9188add436","additionalInfo": ""   }]  
        /// 
        /// ----------------------------------------
        /// 
        /// 6) Deseja participar para Rodadas de Negócios? *  Propriedade: wouldYouLikeParticipateBusinessRound - * Obrigatório - Exemplo | "wouldYouLikeParticipateBusinessRound": true | false
        /// 
        /// Se a resposta for sim, abrir as perguntas abaixo: (wouldYouLikeParticipateBusinessRound == true)
        /// 
        /// a) Ano de Fundação da empresa*; *  Propriedade: businessFoundationYear - * Obrigatório - Exemplo | "businessFoundationYear": 2022
        /// 
        /// b) Faturamento acumulado dos últimos 12 meses (em reais R$) *; Propriedade: accumulatedRevenueForLastTwelveMonths - * Obrigatório - Exemplo | "accumulatedRevenueForLastTwelveMonths": 5000
        /// 
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        [Route("create-startup/{key}"), HttpPost]
        public async Task<IHttpActionResult> CreateStartup(string key,  [FromBody] InnovationOrganizationApiDto innovationOrganizationApiDto)
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
                    DateTime.Now,
                    innovationOrganizationApiDto.AccumulatedRevenue,
                    innovationOrganizationApiDto.Description,
                    "",
                    innovationOrganizationApiDto.Website,
                    innovationOrganizationApiDto.BusinessFocus,
                    innovationOrganizationApiDto.MarketSize,
                    innovationOrganizationApiDto.BusinessEconomicModel,
                    innovationOrganizationApiDto.BusinessOperationalModel,
                    innovationOrganizationApiDto.ImageFile,
                    "",
                    innovationOrganizationApiDto.BusinessDifferentials,
                    innovationOrganizationApiDto.BusinessStage,
                    innovationOrganizationApiDto.ResponsibleName,
                    innovationOrganizationApiDto.Email,
                    "",
                    innovationOrganizationApiDto.CellPhone,
                    innovationOrganizationApiDto.PresentationFile,
                    innovationOrganizationApiDto.PresentationFileName,
                    innovationOrganizationApiDto.AttendeeInnovationOrganizationFounderApiDtos,
                    innovationOrganizationApiDto.AttendeeInnovationOrganizationCompetitorApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationExperienceOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationTrackOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationObjectivesOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationTechnologyOptionApiDtos,
                    innovationOrganizationApiDto.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos,
                    innovationOrganizationApiDto.WouldYouLikeParticipateBusinessRound,
                    innovationOrganizationApiDto.AccumulatedRevenueForLastTwelveMonths,
                    innovationOrganizationApiDto.BusinessFoundationYear
                    );

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

        /// <remarks>
        /// Listagem de opções para exibir no formulário 
        /// 
        /// Formulário Solicitado
        /// -----------------------------------------
        /// 
        /// 1.	Identificação do Participante: 
        /// 
        /// g) Dedicação * (pode marcar apenas um); (Lista) Propriedade:
        /// Objeto para listagem: workDedications
        /// 
        /// 
        /// -----------------------------------------
        /// 
        /// 2.	Produto ou Serviço:
        /// 
        /// e) Tecnologia usadas: (pode marcar mais de uma opção) (Opcional);
        /// Objeto para listagem: organizationTechnologies 
        ///
        /// -----------------------------------------
        /// 
        /// 3)	Enquadre seu produto ou serviço num track abaixo*:
        /// Objeto para listagem: organizationTracks
        /// 
        /// -----------------------------------------
        /// 
        /// 4)	Em qual(s) dos 17 Objetivos de Desenvolvimento Sustentável (ODS) sua startup se enquadra? (pode marcar mais de uma opção); (Opcional);
        /// Objeto para listagem: organizationSustainableDevelopmentObjectives 
        /// 
        /// -----------------------------------------
        ///
        /// 5) Qual o seu principal objetivo em participar do Pitching? (pode marcar mais de uma opção): (Opcional); 
        /// Objeto para listagem: organizationObjectives 
        /// 
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("api/v1.0/innovation/filters")]
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
                var innovationOrganizationSustainableDevelopmentObjectivesOptions = await this.innovationOrganizationSustainableDevelopmentObjectivesOptionRepository.FindAllAsync();
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

                    InnovationOrganizationSustainableDevelopmentObjectivesOptions = innovationOrganizationSustainableDevelopmentObjectivesOptions.Select(iooo => new InnovationOrganizationSustainableDevelopmentObjectivesOptionListItemApiResponse()
                    {
                        Uid = iooo.Uid,
                        Name = iooo.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code),
                        Description = iooo.GetDesctiptionTranslation(requestLanguage?.Code ?? defaultLanguage?.Code),
                        DisplayOrder = iooo.DisplayOrder
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
