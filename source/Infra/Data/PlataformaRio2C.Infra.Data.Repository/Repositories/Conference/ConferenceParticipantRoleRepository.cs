// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Conference Participant Role IQueryable Extensions

    /// <summary>
    /// ConferenceParticipantRoleIQueryableExtensions
    /// </summary>
    internal static class ConferenceParticipantRoleIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="conferenceParticipantRoleUid">The conference participant role uid.</param>
        /// <returns></returns>
        internal static IQueryable<ConferenceParticipantRole> FindByUid(this IQueryable<ConferenceParticipantRole> query, Guid conferenceParticipantRoleUid)
        {
            query = query.Where(cpr => cpr.Uid == conferenceParticipantRoleUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ConferenceParticipantRole> IsNotDeleted(this IQueryable<ConferenceParticipantRole> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>ConferenceParticipantRoleRepository</summary>
    public class ConferenceParticipantRoleRepository : Repository<PlataformaRio2CContext, ConferenceParticipantRole>, IConferenceParticipantRoleRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ConferenceParticipantRoleRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<ConferenceParticipantRole> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all dtos asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<ConferenceParticipantRoleDto>> FindAllDtosAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .Select(cpr => new ConferenceParticipantRoleDto
                            {
                                ConferenceParticipantRole = cpr,
                                ConferenceParticipantRoleTitleDtos = cpr.ConferenceParticipantRoleTitles.Where(cprt => !cprt.IsDeleted).Select(cprt => new ConferenceParticipantRoleTitleDto
                                {
                                    ConferenceParticipantRoleTitle = cprt,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                       Id = cprt.Language.Id,
                                       Uid = cprt.Language.Uid,
                                       Name = cprt.Language.Name,
                                       Code = cprt.Language.Code
                                    }
                                })
                            })
                            .ToListAsync();
        }
    }
}
