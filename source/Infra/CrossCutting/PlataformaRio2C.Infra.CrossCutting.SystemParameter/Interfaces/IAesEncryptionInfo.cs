namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public interface IAesEncryptionInfo
    {
        string Password { get; }
        string Salt { get; set; }
        int PasswordIterations { get; }
        string InitialVector { get; }
        int KeySize { get; }
    }
}
