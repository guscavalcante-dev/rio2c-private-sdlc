// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-18-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-18-2024
// ***********************************************************************
// <copyright file="CountNegotiationsAcceptedByBuyerAttendeeOrganizationUidQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Dtos;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler&lt;PlataformaRio2C.Application.CQRS.Queries.CountNegotiationsAcceptedByBuyerAttendeeOrganizationUid, System.Int32&gt;" />
    public class CountNegotiationsAcceptedByBuyerAttendeeOrganizationUidQueryHandler : IRequestHandler<CountNegotiationsAcceptedByBuyerAttendeeOrganizationUid, int>
    {
        private readonly IProjectBuyerEvaluationRepository repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountNegotiationsAcceptedByBuyerAttendeeOrganizationUidQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CountNegotiationsAcceptedByBuyerAttendeeOrganizationUidQueryHandler(IProjectBuyerEvaluationRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified count all organizations asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<int> Handle(CountNegotiationsAcceptedByBuyerAttendeeOrganizationUid cmd, CancellationToken cancellationToken)
        {
            return await this.repo.CountNegotiationsAcceptedByBuyerAttendeeOrganizationUidAsync(cmd.BuyerAttendeeOrganizationUid);
        }
    }
}