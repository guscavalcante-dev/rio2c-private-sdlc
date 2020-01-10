//using PlataformaRio2C.Domain.Entities.Validations;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Domain.Validation;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Domain.Entities
//{
//    public class Speaker : Entity
//    {
//        [Key]
//        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }
//        public int CollaboratorId { get; set; }

//        public virtual Collaborator Collaborator { get; set; }


//        public Speaker(){}

//        public Speaker(Collaborator collaborator)
//        {
//            CollaboratorId = collaborator.Id;
//            //Uid = Guid.NewGuid();
//            //CreationDate = DateTime.Now;

//            Collaborator = collaborator;
//        }

//        public override bool IsValid()
//        {
//            return true;
//        }
//    }
//}
