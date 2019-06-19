using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class ConferenceEndTimeIsRequired : ISpecification<Conference>
    {
        public string Target { get { return "EndTime"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(Conference entity)
        {
            return entity.EndTime != null;
        }
    }
}
