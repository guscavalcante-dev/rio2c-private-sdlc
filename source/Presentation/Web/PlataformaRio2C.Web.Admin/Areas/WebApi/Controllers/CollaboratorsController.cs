using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/collaborator")]
    public class CollaboratorsController : BaseApiController
    {
        private readonly ICollaboratorAppService _appService;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IMusicalCommissionRepository _musicalCommissionRepository;
        private readonly ISpeakerAppService _speakerAppService;


        public CollaboratorsController(ICollaboratorAppService appService, ISpeakerAppService speakerAppService, IRepositoryFactory repositoryFactory)
        {
            _appService = appService;
            _speakerRepository = repositoryFactory.SpeakerRepository;
            _musicalCommissionRepository = repositoryFactory.MusicalCommissionRepository;
            _speakerAppService = speakerAppService;
        }

        [Route("GetOptions")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOptions([FromUri]string term)
        {
            var result = await Task.FromResult(_appService.GetOptions(term));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("thumbImage")]
        [HttpGet]
        public async Task<IHttpActionResult> GetThumbImage([FromUri]Guid uid)
        {
            var result = await Task.FromResult(_appService.GetThumbImage(uid));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("image")]
        [HttpGet]
        public async Task<IHttpActionResult> GetImage([FromUri]Guid uid)
        {
            var result = await Task.FromResult(_appService.GetImage(uid));

            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("Speaker")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSpeaker()
        {
            //var result = await Task.FromResult(_appService.GetAllSimple());

            //var viewModel = await Task.FromResult(_appService.GetAllSimple().ToList());
            //var viewModel = await Task.FromResult(_speakerAppService.GetAllSimple().ToList());
            var viewModel = await Task.FromResult(_speakerRepository.GetAllSimple());

            List<CollaboratorItemListAppViewModel> entity = new List<CollaboratorItemListAppViewModel>();

            if (viewModel != null)
            {
                int i = 0;
                foreach (Speaker speaker in viewModel)
                {
                    if (speaker.Collaborator != null)
                    {
                        var collaborator = _appService.Get(speaker.Collaborator.Uid).MapReverse();

                        if (collaborator != null)
                        {
                            var teste = new CollaboratorItemListAppViewModel(collaborator);
                            teste.Uid = speaker.Collaborator.Uid;
                            entity.Add(teste);
                        }
                    }

                }
            }

            return await Json(entity);
        }

        [Route("MusicalCommission")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMusicalCommission()
        {
            //var result = await Task.FromResult(_appService.GetAllSimple());

            //var viewModel = await Task.FromResult(_musicalCommissionRepository.GetAllSimple());
            var viewModel = _musicalCommissionRepository.GetAllSimple();

            List<CollaboratorItemListAppViewModel> entity = new List<CollaboratorItemListAppViewModel>();

            if (viewModel != null)
            {
                int i = 0;
                foreach (MusicalCommission mCommission in viewModel)
                {
                    if (mCommission.CollaboratorId != 0)
                    {
                        //if(mCommission.Collaborator == null)
                        //{
                        //    mCommission.Collaborator = _appService.Get(mCommission.CollaboratorId);
                        //}
                        var collaborator = _appService.Get(mCommission.CollaboratorId).MapReverse();

                        if (collaborator != null)
                        {
                            var teste = new CollaboratorItemListAppViewModel(collaborator);
                            teste.Uid = mCommission.Collaborator.Uid;
                            entity.Add(teste);
                        }
                    }

                }
            }

            return await Json(entity);
        }
    }
}
