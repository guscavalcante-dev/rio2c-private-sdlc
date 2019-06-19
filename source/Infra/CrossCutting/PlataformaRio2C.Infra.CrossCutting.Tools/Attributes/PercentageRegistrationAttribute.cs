using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PercentageRegistrationAttribute : Attribute
    {
        private double percentual;
        private bool opcional;
        private int colecaoMinima;

        public PercentageRegistrationAttribute(double percentual, bool opcional = false)
        {
            this.percentual = percentual;
            this.opcional = opcional;
            this.colecaoMinima = 1;
        }

        public PercentageRegistrationAttribute(double percentual, int colecaoMinima)
        {
            this.percentual = percentual;
            this.colecaoMinima = colecaoMinima;
        }

        public double Percentual
        {
            get { return this.percentual; }
        }

        public bool IsOpcional
        {
            get { return this.opcional; }
        }

        public int ColecaoMinima
        {
            get { return this.colecaoMinima; }
        }
    }
}
