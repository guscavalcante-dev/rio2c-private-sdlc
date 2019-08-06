// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="UserUseTermAppService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Application.ViewModels;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Linq;

namespace PlataformaRio2C.Application.Services
{
    /// <summary>UserUseTermAppService</summary>
    public class UserUseTermAppService : AppService<PlataformaRio2CContext, Domain.Entities.UserUseTerm, UserUseTermAppViewModel, UserUseTermAppViewModel, UserUseTermAppViewModel, UserUseTermAppViewModel>, IUserUseTermAppService
    {
        private readonly IEditionRepository _eventRepository;
        private readonly IRoleRepository _roleRepository;

        /// <summary>Initializes a new instance of the <see cref="UserUseTermAppService"/> class.</summary>
        /// <param name="service">The service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public UserUseTermAppService(IUserUseTermService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
            : base(unitOfWork, service)
        {
            _eventRepository = repositoryFactory.EditionRepository;
            _roleRepository = repositoryFactory.RoleRepository;
        }

        public override AppValidationResult Create(UserUseTermAppViewModel viewModel)
        {
            BeginTransaction();

            var _event = _eventRepository.Get(e => e.Name.Contains("2018"));

            if (_event != null)
            {
                viewModel.EventId = _event.Id;
            }

            if (service.Get(e => e.UserId == viewModel.UserId && e.EventId == viewModel.EventId && e.Role.Name == viewModel.Role) == null)
            {
                var entity = viewModel.MapReverse();

                if (viewModel.Role != null)
                {
                    var entityRole = _roleRepository.Get(e => e.Name == viewModel.Role);
                    entity.SetRole(entityRole);
                }

                ValidationResult.Add(service.Create(entity));

                if (ValidationResult.IsValid)
                    Commit();
            }

            return ValidationResult;

        }

        public IEnumerable<UserUseTermAppViewModel> GetAllByUserId(int id)
        {
            IEnumerable<UserUseTermAppViewModel> result = null;

            var s = service as IUserUseTermService;

            var entities = s.GetAllByUserId(id);

            if (entities != null && entities.Any())
            {
                result = UserUseTermAppViewModel.MapList(entities);
            }

            return result;
        }

        public UserUseTermAppViewModel GetByUserId(int id)
        {
            UserUseTermAppViewModel viewModel = null;

            var s = service as IUserUseTermService;

            var entity = s.GetByUserId(id);

            if (entity != null)
            {
                viewModel = new UserUseTermAppViewModel(entity);
            }

            return viewModel;
        }
    }
}
