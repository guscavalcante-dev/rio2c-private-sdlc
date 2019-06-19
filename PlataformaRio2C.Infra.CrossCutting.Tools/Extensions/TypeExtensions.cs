using System;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class TypeExtensions
    {
        public static dynamic Cast(this Type Type, object data)
        {
            var DataParam = Expression.Parameter(typeof(object), "data");
            var Body = Expression.Block(Expression.Convert(Expression.Convert(DataParam, data.GetType()), Type));

            var Run = Expression.Lambda(Body, DataParam).Compile();
            var ret = Run.DynamicInvoke(data);
            return ret;
        }
    }
}
