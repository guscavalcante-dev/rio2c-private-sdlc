//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class MusicalCommissionBasicAppViewModel : EntityViewModel<MusicalCommissionBasicAppViewModel, MusicalCommission>, IEntityViewModel<MusicalCommission>
//    {
//        public int CollaboratorId { get; set; }
//        public Collaborator collaborator { get; set; }

//        public MusicalCommissionBasicAppViewModel(){ }

//        public MusicalCommissionBasicAppViewModel(MusicalCommission entity)
//            : base(entity)
//        {
//            CollaboratorId = entity.CollaboratorId;
//        }

//        public MusicalCommission MapReverse()
//        {
//            MusicalCommission entity = new MusicalCommission(collaborator);
//            //Collaborator collaborator = new Collaborator(CollaboratorId);

//            //entity.CollaboratorId = CollaboratorId;
//            //entity.Collaborator = collaborator;

//            return entity;
//        }

//        public MusicalCommission MapReverse(MusicalCommission entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
