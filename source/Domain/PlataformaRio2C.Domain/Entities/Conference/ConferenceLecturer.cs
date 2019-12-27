//namespace PlataformaRio2C.Domain.Entities
//{
//    public class ConferenceLecturer : Entity
//    {       

//        public bool IsPreRegistered { get; private set; }

//        public int ConferenceId { get; private set; }
//        public virtual Conference Conference { get; private set; }

//        public int? CollaboratorId { get; private set; }
//        public virtual Collaborator Collaborator { get; private set; }

//        public int? LecturerId { get; private set; }
//        public virtual Lecturer Lecturer { get; private set; }

//        public int? RoleLecturerId { get; private set; }
//        public virtual RoleLecturer RoleLecturer { get; private set; }      


//        protected ConferenceLecturer() { }

//        public ConferenceLecturer(Collaborator collaborator)
//        {
//            SetCollaborator(collaborator);
//        }

//        public ConferenceLecturer(Lecturer lecturer)
//        {
//            SetLecturer(lecturer);
//        }

//        public ConferenceLecturer(bool isPreRegistered)
//        {
//            SetIsPreRegistered(isPreRegistered);
//        }

//        public void SetIsPreRegistered(bool isPreRegistered)
//        {
//            IsPreRegistered = isPreRegistered;
//        }       

//        public void SetCollaborator(Collaborator collaborator)
//        {
//            Collaborator = collaborator;
//            if (collaborator != null)
//            {
//                CollaboratorId = collaborator.Id;
//            }
//        }

//        public void SetRoleLecturer(RoleLecturer roleLecturer)
//        {
//            RoleLecturer = roleLecturer;
//            if (roleLecturer != null)
//            {
//                RoleLecturerId = roleLecturer.Id;
//            }
//        }

//        public void SetLecturer(Lecturer lecturer)
//        {
//            Lecturer = lecturer;
//            if (lecturer != null)
//            {
//                LecturerId = lecturer.Id;
//            }
//        }

//        public void SetConference(Conference conference)
//        {
//            Conference = conference;
//            if (Conference != null)
//            {
//                ConferenceId = conference.Id;
//            }
//        }        

//        public override bool IsValid()
//        {
//            return true;
//        }
//    }
//}
