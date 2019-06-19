using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    public class Message : Entity
    {
        public static readonly int TextMaxLength = 1200;

        public string Text { get; private set; }
        public bool IsRead { get; private set; }
        public int SenderId { get; private set; }
        public virtual User Sender { get; private set; }
        public int RecipientId { get; private set; }
        public virtual User Recipient { get; private set; }

        protected Message() { }

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
            IsRead = val;
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

        public override bool IsValid()
        {            
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new MessageIsConsistent().Valid(this));

            return ValidationResult.IsValid;
        }
    }
}
