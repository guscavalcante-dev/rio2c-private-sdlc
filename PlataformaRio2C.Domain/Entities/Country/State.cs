using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class State : Entity
    {
        public static readonly int NameLength = 100;
        public static readonly int CodeLength = 2;

        [Key]
        public int Id { get; private set; }
        public string StateName { get; private set; }
        public string StateCode { get; private set; }
        public int CountryId { get; private set; }

        public virtual Country Country { get; private set; }
        public virtual Address Address { get; set; }

        protected State(){}

        public State(string code, string name, int countryId){
            StateName = name;
            StateCode = code;

            CountryId = countryId;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
