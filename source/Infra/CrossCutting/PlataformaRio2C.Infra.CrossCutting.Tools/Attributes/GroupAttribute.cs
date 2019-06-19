using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class GroupAttribute : Attribute
    {
        private readonly int _groupCode;

        public GroupAttribute(int groupCode)
        {
            this._groupCode = groupCode;
        }

        public int GroupCode
        {
            get { return this._groupCode; }
        }
    }
}
