using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Admin
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomExceptionMvcFilterAttribute());
        }
    }
}
