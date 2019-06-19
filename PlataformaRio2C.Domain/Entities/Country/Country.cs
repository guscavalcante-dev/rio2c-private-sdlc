using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    public class Country : Entity
    {
        public static readonly int NameLength = 100;
        public static readonly int CodeLength = 3;

        [Key]
        public int Id { get; set; }
        public string CountryName { get; private set; }
        public string CountryCode { get; private set; }

        public virtual Address Address { get; set; }

        protected Country() { }

        public Country(string countryCode, string countryName)
        {
            setName(countryName);
            setCode(countryCode);
        }

        public Country(string countryName)
        {
            setName(countryName);
        }


        public void setCode(string code)
        {
            CountryCode = code;
        }

        public void setName(string name)
        {
            CountryName = name;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
