namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    public enum IdentitySignInStatusCodes
    {
        Success = 0,
        LockedOut = 1,
        RequiresVerification = 2,
        Failure = 3
    }
}
