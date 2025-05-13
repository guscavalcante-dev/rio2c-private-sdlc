using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class FinancialReportAppViewModel
    {
        public int QuantitySum { get; set; }
        public double SumOfValue { get; set; }
        public IEnumerable<ItemFinancialReportAppViewModel> Items { get; set; }

        public FinancialReportAppViewModel()
        {

        }

        public FinancialReportAppViewModel(IEnumerable<ItemFinancialReportAppViewModel> items)
        {
            items = items.OrderBy(e => e.Label);
            Items = items;
            SumOfValue = items.Sum(e => e.Value);
            QuantitySum = items.Sum(e => e.Count);
        }
    }
}
