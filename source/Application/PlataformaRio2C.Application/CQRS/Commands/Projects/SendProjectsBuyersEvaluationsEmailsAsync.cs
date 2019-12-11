// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="SendProjectsBuyersEvaluationsEmailsAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendProjectsBuyersEvaluationsEmailsAsync</summary>
    public class SendProjectsBuyersEvaluationsEmailsAsync : IRequest<AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="SendProjectsBuyersEvaluationsEmailsAsync"/> class.</summary>
        public SendProjectsBuyersEvaluationsEmailsAsync()
        {
        }
    }
}