using System.Linq;

namespace PlataformaRio2C.Application
{
    public class AppValidationError
    {
        public string Message { get; private set; }
        public string Code { get; private set; }
        public string Target { get; private set; }

        public AppValidationError(Domain.Validation.ValidationError validationError)
        {
            if (validationError.MemberNames != null)
            {
                Target = validationError.MemberNames.FirstOrDefault();
            }

            Code = validationError.Code;
            Message = validationError.Message;
        }

        public AppValidationError(string message)
        {
            Message = message;
        }

        public AppValidationError(string message, string target)
        {
            Message = message;
            Target = target;
        }

        public AppValidationError(string message, string target, string code)
        {
            Message = message;
            Target = target;
            Code = code;
        }
    }
}
