//using Microsoft.AspNet.Identity;
//using PlataformaRio2C.Application.Dtos;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers
//{
//    [Authorize(Roles = "Player,Producer")]
//    [RoutePrefix("api/messages")]
//    public class MessageController : BaseApiController
//    {
//        private readonly IMessageAppService _messageAppService;

//        public MessageController(IMessageAppService messageAppService)
//        {
//            _messageAppService = messageAppService;
//        }


//        [Route("")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetAll([FromUri]string email)
//        {
//            int userId = User.Identity.GetUserId<int>();
//            var result = await Task.FromResult(_messageAppService.GetAll(userId, email));

//            if (result != null)
//            {
//                return await Json(result);
//            }

//            return await NotFound(Messages.PlayerNotFound);
//        }

//        [Route("unreadsmessages")]
//        [HttpGet]
//        public async Task<IHttpActionResult> GetUnreadsMessages()
//        {
//            int userId = User.Identity.GetUserId<int>();
//            var result = await Task.FromResult(_messageAppService.GetUnreadsMessages(userId));

//            if (result != null)
//            {
//                return await Json(result);
//            }

//            return await NotFound(Messages.PlayerNotFound);
//        }


//        [Route("markmessageasread")]
//        [HttpPost]
//        public async Task<IHttpActionResult> MarkMessageAsRead([FromBody]MarkMessageAsReadDto dto)
//        {
//            int userId = User.Identity.GetUserId<int>();
//            var result = await Task.FromResult(_messageAppService.MarkMessageAsRead(userId, dto.Uids));

//            if (result != null)
//            {
//                return await Json(result);
//            }

//            return await NotFound(Messages.PlayerNotFound);
//        }

//    }

//    public class MarkMessageAsReadDto
//    {
//        public Guid[] Uids { get; set; }

//        public MarkMessageAsReadDto()
//        {

//        }
//    }
//}
