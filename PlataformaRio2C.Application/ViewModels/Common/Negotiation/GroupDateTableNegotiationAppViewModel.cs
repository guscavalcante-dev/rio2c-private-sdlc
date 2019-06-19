using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class GroupDateTableNegotiationAppViewModel
    {
        public IEnumerable<GroupItemDateTableNegotiationAppViewModel> Dates { get; set; }

        public GroupDateTableNegotiationAppViewModel()
        {

        }

        public GroupDateTableNegotiationAppViewModel(IEnumerable<NegotiationAppViewModel> negotiations)
        {
            Dates = negotiations.GroupBy(e => e.Date).Select(e => new GroupItemDateTableNegotiationAppViewModel(e.Key, e.ToList()));
        }
    }   
}
