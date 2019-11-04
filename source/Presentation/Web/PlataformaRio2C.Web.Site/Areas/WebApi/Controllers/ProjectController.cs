//using Microsoft.AspNet.Identity;
//using PlataformaRio2C.Application;
//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Application.Dtos;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
//{
//    [Authorize(Roles = "Producer,Player")]
//    [RoutePrefix("api/projects")]
//    public class ProjectController : BaseApiController
//    {
//        private readonly IProjectAppService _projectAppService;

//        public ProjectController(IProjectAppService projectAppService)
//        {
//            _projectAppService = projectAppService;
//        }

//        [Route("GetAllByUserPlayerId")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetAllByUserPlayerId([FromUri]ProjectPlayerFilterAppDto filter)
//        {
//            int userId = User.Identity.GetUserId<int>();

//            //var result = await Task.FromResult(_projectAppService.GetAllByUserPlayerId(filter, userId));

//            //if (result!= null && result.Any())
//            //{
//            //    return await Json(result);
//            //}          

//            return BadRequest();
//        }

//        [Route("GetStatusOption")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetStatusOption()
//        {
//            int userId = User.Identity.GetUserId<int>();

//            var result = await Task.FromResult(_projectAppService.GetStatusOption());

//            if (result != null && result.Any())
//            {
//                return await Json(result);
//            }

//            return BadRequest();
//        }

//        [Route("{uidProject}/saveplayerselection")]
//        [HttpPost]
//        public async Task<IHttpActionResult> SavePlayerSelection([FromUri]Guid uidProject, [FromBody]ProjectPlayerAppDto playerDto)
//        {
//            if (uidProject != Guid.Empty && playerDto != null && playerDto.Uid != Guid.Empty)
//            {
//                int userId = User.Identity.GetUserId<int>();

//                var result = await Task.FromResult(_projectAppService.SavePlayerSelection(playerDto.Uid, uidProject, userId));

//                if (result.IsValid)
//                {
//                    return await Json(result);
//                }
//                else
//                {
//                    return await BadRequest(result);
//                }
//            }

//            return BadRequest();
//        }

//        [Route("{uidProject}/removeplayerselection")]
//        [HttpPost]
//        public async Task<IHttpActionResult> RemovePlayerSelection([FromUri]Guid uidProject, [FromBody]ProjectPlayerAppDto playerDto)
//        {
//            if (uidProject != Guid.Empty && playerDto != null && playerDto.Uid != Guid.Empty)
//            {
//                int userId = User.Identity.GetUserId<int>();

//                var result = await Task.FromResult(_projectAppService.RemovePlayerSelection(playerDto.Uid, uidProject, userId));

//                if (result.IsValid)
//                {
//                    return await Json(result);
//                }
//                else
//                {
//                    return await BadRequest(result);
//                }
//            }

//            return BadRequest();
//        }

//        [Route("{uidProject}/sendtoplayers")]
//        [HttpPost]
//        public async Task<IHttpActionResult> SendToPlayers([FromUri]Guid uidProject, [FromBody] SendToPlayerAppDto sendToPlayerAppDto)
//        {
//            if (_projectAppService.SendToPlayersDisabled())
//            {
//                return await BadRequest(Messages.ClosedSendingToPlayers);
//            }

//            if (uidProject != Guid.Empty && sendToPlayerAppDto.UidsPlayers != null && sendToPlayerAppDto.UidsPlayers.Any())
//            {
//                int userId = User.Identity.GetUserId<int>();

//                var result = await Task.FromResult(_projectAppService.SendToPlayers(uidProject, sendToPlayerAppDto.UidsPlayers, userId));

//                if (result.IsValid)
//                {
//                    return await Json(Messages.ProjectSuccessfullySubmitted);
//                }
//                else
//                {
//                    return await BadRequest(result);
//                }
//            }

//            return BadRequest();
//        }

//        [Route("{uidProject}/acceptbyplayer")]
//        [HttpPost]
//        public async Task<IHttpActionResult> AcceptByPlayer([FromUri]Guid uidProject, [FromBody]ProjectPlayerAppDto playerDto)
//        {
//            if (playerDto.Uid != Guid.Empty)
//            {
//                int userId = User.Identity.GetUserId<int>();

//                var result = await Task.FromResult(_projectAppService.AcceptByPlayer(playerDto.Uid, uidProject, userId));

//                if (result.IsValid)
//                {
//                    return await Json(new { Message = Messages.EvaluationSavesSuccessfully, Status = Labels.ResourceManager.GetString(StatusProjectCodes.Accepted.ToString()), StatusCode = StatusProjectCodes.Accepted.ToString() });
//                }
//                else
//                {
//                    return await BadRequest(result);
//                }
//            }

//            return BadRequest();
//        }

//        [Route("{uidProject}/rejectbyplayer")]
//        [HttpPost]
//        public async Task<IHttpActionResult> RejectByPlayer([FromUri]Guid uidProject, [FromBody]ProjectPlayerAppDto playerDto)
//        {
//            if (playerDto.Uid != Guid.Empty)
//            {
//                int userId = User.Identity.GetUserId<int>();

//                var result = await Task.FromResult(_projectAppService.RejectByPlayer(playerDto.Uid, uidProject, userId, playerDto.Reason));

//                if (result.IsValid)
//                {
//                    return await Json(new { Message = Messages.EvaluationSavesSuccessfully, Status = Labels.ResourceManager.GetString(StatusProjectCodes.Rejected.ToString()), StatusCode = StatusProjectCodes.Rejected.ToString() });
//                }
//                else
//                {
//                    return await BadRequest(result);
//                }
//            }            
            
//            return await BadRequest(Messages.ReasonIsRequired);
//        }
//    }
//}
