namespace PlataformaRio2C.Infra.CrossCutting.Tools.Mvc
{
    using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
    using System;
	using System.Web.Http.Controllers;
	using System.Web.Mvc;

	/// <summary>
    /// Model Binder to map custom Id classes of Entities
    /// </summary>
    public class IdentityModelBinder : DefaultModelBinder, System.Web.Http.ModelBinding.IModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType.Implements<IIdentity>())
            {
                object idValue = null;

                if (bindingContext.ModelType.IsSubclassOfOrEqualsTo<GuidIdentity>())
                    idValue = new Guid(controllerContext.RouteData.Values["id"].ToString());
                else if (bindingContext.ModelType.IsSubclassOfOrEqualsTo<IntIdentity>())
                    idValue = int.Parse(controllerContext.RouteData.Values["id"].ToString());

                if (idValue != null)
                    return Activator.CreateInstance(bindingContext.ModelType, idValue);
            }

            return base.BindModel(controllerContext, bindingContext);
        }

		public bool BindModel(HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
		{
			if (bindingContext.ModelType.Implements<IIdentity>())
			{
				object idValue = null;

				if (bindingContext.ModelType.IsSubclassOfOrEqualsTo<GuidIdentity>())
					idValue = new Guid(actionContext.ControllerContext.RouteData.Values["id"].ToString());
				else if (bindingContext.ModelType.IsSubclassOfOrEqualsTo<IntIdentity>())
					idValue = int.Parse(actionContext.ControllerContext.RouteData.Values["id"].ToString());

				if (idValue != null)
				{
					bindingContext.Model = Activator.CreateInstance(bindingContext.ModelType, idValue);
					return true;
				}

				return false;
			}

		    return false;
		}
    }
}