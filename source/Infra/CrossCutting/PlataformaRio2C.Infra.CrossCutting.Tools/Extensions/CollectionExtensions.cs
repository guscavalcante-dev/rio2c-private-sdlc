using PlataformaRio2C.Infra.CrossCutting.Tools.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Método que retorna uma query com os registros destacados no topo, baseado pelo critério.
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="query">IQueryable do tipo "T"</param>
        /// <param name="condition">Filtro que irá destacar registro em primeiro</param>
        /// <returns></returns>
        public static IQueryable<T> RiseRecords<T>(this IQueryable<T> query, Expression<Func<T, bool>> condition)
        {
            return query.OrderByDescending(condition);
        }

        public static T GetRandomItem<T>(this IEnumerable<T> collection)
        {
            return collection.Select(e => new { Guid = Guid.NewGuid(), Model = e }).OrderBy(o => o.Guid).Select(e => e.Model).FirstOrDefault();
        }


        public static IEnumerable<T> Sort<T>(this IEnumerable<T> list, string sortBy, bool asc)
        {
            var param = Expression.Parameter(typeof(T), "item");

            var sortExpression = Expression.Lambda<Func<T, object>>
                (Expression.Convert(Expression.Property(param, sortBy), typeof(object)), param);

            return asc ? list.AsQueryable<T>().OrderBy<T, object>(sortExpression) : list.AsQueryable<T>().OrderByDescending<T, object>(sortExpression);
        }

        /// <summary>
        /// Creates a list of a given type from all the rows in a DataReader.
        ///
        /// Note this method uses Reflection so this isn't a high performance
        /// operation, but it can be useful for generic data reader to entity
        /// conversions on the fly and with anonymous types.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="reader">An open DataReader that's in position to read</param>
        /// <param name="fieldsToSkip">Optional - comma delimited list of fields that you don't want to update</param>
        /// <param name="piList">
        /// Optional - Cached PropertyInfo dictionary that holds property info data for this object.
        /// Can be used for caching hte PropertyInfo structure for multiple operations to speed up
        /// translation. If not passed automatically created.
        /// </param>
        /// <returns></returns>
        public static List<TType> DataReaderToObjectList<TType>(this IDataReader reader, string fieldsToSkip, Dictionary<string, PropertyInfo> piList)
            where TType : new()
        {
            if (reader == null)
                return null;

            var items = new List<TType>();

            if (piList == null)
            {
                piList = new Dictionary<string, PropertyInfo>();
                var props = typeof(TType).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var prop in props)
                    piList.Add(prop.Name.ToLower(CultureInfo.InvariantCulture), prop);
            }

            while (reader.Read())
            {
                var inst = new TType();
                DataReaderToObject(reader, inst, fieldsToSkip, piList);
                items.Add(inst);
            }

            return items;
        }

        /// <summary>
        /// Creates a list of a given type from all the rows in a DataReader.
        ///
        /// Note this method uses Reflection so this isn't a high performance
        /// operation, but it can be useful for generic data reader to entity
        /// conversions on the fly and with anonymous types.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="reader">An open DataReader that's in position to read</param>
        /// <param name="piList">
        /// Optional - Cached PropertyInfo dictionary that holds property info data for this object.
        /// Can be used for caching hte PropertyInfo structure for multiple operations to speed up
        /// translation. If not passed automatically created.
        /// </param>
        /// <returns></returns>
        public static List<TType> DataReaderToObjectList<TType>(this IDataReader reader, Dictionary<string, PropertyInfo> piList)
            where TType : new()
        {
            return reader.DataReaderToObjectList<TType>(null, piList);
        }

        /// <summary>
        /// Creates a list of a given type from all the rows in a DataReader.
        ///
        /// Note this method uses Reflection so this isn't a high performance
        /// operation, but it can be useful for generic data reader to entity
        /// conversions on the fly and with anonymous types.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="reader">An open DataReader that's in position to read</param>
        /// <param name="fieldsToSkip">Optional - comma delimited list of fields that you don't want to update</param>
        /// </param>
        /// <returns></returns>
        public static List<TType> DataReaderToObjectList<TType>(this IDataReader reader, string fieldsToSkip)
            where TType : new()
        {
            return reader.DataReaderToObjectList<TType>(fieldsToSkip, null);
        }

        private static void DataReaderToObject(IDataReader reader, object instance, string fieldsToSkip, Dictionary<string, PropertyInfo> piList)
        {
            if (reader.IsClosed)
                throw new InvalidOperationException("O DataReader não está aberto!");

            if (string.IsNullOrEmpty(fieldsToSkip))
                fieldsToSkip = string.Empty;
            else
                fieldsToSkip = "," + fieldsToSkip + ",";

            fieldsToSkip = fieldsToSkip.ToLower(CultureInfo.InvariantCulture);

            if (piList == null)
            {
                piList = new Dictionary<string, PropertyInfo>();
                var props = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var prop in props)
                    piList.Add(prop.Name.ToLower(CultureInfo.InvariantCulture), prop);
            }

            for (int index = 0; index < reader.FieldCount; index++)
            {
                string name = reader.GetName(index).ToLower(CultureInfo.InvariantCulture);
                if (piList.ContainsKey(name))
                {
                    var prop = piList[name];

                    if (fieldsToSkip.Contains("," + name + ","))
                        continue;

                    if ((prop != null) && prop.CanWrite)
                    {
                        var val = reader.GetValue(index);
                        prop.SetValue(instance, (val == DBNull.Value) ? null : val, null);
                    }
                }
            }
            return;
        }       

        public static ICollection<T> MapToList<T>(this DbDataReader dr) where T : new()
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);
                var entities = new List<T>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                Dictionary<string, PropertyInfo> propDict = props.ToDictionary(p => p.Name.ToUpper(CultureInfo.InvariantCulture), p => p);

                while (dr.Read())
                {
                    T newObject = new T();
                    for (int index = 0; index < dr.FieldCount; index++)
                    {
                        if (propDict.ContainsKey(dr.GetName(index).ToUpper(CultureInfo.InvariantCulture)))
                        {
                            var info = propDict[dr.GetName(index).ToUpper(CultureInfo.InvariantCulture)];
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr.GetValue(index);
                                info.SetValue(newObject, (val == DBNull.Value) ? null : val, null);
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return null;
        }

        /// <summary>
        /// Método que verifica se objetos do mesmo tipo são iguais a partir das propriedades identificadoras.
        /// </summary>
        /// <typeparam name="T">Tipo dos objetos a serem comparados</typeparam>
        /// <param name="objA">Objeto A</param>
        /// <param name="objB">Objeto B</param>
        /// <param name="compareProperties">Propriedades utilizadas para diferenciar (PK/AK)</param>
        /// <returns>true se forem iguais e false do contrário</returns>
        ///
        /// EXCEPTION's: ArgumentNullException -> caso não forneça o string[] de propriedades identificadoras do objeto.
        ///              ArgumentException -> caso o objeto não tenha as propriedades informadas.
        public static bool IsSame<T>(this T objA, T objB, string[] compareProperties) where T : class
        {
            if (compareProperties == null || !compareProperties.Any()) { throw new ArgumentNullException("Compare properties can not be empty"); }

            var result = true;
            Type type = typeof(T);

            foreach (var field in compareProperties)
            {
                var prop = type.GetProperty(field);
                if (prop == null) { throw new ArgumentException(string.Format("Field {0} not exit in Type {1}", field, type.Name)); }

                if (prop.GetValue(objA) == null || prop.GetValue(objB) == null)
                {
                    result = (prop.GetValue(objA) == null && prop.GetValue(objB) == null);
                    break;
                }

                if (!prop.GetValue(objA).Equals(prop.GetValue(objB)))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Método que pega uma coleção original e atualiza a partir de uma nova coleção do mesmo tipo.
        /// Esse método somente remove os que não estão na nova coleção e adiciona os que não estão
        /// na coleção de origem.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto das coleções.</typeparam>
        /// <param name="originList">Coleção original, que será atualizada.</param>
        /// <param name="updateList">Coleção com os dados a serem atualizados na coleção original.</param>
        /// <param name="compareProperties">Propriedades que direrenciam cada objeto da coleção. Ex.: Id, Nome, CPF</param>
        ///
        /// EXCEPTION's: ArgumentNullException -> caso não forneça o string[] de propriedades identificadoras do objeto
        ///              ArgumentException -> caso o objeto não tenha as propriedades informadas.
        public static void ResolveUpdateLists<T>(this ICollection<T> originList, ICollection<T> updateList, string[] compareProperties) where T : class
        {
            if (compareProperties == null || !compareProperties.Any()) { throw new ArgumentNullException("Compare properties can not be empty"); }

            //Delete operation
            for (int i = originList.Count - 1; i > -1; i--)
            {
                if (!updateList.Any(e => originList.ElementAt(i).IsSame<T>(e, compareProperties)))
                {
                    originList.Remove(originList.ElementAt(i));
                }
            }

            //Add operation
            foreach (var item in updateList)
            {
                if (!originList.Any(e => item.IsSame<T>(e, compareProperties)))
                {
                    originList.Add(item);
                }
            }
        }

        /// <summary>
        /// Método que pega uma coleção original e atualiza a partir de uma nova coleção do mesmo tipo.
        /// Esse método é capaz de remove os que não estão na nova coleção, adiciona os que não estão
        /// na coleção de origem e atualizar os demais baseando-se no array de atributos de atualização.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto das coleções.</typeparam>
        /// <param name="originList">coleção original, que será atualizada.</param>
        /// <param name="updateList">coleção com os dados a serem atualizados na coleção original.</param>
        /// <param name="compareProperties">Propriedades que direrenciam cada objeto da coleção. Ex.: Id, Nome, CPF</param>
        /// <param name="updateProperties">Propriedades secundárias do objetos e que definem que o objeto sofreu alteração. Ex.: Endereco, Telefone</param>
        ///
        /// EXCEPTION's: ArgumentNullException -> caso não forneça o string[] de propriedades identificadoras ou o string[] de propriedades secundárias do objeto
        ///              ArgumentException -> caso o objeto não tenha as propriedades informadas.
        public static void ResolveUpdateLists<T>(this ICollection<T> originList, ICollection<T> updateList, string[] compareProperties, string[] updateProperties) where T : class
        {
            if (compareProperties == null || !compareProperties.Any()) { throw new ArgumentNullException("Compare properties can not be empty"); }

            //Delete operation
            for (int i = originList.Count - 1; i > -1; i--)
            {
                if (!updateList.Any(e => originList.ElementAt(i).IsSame<T>(e, compareProperties)))
                {
                    originList.Remove(originList.ElementAt(i));
                }
            }

            //Add operation
            foreach (var item in updateList)
            {
                if (!originList.Any(e => item.IsSame<T>(e, compareProperties)))
                {
                    originList.Add(item);
                }
            }

            //Update properties
            foreach (var originElement in originList)
            {
                var updateElement = updateList.FirstOrDefault(e => e.IsSame<T>(originElement, compareProperties));
                foreach (var propertyName in updateProperties)
                {
                    var property = originElement.GetType().GetProperty(propertyName);
                    if (property != null)
                    {
                        property.SetValue(originElement, updateElement.GetType().GetProperty(propertyName).GetValue(updateElement));
                    }
                }
            }
        }

        /// <summary>
        /// Método que retona um enumerado de objetos deletados em uma nova versão da mesma lista.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto das coleções.</typeparam>
        /// <param name="originList">coleção original.</param>
        /// <param name="updateList">coleção atualizada.</param>
        /// <param name="compareProperties">Propriedades que direrenciam cada objeto da coleção. Ex.: Id, Nome, CPF</param>
        ///
        /// EXCEPTION's: ArgumentNullException -> caso não forneça o string[] de propriedades identificadoras ou o string[] de propriedades secundárias do objeto
        ///              ArgumentException -> caso o objeto não tenha as propriedades informadas.
        ///
        /// <returns>Coleção dos item que faziam parte da coleção de origem e não constam mais na coleção atualizada.</returns>
        // [MyAspect(ApplyToStateMachine = true)]
        //[IgnoreWarning("LA0134")]
        public static IEnumerable<T> GetDeleted<T>(this ICollection<T> originList, ICollection<T> updateList, string[] compareProperties) where T : class
        {
            if (compareProperties == null || !compareProperties.Any()) { throw new ArgumentNullException("Compare properties can not be empty"); }

            for (int i = originList.Count - 1; i > -1; i--)
            {
                if (!updateList.Any(e => originList.ElementAt(i).IsSame<T>(e, compareProperties)))
                {
                    yield return originList.ElementAt(i);
                }
            }
        }

        /// <summary>
        /// Extension Method de Collection capaz de montar uma arvore com um nível de hierarquia com duas listas do mesmo tipo de objeto.
        /// Método pensado para montar a arvore de permissão no formato Grupo de Permissão -> Permissão.
        /// O Objeto T deve ser um DTO capaz de armazenar informações de ambos objetos (grupo e permissão)
        /// </summary>
        /// <typeparam name="T">DTO -> Ex.: EnumItem</typeparam>
        /// <param name="itensList">Coleção de DTO contendo Permissões</param>
        /// <param name="groupList">Coleção de DTO contendo Grupos de Permissões</param>
        /// <param name="compareItemProperty">Nome da propriedade FK no DTO de Permissão</param>
        /// <param name="compareGroupProperty">Nome da propriedade PK no DTO de Grupo de Permissão</param>
        /// <returns>Dicionário<GrupoPermissão, Lista(Permissão)></returns>
        public static IDictionary<T, IList<T>> NestItems<T>(this ICollection<T> itensList, ICollection<T> groupList, string compareItemProperty, string compareGroupProperty) where T : class
        {
            if (string.IsNullOrEmpty(compareItemProperty) || string.IsNullOrEmpty(compareGroupProperty)) { throw new ArgumentNullException("Compare properties can not be empty"); }

            Type type = typeof(T);

            var propGroup = type.GetProperty(compareGroupProperty);
            if (propGroup == null) { throw new ArgumentException(string.Format("Property {0} not exit in Type {1}", compareGroupProperty, type.Name)); }

            var propItem = type.GetProperty(compareItemProperty);
            if (propItem == null) { throw new ArgumentException(string.Format("Property {0} not exit in Type {1}", compareItemProperty, type.Name)); }

            if (propGroup.PropertyType != propItem.PropertyType) { throw new ArgumentException(string.Format("properties {0} and {1} must be the same type", compareGroupProperty, compareItemProperty)); }

            var tree = new Dictionary<T, IList<T>>();

            foreach (var group in groupList)
            {
                var propGroupValue = propGroup.GetValue(group);

                if (propGroupValue != null)
                {
                    var groupItens = new List<T>();

                    foreach (var item in itensList)
                    {
                        var propItemValue = propItem.GetValue(item);

                        if (propGroupValue == propItemValue)
                        {
                            groupItens.Add(item);
                        }
                    }
                    tree.Add(group, groupItens);
                }
            }

            return tree;
        }

        /// <summary>
        /// Método que retorna um enumerable de SelectListItemParent a partir da coleção de tipo Tipo qualquer.
        /// </summary>
        /// <typeparam name="E">Tipo do objeto da coleção.</typeparam>
        /// <typeparam name="T">Tipo do campo de texto no objeto da coleção.</typeparam>
        /// <typeparam name="V">Tipo do campo de valor no objeto da coleção.</typeparam>
        /// <param name="items">Coleção de objetos do tipo T.</param>
        /// <param name="dataTextField">Função para identificar o campo de texto.</param>
        /// <param name="dataValueField">Função para identificar o campo de valor.</param>
        /// <param name="selectedValue">Valor do item que deve vir como selecionado.</param>
        /// <example>"ListaDeExemplos.ToSelectList(e => e.PropriedadeA, e => e.PropriedadeB, 1);".</example>
        /// <see cref="SelectListItemParent"/>
        /// <returns>IEnumerable de SelectListItemParent</returns>
        //[IgnoreWarning("LA0134")]
        public static IEnumerable<SelectListItemParent> ToSelectList<E, T, V>(this IEnumerable<E> items, Expression<Func<E, V>> dataValueField, Expression<Func<E, T>> dataTextField, object selectedValue, string startItemText = null, string startItemValue = null)
        {
            //type from collection
            var type = typeof(E);

            //type from selectedValue
            var selectedValueType = selectedValue.GetType();

            //PropertyInfo from dataTextField
            var textPropertyInfo = GetProperty(dataTextField);

            //PropertyInfo from dataValueField
            var valuePropertyInfo = GetProperty(dataValueField);

            if (!string.IsNullOrEmpty(startItemText) && !string.IsNullOrEmpty(startItemValue))
            {
                yield return new SelectListItemParent() { Text = startItemText, Selected = selectedValue.ToString() == startItemValue, Value = startItemValue };
            }

            foreach (var item in items)
            {
                var dataText = textPropertyInfo.GetValue(item); //Value to SelectListItem.Text
                var dataValue = valuePropertyInfo.GetValue(item); //Value to SelectListItem.Value
                bool selected = object.Equals(dataValue, selectedValue); //Value to SelectListItem.Selected

                yield return new SelectListItemParent() { Text = dataText.ToString(), Selected = selected, Value = dataValue.ToString() };
            }
        }

        /// <summary>
        /// Método que retorna um enumerable de SelectListItemParent a partir da coleção de tipo Tipo qualquer.
        /// Ao usar o parente você consegue especificar uma estrutura de grupo, onde o parente representa o nó superior.
        /// O campo de parente pode ser do tipo enum, nesse caso o returno será o valor numérico do mesmo.
        /// </summary>
        /// <typeparam name="E">Tipo do objeto da coleção.</typeparam>
        /// <typeparam name="T">Tipo do campo de texto no objeto da coleção.</typeparam>
        /// <typeparam name="V">Tipo do campo de valor no objeto da coleção.</typeparam>
        /// <typeparam name="P">Tipo do campo de que representa o parente.</typeparam>
        /// <param name="items">Coleção de objetos do tipo T.</param>
        /// <param name="dataTextField">Função para identificar o campo de texto.</param>
        /// <param name="dataValueField">Função para identificar o campo de valor.</param>
        /// <param name="dataParentValueField">Função para identificar o campo de parente.</param>
        /// <param name="selectedValue">Valor do item que deve vir como selecionado.</param>
        /// <example>"ListaDeExemplos.ToSelectList(e => e.PropriedadeA, e => e.PropriedadeB, e => e.Tipo, 1);".</example>
        /// <see cref="SelectListItemParent"/>
        /// <returns>IEnumerable de SelectListItemParent</returns>
        //[IgnoreWarning("LA0134")]
        public static IEnumerable<SelectListItemParent> ToSelectList<E, T, V, P>(this IEnumerable<E> items, Expression<Func<E, V>> dataValueField, Expression<Func<E, T>> dataTextField, Expression<Func<E, P>> dataParentValueField, object selectedValue, string startItemText = null, string startItemValue = null)
        {
            //type from collection
            var type = typeof(E);

            //type from selectedValue
            var selectedValueType = selectedValue.GetType();

            //PropertyInfo from dataTextField
            var textPropertyInfo = GetProperty(dataTextField);

            //PropertyInfo from dataValueField
            var valuePropertyInfo = GetProperty(dataValueField);

            //PropertyInfo from dataParentValueField
            var parentValuePropertyInfo = GetProperty(dataParentValueField);

            if (!string.IsNullOrEmpty(startItemText) && !string.IsNullOrEmpty(startItemValue))
            {
                yield return new SelectListItemParent() { Text = startItemText, Selected = selectedValue.ToString() == startItemValue, Value = startItemValue };
            }

            foreach (var item in items)
            {
                var dataText = textPropertyInfo.GetValue(item); //Value to SelectListItemParent.Text
                var dataValue = valuePropertyInfo.GetValue(item); //Value to SelectListItemParent.Value
                bool selected = object.Equals(dataValue, selectedValue); //Value to SelectListItemParent.Selected
                object parentValue = parentValuePropertyInfo.PropertyType.IsEnum ? (int)parentValuePropertyInfo.GetValue(item) : parentValuePropertyInfo.GetValue(item); //Value to SelectListItemParent.ParentValue
                yield return new SelectListItemParent() { Text = dataText.ToString(), Selected = selected, ParentValue = parentValue.ToString(), Value = dataValue.ToString() };
            }
        }

        /// <summary>
        /// Método que mapea uma coleção de um objeto composto (uma viewmodel por exemplo) em uma coleção de array de string.
        /// Cada objeto é mapeado e adicionado a coleção resultante.
        /// Onde mapear é passar o valor de cada propriedade para a array, convertendo o mesmo para string.
        /// </summary>
        /// <typeparam name="T">Tipo da coleção</typeparam>
        /// <param name="objects">Coleção</param>
        /// <seealso cref="ObjectToArrayString()"/>
        /// <returns>Coleção de array de strings</returns>
        //[IgnoreWarning("LA0134")]
        public static IEnumerable<string[]> CollectionObjectToArrayString<T>(this IEnumerable<T> objects)
        {
            foreach (var obj in objects)
            {
                yield return obj.ObjectToArrayString();
            }
        }

        /// <summary>
        /// Método que retorna a coleção serializada em xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string SerializeColletion<T>(this IEnumerable<T> collection)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<T>));
            using (MemoryStream ms = new MemoryStream())
            {
                xs.Serialize(ms, collection.ToList<T>());
                return UTF8Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// Método que percorre uma coleção e seta a propriedade especificada via expression func como true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="colecao"></param>
        /// <param name="valueProperty"></param>
        public static void SelecionarTodos<T, P>(this IEnumerable<T> colecao, Expression<Func<T, P>> valueProperty)
        {
            var property = valueProperty.GetProperty();
            foreach (var item in colecao)
            {
                property.SetValue(item, true);
            }
        }

        private static PropertyInfo GetProperty<T, P>(Expression<Func<T, P>> valueProperty)
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

        /// <summary>
        /// Método de extensão para atualizar uma lista baseada em uma nova lista do mesmo tipo,
        /// usando uma propriedade chave para comparar os elementos da lista.        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="originList">Lista de Origem</param>
        /// <param name="updateList">Lista Nova</param>
        /// <param name="compareProperty">Propriedade Chave para comparação dos elementos</param>
        /// <remarks>Esse método realiza remoção e adição de itens</remarks>
        public static void ResolveUpdateLists<T, P>(this ICollection<T> originList, ICollection<T> updateList, Expression<Func<T, P>> compareProperty) where T : class
        {
            //Delete operation
            for (int i = originList.Count - 1; i > -1; i--)
            {
                if (!updateList.Any(e => originList.ElementAt(i).IsEquals(e, compareProperty)))
                {
                    originList.Remove(originList.ElementAt(i));
                }
            }

            //Add operation
            foreach (var item in updateList)
            {
                if (!originList.Any(e => item.IsEquals(e, compareProperty)))
                {
                    originList.Add(item);
                }
            }
        }

        /// <summary>
        /// Método para comparar se objetos tem a mesma propriedade chave
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="ObjectA">Objeto Origem</param>
        /// <param name="ObjectB">Objeto Comparação</param>
        /// <param name="compareProperty">Propriedade Chave</param>
        /// <remarks>Se a propriedade chave for nula em um ou em ambos os objetos o retorno será false</remarks>
        /// <returns></returns>
        public static bool IsEquals<T, P>(this T ObjectA, T ObjectB, Expression<Func<T, P>> compareProperty)
        {
            var prop = GetProperty(compareProperty);
            var value1 = prop.GetValue(ObjectA);
            var value2 = prop.GetValue(ObjectB);
            if (value1 == null || value2 == null)
            {
                return false;
            }
            return !value1.Equals(value2);
        }

        public static DataTable GetDataTable<T, K>(this IEnumerable<T> itens, Expression<Func<T, K>> keyProperty, string columnName = "ID")
        {
            var propertyInfo = GetProperty(keyProperty);
            var keyType = typeof(K);
            var table = new DataTable();

            table.Columns.Add(columnName, keyType);

            foreach (var item in itens)
            {
                table.Rows.Add((K)propertyInfo.GetValue(item));
            }

            return table;
        }

        public static DataTable GetDataTable<T>(this IEnumerable<T> itens, string columnName = "ID")
        {
            var keyType = typeof(T);
            var table = new DataTable();

            table.Columns.Add(columnName, keyType);

            foreach (var item in itens)
            {
                table.Rows.Add(item);
            }

            return table;
        }

        public static DataTable ConvertListToDataTable<T>(this IEnumerable<T> list, IEnumerable<string> IgnorePropertyList = null)
        {
            var table = new DataTable();
            var properties = typeof(T).GetProperties();

            if (IgnorePropertyList != null)
            {
                properties = properties.Where(e => !IgnorePropertyList.Contains(e.Name)).ToArray();
            }

            table.Clear();

            foreach (var property in properties)
            {
                Type columnType = property.PropertyType.IsEnum ? typeof(System.Int32) : property.PropertyType.IsNullable() ? Nullable.GetUnderlyingType(property.PropertyType) : property.PropertyType;
                DataColumn column = new DataColumn(property.Name, columnType);
                column.AllowDBNull = columnType == typeof(String) ? true : property.PropertyType.IsNullable();
                table.Columns.Add(column);
            }

            foreach (var obj in list)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(obj, null);
                }

                table.Rows.Add(values);
            }

            return table;
        }
    }
}
