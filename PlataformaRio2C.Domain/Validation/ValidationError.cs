using PlataformaRio2C.Domain.Enums;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Validation
{
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

        public ValidationError(string message, IEnumerable<string> memberNames, ErrorCodes erroCode)
        {
            Message = message;
            MemberNames = memberNames;
            Code = erroCode.ToString();
        }
    }
}
