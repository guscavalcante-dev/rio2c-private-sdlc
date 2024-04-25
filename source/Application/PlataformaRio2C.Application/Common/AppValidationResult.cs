using PlataformaRio2C.Infra.Data.Context.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application
{
    public class AppValidationResult
    {
        private readonly List<AppValidationError> _erros;
        private string Message { get; set; }
        public bool IsValid { get { return !_erros.Any(); } }
        public object Data { get; set; }

        public IEnumerable<AppValidationError> Errors { get { return _erros; } }

        public AppValidationResult()
        {
            _erros = new List<AppValidationError>();
        }       

        public AppValidationResult Add(Domain.Validation.ValidationResult domainValidateResult)
        {
            foreach (var item in domainValidateResult.Errors)
            {
                _erros.Add(new AppValidationError(item));
            }
            return this;
        }

        public AppValidationResult Add(string errorMessage)
        {
            _erros.Add(new AppValidationError(errorMessage));
            return this;
        }

        public AppValidationResult Add(string errorMessage, string target)
        {
            _erros.Add(new AppValidationError(errorMessage, target));
            return this;
        }

        public AppValidationResult Add(string errorMessage, string target, string code)
        {
            _erros.Add(new AppValidationError(errorMessage, target, code));
            return this;
        }

        public AppValidationResult Add(SaveChangesResult saveChangesResult)
        {
            foreach (var validationResult in saveChangesResult.ValidationResults)
            {
                foreach(var memberName in validationResult.MemberNames)
                {
                    _erros.Add(new AppValidationError(validationResult.ErrorMessage, memberName));
                }
            }
            return this;
        }
    }
}
