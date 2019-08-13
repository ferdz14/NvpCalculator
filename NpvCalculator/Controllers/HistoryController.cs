using NpvCalculator.Helper;
using NpvCalculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NpvCalculator.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IGridMvcHelper _gridMvcHelper = new GridMvcHelper();
        private readonly NpvDBEntities _npvDBEntities;
        private const string _historyGrid = "_HistoryGrid";

        public HistoryController()
        {
            _npvDBEntities = new NpvDBEntities();
        }

        public HistoryController(NpvDBEntities npvDBEntities)
        {
            _npvDBEntities = npvDBEntities;
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult GetGrid()
        {
            var listOfCalculationHistory = GetAllCalculationHistory();
            var grid = _gridMvcHelper.GetAjaxGrid(listOfCalculationHistory);

            return PartialView(_historyGrid, grid);
        }

        public ActionResult GridPager(int? page)
        {
            var listOfCalculationHistory = GetAllCalculationHistory();
            var grid = _gridMvcHelper.GetAjaxGrid(listOfCalculationHistory, page);
            var jsonData = _gridMvcHelper.GetGridJsonData(grid, _historyGrid, this);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private IOrderedQueryable<NpvModel> GetAllCalculationHistory()
        {
            var result = new List<NpvModel>();
            var listOfCalculations = _npvDBEntities.Calculations.ToList();

            foreach (var calculationItem in listOfCalculations)
            {
                var npvItem = new NpvModel();
                npvItem.CalculationID = calculationItem.CalculationID;
                npvItem.LowerBoundDiscountRate = calculationItem.LowerBoundDiscountRate;
                npvItem.UpperBoundDiscountRate = calculationItem.UpperBoundDiscountRate;
                npvItem.DiscountRateIncrement = calculationItem.DiscountRateIncrement;
                npvItem.InitialInvestment = calculationItem.InitialInvestment;

                result.Add(npvItem);
            }

            return result.AsQueryable().OrderByDescending(i => i.CalculationID);
        }
    }
}