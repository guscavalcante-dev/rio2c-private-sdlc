using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class GroupItemDateNegotiationAppViewModel
    {
        public string Date { get; set; }

        public IEnumerable<GroupRoomNegotiationAppViewModel> Rooms { get; set; }

        public GroupItemDateNegotiationAppViewModel()
        {

        }

        public GroupItemDateNegotiationAppViewModel(string date, IEnumerable<NegotiationAppViewModel> negotiations)
        {
            Date = date;

            negotiations = negotiations.OrderBy(e => e.Room);

            Rooms = negotiations.GroupBy(e => e.Room).Select(e => new GroupRoomNegotiationAppViewModel(e.Key, e.ToList()));
        }
    }   
}
