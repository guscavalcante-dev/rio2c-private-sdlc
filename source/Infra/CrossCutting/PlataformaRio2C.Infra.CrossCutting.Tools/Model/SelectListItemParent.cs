using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Model
{
    public class SelectListItemParent : SelectListItem
    {
        public string ParentValue { get; set; }
    }

    public class SelectListItemDataModel<T> : SelectListItem where T : class
    {
        public T DataModel { get; set; }
    }
}
