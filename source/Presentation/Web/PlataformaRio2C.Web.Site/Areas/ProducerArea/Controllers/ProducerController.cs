using Microsoft.AspNet.Identity;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Web.Site.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.ProducerArea.Controllers
{
    
    [Authorize(Order = 1, Roles = "Producer")]
    public class ProducerController : PlataformaRio2C.Web.Site.Controllers.ProducerController
    {
        public ProducerController(IProducerAppService producerAppService, ICollaboratorAppService collaboratorAppService, IRepositoryFactory repositoryFactory)
            :base(producerAppService, collaboratorAppService, repositoryFactory)
        {            
        }

       
    }
}