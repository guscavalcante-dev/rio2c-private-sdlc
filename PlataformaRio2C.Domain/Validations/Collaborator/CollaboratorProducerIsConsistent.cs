using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Globalization;
using System.Threading;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class CollaboratorProducerIsConsistent : Validation<Collaborator>
    {
        public CollaboratorProducerIsConsistent()
        {
            base.AddRule(new ValidationRule<Collaborator>(new CollaboratorMustHaveAProducer(), Messages.CollaboratorMustHaveAProducer));
        }
    }
}
