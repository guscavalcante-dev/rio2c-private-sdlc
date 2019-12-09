// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="SendUnreadConversationsEmailsAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendUnreadConversationsEmailsAsync</summary>
    public class SendUnreadConversationsEmailsAsync : IRequest<AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="SendUnreadConversationsEmailsAsync"/> class.</summary>
        public SendUnreadConversationsEmailsAsync()
        {
        }
    }
}