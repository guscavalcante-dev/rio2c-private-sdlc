using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class GroupTableNegotiationAppViewModel
    {        
        public int Table { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public IEnumerable<NegotiationAppViewModel> Negotiations { get; set; }

        public GroupTableNegotiationAppViewModel()
        {

        }

        public GroupTableNegotiationAppViewModel(int table, IEnumerable<NegotiationAppViewModel> negotiations)
        {
            Table = table;
            StartTime = negotiations.Select(e => e.StarTime).FirstOrDefault();
            EndTime = negotiations.Select(e => e.EndTime).FirstOrDefault();
            Negotiations = negotiations.OrderBy(e => e.Table).ToList();
        }
    }
}
