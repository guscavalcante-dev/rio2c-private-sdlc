using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class LogisticsIsConsistent : Validation<Logistics>
    {
        public LogisticsIsConsistent()
        {
            base.AddRule(new ValidationRule<Logistics>(new LogisticsCollaboratorIsRequired(), "Colaborador obrigatório."));
            base.AddRule(new ValidationRule<Logistics>(new LogisticsArrivalDateIsRequired(), "Data de chegada obrigatória."));
            base.AddRule(new ValidationRule<Logistics>(new LogisticsArrivalTimeIsRequired(), "Hora de chegada obrigatória."));
            base.AddRule(new ValidationRule<Logistics>(new LogisticsArrivalTimeIsValid(), "Hora de chegada inválida."));

            base.AddRule(new ValidationRule<Logistics>(new LogisticsDepartureDateIsRequired(), "Data de chegada obrigatória."));
            base.AddRule(new ValidationRule<Logistics>(new LogisticsDepartureTimeIsRequired(), "Data de chegada obrigatória."));
            base.AddRule(new ValidationRule<Logistics>(new LogisticsDepartureTimeIsValid(), "Data de chegada inválida."));


            base.AddRule(new ValidationRule<Logistics>(new LogisticsEventIsRequired(), "Evento obrigatório."));
            base.AddRule(new ValidationRule<Logistics>(new LogisticsArrivalDateSmallerDepartureDate(), "A data de chegada deve ser menor que a data de partida."));
        }      
    }
}
