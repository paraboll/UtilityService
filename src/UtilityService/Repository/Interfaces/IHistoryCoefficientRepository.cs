using System.Collections.Generic;
using UtilityService.Models;

namespace UtilityService.Repository.Interfaces
{
    public interface IHistoryCoefficientRepository
    {
        IEnumerable<Coefficients> GetAllCoefficients();
        Coefficients GetCoefficientsById(int id);
        void AddCoefficients(Coefficients coefficients);
    }
}
