using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using LinqKit;

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

        /// <summary>
        /// Finds the by seller attendee organization uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="sellerAttendeeOrganizationUid">The seller attendee organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> FindBySellerAttendeeOrganizationUid(this IQueryable<MusicBusinessRoundProject> query, Guid sellerAttendeeOrganizationUid)
        {
            query = query.Where(p => p.SellerAttendeeCollaborator.Uid == sellerAttendeeOrganizationUid);

            return query;
        }

        /// <summary>
        /// Finds the by buyer attendee collabrator uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="buyerAttendeeCollaboratorUid">The buyer attendee collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> FindByBuyerAttendeeCollabratorUid(this IQueryable<MusicBusinessRoundProject> query, Guid buyerAttendeeCollaboratorUid)
        {
            query = query.Where(p => p.MusicBusinessRoundProjectBuyerEvaluations.Any(pbe => !pbe.IsDeleted
                                                                                              && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                                                              && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaborator.Uid == buyerAttendeeCollaboratorUid
                                                                                                                                                                            && !aoc.IsDeleted)));

            return query;
        }

        /// <summary>
        /// Determines whether this instance is finished.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> IsFinished(this IQueryable<MusicBusinessRoundProject> query)
        {
            query = query.Where(p => p.FinishDate.HasValue);

            return query;
        }

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> FindByKeywords(this IQueryable<MusicBusinessRoundProject> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<MusicBusinessRoundProject>(false);
                var innerMusicBusinessRoundProjectExpectationsForMeetings = PredicateBuilder.New<MusicBusinessRoundProject>(true);
                var innerSellerAttendeeCollaboratorFirstNameWhere = PredicateBuilder.New<MusicBusinessRoundProject>(true);
                var innerSellerAttendeeCollaboratorLastNamesWhere = PredicateBuilder.New<MusicBusinessRoundProject>(true);
                var innerSellerAttendeeCollaboratorCompanyNameWhere = PredicateBuilder.New<MusicBusinessRoundProject>(true);
                var innerSellerAttendeeCollaboratorStageNameWhere = PredicateBuilder.New<MusicBusinessRoundProject>(true);

                if (!string.IsNullOrEmpty(keywords))
                {
                    innerMusicBusinessRoundProjectExpectationsForMeetings = innerMusicBusinessRoundProjectExpectationsForMeetings.Or(p => p.MusicBusinessRoundProjectExpectationsForMeetings.Any(pt => !pt.IsDeleted && pt.Value.Contains(keywords)));
                    innerSellerAttendeeCollaboratorFirstNameWhere = innerSellerAttendeeCollaboratorFirstNameWhere.Or(sao => sao.SellerAttendeeCollaborator.Collaborator.FirstName.Contains(keywords));
                    innerSellerAttendeeCollaboratorLastNamesWhere = innerSellerAttendeeCollaboratorLastNamesWhere.Or(sao => sao.SellerAttendeeCollaborator.Collaborator.LastNames.Contains(keywords));
                    innerSellerAttendeeCollaboratorCompanyNameWhere = innerSellerAttendeeCollaboratorCompanyNameWhere.Or(sao => sao.SellerAttendeeCollaborator.Collaborator.CompanyName.Contains(keywords));
                    innerSellerAttendeeCollaboratorStageNameWhere = innerSellerAttendeeCollaboratorStageNameWhere.Or(sao => sao.SellerAttendeeCollaborator.Collaborator.StageName.Contains(keywords));
                }

                outerWhere = outerWhere.Or(innerMusicBusinessRoundProjectExpectationsForMeetings);
                outerWhere = outerWhere.Or(innerSellerAttendeeCollaboratorFirstNameWhere);
                outerWhere = outerWhere.Or(innerSellerAttendeeCollaboratorLastNamesWhere);
                outerWhere = outerWhere.Or(innerSellerAttendeeCollaboratorCompanyNameWhere);
                outerWhere = outerWhere.Or(innerSellerAttendeeCollaboratorStageNameWhere);
                
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>
        /// Finds the by project evaluation status.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> FindByProjectEvaluationStatus(this IQueryable<MusicBusinessRoundProject> query, Guid? evaluationStatusUid, Guid attendeeCollaboratorUid)
        {
            if (evaluationStatusUid != null)
            {
                query = query.Where(p => p.MusicBusinessRoundProjectBuyerEvaluations.Any(pbe => !pbe.IsDeleted
                                                                                                  && !pbe.ProjectEvaluationStatus.IsDeleted
                                                                                                  && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => !aoc.IsDeleted
                                                                                                                                                                                && aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid)
                                                                                                  && pbe.ProjectEvaluationStatus.Uid == evaluationStatusUid));
            }

            return query;
        }

        /// <summary>
        /// Converts to listpagedasync.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicBusinessRoundProjectDto>> ToListPagedAsync(this IQueryable<MusicBusinessRoundProjectDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
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
                            .Select(p => new MusicBusinessRoundProjectDto
                            {
                                Uid = p.Uid,
                                PlayerCategoriesThatHaveOrHadContract = p.PlayerCategoriesThatHaveOrHadContract,
                                //AttachmentUrl = m.AttachmentUrl,
                                FinishDate = p.FinishDate,
                                ProjectBuyerEvaluationsCount = p.ProjectBuyerEvaluationsCount,
                                IsFakeProject = false,
                                SellerAttendeeCollaboratorId = p.SellerAttendeeCollaboratorId,
                                SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                {
                                    //Organization = m.SellerAttendeeCollaborator.Organization,
                                    //Edition = m.SellerAttendeeCollaborator.Edition
                                    AttendeeCollaborator = p.SellerAttendeeCollaborator,
                                    Collaborator = p.SellerAttendeeCollaborator.Collaborator,
                                    JobTitlesDtos = p.SellerAttendeeCollaborator.Collaborator.JobTitles.Where(jb => !jb.IsDeleted).Select(jb => new CollaboratorJobTitleBaseDto
                                    {
                                        Id = jb.Id,
                                        Uid = jb.Uid,
                                        Value = jb.Value,
                                        LanguageDto = new LanguageBaseDto
                                        {
                                            Id = jb.Language.Id,
                                            Uid = jb.Language.Uid,
                                            Name = jb.Language.Name,
                                            Code = jb.Language.Code
                                        }
                                    })
                                },
                                MusicBusinessRoundProjectTargetAudienceDtos = p.MusicBusinessRoundProjectTargetAudience
                                    .Where(ta => !ta.IsDeleted)
                                    .Select(ta => new MusicBusinessRoundProjectTargetAudienceDto
                                    {
                                        MusicBusinessRoundProjectTargetAudience = ta,
                                        TargetAudience = ta.TargetAudience
                                    }),
                                MusicBusinessRoundProjectInterestDtos = p.MusicBusinessRoundProjectInterests
                                    .Where(pi => !pi.IsDeleted)
                                    .Select(pi => new MusicBusinessRoundProjectInterestDto
                                    {
                                        MusicBusinessRoundProjectInterest = pi,
                                        Interest = pi.Interest,
                                        InterestGroup = pi.Interest.InterestGroup
                                    }),
                                //PlayerCategoriesDtos = p.PlayerCategories
                                //    .Where(pc => !pc.IsDeleted)
                                //    .Select(pc => new MusicBusinessRoundProjectPlayerCategoryDto
                                //    {
                                //        MusicBusinessRoundProjectPlayerCategory = pc,
                                //        PlayerCategory = pc.PlayerCategory
                                //    }),
                                MusicBusinessRoundProjectExpectationsForMeetingDtos = p.MusicBusinessRoundProjectExpectationsForMeetings
                                    .Where(pe => !pe.IsDeleted)
                                    .Select(pe => new MusicBusinessRoundProjectExpectationsForMeetingDto
                                    {
                                        Value = pe.Value,
                                        Language = pe.Language
                                    }),
                                MusicBusinessRoundProjectBuyerEvaluationDtos = p.MusicBusinessRoundProjectBuyerEvaluations
                                    .Where(pbe => !pbe.IsDeleted)
                                    .Select(pbe => new MusicBusinessRoundProjectBuyerEvaluationDto
                                    {
                                        //MusicBusinessRoundProjectBuyerEvaluation = pbe, //TODO: Enable it! Its returning "Nome de coluna 'AttendeeCollaborator_Id' inválido." error
                                        BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                        {
                                            AttendeeOrganization = pbe.BuyerAttendeeOrganization,
                                            Organization = pbe.BuyerAttendeeOrganization.Organization,
                                        },
                                        ProjectEvaluationStatus = pbe.ProjectEvaluationStatus,
                                        ProjectEvaluationRefuseReason = pbe.ProjectEvaluationRefuseReason
                                    }),
                            });

            return await query
                        .OrderBy(m => m.FinishDate)
                        .ToListAsync();
        }

        /// <summary>Finds all dtos to evaluate asynchronous.</summary>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<MusicBusinessRoundProjectDto>> FindAllDtosToEvaluateAsync(
            Guid attendeeCollaboratorUid,
            string searchKeywords,
            Guid? interestUid,
            Guid? evaluationStatusUid,
            int page,
            int pageSize)
        {
            //var matchInterestsGroups = new List<Guid>
            //{
            //    InterestGroup.AudiovisualLookingFor.Uid,
            //    InterestGroup.AudiovisualProjectStatus.Uid,
            //    InterestGroup.AudiovisualPlatforms.Uid,
            //    InterestGroup.AudiovisualGenre.Uid
            //};

            var query = this.GetBaseQuery()
                                .FindByBuyerAttendeeCollabratorUid(attendeeCollaboratorUid)
                                .IsFinished()
                                .FindByKeywords(searchKeywords)
                                //.FindByInterestUid(interestUid)
                                .FindByProjectEvaluationStatus(evaluationStatusUid, attendeeCollaboratorUid)
                                .Select(p => new MusicBusinessRoundProjectDto
                                {
                                    Uid = p.Uid,
                                    CreateDate = p.CreateDate,
                                    //Project = p,
                                    SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                    {
                                        AttendeeCollaborator = p.SellerAttendeeCollaborator,
                                        Collaborator = p.SellerAttendeeCollaborator.Collaborator,
                                        //Edition = p.SellerAttendeeOrganization.Edition
                                    },
                                    MusicBusinessRoundProjectTargetAudienceDtos = p.MusicBusinessRoundProjectTargetAudience
                                        .Where(ta => !ta.IsDeleted)
                                        .Select(ta => new MusicBusinessRoundProjectTargetAudienceDto
                                        {
                                            MusicBusinessRoundProjectTargetAudience = ta,
                                            TargetAudience = ta.TargetAudience
                                        }),
                                    MusicBusinessRoundProjectExpectationsForMeetingDtos = p.MusicBusinessRoundProjectExpectationsForMeetings
                                        .Where(pe => !pe.IsDeleted)
                                        .Select(pe => new MusicBusinessRoundProjectExpectationsForMeetingDto
                                        {
                                            Value = pe.Value,
                                            Language = pe.Language
                                        }),
                                    MusicBusinessRoundProjectInterestDtos = p.MusicBusinessRoundProjectInterests
                                        .Where(pi => !pi.IsDeleted)
                                        .Select(pi => new MusicBusinessRoundProjectInterestDto
                                        {
                                            MusicBusinessRoundProjectInterest = pi,
                                            Interest = pi.Interest,
                                            InterestGroup = pi.Interest.InterestGroup
                                        }),
                                    PlayerCategoriesDtos = p.PlayerCategories
                                        .Where(pc => !pc.IsDeleted)
                                        .Select(pc => new MusicBusinessRoundProjectPlayerCategoryDto
                                        {
                                            MusicBusinessRoundProjectPlayerCategory = pc,
                                            PlayerCategory = pc.PlayerCategory
                                        }),
                                    MusicBusinessRoundProjectBuyerEvaluationDtos = p.MusicBusinessRoundProjectBuyerEvaluations
                                        .Where(pbe => !pbe.IsDeleted
                                                        && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                        && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                            .Any(aoc => !aoc.IsDeleted
                                                                                                        && aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid))
                                        .Select(mbe => new MusicBusinessRoundProjectBuyerEvaluationDto
                                        {
                                            //MusicBusinessRoundProjectBuyerEvaluation = mbe,
                                            BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                            {
                                                AttendeeOrganization = mbe.BuyerAttendeeOrganization,
                                                Organization = mbe.BuyerAttendeeOrganization.Organization,
                                                Edition = mbe.BuyerAttendeeOrganization.Edition
                                            },
                                            ProjectEvaluationStatus = mbe.ProjectEvaluationStatus,
                                            ProjectEvaluationRefuseReason = mbe.ProjectEvaluationRefuseReason
                                        }),
                                });

            return await query
                            //.OrderByDescending(ao => ao.InterestGroupsMatches.Count())
                            .OrderBy(pd => pd.CreateDate)
                            .ToListPagedAsync(page, pageSize);
        }
    }
}
