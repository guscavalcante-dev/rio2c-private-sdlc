using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.Dtos
{
    public class GroupCountryDto
    {
        public string Key { get; set; }
        public int Count { get; set; }

        public GroupCountryDto()
        {

        }

        public GroupCountryDto(string key, int count)
        {
            Key = key;
            Count = count;
        }
    }
}
