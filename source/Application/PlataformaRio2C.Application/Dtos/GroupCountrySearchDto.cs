using System.Collections.Generic;

namespace PlataformaRio2C.Application.Dtos
{
    public class GroupCountrySearchDto
    {
        public string Label { get; set; }

        public IEnumerable<string> Searches { get; set; }
    }
}
