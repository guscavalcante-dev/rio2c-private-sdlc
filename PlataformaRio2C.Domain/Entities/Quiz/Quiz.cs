using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class Quiz : Entity
    {
        public int EventId { get; private set; }
        public string Name { get; private set; }

        public virtual Event Event { get; private set; }
        public virtual ICollection<QuizQuestion> Question { get; set; }

        protected Quiz() { }

        public Quiz(int eventId, string name)
        {
            this.EventId = eventId;
            this.Name = name;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
