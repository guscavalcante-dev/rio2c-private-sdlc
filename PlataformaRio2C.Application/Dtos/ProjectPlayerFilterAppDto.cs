using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.Dtos
{
    public class ProjectPlayerFilterAppDto
    {
        public Guid[] Players { get; set; }

        public string[] Genres { get; set; }

        public string[] Status { get; set; }
    }
}
