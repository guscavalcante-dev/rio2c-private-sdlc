using System.ComponentModel;

namespace PlataformaRio2C.Domain.Enums
{
    public enum StatusProjectCodes
    {
        [Description("Em avaliação")]
        OnEvaluation,
        [Description("Aprovado")]
        Accepted,
        [Description("Recusado")]
        Rejected
    }
}
