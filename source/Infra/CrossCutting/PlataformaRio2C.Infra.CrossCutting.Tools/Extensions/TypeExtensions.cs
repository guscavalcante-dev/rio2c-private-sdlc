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

        /// <summary>
        /// Determines whether [is subclass of or equals to] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="another">Another.</param>
        /// <returns></returns>
        public static bool IsSubclassOfOrEqualsTo(this Type type, Type another)
        {
            return type != null && (type == another || type.IsSubclassOf(another));
        }

        /// <summary>
        /// Determines whether [is subclass of or equals to] [the specified type].
        /// </summary>
        /// <typeparam name="TAnotherType">The type of another type.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns></returns>
		public static bool IsSubclassOfOrEqualsTo<TAnotherType>(this Type type) where TAnotherType : class
        {
            return type.IsSubclassOfOrEqualsTo(typeof(TAnotherType));
        }
    }
}
