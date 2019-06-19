namespace PlataformaRio2C.Domain.Entities
{
    public class ProjectStatus : Entity
    {
        public static int CodeMaxLength = 150;
        public string Code { get; private set; }

        public string Name { get; private set; }

        protected ProjectStatus()
        {
        }

        public ProjectStatus(string code, string name)
        {
            SetCode(code);
            SetName(name);
        }

        public void SetCode(string value)
        {
            Code = value;
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
