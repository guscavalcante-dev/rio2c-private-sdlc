// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-31-2025
// ***********************************************************************
// <copyright file="Negotiation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Negotiation</summary>
    public class Negotiation : Entity
    {
        public int ProjectBuyerEvaluationId { get; private set; }
        public int RoomId { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public int TableNumber { get; private set; }
        public int RoundNumber { get; private set; }
        public bool IsAutomatic { get; private set; }
        public int EditionId { get; private set; }

        public virtual ProjectBuyerEvaluation ProjectBuyerEvaluation { get; private set; }
        public virtual Room Room { get; private set; }
        public virtual User Updater { get; private set; }
        public virtual ICollection<AttendeeNegotiationCollaborator> AttendeeNegotiationCollaborators { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Negotiation"/> class for automatic negotiation slots.</summary>
        /// <param name="room">The room.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="tableNumber">The table number.</param>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="userId">The user identifier.</param>
        public Negotiation(
            int editionId,
            Room room,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            int tableNumber,
            int roundNumber,
            int userId)
        {
            this.Uid = Guid.NewGuid();
            this.EditionId = editionId;
            this.RoomId = room?.Id ?? 0;
            this.Room = room;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.TableNumber = tableNumber;
            this.RoundNumber = roundNumber;
            this.IsAutomatic = true;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Negotiation" /> class for manual negotiations.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="buyerOrganization">The buyer organization.</param>
        /// <param name="project">The project.</param>
        /// <param name="negotiationConfig">The negotiation configuration.</param>
        /// <param name="negotiationRoomConfig">The negotiation room configuration.</param>
        /// <param name="negotiations">The negotiations.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="isUsingAutomaticTable">if set to <c>true</c> [is using automatic table].</param>
        public Negotiation(
            int editionId,
            Organization buyerOrganization,
            Project project,
            NegotiationConfig negotiationConfig,
            NegotiationRoomConfig negotiationRoomConfig,
            List<Negotiation> negotiations,
            string startTime,
            int roundNumber,
            int userId,
            string userInterfaceLanguage,
            bool isUsingAutomaticTable)
        {
            this.EditionId = editionId;

            // Project buyer evaluation
            var projectBuyerEvaluation = project.ProjectBuyerEvaluations?.FirstOrDefault(pbe => pbe.BuyerAttendeeOrganization.Organization.Uid == buyerOrganization?.Uid && !pbe.IsDeleted);
            this.ProjectBuyerEvaluationId = projectBuyerEvaluation?.Id ?? 0;
            this.ProjectBuyerEvaluation = projectBuyerEvaluation;

            // Room
            var room = negotiationRoomConfig?.Room;
            this.RoomId = room?.Id ?? 0;
            this.Room = room;

            // Dates
            this.GenerateStartAndEndDate(negotiationConfig, startTime);

            // Table Number
            this.GenerateTableNumber(negotiationConfig, negotiationRoomConfig, negotiations, startTime, userInterfaceLanguage, isUsingAutomaticTable);

            this.RoundNumber = roundNumber;
            this.IsAutomatic = isUsingAutomaticTable;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Negotiation"/> class.
        /// </summary>
        protected Negotiation()
        {
        }

        /// <summary>
        /// Updates the specified room.
        /// </summary>
        /// <param name="negotiationConfig">The negotiation configuration.</param>
        /// <param name="negotiationRoomConfig">The negotiation room configuration.</param>
        /// <param name="roomNegotiations">The room negotiations.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void Update(
            NegotiationConfig negotiationConfig,
            NegotiationRoomConfig negotiationRoomConfig,
            List<Negotiation> roomNegotiations,
            string startTime,
            int roundNumber,
            int userId,
            string userInterfaceLanguage,
            bool isUsingAutomaticTable)
        {
            // Room
            var room = negotiationRoomConfig?.Room;
            this.RoomId = room?.Id ?? 0;
            this.Room = room;

            // Dates
            this.GenerateStartAndEndDate(negotiationConfig, startTime);

            // Table Number
            this.GenerateTableNumber(negotiationConfig, negotiationRoomConfig, roomNegotiations, startTime, userInterfaceLanguage, isUsingAutomaticTable);

            this.RoundNumber = roundNumber;
            this.IsAutomatic = isUsingAutomaticTable;
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Generates the star and end date.
        /// </summary>
        /// <param name="negotiationConfig">The negotiation configuration.</param>
        /// <param name="startTime">The start time.</param>
        private void GenerateStartAndEndDate(
            NegotiationConfig negotiationConfig,
            string startTime)
        {
            if (negotiationConfig == null)
                return;

            this.StartDate = negotiationConfig.StartDate.Date.JoinDateAndTime(startTime, true).ToUtcTimeZone();
            this.EndDate = this.StartDate.Add(negotiationConfig.TimeOfEachRound);
        }

        /// <summary>
        /// Generates the table number.
        /// </summary>
        /// <param name="negotiationConfig">The negotiation configuration.</param>
        /// <param name="negotiationRoomConfig">The negotiation room configuration.</param>
        /// <param name="negotiations">The room negotiations.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="isUsingAutomaticTable">if set to <c>true</c> [is using automatic table].</param>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        private void GenerateTableNumber(
            NegotiationConfig negotiationConfig,
            NegotiationRoomConfig negotiationRoomConfig,
            List<Negotiation> negotiations,
            string startTime,
            string userInterfaceLanguage,
            bool isUsingAutomaticTable)
        {
            if (negotiationConfig == null)
                return;
            if (negotiationRoomConfig == null)
                return;

            if (negotiations?.Count > 0)
            {
                //this.TableNumber = negotiations.Max(n => n.TableNumber) + 1;
                List<int> tableNumbers = negotiations.Select(n => n.TableNumber).ToList();
                int smallestTableNumber = isUsingAutomaticTable ? 1 : negotiationRoomConfig.CountAutomaticTables + 1;
                int largestTableNumber = isUsingAutomaticTable ? negotiationRoomConfig.CountAutomaticTables : (negotiationRoomConfig.CountAutomaticTables + negotiationRoomConfig.CountManualTables);
                IEnumerable<int> allTableNumbers = Enumerable.Range(smallestTableNumber, largestTableNumber - smallestTableNumber + 1);
                IEnumerable<int> tableNumbersAvailable = allTableNumbers.Except(tableNumbers);

                if (tableNumbersAvailable.Count() == 0)
                {
                    throw new DomainException(string.Format(Messages.NoMoreTablesAvailableAtTheRoomAndStartTime, startTime, negotiationRoomConfig.Room.GetRoomNameByLanguageCode(userInterfaceLanguage)));
                }
                else
                {
                    // Get the next available table number
                    this.TableNumber = tableNumbersAvailable.OrderBy(i => i).FirstOrDefault();
                }
            }
            else
            {
                this.TableNumber = negotiationRoomConfig.CountAutomaticTables + (negotiationConfig.GetNegotiationRoomConfigPosition(negotiationRoomConfig.Uid) ?? 0) + 1;
            }
        }

        /// <summary>Assigns the project buyer evaluation.</summary>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        public void AssignProjectBuyerEvaluation(ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            this.ProjectBuyerEvaluationId = projectBuyerEvaluation?.Id ?? 0;
            this.ProjectBuyerEvaluation = projectBuyerEvaluation;
        }

        /// <summary>
        /// Disables the slot.
        /// </summary>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <param name="isDeleted">if set to <c>true</c> [is deleted].</param>
        public void DisableSlot(ProjectBuyerEvaluation projectBuyerEvaluation, bool isDeleted)
        {
            this.ProjectBuyerEvaluationId = projectBuyerEvaluation.Id;
            this.ProjectBuyerEvaluation = projectBuyerEvaluation;
            this.IsDeleted = isDeleted;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateProjectBuyerEvaluation();
            this.ValidateRoom();
            this.ValidateDates();
            this.ValidateTableNumber();
            this.ValidateRoundNumber();
            //this.ValidateNegotiationRoomConfigs();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the project buyer evaluation.</summary>
        public void ValidateProjectBuyerEvaluation()
        {
            if (this.ProjectBuyerEvaluation == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Evaluation), new string[] { "ProjectBuyerEvaluationId" }));
            }
        }

        /// <summary>Validates the room.</summary>
        public void ValidateRoom()
        {
            if (this.Room == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Room), new string[] { "Room" }));
            }
        }

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            //if (this.StartDate < this.Edition.StartDate || this.StartDate > this.Edition.EndDate
            //    || this.EndDate < this.Edition.StartDate || this.EndDate > this.Edition.EndDate)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.Edition.EndDate.ToBrazilTimeZone().ToShortDateString(), this.Edition.StartDate.ToBrazilTimeZone().ToShortDateString()), new string[] { "Date" }));
            //}

            if (this.StartDate > this.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndTime, Labels.StartTime), new string[] { "EndTime" }));
            }
        }

        /// <summary>Validates the table number.</summary>
        public void ValidateTableNumber()
        {
            if (this.TableNumber < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.TableNumber, 0), new string[] { "TableNumber" }));
            }
        }

        /// <summary>Validates the round number.</summary>
        public void ValidateRoundNumber()
        {
            if (this.RoundNumber < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.RoundNumber, 0), new string[] { "RoundNumber" }));
            }
        }

        ///// <summary>Validates the negotiation room configs.</summary>
        //public void ValidateNegotiationRoomConfigs()
        //{
        //    if (this.NegotiationRoomConfigs?.Any() != true)
        //    {
        //        return;
        //    }

        //    foreach (var negotiationRoomConfig in this.NegotiationRoomConfigs?.Where(d => !d.IsValid())?.ToList())
        //    {
        //        this.ValidationResult.Add(negotiationRoomConfig.ValidationResult);
        //    }
        //}

        #endregion
    }
}