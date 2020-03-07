//using PlataformaRio2C.Domain.Entities.Specifications;
//using PlataformaRio2C.Domain.Validation;
//using PlataformaRio2C.Infra.CrossCutting.Resources;

//namespace PlataformaRio2C.Domain.Entities.Validations
//{
//    public class NegotiationIsConsistent : Validation<Negotiation>
//    {
//        public NegotiationIsConsistent()
//        {

//            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationPlayerIsRequired(), "Player é obrigatório."));
//            base.AddRule(new ValidationRule<Negotiation>(new NegotiationProjectIsRequired(), "Projeto é obrigatório."));
//            base.AddRule(new ValidationRule<Negotiation>(new NegotiationDateIsRequired(), "Datá é obrigatória."));
//            base.AddRule(new ValidationRule<Negotiation>(new NegotiationRoomIsRequired(), "Sala é obrigatória."));
//            base.AddRule(new ValidationRule<Negotiation>(new NegotiationStartTimeIsRequired(), "Hora de início é obrigatória."));
//            base.AddRule(new ValidationRule<Negotiation>(new NegotiationTableIsRequired(), "Mesa é obrigatória."));            
//        }

//    }
//}
