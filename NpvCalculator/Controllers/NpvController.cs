using NpvCalculator.Helper;
using NpvCalculator.Models;
using System.Web.Mvc;

namespace NpvCalculator.Controllers
{
    public class NpvController : Controller
    {
        public ActionResult Index()
        {
            return View(new NpvModel());
        }

        public ActionResult CalculateNpv(NpvModel model)
        {
            return View(model);
        }
    }
}