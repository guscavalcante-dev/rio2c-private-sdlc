//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class GroupDateNegotiationAppViewModel
//    {
//        public IEnumerable<GroupItemDateNegotiationAppViewModel> Dates { get; set; }

//        public GroupDateNegotiationAppViewModel()
//        {

//        }

//        public GroupDateNegotiationAppViewModel(IEnumerable<NegotiationAppViewModel> negotiations)
//        {
//            Dates = negotiations.GroupBy(e => e.Date).Select(e => new GroupItemDateNegotiationAppViewModel(e.Key, e.ToList()));
//        }
//    }   
//}
