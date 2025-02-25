// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="NegotiationConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>NegotiationConfig</summary>
    public class NegotiationConfig : Entity
    {
        public int EditionId { get; private set; }
        public int ProjectTypeId { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public int RoundFirstTurn { get; private set; }
        public int RoundSecondTurn { get; private set; }
        public TimeSpan TimeIntervalBetweenTurn { get; private set; }
        public TimeSpan TimeOfEachRound { get; private set; }
        public TimeSpan TimeIntervalBetweenRound { get; private set; }
        public virtual ProjectType ProjectType { get; private set; }
        public virtual Edition Edition { get; private set; }
        public virtual ICollection<NegotiationRoomConfig> NegotiationRoomConfigs { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NegotiationConfig" /> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="roundFirstTurn">The round first turn.</param>
        /// <param name="roundSecondTurn">The round second turn.</param>
        /// <param name="timeIntervalBetweenTurnString">The time interval between turn string.</param>
        /// <param name="timeOfEachRoundString">The time of each round string.</param>
        /// <param name="timeIntervalBetweenRoundString">The time interval between round string.</param>
        /// <param name="userId">The user identifier.</param>
        public NegotiationConfig(
            Edition edition,
            DateTime date,
            string startTime,
            string endTime,
            int roundFirstTurn,
            int roundSecondTurn,
            string timeIntervalBetweenTurnString,
            string timeOfEachRoundString,
            string timeIntervalBetweenRoundString,
            int userId,
            int projectTypeId)
        {
            this.EditionId = edition?.Id ?? 0;
            this.Edition = edition;
            this.StartDate = date.JoinDateAndTime(startTime, true).ToUtcTimeZone();
            this.EndDate = date.JoinDateAndTime(endTime, true).ToUtcTimeZone();
            this.RoundFirstTurn = roundFirstTurn;
            this.RoundSecondTurn = roundSecondTurn;
            this.TimeIntervalBetweenTurn = timeIntervalBetweenTurnString.ToTimeSpan();
            this.TimeOfEachRound = timeOfEachRoundString.ToTimeSpan();
            this.TimeIntervalBetweenRound = timeIntervalBetweenRoundString.ToTimeSpan();
            this.ProjectTypeId = projectTypeId;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="NegotiationConfig"/> class.</summary>
        protected NegotiationConfig()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="date">The date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="roundFirstTurn">The round first turn.</param>
        /// <param name="roundSecondTurn">The round second turn.</param>
        /// <param name="timeIntervalBetweenTurnString">The time interval between turn string.</param>
        /// <param name="timeOfEachRoundString">The time of each round string.</param>
        /// <param name="timeIntervalBetweenRoundString">The time interval between round string.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            DateTime date,
            string startTime,
            string endTime,
            int roundFirstTurn,
            int roundSecondTurn,
            string timeIntervalBetweenTurnString,
            string timeOfEachRoundString,
            string timeIntervalBetweenRoundString,
            int userId)
        {
            this.StartDate = date.JoinDateAndTime(startTime, true).ToUtcTimeZone();
            this.EndDate = date.JoinDateAndTime(endTime, true).ToUtcTimeZone();
            this.RoundFirstTurn = roundFirstTurn;
            this.RoundSecondTurn = roundSecondTurn;
            this.TimeIntervalBetweenTurn = timeIntervalBetweenTurnString.ToTimeSpan();
            this.TimeOfEachRound = timeOfEachRoundString.ToTimeSpan();
            this.TimeIntervalBetweenRound = timeIntervalBetweenRoundString.ToTimeSpan();
            
            base.SetUpdateDate(userId);
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
        {
            this.DeleteNegotiationRoomConfigs(userId);

            base.Delete(userId);
        }

        #region Negotiation Room Configs

        /// <summary>Creates the negotiation room configuration.</summary>
        /// <param name="room">The room.</param>
        /// <param name="countAutomaticTables">The count automatic tables.</param>
        /// <param name="countManualTables">The count manual tables.</param>
        /// <param name="userId">The user identifier.</param>
        public void CreateNegotiationRoomConfig(Room room, int countAutomaticTables, int countManualTables, int userId)
        {
            if (this.NegotiationRoomConfigs?.Any() != true)
            {
                this.NegotiationRoomConfigs = new List<NegotiationRoomConfig>();
            }

            this.NegotiationRoomConfigs.Add(new NegotiationRoomConfig(room,this, countAutomaticTables, countManualTables, userId));
        }

        /// <summary>Updates the negotiation room configuration.</summary>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <param name="room">The room.</param>
        /// <param name="countAutomaticTables">The count automatic tables.</param>
        /// <param name="countManualTables">The count manual tables.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateNegotiationRoomConfig(Guid negotiationRoomConfigUid, Room room, int countAutomaticTables, int countManualTables, int userId)
        {
            var negotiationRoomConfig = this.GetNegotiationRoomConfigByUid(negotiationRoomConfigUid);
            negotiationRoomConfig?.Update(room, countAutomaticTables, countManualTables, userId);
        }

        /// <summary>Deletes the negotiation room configuration.</summary>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteNegotiationRoomConfig(Guid negotiationRoomConfigUid, int userId)
        {
            var negotiationRoomConfig = this.GetNegotiationRoomConfigByUid(negotiationRoomConfigUid);
            negotiationRoomConfig?.Delete(userId);
        }

        /// <summary>Gets the negotiation room configuration by uid.</summary>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <returns></returns>
        private NegotiationRoomConfig GetNegotiationRoomConfigByUid(Guid? negotiationRoomConfigUid)
        {
            return this.NegotiationRoomConfigs?.FirstOrDefault(nrc => nrc.Uid == negotiationRoomConfigUid);
        }

        /// <summary>Deletes the negotiation room configs.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteNegotiationRoomConfigs(int userId)
        {
            if (this.NegotiationRoomConfigs?.Any() != true)
            {
                return;
            }

            foreach (var negotiationRoomConfig in this.NegotiationRoomConfigs.Where(c => !c.IsDeleted))
            {
                negotiationRoomConfig.Delete(userId);
            }
        }

        #endregion

        #region Helpers

        /// <summary>Gets the negotiation room configuration position.</summary>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <returns></returns>
        public int? GetNegotiationRoomConfigPosition(Guid negotiationRoomConfigUid)
        {
            return this.NegotiationRoomConfigs?
                .Where(nrc => !nrc.IsDeleted)?
                .Select((n, i) => new { Index = i, NegotiationRoomConfig = n })?
                .Where(item => item.NegotiationRoomConfig.Uid == negotiationRoomConfigUid)?
                .Single()?
                .Index;
        }

        /// <summary>
        /// Gets the maximum automatic slots count by edition.
        /// </summary>
        /// <returns></returns>
        public int GetMaxAutomaticSlotsCountByEdition()
        {
            int tablesTotalCount = this.NegotiationRoomConfigs
                .Where(nrc => !nrc.IsDeleted && !nrc.Room.IsVirtualMeeting)
                .Sum(nrc => nrc.CountAutomaticTables);

            int roundsTotalCount = this.RoundFirstTurn + this.RoundSecondTurn;

            return tablesTotalCount * roundsTotalCount;
        }

        /// <summary>
        /// Gets the maximum manual slots count by edition.
        /// </summary>
        /// <returns></returns>
        public int GetMaxManualSlotsCountByEdition()
        {
            int tablesTotalCount = this.NegotiationRoomConfigs
                .Where(nrc => !nrc.IsDeleted && !nrc.Room.IsVirtualMeeting)
                .Sum(nrc => nrc.CountManualTables);

            int roundsTotalCount = this.RoundFirstTurn + this.RoundSecondTurn;

            return tablesTotalCount * roundsTotalCount;
        }

        /// <summary>
        /// Gets the maximum slots count by player.
        /// </summary>
        /// <param name="includeManualTables">if set to <c>true</c> [include manual tables].</param>
        /// <returns></returns>
        public int GetMaxSlotsCountByPlayer()
        {
            // If this NegotiationConfig has any virtual room associated, cannot calculate.
            // TODO: Maybe this rule needs to be reviewed, because a NegotiationConfig can contains Virtual and Presential rooms associated, and this rule only returns 0 and disconsider the Presential rooms.
            // How to proceed in this cases?
            if(this.NegotiationRoomConfigs.Any(nrc => nrc.Room.IsVirtualMeeting))
            {
                return 0;
            }

            int roundsTotalCount = this.RoundFirstTurn + this.RoundSecondTurn;

            return roundsTotalCount;
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateEdition();
            this.ValidateDates();
            this.ValidateRoundFirstTurn();
            this.ValidateRoundSecondTurn();
            this.ValidateNegotiationRoomConfigs();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Determines whether [is negotiation room configuration valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is negotiation room configuration valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNegotiationRoomConfigValid()
        {
            this.ValidationResult = new ValidationResult();
            this.ValidateNegotiationRoomConfigs();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the edition.</summary>
        public void ValidateEdition()
        {
            if (this.Edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Edition), new string[] { "EditionId" }));
            }
        }

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            if (this.StartDate < this.Edition.StartDate || this.StartDate > this.Edition.EndDate
                || this.EndDate < this.Edition.StartDate || this.EndDate > this.Edition.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.Edition.EndDate.ToBrazilTimeZone().ToShortDateString(), this.Edition.StartDate.ToBrazilTimeZone().ToShortDateString()), new string[] { "Date" }));
            }

            if (this.StartDate > this.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndTime, Labels.StartTime), new string[] { "EndTime" }));
            }
        }

        /// <summary>Validates the round first turn.</summary>
        public void ValidateRoundFirstTurn()
        {
            if (this.RoundFirstTurn < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.RoundFirstTurn, 0), new string[] { "RoundFirstTurn" }));
            }
        }

        /// <summary>Validates the round second turn.</summary>
        public void ValidateRoundSecondTurn()
        {
            if (this.RoundSecondTurn < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.RoundSecondTurn, 0), new string[] { "RoundSecondTurn" }));
            }
        }

        /// <summary>Validates the negotiation room configs.</summary>
        public void ValidateNegotiationRoomConfigs()
        {
            if (this.NegotiationRoomConfigs?.Any() != true)
            {
                return;
            }

            // Check if there is the same room in differente room configs
            if (this.NegotiationRoomConfigs?.Where(nrc => !nrc.IsDeleted)?.GroupBy(nrc => nrc.RoomId)?.Any(r => r.Count() > 1) == true)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityIsAlreadyAction, Labels.TheF + " " + Labels.Room.ToLowerInvariant(), Labels.RegisteredF.ToLowerInvariant()), new string[] { "RoomUid" }));
            }

            foreach (var negotiationRoomConfig in this.NegotiationRoomConfigs?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(negotiationRoomConfig.ValidationResult);
            }
        }

        #endregion
    }
}
