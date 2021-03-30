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
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.Context;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1.0")]
    public class MusicBandsApiController : BaseApiController
    {
        private readonly IMediator commandBus;
        private readonly IdentityAutenticationService identityController;
        private readonly IEditionRepository editionsRepo;
        private readonly IMusicBandTypeRepository musicBandTypesRepo;
        private readonly IMusicGenreRepository musicGenresRepo;
        private readonly ITargetAudienceRepository targetAudiencesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandsApiController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        public MusicBandsApiController(
            IMediator commandBus,
            IdentityAutenticationService identityController,
            IEditionRepository editionsRepo,
            IMusicBandTypeRepository musicBandTypesRepo,
            IMusicGenreRepository musicGenresRepo,
            ITargetAudienceRepository targetAudiencesRepo)
        {
            this.commandBus = commandBus;
            this.identityController = identityController;
            this.editionsRepo = editionsRepo;
            this.musicBandTypesRepo = musicBandTypesRepo;
            this.musicGenresRepo = musicGenresRepo;
            this.targetAudiencesRepo = targetAudiencesRepo;
        }

        /// <summary>
        /// Creates the music band.
        /// </summary>
        /// <param name="musicBandApiDto">The music band dto.</param>
        /// <returns></returns>
        /// <exception cref="DomainException">
        /// </exception>
        [HttpPost]
        [Route("CreateMusicBand/{key?}")]
        public async Task<IHttpActionResult> CreateMusicBand(string key, HttpRequestMessage request)
        {
            var validationResult = new AppValidationResult();

            try
            {
                #region Initial Validations

                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateMusicBandApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var applicationUser = await identityController.FindByEmailAsync(Domain.Entities.User.BatchProcessUser.Email);
                if (applicationUser == null)
                {
                    throw new DomainException(Messages.UserNotFound);
                }

                var currentEdition = await editionsRepo.FindByIsCurrentAsync();
                if (currentEdition == null)
                {
                    throw new DomainException(Messages.CurrentEditionNotFound);
                }

                #endregion

                string jsonMusicBandApiDto = request.Content.ReadAsStringAsync().Result;
                var musicBandApiDto = Newtonsoft.Json.JsonConvert.DeserializeObject<MusicBandApiDto>(jsonMusicBandApiDto);
                if (!musicBandApiDto.IsValid())
                {
                    validationResult.Add(musicBandApiDto.ValidationResult);
                    throw new DomainException(Messages.CorrectFormValues);
                }

                var cmd = new CreateMusicBand(musicBandApiDto);
                cmd.UpdatePreSendProperties(
                    applicationUser.Id,
                    applicationUser.Uid,
                    currentEdition.Id,
                    currentEdition.Uid,
                    ""); //TODO: Implements User interface language?

                validationResult = await this.commandBus.Send(cmd);
                if (!validationResult.IsValid)
                {
                    throw new DomainException(Messages.CorrectFormValues);
                }
            }
            catch (DomainException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage(), errors = validationResult?.Errors?.Select(e => new { e.Code, e.Message }) });
            }
            catch (JsonSerializationException ex)
            {
                return await Json(new { status = ApiStatus.Error, message = ex.GetInnerMessage(), errors = validationResult?.Errors?.Select(e => new { e.Code, e.Message }) });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return await Json(new { status = ApiStatus.Error, message = Messages.WeFoundAndError });
            }

            return await Json(new { status = ApiStatus.Success, message = string.Format(Messages.EntityActionSuccessfull, Labels.MusicBand, Labels.CreatedF) });
        }

        /// <summary>
        /// Gets the music band types.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetMusicBandTypes")]
        public async Task<IHttpActionResult> GetMusicBandTypes(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateMusicBandApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var musicBandTypes = await this.musicBandTypesRepo.FindAllAsync();

                return await Json(new MusicBandTypesApiResponse
                {
                    MusicBandTypes = musicBandTypes.OrderBy(o => o.DisplayOrder).Select(mbt => new MusicBandTypeListApiItem()
                    {
                        Id = mbt.Id,
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
        /// Gets the music genres.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetMusicGenres")]
        public async Task<IHttpActionResult> GetMusicGenres(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateMusicBandApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                var musicGenres = await this.musicGenresRepo.FindAllAsync();

                return await Json(new MusicGenresApiResponse
                {
                    MusicGenres = musicGenres.OrderBy(o => o.DisplayOrder).Select(mg => new MusicGenreListApiItem()
                    {
                        Id = mg.Id,
                        Name = mg.Name
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
        /// Gets the target audiences.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("GetTargetAudiences")]
        public async Task<IHttpActionResult> GetTargetAudiences(string key)
        {
            try
            {
                if (key.ToLowerInvariant() != ConfigurationManager.AppSettings["CreateMusicBandApiKey"].ToLowerInvariant())
                {
                    throw new DomainException(Messages.AccessDenied);
                }

                //Using "ProjectType.Inovation" because in "[dbo].[MigrateMusicProjects]" procedure, its fixed too.
                var targetAudiences = await this.targetAudiencesRepo.FindAllByProjectTypeIdAsync(ProjectType.Inovation.Id);

                return await Json(new TargetAudiencesApiResponse
                {
                    TargetAudiences = targetAudiences.OrderBy(o => o.DisplayOrder).Select(ta => new TargetAudienceListApiItem()
                    {
                        Id = ta.Id,
                        Name = ta.Name
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

        #region Individual endpoints to Create MusicBandMember, MusicBandTeamMember, ReleasedMusicProject. (Disabled. Now this tasks is executed by 'CreateMusicBand' endpoint!)

        ///// <summary>
        ///// Creates the music band member.
        ///// </summary>
        ///// <param name="musicBandMemberApiDto">The music band member dto.</param>
        ///// <returns></returns>
        ///// <exception cref="DomainException">
        ///// </exception>
        //[HttpPost]
        //[Route("CreateMusicBandMember")]
        //public async Task<IHttpActionResult> CreateMusicBandMember([FromBody] MusicBandMemberApiDto musicBandMemberApiDto)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        //TODO: Implementar uma autenticação para executar a API? - Foi criado o parametro, porém como buscar essa informação na SystemParameters? 
        //        //TODO: Qual id de usuário passar para executar a API?
        //        var applicationUser = await identityController.FindByEmailAsync("admin@rio2c.com");
        //        if (applicationUser == null)
        //        {
        //            throw new DomainException(Messages.UserNotFound);
        //        }

        //        var currentEdition = await editionRepo.FindByIsCurrentAsync();
        //        if (currentEdition == null)
        //        {
        //            throw new DomainException(Messages.CurrentEditionNotFound);
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        var cmd = new CreateMusicBandMember(musicBandMemberApiDto);
        //        cmd.UpdatePreSendProperties(
        //            applicationUser.Id,
        //            applicationUser.Uid,
        //            currentEdition.Id,
        //            currentEdition.Uid,
        //            ""); //TODO: Implements User interface language?

        //        result = await this.commandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        return await Json(new { status = "error", message = ex.GetInnerMessage(), errors = result?.Errors?.Select(e => new { e.Code, e.Message }) });
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return await Json(new { status = "error", message = Messages.WeFoundAndError });
        //    }

        //    return await Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.MusicBandMember, Labels.CreatedM) });
        //}

        ///// <summary>
        ///// Creates the music band team member.
        ///// </summary>
        ///// <param name="musicBandTeamMemberApiDto">The music band team member dto.</param>
        ///// <returns></returns>
        ///// <exception cref="DomainException">
        ///// </exception>
        //[HttpPost]
        //[Route("CreateMusicBandTeamMember")]
        //public async Task<IHttpActionResult> CreateMusicBandTeamMember([FromBody] MusicBandTeamMemberApiDto musicBandTeamMemberApiDto)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        //TODO: Implementar uma autenticação para executar a API? - Foi criado o parametro, porém como buscar essa informação na SystemParameters? 
        //        //TODO: Qual id de usuário passar para executar a API?
        //        var applicationUser = await identityController.FindByEmailAsync("admin@rio2c.com");
        //        if (applicationUser == null)
        //        {
        //            throw new DomainException(Messages.UserNotFound);
        //        }

        //        var currentEdition = await editionRepo.FindByIsCurrentAsync();
        //        if (currentEdition == null)
        //        {
        //            throw new DomainException(Messages.CurrentEditionNotFound);
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        var cmd = new CreateMusicBandTeamMember(musicBandTeamMemberApiDto);
        //        cmd.UpdatePreSendProperties(
        //            applicationUser.Id,
        //            applicationUser.Uid,
        //            currentEdition.Id,
        //            currentEdition.Uid,
        //            ""); //TODO: Implements User interface language?

        //        result = await this.commandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        return await Json(new { status = "error", message = ex.GetInnerMessage(), errors = result?.Errors?.Select(e => new { e.Code, e.Message }) });
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return await Json(new { status = "error", message = Messages.WeFoundAndError });
        //    }

        //    return await Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.MusicBandTeamMember, Labels.CreatedM) });
        //}

        ///// <summary>
        ///// Creates the released music project.
        ///// </summary>
        ///// <param name="releasedMusicProjectApiDto">The released music project API dto.</param>
        ///// <returns></returns>
        ///// <exception cref="DomainException">
        ///// </exception>
        //[HttpPost]
        //[Route("CreateReleasedMusicProject")]
        //public async Task<IHttpActionResult> CreateReleasedMusicProject([FromBody] ReleasedMusicProjectApiDto releasedMusicProjectApiDto)
        //{
        //    var result = new AppValidationResult();

        //    try
        //    {
        //        //TODO: Implementar uma autenticação para executar a API? - Foi criado o parametro, porém como buscar essa informação na SystemParameters? 
        //        //TODO: Qual id de usuário passar para executar a API?
        //        var applicationUser = await identityController.FindByEmailAsync("admin@rio2c.com");
        //        if (applicationUser == null)
        //        {
        //            throw new DomainException(Messages.UserNotFound);
        //        }

        //        var currentEdition = await editionRepo.FindByIsCurrentAsync();
        //        if (currentEdition == null)
        //        {
        //            throw new DomainException(Messages.CurrentEditionNotFound);
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }

        //        var cmd = new CreateReleasedMusicProject(releasedMusicProjectApiDto);
        //        cmd.UpdatePreSendProperties(
        //            applicationUser.Id,
        //            applicationUser.Uid,
        //            currentEdition.Id,
        //            currentEdition.Uid,
        //            ""); //TODO: Implements User interface language?

        //        result = await this.commandBus.Send(cmd);
        //        if (!result.IsValid)
        //        {
        //            throw new DomainException(Messages.CorrectFormValues);
        //        }
        //    }
        //    catch (DomainException ex)
        //    {
        //        return await Json(new { status = "error", message = ex.GetInnerMessage(), errors = result?.Errors?.Select(e => new { e.Code, e.Message }) });
        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        return await Json(new { status = "error", message = Messages.WeFoundAndError });
        //    }

        //    return await Json(new { status = "success", message = string.Format(Messages.EntityActionSuccessfull, Labels.ProjectsReleased, Labels.CreatedM) });
        //}

        #endregion
    }
}
