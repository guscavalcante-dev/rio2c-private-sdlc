// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Daniel Giese
// Created          : 05/03/2025
//
// Last Modified By :  Daniel Giese
// Last Modified On :  05/03/2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectBuyerEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Music Business Round Project BuyerEvaluation IQueryable Extensions

    /// <summary>
    /// ProjectBuyerEvaluationIQueryableExtensions
    /// </summary>
    internal static class MusicBusinessRoundProjectBuyerEvaluationIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProjectBuyerEvaluation> IsNotDeleted(this IQueryable<MusicBusinessRoundProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => !pbe.IsDeleted && !pbe.MusicBusinessRoundProject.IsDeleted && !pbe.MusicBusinessRoundProject.SellerAttendeeCollaborator.IsDeleted);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProjectBuyerEvaluation> FindByEditionId(this IQueryable<MusicBusinessRoundProjectBuyerEvaluation> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(pbe => (showAllEditions || (!pbe.MusicBusinessRoundProject.SellerAttendeeCollaborator.IsDeleted
                                                            && pbe.MusicBusinessRoundProject.SellerAttendeeCollaborator.EditionId == editionId)));

            return query;
        }

        /// <summary>Finds the by project evaluation status uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectEvaluationStatusUid">The project evaluation status uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProjectBuyerEvaluation> FindByProjectEvaluationStatusUid(this IQueryable<MusicBusinessRoundProjectBuyerEvaluation> query, Guid projectEvaluationStatusUid)
        {
            query = query.Where(pbe => pbe.ProjectEvaluationStatus.Uid == projectEvaluationStatusUid);

            return query;
        }


        internal static IQueryable<MusicBusinessRoundProjectBuyerEvaluation> FindByUid(this IQueryable<MusicBusinessRoundProjectBuyerEvaluation> query, Guid Uid)
        {
            query = query.Where(pbe => pbe.Uid == Uid);

            return query;
        }

        /// <summary>Determines whether [is project finished].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProjectBuyerEvaluation> IsProjectFinished(this IQueryable<MusicBusinessRoundProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => pbe.MusicBusinessRoundProject.FinishDate.HasValue);

            return query;
        }
    }

    #endregion

    /// <summary>ProjectBuyerEvaluationRepository</summary>
    public class MusicBusinessRoundProjectBuyerEvaluationRepository : Repository<Context.PlataformaRio2CContext, MusicBusinessRoundProjectBuyerEvaluation>, IMusicBusinessRoundProjectBuyerEvaluationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundProjectBuyerEvaluationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MusicBusinessRoundProjectBuyerEvaluationRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicBusinessRoundProjectBuyerEvaluation> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }



        public async Task<List<MusicBusinessRoundProjectBuyerEvaluation>> FindAllForGenerateNegotiationsAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByProjectEvaluationStatusUid(ProjectEvaluationStatus.Accepted.Uid)
                                .IsProjectFinished()
                                .Include(pbe => pbe.MusicBusinessRoundProject)
                                .Include(pbe => pbe.MusicBusinessRoundProject.SellerAttendeeCollaborator)
                                .Include(pbe => pbe.BuyerAttendeeOrganization);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds the dto asynchronous.
        /// </summary>
        /// <param name="projectBuyerEvaluationUid">The project buyer evaluation uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundProjectBuyerEvaluationDto> FindDtoAsync(Guid Uid)
        {
            var query = this.GetBaseQuery()
                               .FindByUid(Uid)
                               .Select(pbe => new MusicBusinessRoundProjectBuyerEvaluationDto
                               {
                                   MusicBusinessRoundProjectBuyerEvaluation = pbe,
                                   BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                   {
                                       AttendeeOrganization = pbe.BuyerAttendeeOrganization,
                                       Organization = pbe.BuyerAttendeeOrganization.Organization
                                   },
                                   MusicBusinessRoundProjectDto = new MusicBusinessRoundProjectDto
                                   {
                                       SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                       {
                                           AttendeeCollaborator = pbe.MusicBusinessRoundProject.SellerAttendeeCollaborator,
                                       },
                                   }
                               });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>Counts the negotiation scheduled asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<int> CountNegotiationScheduledAsync(int editionId, bool showAllEditions = false)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions)
                                .FindByProjectEvaluationStatusUid(ProjectEvaluationStatus.Accepted.Uid)
                                .IsProjectFinished();

            return await query.CountAsync();
        }
    }
}


