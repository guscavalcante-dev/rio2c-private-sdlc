// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-14-2020
// ***********************************************************************
// <copyright file="Message.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Message</summary>
    public class Message : Entity
    {
        public static readonly int TextMinLength = 1;
        public static readonly int TextMaxLength = 1200;

        public int EditionId { get; private set; }
        public int SenderId { get; private set; }
        public int RecipientId { get; private set; }
        public string Text { get; private set; }
        public DateTimeOffset SendDate { get; private set; }
        public DateTimeOffset? ReadDate { get; private set; }
        public DateTimeOffset? NotificationEmailSendDate { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual User Sender { get; private set; }
        public virtual User Recipient { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Message"/> class.</summary>
        /// <param name="messageUid">The message uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="recipient">The recipient.</param>
        /// <param name="text">The text.</param>
        public Message(Guid messageUid, Edition edition, User sender, User recipient, string text)
        {
            this.Uid = messageUid;
            this.EditionId = edition?.Id ?? 0;
            this.Edition = edition;
            this.SenderId = sender?.Id ?? 0;
            this.Sender = sender;
            this.RecipientId = recipient?.Id ?? 0;
            this.Recipient = recipient;
            this.Text = text?.Trim();

            this.SendDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="Message"/> class.</summary>
        protected Message()
        {
        }

        /// <summary>Reads this instance.</summary>
        public void Read()
        {
            if (this.ReadDate.HasValue)
            {
                return;
            }

            this.ReadDate = DateTime.UtcNow;
        }

        /// <summary>Sends the notification email.</summary>
        public void SendNotificationEmail()
        {
            if (this.NotificationEmailSendDate.HasValue)
            {
                return;
            }

            this.NotificationEmailSendDate = DateTime.UtcNow;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            this.ValidateText();

            return ValidationResult.IsValid;
        }

        /// <summary>Validates the text.</summary>
        public void ValidateText()
        {
            if (string.IsNullOrEmpty(this.Text?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Message), new string[] { "Text" }));
            }

            if (this.Text?.Trim().Length < TextMinLength || this.Text?.Trim().Length > TextMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Message, TextMaxLength, TextMinLength), new string[] { "Text" }));
            }
        }

        #endregion

        #region Old Methods

        public Message(string text)
        {
            SetText(text);
        }

        public void SetText(string text)
        {
            Text = text;
        }

        public void SetIsRead(bool val)
        {
            //IsRead = val;
        }

        public void SetSender(User sender)
        {
            Sender = sender;
            if (sender != null)
            {
                SenderId = sender.Id;
            }
        }

        public void SetRecipient(User recipient)
        {
            Recipient = recipient;
            if (recipient != null)
            {
                RecipientId = recipient.Id;
            }
        }

        #endregion
    }
}
