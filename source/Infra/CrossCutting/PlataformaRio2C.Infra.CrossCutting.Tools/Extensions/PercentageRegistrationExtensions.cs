using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class PercentageRegistrationExtensions
    {
        public static double CalculatePercentageFill(this object obj)
        {
            if (obj == null) { return 0; }

            var propriedadesCalculadas = obj.GetType().GetProperties().Where(e => e.GetCustomAttributes(typeof(PercentageRegistrationAttribute), false).Any());

            if (!propriedadesCalculadas.Any()) { throw new InvalidOperationException("Objeto não tem propriedades à calcular"); }

            double totalPreenchido = 0;

            foreach (var propriedade in propriedadesCalculadas)
            {
                var percentual = propriedade.GetCustomAttribute<PercentageRegistrationAttribute>();

                if (propriedade.PropertyType.IsCollection())
                {
                    var colecao = propriedade.GetValue(obj);
                    int preenchidos = 0;

                    if (colecao != null)
                    {
                        foreach (var item in (colecao as IEnumerable))
                        {
                            if ((int)Math.Round(item.CalculatePercentageFill()) == 100) preenchidos++;
                        }
                    }

                    if (preenchidos >= percentual.ColecaoMinima) totalPreenchido += percentual.Percentual;
                }
                else if (propriedade.PropertyType.IsPrimitiveExtension())
                {
                    var valor = propriedade.GetValue(obj);
                    if (propriedade.PropertyType == typeof(int))
                    {
                        if ((int)valor > 0)
                        {
                            totalPreenchido += percentual.Percentual;
                        }
                    }
                    else if (propriedade.PropertyType == typeof(string))
                    {
                        if (!string.IsNullOrEmpty((string)propriedade.GetValue(obj)))
                        {
                            totalPreenchido += percentual.Percentual;
                        }
                    }
                }
                else
                {
                    totalPreenchido += propriedade.GetValue(obj).CalculatePercentageFill();
                }
            }

            if (totalPreenchido > 100) { throw new InvalidOperationException("Configuração do calculo de objeto completo errado"); }

            return totalPreenchido;
        }

        private static bool IsCollection(this Type tipo)
        {
            if (tipo.IsArray) { return true; }

            if (tipo.IsGenericType)
            {
                foreach (Type arg in tipo.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(tipo))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsPrimitiveExtension(this Type tipo)
        {
            return (tipo.IsPrimitive || tipo.IsValueType || (tipo == typeof(string)));
        }
    }
}
