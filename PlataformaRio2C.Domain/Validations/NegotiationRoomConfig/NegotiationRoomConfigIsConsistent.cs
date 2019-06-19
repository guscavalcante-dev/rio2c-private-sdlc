using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class NegotiationRoomConfigIsConsistent : Validation<NegotiationRoomConfig>
    {
        public NegotiationRoomConfigIsConsistent()
        {
            //base.AddRule(new ValidationRule<NegotiationRoomConfig>(new NegotiationRoomConfigRoomIsRequired(), "Sala é obrigatória."));
            base.AddRule(new ValidationRule<NegotiationRoomConfig>(new NegotiationRoomConfigCountAutomaticTablesIsInvalid(), "Número de mesas automáticas inválido"));            
        }

    }
}
