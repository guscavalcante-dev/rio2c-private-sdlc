// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="SiteMailerBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SiteMailerBaseCommandHandler</summary>
    public class SiteMailerBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ISiteMailerService MailerService;

        /// <summary>Initializes a new instance of the <see cref="SiteMailerBaseCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="mailerService">The mailer service.</param>
        public SiteMailerBaseCommandHandler(IMediator commandBus, IUnitOfWork uow, ISiteMailerService mailerService)
            : base(commandBus, uow)
        {
            this.MailerService = mailerService;
        }
    }
}