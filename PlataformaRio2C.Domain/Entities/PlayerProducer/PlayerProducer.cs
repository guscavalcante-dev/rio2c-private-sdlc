using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class PlayerProducer : Entity
    {
        public int PlayerId { get; set; }
        public int ProducerId { get; set; }
        public int CollaboratorId { get; set; }

        public virtual CollaboratorProducer Producer { get; set; }
        public virtual Collaborator Player { get; set; }

        public PlayerProducer() { }

        public override bool IsValid()
        {
            return true;
        }
    }
}
