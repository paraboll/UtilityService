using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityService.Models
{
    /// <summary>
    /// Класс описывает снятые показания сосчетчиков.
    /// </summary>
    public class CounterValues
    {
        /// <summary>
        /// Id для EF.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Показания счетчика горячей воды на кухне.
        /// </summary>
        public int KitchenHotWater { get; set; }

        // <summary>
        /// Показания счетчика холодной воды на кухне.
        /// </summary>
        public int KitchenColdWater { get; set; }

        /// <summary>
        /// Показания счетчика горячей воды на с/у.
        /// </summary>
        public int BathroomHotWater { get; set; }

        /// <summary>
        /// Показания счетчика холодной воды на с/у.
        /// </summary>
        public int BathroomColdWater { get; set; }

        /// <summary>
        /// Показания Т1 электрического счетчика.
        /// </summary>
        public int ElectricityT1Value { get; set; }

        /// <summary>
        /// Показания Т2 электрического счетчика.
        /// </summary>
        public int ElectricityT2Value { get; set; }

        /// <summary>
        /// Показания Т3 электрического счетчика.
        /// </summary>
        public int ElectricityT3Value { get; set; }


        public int CalculationsId { get; set; }
        /// <summary>
        /// Ссылка на таблицу Calculations.
        /// </summary>
        public Calculations Calculations { get; set; }
    }
}
