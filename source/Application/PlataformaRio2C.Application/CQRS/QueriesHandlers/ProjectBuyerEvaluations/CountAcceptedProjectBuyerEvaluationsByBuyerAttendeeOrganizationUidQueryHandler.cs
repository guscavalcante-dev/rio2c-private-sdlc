// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-18-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-21-2024
// ***********************************************************************
// <copyright file="CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUidQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler&lt;PlataformaRio2C.Application.CQRS.Queries.CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUid, System.Int32&gt;" />
    public class CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUidQueryHandler : IRequestHandler<CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUid, int>
    {
        private readonly IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUidQueryHandler"/> class.
        /// </summary>
        /// <param name="projectBuyerEvaluationRepository">The repository.</param>
        public CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUidQueryHandler(IProjectBuyerEvaluationRepository projectBuyerEvaluationRepository)
        {
            this.projectBuyerEvaluationRepo = projectBuyerEvaluationRepository;
        }

        /// <summary>Handles the specified count all organizations asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<int> Handle(CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUid cmd, CancellationToken cancellationToken)
        {
            return await this.projectBuyerEvaluationRepo.CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUidAsync(cmd.BuyerAttendeeOrganizationUid);
        }
    }
}