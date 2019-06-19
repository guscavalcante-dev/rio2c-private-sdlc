using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class CustomDescriptionAttribute : Attribute
    {
        private readonly string _description;
        private readonly int _index;

        public CustomDescriptionAttribute(string description)
        {
            _description = description;
        }

        public CustomDescriptionAttribute(string description, int index)
        {
            _description = description;
            _index = index;
        }

        public string Description
        {
            get { return this._description; }
        }

        public int Index
        {
            get { return this._index; }
        }
    }
}
