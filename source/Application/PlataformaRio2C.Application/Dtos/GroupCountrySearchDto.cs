using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.Dtos
{
    public class GroupCountrySearchDto
    {
        public string Label { get; set; }

        public IEnumerable<string> Searches { get; set; }
    }
}
