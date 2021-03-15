using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToYesOrNoString(this bool boolValue)
        {
            return boolValue ? Labels.Yes : Labels.No;
        }

        public static string ToYesOrNoString(this bool? boolValue)
        {
            if (boolValue == true)
                return Labels.Yes;
            else if (boolValue == false)
                return Labels.No;
            else
                return "-";
        }
    }
}
