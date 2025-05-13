using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    public class RequiredListAttribute : ValidationAttribute
    {
        /// <summary>
        /// Verify if the list has at least one value selected.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is IEnumerable enumerable))
            {
                var errorMessage = ErrorMessage ?? "A lista deve conter pelo menos um item.";
                return new ValidationResult(errorMessage);
            }

            foreach (var item in enumerable)
            {
                var propertyInfo = item?.GetType().GetProperty("Value");
                var itemValue = propertyInfo?.GetValue(item);

                if (itemValue == null || string.IsNullOrWhiteSpace(itemValue.ToString()))
                {
                    var errorMessage = ErrorMessage ?? "A lista deve conter pelo menos um item.";
                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
