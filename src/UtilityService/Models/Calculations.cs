using System;

namespace UtilityService.Models
{
    /// <summary>
    /// Класс описывает расчет, полученый по показанием счетчика.
    /// </summary>
    public class Calculations
    {
        /// <summary>
        /// Id для EF.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата проведения расчета.
        /// </summary>
        public DateTime SettlementDate { get; set; }

        /// <summary>
        /// Количество потраченых кубов холодной воды на кухне.
        /// </summary>
        public int KitchenDeltaColdWater { get; set; }

        /// <summary>
        /// Количество потраченых кубов горячей воды на кухне.
        /// </summary>
        public int KitchenDeltaHotWater { get; set; }

        /// <summary>
        /// Количество потраченых кубов холодной воды в с/у.
        /// </summary>
        public int BathroomDeltaColdWater { get; set; }

        /// <summary>
        /// Количество потраченых кубов горячей воды в с/у.
        /// </summary>
        public int BathroomDeltaHotWater { get; set; }

        /// <summary>
        /// Сумарное количество кубов потраченой холодной воды. 
        /// </summary>
        public int SumDeltaColdWater { get; set; }

        /// <summary>
        /// Стоимость потраченой холодной воды.
        /// </summary>
        public double CalculateColdWater { get; set; }

        /// <summary>
        /// Сумарное количество кубов потраченой горячей воды. 
        /// </summary>
        public int SumDeltaHotWater { get; set; }

        /// <summary>
        /// Стоимость потраченой горячей воды.
        /// </summary>
        public double CalculateHotWater { get; set; }

        /// <summary>
        /// Количество потраченого Т1 электричества.
        /// </summary>
        public int ElectricityDeltaT1 { get; set; }

        /// <summary>
        /// Стоимость потраченого Т1 электричества.
        /// </summary>
        public double CalculateElectricityT1 { get; set; }
       
        /// <summary>
        /// Количество потраченого Т1 электричества.
        /// </summary>
        public int ElectricityDeltaT2 { get; set; }

        /// <summary>
        /// Стоимость потраченого Т2 электричества.
        /// </summary>
        public double CalculateElectricityT2 { get; set; }

        /// <summary>
        /// Количество потраченого Т1 электричества.
        /// </summary>
        public int ElectricityDeltaT3 { get; set; }

        /// <summary>
        /// Стоимость потраченого Т2 электричества.
        /// </summary>
        public double CalculateElectricityT3 { get; set; }

        /// <summary>
        /// Сумарная стоимость потраченого электричества.
        /// </summary>
        public double CalculateAllElectricity { get; set; }

        /// <summary>
        /// Сумарная стоимость к/у.
        /// </summary>
        public double CalculateSum { get; set; }

        /// <summary>
        /// Ссылка на таблицу Coefficients.
        /// </summary>
        public Coefficients Coefficients { get; set; }

        /// <summary>
        /// Ссылка на таблицу CounterValues.
        /// </summary>
        public CounterValues CounterValues { get; set; }
    }
}
