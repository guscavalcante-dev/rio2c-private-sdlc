namespace PlataformaRio2C.Domain.Entities
{
    public class TargetAudience : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;

        public string Name { get; private set; }

        protected TargetAudience()
        {
        }

        public TargetAudience(string name)
        {
            SetName(name);
        }


        public void SetName(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
