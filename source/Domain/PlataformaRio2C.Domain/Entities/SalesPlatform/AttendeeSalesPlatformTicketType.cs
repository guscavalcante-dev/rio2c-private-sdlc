// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-14-2019
// ***********************************************************************
// <copyright file="AttendeeSalesPlatformTicketType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeSalesPlatformTicketType</summary>
    public class AttendeeSalesPlatformTicketType : Entity
    {
        public static readonly int TicketClassIdMinLength = 1;
        public static readonly int TicketClassIdMaxLength = 30;
        public static readonly int TicketClassNameMinLength = 1;
        public static readonly int TicketClassNameMaxLength = 200;

        public int AttendeeSalesPlatformId { get; private set; }
        public int CollaboratorTypeId { get; private set; }
        public string TicketClassId { get; private set; }
        public string TicketClassName { get; private set; }
        public int ProjectMaxCount { get; private set; }
        public int ProjectBuyerEvaluationGroupMaxCount { get; private set; }
        public int ProjectBuyerEvaluationMaxCount { get; private set; }

        public virtual AttendeeSalesPlatform AttendeeSalesPlatform { get; private set; }
        public virtual CollaboratorType CollaboratorType { get; private set; }

        public virtual ICollection<AttendeeCollaboratorTicket> AttendeeCollaboratorTickets { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformTicketType"/> class.</summary>
        protected AttendeeSalesPlatformTicketType()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateTicketClassId();
            this.ValidateTicketClassName();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the ticket class identifier.</summary>
        public void ValidateTicketClassId()
        {
            if (string.IsNullOrEmpty(this.TicketClassId?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Ticket Class Id"), new string[] { "TicketClassId" }));
            }

            if (this.TicketClassId?.Trim().Length < TicketClassIdMinLength || this.TicketClassId?.Trim().Length > TicketClassIdMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Ticket Class Id", TicketClassIdMaxLength, TicketClassIdMinLength), new string[] { "TicketClassId" }));
            }
        }

        /// <summary>Validates the name of the ticket class.</summary>
        public void ValidateTicketClassName()
        {
            if (string.IsNullOrEmpty(this.TicketClassName?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Ticket Class"), new string[] { "TicketClassName" }));
            }

            if (this.TicketClassName?.Trim().Length < TicketClassNameMinLength || this.TicketClassName?.Trim().Length > TicketClassNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Ticket Class", TicketClassNameMaxLength, TicketClassNameMinLength), new string[] { "TicketClassName" }));
            }
        }

        #endregion
    }
}