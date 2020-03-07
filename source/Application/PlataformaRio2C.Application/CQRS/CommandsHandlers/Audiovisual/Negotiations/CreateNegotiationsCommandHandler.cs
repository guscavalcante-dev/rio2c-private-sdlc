// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="CreateNegotiationsCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateNegotiationsCommandHandler</summary>
    public class CreateNegotiationsCommandHandler : NegotiationBaseCommandHandler, IRequestHandler<CreateNegotiations, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly INegotiationConfigRepository negotiationConfigRepo;
        private readonly IProjectBuyerEvaluationRepository projectBuyerEvaluationRepo;

        private IList<Negotiation> _negociations = new List<Negotiation>();
        //private IList<NegotiationConfig> _negotiationConfigs;
        private IList<Logistics> _logistics;
        private IList<Conference> _conferences;

        private IList<ProjectBuyerEvaluation> _projectSubmissionsError = new List<ProjectBuyerEvaluation>();

        /// <summary>Initializes a new instance of the <see cref="CreateNegotiationsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="negotiationConfigRepository">The negotiation configuration repository.</param>
        /// <param name="projectBuyerEvaluationRepository">The project buyer evaluation repository.</param>
        public CreateNegotiationsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            INegotiationRepository negotiationRepository,
            IEditionRepository editionRepository,
            INegotiationConfigRepository negotiationConfigRepository,
            IProjectBuyerEvaluationRepository projectBuyerEvaluationRepository)
            : base(eventBus, uow, negotiationRepository)
        {
            this.editionRepo = editionRepository;
            this.negotiationConfigRepo = negotiationConfigRepository;
            this.projectBuyerEvaluationRepo = projectBuyerEvaluationRepository;
        }

        /// <summary>Handles the specified create negotiations.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateNegotiations cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var negotiationConfigs = await this.negotiationConfigRepo.FindAllForGenerateNegotiationsAsync();
            var negotiationSlots = new List<Negotiation>();
            var negotiationsFilled = new List<Negotiation>();

            if (negotiationConfigs?.Any() == true)
            {
                negotiationSlots = this.GetNegotiationSlots(negotiationConfigs, cmd.UserId);

                var projectBuyerEvaluations = await this.projectBuyerEvaluationRepo.FindAllForGenerateNegotiationsAsync(cmd.EditionId ?? 0);

                if (projectBuyerEvaluations?.Any() == true)
                {
                    //_logistics = _logisticsRepository.GetAll().ToList();
                    //_conferences = _conferenceRepository.GetAllBySchedule().ToList();

                    this.FillNegotiationSlots(negotiationSlots, projectBuyerEvaluations);
                }
            }

            //var t = negotiationSlots.Where(e => e.Evaluation != null).ToList();

            _negociations = negotiationSlots
                                .Where(ns => ns.ProjectBuyerEvaluation != null)
                                .ToList();

            //var oldEntities = this.NegotiationRepo.FindAll();
            //if (oldEntities?.Any() == true)
            //{
                this.NegotiationRepo.Truncate();
            //}

            this.NegotiationRepo.CreateAll(_negociations);

            var edition = await this.editionRepo.FindByUidAsync(cmd.EditionUid ?? Guid.Empty, true);
            edition?.CreateAudiovisualNegotiations(cmd.UserId);

            //this.NegotiationConfigRepo.Create(negotiationConfig);
            this.Uow.SaveChanges();
            //this.AppValidationResult.Data = negotiationConfig;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);
            //return Task.FromResult(propertyId); // use it when the methed is not async
        }

        /// <summary>Gets the negotiation slots.</summary>
        /// <param name="negotiationConfigs">The negotiation configs.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private List<Negotiation> GetNegotiationSlots(List<NegotiationConfig> negotiationConfigs,int userId)
        {
            var negotiationSlots = new List<Negotiation>();
            var numberSlot = 1;

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
                            for (int iTable = 0; iTable < negotiationRoomConfig.CountAutomaticTables; iTable++)
                            {
                                negotiationSlots.Add(this.CreateNegotiationSlot(negotiationConfig, negotiationRoomConfig, numberSlot, iTable, startDate, NegotiationTypeCodes.Automatic, userId));
                            }
                        }
                    }

                    startDate = startDate.Add(negotiationConfig.TimeOfEachRound.Add(negotiationConfig.TimeIntervalBetweenRound));
                    numberSlot++;
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
                            for (int iTable = 0; iTable < negotiationRoomConfig.CountAutomaticTables; iTable++)
                            {
                                negotiationSlots.Add(this.CreateNegotiationSlot(negotiationConfig, negotiationRoomConfig, numberSlot, iTable, startDate, NegotiationTypeCodes.Automatic, userId));
                            }
                        }
                    }

                    startDate = startDate.Add(negotiationConfig.TimeOfEachRound.Add(negotiationConfig.TimeIntervalBetweenRound));
                    numberSlot++;
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
        /// <param name="currentTime">The current time.</param>
        /// <param name="type">The type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private Negotiation CreateNegotiationSlot(
            NegotiationConfig dateConfig, 
            NegotiationRoomConfig roomConfig, 
            int numberSlot, 
            int iTable, 
            DateTimeOffset currentTime, 
            NegotiationTypeCodes type, 
            int userId)
        {
            return new Negotiation(
                roomConfig.Room,
                dateConfig.StartDate,
                dateConfig.StartDate.Add(dateConfig.TimeOfEachRound),
                iTable + 1,
                numberSlot,
                userId);
            //var negociation = new Negotiation(dateConfig.StartDate);
            //negociation.SetRoom(roomConfig.Room);
            //negociation.SetSlotNumber(numberSlot);
            //negociation.SetTable(iTable + 1);
            //negociation.SetStarTime(currentTime);
            //negociation.SetEndTime(currentTime.Add(dateConfig.TimeOfEachRound));
            //return negociation;
        }

        /// <summary>Processes the project submissions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluations">The project buyer evaluations.</param>
        private void FillNegotiationSlots(List<Negotiation> negotiationSlots, List<ProjectBuyerEvaluation> projectBuyerEvaluations)
        {
            projectBuyerEvaluations = this.ProcessProjectSubmissionsByAvailability(negotiationSlots, projectBuyerEvaluations);

            var projectBuyerEvaluationsGroupedByPlayer = projectBuyerEvaluations.GroupBy(e => e.BuyerAttendeeOrganizationId);
            //var reservationsGroupSlot = negotiationSlots.GroupBy(e => e.RoundNumber);

            foreach (var playerProjectBuyerEvaluations in projectBuyerEvaluationsGroupedByPlayer)
            {
                int currentSlot = 1;

                foreach (var playerProjectBuyerEvaluation in playerProjectBuyerEvaluations.ToList())
                {
                    var slotsExpection = this.GetSlotExceptions(negotiationSlots, playerProjectBuyerEvaluation);
                    //var negociation = negotiationSlots.FirstOrDefault(e => e.Evaluation == null && e.ProjectId != itemSub.ProjectId && e.PlayerId != itemSub.PlayerId && !slotsExpection.Contains(e.RoundNumber));
                    //var possiblesNegociation = negotiationSlots.Where(ns => ns.ProjectBuyerEvaluation == null && ns.ProjectId != itemSub.ProjectId && ns.PlayerId != itemSub.PlayerId && !slotsExpection.Contains(ns.RoundNumber));
                    var possibleNegotiationSlots = negotiationSlots?
                                                        .Where(ns => (ns.ProjectBuyerEvaluation == null || ns.ProjectBuyerEvaluation.Id != playerProjectBuyerEvaluation.Id) 
                                                                     && !slotsExpection.Contains(ns.RoundNumber))?
                                                        .ToList();

                    if (possibleNegotiationSlots?.Any() == true)
                    {
                        var dateTest = possibleNegotiationSlots
                                            .Select(e => e.StartDate.Date)
                                            .FirstOrDefault();

                        var negotiationsInDate = negotiationSlots
                                                    .FirstOrDefault(ns => ns.StartDate.Date == dateTest && ns.ProjectBuyerEvaluationId == playerProjectBuyerEvaluation.Id);
                        if (negotiationsInDate != null)
                        {
                            var negotiation = possibleNegotiationSlots.
                                                    FirstOrDefault(e => e.TableNumber == negotiationsInDate.TableNumber && e.RoomId == negotiationsInDate.RoomId);

                            if (negotiation != null)
                            {
                                negotiation.AssignProjectBuyerEvaluation(playerProjectBuyerEvaluation);
                            }
                            else
                            {
                                _projectSubmissionsError.Add(playerProjectBuyerEvaluation);
                            }
                        }
                        else
                        {
                            var negotiation = possibleNegotiationSlots.FirstOrDefault();
                            if (negotiation != null)
                            {
                                negotiation.AssignProjectBuyerEvaluation(playerProjectBuyerEvaluation);
                            }
                            else
                            {
                                _projectSubmissionsError.Add(playerProjectBuyerEvaluation);
                            }
                        }
                    }
                    else
                    {
                        _projectSubmissionsError.Add(playerProjectBuyerEvaluation);
                    }

                    //if (negociation != null)
                    //{
                    //    negociation.SetPlayer(itemSub.Player);
                    //    negociation.SetProject(itemSub.Project);
                    //    negociation.SetSourceEvaluation(itemSub.Evaluation);
                    //}
                    //else
                    //{
                    //    _projectSubmissionsError.Add(itemSub);
                    //}

                    currentSlot++;
                }
            }
        }

        /// <summary>Processes the project submissions by availability.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="projectBuyerEvaluations">The project submissions.</param>
        /// <returns></returns>
        private List<ProjectBuyerEvaluation> ProcessProjectSubmissionsByAvailability(List<Negotiation> negotiationSlots, List<ProjectBuyerEvaluation> projectBuyerEvaluations)
        {
            var projectBuyerEvaluationStotExceptions = new List<Tuple<ProjectBuyerEvaluation, int>>();

            if (projectBuyerEvaluations?.Any() == true)
            {
                foreach (var projectBuyerEvaluation in projectBuyerEvaluations)
                {
                    var slotExceptions = this.GetSlotExceptions(negotiationSlots, projectBuyerEvaluation);
                    projectBuyerEvaluationStotExceptions.Add(new Tuple<ProjectBuyerEvaluation, int>(projectBuyerEvaluation, slotExceptions.Count));
                }

                return projectBuyerEvaluationStotExceptions
                            .GroupBy(e => e.Item1.BuyerAttendeeOrganizationId)
                            .OrderByDescending(e => e.Count())
                            .ThenByDescending(e => e.First().Item2)
                            .SelectMany(e => e.ToList())
                            .Select(e => e.Item1)
                            .ToList();
            }

            return new List<ProjectBuyerEvaluation>();
        }

        #region Slot exceptions

        /// <summary>Gets the slot exceptions.</summary>
        /// <param name="negotiationSlots">The negotiation slots.</param>
        /// <param name="playerProjectBuyerEvaluation">The player project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetSlotExceptions(List<Negotiation> negotiationSlots, ProjectBuyerEvaluation playerProjectBuyerEvaluation)
        {
            var result = new List<int>();

            var playerSlotExceptions = negotiationSlots
                                                    .Where(e => e.ProjectBuyerEvaluationId == playerProjectBuyerEvaluation.Id 
                                                                || e.ProjectBuyerEvaluation?.ProjectId == playerProjectBuyerEvaluation.ProjectId 
                                                                || e.ProjectBuyerEvaluation?.Project?.SellerAttendeeOrganizationId == playerProjectBuyerEvaluation.Project.SellerAttendeeOrganizationId)
                                                    .Select(e => e.RoundNumber)
                                                    .Distinct()
                                                    .ToList();

            result.AddRange(playerSlotExceptions);
            result.AddRange(this.GetLogisticsSlotsExceptions(negotiationSlots, playerProjectBuyerEvaluation));
            result.AddRange(this.GetConferencesSlotsExceptions(negotiationSlots, playerProjectBuyerEvaluation));

            return result;
        }

        #region Logistics

        /// <summary>Gets the logistics slots exceptions.</summary>
        /// <param name="reservationForNegotiation">The reservation for negotiation.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetLogisticsSlotsExceptions(List<Negotiation> reservationForNegotiation, ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            result.AddRange(this.GetPlayerLogisticsSlotsExpection(reservationForNegotiation, projectBuyerEvaluation));
            result.AddRange(this.GetProducerLogisticsSlotsExpection(reservationForNegotiation, projectBuyerEvaluation));

            return result;
        }

        /// <summary>Gets the player logistics slots expection.</summary>
        /// <param name="reservationForNegotiation">The reservation for negotiation.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetPlayerLogisticsSlotsExpection(List<Negotiation> reservationForNegotiation, ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            ////logistic
            //var logisticsPlayers = _logistics.Where(e => (e.Collaborator.Players.Any(p => p.Id == projectBuyerEvaluation.PlayerId)));
            //if (logisticsPlayers != null && logisticsPlayers.Any())
            //{
            //    List<Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>> dateTimesLogistics = new List<Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>>();

            //    foreach (var logistic in logisticsPlayers)
            //    {
            //        dateTimesLogistics.Add(new Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>(logistic.ArrivalDate, logistic.ArrivalTime.Value.Add(TimeSpan.FromHours(4)), logistic.DepartureDate, logistic.DepartureTime.Value.Add(TimeSpan.FromHours(-4))));
            //    }



            //    var slotsExpectionByLogistic = reservationForNegotiation
            //        .Where(reserva => dateTimesLogistics
            //            .Any(logistica =>
            //                (reserva.Date < logistica.Item1) || (reserva.Date == logistica.Item1 && reserva.StarTime < logistica.Item2)
            //                                                 || (reserva.Date > logistica.Item3) || (reserva.Date == logistica.Item3 && reserva.EndTime > logistica.Item4)
            //            )
            //        ).Select(e => e.RoundNumber).Distinct().ToList();

            //    result.AddRange(slotsExpectionByLogistic);
            //}

            return result;
        }

        /// <summary>Gets the producer logistics slots expection.</summary>
        /// <param name="reservationForNegotiation">The reservation for negotiation.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetProducerLogisticsSlotsExpection(List<Negotiation> reservationForNegotiation, ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            //if (submission.Project != null && submission.Project.Producer != null)
            //{
            //    //logistic
            //    var logisticsProducers = _logistics.Where(e => (e.Collaborator.ProducersEvents.Any() && e.Collaborator.ProducersEvents.Select(p => p.ProducerId).Any(p => p == submission.Project.ProducerId)));
            //    if (logisticsProducers != null && logisticsProducers.Any())
            //    {
            //        List<Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>> dateTimesLogistics = new List<Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>>();

            //        foreach (var logistic in logisticsProducers)
            //        {
            //            dateTimesLogistics.Add(new Tuple<DateTime?, TimeSpan, DateTime?, TimeSpan>(logistic.ArrivalDate, logistic.ArrivalTime.Value.Add(TimeSpan.FromHours(4)), logistic.DepartureDate, logistic.DepartureTime.Value.Add(TimeSpan.FromHours(-4))));
            //        }

            //        var slotsExpectionByLogistic = reservationForNegotiation
            //                                      .Where(reserva => dateTimesLogistics
            //                                                      .Any(logistica =>
            //                                                      (reserva.Date < logistica.Item1) || (reserva.Date == logistica.Item1 && reserva.StarTime < logistica.Item2)
            //                                                      || (reserva.Date > logistica.Item3) || (reserva.Date == logistica.Item3 && reserva.EndTime > logistica.Item4)
            //                                                      )
            //                                             ).Select(e => e.RoundNumber).Distinct().ToList();

            //        result.AddRange(slotsExpectionByLogistic);
            //    }
            //}

            return result;
        }

        #endregion

        #region Conferences

        /// <summary>Gets the conferences slots exceptions.</summary>
        /// <param name="reservationForNegotiation">The reservation for negotiation.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetConferencesSlotsExceptions(List<Negotiation> reservationForNegotiation, ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            List<int> result = new List<int>();

            result.AddRange(this.GetPlayerConferencesSlotsExceptions(reservationForNegotiation, projectBuyerEvaluation));
            result.AddRange(this.GetProducerConferencesSlotsExceptions(reservationForNegotiation, projectBuyerEvaluation));

            return result;
        }

        /// <summary>Gets the player conferences slots exceptions.</summary>
        /// <param name="reservationForNegotiation">The reservation for negotiation.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetPlayerConferencesSlotsExceptions(List<Negotiation> reservationForNegotiation, ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            //var conferencePlayers = _conferences.Where(e => (e.Lecturers.Where(l => l.Collaborator != null).SelectMany(l => l.Collaborator.Players).Any(p => p.Id == projectBuyerEvaluation.PlayerId)));
            //if (conferencePlayers != null && conferencePlayers.Any())
            //{
            //    List<Tuple<DateTime?, TimeSpan, TimeSpan>> dateTimes = new List<Tuple<DateTime?, TimeSpan, TimeSpan>>();

            //    foreach (var conference in conferencePlayers)
            //    {
            //        dateTimes.Add(new Tuple<DateTime?, TimeSpan, TimeSpan>(conference.Date, conference.StartTime.Value.Add(TimeSpan.FromMinutes(-30)), conference.EndTime.Value.Add(TimeSpan.FromMinutes(30))));
            //    }

            //    var slotsExpectionByConference = reservationForNegotiation
            //                                           .Where(r => dateTimes
            //                                                           .Any(c => (
            //                                                                        c.Item1 == r.Date &&
            //                                                                        (
            //                                                                            (r.StarTime > c.Item2 && r.StarTime < c.Item3) ||
            //                                                                            (r.EndTime < c.Item3 && r.EndTime > c.Item2)
            //                                                                         )

            //                                                                      )
            //                                                                )
            //                                                  ).Select(e => e.RoundNumber).Distinct().ToList();

            //    result.AddRange(slotsExpectionByConference);
            //}

            return result;
        }

        /// <summary>Gets the producer conferences slots exceptions.</summary>
        /// <param name="reservationForNegotiation">The reservation for negotiation.</param>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        private List<int> GetProducerConferencesSlotsExceptions(List<Negotiation> reservationForNegotiation, ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var result = new List<int>();

            //var conferenceProducer = _conferences.Where(e => (e.Lecturers.Where(l => l.Collaborator != null).SelectMany(l => l.Collaborator.ProducersEvents.Select(p => p.Producer)).Any(p => p.Id == projectBuyerEvaluation.Project.ProducerId)));
            //if (conferenceProducer != null && conferenceProducer.Any())
            //{
            //    List<Tuple<DateTime?, TimeSpan, TimeSpan>> dateTimes = new List<Tuple<DateTime?, TimeSpan, TimeSpan>>();

            //    foreach (var conference in conferenceProducer)
            //    {
            //        dateTimes.Add(new Tuple<DateTime?, TimeSpan, TimeSpan>(conference.Date, conference.StartTime.Value.Add(TimeSpan.FromMinutes(-30)), conference.EndTime.Value.Add(TimeSpan.FromMinutes(30))));
            //    }

            //    var slotsExpectionByConference = reservationForNegotiation
            //                                           .Where(r => dateTimes
            //                                                           .Any(c => (
            //                                                                        c.Item1 == r.Date &&
            //                                                                        (
            //                                                                            (r.StarTime > c.Item2 && r.StarTime < c.Item3) ||
            //                                                                            (r.EndTime < c.Item3 && r.EndTime > c.Item2)
            //                                                                         )

            //                                                                      )
            //                                                                )
            //                                                  ).Select(e => e.RoundNumber).Distinct().ToList();

            //    result.AddRange(slotsExpectionByConference);
            //}

            return result;
        }

        #endregion

        #endregion
    }
}