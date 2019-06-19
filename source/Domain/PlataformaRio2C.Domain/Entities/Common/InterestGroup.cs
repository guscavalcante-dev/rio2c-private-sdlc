using PlataformaRio2C.Domain.Enums;

namespace PlataformaRio2C.Domain.Entities
{
    public class InterestGroup : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 150;
        public static readonly int TypeMaxLength = 100;

        public string Name { get; private set; }

        public string Type { get; private set; }

        protected InterestGroup()
        {

        }

        public InterestGroup(string name, InterestGroupTypeCodes value)
        {
            SetName(name);
            SetType(value);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetType(InterestGroupTypeCodes value)
        {
            Type = value.ToString();
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
