using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    public class MusicalCommissionItemListAppViewModel : EntityViewModel<MusicalCommissionItemListAppViewModel, MusicalCommission>
    {
        public Collaborator Collaborator { get; set; }
        public int collaboratorId { get; set; }


        public MusicalCommissionItemListAppViewModel()
        {

        }

        public MusicalCommissionItemListAppViewModel(MusicalCommission entity)
        {
            collaboratorId = entity.CollaboratorId;
            Collaborator = entity.Collaborator;
        }

        public MusicalCommission MapReverse()
        {
            return new MusicalCommission(Collaborator);
        }
    }
}
