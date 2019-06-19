using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Model
{
    public class ValidationResultExtend : ValidationResult
    {
        public ValidationResultExtend(string errorMessage, IEnumerable<string> memberNames, object dataValue = null) : base(errorMessage, memberNames)
        {
            this.DataValue = dataValue;
        }

        public object DataValue { get; private set; }
    }
}
