using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class GroupSlotNegotiationAppViewModel
    {
        public int Slot { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public IEnumerable<NegotiationAppViewModel> Tables { get; set; }

        public GroupSlotNegotiationAppViewModel()
        {

        }

        public GroupSlotNegotiationAppViewModel(int slot, IEnumerable<NegotiationAppViewModel> negotiations)
        {
            Slot = slot;
            StartTime = negotiations.Select(e => e.StarTime).FirstOrDefault();
            EndTime = negotiations.Select(e => e.EndTime).FirstOrDefault();
            Tables = negotiations.OrderBy(e => e.Table).ToList();
        }
    }
}
