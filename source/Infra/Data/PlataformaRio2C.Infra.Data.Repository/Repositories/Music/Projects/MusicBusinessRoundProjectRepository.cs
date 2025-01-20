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
        /// <param name="showAll">if set to <c>true</c> [show all].</param>
        /// <param name="projectModalityIds">if set to <c>true</c> [show all].</param>
        /// <returns></returns>
        public async Task<List<ProjectDto>> FindAllDtosToSellAsync(Guid attendeeOrganizationUid, bool showAll, List<int> projectModalityIds)
        {
            var query = this.GetBaseQuery()
                                .FindBySellerAttendeeOrganizationUid(attendeeOrganizationUid)
                                .Select(p => new ProjectDto
                                {
                                    //Project = p,
                                    //ProjectType = p.ProjectType,
                                    //SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    //{
                                    //    AttendeeOrganization = p.SellerAttendeeOrganization,
                                    //    Organization = p.SellerAttendeeOrganization.Organization,
                                    //    Edition = p.SellerAttendeeOrganization.Edition
                                    //},
                                    //ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                    //{
                                    //    ProjectTitle = t,
                                    //    Language = t.Language
                                    //}),
                                    //ProjectLogLineDtos = p.ProjectLogLines.Where(ll => !ll.IsDeleted).Select(ll => new ProjectLogLineDto
                                    //{
                                    //    ProjectLogLine = ll,
                                    //    Language = ll.Language
                                    //}),
                                    //ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted).Select(i => new ProjectInterestDto
                                    //{
                                    //    ProjectInterest = i,
                                    //    Interest = i.Interest,
                                    //    InterestGroup = i.Interest.InterestGroup
                                    //}),
                                    //ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations.Where(be => !be.IsDeleted).Select(be => new ProjectBuyerEvaluationDto
                                    //{
                                    //    ProjectBuyerEvaluation = be,
                                    //    BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    //    {
                                    //        AttendeeOrganization = be.BuyerAttendeeOrganization,
                                    //        Organization = be.BuyerAttendeeOrganization.Organization,
                                    //        Edition = be.BuyerAttendeeOrganization.Edition
                                    //    },
                                    //    ProjectEvaluationStatus = be.ProjectEvaluationStatus,
                                    //    ProjectEvaluationRefuseReason = be.ProjectEvaluationRefuseReason
                                    //}),
                                    //ProjectModalityDto = new ProjectModalityDto
                                    //{
                                    //    Id = p.ProjectModality.Id,
                                    //    Uid = p.ProjectModality.Uid,
                                    //    Name = p.ProjectModality.Name,
                                    //}
                                });

            return await query
                            .OrderBy(pd => pd.Project.CreateDate)
                            .ToListAsync();
        }
    }
}
