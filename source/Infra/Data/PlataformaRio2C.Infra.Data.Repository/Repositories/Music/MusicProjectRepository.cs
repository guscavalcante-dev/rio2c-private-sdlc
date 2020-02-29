// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-29-2020
// ***********************************************************************
// <copyright file="MusicProjectRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;
using LinqKit;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region MusicProject IQueryable Extensions

    /// <summary>
    /// MusicProjectIQueryableExtensions
    /// </summary>
    internal static class MusicProjectIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByUid(this IQueryable<MusicProject> query, Guid musicProjectUid)
        {
            query = query.Where(mp => mp.Uid == musicProjectUid);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByEditionId(this IQueryable<MusicProject> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(mp => (showAllEditions || mp.AttendeeMusicBand.EditionId == editionId));

            return query;
        }

        /// <summary>Finds the by music genre uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByMusicGenreUid(this IQueryable<MusicProject> query, Guid? musicGenreUid)
        {
            if (musicGenreUid.HasValue)
            {
                query = query.Where(mp => mp.AttendeeMusicBand.MusicBand.MusicBandGenres.Any(mbg => !mbg.IsDeleted 
                                                                                                    && !mbg.MusicGenre.IsDeleted 
                                                                                                    && mbg.MusicGenre.Uid == musicGenreUid));
            }

            return query;
        }

        /// <summary>Finds the by attendee music band uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeMusicBandUid">The attendee music band uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByAttendeeMusicBandUid(this IQueryable<MusicProject> query, Guid attendeeMusicBandUid)
        {
            query = query.Where(mp => mp.AttendeeMusicBand.Uid == attendeeMusicBandUid);

            return query;
        }

        /// <summary>Finds the by attendee music bands uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeMusicBandsUids">The attendee music bands uids.</param>
        /// <param name="showAll">if set to <c>true</c> [show all].</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByAttendeeMusicBandsUids(this IQueryable<MusicProject> query, List<Guid> attendeeMusicBandsUids, bool showAll)
        {
            if (!showAll && attendeeMusicBandsUids?.Any() == true)
            {
                query = query.Where(mp => attendeeMusicBandsUids.Contains(mp.AttendeeMusicBand.Uid));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByKeywords(this IQueryable<MusicProject> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<MusicProject>(false);
                var innerMusicBandNameWhere = PredicateBuilder.New<MusicProject>(true);
                //var innerProjectSummaryNameWhere = PredicateBuilder.New<MusicProject>(true);
                //var innerProjectInterestNameWhere = PredicateBuilder.New<MusicProject>(true);
                //var innerSellerAttendeeOrganizationNameWhere = PredicateBuilder.New<MusicProject>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerMusicBandNameWhere = innerMusicBandNameWhere.Or(mp => mp.AttendeeMusicBand.MusicBand.Name.Contains(keyword)
                                                                                        && !mp.AttendeeMusicBand.IsDeleted
                                                                                        && !mp.AttendeeMusicBand.MusicBand.IsDeleted);
                        //innerProjectSummaryNameWhere = innerProjectSummaryNameWhere.Or(p => p.ProjectSummaries.Any(pt => !pt.IsDeleted && pt.Value.Contains(keyword)));
                        //innerProjectInterestNameWhere = innerProjectInterestNameWhere.Or(p => p.ProjectInterests.Any(pi => !pi.IsDeleted && pi.Interest.Name.Contains(keyword)));
                        //innerSellerAttendeeOrganizationNameWhere = innerSellerAttendeeOrganizationNameWhere.Or(sao => sao.SellerAttendeeOrganization.Organization.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerMusicBandNameWhere);
                //outerWhere = outerWhere.Or(innerProjectSummaryNameWhere);
                //outerWhere = outerWhere.Or(innerProjectInterestNameWhere);
                //outerWhere = outerWhere.Or(innerSellerAttendeeOrganizationNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        ///// <summary>Finds the by interest uid.</summary>
        ///// <param name="query">The query.</param>
        ///// <param name="interestUid">The interest uid.</param>
        ///// <returns></returns>
        //internal static IQueryable<Project> FindByInterestUid(this IQueryable<Project> query, Guid? interestUid)
        //{
        //    if (interestUid != null)
        //    {
        //        query = query.Where(p => p.ProjectInterests.Any(pi => !pi.IsDeleted
        //                                                              && !pi.Interest.IsDeleted
        //                                                              && pi.Interest.Uid == interestUid));
        //    }

        //    return query;
        //}


        ///// <summary>Finds by interest uids.</summary>
        ///// <param name="query">The query.</param>
        ///// <param name="interestUids">The interest uid.</param>
        ///// <returns></returns>
        //internal static IQueryable<Project> FindByInterestUids(this IQueryable<Project> query, List<Guid> interestUids)
        //{
        //    if (interestUids?.Any() == true)
        //    {
        //        query = query.Where(p => p.ProjectInterests.Any(pi => !pi.IsDeleted
        //                                                              && !pi.Interest.IsDeleted
        //                                                              && interestUids.Contains(pi.Interest.Uid)));
        //    }

        //    return query;
        //}

        ///// <summary>Finds by target audience uid.</summary>
        ///// <param name="query">The query.</param>
        ///// <param name="targetAudienceUid">The target audience uid.</param>
        ///// <returns></returns>
        //internal static IQueryable<Project> FindByTargetAudienceUid(this IQueryable<Project> query, Guid? targetAudienceUid)
        //{
        //    if (targetAudienceUid != null)
        //    {
        //        query = query.Where(p => p.ProjectTargetAudiences.Any(pi => !pi.IsDeleted
        //                                                              && !pi.TargetAudience.IsDeleted
        //                                                              && pi.TargetAudience.Uid == targetAudienceUid));
        //    }

        //    return query;
        //}

        ///// <summary>Finds by target audience uid.</summary>
        ///// <param name="query">The query.</param>
        ///// <param name="targetAudienceUid">The target audience uid.</param>
        ///// <returns></returns>
        //internal static IQueryable<Project> FindByTargetAudienceUids(this IQueryable<Project> query, List<Guid> targetAudienceUids)
        //{
        //    if (targetAudienceUids?.Any() == true)
        //    {
        //        query = query.Where(p => p.ProjectTargetAudiences.Any(pi => !pi.IsDeleted
        //                                                              && !pi.TargetAudience.IsDeleted
        //                                                              && targetAudienceUids.Contains(pi.TargetAudience.Uid)));
        //    }

        //    return query;
        //}

        ///// <summary>Finds by the start and end date</summary>
        ///// <param name="query"></param>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //internal static IQueryable<Project> FindByDate(this IQueryable<Project> query, DateTime? startDate, DateTime? endDate)
        //{
        //    if (startDate != null)
        //    {
        //        query = query.Where(p => p.CreateDate >= startDate || p.FinishDate >= startDate);
        //    }
        //    if (endDate != null)
        //    {
        //        var maxEndDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59);
        //        query = query.Where(p => p.CreateDate <= maxEndDate || p.FinishDate <= maxEndDate);
        //    }
        //    return query;
        //}

        /// <summary>Finds the by project evaluation status.</summary>
        /// <param name="query">The query.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByProjectEvaluationStatus(this IQueryable<MusicProject> query, Guid? evaluationStatusUid)
        {
            if (evaluationStatusUid != null)
            {
                query = query.Where(mp => mp.ProjectEvaluationStatus.Uid == evaluationStatusUid);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> IsNotDeleted(this IQueryable<MusicProject> query)
        {
            query = query.Where(p => !p.IsDeleted);

            return query;
        }
    }

    #endregion

    #region MusicProjectDto IQueryable Extensions

    /// <summary>
    /// MusicProjectDtoIQueryableExtensions
    /// </summary>
    internal static class MusicProjectDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicProjectDto>> ToListPagedAsync(this IQueryable<MusicProjectDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>MusicProjectRepository</summary>
    public class MusicProjectRepository : Repository<Context.PlataformaRio2CContext, MusicProject>, IMusicProjectRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MusicProjectRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MusicProjectRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicProject> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all dtos to evaluate asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<MusicProjectDto>> FindAllDtosToEvaluateAsync(int editionId, string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, int page, int pageSize)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByKeywords(searchKeywords)
                                .FindByMusicGenreUid(musicGenreUid)
                                .FindByProjectEvaluationStatus(evaluationStatusUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new AttendeeMusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandType = mp.AttendeeMusicBand.MusicBand.MusicBandType,
                                        MusicBandGenreDtos = mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                                    .Where(mbg => !mbg.IsDeleted && !mbg.MusicGenre.IsDeleted)
                                                                    .OrderBy(mbg => mbg.MusicGenre.DisplayOrder)
                                                                    .Select(mbg => new MusicBandGenreDto
                                                                    {
                                                                        MusicBandGenre = mbg,
                                                                        MusicGenre = mbg.MusicGenre
                                                                    }),
                                        MusicBandTargetAudienceDtos = mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                            .Where(mbta => !mbta.IsDeleted && !mbta.TargetAudience.IsDeleted)
                                                                            .OrderBy(mbta => mbta.TargetAudience.DisplayOrder)
                                                                            .Select(mbta => new MusicBandTargetAudienceDto
                                                                            {
                                                                                MusicBandTargetAudience = mbta,
                                                                                TargetAudience = mbta.TargetAudience
                                                                            })
                                    },
                                    MusicProjectEvaluationDto = new MusicProjectEvaluationDto
                                    {
                                        EvaluationCollaborator = mp.EvaluationUser.Collaborator,
                                        ProjectEvaluationStatus = mp.ProjectEvaluationStatus,
                                        ProjectEvaluationRefuseReason = mp.ProjectEvaluationRefuseReason,
                                        Reason = mp.Reason,
                                        EvaluationDate = mp.EvaluationDate
                                    }
                                });

            return await query
                            .OrderBy(mpd => mpd.MusicProject.CreateDate)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Finds the dto to evaluate asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindDtoToEvaluateAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new AttendeeMusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandType = mp.AttendeeMusicBand.MusicBand.MusicBandType,
                                        MusicBandGenreDtos = mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                                    .Where(mbg => !mbg.IsDeleted && !mbg.MusicGenre.IsDeleted)
                                                                    .OrderBy(mbg => mbg.MusicGenre.DisplayOrder)
                                                                    .Select(mbg => new MusicBandGenreDto
                                                                    {
                                                                        MusicBandGenre = mbg,
                                                                        MusicGenre = mbg.MusicGenre
                                                                    }),
                                        MusicBandTargetAudienceDtos = mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                            .Where(mbta => !mbta.IsDeleted && !mbta.TargetAudience.IsDeleted)
                                                                            .OrderBy(mbta => mbta.TargetAudience.DisplayOrder)
                                                                            .Select(mbta => new MusicBandTargetAudienceDto
                                                                            {
                                                                                MusicBandTargetAudience = mbta,
                                                                                TargetAudience = mbta.TargetAudience
                                                                            })
                                    },
                                    MusicProjectEvaluationDto = new MusicProjectEvaluationDto
                                    {
                                        EvaluationCollaborator = mp.EvaluationUser.Collaborator,
                                        ProjectEvaluationStatus = mp.ProjectEvaluationStatus,
                                        ProjectEvaluationRefuseReason = mp.ProjectEvaluationRefuseReason,
                                        Reason = mp.Reason,
                                        EvaluationDate = mp.EvaluationDate
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the main information widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindMainInformationWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new AttendeeMusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandType = mp.AttendeeMusicBand.MusicBand.MusicBandType,
                                        MusicBandGenreDtos = mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                                    .Where(mbg => !mbg.IsDeleted && !mbg.MusicGenre.IsDeleted)
                                                                    .OrderBy(mbg => mbg.MusicGenre.DisplayOrder)
                                                                    .Select(mbg => new MusicBandGenreDto
                                                                    {
                                                                        MusicBandGenre = mbg,
                                                                        MusicGenre = mbg.MusicGenre
                                                                    }),
                                        MusicBandTargetAudienceDtos = mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                            .Where(mbta => !mbta.IsDeleted && !mbta.TargetAudience.IsDeleted)
                                                                            .OrderBy(mbta => mbta.TargetAudience.DisplayOrder)
                                                                            .Select(mbta => new MusicBandTargetAudienceDto
                                                                            {
                                                                                MusicBandTargetAudience = mbta,
                                                                                TargetAudience = mbta.TargetAudience
                                                                            })
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the members widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindMembersWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new AttendeeMusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandMembers = mp.AttendeeMusicBand.MusicBand.MusicBandMembers.Where(mbm => !mbm.IsDeleted)
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the team members widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindTeamMembersWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new AttendeeMusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandTeamMembers = mp.AttendeeMusicBand.MusicBand.MusicBandTeamMembers.Where(mbm => !mbm.IsDeleted)
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the released music projects widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindReleasedProjectsWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new AttendeeMusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        ReleasedMusicProjects = mp.AttendeeMusicBand.MusicBand.ReleasedMusicProjects.Where(rmp => !rmp.IsDeleted)
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the project responsible widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindProjectResponsibleWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new AttendeeMusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        AttendeeMusicBandCollaboratorDto = mp.AttendeeMusicBand.AttendeeMusicBandCollaborators
                                                                                .Where(amb => !amb.IsDeleted && !amb.AttendeeCollaborator.IsDeleted && !amb.AttendeeCollaborator.Collaborator.IsDeleted)
                                                                                .Select(amb => new AttendeeMusicBandCollaboratorDto
                                                                                {
                                                                                    AttendeeMusicBandCollaborator = amb,
                                                                                    AttendeeCollaborator = amb.AttendeeCollaborator,
                                                                                    Collaborator = amb.AttendeeCollaborator.Collaborator,
                                                                                    User = amb.AttendeeCollaborator.Collaborator.User
                                                                                })
                                                                                .FirstOrDefault()
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the clippings widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindClippingWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp
                                });

            return await query
                            .FirstOrDefaultAsync();
        }
    }
}