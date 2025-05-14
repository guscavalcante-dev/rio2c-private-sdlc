// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese
// Created          : 05/03/2025
//
// Last Modified By : Daniel Giese
// Last Modified On : 05-12-2025
// ***********************************************************************
// <copyright file="CreateMusicBusinessRoundNegotiationsCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateMusicBusinessRoundNegotiationsCommandHandler</summary>
    public class CreateMusicBusinessRoundNegotiationsCommandHandler : MusicBusinessRoundNegotiationBaseCommandHandler, IRequestHandler<CreateMusicBusinessRoundNegotiations, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly INegotiationConfigRepository negotiationConfigRepo;
        private readonly IMusicBusinessRoundProjectBuyerEvaluationRepository musicBusinessRoundProjectBuyerEvaluationRepo;
        private readonly ILogisticAirfareRepository logisticAirfareRepo;
        private readonly IConferenceRepository conferenceRepo;

        private IList<MusicBusinessRoundProjectBuyerEvaluation> _projectSubmissionsError = new List<MusicBusinessRoundProjectBuyerEvaluation>();
        private List<LogisticAirfare> logisticAirfares = new List<LogisticAirfare>();
        private List<Conference> conferences = new List<Conference>();

        /// <summary>Initializes a new instance of the <see cref="CreateMusicBusinessRoundNegotiationsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        /// <param name="projectBuyerEvaluationRepository">The project buyer evaluation repository.</param>
        /// <param name="logisticAirfareRepository">The logistic airfare repository.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        public CreateMusicBusinessRoundNegotiationsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IMusicBusinessRoundNegotiationRepository negotiationRepository,
            IEditionRepository editionRepository,
            INegotiationConfigRepository negotiationConfigRepository,
            IMusicBusinessRoundProjectBuyerEvaluationRepository projectBuyerEvaluationRepository,
            ILogisticAirfareRepository logisticAirfareRepository,
            IConferenceRepository conferenceRepository)
            : base(eventBus, uow, negotiationRepository)
        {
            this.editionRepo = editionRepository;
            this.negotiationConfigRepo = negotiationConfigRepository;
            this.musicBusinessRoundProjectBuyerEvaluationRepo = projectBuyerEvaluationRepository;
            this.logisticAirfareRepo = logisticAirfareRepository;
            this.conferenceRepo = conferenceRepository;
        }

        /// <summary>Handles the specified create negotiations.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateMusicBusinessRoundNegotiations cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var edition = await this.editionRepo.FindByUidAsync(cmd.EditionUid ?? Guid.Empty, true);
            if (edition.MusicBusinessRoundNegotiationsCreateStartDate.HasValue && !edition.MusicBusinessRoundNegotiationsCreateEndDate.HasValue)
            {
                this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityIsAlreadyAction, Labels.Agenda, Labels.BeingProcessed.ToLowerInvariant()), new string[] { "ToastrError" })));
                return this.AppValidationResult;
            }

            try
            {
                edition?.StartMusicBusinessRoundNegotiationsCreation(cmd.UserId);
                this.Uow.SaveChanges();

                var negotiationConfigs = await this.negotiationConfigRepo.FindAllForGenerateNegotiationsAsync(cmd.EditionId.Value, ProjectType.Music.Id);
                if (negotiationConfigs?.Count == 0)
                {
                    edition?.CancelMusicBusinessRoundNegotiationsCreation(cmd.UserId);
                    this.Uow.SaveChanges();
                    this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Rooms, Labels.FoundFP), new string[] { "ToastrError" })));
                    return this.AppValidationResult;
                }

                var negotiationSlots = this.GetNegotiationSlots(negotiationConfigs, cmd.UserId);

                var projectBuyerEvaluations = await this.musicBusinessRoundProjectBuyerEvaluationRepo.FindAllForGenerateNegotiationsAsync(cmd.EditionId ?? 0);
                if (projectBuyerEvaluations?.Count == 0)
                {
                    edition?.CancelMusicBusinessRoundNegotiationsCreation(cmd.UserId);
                    this.Uow.SaveChanges();
                    this.AppValidationResult.Add(this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Projects, Labels.FoundMP), new string[] { "ToastrError" })));
                    return this.AppValidationResult;
                }

                this.logisticAirfares = await this.logisticAirfareRepo.FindAllForGenerateNegotiationsAsync(cmd.EditionUid ?? Guid.Empty);
                this.conferences = await this.conferenceRepo.FindAllForGenerateNegotiationsAsync(cmd.EditionUid ?? Guid.Empty);

                // Marcar os slots que já estão ocupados
                var existingNegotiations = await this.MusicBusinessRoundNegotiationRepo.FindAllByEditionIdAsync(cmd.EditionId.Value);
                foreach (var existing in existingNegotiations)
                {
                    var slot = negotiationSlots.FirstOrDefault(ns =>
                        ns.RoomId == existing.RoomId &&
                        ns.TableNumber == existing.TableNumber &&
                        ns.RoundNumber == existing.RoundNumber);

                    if (slot != null)
                    {
                        slot.DisableSlot(existing.MusicBusinessRoundProjectBuyerEvaluation, existing.IsDeleted);
                    }
                }

                // Preencher os slots restantes
                this.FillNegotiationSlots(negotiationSlots, projectBuyerEvaluations);

                var newNegotiations = negotiationSlots
                    .Where(ns => ns.MusicBusinessRoundProjectBuyerEvaluation != null &&
                                 !ns.IsDeleted &&
                                 !existingNegotiations.Any(en =>
                                     en.RoomId == ns.RoomId &&
                                     en.TableNumber == ns.TableNumber &&
                                     en.RoundNumber == ns.RoundNumber))
                    .ToList();

                this.MusicBusinessRoundNegotiationRepo.CreateAll(newNegotiations);

                edition?.FinishMusicBusinessRoundNegotiationsCreation(cmd.UserId);

                this.Uow.SaveChanges();
            }
            catch (Exception)
            {
                edition?.CancelMusicBusinessRoundNegotiationsCreation(cmd.UserId);
                this.Uow.SaveChanges();
                throw;
            }

            return this.AppValidationResult;
        }

        /// <summary>Gets the negotiation slots.</summary>
        /// <param name="negotiationConfigs">The negotiation configs.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private List<MusicBusinessRoundNegotiation> GetNegotiationSlots(List<NegotiationConfig> negotiationConfigs, int userId)
        {
            var negotiationSlots = new List<MusicBusinessRoundNegotiation>();
            var roundNumber = 1;

            if (negotiationConfigs?.Any() != true)
            {
                return negotiationSlots;
            }

            // Each day
            foreach (var negotiationConfig in negotiationConfigs)
            {
                var startDate = negotiationConfig.StartDate;

                #region Round first turn

                // Each slot
                for (int iSlot = 0; iSlot < negotiationConfig.RoundFirstTurn; iSlot++)
                {
                    if (startDate.Add(negotiationConfig.TimeOfEachRound) <= negotiationConfig.EndDate)
                    {
                        // Each room
                        foreach (var negotiationRoomConfig in negotiationConfig.NegotiationRoomConfigs.Where(nrc => !nrc.IsDeleted))
                        {
                            // Each automatic table
                            for (int tableNumber = 1; tableNumber <= negotiationRoomConfig.CountAutomaticTables; tableNumber++)
                            {
                                negotiationSlots.Add(this.CreateNegotiationSlot(negotiationConfig, negotiationRoomConfig, roundNumber, tableNumber, startDate, NegotiationTypeCodes.Automatic, userId));
                            }
                        }
                    }

                    startDate = startDate.Add(negotiationConfig.TimeOfEachRound.Add(negotiationConfig.TimeIntervalBetweenRound));
                    roundNumber++;
                }

                #endregion

                #region Round second turn

                startDate = startDate.Add(negotiationConfig.TimeIntervalBetweenTurn);

                // Each slot
                for (int iSlot = 0; iSlot < negotiationConfig.RoundSecondTurn; iSlot++)
                {

                    if (startDate.Add(negotiationConfig.TimeOfEachRound) <= negotiationConfig.EndDate)
                    {
                        // Each room
                        foreach (var negotiationRoomConfig in negotiationConfig.NegotiationRoomConfigs.Where(nrc => !nrc.IsDeleted))
                        {
                            // Each automatic table
                            for (int tableNumber = 1; tableNumber <= negotiationRoomConfig.CountAutomaticTables; tableNumber++)
                            {
                                negotiationSlots.Add(this.CreateNegotiationSlot(negotiationConfig, negotiationRoomConfig, roundNumber, tableNumber, startDate, NegotiationTypeCodes.Automatic, userId));
                            }
                        }
                    }

                    startDate = startDate.Add(negotiationConfig.TimeOfEachRound.Add(negotiationConfig.TimeIntervalBetweenRound));
                    roundNumber++;
                }

                #endregion
            }

            return negotiationSlots;
        }

        /// <summary>Creates the negotiation slot.</summary>
        /// <param name="dateConfig">The date configuration.</param>
        /// <param name="roomConfig">The room configuration.</param>
        /// <param name="numberSlot">The number slot.</param>
        /// <param name="iTable">The i table.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="type">The type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private MusicBusinessRoundNegotiation CreateNegotiationSlot(
            NegotiationConfig dateConfig,
            NegotiationRoomConfig roomConfig,
            int numberSlot,
            int iTable,
            DateTimeOffset startDate,
            NegotiationTypeCodes type,
            int userId)
        {
            return new MusicBusinessRoundNegotiation(
                dateConfig.EditionId,
                roomConfig.Room,
                startDate,
                startDate.Add(dateConfig.TimeOfEachRound),
                iTable,
                numberSlot,
                userId);
        }

        /// <summary>Processes the project submissions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluations">The project buyer evaluations.</param>
        private void FillNegotiationSlots(List<MusicBusinessRoundNegotiation> negotiationSlots, List<MusicBusinessRoundProjectBuyerEvaluation> projectBuyerEvaluations)
        {
            // Project buyer evaluations with more exceptions are in the beginning of the list
            projectBuyerEvaluations = this.GetProjectBuyerEvaluationsOrderedByExceptions(negotiationSlots, projectBuyerEvaluations);

            var buyerAttendeeOrganizations = projectBuyerEvaluations.GroupBy(e => e.BuyerAttendeeOrganizationId);
            foreach (var buyerAttendeeOrganization in buyerAttendeeOrganizations)
            {
                foreach (var projectBuyerEvaluation in buyerAttendeeOrganization.ToList())
                {
                    var roundsExceptions = this.GetRoundsExceptions(negotiationSlots, projectBuyerEvaluation);

                    var possibleNegotiationSlots = negotiationSlots?
                                                        .Where(ns => ns.MusicBusinessRoundProjectBuyerEvaluation == null // Is not allocated
                                                                     && ns.MusicBusinessRoundProjectBuyerEvaluation?.MusicBusinessRoundProjectId != projectBuyerEvaluation.MusicBusinessRoundProjectId
                                                                     && ns.MusicBusinessRoundProjectBuyerEvaluation?.BuyerAttendeeOrganizationId != projectBuyerEvaluation.BuyerAttendeeOrganizationId
                                                                     && !roundsExceptions.Contains(ns.RoundNumber) // Is not a exception
                                                                )?.ToList();

                    if (projectBuyerEvaluation.IsVirtualMeeting)
                    {
                        possibleNegotiationSlots = possibleNegotiationSlots.Where(ns => ns.Room.IsVirtualMeeting).ToList();
                    }
                    else
                    {
                        possibleNegotiationSlots = possibleNegotiationSlots.Where(ns => ns.Room.IsVirtualMeeting == projectBuyerEvaluation.BuyerAttendeeOrganization.Organization.IsVirtualMeeting).ToList();
                    }

                    if (possibleNegotiationSlots?.Any() == true)
                    {
                        var dateTest = possibleNegotiationSlots
                                            .Select(e => e.StartDate.Date)
                                            .FirstOrDefault();

                        var negotiationsInDate = negotiationSlots.FirstOrDefault(ns => ns.StartDate.Date == dateTest && ns.MusicBusinessRoundProjectBuyerEvaluationId == projectBuyerEvaluation.Id);
                        if (negotiationsInDate != null)
                        {
                            var negotiation = possibleNegotiationSlots.FirstOrDefault(e => e.TableNumber == negotiationsInDate.TableNumber && e.RoomId == negotiationsInDate.RoomId);

                            if (negotiation != null)
                            {
                                negotiation.AssignProjectBuyerEvaluation(projectBuyerEvaluation);
                            }
                            else
                            {
                                _projectSubmissionsError.Add(projectBuyerEvaluation);
                            }
                        }
                        else
                        {
                            var negotiation = possibleNegotiationSlots.FirstOrDefault();
                            if (negotiation != null)
                            {
                                negotiation.AssignProjectBuyerEvaluation(projectBuyerEvaluation);
                            }
                            else
                            {
                                _projectSubmissionsError.Add(projectBuyerEvaluation);
                            }
                        }
                    }
                    else
                    {
                        _projectSubmissionsError.Add(projectBuyerEvaluation);
                    }
                }
            }
        }

        /// <summary>Gets the project buyer evaluations ordered by exceptions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluations">The project buyer evaluations.</param>
        /// <returns></returns>
        private List<MusicBusinessRoundProjectBuyerEvaluation> GetProjectBuyerEvaluationsOrderedByExceptions(List<MusicBusinessRoundNegotiation> negotiationSlots, List<MusicBusinessRoundProjectBuyerEvaluation> projectBuyerEvaluations)
        {
            var projectBuyerEvaluationSlotExceptions = new List<Tuple<MusicBusinessRoundProjectBuyerEvaluation, int>>();

            if (projectBuyerEvaluations?.Any() == true)
            {
                foreach (var projectBuyerEvaluation in projectBuyerEvaluations)
                {
                    var slotExceptions = this.GetRoundsExceptions(negotiationSlots, projectBuyerEvaluation);
                    projectBuyerEvaluationSlotExceptions.Add(new Tuple<MusicBusinessRoundProjectBuyerEvaluation, int>(projectBuyerEvaluation, slotExceptions.Count));
                }

                return projectBuyerEvaluationSlotExceptions
                            .GroupBy(e => e.Item1.BuyerAttendeeOrganizationId)
                            .OrderByDescending(e => e.Count())
                            .ThenByDescending(e => e.First().Item2)
                            .SelectMany(e => e.ToList())
                            .Select(e => e.Item1)
                            .ToList();
            }

            return new List<MusicBusinessRoundProjectBuyerEvaluation>();
        }

        #region Slot exceptions

        /// <summary>Gets the slot exceptions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="playerProjectBuyerEvaluation">The player project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetRoundsExceptions(List<MusicBusinessRoundNegotiation> negotiationSlots, MusicBusinessRoundProjectBuyerEvaluation playerProjectBuyerEvaluation)
        {
            var result = new List<int>();

            var playerSlotExceptions = negotiationSlots
                                                    .Where(ns => ns.MusicBusinessRoundProjectBuyerEvaluationId == playerProjectBuyerEvaluation.Id
                                                                || ns.MusicBusinessRoundProjectBuyerEvaluation?.MusicBusinessRoundProjectId == playerProjectBuyerEvaluation.MusicBusinessRoundProjectId
                                                                || ns.MusicBusinessRoundProjectBuyerEvaluation?.MusicBusinessRoundProject?.SellerAttendeeCollaboratorId == playerProjectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaboratorId
                                                                || ns.MusicBusinessRoundProjectBuyerEvaluation?.BuyerAttendeeOrganizationId == playerProjectBuyerEvaluation.BuyerAttendeeOrganizationId)
                                                    .Select(ns => ns.RoundNumber)
                                                    .Distinct()
                                                    .ToList();

            result.AddRange(playerSlotExceptions);
            result.AddRange(this.GetLogisticsRoundsExceptions(negotiationSlots, playerProjectBuyerEvaluation));
            result.AddRange(this.GetConferencesSlotsExceptions(negotiationSlots, playerProjectBuyerEvaluation));

            return result;
        }

        #region Logistics

        /// <summary>Gets the logistics slots exceptions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetLogisticsRoundsExceptions(List<MusicBusinessRoundNegotiation> negotiationSlots, MusicBusinessRoundProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            result.AddRange(this.GetPlayerLogisticsRoundsExceptions(negotiationSlots, projectBuyerEvaluation));
            result.AddRange(this.GetProducerLogisticsRoundsExceptions(negotiationSlots, projectBuyerEvaluation));

            return result;
        }

        /// <summary>Gets the player logistics slots exceptions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetPlayerLogisticsRoundsExceptions(List<MusicBusinessRoundNegotiation> negotiationSlots, MusicBusinessRoundProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            if (this.logisticAirfares?.Any() != true)
            {
                return result;
            }

            // Airfare logistics
            var organizationLogisticAirfares = this.logisticAirfares
                                                    .Where(la => !la.Logistic.AttendeeCollaborator.IsDeleted
                                                                 && la.Logistic.AttendeeCollaborator.AttendeeOrganizationCollaborators
                                                                        .Any(aoc => !aoc.IsDeleted && aoc.AttendeeOrganizationId == projectBuyerEvaluation.BuyerAttendeeOrganizationId))
                                                    .ToList();
            if (organizationLogisticAirfares?.Any() != true)
            {
                return result;
            }

            var organizationLogisticsAirfareExceptions = new List<Tuple<DateTimeOffset, DateTimeOffset, bool>>();
            foreach (var organizationLogisticAirfare in organizationLogisticAirfares)
            {
                organizationLogisticsAirfareExceptions.Add(new Tuple<DateTimeOffset, DateTimeOffset, bool>(organizationLogisticAirfare.DepartureDate, organizationLogisticAirfare.ArrivalDate, organizationLogisticAirfare.IsArrival));
            }

            var logisticSlotsExceptions = negotiationSlots
                                                .Where(ns => organizationLogisticsAirfareExceptions
                                                                .Any(lde => lde.Item3 ? ns.StartDate < lde.Item2.AddHours(4) :   // Arrival
                                                                                        ns.EndDate > lde.Item1.AddHours(-4)))?   // Departure
                                                .Select(e => e.RoundNumber)
                                                .Distinct()
                                                .ToList();

            result.AddRange(logisticSlotsExceptions);

            return result;
        }

        /// <summary>Gets the producer logistics slots exceptions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetProducerLogisticsRoundsExceptions(List<MusicBusinessRoundNegotiation> negotiationSlots, MusicBusinessRoundProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            if (this.logisticAirfares?.Any() != true)
            {
                return result;
            }

            // Airfare logistics
            var organizationLogisticAirfares = this.logisticAirfares
                                                        .Where(la => !la.Logistic.AttendeeCollaborator.IsDeleted
                                                                     && la.Logistic.AttendeeCollaborator.AttendeeOrganizationCollaborators
                                                                            .Any(aoc => !aoc.IsDeleted && aoc.AttendeeCollaboratorId == projectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaboratorId))
                                                        .ToList();
            if (organizationLogisticAirfares?.Any() != true)
            {
                return result;
            }

            var organizationLogisticsAirfareExceptions = new List<Tuple<DateTimeOffset, DateTimeOffset, bool>>();
            foreach (var organizationLogisticAirfare in organizationLogisticAirfares)
            {
                organizationLogisticsAirfareExceptions.Add(new Tuple<DateTimeOffset, DateTimeOffset, bool>(organizationLogisticAirfare.DepartureDate, organizationLogisticAirfare.ArrivalDate, organizationLogisticAirfare.IsArrival));
            }

            var logisticSlotsExceptions = negotiationSlots
                                                .Where(ns => organizationLogisticsAirfareExceptions
                                                                .Any(lde => lde.Item3 ? ns.StartDate < lde.Item2.AddHours(4) :   // Arrival
                                                                                        ns.EndDate > lde.Item1.AddHours(-4)))?   // Departure
                                                .Select(e => e.RoundNumber)
                                                .Distinct()
                                                .ToList();

            result.AddRange(logisticSlotsExceptions);

            return result;
        }

        #endregion

        #region Conferences

        /// <summary>Gets the conferences slots exceptions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetConferencesSlotsExceptions(List<MusicBusinessRoundNegotiation> negotiationSlots, MusicBusinessRoundProjectBuyerEvaluation projectBuyerEvaluation)
        {
            List<int> result = new List<int>();

            result.AddRange(this.GetPlayerConferencesSlotsExceptions(negotiationSlots, projectBuyerEvaluation));
            result.AddRange(this.GetProducerConferencesSlotsExceptions(negotiationSlots, projectBuyerEvaluation));

            return result;
        }

        /// <summary>Gets the player conferences slots exceptions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetPlayerConferencesSlotsExceptions(List<MusicBusinessRoundNegotiation> negotiationSlots, MusicBusinessRoundProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            var organizationConferences = this.conferences
                                                    .Where(c => c.ConferenceParticipants
                                                                    .Any(cp => !cp.IsDeleted
                                                                               && !cp.AttendeeCollaborator.IsDeleted
                                                                               && cp.AttendeeCollaborator.AttendeeOrganizationCollaborators
                                                                                        .Any(aoc => !aoc.IsDeleted && aoc.AttendeeOrganizationId == projectBuyerEvaluation.BuyerAttendeeOrganizationId)))
                                                    .ToList();
            if (organizationConferences?.Any() != true)
            {
                return result;
            }

            var organizationConferencesExceptions = new List<Tuple<DateTimeOffset?, DateTimeOffset?>>();
            foreach (var organizationConference in organizationConferences)
            {
                organizationConferencesExceptions.Add(new Tuple<DateTimeOffset?, DateTimeOffset?>(organizationConference.StartDate?.AddMinutes(-30), organizationConference.EndDate?.AddMinutes(30)));
            }

            var conferenceSlotsExceptions = negotiationSlots
                                                .Where(ns => organizationConferencesExceptions
                                                    .Any(lde => (ns.StartDate > lde.Item1 && ns.StartDate < lde.Item2)
                                                                || (ns.EndDate > lde.Item1 && ns.EndDate < lde.Item2)))
                                                .Select(e => e.RoundNumber)
                                                .Distinct()
                                                .ToList();

            result.AddRange(conferenceSlotsExceptions);

            return result;
        }

        /// <summary>Gets the producer conferences slots exceptions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetProducerConferencesSlotsExceptions(List<MusicBusinessRoundNegotiation> negotiationSlots, MusicBusinessRoundProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            var organizationConferences = this.conferences
                                                    .Where(c => c.ConferenceParticipants
                                                                    .Any(cp => !cp.IsDeleted
                                                                               && !cp.AttendeeCollaborator.IsDeleted
                                                                               && cp.AttendeeCollaborator.AttendeeOrganizationCollaborators
                                                                                        .Any(aoc => !aoc.IsDeleted && aoc.AttendeeCollaboratorId == projectBuyerEvaluation.MusicBusinessRoundProject.SellerAttendeeCollaboratorId)))
                                                    .ToList();
            if (organizationConferences?.Any() != true)
            {
                return result;
            }

            var organizationConferencesExceptions = new List<Tuple<DateTimeOffset?, DateTimeOffset?>>();
            foreach (var organizationConference in organizationConferences)
            {
                organizationConferencesExceptions.Add(new Tuple<DateTimeOffset?, DateTimeOffset?>(organizationConference.StartDate?.AddMinutes(-30), organizationConference.EndDate?.AddMinutes(30)));
            }

            var conferenceSlotsExceptions = negotiationSlots
                                            .Where(ns => organizationConferencesExceptions
                                                .Any(lde => (ns.StartDate > lde.Item1 && ns.StartDate < lde.Item2)
                                                            || (ns.EndDate > lde.Item1 && ns.EndDate < lde.Item2)))
                                            .Select(e => e.RoundNumber)
                                            .Distinct()
                                            .ToList();

            result.AddRange(conferenceSlotsExceptions);

            return result;
        }

        #endregion

        #endregion
    }
}