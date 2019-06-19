using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class City : Entity
    {
        public static readonly int NameLength = 100;

        [Key]
        public int Id{ get; private set; }
        public string Name { get; private set; }
        public int StateId{ get; private set; }

        public virtual State State { get; private set; }
        public virtual Address Address { get; set; }


        protected City() {}

        public City(string name, int stateId)
        {
            Name = name;

            StateId = stateId;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}