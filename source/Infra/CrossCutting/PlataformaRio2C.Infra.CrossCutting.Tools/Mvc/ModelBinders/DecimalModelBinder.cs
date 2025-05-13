namespace PlataformaRio2C.Infra.CrossCutting.Tools.Mvc
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    /// <summary>
    /// Model Binder to correctly map decimals
    /// </summary>
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
                                ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                                                            .GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            if (string.IsNullOrEmpty(valueResult.AttemptedValue))
                return actualValue;
            try
            {
                actualValue = Convert.ToDecimal(valueResult.AttemptedValue,
                                                CultureInfo.CurrentCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
