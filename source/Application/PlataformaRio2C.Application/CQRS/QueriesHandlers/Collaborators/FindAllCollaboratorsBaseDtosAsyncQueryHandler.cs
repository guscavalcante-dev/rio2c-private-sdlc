// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="FindAllCollaboratorsBaseDtosAsyncQueryHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Interfaces;
using X.PagedList;

namespace PlataformaRio2C.Application.CQRS.QueriesHandlers
{
    /// <summary>FindAllCollaboratorsBaseDtosAsyncQueryHandler</summary>
    public class FindAllCollaboratorsBaseDtosAsyncQueryHandler : IRequestHandler<FindAllCollaboratorsBaseDtosAsync, IPagedList<CollaboratorBaseDto>>
    {
        private readonly ICollaboratorRepository repo;

        /// <summary>Initializes a new instance of the <see cref="FindAllCollaboratorsBaseDtosAsyncQueryHandler"/> class.</summary>
        /// <param name="repository">The repository.</param>
        public FindAllCollaboratorsBaseDtosAsyncQueryHandler(ICollaboratorRepository repository)
        {
            this.repo = repository;
        }

        /// <summary>Handles the specified find all collaborators base dtos asynchronous.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorBaseDto>> Handle(FindAllCollaboratorsBaseDtosAsync cmd, CancellationToken cancellationToken)
        {
            return await this.repo.FindAllByDataTable(
                cmd.Page, 
                cmd.PageSize, 
                cmd.Keywords, 
                cmd.SortColumns, 
                cmd.CollaboratorsUids,
                cmd.OrganizationTypeId, 
                cmd.ShowAllEditions, 
                cmd.ShowAllExecutives, 
                cmd.ShowAllParticipants, 
                cmd.EditionId);
        }
    }
}