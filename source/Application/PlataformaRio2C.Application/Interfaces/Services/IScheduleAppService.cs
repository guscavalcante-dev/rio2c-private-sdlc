using PlataformaRio2C.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.Interfaces.Services
{
    public interface IScheduleAppService : IAppService<NegotiationAppViewModel, NegotiationAppViewModel, NegotiationAppViewModel, NegotiationAppViewModel>
    {
        bool ScheduleIsEnable();

        IEnumerable<ScheduleDayAppViewModel> GetSchedulePlayer(int userId);
        IEnumerable<ScheduleDayAppViewModel> GetSchedulePlayer(Guid uidCollaborator);

        IEnumerable<ScheduleDayAppViewModel> GetScheduleProducer(int userId);
        IEnumerable<ScheduleDayAppViewModel> GetScheduleProducer(Guid uidCollaborator);

        IEnumerable<ScheduleDayAppViewModel> GetDays();

        IEnumerable<ScheduleDayAppViewModel> GetComplete(Guid uidCollaborator);
    }
}
