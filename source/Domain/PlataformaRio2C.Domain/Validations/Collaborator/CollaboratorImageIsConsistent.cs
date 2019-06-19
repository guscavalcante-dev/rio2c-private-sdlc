using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class CollaboratorImageIsConsistent : Validation<Collaborator>
    {
        public CollaboratorImageIsConsistent()
        {
            base.AddRule(new ValidationRule<Collaborator>(new CollaboratorResolutionFoto(), Messages.InvalidResolution));            
        }
    }
}
