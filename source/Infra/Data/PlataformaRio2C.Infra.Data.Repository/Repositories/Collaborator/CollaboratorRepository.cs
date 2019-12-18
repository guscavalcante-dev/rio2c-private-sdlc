// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-18-2019
// ***********************************************************************
// <copyright file="CollaboratorRepository.cs" company="Softo">
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
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Collaborator IQueryable Extensions

    /// <summary>
    /// CollaboratorIQueryableExtensions
    /// </summary>
    internal static class CollaboratorIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByUid(this IQueryable<Collaborator> query, Guid collaboratorUid)
        {
            query = query.Where(c => c.Uid == collaboratorUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByUids(this IQueryable<Collaborator> query, List<Guid> collaboratorsUids)
        {
            if (collaboratorsUids?.Any() == true)
            {
                query = query.Where(c => collaboratorsUids.Contains(c.Uid));
            }

            return query;
        }

        /// <summary>Finds the by sales platform attendee identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindBySalesPlatformAttendeeId(this IQueryable<Collaborator> query, string salesPlatformAttendeeId)
        {
            query = query.Where(c => c.AttendeeCollaborators.Any(ac => ac.AttendeeCollaboratorTickets.Any(act => !act.IsDeleted 
                                                                                                                 && act.SalesPlatformAttendeeId == salesPlatformAttendeeId)));

            return query;
        }

        /// <summary>Finds the by collaborator type name and by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByCollaboratorTypeNameAndByEditionId(this IQueryable<Collaborator> query, string collaboratorTypeName, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, int? editionId)
        {
            query = query.Where(c => c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
                                                                       && !ac.IsDeleted
                                                                       && !ac.Edition.IsDeleted
                                                                       && (showAllParticipants
                                                                           || (showAllExecutives && ac.AttendeeOrganizationCollaborators
                                                                                                            .Any(aoc => !aoc.IsDeleted))
                                                                           || (!showAllExecutives && ac.AttendeeCollaboratorTypes
                                                                                                            .Any(act => !act.IsDeleted
                                                                                                                        && !act.CollaboratorType.IsDeleted
                                                                                                                        && act.CollaboratorType.Name == collaboratorTypeName)))));

            return query;
        }

        /// <summary>Finds the by organization type uid and by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByOrganizationTypeUidAndByEditionId(this IQueryable<Collaborator> query, Guid organizationTypeUid, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, int? editionId)
        {
            query = query.Where(c => c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
                                                                       && !ac.IsDeleted
                                                                       && !ac.Edition.IsDeleted
                                                                       && (showAllParticipants
                                                                           || ac.AttendeeOrganizationCollaborators
                                                                                   .Any(aoc => !aoc.IsDeleted
                                                                                               && !aoc.AttendeeOrganization.IsDeleted
                                                                                               && aoc.AttendeeOrganization.AttendeeOrganizationTypes
                                                                                                       .Any(aot => !aot.IsDeleted
                                                                                                                   && (showAllExecutives || aot.OrganizationType.Uid == organizationTypeUid))))));

            return query;
        }

        /// <summary>Finds the by highlights.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByHighlights(this IQueryable<Collaborator> query, string collaboratorTypeName, bool? showHighlights)
        {
            if (showHighlights.HasValue && showHighlights.Value)
            {
                query = query.Where(o => o.AttendeeCollaborators.Any(ac => !ac.IsDeleted
                                                                           && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                      && act.CollaboratorType.Name == collaboratorTypeName
                                                                                                                      && act.IsApiDisplayEnabled
                                                                                                                      && act.ApiHighlightPosition.HasValue)));
            }

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByEditionId(this IQueryable<Collaborator> query, bool showAllEditions, int? editionId)
        {
            if (!showAllEditions && editionId.HasValue)
            {
                query = query.Where(o => o.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
                                                                           && !ac.IsDeleted
                                                                           && !ac.Edition.IsDeleted));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByKeywords(this IQueryable<Collaborator> query, string keywords, int? editionId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Collaborator>(false);
                var innerExecutiveNameWhere = PredicateBuilder.New<Collaborator>(true);
                var innerExecutiveEmailWhere = PredicateBuilder.New<Collaborator>(true);
                var innerOrganizationNameWhere = PredicateBuilder.New<Collaborator>(true);
                var innerHoldingNameWhere = PredicateBuilder.New<Collaborator>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerExecutiveNameWhere = innerExecutiveNameWhere.And(c => (c.User.Name).Contains(keyword));
                        innerExecutiveEmailWhere = innerExecutiveEmailWhere.And(c => (c.User.Email).Contains(keyword));
                        innerOrganizationNameWhere = innerOrganizationNameWhere
                                                        .And(c => c.AttendeeCollaborators.Any(ac => (!editionId.HasValue || ac.EditionId == editionId)
                                                                                                    && !ac.IsDeleted
                                                                                                    && !ac.Edition.IsDeleted
                                                                                                    && ac.AttendeeOrganizationCollaborators
                                                                                                            .Any(aoc => !aoc.IsDeleted
                                                                                                                        && !aoc.AttendeeOrganization.IsDeleted
                                                                                                                        && !aoc.AttendeeOrganization.Organization.IsDeleted
                                                                                                                        && aoc.AttendeeOrganization.Organization.Name.Contains(keyword))));
                        innerHoldingNameWhere = innerHoldingNameWhere
                                                        .And(c => c.AttendeeCollaborators.Any(ac => (!editionId.HasValue || ac.EditionId == editionId)
                                                                                                    && !ac.IsDeleted
                                                                                                    && !ac.Edition.IsDeleted
                                                                                                    && ac.AttendeeOrganizationCollaborators
                                                                                                        .Any(aoc => !aoc.IsDeleted
                                                                                                                    && !aoc.AttendeeOrganization.IsDeleted
                                                                                                                    && !aoc.AttendeeOrganization.Organization.IsDeleted
                                                                                                                    && !aoc.AttendeeOrganization.Organization.Holding.IsDeleted
                                                                                                                    && aoc.AttendeeOrganization.Organization.Holding.Name.Contains(keyword))));

                    }
                }

                outerWhere = outerWhere.Or(innerExecutiveNameWhere);
                outerWhere = outerWhere.Or(innerExecutiveEmailWhere);
                outerWhere = outerWhere.Or(innerOrganizationNameWhere);
                outerWhere = outerWhere.Or(innerHoldingNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> IsNotDeleted(this IQueryable<Collaborator> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }
    }

    #endregion

    #region CollaboratorBaseDto IQueryable Extensions

    /// <summary>
    /// CollaboratorBaseDtoIQueryableExtensions
    /// </summary>
    internal static class CollaboratorBaseDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<CollaboratorBaseDto>> ToListPagedAsync(this IQueryable<CollaboratorBaseDto> query, int page, int pageSize)
        {
            page++;

            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>CollaboratorRepository</summary>
    public class CollaboratorRepository : Repository<PlataformaRio2CContext, Collaborator>, ICollaboratorRepository
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public CollaboratorRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Collaborator> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all collaborators by collaborators uids.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <returns></returns>
        public async Task<List<AdminAccessControlDto>> FindAllCollaboratorsByCollaboratorsUids(int editionId, List<Guid> collaboratorsUids)
        {
            var query = this.GetBaseQuery()
                                .FindByUids(collaboratorsUids);

            return await query
                            .Select(c => new AdminAccessControlDto
                            {
                                User = c.User,
                                Roles = c.User.Roles,
                                Language = c.User.UserInterfaceLanguage,
                                Collaborator = c,
                                EditionCollaboratorTypes = c.AttendeeCollaborators
                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                .SelectMany(ac => ac.AttendeeCollaboratorTypes
                                                                                        .Where(act => !act.IsDeleted && !act.CollaboratorType.IsDeleted)
                                                                                        .Select(act => act.CollaboratorType)),
                            })
                            .ToListAsync();
        }


        /// <summary>Finds the dto by uid and by edition identifier asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<CollaboratorDto> FindDtoByUidAndByEditionIdAsync(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(collaboratorUid);

            return await query
                            .Select(c => new CollaboratorDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                Badge = c.Badge,
                                Email = c.User.Email,
                                PhoneNumber = c.PhoneNumber,
                                CellPhone = c.CellPhone,
                                PublicEmail = c.PublicEmail,
                                //HoldingBaseDto = new HoldingBaseDto
                                //{
                                //    Id = c.Holding.Id,
                                //    Uid = c.Holding.Uid,
                                //    Name = c.Holding.Name
                                //},
                                ImageUploadDate = c.ImageUploadDate,
                                CreateDate = c.CreateDate,
                                CreateUserId = c.CreateUserId,
                                UpdateDate = c.UpdateDate,
                                UpdateUserId = c.UpdateUserId,
                                //Creator = h.Creator,
                                //HoldingBaseDto = new HoldingBaseDto
                                //{
                                //    Id = c.Holding.Id,
                                //    Uid = c.Holding.Uid,
                                //    Name = c.Holding.Name
                                //},
                                EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators.Where(ac => !ac.IsDeleted && ac.EditionId == editionId).Select(ac => new AttendeeCollaboratorBaseDto
                                {
                                    Id = ac.Id,
                                    Uid = ac.Uid,
                                    WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                    OnboardingStartDate = ac.OnboardingStartDate,
                                    OnboardingFinishDate = ac.OnboardingFinishDate,
                                    OnboardingUserDate = ac.OnboardingUserDate,
                                    OnboardingCollaboratorDate = ac.OnboardingCollaboratorDate,
                                    PlayerTermsAcceptanceDate = ac.PlayerTermsAcceptanceDate,
                                    ProducerTermsAcceptanceDate = ac.ProducerTermsAcceptanceDate
                                }).FirstOrDefault(),
                                UpdaterDto = new UserBaseDto
                                {
                                    Uid = c.Updater.Uid,
                                    Name = c.Updater.Name,
                                    Email = c.Updater.Email
                                },
                                JobTitlesDtos = c.JobTitles.Select(d => new CollaboratorJobTitleBaseDto
                                {
                                    Id = d.Id,
                                    Uid = d.Uid,
                                    Value = d.Value,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = d.Language.Id,
                                        Uid = d.Language.Uid,
                                        Name = d.Language.Name,
                                        Code = d.Language.Code
                                    }
                                }),
                                MiniBiosDtos = c.MiniBios.Select(d => new CollaboratorMiniBioBaseDto
                                {
                                    Id = d.Id,
                                    Uid = d.Uid,
                                    Value = d.Value,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = d.Language.Id,
                                        Uid = d.Language.Uid,
                                        Name = d.Language.Name,
                                        Code = d.Language.Code
                                    }
                                }),
                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                    .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                    .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                            .Where(aoc => !aoc.IsDeleted)
                                                                                            .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                            {
                                                                                                Uid = aoc.AttendeeOrganization.Uid,
                                                                                                OrganizationBaseDto = new OrganizationBaseDto
                                                                                                {
                                                                                                    Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                    HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null :new HoldingBaseDto
                                                                                                    {
                                                                                                        Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                    }
                                                                                                }
                                                                                            }))
                            }).FirstOrDefaultAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorBaseDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns, 
            List<Guid> collaboratorsUids,
            string collaboratorTypeName,
            bool showAllEditions,
            bool showAllExecutives,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeName, showAllEditions, showAllExecutives, showAllParticipants, editionId)
                                .FindByUids(collaboratorsUids)
                                .FindByHighlights(collaboratorTypeName, showHighlights);

            return await query
                            .DynamicOrder<Collaborator>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("FullName", "User.Name"),
                                    new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                "User.Name")
                            .Select(c => new CollaboratorBaseDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                Badge = c.Badge,
                                Email = c.User.Email,
                                PhoneNumber = c.PhoneNumber,
                                CellPhone = c.CellPhone,
                                PublicEmail = c.PublicEmail,
                                ImageUploadDate = c.ImageUploadDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                                IsInCurrentEdition = editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
                                                                                                             && !ac.Edition.IsDeleted
                                                                                                             && !ac.IsDeleted
                                                                                                             && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                                        && act.CollaboratorType.Name == collaboratorTypeName)),
                                IsInOtherEdition = editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId != editionId
                                                                                                           && !ac.IsDeleted),
                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                    .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                    .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                            .Where(aoc => !aoc.IsDeleted)
                                                                                            .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                            {
                                                                                                Uid = aoc.AttendeeOrganization.Uid,
                                                                                                OrganizationBaseDto = new OrganizationBaseDto
                                                                                                {
                                                                                                    Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                    HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                    {
                                                                                                        Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                    }
                                                                                                }
                                                                                            }))
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts all by data table.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(string collaboratorTypeName, bool showAllEditions, int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeName, showAllEditions, false, false, editionId);

            return await query.CountAsync();
        }

        /// <summary>Finds the by sales platform attendee identifier asynchronous.</summary>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <returns></returns>
        public async Task<Collaborator> FindBySalesPlatformAttendeeIdAsync(string salesPlatformAttendeeId)
        {
            var query = this.GetBaseQuery()
                                .FindBySalesPlatformAttendeeId(salesPlatformAttendeeId);

            return await query.FirstOrDefaultAsync();
        }

        #region Api

        public async Task<List<Collaborator>> FindAllOrganizationsByHightlightPosition(int editionId, Guid collaboratorTypeUid, int apiHighlightPosition, Guid? organizationUid)
        {
            var query = this.GetBaseQuery()
                                .Where(o => o.Uid != organizationUid
                                            && o.AttendeeCollaborators.Any(ac => !ac.IsDeleted
                                                                                 && ac.EditionId == editionId
                                                                                 && ac.AttendeeCollaboratorTypes.Any(aot => !aot.IsDeleted
                                                                                                                            && aot.CollaboratorType.Uid == collaboratorTypeUid
                                                                                                                            && aot.ApiHighlightPosition == apiHighlightPosition)));

            return await query
                            .ToListAsync();
        }

        #endregion

        #region Old

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<Collaborator> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Address)
                                .Include(i => i.User);
                                //.Include(i => i.MiniBios)
                                //.Include(i => i.MiniBios.Select(j => j.Language))
                                //.Include(i => i.JobTitles)
                                //.Include(i => i.JobTitles.Select(j => j.Language))
                                //.Include(i => i.Players)
                                //.Include(i => i.Players.Select(e => e.Holding))
                                //.Include(i => i.Players.Select(e => e.Address))
                                //.Include(i => i.ProducersEvents)
                                ////.Include(i => i.Countries)
                                ////.Include(i => i.Country)
                                //.Include(i => i.ProducersEvents.Select(e => e.Producer))
                                //.Include(i => i.ProducersEvents.Select(e => e.Producer.Address));

            return @readonly
                          ? consult.AsNoTracking()
                          : consult;
        }

        //public override IQueryable<Collaborator> GetAll(Expression<Func<Collaborator, bool>> filter)
        //{
        //    return this.GetAll().Where(filter);
        //}


        //public override Collaborator Get(Expression<Func<Collaborator, bool>> filter)
        //{
        //    return this.GetAll().FirstOrDefault(filter);
        //}

        //public override Collaborator Get(Guid uid)
        //{
        //    return this.GetAll().FirstOrDefault(e => e.Uid == uid);
        //}

        /// <summary>Método que remove a entidade do Contexto</summary>
        /// <param name="entity">Entidade</param>
        public override void Delete(Collaborator entity)
        {
            var UserRepository = new UserRepository(_context);

            //entity.Players.Clear();

            //if (entity.Image != null)
            //{
            //    _context.Entry(entity.Image).State = EntityState.Deleted;
            //}

            //if (entity.User != null)
            //{
            //    UserRepository.Delete(entity.User);
            //}

            //if (entity.Address != null)
            //{
            //    _context.Entry(entity.Address).State = EntityState.Deleted;
            //}

            //if (entity.JobTitles != null && entity.JobTitles.Any())
            //{
            //    var items = entity.JobTitles.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}

            //if (entity.MiniBios != null && entity.MiniBios.Any())
            //{
            //    var items = entity.MiniBios.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}

            //if (entity.ProducersEvents != null && entity.ProducersEvents.Any())
            //{
            //    entity.ProducersEvents.Clear();
            //}

            base.Delete(entity);
        }

        /// <summary>Gets the status register collaborator by user identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Collaborator GetStatusRegisterCollaboratorByUserId(int id)
        {
            return this.dbSet
                                .Include(i => i.Address)
                                //.Include(i => i.Players)
                                //.Include(i => i.Players.Select(j => j.Address))
                                //.Include(i => i.Players.Select(j => j.Interests))
                                //.Include(i => i.ProducersEvents)
                                //.Include(i => i.Countries)
                                //.Include(i => i.Country)
                                //.Include(i => i.ProducersEvents.Select(pe => pe.Producer))
                                //.Include(i => i.ProducersEvents.Select(pe => pe.Producer.Address))
                                .FirstOrDefault(e => e.Id == id);
        }

        /// <summary>Gets the with producer by user identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Collaborator GetWithProducerByUserId(int id)
        {
            return this.dbSet
                //.Include(i => i.ProducersEvents)
                //.Include(i => i.ProducersEvents.Select(e => e.Edition))
                //.Include(i => i.ProducersEvents.Select(e => e.Producer))
                //.Include(i => i.Countries)
                //.Include(i => i.Country)
                .FirstOrDefault(e => e.Id == id);
        }

        /// <summary>Gets the image.</summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public Collaborator GetImage(Guid uid)
        {
            return this.dbSet
                              //.Include(i => i.Image)
                              .FirstOrDefault(e => e.Uid == uid);
        }

        /// <summary>Gets the with player and producer user identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Collaborator GetWithPlayerAndProducerUserId(int id)
        {
            return this.dbSet
                       //.Include(i => i.Players)
                       //.Include(i => i.ProducersEvents.Select(pe => pe.Producer))
                       .Include(i => i.Address)
                       .FirstOrDefault(e => e.Id == id);
        }

        /// <summary>Gets the with player and producer uid.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Collaborator GetWithPlayerAndProducerUid(Guid id)
        {
            return this.dbSet
                       //.Include(i => i.Players)
                       //.Include(i => i.Players.Select(e => e.Holding))
                       //.Include(i => i.ProducersEvents)
                       //.Include(i => i.ProducersEvents.Select(pe => pe.Producer))
                       .FirstOrDefault(e => e.Uid == id);
        }

        /// <summary>Gets the options.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetOptions(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                                //.Include(i => i.Players)
                                //.Include(i => i.Players.Select(e => e.Holding))
                                //.Include(i => i.ProducersEvents)
                                //.Include(i => i.ProducersEvents.Select(e => e.Producer))
                                //.Include(i => i.Countries)
                                //.Include(i => i.Country)
                                .AsNoTracking()
                                .Where(filter);

        }

        /// <summary>Gets the collaborator producer options.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetCollaboratorProducerOptions(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                             //.Include(i => i.ProducersEvents)
                             //.Include(i => i.ProducersEvents.Select(e => e.Producer))
                             //.Include(i => i.Countries)
                             //.Include(i => i.Country)
                             .AsNoTracking()
                             .Where(filter);
        }

        /// <summary>Gets the collaborator player options.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetCollaboratorPlayerOptions(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                             //.Include(i => i.Players)
                             //.Include(i => i.Players.Select(e => e.Holding))
                             //.Include(i => i.Countries)
                             //.Include(i => i.Country)
                             .AsNoTracking()
                             .Where(filter);
        }

        /// <summary>Gets the options chat.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetOptionsChat(int userId)
        {
            return this.dbSet
                            //.Include(i => i.User)
                            //.Include(i => i.Players)
                            //.Include(i => i.JobTitles)
                            //.Include(i => i.JobTitles.Select(e => e.Language))
                            //.Include(i => i.Players.Select(e => e.Holding))
                            //.Include(i => i.ProducersEvents)
                            //.Include(i => i.ProducersEvents.Select(e => e.Edition))
                            //.Include(i => i.ProducersEvents.Select(e => e.Producer))
                            .AsNoTracking()
                            .Where(e => e.Id != userId);
        }

        /// <summary>Gets the by schedule.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Collaborator GetBySchedule(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet
                                //.Include(i => i.ProducersEvents)
                                //.Include(i => i.ProducersEvents.Select(e => e.Edition))
                                //.Include(i => i.ProducersEvents.Select(e => e.Producer))
                                //.Include(i => i.ProducersEvents.Select(e => e.Producer.EventsCollaborators))
                                //.Include(i => i.ProducersEvents.Select(e => e.Producer.EventsCollaborators.Select(ev => ev.Collaborator)))
                                //.Include(i => i.Players)
                                //.Include(i => i.Players.Select(e => e.Collaborators))
                                .FirstOrDefault(filter);
        }

        public Collaborator GetById(int id)
        {
            return this.GetAll().FirstOrDefault(e => e.Id == id);
        }


        /// <summary>Gets all simple.</summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public override IQueryable<Collaborator> GetAllSimple(Expression<Func<Collaborator, bool>> filter)
        {
            return this.dbSet;
                            //.Include(i => i.User)
                            //.Include(i => i.Players)
                            //.Include(i => i.Countries)
                            //.Include(i => i.Country)
                            //.Include(i => i.Players.Select(e => e.Holding));
        }

        /// <summary>Gets all simple.</summary>
        /// <returns></returns>
        public override IQueryable<Collaborator> GetAllSimple()
        {
            return this.dbSet;
                            //.Include(i => i.User)
                            //.Include(i => i.Players)
                            //.Include(i => i.Countries)
                            //.Include(i => i.Country)
                            //.Include(i => i.Players.Select(e => e.Holding));
        }

        #endregion
    }
}