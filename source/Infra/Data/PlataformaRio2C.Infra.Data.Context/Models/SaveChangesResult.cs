using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Infra.Data.Context.Models
{
    public class SaveChangesResult
    {
        public SaveChangesResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; private set; }
        public IEnumerable<ValidationResult> ValidationResults { get; set; }
    }
}
