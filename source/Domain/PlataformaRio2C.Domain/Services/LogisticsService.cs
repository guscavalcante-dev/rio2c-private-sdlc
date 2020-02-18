//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class LogisticsService : Service<Logistics>, ILogisticsService
//    {
//        public LogisticsService(ILogisticsRepository repository)
//            : base(repository)
//        {

//        }

//        public override ValidationResult Create(Logistics entity)
//        {
//            if (entity.CollaboratorId > 0)
//            {
//                var entityExistByCollaborator = _repository.Get(e => e.CollaboratorId == entity.CollaboratorId && e.EventId == entity.EventId);
//                if (entityExistByCollaborator != null)
//                {
//                    var error = new ValidationError(string.Format("Já existe uma logística para o colaborador '{0}'.", entity.Collaborator.FirstName), new string[] { "CollaboratorUid" });
//                    _validationResult.Add(error);
//                }
//            }

//            return base.Create(entity);
//        }

//        public override ValidationResult Update(Logistics entity)
//        {
//            if (entity.CollaboratorId > 0)
//            {
//                var entityExistByCollaborator = _repository.Get(e => e.CollaboratorId == entity.CollaboratorId && e.EventId == entity.EventId && e.Uid != entity.Uid);
//                if (entityExistByCollaborator != null)
//                {
//                    var error = new ValidationError(string.Format("Já existe uma logística para o colaborador '{0}'.", entity.Collaborator.FirstName), new string[] { "CollaboratorUid" });
//                    _validationResult.Add(error);
//                }
//            }

//            return base.Update(entity);
//        }
//    }
//}
