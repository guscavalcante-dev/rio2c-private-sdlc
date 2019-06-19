namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Models
{
    public class ErrorResponse
    {
        public Error Error { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Target { get; set; }
        public Error[] Details { get; set; }
        public Innererror InnerError { get; set; }
    }

    public class Innererror
    {
        public string Code { get; set; }
        public Innererror InnerError { get; set; }
    }
}