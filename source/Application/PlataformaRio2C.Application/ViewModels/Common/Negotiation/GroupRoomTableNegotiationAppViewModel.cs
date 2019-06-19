using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class GroupRoomTableNegotiationAppViewModel
    {
        public string Room { get; set; }
        public IEnumerable<GroupTableNegotiationAppViewModel> Tables { get; set; }

        public IEnumerable<Tuple<string, string, int>> Times { get; set; }

        public GroupRoomTableNegotiationAppViewModel()
        {

        }

        public GroupRoomTableNegotiationAppViewModel(string room, IEnumerable<NegotiationAppViewModel> negotiations)
        {
            Room = room;

            negotiations = negotiations.OrderBy(e => e.Table);            

            Times = negotiations.OrderBy(e => e.Slot).GroupBy(e => e.Slot).Select(e => new Tuple<string, string, int>(e.FirstOrDefault().StarTime, e.FirstOrDefault().EndTime, e.FirstOrDefault().Slot));

            Tables = negotiations.GroupBy(e => e.Table).Select(e => new GroupTableNegotiationAppViewModel(e.Key, e.ToList()));
        }
    }
}
