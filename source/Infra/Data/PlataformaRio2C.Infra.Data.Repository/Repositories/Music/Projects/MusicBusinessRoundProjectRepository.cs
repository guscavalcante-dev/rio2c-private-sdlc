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
            query = query.Where(p => p.SellerAttendeeCollaborator.Uid == sellerAttendeeOrganizationUid);

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
                                Uid = m.Uid,
                                SellerAttendeeCollaboratorId = m.SellerAttendeeCollaboratorId,
                                SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                {
                                    //AttendeeCollaborator = m.SellerAttendeeCollaborator,
                                    //Organization = m.SellerAttendeeCollaborator.Organization,
                                    //Edition = m.SellerAttendeeCollaborator.Edition
                                    AttendeeCollaborator = m.SellerAttendeeCollaborator,
                                    Collaborator = m.SellerAttendeeCollaborator.Collaborator,
                                    JobTitlesDtos = m.SellerAttendeeCollaborator.Collaborator.JobTitles.Where(jb => !jb.IsDeleted).Select(jb => new CollaboratorJobTitleBaseDto
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
                                PlayerCategoriesThatHaveOrHadContract = m.PlayerCategoriesThatHaveOrHadContract,
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
                                    .Where(pi => !pi.IsDeleted)
                                    .Select(pi => new MusicBusinessRoundProjectInterestDto
                                    {
                                        MusicBusinessRoundProjectInterest = pi,
                                        Interest = pi.Interest
                                    }),
                                PlayerCategoriesDtos = m.PlayerCategories
                                    .Where(pc => !pc.IsDeleted)
                                    .Select(pc => new MusicBusinessRoundProjectPlayerCategoryDto
                                    {
                                        MusicBusinessRoundProjectPlayerCategory = pc,
                                        PlayerCategory = pc.PlayerCategory
                                    }),
                                MusicBusinessRoundProjectExpectationsForMeetingDtos = m.MusicBusinessRoundProjectExpectationsForMeetings
                                    .Where(pe => !pe.IsDeleted)
                                    .Select(pe => new MusicBusinessRoundProjectExpectationsForMeetingDto
                                    {
                                        Value = pe.Value,
                                        Language = pe.Language
                                    }),
                                MusicBusinessRoundProjectBuyerEvaluationDtos = m.MusicBusinessRoundProjectBuyerEvaluations
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
    }
}
