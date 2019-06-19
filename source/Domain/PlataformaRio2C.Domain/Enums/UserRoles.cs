using System.ComponentModel;

namespace PlataformaRio2C.Domain.Enums
{
    public enum UserRoles
    {
        [Description("UserRoles_None")]
        None,
        [Description("UserRoles_Administrator")]
        Administrator,
        [Description("UserRoles_Player")]
        Player,
        [Description("UserRoles_Producer")]
        Producer,
    }
}
