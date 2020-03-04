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
        }
    }
}
