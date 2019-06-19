using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class CollaboratorIsConsistent : Validation<Collaborator>
    {
        public CollaboratorIsConsistent()
        {            
            base.AddRule(new ValidationRule<Collaborator>(new CollaboratorMustHaveName(), Messages.NameIsRequired));
        }
    }
}
