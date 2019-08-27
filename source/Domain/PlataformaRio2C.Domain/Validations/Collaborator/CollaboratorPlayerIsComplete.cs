//using PlataformaRio2C.Domain.Entities.Specifications;
//using PlataformaRio2C.Domain.Validation;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Globalization;
//using System.Threading;

//namespace PlataformaRio2C.Domain.Entities.Validations
//{
//    public class CollaboratorPlayerIsComplete : Validation<Collaborator>
//    {
//        public CollaboratorPlayerIsComplete()
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            base.AddRule(new ValidationRule<Collaborator>(new CollaboratorJobTitleEnIsRequired(), Messages.JobTitleEnIsRequired));
//            base.AddRule(new ValidationRule<Collaborator>(new CollaboratorMiniBioEnIsRequired(), Messages.MiniBioEnIsRequired));
//            base.AddRule(new ValidationRule<Collaborator>(new CollaboratorMustHaveBadge(), Messages.BadgeIsRequired));
//            base.AddRule(new ValidationRule<Collaborator>(new CollaboratorMustHaveAPlayer(), Messages.CollaboratorMustHaveAPlayer));

//            if (currentCulture != null && currentCulture.Name == "pt-BR")
//            {
//                base.AddRule(new ValidationRule<Collaborator>(new CollaboratorJobTitlePtBrIsRequired(), Messages.JobTitlePtBrIsRequired));
//                base.AddRule(new ValidationRule<Collaborator>(new CollaboratorMiniBioPtBrIsRequired(), Messages.MiniBioPtBrIsRequired));
//            }
//        }
//    }
//}
