//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Globalization;
//using System.Text.RegularExpressions;
//using System.Threading;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ProducerCnpjIsValid : ISpecification<Producer>
//    {
//        public string Target { get { return "CNPJ"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Producer entity)
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
//            if (currentCulture != null && currentCulture.Name == "pt-BR")
//            {
//                return !string.IsNullOrWhiteSpace(entity.CNPJ) && IsCnpj(entity.CNPJ);
//            }

//            return !string.IsNullOrWhiteSpace(entity.CNPJ) && (IsCnpj(entity.CNPJ) || IsVatNumber(entity.CNPJ));
//        }

//        public static bool IsCnpj(string cnpj)
//        {
//            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
//            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
//            int soma;
//            int resto;
//            string digito;
//            string tempCnpj;
//            cnpj = cnpj.Trim();
//            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
//            if (cnpj.Length != 14)
//                return false;
//            tempCnpj = cnpj.Substring(0, 12);
//            soma = 0;
//            for (int i = 0; i < 12; i++)
//                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
//            resto = (soma % 11);
//            if (resto < 2)
//                resto = 0;
//            else
//                resto = 11 - resto;
//            digito = resto.ToString();
//            tempCnpj = tempCnpj + digito;
//            soma = 0;
//            for (int i = 0; i < 13; i++)
//                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
//            resto = (soma % 11);
//            if (resto < 2)
//                resto = 0;
//            else
//                resto = 11 - resto;
//            digito = digito + resto.ToString();
//            return cnpj.EndsWith(digito);
//        }

//        public bool IsVatNumber(string vatNumber)
//        {
//            vatNumber = vatNumber.Trim();
//            vatNumber = vatNumber.Replace(".", "").Replace("-", "").Replace("/", "");

//            return NumberValid(vatNumber);
//        }

//        bool NumberValid(string value)
//        {
//            Regex rgx = new Regex(@"[0-9]{5,}");
//            return !string.IsNullOrWhiteSpace(value) && rgx.IsMatch(value);
//        }
//    }
//}
