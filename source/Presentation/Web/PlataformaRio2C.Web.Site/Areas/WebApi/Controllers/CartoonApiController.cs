// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CartoonApiController.cs" company="Softo">
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
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    /// <summary>
    /// CartoonApiController
    /// </summary>
    [System.Web.Http.RoutePrefix("api/v1.0/cartoon")]
    public class CartoonApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICartoonProjectFormatRepository cartoonProjectFormatRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonApiController" /> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="cartoonProjectFormatRepository">The cartoon project format repository.</param>
        public CartoonApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            ICartoonProjectFormatRepository cartoonProjectFormatRepository)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.cartoonProjectFormatRepo = cartoonProjectFormatRepository;
        }

        /// <summary>
        /// Creates the Cartoon Project
        /// </summary>
        /// <param name="key">The key is required</param>
        /// <param name="cartoonProjectApiDto">The cartoonProjectApiDto is required</param>
        /// <remarks>
        /// No objeto innovationOrganizationApiDto existem algums campos obrigatórios.
        /// 
        /// Formulário Solicitado
        /// -----------------------------------------
        /// 1. Dados do projeto: 
        /// 
        /// a) Titulo*; Propriedade: title - * Obrigatório - Exemplo | "title": "Oggy and the Cockroaches"
        /// 
        /// b) LogLine (até 3000 caracteres com espaço)*; Propriedade: logline - * Obrigatório - Exemplo | "logline": "My LogLine with 3000 max lenght."
        /// 
        /// c) Sinopse (até 3000 caracteres com espaço)*; Propriedade: synopsis - * Obrigatório - Exemplo | "synopsis": "My Synopsis with 3000 max lenght."
        /// 
        /// d) Motivação Criativa (até 3000 caracteres com espaço)*; Propriedade: motivation - * Obrigatório - Exemplo | "motivation": "The Creative Motivation with 3000 max lenght."
        /// 
        /// e) Formato (pode marcar apenas um)*; Propriedade: projectFormatUid - * Obrigatório - Exemplo | "projectFormatUid": "44ab63de-66ba-4032-b9ec-171539413e85"
        /// 
        /// f) Número de episódios*; Propriedade: numberOfEpisodes - * Obrigatório - Exemplo | "numberOfEpisodes": 12
        /// 
        /// g) Minutagem de cada episódio*; eachEpisodePlayingTime - * Obrigatório - Exemplo | "eachEpisodePlayingTime": "00:20:00"
        /// 
        /// h) Plano de Produção (até 3000 caracteres com espaço) (Opcional); Propriedade: productionPlan - Exemplo | "productionPlan": "My Production Plan with 3000 max lenght."
        /// 
        /// i) Valor total do projeto (Reais) (Opcional); Propriedade: totalValueOfProject - Exemplo | "totalValueOfProject": "R$ 10.000.000"
        /// 
        /// j) Link para Bíblia Projeto*; Propriedade: projectBibleUrl - * Obrigatório - Exemplo | "projectBibleUrl": "www.youtube.com/my-project-bible"
        /// 
        /// k) Link para Promo/Teaser (Opcional); Propriedade: projectTeaserUrl - Exemplo | "projectTeaserUrl": "www.youtube.com/my-project-teaser"
        /// 
        /// ----------------------------------------
        /// 
        /// 2. Dados da Produtora: 
        /// 
        /// a) Nome fantasia (não obrigatório); Propriedade: company.tradeName - Exemplo | "company.tradeName": "Xilam Animation's Trade Name"
        /// 
        /// b) Razão Social (não obrigatório); Propriedade: company.name - Exemplo | "company.name": "Xilam Animation"
        /// 
        /// c) CNPJ (não obrigatório); Propriedade: company.document - Exemplo | "company.document": "07.294.769/0001-09"
        /// 
        /// d) Endereço (não obrigatório); Propriedade: company.address - Exemplo | "company.address": "Av. Almirante Barroso, 81 - Rio de Janeiro, RJ, Brasil"
        /// 
        /// e) CEP (não obrigatório); Propriedade: company.zipCode - Exemplo | "company.zipCode": "20031004"
        /// 
        /// e) Telefone (não obrigatório); Propriedade: company.phoneNumber - Exemplo | "company.phoneNumber": "49.587.570/0001-19"
        /// 
        /// f) Link p/ Reel da Produtora (não obrigatório); Propriedade: company.ReelUrl - Exemplo | "company.ReelUrl": "www.youtube.com/my-reel-url"
        /// 
        /// ----------------------------------------
        /// 
        /// 3. Dados dos Criadores (Lista):
        /// 
        /// a) Email*; Propriedade: creators.email - * Obrigatório - Exemplo | "creators.email": "jean@xilamtv.com"
        ///
        /// b) Primeiro nome*; Propriedade: creators.firstName - * Obrigatório - Exemplo | "creators.firstName": "Jean-Yves"
        /// 
        /// c) Sobrenome*; Propriedade: creators.lastName - * Obrigatório - Exemplo | "creators.lastName": "Raimbaud"
        /// 
        /// d) Celular*; Propriedade: creators.cellPhone - * Obrigatório - Exemplo | "creators.cellPhone": "(14) 88888-8888"
        /// 
        /// e) Telefone; Propriedade: creators.phoneNumber - Exemplo | "creators.phoneNumber": "(14) 77777-7777"
        /// 
        /// f) Mini-biografia Criador(es) Projeto (no máximo até 3000 caracteres com espaço)*; Propriedade: creators.miniBio - * Obrigatório - Exemplo | "creators.miniBio": "Jean-Yves Minibio's with 3000 max characters."
        /// 
        /// g) É o responsável? (Somente 1 Criador pode ser enviado como responsável!)*; Propriedade: creators.isResponsible - * Obrigatório - Exemplo | "creators.isResponsible": true | false
        /// 
        /// h) Documento; Propriedade: creators.document - * Obrigatório - Exemplo | "creators.document": "451756930"
        /// 
        /// ----------------------------------------
        /// 
        /// </remarks>
        [Route("create-cartoon-project/{key?}"), HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> CreateCartoonProject(string key, [FromBody] CartoonProjectApiDto cartoonProjectApiDto)
        {
            var validationResult = new AppValidationResult();

            try
            {
                #region Initial Validations

                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateCartoonProjectApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var applicationUser = await identityController.FindByEmailAsync(Domain.Entities.User.BatchProcessUser.Email);
                if (applicationUser == null)
                {
                    throw new DomainException($@"{Messages.UserNotFound} ({Domain.Entities.User.BatchProcessUser.Email})");
                }

                var currentEdition = await editionRepo.FindByIsCurrentAsync();
                if (currentEdition == null)
                {
                    throw new DomainException(Messages.CurrentEditionNotFound);
                }

                #endregion

                if (cartoonProjectApiDto == null)
                {
                    throw new DomainException(Messages.IncorrectJsonStructure);
                }

                if (!cartoonProjectApiDto.IsValid())
                {
                    validationResult.Add(cartoonProjectApiDto.ValidationResult);
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var cmd = new CreateCartoonProject(
                    cartoonProjectApiDto.NumberOfEpisodes,
                    cartoonProjectApiDto.EachEpisodePlayingTime,
                    cartoonProjectApiDto.TotalValueOfProject,
                    cartoonProjectApiDto.Title,
                    cartoonProjectApiDto.Logline,
                    cartoonProjectApiDto.Summary,
                    cartoonProjectApiDto.Motivation,
                    cartoonProjectApiDto.ProductionPlan,
                    cartoonProjectApiDto.ProjectBibleUrl,
                    cartoonProjectApiDto.ProjectTeaserUrl,
                    cartoonProjectApiDto.ProjectFormatUid,
                    cartoonProjectApiDto.CartoonProjectCompanyApiDto,
                    cartoonProjectApiDto.CartoonProjectCreatorApiDtos);

                cmd.UpdatePreSendProperties(
                    applicationUser.Id,
                    applicationUser.Uid,
                    currentEdition.Id,
                    currentEdition.Uid,
                    "");

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

            return await Json(new { status = ApiStatus.Success, message = string.Format(Messages.EntityActionSuccessfull, Labels.CartoonProject, Labels.CreatedM) });
        }

        /// <summary>
        /// Get the Cartoon API filters
        /// </summary>
        /// <param name="request">The request.</param>
        /// <remarks>
        /// Listagem de opções para exibir no formulário 
        /// 
        /// Formulário Solicitado
        /// -----------------------------------------
        /// 
        /// 1. Dados do Projeto: 
        /// 
        /// e) Formato do Projeto (pode marcar apenas um)*;
        /// 
        /// Objeto para listagem: projectFormats     
        /// </remarks>
        [Route("filters"), HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Filters([FromUri] CartoonFiltersApiRequest request)
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

                var cartoonProjectFormats = await this.cartoonProjectFormatRepo.FindAllAsync();

                return await Json(new CartoonFiltersApiResponse
                {
                    ProjectFormats = cartoonProjectFormats.Select(ioeo => new ApiListItemBaseResponse()
                    {
                        Uid = ioeo.Uid,
                        Name = ioeo.GetNameTranslation(requestLanguage?.Code ?? defaultLanguage?.Code)
                    })?.ToList(),

                    Status = ApiStatus.Success
                });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new ApiBaseResponse { Status = ApiStatus.Error, Error = new ApiError { Code = "00004", Message = "Cartoon filters api failed." } });
            }
        }
    }
}
