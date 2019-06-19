using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    public class UserAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Domain.Entities.User, UserAppViewModel, UserAppViewModel, UserAppViewModel, UserAppViewModel>, IUserAppService
    {
        private readonly IdentityAutenticationService _identityController;
        protected readonly IRoleRepository _roleRepository;
        protected readonly IUserRepository _userRepository;
        protected readonly IUserRoleRepository _userRoleRepository;

        public UserAppService(IUserService service, IUnitOfWork unitOfWork, IUserRepository userRepository, IUserRoleRepository userRoleRepository ,IdentityAutenticationService identityController, IRoleRepository roleRepository)
            : base(unitOfWork, service)
        {
            _identityController = identityController;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public override AppValidationResult Create(UserAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = viewModel.MapReverse();

            entity.Roles = new List<Role>() { _roleRepository.Get(e => e.Name == "Administrator") };

            ValidationResult.Add(service.Create(entity));

            if (ValidationResult.IsValid)
            {
                Commit();

                if (!string.IsNullOrWhiteSpace(viewModel.Password) && entity != null)
                {
                    _identityController.AddPasswordAsync(entity.Id, viewModel.Password);
                }
            }


            return ValidationResult;
        }

        //public IEnumerable<UserAppViewModel> getAllPlayerProducer()
        //{
        //    var entity = new List<UserAppViewModel>();
        //    List<Role> role = _roleRepository.GetAll(r => r.Name == "Producer" || r.Name == "Player").ToList();

        //    List<User> UsersNoFilter = service.GetAll(u => u.Roles.Equals(role)).ToList();
        //    List<User> users = new List<User>();

        //    foreach(User UserNoFilter in UsersNoFilter)
        //    {

        //        if (UserNoFilter.Roles.Equals(role))
        //        {
        //            users.Add(UserNoFilter);
        //        }
        //    }

        //    //foreach (User user in userRoleProducer)
        //    //{

        //    //    UserAppViewModel vm = new UserAppViewModel();
        //    //    vm.Name = user.Name;
        //    //    vm.Password = user.PasswordHash;
        //    //    vm.PhoneNumber = user.PhoneNumber;
        //    //    vm.Uid = user.Uid;
        //    //    vm.UserName = user.UserName;
        //    //    vm.Active = user.Active;
        //    //    vm.CreationDate = user.CreationDate;
        //    //    vm.Email = user.Email;

        //    //    entity.Add(vm);
        //    //}

        //    return entity;
        //}

        public override AppValidationResult Update(UserAppViewModel viewModel)
        {
            BeginTransaction();

            var entity = service.Get(viewModel.Uid);

            if (entity != null)
            {
                var entityAlter = viewModel.MapReverse(entity);

                ValidationResult.Add(service.Update(entityAlter));
            }


            if (ValidationResult.IsValid)
            {
                Commit();

                if (!string.IsNullOrWhiteSpace(viewModel.Password) && entity != null)
                {
                    _identityController.AddPassword(entity.Id, viewModel.Password);
                }
            }


            return ValidationResult;
        }

        public IEnumerable<UserAppViewModel> listAdmin()
        {
            var entity = new List<UserAppViewModel>();
            var userAdmin = _userRepository.GetAll(u => u.Roles.ToString().Contains("Administrator"));

            foreach (User user in userAdmin)
            {
                UserAppViewModel vm = new UserAppViewModel();
                vm.Name = user.Name;
                vm.Password = user.PasswordHash;
                vm.PhoneNumber = user.PhoneNumber;
                vm.Uid = user.Uid;
                vm.UserName = user.UserName;
                vm.Active = user.Active;
                vm.CreationDate = user.CreationDate;
                vm.Email = user.Email;

                entity.Add(vm);
            }

            return entity;
        }
    }
}
