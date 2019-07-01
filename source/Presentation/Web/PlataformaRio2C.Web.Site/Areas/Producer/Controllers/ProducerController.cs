// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="ProducerController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Domain.Interfaces;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Areas.Producer.Controllers
{
    /// <summary>ProducerController</summary>
    [Authorize(Order = 1, Roles = "Producer")]
    public class ProducerController : PlataformaRio2C.Web.Site.Controllers.ProducerController
    {
        /// <summary>Initializes a new instance of the <see cref="ProducerController"/> class.</summary>
        /// <param name="producerAppService">The producer application service.</param>
        /// <param name="collaboratorAppService">The collaborator application service.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public ProducerController(IProducerAppService producerAppService, ICollaboratorAppService collaboratorAppService, IRepositoryFactory repositoryFactory)
            :base(producerAppService, collaboratorAppService, repositoryFactory)
        {            
        }
    }
}