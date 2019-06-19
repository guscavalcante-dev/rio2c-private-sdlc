namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public class AppAesEncryptionInfo : IAesEncryptionInfo
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int PasswordIterations { get; set; }
        public string InitialVector { get; set; }
        public int KeySize { get; set; }
        public AesEncryptionInfoCodes Code { get; set; }

        public AppAesEncryptionInfo() { }

        public AppAesEncryptionInfo(IAesEncryptionInfo otherEncryptionInfo)
        {
            Password = otherEncryptionInfo.Password;
            Salt = otherEncryptionInfo.Salt;
            PasswordIterations = otherEncryptionInfo.PasswordIterations;
            InitialVector = otherEncryptionInfo.InitialVector;
            KeySize = otherEncryptionInfo.KeySize;
        }
    }
}
