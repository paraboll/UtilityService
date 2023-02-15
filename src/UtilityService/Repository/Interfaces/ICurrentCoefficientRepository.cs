using UtilityService.Models;

namespace UtilityService.Repository.Interfaces
{
    public interface ICurrentCoefficientRepository
    {
        CurrentCoefficients GetCurrentCoefficients();
        void SetCurrentCoefficients(CurrentCoefficients сoefficients);
    }
}
