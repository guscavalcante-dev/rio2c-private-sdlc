using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class PhoneNumberIsValid : ISpecification<string>
    {
        public string Target { get; set; }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public PhoneNumberIsValid()
        {
            Target = "PhoneNumber";
        }

        public PhoneNumberIsValid(string target)
        {
            Target = target;
        }

        public bool IsSatisfiedBy(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && NumberValid(value);
        }

        bool NumberValid(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (Regex.IsMatch(value, @"[a-zA-Z]+"))
                {
                    return false;
                }

                var cleaned = RemoveNonNumeric(value);
                if (cleaned.Length >= 6)
                    return true;
                else
                    return false;
            }
            else
            {
                return true;
            }
        }

        string RemoveNonNumeric(string phone)
        {
            return Regex.Replace(phone, @"[^0-9]+", "");
        }
    }
}
