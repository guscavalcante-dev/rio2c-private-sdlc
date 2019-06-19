using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Application.ViewModels
{
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
            IsRead = entity.IsRead;


            if (entity.CreationDate != null)
            {
                Date = entity.CreationDate.ToString("dd/MM/yyyy HH:mm");
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
