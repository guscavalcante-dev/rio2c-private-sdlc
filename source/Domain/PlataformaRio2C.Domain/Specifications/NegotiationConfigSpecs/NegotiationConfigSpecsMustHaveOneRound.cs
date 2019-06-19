using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class NegotiationConfigSpecsMustHaveOneRound : ISpecification<NegotiationConfig>
    {
        public string Target { get { return "CountSlotsFirstTurn"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }

        public bool IsSatisfiedBy(NegotiationConfig entity)
        {
            return (entity.RoudsFirstTurn > 0 || entity.RoundsSecondTurn > 0); 
        }
    }
}
