using PlataformaRio2C.Application.ViewModels;

namespace PlataformaRio2C.Application.Interfaces.Services
{
    public interface ISpeakerAppService : IAppService<SpeakerBasicAppViewModel, SpeakerDetailAppViewModel, CollaboratorEditAppViewModel, SpeakerItemListAppViewModel>
    {
    }
}