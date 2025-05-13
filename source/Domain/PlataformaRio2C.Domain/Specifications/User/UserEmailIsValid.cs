using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Net.Mail;

namespace PlataformaRio2C.Domain.Entities.Specifications
{
    public class UserEmailIsValid : ISpecification<User>
    {
        public string Target { get { return "User.Email"; } }
        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

        public bool IsSatisfiedBy(User entity)
        {
            if (entity == null) return false;

            try
            {
                if (!string.IsNullOrWhiteSpace(entity.Email))
                {
                    MailAddress m = new MailAddress(entity.Email);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
