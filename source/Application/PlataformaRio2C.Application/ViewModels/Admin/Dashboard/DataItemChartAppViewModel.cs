namespace PlataformaRio2C.Application.ViewModels
{
    public class DataItemChartAppViewModel
    {
        public string Label { get; set; }

        public string Code { get; set; }

        public int Number { get; set; }

        public DataItemChartAppViewModel()
        {

        }

        public DataItemChartAppViewModel(string label, string code, int number)
        {
            Label = label;
            Code = code;
            Number = number;
        }
    }
}
