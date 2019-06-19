using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter.Models
{
    public class AppValidationResult
    {
        private readonly List<AppValidationError> _erros;
        private string Message { get; set; }
        public bool IsValid { get { return !_erros.Any(); } }

        public IEnumerable<AppValidationError> Errors { get { return _erros; } }

        public AppValidationResult()
        {
            _erros = new List<AppValidationError>();
        }

        public AppValidationResult Add(ValidationResult domainValidateResult)
        {
            foreach (var item in domainValidateResult.Errors)
            {
                _erros.Add(new AppValidationError(item));
            }
            return this;
        }

        public AppValidationResult Add(string message)
        {            
            _erros.Add(new AppValidationError(message));
            return this;
        }
    }

    public class AppValidationError
    {
        public string Message { get; private set; }
        public string Code { get; private set; }
        public string Target { get; private set; }

        public AppValidationError(string message)
        {
            Message = message;
        }

        public AppValidationError(ValidationError validationError)
        {
            this.Target = validationError.MemberNames.FirstOrDefault();
            this.Code = validationError.Code;
            this.Message = validationError.Message;
        }
    }

    public class ValidationResult
    {
        private readonly List<ValidationError> _erros;
        private string Message { get; set; }
        public IEnumerable<string> MemberNames { get; set; }
        public bool IsValid { get { return !_erros.Any(); } }
        public IEnumerable<ValidationError> Errors { get { return _erros; } }

        public ValidationResult()
        {
            _erros = new List<ValidationError>();
        }

        public ValidationResult Add(string errorMessage)
        {
            _erros.Add(new ValidationError(errorMessage));
            return this;
        }

        public ValidationResult Add(ValidationError error)
        {
            _erros.Add(error);
            return this;
        }

        public ValidationResult Add(params ValidationResult[] validationResults)
        {
            if (validationResults == null) return this;

            foreach (var result in validationResults.Where(r => r != null))
                _erros.AddRange(result.Errors);

            return this;
        }

        public ValidationResult Remove(ValidationError error)
        {
            if (_erros.Contains(error))
                _erros.Remove(error);
            return this;
        }
    }

    public class ValidationError
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> MemberNames { get; private set; }

        public ValidationError(string message)
        {
            Message = message;
        }

        public ValidationError(string message, IEnumerable<string> memberNames)
        {
            Message = message;
            MemberNames = memberNames;
        }

        public ValidationError(string message, IEnumerable<string> memberNames, string erroCode)
        {
            Message = message;
            MemberNames = memberNames;
            Code = erroCode.ToString();
        }
    }
}
