// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Daniel Giese Rodrigues
// Created          : 27-02-2025             
//
// Last Modified By :Daniel Giese Rodrigues
// Last Modified On :27-02-2025             
// ***********************************************************************
// <copyright file="AttendeeNegotiationCollaboratorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region AttendeeNegotiationCollaborator IQueryable Extensions

    /// <summary>
    /// AttendeeNegotiationCollaboratorIQueryableExtensions
    /// </summary>
    internal static class AttendeeMusicBusinessRoundNegotiationCollaboratorIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBusinessRoundNegotiationCollaborator> IsNotDeleted(this IQueryable<AttendeeMusicBusinessRoundNegotiationCollaborator> query)
        {
            query = query.Where(n => !n.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeNegotiationCollaboratorRepository</summary>
    public class AttendeeMusicBusinessRoundNegotiationCollaboratorRepository : Repository<PlataformaRio2CContext, AttendeeMusicBusinessRoundNegotiationCollaborator>, IAttendeeMusicBusinessRoundNegotiationCollaboratorRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBusinessRoundNegotiationCollaboratorRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeMusicBusinessRoundNegotiationCollaboratorRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeMusicBusinessRoundNegotiationCollaborator> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }
    }
}