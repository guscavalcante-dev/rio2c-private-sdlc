using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.ViewModels
{
    public class SpeakerBasicAppViewModel : EntityViewModel<SpeakerBasicAppViewModel, Speaker>, IEntityViewModel<Speaker>
    {
        public int CollaboratorId { get; set; }
        public Collaborator collaborator { get; set; }

        public SpeakerBasicAppViewModel(){ }

        public SpeakerBasicAppViewModel(Speaker entity)
            : base(entity)
        {
            CollaboratorId = entity.CollaboratorId;
        }

        public Speaker MapReverse()
        {
            Speaker entity = new Speaker(collaborator);
            //Collaborator collaborator = new Collaborator(CollaboratorId);

            //entity.CollaboratorId = CollaboratorId;
            //entity.Collaborator = collaborator;

            return entity;
        }

        public Speaker MapReverse(Speaker entity)
        {
            throw new NotImplementedException();
        }
    }
}
