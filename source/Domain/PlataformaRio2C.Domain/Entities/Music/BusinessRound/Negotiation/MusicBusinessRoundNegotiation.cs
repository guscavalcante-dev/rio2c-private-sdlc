// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 25-02-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 25-02-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundNegotiation.cs" company="Softo">
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
    /// <summary>MusicBusinessRoundNegotiation</summary>
    public class MusicBusinessRoundNegotiation : Entity
    {
        public int MusicBusinessRoundProjectBuyerEvaluationId { get; private set; }
        public int RoomId { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public int TableNumber { get; private set; }
        public int RoundNumber { get; private set; }
        public bool IsAutomatic { get; private set; }
        public int EditionId { get; private set; }

        public virtual MusicBusinessRoundProjectBuyerEvaluation MusicBusinessRoundProjectBuyerEvaluation { get; private set; }
        public virtual Room Room { get; private set; }
        public virtual User Updater { get; private set; }
        public virtual ICollection<AttendeeMusicBusinessRoundNegotiationCollaborator> AttendeeMusicBusinessRoundNegotiationCollaborators { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundNegotiation"/> class.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="room">The room.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="tableNumber">The table number.</param>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBusinessRoundNegotiation(
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
        /// Initializes a new instance of the <see cref="MusicBusinessRoundNegotiation"/> class.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="buyerOrganization">The buyer organization.</param>
        /// <param name="musicBusinessRoundProject">The music business round project.</param>
        /// <param name="negotiationConfig">The negotiation configuration.</param>
        /// <param name="negotiationRoomConfig">The negotiation room configuration.</param>
        /// <param name="musicBusinessRoundNegotiations">The music business round negotiations.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="isUsingAutomaticTable">if set to <c>true</c> [is using automatic table].</param>
        public MusicBusinessRoundNegotiation(
            int editionId,
            Organization buyerOrganization,
            MusicBusinessRoundProject musicBusinessRoundProject,
            NegotiationConfig negotiationConfig,
            NegotiationRoomConfig negotiationRoomConfig,
            List<MusicBusinessRoundNegotiation> musicBusinessRoundNegotiations,
            string startTime,
            int roundNumber,
            int userId,
            string userInterfaceLanguage,
            bool isUsingAutomaticTable)
        {
            this.EditionId = editionId;

            // Project buyer evaluation
            var musicBusinessRoundProjectBuyerEvaluation = musicBusinessRoundProject.MusicBusinessRoundProjectBuyerEvaluations?.FirstOrDefault(pbe => pbe.BuyerAttendeeOrganization.Organization.Uid == buyerOrganization?.Uid && !pbe.IsDeleted);
            this.MusicBusinessRoundProjectBuyerEvaluationId = musicBusinessRoundProjectBuyerEvaluation?.Id ?? 0;
            this.MusicBusinessRoundProjectBuyerEvaluation = musicBusinessRoundProjectBuyerEvaluation;

            // Room
            var room = negotiationRoomConfig?.Room;
            this.RoomId = room?.Id ?? 0;
            this.Room = room;

            // Dates
            this.GenerateStartAndEndDate(negotiationConfig, startTime);

            // Table Number
            this.GenerateTableNumber(negotiationConfig, negotiationRoomConfig, musicBusinessRoundNegotiations, startTime, userInterfaceLanguage, isUsingAutomaticTable);

            this.RoundNumber = roundNumber;
            this.IsAutomatic = isUsingAutomaticTable;

            base.SetCreateDate(userId);
        }

        protected MusicBusinessRoundNegotiation() { }

        public MusicBusinessRoundNegotiation(int value, Organization buyerOrganization, NegotiationConfig negotiationConfig, NegotiationRoomConfig negotiationRoomConfig, List<MusicBusinessRoundNegotiation> negotiationsInThisRoomAndStartDate, string startTime, int v, int userId, string userInterfaceLanguage, bool isUsingAutomaticTable)
        {
        }

        /// <summary>
        /// Updates the specified negotiation configuration.
        /// </summary>
        /// <param name="negotiationConfig">The negotiation configuration.</param>
        /// <param name="negotiationRoomConfig">The negotiation room configuration.</param>
        /// <param name="roomNegotiations">The room negotiations.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="isUsingAutomaticTable">if set to <c>true</c> [is using automatic table].</param>
        public void Update(
            NegotiationConfig negotiationConfig,
            NegotiationRoomConfig negotiationRoomConfig,
            List<MusicBusinessRoundNegotiation> roomNegotiations,
            string startTime,
            int roundNumber,
            int userId,
            string userInterfaceLanguage,
            bool isUsingAutomaticTable)
        {
            var room = negotiationRoomConfig?.Room;
            this.RoomId = room?.Id ?? 0;
            this.Room = room;

            this.GenerateStartAndEndDate(negotiationConfig, startTime);
            this.GenerateTableNumber(negotiationConfig, negotiationRoomConfig, roomNegotiations, startTime, userInterfaceLanguage, isUsingAutomaticTable);

            this.RoundNumber = roundNumber;
            this.IsAutomatic = isUsingAutomaticTable;
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Generates the start and end date.
        /// </summary>
        /// <param name="negotiationConfig">The negotiation configuration.</param>
        /// <param name="startTime">The start time.</param>
        private void GenerateStartAndEndDate(NegotiationConfig negotiationConfig, string startTime)
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
        /// <param name="musicBusinessRoundNegotiations">The negotiations.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="isUsingAutomaticTable">if set to <c>true</c> [is using automatic table].</param>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        private void GenerateTableNumber(
            NegotiationConfig negotiationConfig,
            NegotiationRoomConfig negotiationRoomConfig,
            List<MusicBusinessRoundNegotiation> musicBusinessRoundNegotiations,
            string startTime,
            string userInterfaceLanguage,
            bool isUsingAutomaticTable)
        {
            if (negotiationConfig == null || negotiationRoomConfig == null)
                return;

            if (musicBusinessRoundNegotiations?.Count > 0)
            {
                List<int> tableNumbers = musicBusinessRoundNegotiations.Select(n => n.TableNumber).ToList();
                int smallestTableNumber = isUsingAutomaticTable ? 1 : negotiationRoomConfig.CountAutomaticTables + 1;
                int largestTableNumber = isUsingAutomaticTable ? negotiationRoomConfig.CountAutomaticTables : (negotiationRoomConfig.CountAutomaticTables + negotiationRoomConfig.CountManualTables);
                IEnumerable<int> allTableNumbers = Enumerable.Range(smallestTableNumber, largestTableNumber - smallestTableNumber + 1);
                IEnumerable<int> tableNumbersAvailable = allTableNumbers.Except(tableNumbers);

                if (!tableNumbersAvailable.Any())
                {
                    throw new DomainException(string.Format(Messages.NoMoreTablesAvailableAtTheRoomAndStartTime, startTime, negotiationRoomConfig.Room.GetRoomNameByLanguageCode(userInterfaceLanguage)));
                }
                else
                {
                    this.TableNumber = tableNumbersAvailable.OrderBy(i => i).FirstOrDefault();
                }
            }
            else
            {
                this.TableNumber = negotiationRoomConfig.CountAutomaticTables + (negotiationConfig.GetNegotiationRoomConfigPosition(negotiationRoomConfig.Uid) ?? 0) + 1;
            }
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();
            this.ValidateMusicBusinessRoundProjectBuyerEvaluation();
            this.ValidateRoom();
            this.ValidateDates();
            this.ValidateTableNumber();
            this.ValidateRoundNumber();

            return this.ValidationResult.IsValid;
        }

        #region Privates

        /// <summary>
        /// Validates the project buyer evaluation.
        /// </summary>
        private void ValidateMusicBusinessRoundProjectBuyerEvaluation()
        {
            if (this.MusicBusinessRoundProjectBuyerEvaluation == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Evaluation), new string[] { "MusicBusinessRoundProjectBuyerEvaluation" }));
            }
        }

        /// <summary>
        /// Validates the room.
        /// </summary>
        private void ValidateRoom()
        {
            if (this.Room == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Room), new string[] { "Room" }));
            }
        }

        /// <summary>
        /// Validates the dates.
        /// </summary>
        private void ValidateDates()
        {
            if (this.StartDate > this.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndTime, Labels.StartTime), new string[] { "EndTime" }));
            }
        }

        /// <summary>
        /// Validates the table number.
        /// </summary>
        private void ValidateTableNumber()
        {
            if (this.TableNumber < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.TableNumber, 0), new string[] { "TableNumber" }));
            }
        }

        /// <summary>
        /// Validates the round number.
        /// </summary>
        private void ValidateRoundNumber()
        {
            if (this.RoundNumber < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.RoundNumber, 0), new string[] { "RoundNumber" }));
            }
        }

        #endregion
    }
}
