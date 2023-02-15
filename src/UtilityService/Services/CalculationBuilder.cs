using Microsoft.Extensions.Logging;
using System;
using UtilityService.Models;

namespace UtilityService.Services
{
    public class CalculationBuilder
    {
        private Calculations _calculations;

        private readonly ILogger _log;

        private readonly CounterValues _lastCounterValues;
        private readonly CounterValues _thisСounterValues;
        private readonly Coefficients _coefficients;

        public CalculationBuilder(ILogger log, CounterValues lastCounterValues, CounterValues thisCounterValues, Coefficients coefficients) 
        {
            _log = log;

            _lastCounterValues = lastCounterValues;
            _thisСounterValues = thisCounterValues;
            _coefficients = coefficients;

            _calculations = new Calculations()
            {
                SettlementDate = DateTime.Now
            };
        }

        public CalculationBuilder BathroomCalculations()
        {
            var _bathroomDeltaColdWater = GetDeltaBathroomColdWater();
            var _bathroomDeltaHotWater = GetDeltaBathroomHotWater();

            _calculations.BathroomDeltaColdWater = _bathroomDeltaColdWater;
            _calculations.BathroomDeltaHotWater = _bathroomDeltaHotWater;

            return this;
        }

        public CalculationBuilder KitchenCalculations()
        {
            var _kitchenDeltaColdWater = GetDeltaKitchenColdWater();
            var _kitchenDeltaHotWater = GetDeltaKitchenHotWater();

            _calculations.KitchenDeltaColdWater = _kitchenDeltaColdWater;
            _calculations.KitchenDeltaHotWater = _kitchenDeltaHotWater;

            return this;
        }

        public CalculationBuilder SumWaterCalculations()
        {
            var _sumDeltaColdWater = GetDeltaColdWater();
            var _calculateColdWater = CalculateColdWater();

            var _sumDeltaHotWater = GetDeltaHotWater();
            var _calculateHotWater = CalculateHotWater();

            _calculations.SumDeltaColdWater = _sumDeltaColdWater;
            _calculations.CalculateColdWater = Math.Round(_calculateColdWater, 2);

            _calculations.SumDeltaHotWater = _sumDeltaHotWater;
            _calculations.CalculateHotWater = Math.Round(_calculateHotWater, 2);

            return this;
        }

        public CalculationBuilder ElectricityCalculates()
        {
            var _calculateElectricityT1 = CalculateElectricityT1();
            var _electricityDeltaT1 = GetDeltaElectricityT1();
            var _calculateElectricityT2 = CalculateElectricityT2();
            var _electricityDeltaT2 = GetDeltaElectricityT2();
            var _calculateElectricityT3 = CalculateElectricityT3();
            var _electricityDeltaT3 = GetDeltaElectricityT3();
            var _calculateAllElectricity = CalculateAllElectricity();

            _calculations.CalculateElectricityT1 = Math.Round(_calculateElectricityT1, 2);
            _calculations.ElectricityDeltaT1 = _electricityDeltaT1;
            _calculations.CalculateElectricityT2 = Math.Round(_calculateElectricityT2, 2);
            _calculations.ElectricityDeltaT2 = _electricityDeltaT2;
            _calculations.CalculateElectricityT3 = Math.Round(_calculateElectricityT3, 2);
            _calculations.ElectricityDeltaT3 = _electricityDeltaT3;
            _calculations.CalculateAllElectricity = Math.Round(_calculateAllElectricity, 2);

            return this;
        }

        public CalculationBuilder AllCalculate()
        {
            var _calculateSum = CalculateSum();

            _calculations.CalculateSum = Math.Round(_calculateSum, 2);

            return this;
        }


        public Calculations Build()
        {
            return _calculations;
        }

        #region приватные методы расчета.

        private int GetDeltaBathroomHotWater()
        {
            var result = _thisСounterValues.BathroomHotWater - _lastCounterValues.BathroomHotWater;
            if (result <= 0)
            {
                _log.LogTrace($"Дельта горячей воды с/у {result}, будет возвращен 0");
                return 0;
            }

            return result;
        }

        private int GetDeltaBathroomColdWater()
        {
            var result = _thisСounterValues.BathroomColdWater - _lastCounterValues.BathroomColdWater;
            if (result <= 0)
            {
                _log.LogTrace($"Дельта холодной воды с/у {result}, будет возвращен 0");
                return 0;
            }

            return result;
        }

        private int GetDeltaKitchenHotWater()
        {
            var result = _thisСounterValues.KitchenHotWater - _lastCounterValues.KitchenHotWater;
            if (result <= 0)
            {
                _log.LogTrace($"Дельта горячей воды кухни {result}, будет возвращен 0");
                return 0;
            }

            return result;
        }

        private int GetDeltaKitchenColdWater()
        {
            var result = _thisСounterValues.KitchenColdWater - _lastCounterValues.KitchenColdWater;
            if (result <= 0)
            {
                _log.LogTrace($"Дельта холодной воды кухни {result}, будет возвращен 0");
                return 0;
            }

            return result;
        }

        private int GetDeltaColdWater()
        {
            return GetDeltaBathroomColdWater() + GetDeltaKitchenColdWater();
        }

        private int GetDeltaHotWater()
        {
            return GetDeltaBathroomHotWater() + GetDeltaKitchenHotWater();
        }

        private int GetDeltaElectricityT1()
        {
            return _thisСounterValues.ElectricityT1Value - _lastCounterValues.ElectricityT1Value;

        }

        private int GetDeltaElectricityT2()
        {
            return _thisСounterValues.ElectricityT2Value - _lastCounterValues.ElectricityT2Value;
        }

        private int GetDeltaElectricityT3()
        {
            return _thisСounterValues.ElectricityT3Value - _lastCounterValues.ElectricityT3Value;

        }

        private double CalculateColdWater()
        {
            return (_coefficients.DrinkingWater + _coefficients.WaterDisposal) * GetDeltaColdWater();

        }

        private double CalculateHotWater()
        {
            return _coefficients.HotWater * GetDeltaHotWater();
        }

        private double CalculateElectricityT1()
        {
            return GetDeltaElectricityT1() * _coefficients.ElectricityT1;
        }

        private double CalculateElectricityT2()
        {
            return GetDeltaElectricityT2() * _coefficients.ElectricityT2;
        }

        private double CalculateElectricityT3()
        {
            return GetDeltaElectricityT3() * _coefficients.ElectricityT3;
        }

        private double CalculateAllElectricity()
        {
            return CalculateElectricityT1() +
                   CalculateElectricityT2() +
                   CalculateElectricityT3();
        }

        private double CalculateSum()
        {
            return CalculateColdWater() +
                   CalculateHotWater() +
                   CalculateAllElectricity();

        }

        #endregion
    }
}
