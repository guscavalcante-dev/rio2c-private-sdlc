using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ConferenceLecturerItemListAppViewModel : EntityViewModel<ConferenceLecturerItemListAppViewModel, ConferenceLecturer>
    { 
        public bool IsPreRegistered { get; set; }

        public string Name { get; set; }

        public CollaboratorOptionAppViewModel Collaborator { get; set; }

        public ConferenceLecturerItemListAppViewModel()
            :base()
        {            
        }

        public ConferenceLecturerItemListAppViewModel(ConferenceLecturer entity)
            :base(entity)
        {
            IsPreRegistered = entity.IsPreRegistered;            

            if (entity.Collaborator != null)
            {
                Collaborator = new CollaboratorOptionAppViewModel() { Name = entity.Collaborator.Name, Uid = entity.Collaborator.Uid};
                Name = Collaborator.Name;
            }

            if (entity.Lecturer != null)
            {
                Name = entity.Lecturer.Name;
            }
        }       
    }
}
