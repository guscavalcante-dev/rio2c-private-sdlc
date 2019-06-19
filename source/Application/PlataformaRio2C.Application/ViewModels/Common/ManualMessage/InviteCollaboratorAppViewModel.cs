using System.Collections.Generic;

namespace PlataformaRio2C.Application.ViewModels
{
    public class InviteCollaboratorAppViewModel
    {
        public IEnumerable<CollaboratorItemListAppViewModel> Collaborators { get; set; }
        public string  Message { get; set; }

        public string ContentIdImgHeader1 { get; set; }

        public string ContentIdImgFooter1 { get; set; }

        public InviteCollaboratorAppViewModel()
        {

        }
    }
}
