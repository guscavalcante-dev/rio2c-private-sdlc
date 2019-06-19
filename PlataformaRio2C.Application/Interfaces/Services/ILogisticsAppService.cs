using PlataformaRio2C.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace PlataformaRio2C.Application.Interfaces.Services
{
    public interface ILogisticsAppService : IAppService<LogisticsAppViewModel, LogisticsAppViewModel, LogisticsEditAppViewModel, LogisticsItemListAppViewModel>
    {
        IEnumerable<LogisticsCollaboratorAppViewModel> GetCollaboratorsOptions(string term);

        LogisticsEditAppViewModel GetDefaultOptions(LogisticsEditAppViewModel viewModel);

        AppValidationResult SendEmailToCollaborators(Guid uidsCollaborator, Attachment attachment, string textEmail);
    }
}
