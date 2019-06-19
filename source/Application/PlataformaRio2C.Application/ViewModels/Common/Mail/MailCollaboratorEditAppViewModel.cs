using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class MailCollaboratorEditAppViewModel : MailAppViewModel
    {
        #region props
        public MailAppViewModel Mail { get; set; }
        public CollaboratorAppViewModel Collaborator { get; set; }


        #endregion

        #region ctor

        public MailCollaboratorEditAppViewModel()
            : base()
        {
            Mail = new MailAppViewModel();
            Collaborator = new CollaboratorAppViewModel();
        }

        public MailCollaboratorEditAppViewModel(MailCollaborator entity)
        {
            if (entity.Mail != null)
            {
                Mail = new MailAppViewModel(entity.Mail);
            }
            else
            {
                Mail = new MailAppViewModel();
            }


            if (entity.Collaborator != null)
            {
                Collaborator = new CollaboratorAppViewModel(entity.Collaborator);
            }
            else
            {
                Collaborator = new CollaboratorAppViewModel();
            }
        }

        #endregion

        #region Public methods       



        #endregion
    }
}
