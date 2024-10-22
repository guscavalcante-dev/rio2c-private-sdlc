using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlataformaRio2C.Web.Admin.Areas.WebApi.Controllers
{
    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/financialreports")]
    public class FinancialReportController : BaseApiController
    {
        //private readonly IApiSymplaAppService _appService;

        public FinancialReportController(/*IApiSymplaAppService appService*/)
        {
            //_appService = appService;
        }

        [Route("Sales")]
        [HttpGet]        
        public async Task<IHttpActionResult> Sales()
        {
            string userEmail = User.Identity.GetUserName();

            //if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            //{
            //    return Unauthorized();
            //}

            //var result = await _appService.GetReportSales();

            //if (result != null)
            //{
            //    return await Json(result);
            //}

            return NotFound();
        }

        [Route("SalesByCategory")]
        [HttpGet]
        public async Task<IHttpActionResult> SalesByCategory()
        {
            string userEmail = User.Identity.GetUserName();

            //if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            //{
            //    return Unauthorized();
            //}

            //var result = await _appService.GetReportSalesByCategory();

            //if (result != null)
            //{
            //    return await Json(result);
            //}

            return NotFound();
        }

        [Route("SalesByRegion")]
        [HttpGet]
        public async Task<IHttpActionResult> SalesByRegion()
        {
            string userEmail = User.Identity.GetUserName();

            //if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            //{
            //    return Unauthorized();
            //}

            //var result = await _appService.GetReportSalesByRegion();

            //if (result != null)
            //{
            //    return await Json(result);
            //}

            return NotFound();
        }


        [Route("SalesByPeriod")]
        [HttpGet]
        public async Task<IHttpActionResult> SalesByPeriod()
        {
            string userEmail = User.Identity.GetUserName();

            //if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            //{
            //    return Unauthorized();
            //}

            //var result = await _appService.GetReportSalesByPeriod();

            //if (result != null)
            //{
            //    return await Json(result);
            //}

            return NotFound();
        }

        [Route("SalesByTypeOfPayment")]
        [HttpGet]
        public async Task<IHttpActionResult> SalesByTypeOfPayment()
        {
            string userEmail = User.Identity.GetUserName();

            //if (!(await _appService.ConfirmUserAllowedFinancialReport(userEmail)))
            //{
            //    return Unauthorized();
            //}

            //var result = await _appService.GetReportSalesByTypeOfPayment();

            //if (result != null)
            //{
            //    return await Json(result);
            //}

            return NotFound();
        }
    }
}
