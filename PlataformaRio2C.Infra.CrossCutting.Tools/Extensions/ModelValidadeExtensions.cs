using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class ModelValidadeExtensions
    {
        public static void ValidateModel(this Controller controller, object viewModel)
        {
            controller.ModelState.Clear();

            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);

            foreach (var result in validationResults)
            {
                foreach (var name in result.MemberNames)
                {
                    controller.ModelState.AddModelError(name, result.ErrorMessage);
                }
            }
        }
    }
}
