using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Application.Interfaces.Services
{
    public interface ICollaboratorPlayerAppService : IAppService<CollaboratorBasicAppViewModel, CollaboratorDetailAppViewModel, CollaboratorPlayerEditAppViewModel, CollaboratorPlayerItemListAppViewModel>
    {
        AppValidationResult UpdateByPortal(CollaboratorPlayerEditAppViewModel viewModel);
        void MapEntity(ref Collaborator entity, CollaboratorPlayerEditAppViewModel viewModel);

        ImageFileAppViewModel GetThumbImage(Guid uid);
        ImageFileAppViewModel GetImage(Guid uid);
    }   
}
