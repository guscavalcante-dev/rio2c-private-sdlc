using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Método que mapea um objeto composto em um array de string, onde cada elemento do array
        /// é populado pelo valor de cada propriedade no objeto composto, seguindo a ordem de definição do mesmo
        /// </summary>
        /// <typeparam name="T">Tipo de entrada</typeparam>
        /// <param name="objeto">Objeto a ser convertido</param>
        /// <returns></returns>
        public static string[] ObjectToArrayString<T>(this T objeto)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var result = new string[properties.Count()];
            for (int i = 0; i < properties.Count(); i++)
            {
                result[i] = properties[i].GetValue(objeto).ToString();
            }
            return result;
        }

        /// <summary>
        /// Método que retorna a property baseada em uma expression func
        /// </summary>
        /// <typeparam name="T">Tipo principal</typeparam>
        /// <typeparam name="P">Tipo da property</typeparam>
        /// <param name="valueProperty"></param>
        /// <returns></returns>
        public static PropertyInfo GetProperty<T, P>(this Expression<Func<T, P>> valueProperty)
        {
            var type = typeof(T);

            var expression = valueProperty.Body as MemberExpression;

            if (expression == null)
            {
                expression = ((UnaryExpression)valueProperty.Body).Operand as MemberExpression;
            }

            string propertyName = expression.Member.Name;

            var propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null) { throw new NullReferenceException(string.Format("Property '{0}' not found in '{1}'.", propertyName, type.Name)); }

            return propertyInfo;
        }

        public static byte[] ObjectToByteArray(this object obj)
        {
            if (obj == null) return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static object ByteArrayToObject(this byte[] arrBytes)
        {
            if (arrBytes == null) return null;

            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        public static string GetAuditPropertyFromAttribute<T>(this T obj, Type attributeType)
        {
            if (obj == null)
            {
                return null;
            }

            var resultado = new StringBuilder();

            var propriedades = obj.GetType().GetProperties().Where(e => e.GetCustomAttributes(attributeType, false).Any());

            if (propriedades.Any())
            {
                resultado.AppendFormat("<{0}>", obj.GetType().Name);

                foreach (var propriedade in propriedades)
                {
                    var subTipo = GetCollectionType(propriedade.PropertyType);
                    if (subTipo != null) //Coleção
                    {
                        if (subTipo.IsPrimitive || subTipo.IsValueType || (subTipo == typeof(string))) //Tipo primitivo
                        {
                            resultado.AppendFormat(" - <{0}>: '{1}'", propriedade.Name, GetValueCollectionSimpleObject(propriedade.GetValue(obj) as IEnumerable));
                        }
                        else //Objeto complexo
                        {
                            resultado.AppendFormat(" - <{0}>: '{1}'", propriedade.Name, GetValueObjectComplexCollection(propriedade.GetValue(obj) as IEnumerable, attributeType));
                        }
                    }
                    else // Não Coleção
                    {
                        if (propriedade.PropertyType.IsPrimitive || propriedade.PropertyType.IsValueType || (propriedade.PropertyType == typeof(string))) //Tipo primitivo
                        {
                            resultado.AppendFormat(" - <{0}>: '{1}'", propriedade.Name, propriedade.GetValue(obj));
                        }
                        else //Objeto complexo
                        {
                            resultado.AppendFormat(" - <{0}>: '{1}'", propriedade.Name, propriedade.GetValue(obj).GetAuditPropertyFromAttribute(attributeType));
                        }
                    }
                }
            }
            return resultado.ToString();
        }

        private static Type GetCollectionType(Type tipoDesconhecido)
        {
            if (tipoDesconhecido == null || tipoDesconhecido == typeof(string))
                return null;
            if (tipoDesconhecido.IsArray)
                return tipoDesconhecido.GetElementType();
            if (tipoDesconhecido.IsGenericType)
            {
                foreach (Type arg in tipoDesconhecido.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(tipoDesconhecido))
                    {
                        return arg;
                    }
                }
            }
            return null;
        }

        private static string GetValueCollectionSimpleObject(IEnumerable objetoDeColecao)
        {
            var resultado = new StringBuilder();
            var count = 0;

            resultado.Append("[ ");

            foreach (var item in objetoDeColecao)
            {
                resultado.AppendFormat("{0}, ", item);
                count++;
            }

            if (count > 0)
            {
                resultado.Remove(resultado.Length - 2, 2);
            }

            resultado.Append(" ]");

            return resultado.ToString();
        }

        private static string GetValueObjectComplexCollection(IEnumerable objetoDeColecao, Type attributeType)
        {
            var resultado = new StringBuilder();
            var count = 0;

            resultado.Append("[ ");

            foreach (var item in objetoDeColecao)
            {
                resultado.AppendFormat("{0}, ", item.GetAuditPropertyFromAttribute(attributeType));
                count++;
            }

            if (count > 0)
            {
                resultado.Remove(resultado.Length - 2, 2);
            }

            resultado.Append(" ]");

            return resultado.ToString();
        }
        //public static bool IsNullable<T>(this T t) { return false; }
        //public static bool IsNullable<T>(this T? t) where T : struct { return true; }

        public static bool IsNullable<T>(this T value)
        {
            return IsNullable(typeof(T));
        }

        public static bool IsNullable(this Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));

            //return Nullable.GetUnderlyingType(typeof(T)) != null;
        }

        public static string LdapObjectToStringUTF7Encoding(this object objValue)
        {
            if (objValue == null) return null;

            return (objValue is byte[]) ? new System.Text.UTF7Encoding().GetString((byte[])objValue) : objValue.ToString();
        }

        public static string LdapObjectToStringUTF8Encoding(this object objValue)
        {
            if (objValue == null) return null;

            return (objValue is byte[]) ? new System.Text.UTF8Encoding().GetString((byte[])objValue) : objValue.ToString();
        }

        public static T Cast<T>(this Object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                try
                {
                    propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                    value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

                    propertyInfo.SetValue(x, value, null);
                }
                catch (Exception)
                {
                }
            }
            return (T)x;
        }

        public static IEnumerable<object> Combine<T, U>(this Object one, U two)
        {
            var properties1 = one.GetType().GetProperties().Where(p => p.CanRead && p.GetValue(one, null) != null).Select(p => p.GetValue(one, null));
            var properties2 = two.GetType().GetProperties().Where(p => p.CanRead && p.GetValue(two, null) != null).Select(p => p.GetValue(two, null));

            return new List<object>(properties1.Concat(properties2));
        }

        public static void MergeWith<T>(this Object primary, T secondary)
        {
            foreach (var pi in typeof(T).GetProperties())
            {
                var priValue = pi.GetGetMethod().Invoke(primary, null);
                var secValue = pi.GetGetMethod().Invoke(secondary, null);
                if (priValue == null || (pi.PropertyType.IsValueType && priValue.Equals(Activator.CreateInstance(pi.PropertyType))))
                {
                    pi.GetSetMethod().Invoke(primary, new object[] { secValue });
                }
            }
        }

        /// <summary>
        ///   Checks if an object implements the specified interface
        /// </summary>
        /// <typeparam name = "T">The interface type</typeparam>
        /// <param name = "obj">The object to check</param>
        /// <returns>True if the interface is implemented by the object, otherwise false</returns>
        public static bool Implements<T>(this object obj)
        {
            return obj.Implements(typeof(T));
        }

        /// <summary>
        ///   Checks if an object implements the specified interface
        /// </summary>
        /// <param name = "obj">The object to check</param>
        /// <param name = "interfaceType">The interface type (can be generic, either specific or open)</param>
        /// <returns>True if the interface is implemented by the object, otherwise false</returns>
        public static bool Implements(this object obj, Type interfaceType)
        {
            // FIXME: Embrace Magnum or refactor
            /*
			Guard.AgainstNull(obj, "obj");
			 */

            Type objectType = obj.GetType();

            return objectType.Implements(interfaceType);
        }

        /// <summary>
        ///   Checks if a type implements the specified interface
        /// </summary>
        /// <typeparam name = "T">The interface type (can be generic, either specific or open)</typeparam>
        /// <param name = "objectType">The type to check</param>
        /// <returns>True if the interface is implemented by the type, otherwise false</returns>
        public static bool Implements<T>(this Type objectType)
        {
            return objectType.Implements(typeof(T));
        }

        /// <summary>
        ///   Checks if a type implements the specified interface
        /// </summary>
        /// <param name = "objectType">The type to check</param>
        /// <param name = "interfaceType">The interface type (can be generic, either specific or open)</param>
        /// <returns>True if the interface is implemented by the type, otherwise false</returns>
        public static bool Implements(this Type objectType, Type interfaceType)
        {
            // FIXME: Embrace Magnum or refactor
            /*
			Guard.AgainstNull(objectType, "objectType");
			Guard.AgainstNull(interfaceType, "interfaceType");
			*/
            //			Guard.IsTrue(x => x.IsInterface, interfaceType, "interfaceType", "Must be an interface");

            if (interfaceType.IsGenericTypeDefinition)
                return ImplementsGeneric(objectType, interfaceType);

            return interfaceType.IsAssignableFrom(objectType);
        }

        /// <summary>
        ///   Checks if a type implements an open generic at any level of the inheritance chain, including all
        ///   base classes
        /// </summary>
        /// <param name = "objectType">The type to check</param>
        /// <param name = "interfaceType">The interface type (must be a generic type definition)</param>
        /// <returns>True if the interface is implemented by the type, otherwise false</returns>
        public static bool ImplementsGeneric(this Type objectType, Type interfaceType)
        {
            Type matchedType;
            return objectType.ImplementsGeneric(interfaceType, out matchedType);
        }

        /// <summary>
        ///   Checks if a type implements an open generic at any level of the inheritance chain, including all
        ///   base classes
        /// </summary>
        /// <param name = "objectType">The type to check</param>
        /// <param name = "interfaceType">The interface type (must be a generic type definition)</param>
        /// <param name = "matchedType">The matching type that was found for the interface type</param>
        /// <returns>True if the interface is implemented by the type, otherwise false</returns>
        public static bool ImplementsGeneric(this Type objectType, Type interfaceType, out Type matchedType)
        {
            // FIXME: Embrace Magnum or refactor
            /*
			Guard.AgainstNull(objectType);
			Guard.AgainstNull(interfaceType);
			Guard.IsTrue(x => x.IsGenericType, interfaceType, "interfaceType", "Must be a generic type");
			Guard.IsTrue(x => x.IsGenericTypeDefinition, interfaceType, "interfaceType", "Must be a generic type definition");
			*/
            matchedType = null;

            if (interfaceType.IsInterface)
            {
                matchedType = objectType.GetInterfaces()
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType)
                    .FirstOrDefault();
                if (matchedType != null)
                    return true;
            }

            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == interfaceType)
            {
                matchedType = objectType;
                return true;
            }

            Type baseType = objectType.BaseType;
            if (baseType == null)
                return false;

            return baseType.ImplementsGeneric(interfaceType, out matchedType);
        }

        /// <summary>
        /// Converts object to json.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
