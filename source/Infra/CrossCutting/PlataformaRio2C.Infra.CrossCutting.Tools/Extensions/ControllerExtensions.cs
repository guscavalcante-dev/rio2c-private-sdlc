using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Text;
using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Setar os valores da paginação para capturar posteriormente na view
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="qtdItensNaPagina">Quantidade de itens apresentados em uma pagina</param>
        /// <param name="paginaAtual">Página atual da paginação</param>
        /// <param name="totalItensEncontrados">Quantidade total de itens encontrados na consulta</param>
        public static void SetValuesPaging(this Controller controller, int qtdItensNaPagina, int paginaAtual, int totalItensEncontrados, string named = "")
        {
            var pagingOf = string.Format("paging{0}-Of", named);
            var pagingTo = string.Format("paging{0}-To", named);
            var pagingTotal = string.Format("paging{0}-Total", named);

            var of = (qtdItensNaPagina * (paginaAtual - 1)) + 1;
            var to = (of + (qtdItensNaPagina - 1)) <= totalItensEncontrados ? (of + (qtdItensNaPagina - 1)) : totalItensEncontrados;
            controller.TempData[pagingOf] = of;
            controller.TempData[pagingTo] = to;
            controller.TempData[pagingTotal] = totalItensEncontrados;
        }

        public static void Message(this Controller controller, MessageType type, string message)
        {
            controller.TempData["MessageText"] = message;
            controller.TempData["MessagetYPE"] = type;
        }

        public static void Message(this Controller controller, MessageType type, string format, params object[] args)
        {
            controller.TempData["MessageText"] = string.Format(format, args);
            controller.TempData["MessagetYPE"] = type;
        }

        /// <summary>
        /// retorna o string de uma partial view renderizada.
        /// </summary>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <param name="viewName">Nome da partial view.</param>
        /// <returns>String</returns>
        public static string RenderPartialViewToString(this Controller controller, string viewName)
        {
            return controller.RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        /// retorna o string de uma partial view renderizada.
        /// </summary>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <param name="viewName">Nome da partial view.</param>
        /// <param name="model">objeto de model para polular a partial view.</param>
        /// <returns>String</returns>
        public static string RenderPartialViewToString(this Controller controller, string viewName, object model)
        {
            try
            {
                using (var sw = new StringWriter())
                {
                    if (model != null)
                    {
                        controller.ViewData.Model = model;
                    }

                    var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Verifica se há erros no resultado da validação informado, e se houver adiciona os erros ao ModelState da requisição
        /// </summary>
        /// <param name="modelStateDictionar">Dicionário modelstate</param>
        /// <param name="brokenRules">Coleção de resultados de validação</param>
        /// <returns>true se teve algum error e false se não teve erros</returns>
        public static bool AddBrokenRules(this ModelStateDictionary modelStateDictionary, IEnumerable<ValidationResultExtend> brokenRules)
        {
            foreach (var brokenRule in brokenRules.ToList())
            {
                if (brokenRule.MemberNames != null && brokenRule.MemberNames.Any())
                {
                    foreach (var memberName in brokenRule.MemberNames)
                    {
                        modelStateDictionary.AddModelError(memberName, brokenRule.ErrorMessage);
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError(string.Empty, brokenRule.ErrorMessage);
                }
            }

            return brokenRules.Any();
        }

        /// <summary>
        /// Verifica se há erros no resultado da validação informado, e se houver adiciona os erros ao ModelState da requisição.
        /// Utilização de resource para retorno das mensagens de validação. O objeto de validação que deveria conter a mensagem,
        /// terá no lugar da mensagem o codigo(key) da mensagem no arquivo de resource.
        /// </summary>
        /// <param name="modelStateDictionar">Dicionário modelstate.</param>
        /// <param name="brokenRules">Coleção de resultados de validação.</param>
        /// <param name="resourceManager">Arquivo de resource onde estão as mensagens de erro.</param>
        /// <returns>true se teve algum error e false se não teve erros</returns>
        public static bool AddBrokenRules(this ModelStateDictionary modelStateDictionary, IEnumerable<ValidationResultExtend> brokenRules, ResourceManager resourceManager)
        {
            foreach (var brokenRule in brokenRules.ToList())
            {
                var errorMessage = resourceManager.GetString(brokenRule.ErrorMessage);
                errorMessage = errorMessage ?? brokenRule.ErrorMessage;
                errorMessage = brokenRule.DataValue != null ? string.Format(errorMessage, brokenRule.DataValue) : errorMessage;

                if (brokenRule.MemberNames != null && brokenRule.MemberNames.Any())
                {
                    foreach (var memberName in brokenRule.MemberNames)
                    {
                        modelStateDictionary.AddModelError(memberName, errorMessage);
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError(string.Empty, errorMessage);
                }
            }

            return brokenRules.Any();
        }

        /// <summary>
        /// Verifica se há erros no resultado da validação informado, e se houver adiciona os erros ao ModelState da requisição.
        /// Utilização de resource para retorno das mensagens de validação. O objeto de validação que deveria conter a mensagem,
        /// terá no lugar da mensagem o codigo(key) da mensagem no arquivo de resource, será passado também o codigo(key) de um
        /// segundo resouce, este contendo as labels para as propriedades.
        ///
        /// Sequencia dos parametros para a formação do string de mensagem
        /// {0} -> valor do resource de label
        /// {1} -> valor do objeto informado no validadeResultExtend
        /// </summary>
        /// <param name="modelStateDictionary">Dicionário modelstate.</param>
        /// <param name="brokenRules">Coleção de resultados de validação.</param>
        /// <param name="labelsResource">Arquivo de resource onde estão as labels dos atributos com erro de validação.</param>
        /// <param name="messagesResource">Arquivo de resource onde estão as mensagens de erro.</param>
        /// <returns></returns>
        public static bool AddBrokenRules(this ModelStateDictionary modelStateDictionary, IEnumerable<ValidationResultExtend> brokenRules, ResourceManager labelsResource, ResourceManager messagesResource)
        {
            foreach (var brokenRule in brokenRules.ToList())
            {
                var errorMessage = messagesResource.GetString(brokenRule.ErrorMessage);
                errorMessage = errorMessage ?? brokenRule.ErrorMessage;

                if (brokenRule.MemberNames != null && brokenRule.MemberNames.Any())
                {
                    foreach (var memberName in brokenRule.MemberNames)
                    {
                        string label = null;
                        try { label = labelsResource.GetString(memberName); }
                        catch (Exception) { label = string.Empty; }

                        string value = (brokenRule.DataValue != null) ? brokenRule.DataValue.ToString() : string.Empty;

                        modelStateDictionary.AddModelError(memberName, string.Format(errorMessage, label, value));
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError(string.Empty, errorMessage);
                }
            }

            return brokenRules.Any();
        }

        /// <summary>
        /// Verifica se existe no TempData do controle falhas de validação e adiciona as mesmas ao ModelState do controller
        /// </summary>
        /// <typeparam name="T">Classe do ViewModel a ser validada</typeparam>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <param name="labelsResource">Resorce de Labels</param>
        /// <param name="messagesResource">Resorce de Mensagens</param>
        public static void AddBrokenRulesFromTempData<T>(this Controller controller, ResourceManager labelsResource, ResourceManager messagesResource)
        {
            if (controller.TempData[typeof(T).Name] != null)
            {
                var brokenRules = (IEnumerable<ValidationResultExtend>)controller.TempData[typeof(T).Name];

                foreach (var brokenRule in brokenRules.ToList())
                {
                    var errorMessage = messagesResource.GetString(brokenRule.ErrorMessage);
                    errorMessage = errorMessage ?? brokenRule.ErrorMessage;

                    if (brokenRule.MemberNames != null && brokenRule.MemberNames.Any())
                    {
                        foreach (var memberName in brokenRule.MemberNames)
                        {
                            string label = null;
                            try { label = labelsResource.GetString(memberName.Substring(memberName.LastIndexOf("."))); }
                            catch (Exception) { label = string.Empty; }

                            string value = (brokenRule.DataValue != null) ? brokenRule.DataValue.ToString() : string.Empty;

                            controller.ModelState.AddModelError(memberName, string.Format(errorMessage, label, value));
                        }
                    }
                    else
                    {
                        controller.ModelState.AddModelError(string.Empty, errorMessage);
                    }
                }
            }
        }

        /// <summary>
        /// Armazena as falhas de validação ao TempData do controller
        /// </summary>
        /// <typeparam name="T">Classe do ViewModel a ser validada</typeparam>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <param name="brokenRulesResult">Enumerable de falhas de validação</param>
        public static void AddValidationResultsToTempData<T>(this Controller controller, IEnumerable<ValidationResultExtend> brokenRulesResult)
        {
            controller.TempData[typeof(T).Name] = brokenRulesResult;
        }

        /// <summary>
        /// Retorna string com relação de erros de validação formatadas como html, com um <br/> após cada mensagem
        /// </summary>
        /// <param name="modelStateDictionary">dicionario de atributos do model que popula a view</param>
        /// <returns></returns>
        public static string ErrorMessage(this ModelStateDictionary modelStateDictionary)
        {
            var message = new StringBuilder();

            foreach (var error in modelStateDictionary.Keys.Where(key => modelStateDictionary[key].Value == null).SelectMany(key => modelStateDictionary[key].Errors))
            {
                message.AppendFormat("{0}<br/>", error.ErrorMessage);
            }

            return message.ToString();
        }

        /// <summary>
        /// Cria uma variavel de mensagem de status no tempdata do controller.
        /// Como não é informado o tipo de mensagem, o retorno é de sucesso.
        /// </summary>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <param name="message">Mensagem a ser apresentada na view</param>
        public static void StatusMessage(this Controller controller, string message)
        {
            controller.StatusMessage(message, StatusMessageType.Success);
        }

        /// <summary>
        /// Cria uma variavel de mensagem de status no tempdata do controller
        /// </summary>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <param name="message">Mensagem a ser apresentada na view</param>
        /// <param name="statusMessageType">Tipo de mensagem do status</param>
        public static void StatusMessage(this Controller controller, string message, StatusMessageType statusMessageType)
        {
            List<object> statusMessages;

            if (controller.TempData["StatusMessages"] == null)
            {
                statusMessages = new List<object>();
                statusMessages.Add(new { Type = statusMessageType.ToString().ToLower(), Message = message });

                controller.TempData["StatusMessages"] = statusMessages;
            }
            else
            {
                statusMessages = controller.TempData.ContainsKey("StatusMessages") ? controller.TempData["StatusMessages"] as List<object> : new List<object>();
                statusMessages.Add(new { Type = statusMessageType.ToString().ToLower(), Message = message });
            }

            controller.TempData["StatusMessageText"] = message;
            controller.TempData["StatusMessageCssClass"] = statusMessageType.ToString().ToLower();
        }        

        /// <summary>
        /// Cria uma variavel de mensagem de status no tempdata do controller
        /// </summary>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <param name="message">Mensagem a ser apresentada na view</param>
        /// <param name="statusMessageType">Tipo de mensagem do status</param>
        public static void StatusMessageModal(this Controller controller, string message, StatusMessageType statusMessageType)
        {
            List<object> statusMessagesModal;

            if (controller.TempData["StatusMessageModal"] == null)
            {
                statusMessagesModal = new List<object>();
                statusMessagesModal.Add(new { Type = statusMessageType.ToString().ToLower(), Message = message });

                controller.TempData["StatusMessageModal"] = statusMessagesModal;
            }
            else
            {
                statusMessagesModal = controller.TempData.ContainsKey("StatusMessageModal") ? controller.TempData["StatusMessageModal"] as List<object> : new List<object>();
                statusMessagesModal.Add(new { Type = statusMessageType.ToString().ToLower(), Message = message });
            }

            controller.TempData["StatusMessageModalText"] = message;
            controller.TempData["StatusMessageModalCssClass"] = statusMessageType.ToString().ToLower();
        }

        /// <summary>
        /// Cria uma variavel de mensagem de status no tempdata do controller.
        /// Como não é informado o tipo de mensagem, o retorno é de sucesso.
        /// Usado para partiais views.
        /// </summary>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <param name="message">Mensagem a ser apresentada na view</param>
        public static void StatusMessagePartial(this Controller controller, string message)
        {
            controller.StatusMessagePartial(message, StatusMessageType.Success);
        }

        /// <summary>
        /// Cria uma variavel de mensagem de status no tempdata do controller.
        /// Usado para partiais views.
        /// </summary>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <param name="message">Mensagem a ser apresentada na view</param>
        /// <param name="statusMessageType">Tipo de mensagem do status</param>
        public static void StatusMessagePartial(this Controller controller, string message, StatusMessageType statusMessageType)
        {
            controller.TempData["StatusMessageTextPartial"] = message;
            controller.TempData["StatusMessageCssClassPartial"] = statusMessageType.ToString().ToLower();
        }

        /// <summary>
        /// Capturar a mensagem de status posta no tempdata do controller.
        /// </summary>
        /// <param name="controller">Controler corrente - usar o this.</param>
        /// <returns>String vazia ou string com mensagem de status.</returns>
        public static string GetStatusMessage(this Controller controller)
        {
            return controller.TempData["StatusMessageText"].ToString();
        }

        /// <summary>
        /// Método de extensão que retona enumerado de objetos para usar em autocomplete e dropdownlist,
        /// a partir de uma lista tipada, uma expression func para a propriedade de id
        /// e uma expression func para propriedade de label.
        /// </summary>
        /// <typeparam name="TModel">Tipo da lista de origem</typeparam>
        /// <typeparam name="TPropertyId">tipo da propriedade e id</typeparam>
        /// <typeparam name="TPropertyLabel">Tipo da propriedade de label</typeparam>
        /// <param name="list">Enumerado de origem</param>
        /// <param name="id">Expression Func para a propriedade de id.</param>
        /// <param name="label">Expression Func para a propriedade de Label.</param>
        /// <returns>enumerable de objetos com o formato { id = ?, label = ? }</returns>
        public static IEnumerable<object> ToAutoCompleteList<TModel, TPropertyId, TPropertyLabel>(this IEnumerable<TModel> list, Expression<Func<TModel, TPropertyId>> id, Expression<Func<TModel, TPropertyLabel>> label)
        {
            var functionId = id.Compile();
            var functionLabel = label.Compile();

            return (from item in list select new { id = functionId.Invoke(item), label = functionLabel.Invoke(item) }).ToList();
        }

        /// <summary>
        /// Returna dicionário com as falhas de validação do modelstate.
        /// Retorna no value o dicionario uma lista com o(s) erro(s) de validação para cada propriedade com erro.
        /// </summary>
        /// <param name="modelStateDictionary">modelstate da view</param>
        /// <returns>Dicionário com o nome da propriedade como key e uma lista com todas as mensagems de erro paraa propriedade como value.</returns>
        public static IDictionary<string, IList<string>> GetModelStateErros(this ModelStateDictionary modelStateDictionary)
        {
            var result = new Dictionary<string, IList<string>>();
            foreach (var key in modelStateDictionary.Keys)
            {
                if (modelStateDictionary[key].Errors.Any())
                {
                    var erros = new List<string>();
                    foreach (var error in modelStateDictionary[key].Errors)
                    {
                        erros.Add(error.ErrorMessage);
                    }
                    result.Add(key, erros);
                }
            }

            return result;
        }

        /// <summary>
        /// Returna dicionário com as falhas de validação do modelstate.
        /// Retorna um único erro para cada propriedade com erro.
        /// </summary>
        /// <param name="modelStateDictionary">modelstate da view</param>
        /// <returns>Dicionário com o nome da propriedade como key e mensagem de erro como value.</returns>
        public static IDictionary<string, string> GetModelStateErrosOnlyByKey(this ModelStateDictionary modelStateDictionary)
        {
            var result = new Dictionary<string, string>();
            foreach (var key in modelStateDictionary.Keys)
            {
                if (modelStateDictionary[key].Errors.Any())
                {
                    result.Add(key, modelStateDictionary[key].Errors.First().ErrorMessage);
                }
            }
            return result;
        }

        /// <summary>
        /// Metodo que retorna uma lista com somente as mensagens de error do modelstate
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        public static IList<string> GetModelStateErrosOnlyErrors(this ModelStateDictionary modelStateDictionary)
        {
            var result = new List<string>();
            foreach (var key in modelStateDictionary.Keys)
            {
                if (modelStateDictionary[key].Errors.Any())
                {
                    result.Add(modelStateDictionary[key].Errors.First().ErrorMessage);
                }
            }
            return result;
        }

        public static void RevalidateModel(this Controller controller, object viewModel)
        {
            controller.ModelState.Clear();

            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);

            foreach (var result in validationResults)
            {
                foreach (var name in result.MemberNames)
                {
                    controller.ModelState.AddModelError(name, result.ErrorMessage);
                }
            }
        }
    }
}
