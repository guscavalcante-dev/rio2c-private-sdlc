// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Daniel Giese Rodrigues
// Created          : 02-27-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 02-27-2025
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
using PlataformaRio2C.Domain.Interfaces.Repositories;
using System.Linq.Dynamic;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Negotiation IQueryable Extensions

    /// <summary>
    /// NegotiationIQueryableExtensions
    /// </summary>
    internal static class MusicBusinessRoundNegotiationIQueryableExtensions
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationId">The negotiation identifier.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindById(this IQueryable<MusicBusinessRoundNegotiation> query, int negotiationId)
        {
            query = query.Where(n => n.Id == negotiationId);

            return query;
        }

        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByUid(this IQueryable<MusicBusinessRoundNegotiation> query, Guid negotiationUid)
        {
            query = query.Where(n => n.Uid == negotiationUid);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByEditionId(this IQueryable<MusicBusinessRoundNegotiation> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(n => (showAllEditions || n.EditionId == editionId));

            return query;
        }

        /// <summary>Finds the by buyer attendee organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByBuyerOrganizationUid(this IQueryable<MusicBusinessRoundNegotiation> query, Guid? buyerOrganizationUid)
        {
            if (buyerOrganizationUid.HasValue)
            {
                query = query.Where(n => n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization.Uid == buyerOrganizationUid);
            }

            return query;
        }

        /// <summary>Finds the by seller attendee organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sellerCollaboratorUid">The seller organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindBySellerCollaboratorUid(this IQueryable<MusicBusinessRoundNegotiation> query, Guid? sellerCollaboratorUid)
        {
            if (sellerCollaboratorUid.HasValue)
            {
                query = query.Where(n => n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator.Collaborator.Uid == sellerCollaboratorUid);
            }

            return query;
        }

        /// <summary>Finds the by attendee collaborator identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByAttendeeCollaboratorId(this IQueryable<MusicBusinessRoundNegotiation> query, int? attendeeCollaboratorId)
        {
            if (attendeeCollaboratorId.HasValue)
            {
                query = query
                            .Where(n => n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaboratorId == attendeeCollaboratorId)
                                        || n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaboratorId == attendeeCollaboratorId));
            }

            return query;
        }

        /// <summary>
        /// Finds the by attendee collaborator uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByAttendeeCollaboratorUid(this IQueryable<MusicBusinessRoundNegotiation> query, Guid? attendeeCollaboratorUid)
        {
            if (attendeeCollaboratorUid.HasValue)
            {
                query = query
                            .Where(n => n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid)
                                        || n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid));
            }

            return query;
        }

        /// <summary>Finds the by date range.</summary>
        /// <param name="query">The query.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByDateRange(this IQueryable<MusicBusinessRoundNegotiation> query, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            //endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            query = query.Where(n => (n.StartDate >= startDate && n.StartDate <= endDate)
                                     || (n.EndDate >= startDate && n.EndDate <= endDate));

            return query;
        }

        ///// <summary>Finds the by project keywords.</summary>
        ///// <param name="query">The query.</param>
        ///// <param name="projectKeywords">The project keywords.</param>
        ///// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByProjectKeywords(this IQueryable<MusicBusinessRoundNegotiation> query, string projectKeywords)
        {
            if (!string.IsNullOrEmpty(projectKeywords))
            {
                var outerWhere = PredicateBuilder.New<MusicBusinessRoundNegotiation>(false);
                var innerSellerAttendeeCollaboratorFirstNameWhere = PredicateBuilder.New<MusicBusinessRoundNegotiation>(true);
                var innerSellerAttendeeCollaboratorLastNamesWhere = PredicateBuilder.New<MusicBusinessRoundNegotiation>(true);
                var innerSellerAttendeeCollaboratorCompanyNameWhere = PredicateBuilder.New<MusicBusinessRoundNegotiation>(true);
                var innerSellerAttendeeCollaboratorStageNameWhere = PredicateBuilder.New<MusicBusinessRoundNegotiation>(true);
                if (!string.IsNullOrEmpty(projectKeywords))
                {
                    innerSellerAttendeeCollaboratorFirstNameWhere = innerSellerAttendeeCollaboratorFirstNameWhere.Or(sao => sao.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator.Collaborator.FirstName.Contains(projectKeywords));
                    innerSellerAttendeeCollaboratorLastNamesWhere = innerSellerAttendeeCollaboratorLastNamesWhere.Or(sao => sao.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator.Collaborator.LastNames.Contains(projectKeywords));
                    innerSellerAttendeeCollaboratorCompanyNameWhere = innerSellerAttendeeCollaboratorCompanyNameWhere.Or(sao => sao.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator.Collaborator.CompanyName.Contains(projectKeywords));
                    innerSellerAttendeeCollaboratorStageNameWhere = innerSellerAttendeeCollaboratorStageNameWhere.Or(sao => sao.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator.Collaborator.StageName.Contains(projectKeywords));
                }
                outerWhere = outerWhere.Or(innerSellerAttendeeCollaboratorFirstNameWhere);
                outerWhere = outerWhere.Or(innerSellerAttendeeCollaboratorLastNamesWhere);
                outerWhere = outerWhere.Or(innerSellerAttendeeCollaboratorCompanyNameWhere);
                outerWhere = outerWhere.Or(innerSellerAttendeeCollaboratorStageNameWhere);
                query = query.Where(outerWhere);
            }
            return query;

        }

        /// <summary>Finds the by date.</summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationDate">The negotiation date.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByDate(this IQueryable<MusicBusinessRoundNegotiation> query, DateTime? negotiationDate)
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
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByRoomUid(this IQueryable<MusicBusinessRoundNegotiation> query, Guid? roomUid)
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
        internal static IQueryable<MusicBusinessRoundNegotiation> FindByRoomId(this IQueryable<MusicBusinessRoundNegotiation> query, int? roomId, bool showAllRooms = false)
        {
            query = query.Where(n => showAllRooms || n.Room.Id == roomId);

            return query;
        }

        /// <summary>
        /// Determines whether this instance is manual.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> IsManual(this IQueryable<MusicBusinessRoundNegotiation> query)
        {
            query = query.Where(n => !n.IsAutomatic);

            return query;
        }

        /// <summary>
        /// Determines whether this instance is automatic.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> IsAutomatic(this IQueryable<MusicBusinessRoundNegotiation> query)
        {
            query = query.Where(n => n.IsAutomatic);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBusinessRoundNegotiation> IsNotDeleted(this IQueryable<MusicBusinessRoundNegotiation> query)
        {
            query = query.Where(n => !n.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>NegotiationRepository</summary>
    public class MusicBusinessRoundNegotiationRepository : Repository<PlataformaRio2CContext, MusicBusinessRoundNegotiation>, IMusicBusinessRoundNegotiationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="NegotiationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MusicBusinessRoundNegotiationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicBusinessRoundNegotiation> GetBaseQuery(bool @readonly = false)
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
        public async Task<MusicBusinessRoundNegotiation> FindByIdAsync(int negotiationId)
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
        public async Task<MusicBusinessRoundNegotiation> FindByUidAsync(Guid negotiationUid)
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
        public async Task<MusicBusinessRoundNegotiationDto> FindDtoAsync(Guid negotiationUid)
        {
            var negotiationDto = await this.GetBaseQuery()
                              .FindByUid(negotiationUid)
                              .Select(n => new MusicBusinessRoundNegotiationDto
                              {
                                  Negotiation = n,
                                  ProjectBuyerEvaluationDto = new MusicBusinessRoundProjectBuyerEvaluationDto()
                                  {
                                      BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto()
                                      {
                                          AttendeeOrganization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                          Organization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization,
                                          AttendeeCollaboratorDtos = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                          {
                                              AttendeeCollaborator = aoc.AttendeeCollaborator
                                          })
                                      },
                                      MusicBusinessRoundProjectDto = new MusicBusinessRoundProjectDto()
                                      {
                                          SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto()
                                          {
                                              AttendeeCollaborator = n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator
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
        public async Task<MusicBusinessRoundNegotiationDto> FindMainInformationWidgetDtoAsync(Guid negotiationUid)
        {
            var negotiationDto = await this.GetBaseQuery()
                              .FindByUid(negotiationUid)
                              .Select(n => new MusicBusinessRoundNegotiationDto
                              {
                                  Negotiation = n,
                                  ProjectBuyerEvaluationDto = new MusicBusinessRoundProjectBuyerEvaluationDto()
                                  {
                                      BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto()
                                      {
                                          AttendeeOrganization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                          Organization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization,
                                          AttendeeCollaboratorDtos = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                          {
                                              AttendeeCollaborator = aoc.AttendeeCollaborator
                                          })
                                      },
                                      MusicBusinessRoundProjectDto = new MusicBusinessRoundProjectDto()
                                      {
                                          SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto()
                                          {
                                              AttendeeCollaborator = n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator
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
        /// Finds the virtual meeting widget dto asynchronous.
        /// </summary>
        /// <param name="negotiationUid">The negotiation uid.</param>
        /// <returns></returns>
        public async Task<MusicBusinessRoundNegotiationDto> FindVirtualMeetingWidgetDtoAsync(Guid negotiationUid)
        {
            var negotiationDto = await this.GetBaseQuery()
                              .FindByUid(negotiationUid)
                              .Select(n => new MusicBusinessRoundNegotiationDto
                              {
                                  Negotiation = n,
                                  ProjectBuyerEvaluationDto = new MusicBusinessRoundProjectBuyerEvaluationDto()
                                  {
                                      BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto()
                                      {
                                          AttendeeOrganization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                          Organization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization,
                                          AttendeeCollaboratorDtos = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                          {
                                              AttendeeCollaborator = aoc.AttendeeCollaborator
                                          })
                                      },
                                      MusicBusinessRoundProjectDto = new MusicBusinessRoundProjectDto()
                                      {
                                          SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto()
                                          {
                                              AttendeeCollaborator = n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator
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

        /// <summary>Finds the scheduled widget dto asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <param name="negotiationDate">The negotiation date.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        public async Task<List<MusicBusinessRoundNegotiationGroupedByDateDto>> FindScheduledWidgetDtoAsync(
            int editionId,
            Guid? buyerOrganizationUid,
            Guid? sellerOrganizationUid,
            string projectKeywords,
            DateTime? negotiationDate,
            Guid? roomUid,
            bool showParticipants)
        {
            this.SetProxyEnabled(false);

            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByBuyerOrganizationUid(buyerOrganizationUid)
                                .FindBySellerCollaboratorUid(sellerOrganizationUid)
                                .FindByProjectKeywords(projectKeywords)
                                .FindByDate(negotiationDate)
                                .FindByRoomUid(roomUid)
                                .Include(n => n.Room)
                                .Include(n => n.Room.RoomNames)
                                .Include(n => n.Room.RoomNames.Select(rn => rn.Language))
                                .Include(n => n.MusicBusinessRoundProjectBuyerEvaluation)
                                .Include(n => n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject);
                                //Refactor this.
                                ////.Include(n => n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.ProjectTitles)
                                //.Include(n => n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.ProjectTitles.Select(pt => pt.Language))
                                //.Include(n => n.MusicBusinessRoundProjectBuyerEvaluation.Project.SellerAttendeeOrganization)
                                //.Include(n => n.MusicBusinessRoundProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization)
                                //.Include(n => n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization)
                                //.Include(n => n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization);

            if (showParticipants)
            {
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators);
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator));
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator.Collaborator));
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator.AttendeeOrganizationCollaborators));
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator.AttendeeOrganizationCollaborators.Select(aoc => aoc.AttendeeOrganization)));
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator.AttendeeOrganizationCollaborators.Select(aoc => aoc.AttendeeOrganization.Organization)));
            }

            var negotiationsGroupedByDateDtos = (await query.ToListAsync())
                                                    .GroupBy(n => n.StartDate.ToBrazilTimeZone().Date)
                                                    .Select(nd => new MusicBusinessRoundNegotiationGroupedByDateDto(nd.Key, nd.ToList()))
                                                    .OrderBy(ngd => ngd.Date)
                                                    .ToList();

            this.SetProxyEnabled(true);

            return negotiationsGroupedByDateDtos;
        }

        /// <summary>
        /// Finds the negotiations by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<MusicBusinessRoundNegotiation>> FindNegotiationsByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds the negotiations by edition identifier asynchronous.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="showAllRooms">if set to <c>true</c> [show all rooms].</param>
        /// <returns></returns>
        public async Task<List<MusicBusinessRoundNegotiation>> FindManualScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false)
        {
            var query = this.GetBaseQuery()
                                .IsManual()
                                .FindByRoomId(roomId, showAllRooms);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds the negotiations by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<MusicBusinessRoundNegotiation>> FindAutomaticScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false)
        {
            var query = this.GetBaseQuery()
                                .IsAutomatic()
                                .FindByRoomId(roomId, showAllRooms);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find scheduled negotiations by room identifier as an asynchronous operation.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="showAllRooms">if set to <c>true</c> [show all rooms].</param>
        /// <returns>Task&lt;List&lt;Negotiation&gt;&gt;.</returns>
        public async Task<List<MusicBusinessRoundNegotiation>> FindScheduledNegotiationsByRoomIdAsync(int roomId, bool showAllRooms = false)
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
        public override void CreateAll(IEnumerable<MusicBusinessRoundNegotiation> entities)
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

        /// <summary>
        /// Finds all scheduled negotiations dtos asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public Task<List<MusicBusinessRoundNegotiationDto>> FindAllScheduledNegotiationsDtosAsync(int editionId, int? attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, false)
                                .FindByAttendeeCollaboratorId(attendeeCollaboratorId)
                                .FindByDateRange(startDate, endDate)
                                .Select(n => new MusicBusinessRoundNegotiationDto
                                {
                                    Negotiation = n,
                                    ProjectBuyerEvaluationDto = new MusicBusinessRoundProjectBuyerEvaluationDto()
                                    {
                                        BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto()
                                        {
                                            AttendeeOrganization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                            Organization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization,
                                            AttendeeCollaboratorDtos = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted).Select(aoc => new AttendeeCollaboratorDto
                                            {
                                                AttendeeCollaborator = aoc.AttendeeCollaborator
                                            })
                                        },
                                        MusicBusinessRoundProjectDto = new MusicBusinessRoundProjectDto()
                                        {
                                            SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto()
                                            {
                                                AttendeeCollaborator = n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator
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
                                });

            return query
                        .ToListAsync();
        }

        /// <summary>
        /// Finds the report widget dto asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="sellerOrganizationUid">The seller organization uid.</param>
        /// <param name="projectKeywords">The project keywords.</param>
        /// <param name="negotiationDate">The negotiation date.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <param name="showParticipants">if set to <c>true</c> [show participants].</param>
        /// <returns></returns>
        public async Task<List<MusicBusinessRoundNegotiationReportGroupedByDateDto>> FindReportWidgetDtoAsync(
            int editionId,
            Guid? buyerOrganizationUid,
            Guid? sellerOrganizationUid,
            string projectKeywords,
            DateTime? negotiationDate,
            Guid? roomUid,
            bool showParticipants)
        {
            this.SetProxyEnabled(false);

            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByBuyerOrganizationUid(buyerOrganizationUid)
                                .FindBySellerCollaboratorUid(sellerOrganizationUid)
                                .FindByProjectKeywords(projectKeywords)
                                .FindByDate(negotiationDate)
                                .FindByRoomUid(roomUid)
                                .Include(n => n.Room)
                                .Include(n => n.Room.RoomNames)
                                .Include(n => n.Room.RoomNames.Select(rn => rn.Language))
                                .Include(n => n.MusicBusinessRoundProjectBuyerEvaluation)
                                .Include(n => n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject);
                                //todo:Refactor this.
                                //.Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles)
                                //.Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles.Select(pt => pt.Language))
                                //.Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization)
                                //.Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization)
                                //.Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization)
                                //.Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization);

            if (showParticipants)
            {
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators);
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator));
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator.Collaborator));
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator.AttendeeOrganizationCollaborators));
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator.AttendeeOrganizationCollaborators.Select(aoc => aoc.AttendeeOrganization)));
                query = query.Include(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators.Select(anc => anc.AttendeeCollaborator.AttendeeOrganizationCollaborators.Select(aoc => aoc.AttendeeOrganization.Organization)));
            }

            var negotiationReportGroupedByDateDtos = (await query.ToListAsync())
                                                        .GroupBy(n => n.StartDate.ToBrazilTimeZone().Date)
                                                        .Select(nd => new MusicBusinessRoundNegotiationReportGroupedByDateDto(nd.Key, nd.ToList()))
                                                        .OrderBy(ngd => ngd.Date)
                                                        .ToList();

            this.SetProxyEnabled(true);

            return negotiationReportGroupedByDateDtos;
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
        public async Task<List<MusicBusinessRoundNegotiationGroupedByDateDto>> FindCollaboratorScheduledWidgetDtoAsync(
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
                                .FindBySellerCollaboratorUid(sellerOrganizationUid)
                                .FindByProjectKeywords(projectKeywords)
                                .FindByDate(negotiationDate)
                                .FindByAttendeeCollaboratorUid(attendeeCollaboratorUid)
                                .Include(n => n.Room)
                                .Include(n => n.Room.RoomNames)
                                .Include(n => n.Room.RoomNames.Select(rn => rn.Language))
                                .Include(n => n.MusicBusinessRoundProjectBuyerEvaluation)
                                .Include(n => n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject);
                                //todo:Refactor this.
                                //.Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles)
                                //.Include(n => n.ProjectBuyerEvaluation.Project.ProjectTitles.Select(pt => pt.Language))
                                //.Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization)
                                //.Include(n => n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization)
                                //.Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization)
                                //.Include(n => n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization);

            return (await query.ToListAsync())
                                .GroupBy(n => n.StartDate.ToBrazilTimeZone().Date)
                                .Select(nd => new MusicBusinessRoundNegotiationGroupedByDateDto(
                                    nd.Key,
                                    nd.Select(n => new MusicBusinessRoundNegotiationDto
                                    {
                                        Negotiation = n,
                                        ProjectBuyerEvaluationDto = new MusicBusinessRoundProjectBuyerEvaluationDto()
                                        {
                                            MusicBusinessRoundProjectBuyerEvaluation = n.MusicBusinessRoundProjectBuyerEvaluation,
                                            ProjectEvaluationStatus = n.MusicBusinessRoundProjectBuyerEvaluation.ProjectEvaluationStatus,
                                            BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto()
                                            {
                                                AttendeeOrganization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                                Organization = n.MusicBusinessRoundProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization
                                            },
                                            MusicBusinessRoundProjectDto = new MusicBusinessRoundProjectDto()
                                            {
                                                SellerAttendeeCollaboratorDto = new AttendeeCollaboratorDto()
                                                {
                                                    AttendeeCollaborator = n.MusicBusinessRoundProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaborator
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