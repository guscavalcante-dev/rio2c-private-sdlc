using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class MessageTextMaxLength : ISpecification<Message>
    {
        public string Target { get { return "Text"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Message entity)
        {
            return !string.IsNullOrWhiteSpace(entity.Text) && entity.Text.Length <= Message.TextMaxLength;
        }
    }
}
