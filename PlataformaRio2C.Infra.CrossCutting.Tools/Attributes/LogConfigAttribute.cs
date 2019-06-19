using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class LogConfigAttribute : Attribute
    {
        public bool NoLog { get; set; }
        public string Description { get; set; }
    }
}
