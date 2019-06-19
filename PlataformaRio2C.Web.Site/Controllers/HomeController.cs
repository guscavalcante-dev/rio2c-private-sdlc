using Microsoft.AspNet.Identity;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Web.Site.Controllers
{
    
    [Authorize(Order = 1)]
    public class HomeController : Controller
    {
        private readonly IdentityAutenticationService _identityController;

        public HomeController(IdentityAutenticationService identityController)
        {
            _identityController = identityController;
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            try
            {
                int userId = User.Identity.GetUserId<int>();

                if (await _identityController.IsInRoleAsync(userId, "Player"))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (await _identityController.IsInRoleAsync(userId, "Producer"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "ProducerArea" });
                }

                return RedirectToAction("LogOff", "Account");
            }
            catch (Exception)
            {

                return RedirectToAction("LogOff", "Account");
            }
        }
    }
}