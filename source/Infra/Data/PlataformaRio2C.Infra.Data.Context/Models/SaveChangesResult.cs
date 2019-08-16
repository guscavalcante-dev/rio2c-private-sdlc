using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Context.Models
{
    /// <summary>SaveChangesResult</summary>
    public class SaveChangesResult
    {
        public bool Success { get; private set; }
        public List<ValidationResult> ValidationResults { get; set; }

        public SaveChangesResult(bool success)
        {
            this.Success = success;
        }

        /// <summary>Adds the validation result.</summary>
        /// <param name="validationResult">The validation result.</param>
        public void AddValidationResult(ValidationResult validationResult)
        {
            if (validationResult == null)
            {
                return;
            }

            this.ValidationResults.Add(validationResult);
        }

        /// <summary>Adds the validation results.</summary>
        /// <param name="validationResults">The validation results.</param>
        public void AddValidationResults(List<ValidationResult> validationResults)
        {
            if (validationResults?.Any() != true)
            {
                return;
            }

            this.ValidationResults.AddRange(validationResults);
        }
    }
}