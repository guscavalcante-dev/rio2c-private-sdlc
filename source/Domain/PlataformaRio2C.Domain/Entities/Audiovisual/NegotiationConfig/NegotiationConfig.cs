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

        #endregion

        //public NegotiationConfig(DateTime? date)
        //{
        //    SetDate(date);
        //}


        //public void SetDate(DateTime? val)
        //{
        //    Date = val;
        //}

        //public void SetStartTime(TimeSpan val)
        //{
        //    StartTime = val;
        //}

        //public void SetEndTime(TimeSpan val)
        //{
        //    EndTime = val;
        //}

        //public void SetTimeIntervalBetweenTurn(TimeSpan val)
        //{
        //    TimeIntervalBetweenTurn = val;
        //}

        //public void SetTimeOfEachRound(TimeSpan val)
        //{
        //    TimeOfEachRound = val;
        //}

        //public void SetTimeIntervalBetweenRound(TimeSpan val)
        //{
        //    TimeIntervalBetweenRound = val;
        //}

        //public void SetCountSlotsFirstTurn(int val)
        //{
        //    RoudsFirstTurn = val;
        //}

        //public void SetCountSlotsSecondTurn(int val)
        //{
        //    RoundsSecondTurn = val;
        //}
        //public void SetRooms(ICollection<NegotiationRoomConfig> rooms)
        //{
        //    Rooms = rooms;
        //}

        //public override bool IsValid()
        //{
        //    ValidationResult = new ValidationResult();

        //    ValidationResult.Add(new NegotiationConfigIsConsistent().Valid(this));

        //    if (Rooms != null && Rooms.Any())
        //    {
        //        foreach (var room in Rooms)
        //        {
        //            ValidationResult.Add(new NegotiationRoomConfigIsConsistent().Valid(room));
        //        }
        //    }

        //    return ValidationResult.IsValid;
        //}
    }
}
