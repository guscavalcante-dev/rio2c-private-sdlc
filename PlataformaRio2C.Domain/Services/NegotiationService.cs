using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using System.Linq;
using System;

namespace PlataformaRio2C.Domain.Services
{
    public class NegotiationService : Service<Negotiation>, INegotiationService
    {
        public NegotiationService(INegotiationRepository repository)
            :base(repository)
        {

        }

        public override ValidationResult CreateAll(IEnumerable<Negotiation> entities)
        {
            var r = _repository as INegotiationRepository;
            var oldEntities = _repository.GetAll();
            if (oldEntities != null && oldEntities.Any())
            {
                r.Truncate();
            }

            return base.CreateAll(entities);
        }

        public ValidationResult CreateManual(Negotiation entity)
        {
            var existNegotiationInTable = _repository.Get(e => e.Date == entity.Date && e.RoomId == entity.RoomId && e.StarTime == entity.StarTime && e.TableNumber == entity.TableNumber && e.RoundNumber == entity.RoundNumber);
            if (existNegotiationInTable != null)
            {
                var error = new ValidationError(string.Format("Já existe uma negociação na mesa '{0}'.", entity.TableNumber), new string[] { "Table" });
                _validationResult.Add(error);
            }

            return base.Create(entity);
        }
    }
}
