using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;
using LinqKit;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using PlataformaRio2C.Domain.Dtos.Music.BusinessRoundProject;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories.Music.Projects
{
    internal static class MusicBusinessRoundProjectIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> IsNotDeleted(this IQueryable<MusicBusinessRoundProject> query)
        {
            query = query.Where(p => !p.IsDeleted);

            return query;
        }
        internal static IQueryable<MusicBusinessRoundProject> FindBySellerAttendeeOrganizationUid(this IQueryable<MusicBusinessRoundProject> query, Guid sellerAttendeeOrganizationUid)
        {
            query = query.Where(p => p.SellerAttendeeOrganization.Uid == sellerAttendeeOrganizationUid);

            return query;
        }
    }

    public class MusicBusinessRoundProjectRepository : Repository<Context.PlataformaRio2CContext, MusicBusinessRoundProject>, IMusicBusinessRoundProjectRepository
    {
        private readonly IEditionRepository editioRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public MusicBusinessRoundProjectRepository(
            Context.PlataformaRio2CContext context,
            IEditionRepository editionRepository
            )
            : base(context)
        {
            this.editioRepo = editionRepository;
        }

        #region Private Methods

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicBusinessRoundProject> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the by seller attendee organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sellerAttendeeOrganizationUid">The seller attendee organization uid.</param>
        /// <returns></returns>
     
        #endregion

        /// <summary>Finds all dtos to sell asynchronous.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns></returns>
        public async Task<List<MusicBusinessRoundProjectDto>> FindAllMusicBusinessRoundProjectDtosToSellAsync(Guid attendeeOrganizationUid)
        {
            var query = this.GetBaseQuery() 
                            .FindBySellerAttendeeOrganizationUid(attendeeOrganizationUid) 
                            .Select(m => new MusicBusinessRoundProjectDto
                            {
                                SellerAttendeeOrganizationId = m.SellerAttendeeOrganizationId,
                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                {
                                    AttendeeOrganization = m.SellerAttendeeOrganization,
                                    Organization = m.SellerAttendeeOrganization.Organization,
                                    Edition = m.SellerAttendeeOrganization.Edition
                                },
                                PlayerCategoriesThatHaveOrHadContract = m.PlayerCategoriesThatHaveOrHadContract,
                                ExpectationsForOneToOneMeetings = m.ExpectationsForOneToOneMeetings,
                                AttachmentUrl = m.AttachmentUrl,
                                FinishDate = m.FinishDate,
                                ProjectBuyerEvaluationsCount = m.ProjectBuyerEvaluationsCount,
                                IsFakeProject = false,
                                MusicBusinessRoundProjectTargetAudienceDtos = m.MusicBusinessRoundProjectTargetAudience
                                    .Where(ta => !ta.IsDeleted)
                                    .Select(ta => new MusicBusinessRoundProjectTargetAudienceDto
                                    {
                                        MusicBusinessRoundProjectTargetAudience = ta,
                                        TargetAudience = ta.TargetAudience
                                    }),
                                MusicBusinessRoundProjectInterestDtos = m.MusicBusinessRoundProjectInterests
                                    .Where(i => !i.IsDeleted)
                                    .Select(i => new MusicBusinessRoundProjectInterestDto
                                    {
                                        MusicBusinessRoundProjectInterest = i,
                                        Interest = i.Interest
                                    }),
                                PlayerCategoriesDtos = m.PlayerCategories
                                    .Where(pc => !pc.IsDeleted)
                                    .Select(pc => new MusicBusinessRoundProjectPlayerCategoryDto
                                    {
                                        MusicBusinessRoundProjectPlayerCategory = pc,
                                        PlayerCategory = pc.PlayerCategory
                                    })
                            });

            return await query
                        .OrderBy(m => m.FinishDate)
                        .ToListAsync();
        }
    }
}
