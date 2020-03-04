// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="MusicBand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>NegotiationConfig</summary>
    public class NegotiationConfig : Entity
    {
        public int EditionId { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public int RoundFirstTurn { get; private set; }
        public int RoundSecondTurn { get; private set; }
        public TimeSpan TimeIntervalBetweenTurn { get; private set; }
        public TimeSpan TimeOfEachRound { get; private set; }
        public TimeSpan TimeIntervalBetweenRound { get; private set; }

        public virtual Edition Edition { get; private set; }

        //public virtual ICollection<NegotiationRoomConfig> Rooms { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationConfig"/> class.</summary>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
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
            Guid negotiationConfigUid,
            Edition edition,
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
            //this.Uid = negotiationConfigUid;
            this.EditionId = edition?.Id ?? 0;
            this.Edition = edition;
            this.StartDate = date.JoinDateAndTime(startTime, true).ToUtcTimeZone();
            this.EndDate = date.JoinDateAndTime(endTime, true).ToUtcTimeZone();
            this.RoundFirstTurn = roundFirstTurn;
            this.RoundSecondTurn = roundSecondTurn;
            this.TimeIntervalBetweenTurn = timeIntervalBetweenTurnString.ToTimeSpan();
            this.TimeOfEachRound = timeOfEachRoundString.ToTimeSpan();
            this.TimeIntervalBetweenRound = timeIntervalBetweenRoundString.ToTimeSpan();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="NegotiationConfig"/> class.</summary>
        protected NegotiationConfig()
        {
        }


        //TODO: Implement validations

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateEdition();
            this.ValidateDates();

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
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.Edition.EndDate.ToUserTimeZone().ToShortDateString(), this.Edition.StartDate.ToUserTimeZone().ToShortDateString()), new string[] { "Date" }));
            }

            if (this.StartDate > this.EndDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.EndTime, Labels.StartTime), new string[] { "EndTime" }));
            }
        }

        #endregion
    }
}
