using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    public class UserRole : Entity
    {
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }       

        public override bool IsValid()
        {
            return true;
        }
    }
}
