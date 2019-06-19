namespace PlataformaRio2C.Domain.Entities
{
    public class Language : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 50;

        public string Name { get; private set; }

        public string Code { get; private set; }

        protected Language()
        {

        }

        public Language(string name, string code)
        {
            SetName(name);
            Code = code;
        }

        public void SetName(string value)
        {
            Name = value;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
