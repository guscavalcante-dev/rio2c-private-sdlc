using System;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Domain.Entities.Validations;
using System.Linq;

namespace PlataformaRio2C.Domain.Services
{
    public class ProducerService : Service<Producer>, IProducerService
    {
        protected readonly ICollaboratorRepository _collaboratorRepository;

        public ProducerService(IProducerRepository repository, IRepositoryFactory repositoryFactory)
            : base(repository)
        {
            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
        }

        public ValidationResult UpdateByPortal(Producer entity)
        {
            _validationResult.Add(new ProducerIsConsistentForPortal().Valid(entity));

            _validationResult.Add(new ImageIsConsistent().Valid(entity.Image));

            //_validationResult.Add(new AddressIsConsistent().Valid(entity.Address));

            _validationResult.Add(new PhoneNumberIsConsistent("PhoneNumber").Valid(entity.PhoneNumber));

            return base.Update(entity);
        }

        public Producer GetByUserIdAndEventId(int userId, int eventId)
        {
            var entityCollaborator = _collaboratorRepository.Get(e => e.Id == userId);
            if (entityCollaborator != null)
            {
                //var entityProducers = entityCollaborator.ProducersEvents.Where(e => e.EventId == eventId).Select(e => e.Producer);
                //if (entityProducers != null)
                //{
                //    return entityProducers.FirstOrDefault();
                //}
            }

            return null;
        }

        public Producer GetImage(Guid uid)
        {
            var r = _repository as IProducerRepository;
            return r.GetImage(uid);
        }
    }
}
