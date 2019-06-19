using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class NameFromResourceAttribute : Attribute
    {
        private readonly string _name;
        private readonly string _compareName;

        public NameFromResourceAttribute(string name)
        {
            _name = name;
        }

        public NameFromResourceAttribute(string compareName, string name)
        {
            _name = name;
            _compareName = compareName;
        }

        public string Name
        {
            get { return this._name; }
        }

        public string CompareName
        {
            get { return this._compareName; }
        }
    }
}
