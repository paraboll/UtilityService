using System;

namespace UtilityService.Models
{
    /// <summary>
    /// Класс описывает расчетные коэффициенты.
    /// </summary>
    public class Coefficients
    {
        /// <summary>
        /// Id Для EF.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Цена питьевой воды, руб.
        /// </summary>
        public double DrinkingWater { get; set; }

        /// <summary>
        /// Цена горячей воды,  руб.
        /// </summary>
        public double HotWater { get; set; }

        /// <summary>
        /// Цена водоотведения, руб.
        /// </summary>
        public double WaterDisposal { get; set; }

        /// <summary>
        /// Цена за электричество T1.
        /// </summary>
        public double ElectricityT1 { get; set; }

        /// <summary>
        /// Цена за электричество T2.
        /// </summary>
        public double ElectricityT2 { get; set; }

        /// <summary>
        /// Цена за электричество T3.
        /// </summary>
        public double ElectricityT3 { get; set; }


        public int CalculationsId { get; set; }
        /// <summary>
        /// Ссылка на таблицу Calculations.
        /// </summary>
        public Calculations Calculations { get; set; }
    }
}
