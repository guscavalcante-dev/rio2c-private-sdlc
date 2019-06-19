using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.Models;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.ViewModels;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public class SystemParameterAppService: ISystemParameterAppService
    {
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly IUnitOfWorkSystemParameter _unitOfWork;


        protected AppValidationResult ValidationResult { get; private set; }

        public SystemParameterAppService(ISystemParameterRepository systemParameterRepository, IUnitOfWorkSystemParameter uow)
        {
            _systemParameterRepository = systemParameterRepository;
            _unitOfWork = uow;
            ValidationResult = new AppValidationResult();
        }

        public IEnumerable<SystemParameterAppViewModel> All(bool @readonly = false)
        {
            var parameters = _systemParameterRepository.GetAll().ToList();
            var results = new List<SystemParameterAppViewModel>();
            string[] names = new string[] { "NdsPassword", "AdPassword", "UrlGatewaySms", "password" };

            foreach (var parameter in parameters)
            {
                var result = new SystemParameterAppViewModel(parameter);

                if (names.Any(name => result.Code.ToLower().Contains(name)))
                {
                    result.Value = string.Empty;
                }

                results.Add(result);
            }

            return results;
        }

        public void ReIndex()
        {

            var parameterDtos = default(SystemParameterCodes).SystemParametersDescriptions();

            foreach (var dto in parameterDtos)
            {
                var parameter = new SystemParameter(dto);
                var parameterExisting = _systemParameterRepository.Get(e => e.Code == parameter.Code && e.LanguageCode == parameter.LanguageCode);
                if (parameterExisting == null)
                {
                    _systemParameterRepository.Create(parameter);
                }
                else
                {
                    parameterExisting.UpdateFromDto(dto, true);
                }
            }

            if (ValidationResult.IsValid)
                Commit();
        }


        public AppValidationResult UpdateAll(IEnumerable<SystemParameterAppViewModel> systemParameterAppViewModels)
        {
            //var i = 0;

            //systemParameterViewModels.RemoveAll(VisibleIsFalseAndValueNull);

            foreach (var systemParameterViewModel in systemParameterAppViewModels)
            {
                var systemParameter = _systemParameterRepository.Get(e => e.Uid == systemParameterViewModel.Uid);

                if (!systemParameterViewModel.Visible)
                {
                    systemParameterViewModel.Value = systemParameter.GetValue<string>();
                }

                systemParameter.SetValue(systemParameterViewModel.Value);

                _systemParameterRepository.Update(systemParameter);
            }

            if (ValidationResult.IsValid)
                Commit();

            return ValidationResult;
        }

        public T Get<T>(string code)
        {
            return _systemParameterRepository.Get<T>((SystemParameterCodes)Enum.Parse(typeof(SystemParameterCodes), code));
        }

        public virtual void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }

        public virtual void Commit()
        {
            var result = _unitOfWork.SaveChanges();
            if (!result.Success)
            {
                if (result.ValidationResults.Any())
                {
                    foreach (var error in result.ValidationResults)
                    {
                        ValidationResult.Add(error.ErrorMessage);
                    }
                }
                else
                {
                    ValidationResult.Add(Messages.ErrorWhileSavingInDataBase);
                }
            }
        }
    }
}
