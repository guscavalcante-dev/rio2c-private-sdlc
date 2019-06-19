using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    public class SpeakerItemListAppViewModel : EntityViewModel<SpeakerItemListAppViewModel, Speaker>
    {
        public Collaborator Collaborator { get; set; }
        public int collaboratorId { get; set; }


        public SpeakerItemListAppViewModel()
        {

        }

        public SpeakerItemListAppViewModel(Speaker entity)
        {
            collaboratorId = entity.CollaboratorId;
            Collaborator = entity.Collaborator;
        }

        public Speaker MapReverse()
        {
            return new Speaker(Collaborator);
        }
    }
}
