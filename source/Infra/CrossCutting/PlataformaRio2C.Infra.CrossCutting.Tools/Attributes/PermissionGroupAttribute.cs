using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class PermissionGroupAttribute : Attribute
    {
        private readonly int _groupCode;

        public PermissionGroupAttribute(int groupCode)
        {
            this._groupCode = groupCode;
        }

        public int GroupCode
        {
            get { return this._groupCode; }
        }
    }
}
