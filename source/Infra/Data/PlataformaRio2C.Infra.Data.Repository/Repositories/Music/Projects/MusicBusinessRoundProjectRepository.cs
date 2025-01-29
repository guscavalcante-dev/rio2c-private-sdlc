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
        /// Finds the by uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> FindByUid(this IQueryable<MusicBusinessRoundProject> query, Guid projectUid)
        {
            query = query.Where(p => p.Uid == projectUid);

            return query;
        }

        /// <summary>
        /// Finds the by edition identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> FindByEditionId(this IQueryable<MusicBusinessRoundProject> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(p => (showAllEditions || p.SellerAttendeeCollaborator.EditionId == editionId));

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
            //query = query.Where(p => p.MusicBusinessRoundProjectBuyerEvaluations.Any(pbe => !pbe.IsDeleted
            //                                                                                  && !pbe.BuyerAttendeeOrganization.IsDeleted
            //                                                                                  && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaborator.Uid == buyerAttendeeCollaboratorUid
            //                                                                                                                                                                && !aoc.IsDeleted)));

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
            //if (evaluationStatusUid != null)
            //{
            //    query = query.Where(p => p.MusicBusinessRoundProjectBuyerEvaluations.Any(pbe => !pbe.IsDeleted
            //                                                                                      && !pbe.ProjectEvaluationStatus.IsDeleted
            //                                                                                      && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => !aoc.IsDeleted
            //                                                                                                                                                                    && aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid)
            //                                                                                      && pbe.ProjectEvaluationStatus.Uid == evaluationStatusUid));
            //}

            return query;
        }

        /// <summary>
        /// Finds the by target audience.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="targetAudienceUid">The target audience uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> FindByTargetAudience(this IQueryable<MusicBusinessRoundProject> query, Guid? targetAudienceUid)
        {
            if (targetAudienceUid != null)
            {
                query = query.Where(p => p.MusicBusinessRoundProjectTargetAudiences.Any(ta => !ta.IsDeleted && 
                                                                                              !ta.TargetAudience.IsDeleted && 
                                                                                              ta.TargetAudience.Uid == targetAudienceUid));
            }

            return query;
        }

        /// <summary>
        /// Finds the by interest.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundProject> FindByInterest(this IQueryable<MusicBusinessRoundProject> query, Guid? interestUid)
        {
            if (interestUid != null)
            {
                query = query.Where(p => p.MusicBusinessRoundProjectInterests.Any(i => !i.IsDeleted &&
                                                                                       !i.Interest.IsDeleted &&
                                                                                        i.Interest.Uid == interestUid));
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
                                MusicBusinessRoundProjectTargetAudienceDtos = p.MusicBusinessRoundProjectTargetAudiences
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
                                        MusicBusinessRoundProjectBuyerEvaluation = pbe,
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

        /// <summary>
        /// Finds all dtos to evaluate asynchronous.
        /// </summary>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="targetAudienceUid">The target audience uid.</param>
        /// <param name="interestAreaInterestUid">The interest area interest uid.</param>
        /// <param name="businessRoundObjetiveInterestsUid">The business round objetive interests uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<MusicBusinessRoundProjectDto>> FindAllDtosToEvaluateAsync(
            Guid attendeeCollaboratorUid,
            string searchKeywords,
            Guid? evaluationStatusUid,
            Guid? targetAudienceUid,
            Guid? interestAreaInterestUid,
            Guid? businessRoundObjetiveInterestsUid,
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
                                .FindByProjectEvaluationStatus(evaluationStatusUid, attendeeCollaboratorUid)
                                .FindByTargetAudience(targetAudienceUid)
                                .FindByInterest(interestAreaInterestUid)
                                .FindByInterest(businessRoundObjetiveInterestsUid)
                                .Select(p => new MusicBusinessRoundProjectDto
                                {
                                    Uid = p.Uid,
                                    CreateDate = p.CreateDate,
                                    PlayerCategoriesThatHaveOrHadContract = p.PlayerCategoriesThatHaveOrHadContract,
                                    AttachmentUrl = p.AttachmentUrl,
                                    SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                    {
                                        AttendeeCollaborator = p.SellerAttendeeCollaborator,
                                        Collaborator = p.SellerAttendeeCollaborator.Collaborator,
                                    },
                                    MusicBusinessRoundProjectTargetAudienceDtos = p.MusicBusinessRoundProjectTargetAudiences
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
                                    MusicBusinessRoundProjectPlayerCategoryDtos = p.MusicBusinessRoundProjectPlayerCategories
                                        .Where(pc => !pc.IsDeleted)
                                        .Select(pc => new MusicBusinessRoundProjectPlayerCategoryDto
                                        {
                                            MusicBusinessRoundProjectPlayerCategory = pc,
                                            PlayerCategory = pc.PlayerCategory
                                        }),
                                    MusicBusinessRoundProjectActivityDtos = p.MusicBusinessRoundProjectActivities
                                        .Where(pa => !pa.IsDeleted)
                                        .Select(pa => new MusicBusinessRoundProjectActivityDto
                                        {
                                            MusicBusinessRoundProjectActivity = pa,
                                            Activity = pa.Activity
                                        }),
                                    MusicBusinessRoundProjectBuyerEvaluationDtos = p.MusicBusinessRoundProjectBuyerEvaluations
                                        .Where(pbe => !pbe.IsDeleted
                                                        && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                        && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                            .Any(aoc => !aoc.IsDeleted
                                                                                                        && aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid))
                                        .Select(pbe => new MusicBusinessRoundProjectBuyerEvaluationDto
                                        {
                                            MusicBusinessRoundProjectBuyerEvaluation = pbe,
                                            BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                            {
                                                AttendeeOrganization = pbe.BuyerAttendeeOrganization,
                                                Organization = pbe.BuyerAttendeeOrganization.Organization,
                                                Edition = pbe.BuyerAttendeeOrganization.Edition
                                            },
                                            ProjectEvaluationStatus = pbe.ProjectEvaluationStatus,
                                            ProjectEvaluationRefuseReason = pbe.ProjectEvaluationRefuseReason
                                        }),
                                });

            return await query
                            //.OrderByDescending(ao => ao.InterestGroupsMatches.Count())
                            .OrderBy(pd => pd.CreateDate)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds the site details dto by project uid asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(p => new MusicBusinessRoundProjectDto
                            {

                                Uid = p.Uid,
                                CreateDate = p.CreateDate,
                                PlayerCategoriesThatHaveOrHadContract = p.PlayerCategoriesThatHaveOrHadContract,
                                AttachmentUrl = p.AttachmentUrl,
                                FinishDate = p.FinishDate,
                                SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                {
                                    AttendeeCollaborator = p.SellerAttendeeCollaborator,
                                    Collaborator = p.SellerAttendeeCollaborator.Collaborator,
                                    JobTitlesDtos = p.SellerAttendeeCollaborator.Collaborator.JobTitles.Where(jt => !jt.IsDeleted).Select(jt => new CollaboratorJobTitleBaseDto
                                    {
                                        Id = jt.Id,
                                        Uid = jt.Uid,
                                        Value = jt.Value,
                                        LanguageDto = new LanguageBaseDto
                                        {
                                            Id = jt.Language.Id,
                                            Uid = jt.Language.Uid,
                                            Name = jt.Language.Name,
                                            Code = jt.Language.Code
                                        }
                                    }),
                                },
                                MusicBusinessRoundProjectBuyerEvaluationDtos = p.MusicBusinessRoundProjectBuyerEvaluations
                                    .Where(pbe => !pbe.IsDeleted)
                                    .Select(pbe => new MusicBusinessRoundProjectBuyerEvaluationDto
                                    {
                                        MusicBusinessRoundProjectBuyerEvaluation = pbe,
                                        BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                        {
                                            AttendeeOrganization = pbe.BuyerAttendeeOrganization,
                                            Organization = pbe.BuyerAttendeeOrganization.Organization,
                                        },
                                        ProjectEvaluationStatus = pbe.ProjectEvaluationStatus,
                                        ProjectEvaluationRefuseReason = pbe.ProjectEvaluationRefuseReason
                                    }),
                            })
                            .FirstOrDefaultAsync();
        }

        public async Task<MusicBusinessRoundProjectDto> FindDtoToEvaluateAsync(Guid attendeeCollaboratorUid, Guid projectUid)
        {
            //var matchInterestsGroups = new List<Guid>
            //{
            //    InterestGroup.AudiovisualLookingFor.Uid,
            //    InterestGroup.AudiovisualProjectStatus.Uid,
            //    InterestGroup.AudiovisualPlatforms.Uid,
            //    InterestGroup.AudiovisualGenre.Uid
            //};

            var query = this.GetBaseQuery()
                                .FindByUid(projectUid)
                                .IsFinished()
                                .Select(p => new MusicBusinessRoundProjectDto
                                {
                                    Uid = p.Uid,
                                    CreateDate = p.CreateDate,
                                    PlayerCategoriesThatHaveOrHadContract = p.PlayerCategoriesThatHaveOrHadContract,
                                    AttachmentUrl = p.AttachmentUrl,
                                    SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                    {
                                        AttendeeCollaborator = p.SellerAttendeeCollaborator,
                                        Collaborator = p.SellerAttendeeCollaborator.Collaborator,
                                    },
                                    MusicBusinessRoundProjectTargetAudienceDtos = p.MusicBusinessRoundProjectTargetAudiences
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
                                    MusicBusinessRoundProjectPlayerCategoryDtos = p.MusicBusinessRoundProjectPlayerCategories
                                        .Where(pc => !pc.IsDeleted)
                                        .Select(pc => new MusicBusinessRoundProjectPlayerCategoryDto
                                        {
                                            MusicBusinessRoundProjectPlayerCategory = pc,
                                            PlayerCategory = pc.PlayerCategory
                                        }),
                                    MusicBusinessRoundProjectActivityDtos = p.MusicBusinessRoundProjectActivities
                                        .Where(pa => !pa.IsDeleted)
                                        .Select(pa => new MusicBusinessRoundProjectActivityDto
                                        {
                                            MusicBusinessRoundProjectActivity = pa,
                                            Activity = pa.Activity
                                        }),
                                    MusicBusinessRoundProjectBuyerEvaluationDtos = p.MusicBusinessRoundProjectBuyerEvaluations
                                        .Where(pbe => !pbe.IsDeleted
                                                        && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                        && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                            .Any(aoc => !aoc.IsDeleted
                                                                                                        && aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid))
                                        .Select(pbe => new MusicBusinessRoundProjectBuyerEvaluationDto
                                        {
                                            MusicBusinessRoundProjectBuyerEvaluation = pbe,
                                            BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                            {
                                                AttendeeOrganization = pbe.BuyerAttendeeOrganization,
                                                Organization = pbe.BuyerAttendeeOrganization.Organization,
                                                Edition = pbe.BuyerAttendeeOrganization.Edition
                                            },
                                            ProjectEvaluationStatus = pbe.ProjectEvaluationStatus,
                                            ProjectEvaluationRefuseReason = pbe.ProjectEvaluationRefuseReason
                                        }),
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site interest dto by project uid asynchronous.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundProjectDto> FindSiteInterestWidgetDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid);

            return await query
                            .Select(p => new MusicBusinessRoundProjectDto
                            {
                                Uid = p.Uid,
                                SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                {
                                    AttendeeCollaborator = p.SellerAttendeeCollaborator,
                                    Collaborator = p.SellerAttendeeCollaborator.Collaborator,
                                    //Edition = p.SellerAttendeeOrganization.Edition
                                },
                                MusicBusinessRoundProjectInterestDtos = p.MusicBusinessRoundProjectInterests
                                        .Where(pi => !pi.IsDeleted)
                                        .Select(pi => new MusicBusinessRoundProjectInterestDto
                                        {
                                            MusicBusinessRoundProjectInterest = pi,
                                            Interest = pi.Interest,
                                            InterestGroup = pi.Interest.InterestGroup
                                        }),
                                MusicBusinessRoundProjectTargetAudienceDtos = p.MusicBusinessRoundProjectTargetAudiences
                                    .Where(ta => !ta.IsDeleted)
                                    .Select(ta => new MusicBusinessRoundProjectTargetAudienceDto
                                    {
                                        MusicBusinessRoundProjectTargetAudience = ta,
                                        TargetAudience = ta.TargetAudience
                                    }),
                                MusicBusinessRoundProjectBuyerEvaluationDtos = p.MusicBusinessRoundProjectBuyerEvaluations
                                        .Where(pbe => !pbe.IsDeleted
                                                        && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                        && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                            .Any(aoc => !aoc.IsDeleted
                                                                                                        /*&&Checar com o Renan... aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid*/))
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
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site buyer company dto by project uid asynchronous.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundProjectDto> FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                 .FindByUid(projectUid);

            return await query
                            .Select(p => new MusicBusinessRoundProjectDto
                            {
                                Uid = p.Uid,
                                SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                {
                                    AttendeeCollaborator = p.SellerAttendeeCollaborator,
                                    Collaborator = p.SellerAttendeeCollaborator.Collaborator,
                                    //Edition = p.SellerAttendeeOrganization.Edition
                                },
                                MusicBusinessRoundProjectBuyerEvaluationDtos = p.MusicBusinessRoundProjectBuyerEvaluations
                                        .Where(pbe => !pbe.IsDeleted
                                                        && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                        && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                            .Any(aoc => !aoc.IsDeleted
                                                                                                        /*&&Checar com o Renan... aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid*/))
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
                            })
                            .FirstOrDefaultAsync();
        }
    }
}
