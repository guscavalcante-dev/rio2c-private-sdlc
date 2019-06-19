using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.Dtos
{
    public class SendToPlayerAppDto
    {
        public Guid[] UidsPlayers { get; set; }
        public Guid[] Uids{ get; set; }

        public SendToPlayerAppDto()
        {

        }
    }
}
