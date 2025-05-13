using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class EnumExtensions
    {
        #region Public Methods        

        public static ICollection<SystemTextDescriptionDto> SystemTextDescriptions<T>(this T enumerator)
        {
            //SystemTextDescriptionAttribute
            var enumType = enumerator.GetType();
            var result = new List<SystemTextDescriptionDto>();

            if (!enumType.IsEnum) return result;

            var names = Enum.GetNames(enumType);

            if (names.Length > 0)
            {
                var itemEnumType = enumType.Assembly.GetType(enumType.FullName);
                foreach (var item in names)
                {
                    var enumItem1 = itemEnumType.GetField(item);
                    var intValue = (int)enumItem1.GetValue(itemEnumType);
                    var itemInstance = (T)Enum.ToObject(enumType, intValue);
                    var systemTextAttributes = itemInstance.GetType().GetField(item).GetCustomAttributes(typeof(SystemTextDescriptionAttribute), false);
                    var stringValue = enumItem1.GetValue(itemEnumType).ToString();

                    if (systemTextAttributes.Any())
                    {
                        foreach (var subItem in systemTextAttributes)
                        {
                            var dto = ((SystemTextDescriptionAttribute)subItem).GetDto();
                            dto.Code = stringValue;
                            result.Add(dto);
                        }
                    }
                }
            }

            return result;
        }

        public static ICollection<SystemMessageDescriptionDto> SystemMessageDescriptions<T>(this T enumerator)
        {
            var enumType = enumerator.GetType();
            var result = new List<SystemMessageDescriptionDto>();

            if (!enumType.IsEnum) return result;

            var names = Enum.GetNames(enumType);

            if (names.Length > 0)
            {
                var itemEnumType = enumType.Assembly.GetType(enumType.FullName);
                foreach (var item in names)
                {
                    var enumItem1 = itemEnumType.GetField(item);
                    var intValue = (int)enumItem1.GetValue(itemEnumType);
                    var itemInstance = (T)Enum.ToObject(enumType, intValue);
                    var systemMessageAttributes = itemInstance.GetType().GetField(item).GetCustomAttributes(typeof(SystemMessageDescriptionAttribute), false);
                    var stringValue = enumItem1.GetValue(itemEnumType).ToString();

                    if (systemMessageAttributes.Any())
                    {
                        foreach (var subItem in systemMessageAttributes)
                        {
                            var dto = ((SystemMessageDescriptionAttribute)subItem).GetDto();
                            dto.Code = stringValue;
                            result.Add(dto);
                        }
                    }
                }
            }

            return result;
        }

        public static string GetResourceDescriptionFromCompareAttribute<T>(this T enumerator, string value, ResourceManager resourceManager)
        {
            var enumType = enumerator.GetType();

            if (!enumType.IsEnum) return value;

            var names = Enum.GetNames(enumType);

            if (names.Length > 0)
            {
                var itemEnumType = enumType.Assembly.GetType(enumType.FullName);

                foreach (var item in names)
                {
                    var enumItem1 = itemEnumType.GetField(item);
                    var intValue = (int)enumItem1.GetValue(itemEnumType);
                    var itemInstance = (T)Enum.ToObject(enumType, intValue);
                    var namesAttributes = itemInstance.GetType().GetField(item).GetCustomAttributes(typeof(NameFromResourceAttribute), false);

                    if (namesAttributes.Any())
                    {
                        var attrib = (NameFromResourceAttribute)namesAttributes.First();
                        if (attrib != null && !string.IsNullOrEmpty(attrib.CompareName))
                        {
                            if (attrib.CompareName.ToLower(CultureInfo.InvariantCulture) == value.ToLower(CultureInfo.InvariantCulture))
                            {
                                return resourceManager.GetString(attrib.Name);
                            }
                        }
                    }
                }
            }

            return value;
        }

        public static string GetResourceDescription<T>(this T enumerator, string value, ResourceManager resourceManager)
        {
            var enumType = enumerator.GetType();

            if (!enumType.IsEnum) return value;

            var field = enumType.GetField(value);
            if (field != null)
            {
                var customAttributes = field.GetCustomAttributes(typeof(NameFromResourceAttribute), false);
                if (customAttributes.Length > 0)
                {
                    var keyValue = (customAttributes[0] as NameFromResourceAttribute).Name;
                    return resourceManager.GetString(keyValue);
                }
            }

            return value;
        }

        public static string GetResourceDescription(this Enum value, ResourceManager resourceManager)
        {
            var result = resourceManager.GetString(value.ToDescription());
            return result ?? string.Empty;
        }

        public static IList<EnumItem> GetCustomDescriptions<T>(this T enumerator, ResourceManager resourceManager, int index, bool sort)
        {
            List<EnumItem> result = null;
            var enumType = enumerator.GetType();

            if (!enumType.IsEnum)
                return result;

            var names = Enum.GetNames(enumType);

            if (names.Length > 0)
            {
                result = new List<EnumItem>(names.Length);
                var itemEnumType = enumType.Assembly.GetType(enumType.FullName);

                foreach (var name in names)
                {
                    var enumItem1 = itemEnumType.GetField(name);
                    var intValue = (int)enumItem1.GetValue(itemEnumType);
                    var itemInstance = (T)Enum.ToObject(enumType, intValue);
                    var namesAttributes = itemInstance.GetType().GetField(name).GetCustomAttributes(typeof(CustomDescriptionAttribute), false);

                    string description = null;

                    if (namesAttributes.Any())
                    {
                        foreach (var attribute in namesAttributes)
                        {
                            if (((CustomDescriptionAttribute)attribute).Index == index)
                            {
                                description = resourceManager.GetString(((CustomDescriptionAttribute)attribute).Description);
                                break;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(description)) description = name;

                    result.Add(new EnumItem { Id = intValue, Value = name, Description = description });
                }
            }

            if (sort && result != null)
            {
                // Cria um método anônimo para fazer a comparação entre os itens da lista e ordená-la pela descrição
                result.Sort((enum1, enum2) => enum1.Description.CompareTo(enum2.Description));
            }

            return result;
        }

        public static IList<EnumItem> GetCustomDescriptions<T>(this T enumerator, ResourceManager resourceManager, int index)
        {
            return enumerator.GetCustomDescriptions<T>(resourceManager, index, false);
        }

        public static IList<EnumItem> GetCustomDescriptions<T>(this T enumerator, ResourceManager resourceManager, bool sort)
        {
            return enumerator.GetCustomDescriptions<T>(resourceManager, 0, sort);
        }

        /// <summary>
        /// Método de extensão que enumerable de SelectListItem com os dados de um enum, inclusive com as descriptions em resource
        /// </summary>
        /// <param name="value">Enum</param>
        /// <param name="resourceManager">Arquivo de resource com as descriptions do enum em questão.</param>
        /// <param name="defaultValue">Valor da seleção ou seja o item do enum que deve vim como select.</param>
        /// <param name="order">Para que os itens venham ordenados por ordem alfabética das descriptions.</param>
        /// <returns></returns>
        //[IgnoreWarning("LA0134")]
        public static IEnumerable<SelectListItem> ToSelectList(this Enum value, ResourceManager resourceManager, int defaultValue = -1, bool order = false)
        {
            foreach (var item in value.ToEnumDescriptions(order, resourceManager))
            {
                yield return new SelectListItem { Text = item.Description, Value = item.Id.ToString(), Selected = (item.Id == defaultValue) };
            }
        }

        /// <summary>
        /// Método que faz a conversão de uma coleção de strings nos itens do enum com o mesmo valor
        /// </summary>
        /// <typeparam name="E">Tipo do Enum resultante</typeparam>
        /// <param name="collection">Coleção de strings com os values do enum.</param>
        /// <returns></returns>
        //[IgnoreWarning("LA0134")]
        public static IEnumerable<E> Convert<E>(this IEnumerable<string> collection)
        {
            foreach (string item in collection)
            {
                yield return (E)Enum.Parse(typeof(E), item);
            }
        }

        /// <summary>
        /// Extension Method que retorna o valor contido no resource informado, onde a chave par no resource
        /// é definido no atributo "Description" do item de qualquer Enum. Caso o item não contenha o atributo "Description",
        /// o valor original do item é exibido.
        /// </summary>
        ///
        /// <param name="value">O item do qual obter a descrição</param>
        /// <param name="resourceManager">Arquivo de resource onde as descriptions do enum estão traduzidas</param>
        /// <returns>O valor armazenado no resource, amarrado ao valor do atributo "Description" ou o próprio valor do Enum</returns>
        ///
        /// <remarks>
        /// Útil para ser utilizado na camada de interface
        /// para exibição de valores amigáveis para os Enum's, centralizando o
        /// "De - Para" dos Enums para suas respectivas descrições.<br/><br/>
        ///
        /// Ex.: No seguinte Enum:<br/><br/>
        ///
        /// <code>
        /// enum Situacao
        /// {
        ///     Aberto,
        ///
        ///     Fechado,
        ///
        ///     [Description("Em andamento")]
        ///     EmAndamento,
        ///
        ///     EmAtraso
        /// }
        /// </code>
        ///
        /// <br/>
        /// Teremos o seguinte comportamento:<br/><br/>
        ///
        /// <code>
        /// Situacao.Aberto.ToDescription();        => Retorna "Aberto"
        /// Situacao.Fechado.ToDescription();       => Retorna "Fechado"
        /// Situacao.EmAndamento.ToDescription();   => Retorna "Em andamento"
        /// Situacao.EmAtraso.ToDescription();      => Retorna "EmAtraso"
        /// </code>
        /// </remarks>
        public static IList<EnumItem> ToEnumDescriptions(this Enum value, bool sort, ResourceManager resourceManager)
        {
            var list = GetListEnumItens(value, sort, true);
            foreach (var item in list)
            {
                var resourceValue = resourceManager.GetString(item.Description);
                if (!string.IsNullOrEmpty(resourceValue))
                {
                    item.Description = resourceValue;
                }
            }
            return list;
        }

        /// <summary>
        /// Extension Method que retorna o valor definido no atributo "Description"
        /// do item de qualquer Enum. Caso o item não contenha o atributo "Description",
        /// o valor do item é exibido.
        /// </summary>
        ///
        /// <param name="value">O item do qual obter a descrição</param>
        /// <returns>O valor do atributo "Description" ou o próprio valor do Enum</returns>
        ///
        /// <remarks>
        /// Útil para ser utilizado na camada de interface
        /// para exibição de valores amigáveis para os Enum's, centralizando o
        /// "De - Para" dos Enums para suas respectivas descrições.<br/><br/>
        ///
        /// Ex.: No seguinte Enum:<br/><br/>
        ///
        /// <code>
        /// enum Situacao
        /// {
        ///     Aberto,
        ///
        ///     Fechado,
        ///
        ///     [Description("Em andamento")]
        ///     EmAndamento,
        ///
        ///     EmAtraso
        /// }
        /// </code>
        ///
        /// <br/>
        /// Teremos o seguinte comportamento:<br/><br/>
        ///
        /// <code>
        /// Situacao.Aberto.ToDescription();        => Retorna "Aberto"
        /// Situacao.Fechado.ToDescription();       => Retorna "Fechado"
        /// Situacao.EmAndamento.ToDescription();   => Retorna "Em andamento"
        /// Situacao.EmAtraso.ToDescription();      => Retorna "EmAtraso"
        /// </code>
        /// </remarks>
        public static string ToDescription(this Enum value)
        {
            return GetDescription(value);
        }

        /// <summary>
        /// Extension Method que retorna o valor definido no atributo "Description"
        /// do item de qualquer Enum. Caso o item não contenha o atributo "Description",
        /// o valor do item é exibido.
        /// </summary>
        ///
        /// <param name="value">O item do qual obter a descrição</param>
        /// <param name="resourceManager">Arquivo de resource onde as descriptions do enum estão traduzidas</param>
        /// <returns>O valor do atributo "Description" ou o próprio valor do Enum</returns>
        ///
        /// <remarks>
        /// Útil para ser utilizado na camada de interface
        /// para exibição de valores amigáveis para os Enum's, centralizando o
        /// "De - Para" dos Enums para suas respectivas descrições.<br/><br/>
        ///
        /// Ex.: No seguinte Enum:<br/><br/>
        ///
        /// <code>
        /// enum Situacao
        /// {
        ///     Aberto,
        ///
        ///     Fechado,
        ///
        ///     [Description("Em andamento")]
        ///     EmAndamento,
        ///
        ///     EmAtraso
        /// }
        /// </code>
        ///
        /// <br/>
        /// Teremos o seguinte comportamento:<br/><br/>
        ///
        /// <code>
        /// Situacao.Aberto.ToDescription();        => Retorna "Aberto"
        /// Situacao.Fechado.ToDescription();       => Retorna "Fechado"
        /// Situacao.EmAndamento.ToDescription();   => Retorna "Em andamento"
        /// Situacao.EmAtraso.ToDescription();      => Retorna "EmAtraso"
        /// </code>
        /// </remarks>
        public static string ToDescription(this Enum value, ResourceManager resourceManager)
        {
            var description = GetDescription(value);
            var resourceDescription = resourceManager.GetString(description);
            return resourceDescription ?? description;
        }

        /// <summary>
        /// Extension Method que retorna a lista de itens existentes em um Enum,
        /// em um formato que pode ser feito "Bind" com controles Data Bound.
        /// Esse formato é um DTO (<seealso cref="EnumItem">EnumItensDTO</seealso>)
        /// contendo o código do item (inteiro) e a descrição do item. Caso o item não
        /// contenha o atributo "Description", o valor do item é exibido. A lista é
        /// retornada ordenada pela descrição.
        /// </summary>
        ///
        /// <param name="value">O item do qual obter a descrição</param>
        /// <returns>Uma lista de <see cref="EnumItem"/> contendo os valores do enumerado</returns>
        ///
        /// <remarks>
        /// Útil para ser utilizado na camada de interface como DataSource para
        /// DropDownList's, por exemplo, exibindo valores amigáveis na mesma.<br/><br/>
        ///
        /// Ex.: No seguinte Enum:<br/><br/>
        ///
        /// <code>
        /// enum Situacao
        /// {
        ///     Aberto,
        ///
        ///     Fechado,
        ///
        ///     [Description("Em andamento")]
        ///     EmAndamento,
        ///
        ///     EmAtraso
        /// }
        /// </code>
        ///
        /// <br/>
        /// Teremos o seguinte comportamento:<br/><br/>
        ///
        /// <code>
        /// DropDownList ddlSituacao;
        ///
        /// ddlSituacao.DataValueField = "Id";
        /// ddlSituacao.DataTextField = "Description";
        /// ddlSituacao.DataSource = default(Situacao).ToEnumDescriptions();
        /// ddlSituacao.DataBind();
        ///
        /// => A DropDownList exibirá os itens:
        ///     &lt;option value="0"&gt;Aberto&lt;/option&gt;
        ///     &lt;option value="2"&gt;Em andamento&lt;/option&gt;
        ///     &lt;option value="3"&gt;EmAtraso&lt;/option&gt;
        ///     &lt;option value="1"&gt;Fechado&lt;/option&gt;
        /// </code>
        /// </remarks>
        public static IList<EnumItem> ToEnumDescriptions(this Enum value)
        {
            return GetListEnumItens(value, true, true);
        }

        /// <summary>
        /// Extension Method que retorna a lista de itens existentes em um Enum,
        /// em um formato que pode ser feito "Bind" com controles Data Bound.
        /// Esse formato é um DTO (<seealso cref="EnumItem">EnumItemDTO</seealso>)
        /// contendo o código do item (inteiro) e a descrição do item. Caso o item não
        /// contenha o atributo "Description", o valor do item é exibido. A lista é
        /// retornada ordenada pela descrição.
        /// </summary>
        ///
        /// <param name="value">O item do qual obter a descrição</param>
        /// <param name="sort">Indica se a lista será ordenada por descrição</param>
        /// <returns>Uma lista de <see cref="EnumItem"/> contendo os valores do enumerado</returns>
        ///
        /// <remarks>
        /// Útil para ser utilizado na camada de interface como DataSource para
        /// DropDownList's, por exemplo, exibindo valores amigáveis na mesma.<br/><br/>
        ///
        /// Ex.: No seguinte Enum:<br/><br/>
        ///
        /// <code>
        /// enum Situacao
        /// {
        ///     Aberto,
        ///
        ///     Fechado,
        ///
        ///     [Description("Em andamento")]
        ///     EmAndamento,
        ///
        ///     EmAtraso
        /// }
        /// </code>
        ///
        /// <br/>
        /// Teremos o seguinte comportamento:<br/><br/>
        ///
        /// <code>
        /// DropDownList ddlSituacao;
        ///
        /// ddlSituacao.DataValueField = "Id";
        /// ddlSituacao.DataTextField = "Description";
        /// ddlSituacao.DataSource = default(Situacao).ToEnumDescriptions(false);
        /// ddlSituacao.DataBind();
        ///
        /// => A DropDownList exibirá os itens:
        ///     &lt;option value="0"&gt;Aberto&lt;/option&gt;
        ///     &lt;option value="1"&gt;Fechado&lt;/option&gt;
        ///     &lt;option value="2"&gt;Em andamento&lt;/option&gt;
        ///     &lt;option value="3"&gt;EmAtraso&lt;/option&gt;
        /// </code>
        /// </remarks>
        public static IList<EnumItem> ToEnumDescriptions(this Enum value, bool sort)
        {
            return GetListEnumItens(value, sort, true);
        }

        /// <summary>
        /// Extension Method que retorna a lista de String's de um enumerado
        /// </summary>
        /// <param name="value">O enumerado a ser utilizado</param>
        /// <returns>Uma lista de <see cref="EnumItem"/> contendo os valores do enumerado</returns>
        public static IList<EnumItem> ToEnumStrings(this Enum value)
        {
            return GetListEnumItens(value, true);
        }

        /// <summary>
        /// Extension Method que retorna a lista de <see cref="EnumItem"/> de um enumerado.
        /// </summary>
        /// <param name="value">O enumerado a ser utilizado</param>
        /// <param name="sort">Informa se a lista deverá ser ordenada alfabeticamente, pelo valor da string do enumerado.</param>
        /// <returns>Uma lista de <see cref="EnumItem"/> contendo os valores do enumerado</returns>
        public static IList<EnumItem> ToEnumStrings(this Enum value, bool sort)
        {
            return GetListEnumItens(value, sort);
        }

        /// <summary>
        /// Extension Method que retorna a lista de <see cref="EnumItem"/> de um enumerado.
        /// </summary>
        /// <param name="value">O enumerado a ser utilizado</param>
        /// <returns>Uma lista de <see cref="EnumItem"/> contendo os valores do enumerado</returns>
        public static IList<EnumItem> ToEnumValues(this Enum value)
        {
            return ToEnumValues(value, true);
        }

        /// <summary>
        /// Extension Method que retorna a lista de <see cref="EnumItem"/> de um enumerado.
        /// </summary>
        /// <param name="value">O enumerado a ser utilizado</param>
        /// <param name="sort">Informa se a lista deverá ser ordenada pelo Valor (ID) do enumerado.</param>
        /// <returns>Uma lista de <see cref="EnumItem"/> contendo os valores do enumerado</returns>
        public static IList<EnumItem> ToEnumValues(this Enum value, bool sort)
        {
            var list = GetListEnumItens(value, sort);

            if (sort && (list != null && list.Count() > 0))
            {
                list.ToList().Sort((enum1, enum2) => enum1.Id.CompareTo(enum2.Id));
            }

            return list;
        }

        /// <summary>
        /// Extension Method que converte uma string em um item de um enumerado
        /// </summary>
        /// <remarks>
        /// Ex.: No seguinte Enum:<br/><br/>
        ///
        /// <code>
        /// enum Situacao
        /// {
        ///     Aberto,
        ///     Fechado,
        ///     EmAndamento,
        ///     EmAtraso
        /// }
        /// </code>
        ///
        /// <br/>
        /// Teremos o seguinte comportamento:<br/><br/>
        ///
        /// <code>
        /// string valor = "Aberto";
        /// Situacao valorConvertido = default(Situacao).ParseToEnum(valor); // Terá o valor Aberto do enum Situacao
        /// </code>
        /// </remarks>
        /// <typeparam name="T">O tipo do enumerado</typeparam>
        /// <param name="value">O valor a ser convertido</param>
        /// <returns>Um item do enumerado informado</returns>
        public static T ParseToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Método que retorna um item do enum T baseado no int value dele.
        /// </summary>
        /// <typeparam name="T">Tipo do enum.</typeparam>
        /// <param name="value">Int value do item do enum.</param>
        /// <returns></returns>
        public static T ParseToEnum<T>(this int value)
        {
            return (T)Enum.Parse(typeof(T), value.ToString());
        }

        /// <summary>
        /// Método que retorna um item do enum T baseado no int value dele.
        /// Aceitando que o int possa ser null, nesse caso o retorno será null também!
        /// </summary>
        /// <typeparam name="T">Tipo do enum.</typeparam>
        /// <param name="value">Int value do item do enum.</param>
        /// <returns></returns>
        public static T? ParseToEnum<T>(this int? value) where T : struct
        {
            return value == null ? null : (T?)Enum.Parse(typeof(T), value.ToString());
        }

        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static T ParseToEnumFromNameFromResourceAttribute<T>(this Enum enumVal, string compareString) where T : struct
        {
            var enumType = enumVal.GetType();

            if (!enumType.IsEnum) return default(T);

            var names = Enum.GetNames(enumType);

            if (names.Length > 0)
            {
                var itemEnumType = enumType.Assembly.GetType(enumType.FullName);

                foreach (var item in names)
                {
                    var enumItem1 = itemEnumType.GetField(item);
                    var intValue = (int)enumItem1.GetValue(itemEnumType);
                    var itemInstance = (T)Enum.ToObject(enumType, intValue);
                    var namesAttributes = itemInstance.GetType().GetField(item).GetCustomAttributes(typeof(NameFromResourceAttribute), false);

                    if (namesAttributes.Any())
                    {
                        var attrib = (NameFromResourceAttribute)namesAttributes.First();
                        if (attrib.CompareName.ToLower(CultureInfo.InvariantCulture) == compareString.ToLower(CultureInfo.InvariantCulture))
                        {
                            return itemInstance;
                        }
                    }
                }
            }
            return default(T);
        }

        #endregion

        #region Private Members

        private const char EnumSeparatorCharacter = ',';

        private static string GetDescription(Enum value)
        {
            // Check for Enum that is marked with FlagAttribute
            var entries = value.ToString().Split(EnumSeparatorCharacter);
            var description = new string[entries.Length];

            for (var i = 0; i < entries.Length; i++)
            {
                var fieldInfo = value.GetType().GetField(entries[i].Trim());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                description[i] = (attributes.Length > 0) ? attributes[0].Description : entries[i].Trim();
            }

            return String.Join(", ", description);
        }

        private static IList<EnumItem> GetListEnumItens<T>(T enumerator, bool sort, bool useDescriptionAttributeIfHasOne = false)
        {
            List<EnumItem> result = null;

            var enumType = enumerator.GetType();

            if (!enumType.IsEnum)
                return result;

            var names = Enum.GetNames(enumType);

            if (names.Length > 0)
            {
                result = new List<EnumItem>(names.Length);

                foreach (var item in names)
                {
                    var itemEnumType = enumType.Assembly.GetType(enumType.FullName);
                    var enumItem1 = itemEnumType.GetField(item);

                    var intValue = (int)enumItem1.GetValue(itemEnumType);
                    var stringValue = enumItem1.GetValue(itemEnumType).ToString();

                    var itemInstance = (T)Enum.ToObject(enumType, intValue);

                    // Get instance of the attribute.
                    var descriptions = itemInstance.GetType().GetField(item).GetCustomAttributes(typeof(DescriptionAttribute), false);
                    var description = (useDescriptionAttributeIfHasOne && (descriptions.Count() > 0)) ? ((DescriptionAttribute)descriptions.First()).Description : item;

                    // Get instance of the Group attribute.
                    var groupAttributes = itemInstance.GetType().GetField(item).GetCustomAttributes(typeof(PermissionGroupAttribute), false);
                    var groupCode = (groupAttributes.Count() > 0) ? (int)((PermissionGroupAttribute)groupAttributes.First()).GroupCode : 0;

                    result.Add(new EnumItem { Id = intValue, Value = stringValue, Description = description, GroupId = groupCode });
                }
            }

            if (sort && result != null)
            {
                // Cria um método anônimo para fazer a comparação entre os itens da lista e ordená-la pela descrição
                result.Sort((enum1, enum2) => enum1.Description.CompareTo(enum2.Description));
            }

            return result;
        }

        #endregion
    }

    public class EnumItem
    {
        /// <summary>
        /// O id do item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// O valor textual do item
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// O texto do atributo Description do item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Código do grupo - uso do atributo PermissionGroupAttribute do item
        /// </summary>
        public int GroupId { get; set; }
    }
}
