// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="NegotiationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Negotiation IQueryable Extensions

    /// <summary>
    /// NegotiationIQueryableExtensions
    /// </summary>
    internal static class NegotiationIQueryableExtensions
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationId">The negotiation identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindById(this IQueryable<Negotiation> query, int negotiationId)
        {
            query = query.Where(n => n.Id == negotiationId);

            return query;
        }

        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByUid(this IQueryable<Negotiation> query, Guid negotiationUid)
        {
            query = query.Where(n => n.Uid == negotiationUid);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByEditionId(this IQueryable<Negotiation> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(n => (showAllEditions || n.EditionId == editionId));

            return query;
        }

        /// <summary>Finds the by buyer attendee organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByBuyerOrganizationUid(this IQueryable<Negotiation> query, Guid? buyerOrganizationUid)
        {
            if (buyerOrganizationUid.HasValue)
            {
                query = query.Where(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization.Uid == buyerOrganizationUid);
            }

            return query;
        }

        /// <summary>Finds the by seller attendee organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindBySellerOrganizationUid(this IQueryable<Negotiation> query, Guid? sellerOrganizationUid)
        {
            if (sellerOrganizationUid.HasValue)
            {
                query = query.Where(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization.Uid == sellerOrganizationUid);
            }

            return query;
        }

        /// <summary>Finds the by attendee collaborator identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByAttendeeCollaboratorId(this IQueryable<Negotiation> query, int? attendeeCollaboratorId)
        {
            if (attendeeCollaboratorId.HasValue)
            {
                query = query
                            .Where(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaboratorId == attendeeCollaboratorId)
                                        || n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaboratorId == attendeeCollaboratorId));
            }

            return query;
        }

        /// <summary>
        /// Finds the by attendee collaborator uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByAttendeeCollaboratorUid(this IQueryable<Negotiation> query, Guid? attendeeCollaboratorUid)
        {
            if (attendeeCollaboratorUid.HasValue)
            {
                query = query
                            .Where(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid)
                                        || n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid));
            }

            return query;
        }

        /// <summary>Finds the by date range.</summary>
        /// <param name="query">The query.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByDateRange(this IQueryable<Negotiation> query, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            //endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            query = query.Where(n => (n.StartDate >= startDate && n.StartDate <= endDate)
                                     || (n.EndDate >= startDate && n.EndDate <= endDate));

            return query;
        }

        /// <summary>Finds the by project keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByProjectKeywords(this IQueryable<Negotiation> query, string projectKeywords)
        {
            if (!string.IsNullOrEmpty(projectKeywords))
            {
                var outerWhere = PredicateBuilder.New<Negotiation>(false);
                var innerProjectTitleWhere = PredicateBuilder.New<Negotiation>(true);

                foreach (var keyword in projectKeywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerProjectTitleWhere = innerProjectTitleWhere.Or(n => n.ProjectBuyerEvaluation.Project.ProjectTitles.Any(pt => !pt.IsDeleted && pt.Value.Contains(keyword)));
                    }
                }

                outerWhere = outerWhere.Or(innerProjectTitleWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Finds the by date.</summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationDate">The negotiation date.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByDate(this IQueryable<Negotiation> query, DateTime? negotiationDate)
        {
            if (negotiationDate.HasValue)
            {
                query = query.Where(n => DbFunctions.TruncateTime(n.StartDate) == DbFunctions.TruncateTime(negotiationDate));
            }

            return query;
        }

        /// <summary>Finds the by room uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByRoomUid(this IQueryable<Negotiation> query, Guid? roomUid)
        {
            if (roomUid.HasValue)
            {
                query = query.Where(n => n.Room.Uid == roomUid);
            }

            return query;
        }

        /// <summary>Finds the by room uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> FindByRoomId(this IQueryable<Negotiation> query, int? roomId, bool showAllRooms = false)
        {
            query = query.Where(n => showAllRooms || n.Room.Id == roomId);

            return query;
        }

        /// <summary>
        /// Determines whether this instance is manual.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> IsManual(this IQueryable<Negotiation> query)
        {
            query = query.Where(n => !n.IsAutomatic);

            return query;
        }

        /// <summary>
        /// Determines whether this instance is automatic.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> IsAutomatic(this IQueryable<Negotiation> query)
        {
            query = query.Where(n => n.IsAutomatic);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Negotiation> IsNotDeleted(this IQueryable<Negotiation> query)
        {
            query = query.Where(n => !n.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>NegotiationRepository</summary>
    public class NegotiationRepository : Repository<PlataformaRio2CContext, Negotiation>, INegotiationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="NegotiationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public NegotiationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Negotiation> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="negotiationId">The negotiation identifier.</param>
        /// <returns></returns>
        public async Task<Negotiation> FindByIdAsync(int negotiationId)
        {
            var query = this.GetBaseQuery()
                               .FindById(negotiationId);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        public async Task<Negotiation> FindByUidAsync(Guid negotiationUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUid(negotiationUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the dto asynchronous.
        /// </summary>
        /// <param name="negotiationUid">The edition uid.</param>
        /// <returns></returns>
        public async Task<NegotiationDto> FindDtoAsync(Guid negotiationUid)
        {
            var negotiationDto = await this.GetBaseQuery()
                              .FindByUid(negotiationUid)
                              .Select(n => new NegotiationDto
                              {
                                  Negotiation = n,
                                  ProjectBuyerEvaluationDto = new ProjectBuyerEvaluationDto()
                                  {
                                      BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto()
                                      {
                                          AttendeeOrganization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                          Organization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization,
                                          AttendeeCollaboratorDtos = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                          {
                                              AttendeeCollaborator = aoc.AttendeeCollaborator
                                          })
                                      },
                                      ProjectDto = new ProjectDto()
                                      {
                                          Project = n.ProjectBuyerEvaluation.Project,
                                          SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                          {
                                              AttendeeOrganization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization,
                                              Organization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization,
                                              AttendeeCollaboratorDtos = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                              {
                                                  AttendeeCollaborator = aoc.AttendeeCollaborator
                                              })
                                          }
                                      }
                                  },
                                  RoomDto = new RoomDto()
                                  {
                                      Room = n.Room,
                                      RoomNameDtos = n.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                      {
                                          RoomName = rn,
                                          LanguageDto = new LanguageDto
                                          {
                                              Id = rn.Language.Id,
                                              Uid = rn.Language.Uid,
                                              Code = rn.Language.Code
                                          }
                                      })
                                  }
                              })
                              .FirstOrDefaultAsync();

            return negotiationDto;
        }

        /// <summary>
        /// Finds the main information widget dto asynchronous.
        /// </summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        public async Task<NegotiationDto> FindMainInformationWidgetDtoAsync(Guid negotiationUid)
        {
            var negotiationDto = await this.GetBaseQuery()
                              .FindByUid(negotiationUid)
                              .Select(n => new NegotiationDto
                              {
                                  Negotiation = n,
                                  ProjectBuyerEvaluationDto = new ProjectBuyerEvaluationDto()
                                  {
                                      BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto()
                                      {
                                          AttendeeOrganization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                          Organization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization,
                                          AttendeeCollaboratorDtos = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                          {
                                              AttendeeCollaborator = aoc.AttendeeCollaborator
                                          })
                                      },
                                      ProjectDto = new ProjectDto()
                                      {
                                          Project = n.ProjectBuyerEvaluation.Project,
                                          SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                          {
                                              AttendeeOrganization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization,
                                              Organization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization,
                                              AttendeeCollaboratorDtos = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                              {
                                                  AttendeeCollaborator = aoc.AttendeeCollaborator
                                              })
                                          },
                                          ProjectTitleDtos = n.ProjectBuyerEvaluation.Project.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                          {
                                              ProjectTitle = t,
                                              Language = t.Language
                                          }),
                                          ProjectLogLineDtos = n.ProjectBuyerEvaluation.Project.ProjectLogLines.Where(ll => !ll.IsDeleted).Select(ll => new ProjectLogLineDto
                                          {
                                              ProjectLogLine = ll,
                                              Language = ll.Language
                                          })
                                      }
                                  },
                                  RoomDto = new RoomDto()
                                  {
                                      Room = n.Room,
                                      RoomNameDtos = n.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                      {
                                          RoomName = rn,
                                          LanguageDto = new LanguageDto
                                          {
                                              Id = rn.Language.Id,
                                              Uid = rn.Language.Uid,
                                              Code = rn.Language.Code
                                          }
                                      })
                                  }
                              })
                              .FirstOrDefaultAsync();

            return negotiationDto;
        }

        /// <summary>
        /// Finds the virtual meeting widget dto asynchronous.
        /// </summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        public async Task<NegotiationDto> FindVirtualMeetingWidgetDtoAsync(Guid negotiationUid)
        {
            var negotiationDto = await this.GetBaseQuery()
                              .FindByUid(negotiationUid)
                              .Select(n => new NegotiationDto
                              {
                                  ProjectBuyerEvaluationDto = new ProjectBuyerEvaluationDto()
                                  {
                                      BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto()
                                      {
                                          AttendeeCollaboratorDtos = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                          {
                                              AttendeeCollaborator = aoc.AttendeeCollaborator
                                          })
                                      },
                                      ProjectDto = new ProjectDto()
                                      {
                                          SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                          {
                                              AttendeeCollaboratorDtos = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                              {
                                                  AttendeeCollaborator = aoc.AttendeeCollaborator
                                              })
                                          }
                                      }
                                  }
                              })
                              .FirstOrDefaultAsync();

            return negotiationDto;
        }

        /// <summary>Finds the scheduled widget dto asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <param name="negotiationDate">The negotiation date.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        public async Task<List<NegotiationGroupedByDateDto>> FindScheduledWidgetDtoAsync(
            int editionId,
            Guid? buyerOrganizationUid,
            Guid? sellerOrganizationUid,
            string projectKeywords,
            DateTime? negotiationDate,
            Guid? roomUid)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByBuyerOrganizationUid(buyerOrganizationUid)
                                .FindBySellerOrganizationUid(sellerOrganizationUid)
                                .FindByProjectKeywords(projectKeywords)
                                .FindByDate(negotiationDate)
                                .FindByRoomUid(roomUid)
                                .Include(n => n.Room)
                                .Include(n => n.Room.RoomNames)
                                .Include(n => n.Room.RoomNames.Select(rn => rn.Language))
                                .Include(n => n.ProjectBuyerEvaluation)
                                .Include(n => n.ProjectBuyerEvaluation.Project)
                                .Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles)
                                .Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles.Select(pt => pt.Language))
                                .Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization)
                                .Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization)
                                .Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization)
                                .Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization);

            return (await query.ToListAsync())
                                .GroupBy(n => n.StartDate.ToBrazilTimeZone().Date)
                                .Select(nd => new NegotiationGroupedByDateDto(nd.Key, nd.ToList()))
                                .OrderBy(ngd => ngd.Date)
                                .ToList();
        }

        /// <summary>
        /// Finds the negotiations by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<Negotiation>> FindNegotiationsByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds the negotiations by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<Negotiation>> FindManualScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false)
        {
            var query = this.GetBaseQuery()
                                .IsManual()
                                .FindByRoomId(roomId, showAllRooms);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find scheduled negotiations by room identifier as an asynchronous operation.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="showAllRooms">if set to <c>true</c> [show all rooms].</param>
        /// <returns>Task&lt;List&lt;Negotiation&gt;&gt;.</returns>
        public async Task<List<Negotiation>> FindScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false)
        {
            var query = this.GetBaseQuery()
                                .FindByRoomId(roomId, showAllRooms);

            return await query.ToListAsync();
        }

        /// <summary>Truncates this instance.</summary>
        public void Truncate()
        {
            this._context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "TRUNCATE TABLE [dbo].[Negotiations]");
        }

        /// <summary>Creates multiple entities</summary>
        /// <param name="entities">Entities</param>
        public override void CreateAll(IEnumerable<Negotiation> entities)
        {
            try
            {
                this._context.BulkInsert(entities);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>Finds all schedule dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public Task<List<NegotiationDto>> FindAllScheduleDtosAsync(int editionId, int? attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, false)
                                .FindByAttendeeCollaboratorId(attendeeCollaboratorId)
                                .FindByDateRange(startDate, endDate)
                                .Select(n => new NegotiationDto
                                {
                                    Negotiation = n,
                                    ProjectBuyerEvaluationDto = new ProjectBuyerEvaluationDto
                                    {
                                        ProjectBuyerEvaluation = n.ProjectBuyerEvaluation,
                                        BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                        {
                                            AttendeeOrganization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                            Organization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization
                                        },
                                        ProjectDto = new ProjectDto
                                        {
                                            Project = n.ProjectBuyerEvaluation.Project,
                                            SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                            {
                                                AttendeeOrganization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization,
                                                Organization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization
                                            },
                                            ProjectTitleDtos = n.ProjectBuyerEvaluation.Project.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                            {
                                                ProjectTitle = t,
                                                Language = t.Language
                                            }),
                                            ProjectLogLineDtos = n.ProjectBuyerEvaluation.Project.ProjectLogLines.Where(ll => !ll.IsDeleted).Select(ll => new ProjectLogLineDto
                                            {
                                                ProjectLogLine = ll,
                                                Language = ll.Language
                                            })
                                        }
                                    },
                                    RoomDto = new RoomDto
                                    {
                                        Room = n.Room,
                                        RoomNameDtos = n.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                        {
                                            RoomName = rn,
                                            LanguageDto = new LanguageDto
                                            {
                                                Id = rn.Language.Id,
                                                Uid = rn.Language.Uid,
                                                Code = rn.Language.Code
                                            }
                                        })
                                    }
                                });

            return query
                        .ToListAsync();
        }

        /// <summary>Finds the report widget dto asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <param name="negotiationDate">The negotiation date.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        public async Task<List<NegotiationReportGroupedByDateDto>> FindReportWidgetDtoAsync(
            int editionId,
            Guid? buyerOrganizationUid,
            Guid? sellerOrganizationUid,
            string projectKeywords,
            DateTime? negotiationDate,
            Guid? roomUid)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByBuyerOrganizationUid(buyerOrganizationUid)
                                .FindBySellerOrganizationUid(sellerOrganizationUid)
                                .FindByProjectKeywords(projectKeywords)
                                .FindByDate(negotiationDate)
                                .FindByRoomUid(roomUid)
                                .Include(n => n.Room)
                                .Include(n => n.Room.RoomNames)
                                .Include(n => n.Room.RoomNames.Select(rn => rn.Language))
                                .Include(n => n.ProjectBuyerEvaluation)
                                .Include(n => n.ProjectBuyerEvaluation.Project)
                                .Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles)
                                .Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles.Select(pt => pt.Language))
                                .Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization)
                                .Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization)
                                .Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization)
                                .Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization);

            return (await query.ToListAsync())
                                .GroupBy(n => n.StartDate.ToBrazilTimeZone().Date)
                                .Select(nd => new NegotiationReportGroupedByDateDto(nd.Key, nd.ToList()))
                                .OrderBy(ngd => ngd.Date)
                                .ToList();
        }

        /// <summary>
        /// Finds the collaborator scheduled widget dto asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <param name="negotiationDate">The negotiation date.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <returns></returns>
        public async Task<List<NegotiationGroupedByDateDto>> FindCollaboratorScheduledWidgetDtoAsync(
            int editionId,
            Guid? buyerOrganizationUid,
            Guid? sellerOrganizationUid,
            string projectKeywords,
            DateTime? negotiationDate,
            Guid? attendeeCollaboratorUid)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByBuyerOrganizationUid(buyerOrganizationUid)
                                .FindBySellerOrganizationUid(sellerOrganizationUid)
                                .FindByProjectKeywords(projectKeywords)
                                .FindByDate(negotiationDate)
                                .FindByAttendeeCollaboratorUid(attendeeCollaboratorUid)
                                .Include(n => n.Room)
                                .Include(n => n.Room.RoomNames)
                                .Include(n => n.Room.RoomNames.Select(rn => rn.Language))
                                .Include(n => n.ProjectBuyerEvaluation)
                                .Include(n => n.ProjectBuyerEvaluation.Project)
                                .Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles)
                                .Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles.Select(pt => pt.Language))
                                .Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization)
                                .Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization)
                                .Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization)
                                .Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization);

            return (await query.ToListAsync())
                                .GroupBy(n => n.StartDate.ToBrazilTimeZone().Date)
                                .Select(nd => new NegotiationGroupedByDateDto(
                                    nd.Key,
                                    nd.Select(n => new NegotiationDto
                                    {
                                        Negotiation = n,
                                        ProjectBuyerEvaluationDto = new ProjectBuyerEvaluationDto()
                                        {
                                            ProjectBuyerEvaluation = n.ProjectBuyerEvaluation,
                                            ProjectEvaluationStatus = n.ProjectBuyerEvaluation.ProjectEvaluationStatus,
                                            BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto()
                                            {
                                                AttendeeOrganization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                                Organization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization
                                            },
                                            ProjectDto = new ProjectDto()
                                            {
                                                Project = n.ProjectBuyerEvaluation.Project,
                                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                                {
                                                    AttendeeOrganization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization,
                                                    Organization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization
                                                },
                                                ProjectTitleDtos = n.ProjectBuyerEvaluation.Project.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                                {
                                                    ProjectTitle = t,
                                                    Language = t.Language
                                                }),
                                            }
                                        },
                                        RoomDto = new RoomDto()
                                        {
                                            Room = n.Room,
                                            RoomNameDtos = n.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                            {
                                                RoomName = rn,
                                                LanguageDto = new LanguageDto
                                                {
                                                    Id = rn.Language.Id,
                                                    Uid = rn.Language.Uid,
                                                    Code = rn.Language.Code
                                                }
                                            })
                                        },
                                        UpdaterDto = new UserBaseDto
                                        {
                                            Id = n.Updater.Id,
                                            Uid = n.Updater.Uid,
                                            Name = n.Updater.Name,
                                            Email = n.Updater.Email
                                        }
                                    }).ToList())
                                )
                                .OrderBy(ngd => ngd.Date)
                                .ToList();
        }
    }
}