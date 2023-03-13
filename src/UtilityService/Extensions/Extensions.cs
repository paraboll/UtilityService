using Microsoft.Extensions.Logging;
using System;
using UtilityService.Models;

namespace UtilityService.Extensions
{
    public static class Extensions
    {
        public static bool Overwrite(this CurrentCoefficients currentCoefficient, CurrentCoefficients coefficient)
        {
            var isModification = false;

            if (coefficient.DrinkingWater > 0)
            {
                currentCoefficient.DrinkingWater = coefficient.DrinkingWater;
                isModification = true;
            }
                
            if (coefficient.HotWater > 0)
            {
                currentCoefficient.HotWater = coefficient.HotWater;
                isModification = true;
            }
                
            if (coefficient.WaterDisposal > 0)
            {
                currentCoefficient.WaterDisposal = coefficient.WaterDisposal;
                isModification = true;
            }
                
            if (coefficient.ElectricityT1 > 0)
            {
                currentCoefficient.ElectricityT1 = coefficient.ElectricityT1;
                isModification = true;
            }
                
            if (coefficient.ElectricityT2 > 0)
            {
                currentCoefficient.ElectricityT2 = coefficient.ElectricityT2;
                isModification = true;
            }
               
            if (coefficient.ElectricityT3 > 0)
            {
                currentCoefficient.ElectricityT3 = coefficient.ElectricityT3;
                isModification = true;
            }
                

            return isModification;
        }

        //TODO: переписать проверку на прошлый месяц.
        public static void CheckCounterValues(this CounterValues counterValues, ILogger log)
        {
            string errorString = string.Empty;

            if (counterValues.KitchenHotWater <= 0)
                errorString += "Показание горячей воды кухни заполнено не корректно.";
            if (counterValues.KitchenColdWater <= 0)
                errorString += "Показание холодной воды кухни заполнено не корректно.";
            if (counterValues.BathroomHotWater <= 0)
                errorString += "Показание горячей воды ванны заполнено не корректно.";
            if (counterValues.BathroomColdWater <= 0)
                errorString += "Показание холодной воды ванны заполнено не корректно.";
            if (counterValues.ElectricityT1Value <= 0)
                errorString += "Показание электричества Т1 заполнено не корректно.";
            if (counterValues.ElectricityT2Value <= 0)
                errorString += "Показание электричества Т2 заполнено не корректно.";
            if (counterValues.ElectricityT3Value <= 0)
                errorString += "Показание электричества Т3 заполнено не корректно.";

            if (!string.IsNullOrEmpty(errorString))
            {
                log.LogTrace(errorString);
                throw new ArgumentException(errorString);
            }
        }

        public static Coefficients Convert(this CurrentCoefficients currentCoefficient)
        {
            return new Coefficients()
            {
                DrinkingWater = currentCoefficient.DrinkingWater,
                WaterDisposal = currentCoefficient.WaterDisposal,
                HotWater = currentCoefficient.HotWater,
                ElectricityT1 = currentCoefficient.ElectricityT1,
                ElectricityT2 = currentCoefficient.ElectricityT2,
                ElectricityT3 = currentCoefficient.ElectricityT3,
            };
        }
    }
}
