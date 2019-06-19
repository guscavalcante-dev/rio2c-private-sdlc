using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ProducerTradeNameIsRequired : ISpecification<Producer>
    {
        public string Target { get { return "TradeName"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(Producer entity)
        {
            return !string.IsNullOrWhiteSpace(entity.TradeName);
        }
    }
}
