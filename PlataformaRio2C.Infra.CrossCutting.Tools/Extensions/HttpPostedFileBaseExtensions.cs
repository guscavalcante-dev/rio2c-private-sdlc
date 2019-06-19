using System.Web;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class HttpPostedFileBaseExtensions
    {
        public static byte[] GetBytes(this HttpPostedFileBase file)
        {
            byte[] uploadedFile = new byte[file.InputStream.Length];
            file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            return uploadedFile;
        }

        public static bool IsContentType(this HttpPostedFileBase file, string contentType)
        {
            return file.ContentType == contentType;
        }
    }
}
