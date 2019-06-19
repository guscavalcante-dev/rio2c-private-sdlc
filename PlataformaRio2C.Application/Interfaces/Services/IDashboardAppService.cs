using PlataformaRio2C.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.Interfaces.Services
{
    public interface IDashboardAppService
    {
        int GetTotalHolding();
        int GetTotalPlayer();
        int GetTotalProducer();
        int GetTotalProjects();
        IEnumerable<DataItemChartAppViewModel> GetProjetcsSubmissions();
        IEnumerable<DataItemChartAppViewModel> GetProjetcChart();

        IEnumerable<DataItemChartAppViewModel> GetCountryPlayer();
        IEnumerable<DataItemChartAppViewModel> GetCountryProducer();

        


    }
}
