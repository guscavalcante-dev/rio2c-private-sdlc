using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Dtos;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/scheduleonetoonemeetings")]
    public class ScheduleOneToOneMeetingsController : BaseApiController
    {
        private readonly INegotiationAppService _appService;
        private readonly IScheduleAppService _scheduleAppService;

        public ScheduleOneToOneMeetingsController(INegotiationAppService appService, IScheduleAppService scheduleAppService)
        {
            _appService = appService;
            _scheduleAppService = scheduleAppService;
        }
        
        [Route("process")]
        [HttpPost]
        public async Task<IHttpActionResult> Process()
        {
            int userId = User.Identity.GetUserId<int>();
            var result = _appService.ProcessScheduleOneToOneMeetings(userId);

            if (result.IsValid)
            {
                return await Json(result);
            }
            else
            {
                return await BadRequest(result);
            }
        }

        [Route("process")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProcess()
        {
            int userId = User.Identity.GetUserId<int>();
            var result = _appService.ResultProcessScheduleOneToOneMeetings(userId);

            if (result != null)
            {
                return await Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("list")]
        [HttpGet]
        public async Task<IHttpActionResult> GetList()
        {
            int userId = User.Identity.GetUserId<int>();
            var result = _appService.GetNegotiations(userId);

            if (result != null)
            {
                return await Json(result);
            }
            else
            {
                return BadRequest();
            }
        }
        
        [Route("generateTemp")]
        [HttpPost]
        public async Task<IHttpActionResult> GenerateTemp()
        {
            int userId = User.Identity.GetUserId<int>();
            var result = _appService.GenerateTemp(userId);
            if (result.IsValid)
            {
                return await Json(result);
            }
            else
            {
                return await BadRequest(result);
            }
        }
        
        [Route("delete")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete([FromUri]Guid uid)
        {
            int userId = User.Identity.GetUserId<int>();
            var result = _appService.Delete(userId, uid);

            if (result.IsValid)
            {
                return await Json("Negociação apagada com sucesso!");
            }
            else
            {
                return await BadRequest(result);
            }
        }


        [Route("getoptionsdates")]
        [HttpGet]
        public async Task<IHttpActionResult> GetoptionsDates()
        {
            var result = _appService.GetOptionsDates();

            if (result != null)
            {
                return await Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("getoptionsroomsbydate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOptionsRoomsByDate([FromUri]string date)
        {
            var result = _appService.GetOptionsRoomsByDate(date);

            if (result != null)
            {
                return await Json(result);
            }
            else
            {
                return BadRequest();
            }
        }


        [Route("getoptionsstarttime")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOptionsStartTime([FromUri]string date)
        {
            var result = _appService.GetOptionsStartTime(date);

            if (result != null)
            {
                return await Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("getoptionstables")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOptionsTables([FromUri]string date, [FromUri]string startTime, [FromUri]Guid room)
        {
            var result = _appService.GetOptionsTables(date, startTime, room);

            if (result != null)
            {
                return await Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("players")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPlayers()
        {            
            var result = _appService.GetPlayers();

            if (result != null)
            {
                return await Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("producers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProducers()
        {            
            var result = _appService.GetProducers();

            if (result != null)
            {
                return await Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("sendEmailToPlayers")]
        [HttpPost]
        public async Task<IHttpActionResult> SendEmailToPlayers([FromBody]UidsDto uids)
        {            
            var result = _appService.SendEmailToPlayers(uids.Uids);

            if (result.IsValid)
            {
                return await Json(result);
            }
            else
            {
                return await BadRequest(result);
            }
        }

        [Route("sendEmailToProducers")]
        [HttpPost]
        public async Task<IHttpActionResult> SendEmailToProducers([FromBody]UidsDto uids)
        {
            var result = _appService.SendEmailToProducers(uids.Uids);

            if (result.IsValid)
            {
                return await Json(result);
            }
            else
            {
                return await BadRequest(result);
            }
        }

        [Route("completeschedule")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCompleteSchedule()
        {
            var result = await Task.FromResult(_appService.GetAllNegotiationsGroupTable());
            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("completeschedule")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCompleteSchedule([FromUri]Guid uidCollaborator)
        {            
            var result = await Task.FromResult(_scheduleAppService.GetComplete(uidCollaborator));
            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

       

        [Route("scheduleplayer")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSchedulePlayer([FromUri]Guid uidCollaborator)
        {            
            var result = await Task.FromResult(_scheduleAppService.GetSchedulePlayer(uidCollaborator));
            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("scheduleproducer")]
        [HttpGet]
        public async Task<IHttpActionResult> GetScheduleProducer([FromUri]Guid uidCollaborator)
        {
            int userId = User.Identity.GetUserId<int>();
            var result = await Task.FromResult(_scheduleAppService.GetScheduleProducer(uidCollaborator));
            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("days")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDays()
        {
            var result = await Task.FromResult(_scheduleAppService.GetDays());
            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }

        [Route("completeschedulegroupbytable")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCompleteScheduleGroupByTable()
        {
            var result = await Task.FromResult(_appService.GetAllNegotiationsGroupTable());
            if (result != null)
            {
                return await Json(result);
            }

            return NotFound();
        }
    }
}
