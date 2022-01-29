using System.Collections.Generic;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Controllers.Contracts
{
    public class ResponseApi
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<ResponseApiErros> Erros { get; set; }

    }

    public class ResponseApiErros
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}