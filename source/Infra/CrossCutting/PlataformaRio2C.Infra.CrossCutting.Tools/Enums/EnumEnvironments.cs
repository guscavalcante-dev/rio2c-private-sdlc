using System.ComponentModel;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Enums
{
    public enum EnumEnvironments
    {
        [Description("all")]
        All,

        [Description("dev")]
        Dev,

        [Description("test")]
        Test,

        [Description("prod")]
        Prod
    }
}
