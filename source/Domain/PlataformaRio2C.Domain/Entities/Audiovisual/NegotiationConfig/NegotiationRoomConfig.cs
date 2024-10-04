// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-03-2024
// ***********************************************************************
// <copyright file="NegotiationRoomConfig.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>NegotiationRoomConfig</summary>
    public class NegotiationRoomConfig : Entity
    {
        public int RoomId { get; private set; }
        public int NegotiationConfigId { get; private set; }
        public int CountAutomaticTables { get; private set; }
        public int CountManualTables { get; private set; }

        public virtual Room Room { get; private set; }
        public virtual NegotiationConfig NegotiationConfig { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationRoomConfig"/> class.</summary>
        /// <param name="room">The room.</param>
        /// <param name="negotiationConfig">The negotiation configuration.</param>
        /// <param name="countAutomaticTables">The count automatic tables.</param>
        /// <param name="countManualTables">The count manual tables.</param>
        /// <param name="userId">The user identifier.</param>
        public NegotiationRoomConfig(
            Room room,
            NegotiationConfig negotiationConfig,
            int countAutomaticTables,
            int countManualTables,
            int userId)
        {
            this.RoomId = room?.Id ?? 0;
            this.Room = room;
            this.NegotiationConfigId = negotiationConfig?.Id ?? 0;
            this.NegotiationConfig = negotiationConfig;
            this.CountAutomaticTables = countAutomaticTables;
            this.CountManualTables = countManualTables;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="NegotiationRoomConfig"/> class.</summary>
        protected NegotiationRoomConfig()
        {
        }

        /// <summary>Updates the specified room.</summary>
        /// <param name="room">The room.</param>
        /// <param name="countAutomaticTables">The count automatic tables.</param>
        /// <param name="countManualTables">The count manual tables.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(Room room, int countAutomaticTables, int countManualTables, int userId)
        {
            this.RoomId = room?.Id ?? 0;
            this.Room = room;
            this.CountAutomaticTables = countAutomaticTables;
            this.CountManualTables = countManualTables;

            base.SetUpdateDate(userId);
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateRoom();
            this.ValidateNegotiationConfig();
            this.ValidateCountAutomaticTables();
            this.ValidateCountManualTables();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the room.</summary>
        public void ValidateRoom()
        {
            if (this.Room == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Room), new string[] { "RoomId" }));
            }
        }

        /// <summary>Validates the negotiation configuration.</summary>
        public void ValidateNegotiationConfig()
        {
            if (this.NegotiationConfig == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Parameter), new string[] { "NegotiationConfigId" }));
            }
        }

        /// <summary>Validates the count automatic tables.</summary>
        public void ValidateCountAutomaticTables()
        {
            if (this.CountAutomaticTables < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.CountAutomaticTables, 0), new string[] { "CountAutomaticTables" }));
            }
        }

        /// <summary>Validates the count manual tables.</summary>
        public void ValidateCountManualTables()
        {
            if (this.CountManualTables < 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.CountManualTables, 0), new string[] { "CountManualTables" }));
            }
        }

        #endregion
    }
}
