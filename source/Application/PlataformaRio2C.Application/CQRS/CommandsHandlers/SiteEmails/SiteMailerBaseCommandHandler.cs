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
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.Services;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>SiteMailerBaseCommandHandler</summary>
    public class SiteMailerBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IMailerService MailerService;
        protected readonly ISentEmailRepository SentEmailRepo;

        /// <summary>Initializes a new instance of the <see cref="SiteMailerBaseCommandHandler"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="mailerService">The mailer service.</param>
        /// <param name="sentEmailRepository">The sent email repository.</param>
        public SiteMailerBaseCommandHandler(IMediator commandBus, IUnitOfWork uow, IMailerService mailerService, ISentEmailRepository sentEmailRepository)
            : base(commandBus, uow)
        {
            this.MailerService = mailerService;
            this.SentEmailRepo = sentEmailRepository;
        }

        /// <summary>Gets the sent email by uid.</summary>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        public async Task<SentEmail> GetSentEmailByUid(Guid sentEmailUid)
        {
            var holding = await this.SentEmailRepo.GetAsync(sentEmailUid);
            if (holding == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Email, Labels.FoundM)));
            }

            return holding;
        }
    }
}