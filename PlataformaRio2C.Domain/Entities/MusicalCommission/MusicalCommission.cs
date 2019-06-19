using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicalCommission : Entity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CollaboratorId { get; set; }

        public Collaborator Collaborator { get; set; }


        public MusicalCommission() { }

        public MusicalCommission(Collaborator collaborator)
        {
            CollaboratorId = collaborator.Id;

            Collaborator = collaborator;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
