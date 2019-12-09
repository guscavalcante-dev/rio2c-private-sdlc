using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    public class SpeakerAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Speaker, SpeakerBasicAppViewModel, SpeakerDetailAppViewModel, SpeakerEditAppViewModel, SpeakerItemListAppViewModel>, ISpeakerAppService
    {
        private readonly ICollaboratorAppService _collaboratorService;
        private readonly ISpeakerService _service;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly ISpeakerRepository _speakerRepository;

        public SpeakerAppService(ICollaboratorAppService CollaboratorService, ISpeakerService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, IdentityAutenticationService identityController)
            : base(unitOfWork, service)
        {
            _service = service;
            _collaboratorService = CollaboratorService;
            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
            _speakerRepository = repositoryFactory.SpeakerRepository;
        }

        public AppValidationResult Create(CollaboratorEditAppViewModel entity)
        {
            BeginTransaction();

            var resultCollaborator = _collaboratorService.Create(entity);
            var resultSpeaker = ValidationResult;

            if (resultCollaborator.IsValid)
            {
                //Commit();

                var collaborator = _collaboratorRepository.Get(a => a.User.Email == entity.User.Email);

                int id = _service.GetAll().ToList().Count()+1;
                Speaker speaker = new Speaker(collaborator);
                //speaker.Id = id;
                SpeakerEditAppViewModel vm = new SpeakerEditAppViewModel(speaker);
                vm.collaborator = speaker.Collaborator;

                //resultSpeaker = base.Create(vm);
                _speakerRepository.Create(speaker);

                if (resultSpeaker.IsValid)
                    Commit();
            }

            return resultSpeaker;
        }

        public AppValidationResult Update(CollaboratorEditAppViewModel viewModel)
        {
            throw new NotImplementedException();
        }


        CollaboratorEditAppViewModel IAppService<SpeakerBasicAppViewModel, SpeakerDetailAppViewModel, CollaboratorEditAppViewModel, SpeakerItemListAppViewModel>.GetByEdit(Guid uid)
        {
            throw new NotImplementedException();
        }

        CollaboratorEditAppViewModel IAppService<SpeakerBasicAppViewModel, SpeakerDetailAppViewModel, CollaboratorEditAppViewModel, SpeakerItemListAppViewModel>.GetEditViewModel()
        {
            throw new NotImplementedException();
        }
    }

}
