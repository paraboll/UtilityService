using Microsoft.Extensions.Logging;
using UtilityService.Models;

namespace UtilityService.Services
{
    public class SettlementService : ISettlementService
    {
        private readonly ILogger _log;

        private CounterValues _lastCounterValues;
        private CounterValues _thisСounterValues;
        private Coefficients _coefficients;

        public SettlementService(ILogger log, CounterValues lastCounterValues, CounterValues thisCounterValues, Coefficients coefficients)
        {
            _log = log;

            _lastCounterValues = lastCounterValues;
            _thisСounterValues = thisCounterValues;
            _coefficients = coefficients;
        }

        public Calculations CalculatePayment()
        {
            _log.LogTrace("Вызван метод CalculatePayment");

            var result = new CalculationBuilder(_log, _lastCounterValues, _thisСounterValues, _coefficients)
                                .BathroomCalculations()
                                .KitchenCalculations()
                                .SumWaterCalculations()
                                .ElectricityCalculates()
                                .AllCalculate()
                                .Build();

            return result;
        }

        
    }
}
