// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="CollaboratorProducerService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Domain.Services
{
    /// <summary>CollaboratorProducerService</summary>
    public class CollaboratorProducerService : CollaboratorService, ICollaboratorProducerService
    {        
        private readonly IProducerRepository _producerRepository;
        private readonly ICollaboratorProducerRepository _collaboratorProducerRepository;

        public CollaboratorProducerService(ICollaboratorRepository repository, IRepositoryFactory repositoryFactory)
           : base(repository, repositoryFactory)
        {
            _producerRepository = repositoryFactory.ProducerRepository;
            _collaboratorProducerRepository = repositoryFactory.CollaboratorProducerRepository;
        }

        public override IEnumerable<Collaborator> GetAll(bool @readonly = false)
        {
            return _collaboratorProducerRepository.GetAll(@readonly).ToList().Select(e => e.Collaborator);
        }

        public ValidationResult PreRegister(string email, string company, string collaboratorName, Edition eventEntity)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            bool isCreate = false;
            Collaborator entityCollaborator = _repository.Get(e => e.User.Email == email);

            if (entityCollaborator == null)
            {
                isCreate = true;
                var userEntity = new User(email);
                userEntity.SetName(collaboratorName);

                userEntity.Roles = new List<Role>() { _roleRepository.Get(e => e.Name == "Producer") };

                entityCollaborator = new Collaborator(collaboratorName, userEntity);
            }
            else
            {
                entityCollaborator.User.Roles.Add(_roleRepository.Get(e => e.Name == "Producer"));
            }

            Producer entityProducer = null;
            if (currentCulture != null && currentCulture.Name == "pt-BR")
            {
                entityProducer = _producerRepository.GetByCnpj(company);
                if (entityProducer == null)
                {
                    entityProducer = new Producer(company);

                    _producerRepository.Create(entityProducer);
                }
            }
            else
            {
                entityProducer = _producerRepository.GetByName(company);
                if (entityProducer == null)
                {
                    entityProducer = new Producer(null);
                    entityProducer.SetName(company);

                    _producerRepository.Create(entityProducer);
                }
            }           

            if (entityCollaborator.ProducersEvents == null || !entityCollaborator.ProducersEvents.Any())
            {
                entityCollaborator.AddEventsProducers(new CollaboratorProducer
                {
                    CollaboratorId = entityCollaborator.Id,
                    ProducerId = entityProducer.Id,
                    EventId = eventEntity.Id
                });
            }

            return isCreate ? base.Create(entityCollaborator) : base.Update(entityCollaborator);
        }


        public ValidationResult UpdateByPortal(Collaborator entity)
        {
            _validationResult.Add(new CollaboratorProducerIsComplete().Valid(entity));

            //_validationResult.Add(new AddressIsConsistent().Valid(entity.Address));

            _validationResult.Add(new ImageIsConsistent().Valid(entity.Image));

            //_validationResult.Add(new PhoneNumberIsConsistent("PhoneNumber").Valid(entity.PhoneNumber));

            _validationResult.Add(new PhoneNumberIsConsistent("CellPhone").Valid(entity.CellPhone));
            return base.Update(entity);
        }
    }
}
