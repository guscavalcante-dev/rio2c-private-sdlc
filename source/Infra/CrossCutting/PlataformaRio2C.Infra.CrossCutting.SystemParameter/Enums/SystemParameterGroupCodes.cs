using System.ComponentModel;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public enum SystemParameterGroupCodes
    {
        [Description("Emails")]
        Emails = 0,

        [Description("Geral")]
        General = 1,

        [Description("Configurações de SMS")]
        Sms = 3,

        [Description("Projetos")]
        Projets = 4,

        [Description("Sympla")]
        Sympla = 5,

        [Description("Agenda de rodadas de negócios")]
        ScheduleOneToOneMeetings = 6,

        [Description("Rede Rio2C")]
        NetworkRio2C = 7,

        [Description("Relatório Financeiro")]
        FinancialReport = 8
    }
}
