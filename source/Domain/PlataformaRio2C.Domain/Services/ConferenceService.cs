//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System.Linq;
//using System;

//namespace PlataformaRio2C.Domain.Services
//{
//    public class ConferenceService : Service<Conference>, IConferenceService
//    {
//        private readonly IConferenceLecturerRepository _conferenceLecturerRepository;

//        public ConferenceService(IConferenceRepository repository, IRepositoryFactory repositoryFactory)
//            : base(repository)
//        {
//            _conferenceLecturerRepository = repositoryFactory.ConferenceLecturerRepository;
//        }


//        public override ValidationResult Create(Conference entity)
//        {
//            var result = base.Create(entity);

//            if (_validationResult.IsValid)
//            {
//                if (entity.RoomId > 0)
//                {
//                    ValidateRoom(entity);
//                }

//                if (ValidationResult.IsValid && entity.Lecturers != null && entity.Lecturers.Any())
//                {
//                    foreach (var lecturer in entity.Lecturers)
//                    {

//                        if (lecturer.Collaborator != null)
//                        {
//                            ValidateExecutive(entity, lecturer.Collaborator);
//                        }

//                        var ValidationEntity = lecturer as IEntity;
//                        if (ValidationEntity != null && !ValidationEntity.IsValid())
//                            ValidationResult.Add(ValidationEntity.ValidationResult);
//                    }
//                }
//            }

//            return _validationResult;
//        }

//        private void ValidateRoom(Conference entity)
//        {
//            var conferencesWithCommonRoom = _repository.GetAll(e => e.RoomId == entity.RoomId && e.Id != entity.Id && e.Date == entity.Date && ((e.StartTime <= entity.StartTime && entity.StartTime <= e.EndTime) || (e.StartTime <= entity.EndTime && entity.StartTime <= e.EndTime)));

//            if (conferencesWithCommonRoom != null && conferencesWithCommonRoom.Any())
//            {
//                var error = new ValidationError("Já existe palestra acontecendo nesta sala nesse momento.", new string[] { "Room" });
//                _validationResult.Add(error);
//            }
//        }


//        private void ValidateExecutive(Conference entity, Collaborator collaborator)
//        {
//            var executivesWithCommonConference = _conferenceLecturerRepository.GetAll(e => e.ConferenceId != entity.Id && e.CollaboratorId == collaborator.Id && e.Conference.Date == entity.Date && ((e.Conference.StartTime <= entity.StartTime && entity.StartTime <= e.Conference.EndTime) || (e.Conference.StartTime <= entity.EndTime && entity.StartTime <= e.Conference.EndTime)));

//            if (executivesWithCommonConference != null && executivesWithCommonConference.Any())
//            {
//                var error = new ValidationError(string.Format("O executivo {0} já estará participando de uma palestra neste momento.", collaborator.FirstName), new string[] { "Lecturers" });
//                _validationResult.Add(error);
//            }
//        }

//        public ConferenceLecturer GetLecturerImage(Guid uid)
//        {
//            return _conferenceLecturerRepository.GetLecturerImage(uid);
//        }



//        public override ValidationResult Update(Conference entity)
//        {
//            var result = base.Update(entity);

//            if (_validationResult.IsValid)
//            {
//                if (entity.RoomId > 0)
//                {
//                    ValidateRoom(entity);
//                }

//                if (ValidationResult.IsValid && entity.Lecturers != null && entity.Lecturers.Any())
//                {
//                    foreach (var lecturer in entity.Lecturers)
//                    {
//                        if (lecturer.Collaborator != null)
//                        {
//                            ValidateExecutive(entity, lecturer.Collaborator);
//                        }

//                        var ValidationEntity = lecturer as IEntity;
//                        if (ValidationEntity != null && !ValidationEntity.IsValid())
//                            ValidationResult.Add(ValidationEntity.ValidationResult);
//                    }
//                }
//            }

//            return _validationResult;
//        }
//    }
//}
