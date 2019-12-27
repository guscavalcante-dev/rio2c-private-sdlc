//using PlataformaRio2C.Domain.Entities.Validations;

//namespace PlataformaRio2C.Domain.Entities
//{
//    public class ImageFile : Entity
//    {
//        public static readonly int FileNameMinLength = 2;
//        public static readonly int FileNameMaxLength = 500;
//        public static readonly int ContentTypeLength = 200;
//        public static readonly double ImageMinMByteSize = 0.001f;
//        public static readonly double ImageMaxMByteSize = 10f;

//        public string FileName { get; private set; }
//        public byte[] File { get; private set; }
//        public string ContentType { get; private set; }
//        public int ContentLength { get; private set; }

//        protected ImageFile()
//        {

//        }

//        public ImageFile(string fileName, byte[] file, string contentType, int contentLength)
//        {
//            SetFileName(fileName);
//            SetFile(file);
//            SetContentType(contentType);
//            SetContentLength(contentLength);
//        }

//        public void SetFileName(string fileName)
//        {
//            FileName = fileName;
//        }

//        public void SetFile(byte[] file)
//        {
//            File = file;
//        }

//        public void SetContentType(string contentType)
//        {
//            ContentType = contentType;
//        }

//        public void SetContentLength(int contentLength)
//        {
//            ContentLength = contentLength;
//        }

//        public override bool IsValid()
//        {
//            var validate = new ImageIsConsistent();
//            ValidationResult = validate.Valid(this);

//            return ValidationResult.IsValid;
//        }


//        public static double ConvertBytesToMegabytes(long bytes)
//        {
//            return (bytes / 1024f) / 1024f;
//        }
//    }
//}
