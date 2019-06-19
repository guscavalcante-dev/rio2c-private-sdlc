using OfficeOpenXml;
using PlataformaRio2C.Application.Dtos;
using PlataformaRio2C.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.Interfaces.Services
{
    public interface IApiSymplaAppService 
    {
        Task<bool> ConfirmPaymentByCnpj(string email, string cnpj);
        Task<bool> ConfirmPaymentByCompanyName(string email, string companyName);
        Task<FinancialReportAppViewModel> GetReportSalesByCategory();
        Task<ExcelPackage> ExportReportSales();
        Task<List<ParticipantSympla>> GetReportSales();

        Task<FinancialReportAppViewModel> GetReportSalesByRegion();
        Task<FinancialReportAppViewModel> GetReportSalesByPeriod();

        Task<FinancialReportAppViewModel> GetReportSalesByTypeOfPayment();
        Task<ExcelPackage> ExportReportSalesByCategory();

        Task<bool> ConfirmUserAllowedFinancialReport(string email);
    }
}
