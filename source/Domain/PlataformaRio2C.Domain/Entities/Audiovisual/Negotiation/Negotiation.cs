// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="Negotiation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using PlataformaRio2C.Infra.CrossCutting.Resources;

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

        public virtual ProjectBuyerEvaluation ProjectBuyerEvaluation { get; private set; }
        public virtual Room Room { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Negotiation"/> class for automatic negotiation slots.</summary>
        /// <param name="room">The room.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="tableNumber">The table number.</param>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="userId">The user identifier.</param>
        public Negotiation(
            Room room,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            int tableNumber,
            int roundNumber,
            int userId)
        {
            this.Uid = Guid.NewGuid();
            this.RoomId = room?.Id ?? 0;
            this.Room = room;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.TableNumber = tableNumber;
            this.RoundNumber = roundNumber;
            this.IsAutomatic = true;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Negotiation"/> class.</summary>
        protected Negotiation()
        {
        }

        /// <summary>Assigns the project buyer evaluation.</summary>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        public void AssignProjectBuyerEvaluation(ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            this.ProjectBuyerEvaluationId = projectBuyerEvaluation?.Id ?? 0;
            this.ProjectBuyerEvaluation = projectBuyerEvaluation;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
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
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenDates, Labels.Date, this.Edition.EndDate.ToUserTimeZone().ToShortDateString(), this.Edition.StartDate.ToUserTimeZone().ToShortDateString()), new string[] { "Date" }));
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