using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using System.Linq;

namespace PlataformaRio2C.Domain.Services
{
    public class CollaboratorPlayerService : CollaboratorService, ICollaboratorPlayerService
    {
        public CollaboratorPlayerService(ICollaboratorRepository repository, IRepositoryFactory repositoryFactory)
            :base(repository, repositoryFactory)
        {

        }

        public override IEnumerable<Collaborator> GetAll(bool @readonly = false)
        {
            return base.GetAll(@readonly).Where(c => c.Players.Any());
        }

        public override ValidationResult Create(Collaborator entity)
        {
            _validationResult.Add(new CollaboratorPlayerIsConsistent().Valid(entity));
            return base.Create(entity);
        }

        public override ValidationResult Update(Collaborator entity)
        {
            _validationResult.Add(new CollaboratorPlayerIsConsistent().Valid(entity));
            return base.Update(entity);
        }

        public ValidationResult UpdateByPortal(Collaborator entity)
        {
            _validationResult.Add(new CollaboratorPlayerIsComplete().Valid(entity));

            //_validationResult.Add(new AddressIsConsistent().Valid(entity.Address));

            _validationResult.Add(new ImageIsConsistent().Valid(entity.Image));

            _validationResult.Add(new PhoneNumberIsConsistent("PhoneNumber").Valid(entity.PhoneNumber));

            _validationResult.Add(new PhoneNumberIsConsistent("CellPhone").Valid(entity.CellPhone));
            return base.Update(entity);
        }
    }
}
