// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="UserUseTermAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>MessageChatAppViewModel</summary>
    public class MessageChatAppViewModel : EntityViewModel<MessageChatAppViewModel, Message>, IEntityViewModel<Message>
    {
        public static readonly int TextMaxLength = Message.TextMaxLength;

        public string Text { get; set; }
        public bool IsRead { get; set; }
        public string Date { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientName { get; set; }

        public MessageChatAppViewModel()
            :base()
        {

        }

        public MessageChatAppViewModel(Message entity)
            :base(entity)
        {
            Text = entity.Text;
            IsRead = entity.ReadDate.HasValue;


            if (entity.CreateDate != null)
            {
                Date = entity.CreateDate.ToString("dd/MM/yyyy HH:mm");
            }

            if (entity.Sender != null)
            {
                SenderEmail = entity.Sender.Email;
                SenderName = entity.Sender.Name;
            }

            if (entity.Recipient != null)
            {
                RecipientEmail = entity.Recipient.Email;
                RecipientName = entity.Recipient.Name;
            }
        }


        public Message MapReverse()
        {
            var entity = new Message(Text);

            return entity;
        }

        public Message MapReverse(Message entity)
        {
            entity.SetText(Text);
            return entity;
        }
    }
}
