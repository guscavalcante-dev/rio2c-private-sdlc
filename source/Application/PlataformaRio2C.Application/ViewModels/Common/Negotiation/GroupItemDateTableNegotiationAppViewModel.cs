//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class GroupItemDateTableNegotiationAppViewModel
//    {
//        public string Date { get; set; }

//        public IEnumerable<GroupRoomTableNegotiationAppViewModel> Rooms { get; set; }

//        public GroupItemDateTableNegotiationAppViewModel()
//        {

//        }

//        public GroupItemDateTableNegotiationAppViewModel(string date, IEnumerable<NegotiationAppViewModel> negotiations)
//        {
//            Date = date;

//            negotiations = negotiations.OrderBy(e => e.Room);

//            Rooms = negotiations.GroupBy(e => e.Room).Select(e => new GroupRoomTableNegotiationAppViewModel(e.Key, e.ToList()));
//        }
//    }   
//}
