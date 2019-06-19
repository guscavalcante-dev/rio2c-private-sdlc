namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public interface ISystemParameterCollection
    {
        SystemParameter this[SystemParameterCodes systemParameterCode] { get; }
    }
}
