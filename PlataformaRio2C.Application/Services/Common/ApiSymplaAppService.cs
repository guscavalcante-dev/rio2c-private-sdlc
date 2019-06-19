using Newtonsoft.Json;
using PlataformaRio2C.Application.Dtos;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PlataformaRio2C.Application.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using PlataformaRio2C.Domain.Interfaces;
using LazyCache;

namespace PlataformaRio2C.Application.Services
{
    public class ApiSymplaAppService : IApiSymplaAppService
    {
        private readonly ISystemParameterRepository _systemParameterRepository;


        public ApiSymplaAppService(ISystemParameterRepository systemParameterRepository)
        {
            _systemParameterRepository = systemParameterRepository;
        }

        public async Task<bool> ConfirmPaymentByCnpj(string email, string cnpj)
        {
            cnpj = Producer.GetLintCnpj(cnpj);
            email = email.Trim().ToLower(CultureInfo.InvariantCulture);

            string baseUrl = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaUrlBase);
            string urlPathParticipats = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaUrlPathParticipants);
            string systemHeaders = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaHeaders);
            string symplaFieldKeyCnpj = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaFieldKeyCnpj);
            string symplaOrderStatusConfirmPayment = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaOrderStatusConfirmPayment);
            string symplaTicketNameForProducer = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaTicketNameForProducer);

            if (_systemParameterRepository.Get<bool>(SystemParameterCodes.SymplaMockEnable) && _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaCnpjMock).Contains(cnpj) && _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaEmailMock).Contains(email))
            {
                return true;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();

                foreach (var item in JsonConvert.DeserializeObject<List<HeaderDtoSympla>>(systemHeaders))
                {
                    client.DefaultRequestHeaders.Add(item.Name, item.Value);
                }

                HttpResponseMessage Res = await client.GetAsync(urlPathParticipats);

                if (Res.IsSuccessStatusCode)
                {
                    var symplaInfo = JsonConvert.DeserializeObject<List<ParticipantSympla>>(Res.Content.ReadAsStringAsync().Result);

                    var collaboratorProducerSympla = symplaInfo.FirstOrDefault(e => e.email.Trim().ToLower(CultureInfo.InvariantCulture).Contains(email) && e.order_status == symplaOrderStatusConfirmPayment && e.ticket_name.Contains(symplaTicketNameForProducer) && e.custom_form.Any(c => c.name.Contains(symplaFieldKeyCnpj) && Producer.GetLintCnpj(c.value) == cnpj));

                    if (collaboratorProducerSympla.email != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> ConfirmPaymentByCompanyName(string email, string companyName)
        {
            if (companyName != null)
            {
                companyName = companyName.Trim();
            }

            email = email.Trim().ToLower(CultureInfo.InvariantCulture);

            string baseUrl = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaUrlBase);
            string urlPathParticipats = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaUrlPathParticipants);
            string systemHeaders = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaHeaders);
            string symplaFieldKeyCompanyName = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaFieldKeyCompanyName);
            string symplaOrderStatusConfirmPayment = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaOrderStatusConfirmPayment);
            string symplaTicketNameForProducer = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaTicketNameForProducer);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();

                foreach (var item in JsonConvert.DeserializeObject<List<HeaderDtoSympla>>(systemHeaders))
                {
                    client.DefaultRequestHeaders.Add(item.Name, item.Value);
                }

                HttpResponseMessage Res = await client.GetAsync(urlPathParticipats);

                if (Res.IsSuccessStatusCode)
                {
                    var symplaInfo = JsonConvert.DeserializeObject<List<ParticipantSympla>>(Res.Content.ReadAsStringAsync().Result);

                    var collaboratorProducerSympla = symplaInfo.FirstOrDefault(e => e.email.Trim().ToLower(CultureInfo.InvariantCulture).Contains(email) && e.order_status == symplaOrderStatusConfirmPayment && e.ticket_name.Contains(symplaTicketNameForProducer) && e.custom_form.Any(c => c.name == symplaFieldKeyCompanyName && c.value == companyName));

                    if (collaboratorProducerSympla.email != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<ExcelPackage> ExportReportSales()
        {
            var participants = await GetParticipants();

            if (participants.Any())
            {
                ExcelPackage excelFile = new ExcelPackage();

                ExcelWorksheet worksheet = excelFile.Workbook.Worksheets.Add("Todos os Dados Sympla");

                int row = 1;
                int column = 1;

                worksheet.Cells[row, column].Value = string.Format("Rio2C - Relatório todos os Dados Sympla - {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                row++;


                //Label, propriedade no retorno da apiSynmpla
                var headers = new List<Tuple<string, string>> {
                    new Tuple<string, string>("event_id", "event_id"),
                    new Tuple<string, string>("order_num", "order_num"),
                    new Tuple<string, string>("order_date", "order_date"),
                    new Tuple<string, string>("order_status", "order_status"),
                    new Tuple<string, string>("transaction_type", "transaction_type"),
                    new Tuple<string, string>("order_total_sale_price", "order_total_sale_price"),
                    new Tuple<string, string>("discount_code", "discount_code"),
                    new Tuple<string, string>("ticket_name", "ticket_name"),
                    new Tuple<string, string>("ticket_sale_price", "ticket_sale_price"),
                    new Tuple<string, string>("ticket_number", "ticket_number"),
                    new Tuple<string, string>("first_name", "first_name"),
                    new Tuple<string, string>("last_name", "last_name"),
                    new Tuple<string, string>("email", "email"),
                    new Tuple<string, string>("check_in", "check_in"),
                    new Tuple<string, string>("check_in_date", "check_in_date"),
                    new Tuple<string, string>("badge_name", null),
                    new Tuple<string, string>("company_name", null),
                    new Tuple<string, string>("company_trade_name", null),
                    new Tuple<string, string>("name_of_the_group", null),
                    new Tuple<string, string>("number_of_people_in_group", null),
                    new Tuple<string, string>("cnpj", null),
                    new Tuple<string, string>("cpf", null),
                    new Tuple<string, string>("country", null),
                    new Tuple<string, string>("special_needs", null),
                    new Tuple<string, string>("data_nascimento", null),
                    new Tuple<string, string>("cep", null),
                    new Tuple<string, string>("documento_meia_entrada", null),
                    new Tuple<string, string>("banda", null)
                };

                int countHeaders = headers.Count;

                foreach (var itemHeader in headers)
                {
                    worksheet.Cells[row, column].Value = itemHeader.Item1;
                    column++;
                }

                row++;
                column = 1;

                foreach (var itemParticipant in participants)
                {

                    foreach (var itemHeader in headers)
                    {
                        if (itemHeader.Item2 != null && itemHeader.Item2 != "order_date")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.GetType().GetProperty(itemHeader.Item2).GetValue(itemParticipant, null);
                        }
                        if (itemHeader.Item2 != null && itemHeader.Item2 == "order_date")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.order_date.ToString("dd/MM/yyyy");
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "country")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Country")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "badge_name")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Badge Name")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "company_name")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Company Name")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "company_trade_name")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Company Trade Name")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "name_of_the_group")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Name of the Group")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "number_of_people_in_group")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Number of people in the Group")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "cnpj")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Company Number")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "cpf")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("CPF")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "special_needs")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("special needs")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "data_nascimento")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Data de Nascimento")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "cep")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("CEP")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "documento_meia_entrada")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Documento de Meia Entrada")).Select(c => c.value).FirstOrDefault();
                        }
                        else if (itemHeader.Item2 == null && itemHeader.Item1 == "banda")
                        {
                            worksheet.Cells[row, column].Value = itemParticipant.custom_form.Where(c => c.name.Contains("Banda")).Select(c => c.value).FirstOrDefault();
                        }

                        column++;
                    }

                    row++;
                    column = 1;
                }

                //estilizando primeira linha de titulo  
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.Font.Bold = true;
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, 1, (countHeaders)].Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                worksheet.Cells[1, 1, 1, (countHeaders)].Merge = true;

                //estilizando primeira linha de cabeçalho dos dados                
                worksheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(2).Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Row(2).Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                worksheet.Row(2).Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Row(2).Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                worksheet.Row(2).Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Row(2).Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                worksheet.Row(2).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Row(2).Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                worksheet.Row(2).Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Row(2).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(206, 206, 206));
                worksheet.Row(2).Style.Font.Bold = true;

                //
                worksheet.Cells[2, 1, 2, (countHeaders + 1)].AutoFilter = true;
                worksheet.Cells[1, 1, (participants.Count + 2), (countHeaders + 1)].Style.Font.Size = 10;
                worksheet.Cells.AutoFitColumns();

                return excelFile;
            }

            return null;
        }

        public async Task<FinancialReportAppViewModel> GetReportSalesByCategory()
        {
            var participants = await GetParticipants();

            if (participants.Any())
            {
                var groupParticipant = participants.GroupBy(e => e.ticket_name);
                var items = ItemFinancialReportAppViewModel.MapList(groupParticipant);
                return new FinancialReportAppViewModel(items);
            }

            return null;
        }

        public async Task<FinancialReportAppViewModel> GetReportSalesByPeriod()
        {
            var participants = await GetParticipants();

            if (participants.Any())
            {
                var groupParticipant = participants.GroupBy(e => e.order_date.ToString("dd/MM/yyyy"));
                var items = ItemFinancialReportAppViewModel.MapList(groupParticipant);
                return new FinancialReportAppViewModel(items);
            }

            return null;
        }

        public async Task<FinancialReportAppViewModel> GetReportSalesByTypeOfPayment()
        {
            var participants = await GetParticipants();

            if (participants.Any())
            {
                var groupParticipant = participants.GroupBy(e => e.transaction_type);
                var items = ItemFinancialReportAppViewModel.MapList(groupParticipant);
                return new FinancialReportAppViewModel(items);
            }

            return null;
        }

        public async Task<FinancialReportAppViewModel> GetReportSalesByRegion()
        {
            var participants = await GetParticipants();

            if (participants.Any())
            {
                var groupParticipant = participants.GroupBy(e => e.custom_form.Where(c => c.name.Contains("Country")).Select(c => c.value).FirstOrDefault());
                var items = ItemFinancialReportAppViewModel.MapList(groupParticipant);
                return new FinancialReportAppViewModel(items);
            }

            return null;
        }

        private async Task<List<ParticipantSympla>> GetParticipants()
        {            
            IAppCache cache = new CachingService();
            List<ParticipantSympla> participants = cache.Get<List<ParticipantSympla>>("ParticipantSympla-list");

            if (participants == null)
            {
                string baseUrl = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaUrlBase);
                string urlPathParticipats = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaUrlPathParticipants);
                string systemHeaders = _systemParameterRepository.Get<string>(SystemParameterCodes.SymplaHeaders);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    client.DefaultRequestHeaders.Clear();

                    foreach (var item in JsonConvert.DeserializeObject<List<HeaderDtoSympla>>(systemHeaders))
                    {
                        client.DefaultRequestHeaders.Add(item.Name, item.Value);
                    }

                    try
                    {
                        var res = client.GetAsync(urlPathParticipats).Result;

                        if (res.IsSuccessStatusCode)
                        {
                            participants = JsonConvert.DeserializeObject<List<ParticipantSympla>>(res.Content.ReadAsStringAsync().Result);
                            participants = cache.GetOrAdd("ParticipantSympla-list", () => participants, DateTimeOffset.Now.AddMinutes(5));
                        }
                    }
                    catch (Exception e)
                    {

                        throw;
                    }
                }
            }

            return participants;
        }

        public async Task<ExcelPackage> ExportReportSalesByCategory()
        {
            var participants = await GetParticipants();

            if (participants.Any())
            {
                var groupParticipant = participants.GroupBy(e => e.ticket_name);
                var items = ItemFinancialReportAppViewModel.MapList(groupParticipant);
                var viewModelReport = new FinancialReportAppViewModel(items);

                ExcelPackage excelFile = new ExcelPackage();

                ExcelWorksheet worksheet = excelFile.Workbook.Worksheets.Add("Todos os Dados Sympla");

                int row = 1;
                int column = 1;

                worksheet.Cells[row, column].Value = string.Format("Rio2C - Relatório vendas por categoria - {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                row++;

                worksheet.Cells[row, column].Value = "Total:";
                column++;

                worksheet.Cells[row, column].Value = "-";
                //worksheet.Cells[row, column].Formula = "SUM()"
                column++;

                worksheet.Cells[row, column].Value = "-";
                column++;

                row++;
                column = 1;

                //Label, propriedade no retorno da apiSynmpla
                var headers = new List<Tuple<string, string>> {
                    new Tuple<string, string>("Categoria", "Label"),
                    new Tuple<string, string>("Valor", "Value"),
                    new Tuple<string, string>("Quantidade", "Count")
                };

                int countHeaders = headers.Count;

                foreach (var itemHeader in headers)
                {
                    worksheet.Cells[row, column].Value = itemHeader.Item1;
                    column++;
                }

                row++;
                column = 1;

                foreach (var itemParticipant in viewModelReport.Items)
                {
                    foreach (var itemHeader in headers)
                    {
                        worksheet.Cells[row, column].Value = itemParticipant.GetType().GetProperty(itemHeader.Item2).GetValue(itemParticipant, null);
                        column++;
                    }

                    row++;
                    column = 1;
                }

                worksheet.Cells[row, column].Value = "Total:";
                column++;

                worksheet.Cells[row, column].Value = "-";
                column++;

                worksheet.Cells[row, column].Value = "-";
                column++;

                return excelFile;
            }

            return null;
        }

        public async Task<List<ParticipantSympla>> GetReportSales()
        {
            var participants = await GetParticipants();

            if (participants.Any())
            {
                return participants;
            }

            return null;
        }

        public Task<bool> ConfirmUserAllowedFinancialReport(string email)
        {
            string emailsAllowed = _systemParameterRepository.Get<string>(SystemParameterCodes.FinancialReportUsersAllowed);

            return Task.FromResult(emailsAllowed.Contains(email));
        }
    }
}
