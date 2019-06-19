using Microsoft.AspNet.Identity.EntityFramework;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
}
