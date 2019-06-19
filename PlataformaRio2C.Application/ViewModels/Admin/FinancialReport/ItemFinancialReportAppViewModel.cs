using PlataformaRio2C.Application.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ItemFinancialReportAppViewModel
    {
        public string Label { get; set; }

        public int Count { get; set; }

        public double ValueUnit { get; set; }

        public double Value { get; set; }


        public ItemFinancialReportAppViewModel()
        {

        }

        public ItemFinancialReportAppViewModel(string label, int count, double value, double valueUnit)
        {
            Label = label;
            Count = count;
            Value = value;
            ValueUnit = valueUnit;
        }
       

        public static IEnumerable<ItemFinancialReportAppViewModel> MapList(IEnumerable<IGrouping<string, ParticipantSympla>> groupParticipants)
        {
            foreach (var itemGroup in groupParticipants)
            {
                yield return new ItemFinancialReportAppViewModel(itemGroup.Key, itemGroup.Count(), itemGroup.Sum(e => e.order_total_sale_price), itemGroup.Select(e => e.ticket_sale_price).FirstOrDefault());
            }
        }
    }
}
