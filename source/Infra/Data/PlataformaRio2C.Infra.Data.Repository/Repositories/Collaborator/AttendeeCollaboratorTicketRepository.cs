// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 10-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-03-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicketRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Attendee Collaborator Ticket IQueryable Extensions

    /// <summary>
    /// AttendeeeCollaboratorTicketIQueryableExtensions
    /// </summary>
    internal static class AttendeeeCollaboratorTicketIQueryableExtensions
    {
        /// <summary>Finds the by collaborator identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorId">The collaborator identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaboratorTicket> FindByCollaboratorId(this IQueryable<AttendeeCollaboratorTicket> query, int collaboratorId)
        {
            query = query.Where(act => act.AttendeeCollaborator.Collaborator.Id == collaboratorId);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaboratorTicket> FindByEditionId(this IQueryable<AttendeeCollaboratorTicket> query, int editionId)
        {
            query = query.Where(act => act.AttendeeCollaborator.EditionId == editionId);

            return query;
        }

        /// <summary>
        /// Finds the by barcode.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="barcode">The barcode.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaboratorTicket> FindByBarcode(this IQueryable<AttendeeCollaboratorTicket> query, string barcode)
        {
            query = query.Where(act => act.Barcode == barcode);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaboratorTicket> IsNotDeleted(this IQueryable<AttendeeCollaboratorTicket> query)
        {
            query = query.Where(act => !act.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeCollaboratorTicketRepository</summary>
    public class AttendeeCollaboratorTicketRepository : Repository<PlataformaRio2CContext, AttendeeCollaboratorTicket>, IAttendeeCollaboratorTicketRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTicketRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeCollaboratorTicketRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeCollaboratorTicket> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds the dto by barcode.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="barcode">The barcode.</param>
        /// <returns></returns>
        public async Task<AttendeeCollaboratorTicketDto> FindDtoByBarcode(int editionId, string barcode)
        {
            var query = this.GetBaseQuery(true)
                                .FindByEditionId(editionId)
                                .FindByBarcode(barcode);

            return await query
                            .Select(act => new AttendeeCollaboratorTicketDto
                            {
                                Barcode = act.Barcode
                            })
                            .FirstOrDefaultAsync();
        }
    }
}