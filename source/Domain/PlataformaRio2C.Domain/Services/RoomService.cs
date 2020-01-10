//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class RoomService : Service<Room>, IRoomService
//    {
//        private readonly IConferenceRepository _conferenceRepository;

//        public RoomService(IRoomRepository repository, IRepositoryFactory repositoryFactory)
//            :base(repository)
//        {
//            _conferenceRepository = repositoryFactory.ConferenceRepository;
//        }

//        public override ValidationResult Delete(Room entity)
//        {
//            var entitiesConferences = _conferenceRepository.GetAllSimple(e => e.RoomId == entity.Id);
//            if (entitiesConferences != null && entitiesConferences.Any())
//            {
//                var error = new ValidationError(string.Format("Existem '{0}' palestra(s) associada(s) a sala '{1}'.", entitiesConferences.Count(), entity.GetName()), new string[] { "" });
//                _validationResult.Add(error);                
//            }
           

//            return base.Delete(entity);
//        }
//    }
//}
