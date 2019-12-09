using System;
using System.Linq;
using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.Services
{
    public class MusicalCommissionAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, MusicalCommission, MusicalCommissionBasicAppViewModel, MusicalCommissionDetailAppViewModel, MusicalCommissionEditAppViewModel, MusicalCommissionItemListAppViewModel>, IMusicalCommissionAppService
    {
        private readonly ICollaboratorAppService _collaboratorService;
        private readonly IMusicalCommissionService _service;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IMusicalCommissionRepository _musicalCommissionRepository;
        private readonly IUserRoleRepository _roleRepository;

        public MusicalCommissionAppService(ICollaboratorAppService CollaboratorService, IMusicalCommissionService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, IdentityAutenticationService identityController)
            : base(unitOfWork, service)
        {
            _service = service;
            _collaboratorService = CollaboratorService;
            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
            _musicalCommissionRepository = repositoryFactory.MusicalCommissionRepository;
            _roleRepository = repositoryFactory.UserRoleRepository;
        }

        public AppValidationResult Create(CollaboratorEditAppViewModel entity)
        {
            BeginTransaction();

            var resultCollaborator = _collaboratorService.Create(entity);
            var resultMusical = ValidationResult;

            if (resultCollaborator.IsValid)
            {
                //Commit();

                var collaborator = _collaboratorRepository.Get(a => a.User.Email == entity.User.Email);

                int id = _service.GetAll().ToList().Count() + 1;
                MusicalCommission mComission = new MusicalCommission(collaborator);
                MusicalCommissionEditAppViewModel vm = new MusicalCommissionEditAppViewModel(mComission);
                vm.collaborator = mComission.Collaborator;

                //resultSpeaker = base.Create(vm);
                _musicalCommissionRepository.Create(mComission);
                //UserRole role = new UserRole();

                //role.UserId = vm.collaborator.UserId;
                //role.RoleId = 5;

                //_roleRepository.Create(role);

                if (resultMusical.IsValid)
                    Commit();
            }

            return resultMusical;
        }

        public AppValidationResult Update(CollaboratorEditAppViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        CollaboratorEditAppViewModel IAppService<MusicalCommissionBasicAppViewModel, MusicalCommissionDetailAppViewModel, CollaboratorEditAppViewModel, MusicalCommissionItemListAppViewModel>.GetByEdit(Guid uid)
        {
            throw new NotImplementedException();
        }

        CollaboratorEditAppViewModel IAppService<MusicalCommissionBasicAppViewModel, MusicalCommissionDetailAppViewModel, CollaboratorEditAppViewModel, MusicalCommissionItemListAppViewModel>.GetEditViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
