using PlataformaRio2C.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.ProducerArea.Controllers
{
    public class Rio2CNetworkController : Site.Controllers.Rio2CNetworkController
    {
        public Rio2CNetworkController(IMessageAppService messageAppService)
            :base(messageAppService)
        {            
        }
    }
}