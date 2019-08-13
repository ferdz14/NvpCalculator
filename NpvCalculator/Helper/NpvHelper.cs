using NpvCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NpvCalculator.Helper
{
    public class NpvHelper
    {
        private readonly NpvDBEntities _npvDBEntities;

        public NpvHelper()
        {
            _npvDBEntities = new NpvDBEntities();
        }

        public NpvHelper(NpvDBEntities npvDBEntities)
        {
            _npvDBEntities = npvDBEntities;
        }

        public async Task<NpvChartModel> GetNpvByIDAsync(int id)
        {
            var result = new NpvChartModel();

            await Task.Run(() =>
            {
                var nvpModel = GetCalculationByID(id);
                var cashFlows = nvpModel.ListOfCashFlows;
                cashFlows.Insert(0, nvpModel.InitialInvestment);

                var discountRate = nvpModel.LowerBoundDiscountRate;
                while (discountRate <= nvpModel.UpperBoundDiscountRate)
                {
                    result.Labels.Add($"{discountRate}%");
                    result.Values.Add(CaculateNpv(cashFlows, discountRate));

                    discountRate = Math.Round(discountRate + nvpModel.DiscountRateIncrement, 2);
                };
            });

            return result;
        }

        public async Task<NpvChartModel> GetNpvResultAsync(NpvModel model)
        {
            var result = new NpvChartModel();

            await Task.Run(() =>
            {
                if (!model.CalculationID.HasValue)
                {
                    var calculationModel = InsertCalculation(model);
                    InsertCashFlows(calculationModel.CalculationID, model.CommaDelimetedCashFlows);
                }

                var cashFlows = model.CommaDelimetedCashFlows.Split(',').Select(Double.Parse).ToList();
                cashFlows.Insert(0, model.InitialInvestment);

                var discountRate = model.LowerBoundDiscountRate;
                while (discountRate <= model.UpperBoundDiscountRate)
                {
                    result.Labels.Add($"{discountRate}%");
                    result.Values.Add(CaculateNpv(cashFlows, discountRate));

                    discountRate = Math.Round(discountRate + model.DiscountRateIncrement, 2);
                };
            });

            return result;
        }

        private double CaculateNpv(List<double> cashFlows, double discountRate)
        {
            var totalDiscountedCashFlow = cashFlows.Select((c, n) => c / Math.Pow(1 + (discountRate / 100), n)).Sum();
            var discountedCashFlow = Math.Round(totalDiscountedCashFlow, 2);

            return discountedCashFlow;
        }

        private NpvModel GetCalculationByID(int calculationID)
        {
            var calculation = _npvDBEntities.Calculations.SingleOrDefault(b => b.CalculationID == calculationID);
            var npvModel = ConvertCalculationToVM(calculation);
            var cashFlows = _npvDBEntities.CashFlows.Where(i => i.CalculationID == calculationID).Select(i => i.CashFlowValue).ToList();
            npvModel.ListOfCashFlows = cashFlows;
            npvModel.CommaDelimetedCashFlows = string.Join(",", cashFlows);

            return npvModel;
        }

        private Calculation InsertCalculation(NpvModel model)
        {
            var calculation = new Calculation()
            {
                LowerBoundDiscountRate = model.LowerBoundDiscountRate,
                UpperBoundDiscountRate = model.UpperBoundDiscountRate,
                DiscountRateIncrement = model.DiscountRateIncrement,
                InitialInvestment = model.InitialInvestment
            };

            _npvDBEntities.Calculations.Add(calculation);
            _npvDBEntities.SaveChanges();

            return calculation;
        }

        private NpvModel ConvertCalculationToVM(Calculation model)
        {
            var npvModel = new NpvModel();
            npvModel.CalculationID = model.CalculationID;
            npvModel.LowerBoundDiscountRate = model.LowerBoundDiscountRate;
            npvModel.UpperBoundDiscountRate = model.UpperBoundDiscountRate;
            npvModel.DiscountRateIncrement = model.DiscountRateIncrement;
            npvModel.InitialInvestment = model.InitialInvestment;

            return npvModel;
        }

        private void InsertCashFlows(int calculationID, string commaDelimitedCashFlow)
        {
            foreach (var cashFlowValue in commaDelimitedCashFlow.Split(',').Select(Double.Parse))
            {
                var cashflow = new CashFlow()
                {
                    CalculationID = calculationID,
                    CashFlowValue = cashFlowValue
                };

                _npvDBEntities.CashFlows.Add(cashflow);
            }

            _npvDBEntities.SaveChanges();
        }
    }
}