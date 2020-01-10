//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class GroupRoomNegotiationAppViewModel
//    {
//        public string Room { get; set; }
//        public IEnumerable<GroupSlotNegotiationAppViewModel> Slots { get; set; }

//        public GroupRoomNegotiationAppViewModel()
//        {

//        }

//        public GroupRoomNegotiationAppViewModel(string room, IEnumerable<NegotiationAppViewModel> negotiations)
//        {
//            Room = room;

//            Slots = negotiations.GroupBy(e => e.Slot).Select(e => new GroupSlotNegotiationAppViewModel(e.Key, e.ToList()));
//        }
//    }
//}
