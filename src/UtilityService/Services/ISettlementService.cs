using UtilityService.Models;

namespace UtilityService.Services
{
    public interface ISettlementService
    {
        Calculations CalculatePayment();
    }
}
