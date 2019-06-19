using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class NegotiationConfigStartTimeSmallerEndTime : ISpecification<NegotiationConfig>
    {
        public string Target { get { return "StartTime"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(NegotiationConfig entity)
        {
            return entity.StartTime <= entity.EndTime;
        }
    }
}
